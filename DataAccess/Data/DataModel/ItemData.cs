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
    public class ItemData : IItemData
	{
		private readonly IGenericCrudService _services;
		public ItemData(IGenericCrudService services)
		{
			_services = services;
		}

		public Task<IEnumerable<Item>> GetItems(int LocationID) =>
			_services.LoadData<Item, dynamic>("[dbo].[sp_GetItems_P_API]", new { LocationID });
	}
}
