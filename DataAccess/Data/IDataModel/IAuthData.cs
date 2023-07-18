using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IAuthData
    {
        //Task<User?> GetDataforUserAuth(string email, string password);
        Task<SubUser?> GetDataforSubUserAuth(string BusinessKey, string Passcode);
        Task<IEnumerable<Location>?> GetDataforSubUserLocations(int ID);
	}
}