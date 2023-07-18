using DataAccess.Data.IDataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pos_API.GlobalAndCommon;

namespace Pos_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportController : ControllerBase
	{
		private readonly IReportData _data;
		private readonly ILogger<ReportController> _Logger;

		public ReportController(ILogger<ReportController> logger, IReportData data)
		{
			_Logger = logger;
			_data = data;
		}

		[HttpGet("XZReport/{SubUserID}/{LocationID}/{OrderStartDate}/{OrderLastDate}")]
		[Authorize(Roles = "Cashier")]
        public async Task<IActionResult> XZReport(int SubUserID, int LocationID, DateTime OrderStartDate, DateTime OrderLastDate)
		{
			_Logger.LogInformation("Getting data...");
			if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
			var result = await _data.GetXZReportData(SubUserID, LocationID, OrderStartDate, OrderLastDate);
			if (result == null) return BadRequest();
			return Ok(new { message = Message.Success, data = result });
		}
	}
}
