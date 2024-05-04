namespace DataServices;

public interface IDataService
{
    Cheque? GetCheques(int id);
    string? GetUserLevelNameById(int id);
    List<UserLevel>? GetUserLevels();
    int GetCurrentUserId(string? name, string? password, int userLevel);
    bool IsValidUser(string? name, string? password, int userLevel);
    bool IsExistUser(string? name, int userLevel);
    List<User> GetAllUsers();
    int AddUser(string userName, string password, string? jobTitle, int userLevel, int currentUserId);
    int DeleteUser(int userId);
}