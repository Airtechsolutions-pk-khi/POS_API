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
    public class ProductSummaryData : IProductSummaryData
	{
		private readonly IGenericCrudService _services;
		public ProductSummaryData(IGenericCrudService services)
		{
			_services = services;
		}

		public Task<IEnumerable<ProductSummary>> GetProductSummary(string OrderStartDate, string OrderLastDate, int LocationID) =>
			_services.LoadData<ProductSummary, dynamic>("[dbo].[sp_PRoductSummary]", new { OrderStartDate, OrderLastDate, LocationID });
		
		public Task<IEnumerable<UserProductSummary>> GetUserProductSummary(string OrderStartDate, string OrderLastDate, int LocationID) =>
			_services.LoadData<UserProductSummary, dynamic>("[dbo].[sp_UserPRoductSummary]", new { OrderStartDate, OrderLastDate, LocationID });

	}
}
