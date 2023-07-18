using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface ICustomerData
    {
        Task<IEnumerable<Customer>> GetAllCustomers(int LocationID);
        Task SaveCustomer(Customer customer);
    }
}