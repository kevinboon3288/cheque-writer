namespace DataServices;

public interface IDataService
{
    #region ChequeModule
    List<Cheque>? GetCheques();
    Cheque? GetChequeById(int id);
    void AddCheque(Cheque cheque);
    void UpdateCheque(Cheque cheque);
    #endregion
    #region UserModule
    string? GetUserLevelNameById(int id);
    List<UserLevel>? GetUserLevels();
    int GetCurrentUserId(string? name, string? password, int userLevel);
    bool IsValidUser(string? name, string? password, int userLevel);
    bool IsExistUser(string? name, int userLevel);
    List<User> GetAllUsers();
    int AddUser(string userName, string password, string? jobTitle, int userLevel, int currentUserId);
    int DeleteUser(int userId);
    #endregion
}