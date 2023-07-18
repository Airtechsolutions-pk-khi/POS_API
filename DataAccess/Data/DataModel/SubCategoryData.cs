using DataAccess.Models;
using DataAccess.Data.IDataModel;
using DataAccess.Services.IService;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data.DataModel
{
    public class SubCategoryData : ISubCategoryData
	{
		private readonly IGenericCrudService _service;
		private readonly IMemoryCache _cache;

		public SubCategoryData(IGenericCrudService services, IMemoryCache cache)
		{
			_service = services;
			_cache = cache;
		}

		public async Task<IEnumerable<SubCategory>> GetSubCategories(int LocationID)
		{
			IEnumerable<SubCategory>? res;

            string key = string.Format("{0}{1}", LocationID.ToString(), "subCategory");
            res = _cache.Get<IEnumerable<SubCategory>>(key);
			if (res == null)
			{
				res = await _service.LoadData<SubCategory, dynamic>("[dbo].[sp_GetSubCategory_menu]", new { LocationID });
				_cache.Set(key, res, TimeSpan.FromMinutes(1));
			}
			return res;
		}
		//_services.LoadData<SubCategory, dynamic>("[dbo].[sp_GetSubCategory_menu]", new { LocationID });

	}
}
