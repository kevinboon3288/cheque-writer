namespace DataServices;

public interface IDataService
{
    Cheque? GetCheques(int id);
    string? GetUserLevelNameById(int id);
    List<UserLevel>? GetUserLevels();
    bool IsValidUser(string? name, string? password, int userLevel);
    List<User> GetAllUsers();
    int AddUser(string userName, string password, string? jobTitle, int userLevel);
    int DeleteUser(int userId);
}