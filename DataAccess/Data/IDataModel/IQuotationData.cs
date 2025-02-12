using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IQuotationData
    {
        Task<IEnumerable<CompanyQuotationList>> GetAllQuotation(int UserID, int CompanyQuotationID);
       
        Task EditQuotation(CompanyQuotationList customer);
        Task<RspModel> DeleteQuotation(int CompanyQuotationID);

        Task<RspModel> SaveQuotation(CompanyQuotationList customer);
    }
}