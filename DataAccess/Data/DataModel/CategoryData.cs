using DataAccess.Models;
using DataAccess.Data.IDataModel;
using DataAccess.Services.IService;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data.DataModel
{
    public class CategoryData : ICategoryData
	{
		private readonly IGenericCrudService _service;
		private readonly IMemoryCache _cache;

		public CategoryData(IGenericCrudService services, IMemoryCache cache)
		{
			_service = services;
			_cache = cache;
		}

		public async Task<IEnumerable<Category>> GetCategories(int LocationID)
		{
			IEnumerable<Category>? res;

   //         string key = string.Format("{0}{1}", LocationID.ToString(), "Category");
   //         res = _cache.Get<IEnumerable<Category>>(key);
			//if (res == null)
			//{
				res = await _service.LoadData<Category, dynamic>("[dbo].[sp_GetCategory_menu]", new { LocationID });
			//_cache.Set(key, res, TimeSpan.FromMinutes(1));
			//}
			 
			return res;
		}

        
        //public async Task<IEnumerable<Category>> GetFavCategories(int LocationID)
        //{
        //    IEnumerable<Category>? res;

        //    string key = string.Format("{0}{1}", LocationID.ToString(), "Category");
        //    res = _cache.Get<IEnumerable<Category>>(key);
        //    if (res == null)
        //    {
        //        res = await _service.LoadData<Category, dynamic>("[dbo].[sp_GetCategory_menu]", new { LocationID });
        //        _cache.Set(key, res, TimeSpan.FromMinutes(1));
        //    }
        //    return res;
        //}

    }
}
