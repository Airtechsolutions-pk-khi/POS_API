using DataAccess.Models;
using DataAccess.Data.IDataModel;
using DataAccess.Services.IService;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data.DataModel
{
    public class ItemData : IItemData
	{
		private readonly IGenericCrudService _service;
		private readonly IMemoryCache _cache;

		public ItemData(IGenericCrudService services, IMemoryCache cache)
		{
			_service = services;
			_cache = cache;
		}

		public async Task<IEnumerable<Item>> GetItems(int LocationID)
		{
			IEnumerable<Item>? res;

            string key = string.Format("{0}{1}", LocationID.ToString(), "Items");
            res = _cache.Get<IEnumerable<Item>>(key);
			if (res == null)
			{
				res = await _service.LoadData<Item, dynamic>("[dbo].[sp_GetItems_P_API]", new { LocationID });
				_cache.Set(key, res, TimeSpan.FromMinutes(1));
			}
			return res;
		}
	}
}
