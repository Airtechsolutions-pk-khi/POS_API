using DataAccess.Models;
using DataAccess.Data.IDataModel;
using DataAccess.Services.IService;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Intrinsics.Arm;

namespace DataAccess.Data.DataModel
{
    public class OrderData : IOrderData
	{
		private readonly IGenericCrudService _service;
		private readonly IMemoryCache _cache;
		public OrderData(IGenericCrudService services, IMemoryCache cache)
		{
			_service = services;
			_cache = cache;
		}

		public async Task<OrderReturn> SaveData(Order<Item> order)
		{
            var or = await _service.SaveSingleQueryable<OrderReturn, dynamic>("[dbo].[sp_InsertOrder_P_API_V2]",
				new { ParamTable1 = JsonConvert.SerializeObject(order) });

			var orderID = or.OrderID;
			foreach (var item in order.Items)
			{
                //insert order detail
                var OD = await _service.SaveSingleQueryable<OrderDetail, dynamic>("[dbo].[sp_InsertOrderDetail_P_API]",
                new { orderID, item.ID, item.Name, item.Price, item.StatusID, item.LocationID, item.Quantity });

				if (item.Modifiers != null)
				{
                    foreach (var modi in or.Items)
                    {
                        var odM = await _service.LoadData<OrderDetail, dynamic>("[dbo].[sp_GetOrderDetailsByOrderId_P_API]", new { or.OrderID });
                        //insert modifier						
						
                        //var mData = odM.Where(x => x.OrderDetailID == OD.OrderDetailID);

                        var inM = await _service.SaveSingleQueryable<OrderModifierDetail, dynamic>("[dbo].[sp_InsertModifier_P_API]",
                    new {  });
                    }
                }              
			}			 
			var od = await _service.LoadData<OrderDetail, dynamic>("[dbo].[sp_GetOrderDetailsByOrderId_P_API]", new { or.OrderID });
			return or;
		}

		public async Task UpdateData(Order<Item> order) => 
			await _service.SaveData("[dbo].[sp_UpdateOrder_P_API_V2]",
				new { ParamTable1 = JsonConvert.SerializeObject(order) });

		public async Task<IEnumerable<Order<OrderDetail>>> GetOrderByLocation(int LocationID, DateTime FromDate, DateTime ToDate)
		{
			IEnumerable<Order<OrderDetail>>? res;

            string key = string.Format("{0}{1}{2}{3}", LocationID.ToString(), "Orders", FromDate.ToString(), ToDate.ToString());
            res = _cache.Get<IEnumerable<Order<OrderDetail>>>(key);
			if (res == null)
			{
                res = await _service.LoadData<Order<OrderDetail>, dynamic>("[dbo].[sp_GetOrderByLocation_P_API]", new { LocationID, FromDate, ToDate });
				_cache.Set(key, res, TimeSpan.FromMinutes(1));
			}
			return res;
		}
		public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByLocation(int LocationID, DateTime FromDate, DateTime ToDate)
		{
			IEnumerable<OrderDetail>? res;

            string key = string.Format("{0}{1}{2}{3}", LocationID.ToString(), "OrderDetails", FromDate.ToString(), ToDate.ToString());
            res = _cache.Get<IEnumerable<OrderDetail>>(key);
			if (res == null)
			{
                res = await _service.LoadData<OrderDetail, dynamic>("[dbo].[sp_GetOrderDetailsByLocation_P_API]", new { LocationID, FromDate, ToDate });
				_cache.Set(key, res, TimeSpan.FromMinutes(1));
			}
			return res;
		}
		//public async Task<IEnumerable<OrderModifierDetail>> GetOrderModifiers(int LocationID, DateTime FromDate, DateTime ToDate)
		//{
		//	IEnumerable<OrderModifierDetail>? res;

		//	string key = string.Format("{0}{1}{2}{3}", LocationID.ToString(), "Modifiers", FromDate.ToString(), ToDate.ToString());
		//	res = _cache.Get<IEnumerable<OrderModifierDetail>>(key);
		//	if (res == null)
		//	{
		//		res = await _service.LoadData<OrderModifierDetail, dynamic>("[dbo].[sp_GetOrderModifiers_P_API]", new { LocationID, FromDate, ToDate });
		//		_cache.Set(key, res, TimeSpan.FromMinutes(1));
		//	}
		//	return res;
		//}
	}
}
