﻿namespace ChequeWriter.Modules.UserModule.Core;

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
}