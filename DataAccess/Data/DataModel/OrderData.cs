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
using System.Transactions;

namespace DataAccess.Data.DataModel
{
	public class OrderData : IOrderData
	{
		private readonly IGenericCrudService _service;
		private readonly ItemData _Iservice;
		private readonly IMemoryCache _cache;
		public OrderData(IGenericCrudService services, IMemoryCache cache)
		{
			_service = services;
			_cache = cache;
		}

		public async Task<OrderReturn> SaveData(Order<Item> order)
		{

            var or = new OrderReturn();
            if (order.ID > 0)
            {
                or = await _service.SaveSingleQueryable<OrderReturn, dynamic>("[dbo].[sp_UpdateHoldOrder_P_API_V2]",
                new { ParamTable1 = JsonConvert.SerializeObject(order) });

            }
            if (order.ID == 0)
            {
                or = await _service.SaveSingleQueryable<OrderReturn, dynamic>("[dbo].[sp_InsertOrder_P_API_V2]",
                new { ParamTable1 = JsonConvert.SerializeObject(order) });
            }
            

			//or.GrandTotal = or.Total;
			var Tax = or.Tax;
			var orderID = or.OrderID;
			var TransactionNo = or.TransactionNo;
			var OrderNo = or.OrderNo;
            double? ItemDiscountAmount = 0;
            foreach (var item in order.Items)
			{
				int OrderDetailID = 0;
				try
				{

					item.StatusID = 1;
					SqlParameter[] p = new SqlParameter[10];
					p[0] = new SqlParameter("@OrderId", orderID);
					p[1] = new SqlParameter("@Name", item.Name);
					p[2] = new SqlParameter("@Price", item.Price);
					p[3] = new SqlParameter("@ItemID", item.ID);
					p[4] = new SqlParameter("@Quantity", item.Quantity);
					p[5] = new SqlParameter("@StatusID", item.StatusID);
					p[6] = new SqlParameter("@OrderDate", DateTime.UtcNow.AddMinutes(180));
					p[7] = new SqlParameter("@TransactionNo", TransactionNo);
					p[8] = new SqlParameter("@OrderNo", OrderNo);
                    p[9] = new SqlParameter("@DiscountPrice", item.DiscountPrice);

                    if (item.ItemMode == 1)
                    {
                        OrderDetailID = int.Parse(new DBHelper().GetTableFromSP("sp_InsertOrderDetail_P_API", p).Rows[0]["ID"].ToString());
                    }
                    if (item.ItemMode == 2)
                    {
                        OrderDetailID = int.Parse(new DBHelper().GetTableFromSP("sp_UpdateOrderDetail_P_API", p).Rows[0]["ID"].ToString());
                    }

                    ItemDiscountAmount += item.DiscountPrice;
                }
				catch { }

                try
                {
                    SqlParameter[] updateParams = new SqlParameter[2];
                    updateParams[0] = new SqlParameter("@OrderId", orderID);
                    updateParams[1] = new SqlParameter("@ItemDiscountAmount", ItemDiscountAmount);

                    new DBHelper().ExecuteNonQueryReturn("sp_UpdateOrderCheckout_P_API", updateParams);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating OrderCheckout: " + ex.Message);
                }

                if (item.Modifiers != null && OrderDetailID > 0)
				{
					foreach (var m in item.Modifiers)
					{
						m.OrderDetailID = OrderDetailID;
						m.StatusID = 1;
						m.Type = "Modifier";
						SqlParameter[] p = new SqlParameter[6];
						p[0] = new SqlParameter("@OrderDetailID", OrderDetailID);
						p[1] = new SqlParameter("@ModifierID", m.ModifierID);
						p[2] = new SqlParameter("@Name", m.Name);
						p[3] = new SqlParameter("@Price", m.Price);
						p[4] = new SqlParameter("@Type", m.Type);
						p[5] = new SqlParameter("@StatusID", m.StatusID);
						(new DBHelper().ExecuteNonQueryReturn)("sp_InsertModifier_P_API", p);
					}
				}
			}

			or.Items = await _service.LoadData<OrderDetail, dynamic>("[dbo].[sp_GetOrderDetailsByOrderId_P_API]", new { or.OrderID });
			var odm = await _service.LoadData<OrderModifierDetail, dynamic>("[dbo].[sp_GetOrderModifierByOrderId_P_API]", new { or.OrderID });
			foreach (var item in or.Items)
			{
				item.Modifiers = odm.Where(x => x.OrderDetailID == item.ID).ToList();
			}
			return or;
		}

        public async Task<OrderReturn> SaveCreditData(Order<Item> order)
        {
            var or = await _service.SaveSingleQueryable<OrderReturn, dynamic>("[dbo].[sp_InsertCreditOrder_P_API]",
                new { ParamTable1 = JsonConvert.SerializeObject(order) });

            //or.GrandTotal = or.Total;
            var Tax = or.Tax;
            var orderID = or.OrderID;
            var TransactionNo = or.TransactionNo;
            var OrderNo = or.OrderNo;

            foreach (var item in order.Items)
            {
                int OrderDetailID = 0;
                try
                {

                    item.StatusID = 1;
                    SqlParameter[] p = new SqlParameter[10];
                    p[0] = new SqlParameter("@OrderId", orderID);
                    p[1] = new SqlParameter("@Name", item.Name);
                    p[2] = new SqlParameter("@Price", item.Price);
                    p[3] = new SqlParameter("@ItemID", item.ID);
                    p[4] = new SqlParameter("@Quantity", item.Quantity);
                    p[5] = new SqlParameter("@StatusID", item.StatusID);
                    p[6] = new SqlParameter("@OrderDate", DateTime.UtcNow.AddMinutes(180));
                    p[7] = new SqlParameter("@TransactionNo", TransactionNo);
                    p[8] = new SqlParameter("@OrderNo", OrderNo);
                    p[9] = new SqlParameter("@DiscountPrice", item.DiscountPrice);

                    OrderDetailID = int.Parse(new DBHelper().GetTableFromSP("sp_InsertOrderDetail_P_API", p).Rows[0]["ID"].ToString());

                }
                catch { }

                if (item.Modifiers != null && OrderDetailID > 0)
                {
                    foreach (var m in item.Modifiers)
                    {
                        m.OrderDetailID = OrderDetailID;
                        m.StatusID = 1;
                        m.Type = "Modifier";
                        SqlParameter[] p = new SqlParameter[6];
                        p[0] = new SqlParameter("@OrderDetailID", OrderDetailID);
                        p[1] = new SqlParameter("@ModifierID", m.ModifierID);
                        p[2] = new SqlParameter("@Name", m.Name);
                        p[3] = new SqlParameter("@Price", m.Price);
                        p[4] = new SqlParameter("@Type", m.Type);
                        p[5] = new SqlParameter("@StatusID", m.StatusID);
                        (new DBHelper().ExecuteNonQueryReturn)("sp_InsertModifier_P_API", p);
                    }
                }
            }

            or.Items = await _service.LoadData<OrderDetail, dynamic>("[dbo].[sp_GetOrderDetailsByOrderId_P_API]", new { or.OrderID });
            var odm = await _service.LoadData<OrderModifierDetail, dynamic>("[dbo].[sp_GetOrderModifierByOrderId_P_API]", new { or.OrderID });
            foreach (var item in or.Items)
            {
                item.Modifiers = odm.Where(x => x.OrderDetailID == item.ID).ToList();
            }
            return or;
        }
        public async Task UpdateData(Order<Item> order) =>
			await _service.SaveData("[dbo].[sp_UpdateOrder_P_API_V2]",
				new { ParamTable1 = JsonConvert.SerializeObject(order) });

		public async Task<IEnumerable<Order<OrderDetail>>> GetOrderByLocation(int LocationID, DateTime FromDate, DateTime ToDate)
		{
			IEnumerable<Order<OrderDetail>>? res;

			//string key = string.Format("{0}{1}{2}{3}", LocationID.ToString(), "Orders", FromDate.ToString(), ToDate.ToString());
			//res = _cache.Get<IEnumerable<Order<OrderDetail>>>(key);
			//if (res == null)
			//{
				res = await _service.LoadData<Order<OrderDetail>, dynamic>("[dbo].[sp_GetOrderByLocation_P_API]", new { LocationID, FromDate, ToDate });
            //foreach (var item in res)
            //{
            //	if (item.RefundAmount != 0)
            //	{
            //		item.IsPartial = true;
            //	}
            //	else {
            //                    item.IsPartial = false;
            //                }

            //}
            //_cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}
            foreach (var item in res)
            {
                if (item.PartialAmount == 0)
                {
                    item.IsPartial = false;
                }
                
            }
			return res;
		}
		public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByLocation(int LocationID, DateTime FromDate, DateTime ToDate)
		{
			IEnumerable<OrderDetail>? res;

			//string key = string.Format("{0}{1}{2}{3}", LocationID.ToString(), "OrderDetails", FromDate.ToString(), ToDate.ToString());
			//res = _cache.Get<IEnumerable<OrderDetail>>(key);
			//if (res == null)
			//{
				res = await _service.LoadData<OrderDetail, dynamic>("[dbo].[sp_GetOrderDetailsByLocation_P_API]", new { LocationID, FromDate, ToDate });
				//_cache.Set(key, res, TimeSpan.FromMinutes(1));
			//}
			return res;
		}
		public async Task<IEnumerable<OrderModifierDetail>> GetOrderModifiers(int LocationID, DateTime FromDate, DateTime ToDate)
		{
			IEnumerable<OrderModifierDetail>? res;

			//string key = string.Format("{0}{1}{2}{3}", LocationID.ToString(), "Modifiers", FromDate.ToString(), ToDate.ToString());
			//res = _cache.Get<IEnumerable<OrderModifierDetail>>(key);
			//if (res == null)
			//{
				res = await _service.LoadData<OrderModifierDetail, dynamic>("[dbo].[sp_GetOrderModifiers_P_API]", new { LocationID, FromDate, ToDate });
				//_cache.Set(key, res, TimeSpan.FromMinutes(1));
			//}
			return res;
		}

        public async Task<IEnumerable<Order<OrderDetail>>> GetOrderByID(int LocationID, int OrderID)
        {
            IEnumerable<Order<OrderDetail>>? res;

            //string key = string.Format("{0}{1}{2}", LocationID.ToString(), "Orders", OrderID);
            //res = _cache.Get<IEnumerable<Order<OrderDetail>>>(key);
            //if (res == null)
            //{
                res = await _service.LoadData<Order<OrderDetail>, dynamic>("[dbo].[sp_GetOrderByID_P_API]", new { LocationID, OrderID });
                //foreach (var item in res)
                //{
                //    if (item.RefundAmount != null)
                //    {
                //        item.IsPartial = true;
                //    }
                //    else
                //    {
                //        item.IsPartial = false;
                //    }

                //}
              //  _cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}
            return res;
        }
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByID(int LocationID, int OrderID)
        {
            IEnumerable<OrderDetail>? res;

            //string key = string.Format("{0}{1}{2}", LocationID.ToString(), "OrderDetails", OrderID);
            //res = _cache.Get<IEnumerable<OrderDetail>>(key);
            //if (res == null)
            //{
                res = await _service.LoadData<OrderDetail, dynamic>("[dbo].[sp_GetOrderDetailsByID_P_API]", new { LocationID, OrderID });
              //  _cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}
            return res;
        }
        public async Task<IEnumerable<OrderModifierDetail>> GetOrderIDModifiers(int LocationID, int OrderID)
        {
            IEnumerable<OrderModifierDetail>? res;

            //string key = string.Format("{0}{1}{2}", LocationID.ToString(), "Modifiers", OrderID);
            //res = _cache.Get<IEnumerable<OrderModifierDetail>>(key);
            //if (res == null)
            //{
                res = await _service.LoadData<OrderModifierDetail, dynamic>("[dbo].[sp_GetOrderModifiersByID_P_API]", new { LocationID, OrderID });
              //  _cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}
            return res;
        }
        public async Task DeleteCustomerFromCart(Item Item)
		{
			//await _service.SaveData<dynamic>("[dbo].[sp_UpdateCustomer_P_API]",
			// new { ParamTable1 = JsonConvert.SerializeObject(Item) });
			try
			{
                if (Item.ItemMode == 3)
                {
                    if (Item.OrderID != null)
                    {
                        SqlParameter[] p = new SqlParameter[2];
                        p[0] = new SqlParameter("@ItemID", Item.ID);
                        p[1] = new SqlParameter("@OrderID", Item.OrderID);

                        var dataTable = await new DBHelper().GetTableFromSPAsync("sp_DeleteCartOrderDetail_P_API", p);
                    }
                    

                    //if (dataTable != null && dataTable.Rows.Count > 0)
                    //{
                    //    var OrderDetailID = int.Parse(dataTable.Rows[0]["ID"].ToString());
                    //}
                    //else
                    //{

                    //    throw new Exception("No data returned from stored procedure.");
                    //}
                }
                else
                {

                }
               

                //var OrderDetailID = await  int.Parse(new DBHelper().GetTableFromSPAsync("sp_InsertOrderDetail_P_API", p).Rows[0]["ID"].ToString());
            }
			catch (Exception ex)
			{

				 
			}
        }
         
    }
}
