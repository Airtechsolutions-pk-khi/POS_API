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

		public async Task<IEnumerable<Item>> GetItems(int LocationID, PagingParameterModel pagingParams)
		{
            IEnumerable<Item>? res;

            //string key = string.Format("{0}{1}", LocationID.ToString(), "Items");
            //res = _cache.Get<IEnumerable<Item>>(key);
            //if (res == null)
            //{
                res = await _service.LoadData<Item, dynamic>("[dbo].[sp_GetItems_P_API_V2]", new
                {
                    LocationID = LocationID,
                    PageNumber = pagingParams.PageNumber,
                    PageSize = pagingParams.PageSize
                });
                //_cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}

            // Paginate items
            res = res.Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize).Take(pagingParams.PageSize);
            res.OrderBy(x => x.DisplayOrder);
            return res;
            //IEnumerable<Item>? res;

            //         string key = string.Format("{0}{1}", LocationID.ToString(), "Items");
            //         res = _cache.Get<IEnumerable<Item>>(key);
            //if (res == null)
            //{
            //	res = await _service.LoadData<Item, dynamic>("[dbo].[sp_GetItems_P_API]", new { LocationID });
            //	_cache.Set(key, res, TimeSpan.FromMinutes(1));
            //}
            //return res;
        }
        public async Task<IEnumerable<Item>> GetItemsByName(int locationid, string name)
        {
            IEnumerable<Item>? res;
 
            res = await _service.LoadData<Item, dynamic>("[dbo].[sp_GetItemsSearch_P_API]", new
            {
                LocationID = locationid,
                name = name                
            });
            
            return res;
            
        }
        public async Task<IEnumerable<Item>> GetFavoriteItems(int LocationID)
        {
            IEnumerable<Item>? res;

            string key = string.Format("{0}{1}", LocationID.ToString(), "Items");
            res = _cache.Get<IEnumerable<Item>>(key);
            if (res == null)
            {
                res = await _service.LoadData<Item, dynamic>("[dbo].[sp_GetFavoriteItems_P_API]", new { LocationID });
                _cache.Set(key, res, TimeSpan.FromMinutes(1));
            }
            return res;
        }
        public async Task<IEnumerable<OrderModifierDetail>> GetModifiers(int itemid)
		{
			IEnumerable<OrderModifierDetail>? res;

			string key = string.Format("{0}{1}", itemid.ToString(), "Modifiers");
			res = _cache.Get<IEnumerable<OrderModifierDetail>>(key);
			if (res == null)
			{
				res = await _service.LoadData<OrderModifierDetail, dynamic>("[dbo].[sp_GetModifiers_P_API]", new { itemid });
				_cache.Set(key, res, TimeSpan.FromMinutes(1));
			}
			return res;
		}
	}
}
