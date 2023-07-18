using DataAccess.Data.IDataModel;
using DataAccess.Models;
using DataAccess.Services.IService;

namespace DataAccess.Data.DataModel
{
    public class ReportData : IReportData
	{
		private readonly IGenericCrudService _service;
		public ReportData(IGenericCrudService service)
		{
			_service = service;
		}

		public async Task<Report?> GetXZReportData(int SubUserID, int LocationID, DateTime OrderStartDate, DateTime OrderLastDate)
		{
			var result = await _service.LoadSingleOrDefaultData<Report, dynamic>(
				"[dbo].[sp_ZXReport_P_API]",
				new { SubUserID, LocationID, OrderLastDate, OrderStartDate });

			return result;
		}
	}
}