using DataAccess.Data.IDataModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pos_API.GlobalAndCommon;

namespace Pos_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminTrackController : ControllerBase
	{
		private readonly IAdminData _data;
		private readonly ILogger<AdminTrackController> _Logger;

		public AdminTrackController(ILogger<AdminTrackController> logger, IAdminData data)
		{
			_Logger = logger;
			_data = data;
		}

		 

        [HttpGet("Admin/OverView/{Locations}/{StartDate}/{EndDate}")]
        public async Task<IActionResult> ValidateSubUser(string Locations, string StartDate, string EndDate)
        {
            _Logger.LogInformation("Getting all data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _data.GetOverViewData(Locations, StartDate, EndDate);
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }
        [HttpGet("Admin/SaleStats/{Locations}")]
        public async Task<IActionResult> SaleStatistic(string Locations)
        {
            _Logger.LogInformation("Getting all data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _data.SaleStatistics(Locations);
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }

        [HttpGet("Admin/BranchStats/{Locations}")]
        public async Task<IActionResult> BranchStats(string Locations)
        {
            _Logger.LogInformation("Getting all data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _data.BranchStats(Locations);
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }
        [HttpGet("Admin/BestItems/{Locations}")]
        public async Task<IActionResult> BestItems(string Locations)
        {
            _Logger.LogInformation("Getting all data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _data.BestItems(Locations);
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }
        [HttpGet("Admin/StockAlert/{Locations}")]
        public async Task<IActionResult> StockAlert(string Locations)
        {
            _Logger.LogInformation("Getting all data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _data.StockAlert(Locations);
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }
        [HttpGet("Admin/LastSeavenDaysSales/{Locations}")]
        public async Task<IActionResult> LastSeavenDaysSales(string Locations)
        {
            _Logger.LogInformation("Getting all data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _data.LastSeavenDaysSales(Locations);
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }
        [HttpGet("Admin/Report/SaleSummaryMultiLocation/{Locations}/{StartDate}/{LastDate}")]
        public async Task<IActionResult> SaleSummaryMultiLocation(string Locations, string StartDate, string LastDate)
        {
            _Logger.LogInformation("Getting all data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _data.SalesSummary(Locations, StartDate, LastDate);
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }
    }
}
