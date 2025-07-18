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
        public async Task<IEnumerable<FloorTreeDto>> GetTablesFLoorData(int LocationID)
        {
            IEnumerable<FloorTreeDto>? res;
            string key = string.Format("{0}{1}", LocationID.ToString(), "TablesFloorTree");

            res = _cache.Get<IEnumerable<FloorTreeDto>>(key);
            if (res == null)
            {
                // Get Floors
                var floors = await _service.LoadData<FloorTreeDto, dynamic>("[dbo].[sp_GetFloors_P_API]", new { LocationID });

                // Get Tables
                var tables = await _service.LoadData<TableTreeDto, dynamic>("[dbo].[sp_GetFloorTables_P_API]", new { LocationID });

                // Get Waiters (SubUsers with UserType = 8)
                var waiters = await _service.LoadData<SubUser, dynamic>("[dbo].[sp_GetFloorWaiters_P_API]", new { LocationID, UserType = 8 });

                // Build the tree structure
                res = floors.Select(floor => new FloorTreeDto
                {
                    ID = floor.ID,
                    Title = floor.Title,
                    Name = floor.Name,
                    Tables = tables.Where(t => t.FloorID == floor.ID).Select(table => new TableTreeDto
                    {
                        ID = table.ID,
                        TableID = table.TableID,
                        TableName = table.TableName,
                        Capacity = table.Capacity,
                        TableType = table.TableType,
                        TableNo = table.TableNo,
                        x = table.x,
                        y= table.y, 
                        Width = table.Width,    
                        Height = table.Height,
                        StatusID = table.StatusID,  
                        TableStatus = table.TableStatus,
                        Waiters = waiters.Where(w => w.LocationID == LocationID) // You might need to add TableID to SubUser or define the relationship
                            .Select(waiter => new WaiterDto
                            {
                                ID = waiter.ID,
                                UserName = waiter.UserName,
                                FirstName = waiter.FirstName,
                                LastName = waiter.LastName,
                                Designation = waiter.Designation,
                                Email = waiter.Email,
                                ContactNo = waiter.ContactNo
                            }).ToList()
                    }).ToList()
                }).ToList();

                _cache.Set(key, res, TimeSpan.FromMinutes(1));
            }
            return res;
        }


    }
}
