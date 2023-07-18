using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IItemData
    {
        Task<IEnumerable<Item>> GetItems(int LocationID);
    }
}