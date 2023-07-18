using DataAccess.Data.IDataModel;
using DataAccess.Services.IService;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data.DataModel
{
    public class WaiterData : IWaiterData
	{
		private readonly IGenericCrudService _service;
		private readonly IMemoryCache _cache;

		public WaiterData(IGenericCrudService service, IMemoryCache cache)
		{
			_service = service;
			_cache = cache;
		}
		public async Task<IEnumerable<string>> GetAllWaiters(int LocationID)
		{
			IEnumerable<string>? res;

            string key = string.Format("{0}{1}", LocationID.ToString(), "Waiters");
            res = _cache.Get<IEnumerable<string>>(key);
			if (res == null)
			{
				res = await _service.LoadData<string, dynamic>("[dbo].[sp_GetWaiters_P_API]", new { LocationID });
				_cache.Set(key, res, TimeSpan.FromMinutes(1));
			}
			return res;
		}
		//await _service.LoadData<string, dynamic>(
		//	"[dbo].[sp_GetWaiters_P_API]",
		//	new { LocationID }
		//);
	}
}
