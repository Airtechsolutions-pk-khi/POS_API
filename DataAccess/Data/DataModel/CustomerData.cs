using DataAccess.Data.IDataModel;
using DataAccess.Models;
using DataAccess.Services.IService;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace DataAccess.Data.DataModel
{
    public class CustomerData : ICustomerData
    {
        private readonly IGenericCrudService _service;
        private readonly IMemoryCache _cache;

        public CustomerData(IGenericCrudService service, IMemoryCache cache)
        {
            _service = service;
            _cache = cache;
        }
        public async Task<IEnumerable<Customer>> GetAllCustomers(int UserID)
        {
            IEnumerable<Customer>? res;
            res = await _service.LoadData<Customer, dynamic>("[dbo].[sp_GetCustomers_P_API]", new { UserID });
            //string key = string.Format("{0}{1}", UserID.ToString(), "Customers");
            //res = _cache.Get<IEnumerable<Customer>>(key);

            //if (res == null)
            //{
            //    res = await _service.LoadData<Customer, dynamic>("[dbo].[sp_GetCustomers_P_API]", new { UserID });
            //    //_cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}
            return res;
        }
        //await _service.LoadData<Customer, dynamic>("[dbo].[sp_GetCustomers_P_API]",
        //	new { UserID });

        //public async Task SaveCustomer(Customer customer) => 
        //	await _service.SaveData<dynamic>("[dbo].[sp_InsertCustomer_P_API]",
        //		new { ParamTable1 = JsonConvert.SerializeObject(customer) });

        public async Task<RspModel> SaveCustomer(Customer customer)
        {
            
            var MobileNo = customer.Mobile;
            var LocationID = customer.LocationID;
            var check = await _service.LoadData<Customer, dynamic>("[dbo].[sp_GetCustomersByMobile_P_API]", new { MobileNo,LocationID });
            if (!check.Any())
            {
               var data = await _service.SaveSingleQueryable<RspModel, dynamic>("[dbo].[sp_InsertCustomer_P_API_V2]",
                    new { ParamTable1 = JsonConvert.SerializeObject(customer) });
            }
            else
            {
                RspModel model = new()
                {
                    Status = 0,
                    Description = "Customer is Already Exist!"
                };                
                return model;
            }
            RspModel model1 = new()
            {
                Status = 0,
                Description = "Customer is Added!"
            };
            return model1;
        }
        public async Task EditCustomer(Customer customer) =>
            await _service.SaveData<dynamic>("[dbo].[sp_UpdateCustomer_P_API_V2]",
                new { ParamTable1 = JsonConvert.SerializeObject(customer) });

        
    }
}
