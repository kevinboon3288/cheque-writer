namespace DataServices;

public interface IDataService
{
    Cheque? GetCheques(int id);
    string? GetUserLevelNameById(int id);
    List<UserLevel>? GetUserLevels();
    User? GetUserByInfo(string? name, int userLevel);
    bool IsExistUser(string? name, int userLevel);
    List<User> GetAllUsers();
    int AddUser(string userName, string password, string? jobTitle, int userLevel, int currentUserId);
    int DeleteUser(int userId);
}