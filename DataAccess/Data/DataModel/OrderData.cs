using DataAccess.Models;
using DataAccess.Data.IDataModel;
using DataAccess.Services.IService;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Data.SqlClient;
using WebAPICode.Helpers;

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
				try
				{
                    //insert order detail
                    int OrderDetailID = 0;
                    SqlParameter[] p = new SqlParameter[9];
                    p[0] = new SqlParameter("@OrderID", orderID);
                    p[1] = new SqlParameter("@Name", item.Name);
                    p[2] = new SqlParameter("@Price", item.Price);
                    p[3] = new SqlParameter("@ItemID", item.ID);
                    p[4] = new SqlParameter("@Quantity", item.Quantity);
                    p[5] = new SqlParameter("@StatusID", item.StatusID);
                    p[6] = new SqlParameter("@OrderDate", DateTime.UtcNow);
                    p[7] = new SqlParameter("@TransactionNo", order.TransactionNo);
                    p[8] = new SqlParameter("@OrderNo", order.OrderNo);

                    //rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertCategory_Admin", p);
                     OrderDetailID = int.Parse(new DBHelper().GetTableFromSP("sp_InsertOrderDetail_P_API", p).Rows[0]["ID"].ToString());
                }
				catch (Exception ex)
				{

					
				}


				//var OD = await _service.SaveSingleQueryable<OrderDetail, dynamic>("[dbo].[sp_InsertOrderDetail_P_API]",
				//new { ParamTable2 = JsonConvert.SerializeObject(order) });
				//new { orderID, item.ID, item.Name, item.Price, item.StatusID, order.CreatedOn, item.Quantity });

				if (item.Modifiers != null && item.Modifiers.Count>0)
				{
					foreach (var modi in or.Items)
					{
						var odM = await _service.LoadData<OrderDetail, dynamic>("[dbo].[sp_GetOrderDetailsByOrderId_P_API]", new { or.OrderID });
						//insert modifier						

						//var mData = odM.Where(x => x.OrderDetailID == OD.OrderDetailID);

						var inM = await _service.SaveSingleQueryable<OrderModifierDetail, dynamic>("[dbo].[sp_InsertModifier_P_API]",
					new { });
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
