
namespace DataServices
{
    public interface IDataService
    {
        Cheque? GetCheques(int id);
        List<UserLevel>? GetUserLevels();
    }
}