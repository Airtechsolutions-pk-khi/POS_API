using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface ISubCategoryData
    {
        Task<IEnumerable<SubCategory>> GetSubCategories(int LocationidID);
        //Task<IEnumerable<SubCategory>> GetFavSubCategories(int LocationidID);
    }
}