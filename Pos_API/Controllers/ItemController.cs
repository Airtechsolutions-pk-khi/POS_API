using DataAccess.Data.IDataModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json;
using Pos_API.GlobalAndCommon;

namespace Pos_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ItemController : ControllerBase
	{
		private readonly IItemData _data;
		private readonly ICategoryData _categories;
		private readonly ISubCategoryData _subcategories;
		private readonly ILogger<ItemController> _logger;

		public ItemController(IItemData data, ILogger<ItemController> logger, ICategoryData categories, ISubCategoryData subcategories)
		{
			_data = data;
			_logger = logger;
			_categories = categories;
			_subcategories = subcategories;
		}

		//[HttpGet("GetItems/{locationid}")]
		//[Authorize(Roles = "Cashier")]
		//public async Task<IActionResult> GetItems(int locationid)
		//{
		//	_logger.LogInformation("Getting all data...");
		//	var result = await _data.GetItems(locationid);
		//	if (result == null) return NotFound();
		//	return Ok(result);
		//}

		[HttpGet("GetItemsList/{locationid}")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> GetItemsList(int locationid)
		{
			_logger.LogInformation("Getting all data...");
			var result = await GetItemsSerialized(locationid);
			if (result == null) return NotFound();
			return Ok( new { message = Message.Success, data = result } );
		}

		private async Task<string> GetItemsSerialized(int locationid)
		{
			var items = await _data.GetItems(locationid);
			var categories = await _categories.GetCategories(locationid);
			var subcategories = await _subcategories.GetSubCategories(locationid);

			var dataTable = new DataTable();
			dataTable.Columns.Add("CategoryID", typeof(int));
			dataTable.Columns.Add("CategoryName", typeof(string));
			dataTable.Columns.Add("SubCategories", typeof(List<SubCategory>));

			foreach (var category in categories)
			{
				var subcategoriesGroup = subcategories.Where(sub => sub.CategoryID == category.ID).ToList();
				foreach (var subcategory in subcategoriesGroup)
				{
					var itemsGroup = items.Where(item => item.SubCategoryID == subcategory.ID).ToList();
					subcategory.Items = itemsGroup;
					dataTable.Rows.Add(category.ID, category.Name, subcategoriesGroup);
				}
			}

			var serializedData = JsonConvert.SerializeObject(new { Categories = dataTable });
			return serializedData;
		}
	}
}
