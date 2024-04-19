using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IReportData
    {
        Task<Report?> GetXZReportData(int SubUserID, int LocationID, DateTime OrderStartDate, DateTime OrderLastDate);
        Task<IEnumerable<StockReport>> GetStockReportData(string LocationID);
    }
}