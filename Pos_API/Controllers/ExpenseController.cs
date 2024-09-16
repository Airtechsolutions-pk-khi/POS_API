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

        [HttpPost("Edit")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> Edit(Customer model)
        {
            _logger.LogInformation("Saving data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            Global.StrDateTimeSqlFormat(model);
            await _data.EditCustomer(model);
            return Ok(new { message = Message.Success });
        }
    }
}
