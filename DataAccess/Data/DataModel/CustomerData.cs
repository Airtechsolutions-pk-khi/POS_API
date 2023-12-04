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

            string key = string.Format("{0}{1}", UserID.ToString(), "Customers");
            res = _cache.Get<IEnumerable<Customer>>(key);
			if (res == null)
			{
				res = await _service.LoadData<Customer, dynamic>("[dbo].[sp_GetCustomers_P_API]", new { UserID });
				_cache.Set(key, res, TimeSpan.FromMinutes(1));
			}
			return res;
		}
		//await _service.LoadData<Customer, dynamic>("[dbo].[sp_GetCustomers_P_API]",
		//	new { UserID });

		public async Task SaveCustomer(Customer customer) => 
			await _service.SaveData<dynamic>("[dbo].[sp_InsertCustomer_P_API]",
				new { ParamTable1 = JsonConvert.SerializeObject(customer) });

        public async Task EditCustomer(Customer customer) =>
            await _service.SaveData<dynamic>("[dbo].[sp_UpdateCustomer_P_API]",
                new { ParamTable1 = JsonConvert.SerializeObject(customer) });
    }
}
