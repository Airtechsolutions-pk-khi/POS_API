using DataAccess.Data.IDataModel;
using DataAccess.Services.IService;
using DataAccess.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Pos_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductSummaryController : ControllerBase
	{
		private readonly IProductSummaryData _data;
		private readonly ILogger<ProductSummaryController> _logger;

		public ProductSummaryController(IProductSummaryData data, ILogger<ProductSummaryController> logger)
		{
			_data = data;
			_logger = logger;
		}

		//[HttpGet("GetProductSummary/{OrderStartDate}/{OrderLastDate}/{LocationID}")]
		//[Authorize(Roles = "Cashier")]
		//public async Task<IActionResult> GetProductSummary(string OrderStartDate, string OrderLastDate, int LocationID)
		//{
		//	_logger.LogInformation("Getting all data...");
		//	var result = await _data.GetProductSummary(OrderStartDate, OrderLastDate, LocationID);
		//	if (result == null) return NotFound();
		//	return Ok(result);
		//}
		//[HttpGet("GetUserProductSummary/{OrderStartDate}/{OrderLastDate}/{LocationID}")]
		//[Authorize(Roles = "Cashier")]
		//public async Task<IActionResult> GetUserProductSummary(string OrderStartDate, string OrderLastDate, int LocationID)
		//{
		//	_logger.LogInformation("Getting all data...");
		//	var result = await _data.GetUserProductSummary(OrderStartDate, OrderLastDate, LocationID);
		//	if (result == null) return NotFound();
		//	return Ok(result);
		//}

	}
}
