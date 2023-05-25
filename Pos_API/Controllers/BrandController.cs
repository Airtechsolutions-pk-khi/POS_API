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
using Pos_API.GlobalAndCommon;

namespace Pos_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandData _data;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IBrandData data, ILogger<BrandController> logger)
        {
            _data = data;
            _logger = logger;
        }

  //      [HttpGet("GetAll")]
  //      [Authorize(Roles = "Cashier")]
  //      public async Task<IActionResult> GetAll()
  //      {
  //          _logger.LogInformation("Getting all data...");
  //          return Ok(await _data.GetAll());
  //      }

  //      [HttpGet("GetById")]
		//[Authorize(Roles = "Cashier")]
  //      public async Task<IActionResult> GetById(int id)
  //      {
  //          _logger.LogInformation("Getting data by ID...");
  //          var result = await _data.GetById(id);
  //          if (result == null) return NotFound();
  //          return Ok(result);
  //      }

  //      [HttpPost("Insert")]
		//[Authorize(Roles = "Cashier")]
  //      public async Task<IActionResult> Insert(Brand model)
  //      {
  //          _logger.LogInformation("Saving data...");
		//	if (model == null) return BadRequest(Message.CanNotBeNull);
		//	await _data.SaveData(model);
  //          return Ok();
  //      }

  //      [HttpPost("Update")]
		//[Authorize(Roles = "Cashier")]
  //      public async Task<IActionResult> Update(Brand model)
  //      {
  //          _logger.LogInformation("Updating data...");
		//	if (model == null) return BadRequest(Message.CanNotBeNull);
		//	await _data.UpdateData(model);
  //          return Ok();
  //      }

  //      [HttpPost("Delete")]
		//[Authorize(Roles = "Cashier")]
  //      public async Task<IActionResult> Delete(int id)
  //      {
  //          _logger.LogInformation("Deleting data...");
  //          await _data.DeleteData(id);
  //          return Ok();
  //      }
    }
}
