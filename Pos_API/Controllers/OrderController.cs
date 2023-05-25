using DataAccess.Data.IDataModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

		public OrderController(IOrderData data, ILogger<OrderController> logger)
		{
			_data = data;
			_logger = logger;
		}

		[HttpPost("Insert")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> Insert(Order model)
		{
			_logger.LogInformation("Saving data...");
			if (model == null) return BadRequest(Message.CanNotBeNull);
			await _data.SaveData(model);
			return Ok( new{ message = Message.Success } );
		}

		[HttpPost("Update")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> Update(Order model)
		{
			_logger.LogInformation("Updating data...");
			if (model == null) return BadRequest(Message.CanNotBeNull);
			await _data.UpdateData(model);
			return Ok( new{ message = Message.Success } );
		}

		[HttpGet("GetOrderByLocation/{LocationID}")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> GetOrderByLocation(int LocationID)
		{
			_logger.LogInformation("Getting data...");
			if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
			var result = await GetOrderList(LocationID);
			if (result == null) return BadRequest();
			return Ok( new {message = Message.Success, data = result } );
		}

		private async Task<List<Order>> GetOrderList(int LocationID)
		{
			List<Order> orders = new List<Order>();
			var orderList = await _data.GetOrderByLocation(LocationID);
			var orderDetailList = await _data.GetOrderDetailsByLocation(LocationID);

			foreach (var order in orderList)
			{
				var orderDetailsGroup = orderDetailList.Where(od => od.OrderID == order.ID).ToList();
				order.Items = orderDetailsGroup;
				orders.Add(order);
			}
			return orders;
			
		}
	}
}
