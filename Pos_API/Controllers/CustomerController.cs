using DataAccess.Data.IDataModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pos_API.GlobalAndCommon;

namespace Pos_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerData _data;
		private readonly ILogger<CustomerController> _logger;

		public CustomerController(ILogger<CustomerController> logger, ICustomerData data)
		{
			_logger = logger;
			_data = data;
		}

		[HttpPost("Insert")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> Insert(Customer model)
		{
			_logger.LogInformation("Saving data...");
			if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
			Global.StrDateTimeSqlFormat(model);
			await _data.SaveCustomer(model);
			return Ok( new { message = Message.Success } );
		}
	}
}
