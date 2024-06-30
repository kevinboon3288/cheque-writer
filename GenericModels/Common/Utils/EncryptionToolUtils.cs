namespace ChequeWriter.GenericModels.Common.Utils;

public static class EncryptionToolUtils
{
    public static string EncryptedPassword(string password) 
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPassword(string password, string hashPassword) 
    {
        return BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }
}
