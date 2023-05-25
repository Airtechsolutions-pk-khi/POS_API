using DataAccess.Data.IDataModel;
using DataAccess.Models;
using DataAccess.Services.IService;

namespace DataAccess.Data.DataModel
{
    public class CustomerData : ICustomerData
	{
		private readonly IGenericCrudService _service;
		public CustomerData(IGenericCrudService service)
		{
			_service = service;
		}
		public async Task<IEnumerable<Customer>> GetAllCustomers(int UserID) =>
			await _service.LoadData<Customer, dynamic>(
				"[dbo].[sp_GetCustomers_P_API]",
				new { UserID }
			);
	}
}
