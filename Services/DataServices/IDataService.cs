
namespace DataServices
{
    public interface IDataService
    {
        Cheque? GetCheques(int id);
        List<UserLevel>? GetUserLevels();
        bool IsValidUser(string? name, string? password, int userLevel);
    }
}