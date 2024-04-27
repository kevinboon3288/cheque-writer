namespace ChequeWriter.Modules.UserModule.Models;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? JobTitle { get; set; }
    public string? UserLevel { get; set; }
    public bool IsChecked { get; set; }

    public User()
    {
        IsChecked = false;
    }
}
