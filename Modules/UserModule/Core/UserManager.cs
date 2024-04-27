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
        try
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
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public bool IsValidUser(string name, string password, int userLevel) 
    {
        try
        {
            return _dataService.IsValidUser(name, password, userLevel);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public List<User> GetAllUsers() 
    {
        try
        {
            List<User> users = new();

            foreach (var user in _dataService.GetAllUsers()) 
            {
                users.Add(new User() 
                {
                    Id = user.Id,
                    Name = user.Name,
                    JobTitle = user.JobTitle,
                    UserLevel = _dataService.GetUserLevelNameById(user.UserLevelId),
                    IsChecked = false
                });
            }

            return users;
        }
        catch(Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public int AddUser(string userName, string password, string? jobTitle, int userLevel)
    {
        try
        {
            return _dataService.AddUser(userName, password, jobTitle, userLevel);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public int DeletUser(User user) 
    { 
        try
        { 
            return _dataService.DeleteUser(user.Id);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
}
