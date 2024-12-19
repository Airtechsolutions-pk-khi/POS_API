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
	public class RefundOrderData : IRefundOrderData
	{
		private readonly IGenericCrudService _service;
		private readonly ItemData _Iservice;
		private readonly IMemoryCache _cache;
		public RefundOrderData(IGenericCrudService services, IMemoryCache cache)
		{
			_service = services;
			_cache = cache;
		}

		public async Task<int> RefundOrder(Order<Item> order)
		{
            try
            {                 
                double ? tRefund = 0;
                double? rfndAmt = 0;
                foreach (var item in order.Items.ToList())
                {
                    if (item.IsSelected == true)
                    {                         
                        //var UPrice = item.UnitPrice / item.Quantity;
                        var UPrice = item.UnitPrice;

                        if (item.DiscountPrice > 0)
                        {
                            item.RefundPrice = (UPrice * item.RefundQuantity) - item.DiscountPrice ;
                        }
                        else
                        {
                            item.RefundPrice = UPrice * item.RefundQuantity;
                        }
                        SqlParameter[] parm = new SqlParameter[3];
                        parm[0] = new SqlParameter("@ItemID", item.ItemID);
                        parm[1] = new SqlParameter("@LocationID", order.LocationID);
                        parm[2] = new SqlParameter("@Quantity", item.RefundQuantity);
                         new DBHelper().GetTableFromSP("sp_UpdateStockRefund", parm);

                        SqlParameter[] parm1 = new SqlParameter[4];
                        parm1[0] = new SqlParameter("@ItemID", item.ItemID);
                        parm1[1] = new SqlParameter("@RefundDiscountPrice", item.DiscountPrice);
                        parm1[2] = new SqlParameter("@Quantity", item.RefundQuantity);
                        parm1[3] = new SqlParameter("@RefundAmount", item.RefundPrice);
                        new DBHelper().GetTableFromSP("sp_QuantityRefundOrderDetail", parm1);

                        tRefund += (item.RefundPrice * (double?)order.Tax / 100);
                        rfndAmt += (UPrice * item.RefundQuantity) - item.DiscountPrice ?? 0;
                    }
                }
                DateTime lastupdatedate = DateTime.UtcNow.AddMinutes(1);

                SqlParameter[] parm3 = new SqlParameter[2];
                parm3[0] = new SqlParameter("@OrderID", order.ID);
                parm3[1] = new SqlParameter("@StatusID", order.StatusID);
                new DBHelper().GetTableFromSP("sp_UpdateOrderStatus_POSAPI", parm3);

                SqlParameter[] parm2 = new SqlParameter[5];
                parm2[0] = new SqlParameter("@ID", order.ID);
                parm2[1] = new SqlParameter("@LocationID", order.LocationID);
                parm2[2] = new SqlParameter("@RefundAmount", rfndAmt);
                parm2[3] = new SqlParameter("@TaxRefund", tRefund);
                parm2[4] = new SqlParameter("@lastupdatedate", lastupdatedate);
                new DBHelper().GetTableFromSP("sp_UpdateOrderCheckoutRefund", parm2);

                
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }             
        }
         
        public async Task<IEnumerable<Order<OrderDetail>>> GetOrderByTransNo(int LocationID, string TransNo)
        {
            IEnumerable<Order<OrderDetail>>? res;

            //string key = string.Format("{0}{1}{2}", LocationID.ToString(), "Orders", TransNo);
            //res = _cache.Get<IEnumerable<Order<OrderDetail>>>(key);
            //if (res == null)
            //{
                res = await _service.LoadData<Order<OrderDetail>, dynamic>("[dbo].[sp_GetRefundOrderByTransNo_P_API]", new { LocationID, TransNo });
                //foreach (var item in res)
                //{
                //    if (item.RefundAmount != null)
                //    {
                //        item.IsPartial = false;
                //    }
                //    else
                //    {
                //        item.IsPartial = false;
                //    }

                //}
               // _cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}
            return res;
        }
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByTransNo(int LocationID, string TransNo)
        {
            IEnumerable<OrderDetail>? res;

            //string key = string.Format("{0}{1}{2}", LocationID.ToString(), "OrderDetails", TransNo);
            //res = _cache.Get<IEnumerable<OrderDetail>>(key);
            //if (res == null)
            //{
                res = await _service.LoadData<OrderDetail, dynamic>("[dbo].[sp_GetRefundOrderDetailsByTransNo_P_API]", new { LocationID, TransNo });
              //  _cache.Set(key, res, TimeSpan.FromMinutes(10));
            //}
            return res;
        }
        public async Task<IEnumerable<OrderModifierDetail>> GetOrderTransNoModifiers(int LocationID, string TransNo)
        {
            IEnumerable<OrderModifierDetail>? res;

            //string key = string.Format("{0}{1}{2}", LocationID.ToString(), "Modifiers", TransNo);
            //res = _cache.Get<IEnumerable<OrderModifierDetail>>(key);
            //if (res == null)
            //{
                res = await _service.LoadData<OrderModifierDetail, dynamic>("[dbo].[sp_GetOrderModifiersByTransNo_P_API]", new { LocationID, TransNo });
              //  _cache.Set(key, res, TimeSpan.FromMinutes(10));
            //}
            return res;
        }
    }
}
