
namespace ChequeWriter.Modules.UserModule.Core
{
    public interface IUserManager
    {
        List<UserLevel> GetUserLevels();
        bool IsValidUser(string name, string password, int userLevel);
        List<User> GetAllUsers();
        int AddUser(string userName, string password, string? jobTitle, int userLevel);
        int DeletUser(User user);
    }
}