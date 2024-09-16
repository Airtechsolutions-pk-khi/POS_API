using DataAccess.Data.IDataModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Pos_API.GlobalAndCommon;
using Microsoft.Extensions.Caching.Memory;

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
        private readonly IMemoryCache _cache;

        public ItemController(IItemData data, ILogger<ItemController> logger, ICategoryData categories, ISubCategoryData subcategories, IMemoryCache cache)
        {
            _data = data;
            _logger = logger;
            _categories = categories;
            _subcategories = subcategories;
            _cache = cache;
        }

        [HttpGet("GetItemsList/{locationid}")]
		[Authorize(Roles = "Cashier")]
		public async Task<IActionResult> GetItemsList(int locationid)
		{
			_logger.LogInformation("Getting all data...");
			if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
			var result = await GetItemsSerialized(locationid);
			if (result == null) return NotFound();
			return Ok(new { message = Message.Success, data = new { Categories = result } });
		}

		private async Task<List<Category>> GetItemsSerialized(int locationId)
		{
            List<Category>? res;

            string key = string.Format("{0}{1}", locationId.ToString(), "ItemLists");
            res = _cache.Get<List<Category>>(key);
            if (res == null)
            {
                res = await GetItemsFromDb(locationId);
                _cache.Set(key, res, TimeSpan.FromMinutes(1));
            }

            return res;
		}
		private async Task<List<Category>> GetItemsFromDb(int locationId)
		{
			List<Category> res = new();
			var itemsTask = _data.GetItems(locationId);
			var categoriesTask = _categories.GetCategories(locationId);
			var subcategoriesTask = _subcategories.GetSubCategories(locationId);

			await Task.WhenAll(itemsTask, categoriesTask, subcategoriesTask);

			var items = itemsTask.Result;
			var categories = categoriesTask.Result;
			var subcategories = subcategoriesTask.Result;

			foreach (var category in categories)
			{
				Global.InsertImagePreURL<Category>(category);

				var subcategoriesGroup = subcategories.Where(sub => sub.CategoryID == category.ID).ToList();
				Global.InsertImagePreURL<SubCategory>(subcategoriesGroup);

				category.SubCategories = subcategoriesGroup;

				foreach (var subcategory in subcategoriesGroup)
				{
					var itemsGroup = items.Where(item => item.SubCategoryID == subcategory.ID).ToList();
					Global.InsertImagePreURL<Item>(itemsGroup);
					subcategory.Items = itemsGroup;

					foreach (var item in itemsGroup)
					{
						var modifiers = await _data.GetModifiers(item.ID);
						item.Modifiers = modifiers.ToList();
					}
				}
				res.Add(category);
			}

			return res;
		}


        [HttpGet("GetFavItemsList/{locationid}")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> GetFavItemsList(int locationid)
        {
            _logger.LogInformation("Getting all data...");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await GetFavItemsSerialized(locationid);
            if (result == null) return NotFound();
            return Ok(new { message = Message.Success, data = new { Categories = result } });
        }

        private async Task<List<Category>> GetFavItemsSerialized(int locationId)
        {
            List<Category>? res;

            string key = string.Format("{0}{1}", locationId.ToString(), "ItemLists");
            res = _cache.Get<List<Category>>(key);
            if (res == null)
            {
                res = await GetFavItemsFromDb(locationId);
                _cache.Set(key, res, TimeSpan.FromMinutes(1));
            }

            return res;
        }
        private async Task<List<Category>> GetFavItemsFromDb(int locationId)
        {
            List<Category> res = new();
            var itemsTask = _data.GetFavoriteItems(locationId);
            var categoriesTask = _categories.GetCategories(locationId);
            var subcategoriesTask = _subcategories.GetSubCategories(locationId);

            await Task.WhenAll(itemsTask, categoriesTask, subcategoriesTask);

            var items = itemsTask.Result;
            var categories = categoriesTask.Result;
            var subcategories = subcategoriesTask.Result;

            foreach (var category in categories)
            {
                Global.InsertImagePreURL<Category>(category);

                var subcategoriesGroup = subcategories.Where(sub => sub.CategoryID == category.ID).ToList();
                Global.InsertImagePreURL<SubCategory>(subcategoriesGroup);

                category.SubCategories = subcategoriesGroup;

                foreach (var subcategory in subcategoriesGroup)
                {
                    var itemsGroup = items.Where(item => item.SubCategoryID == subcategory.ID).ToList();
                    Global.InsertImagePreURL<Item>(itemsGroup);
                    subcategory.Items = itemsGroup;

                    foreach (var item in itemsGroup)
                    {
                        var modifiers = await _data.GetModifiers(item.ID);
                        item.Modifiers = modifiers.ToList();
                    }
                }
                res.Add(category);
            }

            return res;
        }


        //     private async Task<List<Category>> GetItemsFromDb(int locationId)
        //     {
        //         List<Category> res = new();
        //         var itemsTask = _data.GetItems(locationId);
        //         var categoriesTask = _categories.GetCategories(locationId);
        //         var subcategoriesTask = _subcategories.GetSubCategories(locationId);
        //         //var modifiersTask = _data.GetModifiers(locationId);

        //         await Task.WhenAll(itemsTask, categoriesTask, subcategoriesTask);

        //         var items = itemsTask.Result;
        //         var categories = categoriesTask.Result;
        //         var subcategories = subcategoriesTask.Result;
        ////var modifiers = modifiersTask.Result;

        //foreach (var category in categories)
        //         {
        //             Global.InsertImagePreURL<Category>(category);

        //             var subcategoriesGroup = subcategories.Where(sub => sub.CategoryID == category.ID).ToList();
        //             Global.InsertImagePreURL<SubCategory>(subcategoriesGroup);

        //             category.SubCategories = subcategoriesGroup;

        //             foreach (var subcategory in subcategoriesGroup)
        //             {
        //                 var itemsGroup = items.Where(item => item.SubCategoryID == subcategory.ID).ToList();
        //                 Global.InsertImagePreURL<Item>(itemsGroup);
        //                 subcategory.Items = itemsGroup;
        //             }

        //             res.Add(category);
        //         }


        //return res;
        //     }
    }
}
