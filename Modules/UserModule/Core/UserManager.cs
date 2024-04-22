namespace ChequeWriter.Modules.UserModule.Core;

public class UserManager : IUserManager
{
    private readonly IDataService _dataService;

    public UserManager(IDataService dataService)
    {
        _dataService = dataService;
    }

    public List<UserLevel> GetUserLevels()
    {
        List<UserLevel> userLevels = new();

        foreach (var userLevel in _dataService.GetUserLevels()!)
        {
            userLevels.Add(new UserLevel()
            {
                Id = userLevel.Id,
                Name = userLevel.Name,
            });
        }

        return userLevels;
    }

    public bool IsValidUser(string name, string password, int userLevel) 
    {
        return _dataService.IsValidUser(name, password, userLevel);
    }

    public List<User> GetAllUsers() 
    {
        List<User> users = new();

        foreach (var user in _dataService.GetAllUsers()) 
        {
            users.Add(new User() 
            {
                Id = user.Id,
                Name = user.Name,
                JobTitle = user.JobTitle,
                UserLevel = _dataService.GetUserLevelNameById(user.UserLevelId)
            });
        }

        return users;
    }
}
