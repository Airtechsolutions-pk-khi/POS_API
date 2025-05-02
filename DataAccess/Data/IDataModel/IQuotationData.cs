using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IQuotationData
    {
        Task<IEnumerable<CompanyQuotationList>> GetAllQuotation(int UserID, int CompanyQuotationID);
        Task<IEnumerable<CompanyQuotationList>> GetAllQuotationData(string StartDate, string EndDate,int UserID,int LocationID);
         Task<RspModelCus> EditQuotation(CompanyQuotationList customer);
        Task<RspModel> DeleteQuotation(int CompanyQuotationID);
        Task<RspModelQ> SaveQuotation(CompanyQuotationList customer);
    }
}