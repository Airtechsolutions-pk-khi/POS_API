using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IWaiterData
    {
        Task<IEnumerable<string>> GetAllWaiters(int LocationID);
    }
}