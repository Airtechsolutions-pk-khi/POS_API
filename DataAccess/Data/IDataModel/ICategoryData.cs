using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface ICategoryData
    {
        Task<IEnumerable<Category>> GetCategories(int LocationID);
        //Task<IEnumerable<Category>> GetFavCategories(int LocationID);
    }
}