using DataAccess.Data.IDataModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pos_API.GlobalAndCommon;

namespace Pos_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExpenseController : ControllerBase
	{
		private readonly IExpenseData _data;
		private readonly ILogger<ExpenseController> _logger;

		public ExpenseController(ILogger<ExpenseController> logger, IExpenseData data)
		{
			_logger = logger;
			_data = data;
		}

		[HttpPost("Insert")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> InsertExpenseType(ExpenseType model)
		{
			_logger.LogInformation("Saving data...");
			if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
			Global.StrDateTimeSqlFormat(model);
			var res = await _data.SaveExpenseType(model);
			if (res.Description == "Error!")
			{
				return BadRequest(res);
			}
			else
			{
                return Ok(res);
            }
			
		}

        [HttpPost("InsertExpense")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> InsertExpense(Expense model)
        {
            _logger.LogInformation("Saving data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            Global.StrDateTimeSqlFormat(model);
            var res = await _data.SaveExpense(model);
            if (res.Description == "Error!")
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpGet("GetExpenseType/{LocationID}")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> GetExpenseTypeByLocation(int LocationID)
        {
            _logger.LogInformation("Getting data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _data.GetExpenseTypeByLocation(LocationID);
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }

        [HttpGet("GetExpense/{LocationID}")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> GetExpenseByLocation(int LocationID)
        {
            _logger.LogInformation("Getting data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _data.GetExpenseByLocation(LocationID);
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }

        [HttpGet("GetExpenseTypeByID/{locationid}/{expensetypeid}")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> GetExpenseTypeByID(int locationid, int expensetypeid)
        {
            _logger.LogInformation("Getting all data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _data.GetExpenseTypeByID(locationid, expensetypeid);
            if (result == null) return NotFound();
            return Ok(new { message = Message.Success, data = new { data = result } });
        }

        [HttpGet("GetExpenseByID/{locationid}/{expenseid}")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> GetExpenseByID(int locationid, int expenseid)
        {
            _logger.LogInformation("Getting all data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _data.GetExpenseByID(locationid, expenseid);
            if (result == null) return NotFound();
            return Ok(new { message = Message.Success, data = new { data = result } });
        }

        [HttpPost("UpdateExpenseType")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> UpdateExpenseType(ExpenseType model)
        {
            _logger.LogInformation("Saving data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            Global.StrDateTimeSqlFormat(model);
            var res = await _data.UpdateExpenseType(model);
            if (res.Description == "Error!")
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("UpdateExpense")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> UpdateExpense(Expense model)
        {
            _logger.LogInformation("Saving data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            Global.StrDateTimeSqlFormat(model);
            var res = await _data.UpdateExpense(model);
            if (res.Description == "Error!")
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res);
            }
        }
    }
}
