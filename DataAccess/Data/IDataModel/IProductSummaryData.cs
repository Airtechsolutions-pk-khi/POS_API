using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IProductSummaryData
    {
        Task<IEnumerable<ProductSummary>> GetProductSummary(string StartDate, string LastDate, int LocationID);
        Task<IEnumerable<UserProductSummary>> GetUserProductSummary(string StartDate, string LastDate, int LocationID);
    }
}