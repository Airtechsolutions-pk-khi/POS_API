using DataAccess.Data.IDataModel;
using DataAccess.Models;
using DataAccess.Services.IService;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data.DataModel
{
    public class TablesData : ITablesData
	{
		private readonly IGenericCrudService _service;
		private readonly IMemoryCache _cache;

		public TablesData(IGenericCrudService service, IMemoryCache cache)
		{
			_service = service;
			_cache = cache;
		}
		public async Task<IEnumerable<Table>> GetAllTables(int LocationID)
		{
			IEnumerable<Table>? res;

            string key = string.Format("{0}{1}", LocationID.ToString(), "Tables");
            res = _cache.Get<IEnumerable<Table>>(key);
			if (res == null)
			{
				res = await _service.LoadData<Table, dynamic>("[dbo].[sp_GetTables_P_API]", new { LocationID });
				_cache.Set(key, res, TimeSpan.FromMinutes(1));
			}
			return res;
		}
		//await _service.LoadData<Table, dynamic>(
		//	"[dbo].[sp_GetTables_P_API]",
		//	new { LocationID });

	}
}
