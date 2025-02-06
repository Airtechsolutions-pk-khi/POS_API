using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IAdminData
    {        
        Task<IEnumerable<Overview>> GetOverViewData(string Locations, string OrderStartDate, string OrderLastDate);
        Task<IEnumerable<ThreeMonthSales>> SaleStatistics(string Locations);
        Task<IEnumerable<BranchStats>> BranchStats(string Locations, string OrderStartDate, string OrderLastDate);
        Task<IEnumerable<BestSellingItems>> BestItems(string Locations, string StartDate, string EndDate);
        Task<IEnumerable<StockAlert>> StockAlert(string Locations, string StartDate, string EndDate);

        Task<IEnumerable<SeavenDaysSales>> LastSeavenDaysSales(string Locations);

        Task<IEnumerable<SalesSummary>> SalesSummary(string Locations, string StartDate, string LastDate);
        Task<IEnumerable<Branches>> Branches(int userid);
    }
}