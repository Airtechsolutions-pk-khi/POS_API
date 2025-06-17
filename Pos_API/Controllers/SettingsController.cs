using DataAccess.Data.IDataModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pos_API.GlobalAndCommon;

namespace Pos_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SettingsController : ControllerBase
	{
		private readonly ILogger<SettingsController> _logger;
		private readonly ITablesData _tabledata;
		private readonly IWaiterData _waiterdata;
		private readonly ICustomerData _customerdata;

		public SettingsController(
			ILogger<SettingsController> logger,
			ITablesData tabledata,
			IWaiterData waiterdata,
			ICustomerData customerdata)
		{
			_logger = logger;
			_tabledata = tabledata;
			_waiterdata = waiterdata;
			_customerdata = customerdata;
		}

		[HttpGet("GetAllSettingsData/{LocationID}/{UserID}")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> GetAllSettingsData(int LocationID, int UserID)
		{
			_logger.LogInformation("Getting data ...");
			if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
			var waitersTask = _waiterdata.GetAllWaiters(LocationID);
			var tablesTask = _tabledata.GetAllTables(LocationID);
			var customersTask = _customerdata.GetAllCustomers(LocationID,UserID);

			await Task.WhenAll(waitersTask, tablesTask, customersTask);

			var waiters = waitersTask.Result;
			var tables = tablesTask.Result;
			var customers = customersTask.Result;

			if (waiters == null || tables == null || customers == null) return BadRequest();
			return Ok( new { message = Message.Success, data = new { waiters, tables, customers }});
		}
        [HttpGet("GetTableFloor/{LocationID}/{UserID}")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> GetTableFloorData(int LocationID, int UserID)
        {
            _logger.LogInformation("Getting data ...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            
            var result = _tabledata.GetTablesFLoorData(LocationID);
            if (result == null)
            {
                RspModel model = new()
                {
                    Status = 0,
                    Description = "Data Not Found"
                };
                
                return Ok(model);
            }
            
            return Ok(new { message = Message.Success, data = new {  result  } });
        }
    }
}
