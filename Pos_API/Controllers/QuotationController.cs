using DataAccess.Data.IDataModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pos_API.GlobalAndCommon;

namespace Pos_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuotationController : ControllerBase
	{
		private readonly IQuotationData _data;
		private readonly ILogger<QuotationController> _logger;

		public QuotationController(ILogger<QuotationController> logger, IQuotationData data)
		{
			_logger = logger;
			_data = data;
		}

        [HttpGet("GetQuotation/{StartDate}/{EndDate}/{UserID}")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> GetItemsList(string StartDate ,string EndDate, int UserID)
        {
            _logger.LogInformation("Getting all data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _data.GetAllQuotationData(StartDate, EndDate, UserID);
            if (result == null) return NotFound();
            return Ok(new { message = Message.Success, data = result });
        }

        [HttpGet("GetQuotation/{UserID}/{CompanyQuotationID}")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> GetItemsList(int UserID, int CompanyQuotationID)
		{
            _logger.LogInformation("Getting all data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
			var result = await _data.GetAllQuotation(UserID, CompanyQuotationID);
            if (result == null) return NotFound();
            return Ok(new { message = Message.Success, data = result });
        }

            [HttpPost("Insert")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> Insert(CompanyQuotationList model)
		{
			_logger.LogInformation("Saving data...");
			if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
			Global.StrDateTimeSqlFormat(model);
			var res = await _data.SaveQuotation(model);
			if (res.Description == "Quotation is Already Exist!")
			{
				return BadRequest(res);
			}
			else
			{
                return Ok(res);
            }
			
		}
        [HttpPost("Delete")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> Delete(int CompanyQuotationID)
        {
            _logger.LogInformation("Saving data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            //Global.StrDateTimeSqlFormat(CompanyQuotationID);
            var res = await _data.DeleteQuotation(CompanyQuotationID);
            if (res.Description == "Quotation is Already Exist!")
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
        public async Task<IActionResult> Edit(CompanyQuotationList model)
        {
            _logger.LogInformation("Saving data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            Global.StrDateTimeSqlFormat(model);
            var res = await _data.EditQuotation(model);
            return Ok(res);
        }
    }
}
