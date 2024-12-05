using Dapper;
using DataAccess.Data.IDataModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pos_API.GlobalAndCommon;
using System.Data;
using System.Linq;

namespace Pos_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RefundOrderController : ControllerBase
	{
		private readonly IRefundOrderData _data;
		private readonly ILogger<RefundOrderController> _logger;
        private readonly IMemoryCache _cache;

        public RefundOrderController(IRefundOrderData data, ILogger<RefundOrderController> logger, IMemoryCache cache)
        {
            _data = data;
            _logger = logger;
            _cache = cache;
        }

        [HttpPost("RefundInsert")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> RefundInsert(Order<Item> model)
		{
			_logger.LogInformation("Saving data...");
			if (model == null) return BadRequest(Message.CanNotBeNull);
			var result = await _data.RefundOrder(model);
			var GrandTotal = model.GrandTotal;
			var Tax = model.Tax;
			return Ok( new{ data = result, message = Message.Success } );
		}

        //[HttpGet("GetOrderByTransNo/{LocationID}/{transno}")]
        //[Authorize(Roles = "Cashier")]
        //public async Task<IActionResult> GetOrderByTransNo(int LocationID, string TransNo)
        //{
        //    _logger.LogInformation("Getting data...");
        //    if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
        //    var result = await GetOrderByTrnxNo(LocationID, TransNo);
        //    if (result == null) return BadRequest();
        //    return Ok(new { message = Message.Success, data = result });
        //}
        //private async Task<List<Order<OrderDetail>>> GetOrderByTrnxNo(int LocationID,string TransNo)
        //{
        //    List<Order<OrderDetail>>? res;

        //    string key = string.Format("{0}{1}{2}", LocationID.ToString(), "OrdersList", TransNo);
        //    res = _cache.Get<List<Order<OrderDetail>>>(key);
        //    if (res == null)
        //    {
        //        res = await GetOrder(LocationID, TransNo);
        //        _cache.Set(key, res, TimeSpan.FromMinutes(1));
        //    }

        //    return res;
        //}
        //private async Task<List<Order<OrderDetail>>> GetOrder(int LocationID, string TransNo)
        //{
        //    List<Order<OrderDetail>> res = new();

        //    var orderTask = _data.GetOrderByTransNo(LocationID, TransNo);
        //    var orderDetailTask = _data.GetOrderDetailsByTransNo(LocationID, TransNo);
        //    var orderDetailmodTask = _data.GetOrderTransNoModifiers(LocationID, TransNo);


        //    await Task.WhenAll(orderTask, orderDetailTask, orderDetailmodTask);

        //    var orderList = orderTask.Result;
        //    var orderDetailList = orderDetailTask.Result;
        //    var modifierList = orderDetailmodTask.Result;

        //    foreach (var order in orderList)
        //    {
        //        var orderDetailsGroup = orderDetailList.Where(od => od.OrderID == order.ID).ToList();
        //        //Global.InsertImagePreURL<OrderDetail>(orderDetailsGroup);
        //        order.Items = orderDetailsGroup;

        //        order.ItemDiscountAmount = 0.0;

        //        foreach (var od in orderDetailsGroup)
        //        {
        //            var orderDetailsModGroup = modifierList.Where(odm => odm.OrderDetailID == od.OrderDetailID).ToList();
        //            od.Modifiers = orderDetailsModGroup;
        //            order.ItemDiscountAmount += (double)od.DiscountPrice;
        //        }
        //        res.Add(order);
        //    }
        //    return res;
        //}



        [HttpGet("GetOrderByTransNo/{LocationID}/{transno}")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> GetOrderByTransNo(int LocationID, string TransNo)
        {
            _logger.LogInformation("Getting data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");

            // Retrieve cached data or fetch fresh data
            var result = await GetOrder(LocationID, TransNo);
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }

        //private async Task<List<Order<OrderDetail>>> GetOrderByTrnxNo(int LocationID, string TransNo)
        //{
        //    // Construct cache key
        //    string key = $"{LocationID}OrdersList{TransNo}";
        //    //var cachedResult = _cache.Get<List<Order<OrderDetail>>>(key);

        //    // Return cached result if available
        //    //if (cachedResult != null) return cachedResult;

        //    // Fetch fresh data
        //    var result = await GetOrder(LocationID, TransNo);
        //    //_cache.Set(key, result, TimeSpan.FromMinutes(10)); // Increase cache duration for better performance
        //    return result;
        //}
         
        private async Task<List<Order<OrderDetail>>> GetOrder(int LocationID, string TransNo)
        {
            List<Order<OrderDetail>> res = new();

            var orderTask = _data.GetOrderByTransNo(LocationID, TransNo);
            var orderDetailTask = _data.GetOrderDetailsByTransNo(LocationID, TransNo);
            var orderDetailmodTask = _data.GetOrderTransNoModifiers(LocationID, TransNo);


            //await Task.WhenAll(orderTask, orderDetailTask, orderDetailmodTask);

            var orderList =  orderTask.Result;
            var orderDetailList =  orderDetailTask.Result;
            var modifierList = orderDetailmodTask.Result;

            foreach (var order in orderList)
            {
                var orderDetailsGroup = orderDetailList.Where(od => od.OrderID == order.ID).ToList();
                //Global.InsertImagePreURL<OrderDetail>(orderDetailsGroup);
                order.Items = orderDetailsGroup;

                order.ItemDiscountAmount = 0.0;

                foreach (var od in orderDetailsGroup)
                {
                    var orderDetailsModGroup = modifierList.Where(odm => odm.OrderDetailID == od.OrderDetailID).ToList();
                    od.Modifiers = orderDetailsModGroup;
                    order.ItemDiscountAmount += (double)od.DiscountPrice;
                }
                res.Add(order);
            }
            return res;
        }


    }
}
