namespace ChequeWriter.Modules.UserModule.Core;

public interface IUserManager
{
    List<UserLevel> GetUserLevels();
    int? GetCurrentUserId(string name, string password, int userLevel);
    bool IsValidUser(string name, string password, int userLevel);
    bool IsExistUser(string name, int userLevel);
    List<User> GetAllUsers();
    int AddUser(string userName, string password, string? jobTitle, int userLevel, int currentUserId);
    int DeletUser(User user);
}