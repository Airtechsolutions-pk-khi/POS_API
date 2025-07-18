using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface ICustomerData
    {
        Task<IEnumerable<Customer>> GetAllCustomers(int LocationID,int UserID );
        //Task SaveCustomer(Customer customer);
        Task EditCustomer(Customer customer);
        Task<RspModel> DeleteCustomer(int CustomerID);
        Task<RspModel> SaveCustomer(Customer customer);

        Task<int> GetRemainingDays(int UserID);
    }
}