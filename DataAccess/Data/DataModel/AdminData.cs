using DataAccess.Data.IDataModel;
using DataAccess.Models;
using DataAccess.Services.IService;
using System.ComponentModel.Design;

namespace DataAccess.Data.DataModel
{
    public class AdminData : IAdminData
	{
		private readonly IGenericCrudService _service;
		public AdminData(IGenericCrudService service)
		{
			_service = service;
		}
        
        public async Task<IEnumerable<Overview>> GetOverViewData(string Locations, string OrderStartDate, string OrderLastDate)
        {
            var result = await _service.LoadData<Overview, dynamic>(
                  "[dbo].[sp_GetOverviewData_Track_API]",
                  new { Locations, OrderStartDate, OrderLastDate });

            return result;
        }

        public async Task<IEnumerable<ThreeMonthSales>> SaleStatistics(string Locations)
        {
            var result = await _service.LoadData<ThreeMonthSales, dynamic>(
                  "[dbo].[sp_GetDashboardLastThreeMonthsSales_Track_API]",
                  new { Locations });

            return result;
        }
        public async Task<IEnumerable<BranchStats>> BranchStats(string Locations, string OrderStartDate, string OrderLastDate)
        {
            var result = await _service.LoadData<BranchStats, dynamic>(
                  "[dbo].[sp_GetBranchStats_Track_API]",
                  new { Locations, OrderStartDate, OrderLastDate });

            return result;
        }
        public async Task<IEnumerable<BestSellingItems>> BestItems(string Locations, string StartDate, string EndDate)
        {
            var result = await _service.LoadData<BestSellingItems, dynamic>(
                  "[dbo].[sp_BestSellingItems_Track_API]",
                  new { Locations, StartDate, EndDate });

            return result;
        }
        public async Task<IEnumerable<StockAlert>> StockAlert(string Locations, string StartDate, string EndDate)
        {
            var result = await _service.LoadData<StockAlert, dynamic>(
                  "[dbo].[sp_StockAlert_Track_API]",
                  new { Locations, StartDate, EndDate });

            return result;
        }
        public async Task<IEnumerable<SeavenDaysSales>> LastSeavenDaysSales(string Locations)
        {
            var result = await _service.LoadData<SeavenDaysSales, dynamic>(
                  "[dbo].[sp_Last7DaysSales_Track_API]",
                  new { Locations });

            return result;
        }
        public async Task<IEnumerable<Branches>> Branches(int userid)
        {
            var result = await _service.LoadData<Branches, dynamic>(
                  "[dbo].[sp_BranchesDetail_Track_API]",
                  new { userid });

            return result;
        }

        
        public async Task<IEnumerable<SalesSummary>> SalesSummary(string Locations, string OrderStartDate, string OrderLastDate)
        {
            var dataOT = await _service.LoadData<SalesSummaryOT, dynamic>(
                  "[dbo].[sp_SalesSummaryOTMultiLoc_Track_API]",
                  new { Locations, OrderStartDate, OrderLastDate });

            var data = await _service.LoadData<SalesSummary, dynamic>(
                 "[dbo].[sp_SalesSummaryMultiLoc_V2_Track_API]",
                 new { Locations, OrderStartDate, OrderLastDate });
            foreach (var item in data)
            {
                item.Checkout = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Counter Order").Select(x => x.Value).FirstOrDefault());
                item.CheckoutOrders = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Counter Order").Select(x => x.Count).FirstOrDefault());

                item.Delivery = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Delivery").Select(x => x.Value).FirstOrDefault());
                item.DeliveryOrders = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Delivery").Select(x => x.Count).FirstOrDefault());

                item.Pickup = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Pick Up").Select(x => x.Value).FirstOrDefault());
                item.PickupCount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Pick Up").Select(x => x.Count).FirstOrDefault());

                item.Takeaway = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Take Away").Select(x => x.Value).FirstOrDefault());
                item.TakeawayCount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Take Away").Select(x => x.Count).FirstOrDefault());

                item.WebCash = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Web Cash").Select(x => x.Value).FirstOrDefault());
                item.WebCashCount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Web Cash").Select(x => x.Count).FirstOrDefault());

                item.WebCard = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Web Card").Select(x => x.Value).FirstOrDefault());
                item.WebCardCount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Web Card").Select(x => x.Count).FirstOrDefault());
            }
            return data;
        }

    }
}