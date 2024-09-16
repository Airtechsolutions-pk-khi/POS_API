using DataAccess.Data.IDataModel;
using DataAccess.Models;
using DataAccess.Services.IService;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Data.SqlClient;
using WebAPICode.Helpers;

namespace DataAccess.Data.DataModel
{
    public class ExpenseData : IExpenseData
    {
        private readonly IGenericCrudService _service;
        private readonly IMemoryCache _cache;

        public ExpenseData(IGenericCrudService service, IMemoryCache cache)
        {
            _service = service;
            _cache = cache;
        }
        public async Task<IEnumerable<Customer>> GetAllCustomers(int UserID)
        {
            IEnumerable<Customer>? res;

            string key = string.Format("{0}{1}", UserID.ToString(), "Customers");
            res = _cache.Get<IEnumerable<Customer>>(key);
            if (res == null)
            {
                res = await _service.LoadData<Customer, dynamic>("[dbo].[sp_GetCustomers_P_API]", new { UserID });
                _cache.Set(key, res, TimeSpan.FromMinutes(1));
            }
            return res;
        }
        
        public async Task<RspModel> SaveExpenseType(ExpenseType expenseType)
        {
            RspModel model = new RspModel();
            try
            {
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@LocationID", expenseType.LocationID);
                parm[1] = new SqlParameter("@Name", expenseType.Name);
                parm[2] = new SqlParameter("@Description", expenseType.Description);
                parm[3] = new SqlParameter("@StatusID", 1);
                parm[4] = new SqlParameter("@CreatedDate", DateTime.UtcNow.AddMinutes(180));
                parm[5] = new SqlParameter("@LastUpdatedDate", DateTime.UtcNow.AddMinutes(180));
                int? id = int.Parse(new DBHelper().GetTableFromSP("sp_InsertExpenseType_P_API", parm).Rows[0]["ID"].ToString());
                if (id != null || id > 0)
                {
                    model = new()
                    {
                        Status = 1,
                        Description = "Expense Type is Added!"
                    };
                    return model;
                }
                else
                {
                    RspModel model1 = new()
                    {
                        Status = 0,
                        Description = "Error!"
                    };
                    return model1;
                }
            }
            catch (Exception)
            {
                 
            }            
            
            
            
            return model;
        }
        public async Task EditCustomer(Customer customer) =>
            await _service.SaveData<dynamic>("[dbo].[sp_UpdateCustomer_P_API]",
                new { ParamTable1 = JsonConvert.SerializeObject(customer) });

        
    }
}
