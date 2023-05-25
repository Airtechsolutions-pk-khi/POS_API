using DataAccess.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DataAccess.Data.IDataModel;
using System.Text.Json;
using DataAccess.Services.IService;

namespace DataAccess.Data.DataModel
{
    public class SubCategoryData : ISubCategoryData
	{
		private readonly IGenericCrudService _services;
		public SubCategoryData(IGenericCrudService services)
		{
			_services = services;
		}

		public Task<IEnumerable<SubCategory>> GetSubCategories(int LocationID) =>
			_services.LoadData<SubCategory, dynamic>("[dbo].[sp_GetSubCategory_menu]", new { LocationID });

	}
}
