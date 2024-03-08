using DataAccess.Data.IDataModel;
using DataAccess.Models;
using DataAccess.Services.IService;
using System.ComponentModel.Design;

namespace DataAccess.Data.DataModel
{
    public class ReportData : IReportData
	{
		private readonly IGenericCrudService _service;
		public ReportData(IGenericCrudService service)
		{
			_service = service;
		}
        public async Task<IEnumerable<StockReport>> GetStockReportData(int LocationID)
        {
            var result = await _service.LoadData<StockReport, dynamic>(
                  "[dbo].[sp_StockStoreSummary_P_API]",
                  new { LocationID});
             
            return result;
        }

        public async Task<Report?> GetXZReportData(int SubUserID, int LocationID, DateTime OrderStartDate, DateTime OrderLastDate)
		{
            
               var result = await _service.LoadSingleOrDefaultData<Report, dynamic>(
                    "[dbo].[sp_ZXReport_P_API]",
                    new { SubUserID, LocationID, OrderLastDate, OrderStartDate });
             
            var resultOrderType = await _service.LoadData<CardTypeModel, dynamic>(
                "[dbo].[sp_RptOrderType_P_API]",
                new { SubUserID, LocationID, OrderLastDate, OrderStartDate });
            if (resultOrderType.Any())
            {
                foreach (var item in resultOrderType)
                {
                    if (item.CardType == "Talabat")
                    {
                        result.TalabatTotal = item.Total;
                    }
                    else if (item.CardType == "Visa")
                    {
                        result.VisaTotal = item.Total;
                    }
                    else if (item.CardType == "Master")
                    {
                        result.MasterTotal = item.Total;
                    }
                    else if (item.CardType == "StcPay")
                    {
                        result.StcpayTotal = item.Total;
                    }
                    else if (item.CardType == "Benefit Pay")
                    {
                        result.BenefitPayTotal = item.Total;
                    }
                    else if (item.CardType == "Jahez")
                    {
                        result.JahezTotal = item.Total;
                    }
                     
                    else if (item.CardType == "Ahlan")
                    {
                        result.AhlanTotal = item.Total;
                    }
                }                
            }
            else
            {
                result.TalabatTotal = 0;
                result.VisaTotal  = 0;
                result.MasterTotal = 0;
                result.StcpayTotal = 0;
                result.BenefitPayTotal =0;
                result.JahezTotal = 0;
                result.AhlanTotal = 0;
            }
			

            try
            {
                var dataOT = await _service.LoadData<SalesOT, dynamic>(
                "[dbo].[sp_SalesSummaryOTMultiLoc_P_API]",
                new { LocationID, OrderLastDate, OrderStartDate });

                var data = await _service.LoadData<Report, dynamic>(
               "[dbo].[sp_SalesSummaryMultiLoc_P_API]",
               new { LocationID, OrderLastDate, OrderStartDate });
                
                foreach (var item in data)
                {
                    result.CounterSellAmount = item.CounterSellAmount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Counter Order").Select(x => x.Value).FirstOrDefault());
                    result.CounterSellCount = item.CounterSellCount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Counter Order").Select(x => x.Count).FirstOrDefault());

                    result.DeliveryAmount = item.DeliveryAmount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Delivery").Select(x => x.Value).FirstOrDefault());
                    result.DeliveryCount = item.DeliveryCount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Delivery").Select(x => x.Count).FirstOrDefault());

                    result.PickUpAmount = item.PickUpAmount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Pick Up").Select(x => x.Value).FirstOrDefault());
                    result.PickUpCount = item.PickUpCount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Pick Up").Select(x => x.Count).FirstOrDefault());

                    result.TakeawayAmount = item.TakeawayAmount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Take Away").Select(x => x.Value).FirstOrDefault());
                    result.TakeawayCount = item.TakeawayCount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Take Away").Select(x => x.Count).FirstOrDefault());

                    result.WebCashAmount = item.WebCash = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Web Cash").Select(x => x.Value).FirstOrDefault());
                    result.WebCashCount = item.WebCashCount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Web Cash").Select(x => x.Count).FirstOrDefault());

                    result.WebCardAmount = item.WebCard = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Web Card").Select(x => x.Value).FirstOrDefault());
                    result.WebCardCount = item.WebCardCount = Convert.ToDouble(dataOT.Where(x => x.OrderType == "Web Card").Select(x => x.Count).FirstOrDefault());
                }
                

            }
            catch (Exception e)
            {
               
            }

            return result;
        }
    }
}