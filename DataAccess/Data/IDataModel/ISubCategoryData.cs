using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface ISubCategoryData
    {
        Task<IEnumerable<SubCategory>> GetSubCategories(int LocationidID);
    }
}