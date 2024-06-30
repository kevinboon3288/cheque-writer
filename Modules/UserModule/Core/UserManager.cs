using ChequeWriter.GenericModels.Common.Utils;

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

    public int? GetCurrentUserId(string name, string password, int userLevel)
    {
        try
        {
            var foundUser = _dataService.GetUserByInfo(name, userLevel);

            if (foundUser != null && !string.IsNullOrEmpty(foundUser.Password) && 
                EncryptionToolUtils.VerifyPassword(password, foundUser.Password))
            {
                return foundUser.Id;
            }

            return null;
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
            var foundUser = _dataService.GetUserByInfo(name, userLevel);

            if (foundUser != null && !string.IsNullOrEmpty(foundUser.Password)) 
            {
                return EncryptionToolUtils.VerifyPassword(password, foundUser.Password);
            }

            return false;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public bool IsExistUser(string name, int userLevel)
    {
        try
        {
            return _dataService.IsExistUser(name, userLevel);
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
                    UserId = user.UserId.ToString(),
                    Name = user.Name,
                    JobTitle = user.JobTitle,
                    CreatedBy = user.CreatedBy,
                    UserLevel = _dataService.GetUserLevelNameById(user.UserLevelId),
                    IsEnabled = (user.Id == 1) ? false : true
                });
            }

            return users;
        }
        catch(Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public int AddUser(string userName, string password, string? jobTitle, int userLevel, int currentUserId)
    {
        try
        {
            return _dataService.AddUser(userName, EncryptionToolUtils.EncryptedPassword(password), jobTitle, userLevel, currentUserId);
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
