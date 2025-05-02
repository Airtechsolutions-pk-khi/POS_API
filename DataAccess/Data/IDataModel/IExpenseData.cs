using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IExpenseData
    {
        Task<IEnumerable<Customer>> GetAllCustomers(int LocationID);
         
        Task<RspModel> SaveExpenseType(ExpenseType expenseType);

        Task<RspModel> SaveExpense(Expense expense);

        Task<IEnumerable<ExpenseType>> GetExpenseTypeByLocation(int LocationID);

        Task<IEnumerable<Expense>> GetExpenseByLocation(int LocationID);
        Task<IEnumerable<PaymentType>> GetPaymentType();

        Task<IEnumerable<ExpenseType>> GetExpenseTypeByID(int LocationID, int ExpenseTypeID);

        Task<IEnumerable<Expense>> GetExpenseByID(int LocationID, int ExpenseID);

        Task<RspModel> UpdateExpense(Expense expense);
        Task<RspModel> UpdateExpenseType(ExpenseType expenseType);
        Task<RspModel> DeleteExpenseType(ExpenseType expenseType);
        Task<RspModel> DeleteExpense(Expense expense);
    }
}