using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IItemData
    {
        Task<IEnumerable<Item>> GetItems(int LocationID, PagingParameterModel pagingParams);
        Task<IEnumerable<Item>> GetFavoriteItems(int LocationID);
        Task<IEnumerable<OrderModifierDetail>> GetModifiers(int itemid);
    }
}