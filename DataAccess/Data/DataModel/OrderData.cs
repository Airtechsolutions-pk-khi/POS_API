using DataAccess.Models;
using DataAccess.Data.IDataModel;
using DataAccess.Services.IService;
using Newtonsoft.Json;

namespace DataAccess.Data.DataModel
{
    public class OrderData : IOrderData
	{
		private readonly IGenericCrudService _services;
		public OrderData(IGenericCrudService services)
		{
			_services = services;
		}

		public async Task SaveData(Order order)
		{
			await _services.SaveData("[dbo].[sp_InsertOrder_P_API_V2]",
				new { ParamTable1 = JsonConvert.SerializeObject(order) });
		}

		public async Task UpdateData(Order order)
		{
			await _services.SaveData("[dbo].[sp_UpdateOrder_P_API_V2]",
				new { ParamTable1 = JsonConvert.SerializeObject(order) });
		}

		public async Task<IEnumerable<Order>> GetOrderByLocation(int LocationID) => 
			await _services.LoadData<Order, dynamic>("[dbo].[sp_GetOrderByLocation_P_API]",
				new { LocationID });

		public async Task<IEnumerable<OrderItem>> GetOrderDetailsByLocation(int LocationID) => 
			await _services.LoadData<OrderItem, dynamic>("[dbo].[sp_GetOrderDetailsByLocation_P_API]",
				new { LocationID });
	}
}
