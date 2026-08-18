# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project overview

Cheque Writer is a Windows desktop WPF application (.NET 8, C#) that automates cheque writing and generates financial reports. It uses the Prism Library for MVVM + modularity, Entity Framework Core against PostgreSQL for persistence, and Unity as the DI container.

## Common commands

All commands run from the repository root (contains `ChequeWriter.sln`). This is a Windows-only WPF app (`net8.0-windows`), so builds/runs require Windows; tests can run cross-platform.

```powershell
# Restore & build the whole solution
dotnet restore ChequeWriter.sln
dotnet build ChequeWriter.sln

# Run the WPF app (Windows only)
dotnet run --project ChequeWriter.csproj

# Run all unit tests
dotnet test

# Run a single test project
dotnet test UnitTest/DataServiceUnitTests/DataServiceUnitTests.csproj
dotnet test UnitTest/CommonUtilsUnitTests/CommonUtilsUnitTests.csproj

# Run a single test by fully-qualified name
dotnet test --filter "FullyQualifiedName~ChequeDataServiceUnitTest.AddCheque_ReturnNewChequeId_WithNewChequeInfo"

# Seed/reset the database (requires a reachable PostgreSQL matching App.config)
dotnet run --project ChequeWriter.DbInitializer.Console/ChequeWriter.DbInitializer.Console.csproj
```

CI (`.github/workflows/pipelines.yml`) runs `dotnet test` then `msbuild /t:Restore` for Debug and Release on `windows-latest`, triggered on pushes/PRs to `master`.

Test stack: NUnit + NSubstitute for mocking, `Microsoft.EntityFrameworkCore.Sqlite` in-memory (`Filename=:memory:`) to fake the Postgres-backed `ChequeWriterDbContext` in `DataServiceUnitTests` (see `ChequeDataServiceUnitTest.SetUp` for the pattern: substitute `IDesignTimeDbContextFactory<ChequeWriterDbContext>`, back it with a SQLite connection, seed rows directly via the context).

## Configuration

- The DB connection string lives in `App.config` under `connectionStrings/cheque-writer-ui` (and separately in `ChequeWriter.DbInitializer.Console/App.config`). It targets PostgreSQL (`Npgsql`) via `ChequeWriterDbContextFactory`. `App.xaml.cs.ConfigureOptions` reads it from `ConfigurationManager` and registers it as `ChequeWriterOption` for DI.
- `LiveCurrencyService` (`Services/LiveCurrencyServices/LiveCurrencyService.cs`) calls `api.freecurrencyapi.com` with a hardcoded API key — treat as a placeholder/dev key, not a secret to propagate.

## Architecture

### Solution layout — layered, feature-modular

- **`ChequeWriter` (root project / shell)** — the WPF host (`App.xaml.cs`, `ChequeWriterMainWindow.xaml`). Registers services into the Unity/Prism container and declares which Prism modules load (`ConfigureModuleCatalog`). `ChequeWriter.csproj` deliberately excludes `Modules/**`, `Services/**`, `Resources/**`, `GenericModels/**`, `UnitTest/**` from compilation — those live in their own projects and are pulled in only via `ProjectReference`.
- **`Modules/*`** — one Prism module (`IModule`) per feature area: `MainModule` (shell chrome — header + module picker), `ChequeModule` (+ nested `ReportModule` type), `UserModule`, `NotificationModule`, `LiveCurrencyModule`, and `CommonModule` (shared converters/validation rules/events, no `IModule` of its own — referenced by other modules). Each module's `RegisterTypes` registers its views/view-models with `ViewModelLocationProvider` (convention-based `View`↔`ViewModel` wiring — no code-behind DI wiring needed), and `OnInitialized` registers views into named Prism regions (e.g. `HeaderContentRegion`, `ModuleContentRegion`, `UserContentRegion`, `NotificationContentRegion` — the region names are the seam between modules and are declared as `prism:RegionManager.RegionName` in XAML).
- **`Services/DataServices`** — the only project talking to the database. `IDataService`/`DataService` wrap EF Core (`ChequeWriterDbContext`) queries for both Cheque and User concerns (see the `#region ChequeModule` / `#region UserModule` split inside `DataService.cs`). Throws `DataServiceException` for invariant violations (e.g. delete of a non-existent id). `ChequeWriterDbContextFactory` implements `IDesignTimeDbContextFactory<ChequeWriterDbContext>` so the same factory is used at runtime (via DI) and by `dotnet ef` migration tooling.
- **`Services/LiveCurrencyServices`** — standalone HTTP client wrapper (`FluentHttpClient`) around a third-party currency-rate API, unrelated to `DataServices`.
- **`GenericModels/Common`** — framework-agnostic utility library (`AmountUtils` for number-to-words cheque amounts, `EncryptionToolUtils` for password hashing). No Prism/EF/WPF dependencies — reused by both modules and `ChequeWriter.DbInitializer`.
- **`ChequeWriter.DbInitializer`** — seeding logic (`DbInitializer.Seed`), invoked by the separate `ChequeWriter.DbInitializer.Console` executable.
- **`Resources`** — shared XAML `ResourceDictionary` styles/icons, referenced from module XAML via pack URIs (e.g. `/Resources;component/Styles/ControlStyles.xaml`).
- **`UnitTest/*`** — one project per thing under test (`DataServiceUnitTests` for `DataServices`, `CommonUtilsUnitTests` for `GenericModels/Common`), not mirroring the module split.

### Key patterns to follow when extending the app

- **Models exist in two layers**: EF entity models under `Services/DataServices/Models` (persistence shape, with `[Key]`/FK attributes) vs. plain view models under `Modules/*/Models` (e.g. `Modules/ChequeModule/Models/Cheque.cs`) used for binding. Managers (`ChequeManager`, `UserManager` in each module's `Core/`) translate between the two — don't bind XAML directly to `DataServices.Models` types.
- **Cross-module communication goes through Prism's `IEventAggregator`**, using events defined centrally in `Modules/CommonModule/Events/UIControlEvent.cs` (e.g. `HeaderTitleUIControlEvent`, `NotificationEvent`, `PreviewChequeEvent`). Don't wire modules together directly; publish/subscribe instead.
- **Navigation** between views within a region uses `IRegionManager.Regions["<RegionName>"].RequestNavigate(...)`, and view models implement `INavigationAware` to react to navigation (see `ChequeManagementViewModel`).
- **New modules** need: an `IModule` implementation registering views/view models and regions, a `<ProjectReference>` added to `ChequeWriter.csproj`, and a `moduleCatalog.AddModule<...>()` line in `App.xaml.cs`.
- **New DataService operations**: add to `IDataService`, implement in `DataService` (open a `ChequeWriterDbContext` via `_dbContextFactory.CreateDbContext([_connectionString])` per call, throw `DataServiceException` on invariant failures), then cover with a `DataServiceUnitTests` test using the SQLite in-memory `SetUp` pattern already established.
- **EF Core migrations** live in `Services/DataServices/Migrations`; generate new ones with `dotnet ef migrations add <Name> --project Services/DataServices/DataServices.csproj` (requires the `dotnet-ef` tool and a reachable Postgres connection string).
