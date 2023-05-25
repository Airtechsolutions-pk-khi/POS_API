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
	public class SubCategoryController : ControllerBase
	{
		private readonly ISubCategoryData _data;
		private readonly ILogger<SubCategoryController> _logger;

		public SubCategoryController(ISubCategoryData data, ILogger<SubCategoryController> logger)
		{
			_data = data;
			_logger = logger;
		}

		//[HttpGet("GetSubCategories/{locationid}")]
		//[Authorize(Roles = "Cashier")]
		//public async Task<IActionResult> GetSubCategories(int locationid)
		//{
		//	_logger.LogInformation("Getting all data...");
		//	var result = await _data.GetSubCategories(locationid);
		//	if (result == null) return NotFound();
		//	return Ok(result);
		//}

	}
}
