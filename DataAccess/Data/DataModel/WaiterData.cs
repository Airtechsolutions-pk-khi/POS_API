using DataAccess.Data.IDataModel;
using DataAccess.Services.IService;

namespace DataAccess.Data.DataModel
{
    public class WaiterData : IWaiterData
	{
		private readonly IGenericCrudService _service;
		public WaiterData(IGenericCrudService service)
		{
			_service = service;
		}
		public async Task<IEnumerable<string>> GetAllWaiters(int LocationID) =>
			await _service.LoadData<string, dynamic>(
				"[dbo].[sp_GetWaiters_P_API]",
				new { LocationID }
			);
	}
}
