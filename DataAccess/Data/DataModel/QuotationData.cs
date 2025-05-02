using DataAccess.Data.IDataModel;
using DataAccess.Models;
using DataAccess.Services.IService;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Data;

namespace DataAccess.Data.DataModel
{
    public class QuotationData : IQuotationData
    {
        private readonly IGenericCrudService _service;
        private readonly IMemoryCache _cache;

        public QuotationData(IGenericCrudService service, IMemoryCache cache)
        {
            _service = service;
            _cache = cache;
        }
        public async Task<IEnumerable<CompanyQuotationList>> GetAllQuotationData(string StartDate, string EndDate, int UserID, int LocationID)
        {
            IEnumerable<CompanyQuotationList>? res;
            try
            {
                //res = await _service.LoadData<CompanyQuotationList, dynamic>("[dbo].[sp_QuotationAll_P_API]", new { StartDate, EndDate, UserID });
                var result = await _service.LoadMultipleData<CompanyQuotationList, CQuotationDetailList, dynamic>(
                    "[dbo].[sp_QuotationAll_P_API_V2]",
                    new { StartDate, EndDate, UserID, LocationID }
                );

                 

                // Debug before processing
                if (result == null)
                {
                    Console.WriteLine("Result is NULL");
                }
                else
                {
                    Console.WriteLine($"Result Count: {result.Item1?.Count()} Quotations, {result.Item2?.Count()} Details");
                }

                var companyQuotations = result.Item1.ToList(); // First result set (quotations)
                var companyQuotationDetails = result.Item2.ToList(); // Second result set (quotation details)


                var quotationDict = companyQuotations.ToDictionary(q => q.CompanyQuotationID);

                foreach (var detail in companyQuotationDetails)
                {
                    if (quotationDict.TryGetValue(detail.CompanyQuotationID, out var quotation))
                    {
                        quotation.CompanyQuotationDetails ??= new List<CQuotationDetailList>();
                        quotation.CompanyQuotationDetails.Add(detail);
                    }
                }

                return companyQuotations;
            }
            catch (Exception ex)
            {

                throw;
            }
             
        }
        public async Task<IEnumerable<CompanyQuotationList>> GetAllQuotation(int UserID, int CompanyQuotationID)
        {
            try
            {
                
                var result = await _service.LoadMultipleData<CompanyQuotationList, CQuotationDetailList, dynamic>(
                    "[dbo].[sp_QuotationAll_V2_P_API]",
                    new { UserID, CompanyQuotationID }
                );

                var companyQuotations = result.Item1.ToList(); // First result set (quotations)
                var companyQuotationDetails = result.Item2.ToList(); // Second result set (quotation details)

                
                var quotationDict = companyQuotations.ToDictionary(q => q.CompanyQuotationID);

                foreach (var detail in companyQuotationDetails)
                {
                    if (quotationDict.TryGetValue(detail.CompanyQuotationID, out var quotation))
                    {
                        quotation.CompanyQuotationDetails ??= new List<CQuotationDetailList>();
                        quotation.CompanyQuotationDetails.Add(detail);
                    }
                }

                return companyQuotations;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task<IEnumerable<CompanyQuotationList>> GetAllQuotation(int UserID, int CompanyQuotationID)
        //{
        //    IEnumerable<CompanyQuotationList>? res;
        //    try
        //    {
        //        res = await _service.LoadData<CompanyQuotationList, dynamic>("[dbo].[sp_QuotationAll_V2_P_API]", new { UserID, CompanyQuotationID });
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //    return res;
        //}
        public async Task<RspModel> DeleteQuotation(int CompanyQuotationID)
        {
            RspModel model = new();

            var parameters = new { CompanyQuotationID }; // Correct way to pass parameters

            var quotationID = await _service.LoadData<int, dynamic>("[dbo].[sp_DeleteCompanyQuotation_P_API]", parameters);
            //var quotationID = await _service.LoadData<int, dynamic>("[dbo].[sp_DeleteCompanyQuotation_P_API]", CompanyQuotationID);
               
                
                return new RspModel
                {
                    Status = 1,
                    Description = "Quotation Deleted successfully!"
                };

            
        }
        public async Task<RspModelQ> SaveQuotation(CompanyQuotationList quotation)
        {
            RspModelQ model = new();

            if (quotation == null)
            {
                return new RspModelQ { Status = 0, Description = "Invalid quotation data!" };
            }
            try
            {
                var parameters = new
                {
                    quotation.CustomerID,
                    quotation.QuotationNo,
                    quotation.QuotationDate,
                    quotation.ExpiryDate,
                    quotation.SupplyDate,
                    quotation.TaxNo,
                    quotation.SellerName,
                    quotation.SellerAddress,
                    quotation.SellerVAT,
                    quotation.SellerContact,
                    quotation.BuyerName,
                    quotation.BuyerAddress,
                    quotation.BuyerContact,
                    quotation.BuyerVAT,
                    quotation.Notes,
                    quotation.TotalDiscount,
                    quotation.DiscountOnTotal,
                    quotation.TotalAmount,
                    quotation.SubTotal,
                    quotation.NetTotal,
                    quotation.TotalVAT,
                    quotation.GrandTotal,
                    quotation.DeliveryCharges,
                    quotation.ServiceCharges,
                    quotation.StatusID,
                    quotation.UserID,
                    LastUpdatedDate = DateTime.UtcNow.AddMinutes(180),
                    quotation.LastUpdatedBy,
                    quotation.TermAndCondition,
                    quotation.LocationID

                };
 

                // Call stored procedure with Dapper
                var quotationID = await _service.LoadData<int, dynamic>("[dbo].[sp_InsertCompanyQuotation_P_API]", parameters);
                int quotationID1 = quotationID.FirstOrDefault();
                // Insert Quotation Details if ID is valid
                if (quotation.CompanyQuotationDetails?.Any() == true)
                {
                    foreach (var detail in quotation.CompanyQuotationDetails)
                    {
                        var detailParams = new
                        {
                            CompanyQuotationID = quotationID1,
                            ItemID =  detail.ID,
                            ItemName = detail.Name,
                            ItemNameArabic = detail.ArabicName ?? "",
                            detail.UnitPrice,
                            detail.Quantity,
                            detail.Price,
                            detail.Discount,
                            detail.TaxRate,
                            detail.TaxAmount,
                            detail.Total,
                            detail.StatusID
                        };

                        await _service.LoadData<int, dynamic>("[dbo].[sp_InsertCompanyQuotationDetail_P_API]", detailParams);
                    }
                }

                return new RspModelQ
                {
                    Status = 1,
                    Description = "Quotation added successfully!",
                    QuotationNo = quotation.QuotationNo
                };
            }
            catch (Exception ex)
            {
                return new RspModelQ
                {
                    Status = 0,
                    Description = $"Failed to add quotation! Error: {ex.Message}"
                };
            }
        }

        public async Task<RspModelCus> EditQuotation(CompanyQuotationList quotation)
        {
            try
            {
                var parameters = new
                {
                    quotation.CompanyQuotationID,
                    quotation.CustomerID,
                    quotation.QuotationNo,
                    quotation.QuotationDate,
                    quotation.ExpiryDate,
                    quotation.SupplyDate,
                    quotation.TaxNo,
                    quotation.SellerName,
                    quotation.SellerAddress,
                    quotation.SellerVAT,
                    quotation.SellerContact,
                    quotation.BuyerName,
                    quotation.BuyerAddress,
                    quotation.BuyerContact,
                    quotation.BuyerVAT,
                    quotation.Notes,
                    quotation.TotalDiscount,
                    quotation.DiscountOnTotal,
                    quotation.TotalAmount,
                    quotation.SubTotal,
                    quotation.NetTotal,
                    quotation.TotalVAT,
                    quotation.GrandTotal,
                    quotation.DeliveryCharges,
                    quotation.ServiceCharges,
                    quotation.StatusID,
                    quotation.UserID,
                    LastUpdatedDate = DateTime.UtcNow.AddMinutes(180),
                    quotation.LastUpdatedBy,
                    quotation.TermAndCondition,
                    quotation.LocationID
                };

                // Execute stored procedure to edit quotation
                var quotationIDList = await _service.LoadData<int, dynamic>("[dbo].[sp_EditCompanyQuotation_P_API_V2]", parameters);
                int quotationID = quotationIDList.FirstOrDefault();

                // If quotation details exist, insert them
                if (quotation.CompanyQuotationDetails?.Any() == true)
                {
                    var detailTasks = quotation.CompanyQuotationDetails.Select(detail =>
                    {
                        var detailParams = new
                        {
                            CompanyQuotationID = quotationID,
                            ItemID = detail.ID,
                            ItemName = detail.Name,
                            ItemNameArabic = detail.ArabicName ?? "",
                            detail.UnitPrice,
                            detail.Quantity,
                            detail.Price,
                            detail.Discount,
                            detail.TaxRate,
                            detail.TaxAmount,
                            detail.Total,
                            detail.StatusID
                        };

                        return _service.LoadData<int, dynamic>("[dbo].[sp_InsertCompanyQuotationDetail_P_API]", detailParams);
                    });

                    await Task.WhenAll(detailTasks);
                     
                }

                // Fetch customer data
                var customerData = await _service.LoadData<Customer, dynamic>("[dbo].[sp_GetCustomerById_P_API]", new { quotation.CustomerID });
                var customer = customerData.FirstOrDefault();

                return new RspModelCus
                {
                    Status = 1,
                    Description = "Quotation updated successfully!",
                    Data = new QuotationResponse
                    {
                        QuotationID = quotationID,
                        Customer = customer
                    }
                };

                //return new RspModel
                //{
                //    Status = 1,
                //    Description = "Quotation updated successfully!"
                //};
            }
            catch (Exception ex)
            {
                return new RspModelCus
                {
                    Status = 0,
                    Description = $"Failed to update quotation! Error: {ex.Message}"
                };
            }
        }

        //public async Task EditQuotation(CompanyQuotationList companyQuotation) =>
        //    await _service.SaveData<dynamic>("[dbo].[sp_UpdateCompanyQuotation_P_API]",
        //        new { ParamTable1 = JsonConvert.SerializeObject(companyQuotation) });

    }
}
