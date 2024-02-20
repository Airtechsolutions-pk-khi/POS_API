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
            foreach (var item in result.ToList())
            {
				if (item.ImagePath != null && item.ImagePath != "") 
				{
                    item.ImagePath = "http://retail.premium-pos.com/" + item.ImagePath;
                }
				else
				{
					item.ImagePath = "";

                }
            }
			foreach (var item in result.ToList())
			{
				if (item.UserType == "1")
				{
					item.Designation = "MANAGER";
				}
                else if (item.UserType == "2")
                {
                    item.Designation = "CASHIER";
                }
                else if (item.UserType == "3")
                {
                    item.Designation = "STAFF";
                }
                else if (item.UserType == "4")
                {
                    item.Designation = "CEO";
                }
                else if (item.UserType == "5")
                {
                    item.Designation = "ACCOUNTANT";
                }
                else if (item.UserType == "6")
                {
                    item.Designation = "STORE INCHARGE";
                }
                else if (item.UserType == "7")
                {
                    item.Designation = "SUPERVISOR";
                }
                else if (item.UserType == "8")
                {
                    item.Designation = "WAITER";
                }
                else if (item.UserType == "9")
                {
                    item.Designation = "PRODUCT MANAGER";
                }
                else if (item.UserType == "10")
                {
                    item.Designation = "INVENTORY STAFF";
                }
                else
                {
                    item.Designation = "N/A";
                }
            } 
            return result.FirstOrDefault();
		}

		public async Task<IEnumerable<Location>?> GetDataforSubUserLocations(int ID)
		{
			var result = await _service.LoadData<Location, dynamic>(
				"[dbo].[sp_SubUserLocations_P_API]",
				new { ID });
			
            foreach (var item in result.ToList())
            {
				if (item.ImageURL != null && item.ImageURL != "")
				{
                    item.ImageURL = "http://retail.premium-pos.com/" + item.ImageURL;
                }
				else
				{
					item.ImageURL = "";

                }
            }
            return result;
		}
	}
}
