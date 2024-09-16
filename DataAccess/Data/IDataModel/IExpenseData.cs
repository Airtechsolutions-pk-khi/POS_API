using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IExpenseData
    {
        Task<IEnumerable<Customer>> GetAllCustomers(int LocationID);
         
        Task EditCustomer(Customer customer);

        Task<RspModel> SaveExpenseType(ExpenseType expenseType);
    }
}