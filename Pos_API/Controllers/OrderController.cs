﻿using DataAccess.Data.IDataModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pos_API.GlobalAndCommon;
using System.Data;

namespace Pos_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderData _data;
		private readonly ILogger<OrderController> _logger;
        private readonly IMemoryCache _cache;

        public OrderController(IOrderData data, ILogger<OrderController> logger, IMemoryCache cache)
        {
            _data = data;
            _logger = logger;
            _cache = cache;
        }

        [HttpPost("Insert")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> Insert(Order<Item> model)
		{
			_logger.LogInformation("Saving data...");
			if (model == null) return BadRequest(Message.CanNotBeNull);
			var result = await _data.SaveData(model);
			return Ok( new{ data = result, message = Message.Success } );
		}

		[HttpPost("Update")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> Update(Order<Item> model)
		{
			_logger.LogInformation("Updating data...");
			if (model == null) return BadRequest(Message.CanNotBeNull);
			await _data.UpdateData(model);
			return Ok( new{ message = Message.Success } );
		}

		[HttpGet("GetOrderByLocation/{LocationID}/{FromDate}/{ToDate}")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> GetOrderByLocation(int LocationID, DateTime FromDate, DateTime ToDate)
		{
			_logger.LogInformation("Getting data...");
			if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
			var result = await GetOrdersSerialized(LocationID, FromDate, ToDate);
			if (result == null) return BadRequest();
			return Ok( new {message = Message.Success, data = result } );
		}

        private async Task<List<Order<OrderDetail>>> GetOrdersSerialized(int LocationID, DateTime FromDate, DateTime ToDate)
        {
            List<Order<OrderDetail>>? res;

            string key = string.Format("{0}{1}{2}{3}", LocationID.ToString(), "OrdersList", FromDate.ToString(), ToDate.ToString());
            res = _cache.Get<List<Order<OrderDetail>>>(key);
            if (res == null)
            {
                res = await GetOrderList(LocationID, FromDate, ToDate);
                _cache.Set(key, res, TimeSpan.FromMinutes(1));
            }

            return res;
        }

        private async Task<List<Order<OrderDetail>>> GetOrderList(int LocationID, DateTime FromDate, DateTime ToDate)
		{
            List<Order<OrderDetail>> res = new();

            var orderTask = _data.GetOrderByLocation(LocationID, FromDate, ToDate);
			var orderDetailTask = _data.GetOrderDetailsByLocation(LocationID, FromDate, ToDate);

			await Task.WhenAll(orderTask, orderDetailTask);

			var orderList = orderTask.Result;
			var orderDetailList = orderDetailTask.Result;

			foreach (var order in orderList)
			{
				var orderDetailsGroup = orderDetailList.Where(od => od.OrderID == order.ID).ToList();
				Global.InsertImagePreURL<OrderDetail>(orderDetailsGroup);
				order.Items = orderDetailsGroup;
                res.Add(order);
            }
            return res;
        }
    }
}