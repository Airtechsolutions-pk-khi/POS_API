using DataAccess.Data.IDataModel;
using DataAccess.Models;
using DataAccess.Services.IService;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using WebAPICode.Helpers;

namespace DataAccess.Data.DataModel
{
    public class ExpenseData : IExpenseData
    {
        private readonly IGenericCrudService _service;
        private readonly IMemoryCache _cache;
        private DataTable _dt;

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
            { }
            return model;
        }

        public async Task<RspModel> SaveExpense(Expense expense)
        {
             
            RspModel model = new RspModel();
            try
            {
                SqlParameter[] parm = new SqlParameter[14];
                parm[0] = new SqlParameter("@ExpenseTypeID", expense.ExpenseTypeID);
                parm[1] = new SqlParameter("@LocationID", expense.LocationID);
                parm[2] = new SqlParameter("@PaymentTypeID", expense.PaymentTypeID);
                parm[3] = new SqlParameter("@ExpenseNo", expense.ExpenseNo);
                parm[4] = new SqlParameter("@Name", expense.Name);
                parm[5] = new SqlParameter("@InvoiceReferenceNo", expense.InvoiceReferenceNo);
                parm[6] = new SqlParameter("@Amount", expense.Amount);
                parm[7] = new SqlParameter("@Date", expense.Date);
                parm[8] = new SqlParameter("@Reason", expense.Reason);
                parm[9] = new SqlParameter("@StatusID", 1);
                parm[10] = new SqlParameter("@CreatedDate", DateTime.UtcNow.AddMinutes(180));
                parm[11] = new SqlParameter("@Contact", expense.Contact);
                parm[12] = new SqlParameter("@Description", expense.Description);
                parm[13] = new SqlParameter("@PaymentMethod", expense.PaymentMethod);
                
                int? id = int.Parse(new DBHelper().GetTableFromSP("sp_InsertExpense_P_API_V3", parm).Rows[0]["ID"].ToString());
                if (id != null || id > 0)
                {
                    model = new()
                    {
                        Status = 1,
                        Description = "Expense is Added!"
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
            { }
            return model;
        }

        public async Task<IEnumerable<ExpenseType>> GetExpenseTypeByLocation(int LocationID)
        {
            IEnumerable<ExpenseType>? res;

            //string key = string.Format("{0}{1}", LocationID.ToString(), "ExpenseType");
            //res = _cache.Get<IEnumerable<ExpenseType>>(key);
            //if (res == null)
            //{
                res = await _service.LoadData<ExpenseType, dynamic>("[dbo].[sp_GetExpenseTypeByLocation_P_API]", new { LocationID });
               // _cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}
            return res;
        }

        public async Task<IEnumerable<PaymentType>> GetPaymentType()
        {
            IEnumerable<PaymentType>? res;

            
            res = await _service.LoadData<PaymentType, dynamic>("[dbo].[sp_GetPaymentType_P_API]", new { });
           
            return res;
        }

        public async Task<IEnumerable<Expense>> GetExpenseByLocationForReport(int LocationID, DateTime? FromDate, DateTime? ToDate)
        {
            IEnumerable<Expense>? res;

            //string key = string.Format("{0}{1}", LocationID.ToString(), "Expense", FromDate.ToString(), ToDate.ToString());
            //res = _cache.Get<IEnumerable<Expense>>(key);
            //if (res == null)
            //{
                res = await _service.LoadData<Expense, dynamic>("[dbo].[sp_GetExpenseByLocation_P_API]", new { LocationID, FromDate, ToDate });
               // _cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}
            return res;
        }
        public async Task<IEnumerable<Expense>> GetExpenseByLocation(int LocationID)
        {
            IEnumerable<Expense>? res;

            string key = string.Format("{0}{1}", LocationID.ToString(), "Expense");
            //res = _cache.Get<IEnumerable<Expense>>(key);
            //if (res == null)
            //{
                res = await _service.LoadData<Expense, dynamic>("[dbo].[sp_GetExpenseByLocation_V2_P_API]", new { LocationID});
               // _cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}
            return res;
        }

        public async Task<IEnumerable<ExpenseType>> GetExpenseTypeByID(int LocationID, int ExpenseTypeID)
        {
            IEnumerable<ExpenseType>? res;

            //string key = string.Format("{0}{1}", LocationID.ToString(), "ExpenseType");
            //res = _cache.Get<IEnumerable<ExpenseType>>(key);
            //if (res == null)
            //{
                res = await _service.LoadData<ExpenseType, dynamic>("[dbo].[sp_GetExpenseTypeByID_P_API]", new { LocationID, ExpenseTypeID });
               // _cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}
            return res;
        }

        public async Task<IEnumerable<Expense>> GetExpenseByID(int LocationID, int ExpenseID)
        {
            IEnumerable<Expense>? res;

            //string key = string.Format("{0}{1}", LocationID.ToString(), "Expense");
            //res = _cache.Get<IEnumerable<Expense>>(key);
            //if (res == null)
            //{
                res = await _service.LoadData<Expense, dynamic>("[dbo].[sp_GetExpenseByID_P_API]", new { LocationID, ExpenseID });
               // _cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}
            return res;
        }

        public async Task<RspModel> UpdateExpense(Expense expense)
        {

            int id = 0;
            RspModel model = new RspModel();
            try
            {
                SqlParameter[] parm = new SqlParameter[15];
                 
                parm[0] = new SqlParameter("@ExpenseID", expense.ExpenseID);
                parm[1] = new SqlParameter("@ExpenseTypeID", expense.ExpenseTypeID);
                parm[2] = new SqlParameter("@LocationID", expense.LocationID);
                parm[3] = new SqlParameter("@PaymentTypeID", expense.LocationID);
                parm[4] = new SqlParameter("@ExpenseNo", expense.ExpenseNo);
                parm[5] = new SqlParameter("@Name", expense.Name);
                parm[6] = new SqlParameter("@InvoiceReferenceNo", expense.InvoiceReferenceNo);
                parm[7] = new SqlParameter("@Amount", expense.Amount);
                parm[8] = new SqlParameter("@Date", expense.Date);
                parm[9] = new SqlParameter("@Reason", expense.Reason);
                parm[10] = new SqlParameter("@StatusID", 1);
                parm[11] = new SqlParameter("@CreatedDate", DateTime.UtcNow.AddMinutes(180));
                parm[12] = new SqlParameter("@Contact", expense.Contact);
                parm[13] = new SqlParameter("@Description", expense.Description);
                parm[14] = new SqlParameter("@PaymentMethod", expense.PaymentMethod);



                id = int.Parse(new DBHelper().GetTableFromSP("sp_UpdateExpense_P_API_V3", parm).Rows[0]["ID"].ToString());
                if (id != null || id > 0)
                {
                    model = new()
                    {
                        Status = 1,
                        Description = "Expense Updated!"
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
            { }
            return model;
        }

        public async Task<RspModel> UpdateExpenseType(ExpenseType expenseType)
        {
            RspModel model = new RspModel();
            try
            {
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@LocationID", expenseType.LocationID);
                parm[1] = new SqlParameter("@Name", expenseType.Name);
                parm[2] = new SqlParameter("@Description", expenseType.Description);
                parm[3] = new SqlParameter("@StatusID", 1);
                parm[4] = new SqlParameter("@LastUpdatedDate", DateTime.UtcNow.AddMinutes(180));
                parm[5] = new SqlParameter("@ExpenseTypeID", expenseType.ExpenseTypeID);
                int? id = int.Parse(new DBHelper().GetTableFromSP("sp_UpdateExpenseType_P_API", parm).Rows[0]["ID"].ToString());
                if (id != null || id > 0)
                {
                    model = new()
                    {
                        Status = 1,
                        Description = "Expense Type Updated!"
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
            { }
            return model;
        }

        //public async Task<RspModel> DeleteExpenseType(ExpenseType expenseType)
        //{
        //    RspModel model = new RspModel();
        //    try
        //    {
        //        SqlParameter[] parm = new SqlParameter[6];
        //        parm[0] = new SqlParameter("@LocationID", expenseType.LocationID);
        //        parm[1] = new SqlParameter("@Name", expenseType.Name);
        //        parm[2] = new SqlParameter("@Description", expenseType.Description);
        //        parm[3] = new SqlParameter("@StatusID", 3);
        //        parm[4] = new SqlParameter("@LastUpdatedDate","");
        //        parm[5] = new SqlParameter("@ExpenseTypeID", expenseType.ExpenseTypeID);
        //        int? id = int.Parse(new DBHelper().GetTableFromSP("sp_DeleteExpenseType_P_API", parm).Rows[0]["ID"].ToString());
        //        if (id != null || id > 0)
        //        {
        //            model = new()
        //            {
        //                Status = 1,
        //                Description = "Expense Type Deleted!"
        //            };
        //            return model;
        //        }
        //        else
        //        {
        //            RspModel model1 = new()
        //            {
        //                Status = 0,
        //                Description = "Error!"
        //            };
        //            return model1;
        //        }
        //    }
        //    catch (Exception)
        //    { }
        //    return model;
        //}
        public async Task<RspModel> DeleteExpenseType(ExpenseType obj)
        {
            RspModel rsp = new RspModel();

            try
            {
                var currDate = DateTime.UtcNow.AddMinutes(180);

                SqlParameter[] p2 = new SqlParameter[4];
                p2[0] = new SqlParameter("@ExpenseTypeID", obj.ExpenseTypeID);
                p2[1] = new SqlParameter("@StatusID", 3);
                p2[2] = new SqlParameter("@LocationID", obj.LocationID);
                p2[3] = new SqlParameter("@LastUpdatedDate", DateTime.UtcNow.AddMinutes(180));
                _dt = (new DBHelper().GetTableRow)("sp_DeleteExpenseType_P_API", p2);
            }
            catch (Exception ex)
            {
                rsp.Status = null;
                rsp.Description = null;
            }
            rsp.Status = 200;
            rsp.Description = "Expense Type Deleted!";
            return rsp;
        }

        public async Task<RspModel> DeleteExpense(Expense obj)
        {
            RspModel rsp = new RspModel();

            try
            {
                var currDate = DateTime.UtcNow.AddMinutes(180);

                SqlParameter[] p2 = new SqlParameter[4];
                p2[0] = new SqlParameter("@ExpenseID", obj.ExpenseID);
                p2[1] = new SqlParameter("@StatusID", 3);
                p2[2] = new SqlParameter("@LocationID", obj.LocationID);
                p2[3] = new SqlParameter("@LastUpdatedDate", DateTime.UtcNow.AddMinutes(180));
                _dt = (new DBHelper().GetTableRow)("sp_DeleteExpense_P_API", p2);
            }
            catch (Exception ex)
            {
                rsp.Status = null;
                rsp.Description = null;
            }
            rsp.Status = 200;
            rsp.Description = "Expense Deleted!";
            return rsp;
        }
    }
}
