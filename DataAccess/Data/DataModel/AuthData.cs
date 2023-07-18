using DataAccess.Data.IDataModel;
using DataAccess.Models;
using DataAccess.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.DataModel
{
    public class AuthData : IAuthData
	{
		private readonly IGenericCrudService _service;

		public AuthData(IGenericCrudService service)
		{
			_service = service;
		}

		public async Task<SubUser?> GetDataforSubUserAuth(string BusinessKey, string Passcode)
		{
			var result = await _service.LoadData<SubUser, dynamic>(
				"[dbo].[sp_LoginSubUser_P_API]",
				new { BusinessKey, Passcode });

			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<Location>?> GetDataforSubUserLocations(int ID)
		{
			var result = await _service.LoadData<Location, dynamic>(
				"[dbo].[sp_SubUserLocations_P_API]",
				new { ID });

			return result;
		}
	}
}
