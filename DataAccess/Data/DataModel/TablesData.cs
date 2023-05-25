using DataAccess.Data.IDataModel;
using DataAccess.Models;
using DataAccess.Services.IService;

namespace DataAccess.Data.DataModel
{
    public class TablesData : ITablesData
	{
		private readonly IGenericCrudService _service;
		public TablesData(IGenericCrudService service)
		{
			_service = service;
		}
		public async Task<IEnumerable<Table>> GetAllTables(int LocationID) =>
			await _service.LoadData<Table, dynamic>(
				"[dbo].[sp_GetTables_P_API]",
				new { LocationID });

	}
}
