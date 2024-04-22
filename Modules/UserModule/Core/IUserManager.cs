
namespace ChequeWriter.Modules.UserModule.Core
{
    public interface IUserManager
    {
        List<UserLevel> GetUserLevels();
        bool IsValidUser(string name, string password, int userLevel);
        List<User> GetAllUsers();
    }
}