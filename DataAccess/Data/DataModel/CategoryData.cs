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
    public class CategoryData : ICategoryData
	{
		private readonly IGenericCrudService _services;
		public CategoryData(IGenericCrudService services)
		{
			_services = services;
		}

		public Task<IEnumerable<Category>> GetCategories(int LocationID) =>
			_services.LoadData<Category, dynamic>("[dbo].[sp_GetCategory_menu]", new { LocationID });

	}
}
