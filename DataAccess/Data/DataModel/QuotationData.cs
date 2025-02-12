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
        public async Task<IEnumerable<CompanyQuotationList>> GetAllQuotation(int UserID, int CompanyQuotationID)
        {
            try
            {
                // Execute stored procedure and get multiple result sets
                var result = await _service.LoadMultipleData<CompanyQuotationList, CQuotationDetailList, dynamic>(
                    "[dbo].[sp_QuotationAll_V2_P_API]",
                    new { UserID, CompanyQuotationID }
                );

                var companyQuotations = result.Item1.ToList(); // First result set (quotations)
                var companyQuotationDetails = result.Item2.ToList(); // Second result set (quotation details)

                // Mapping details to the main quotations
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

             
                var quotationID = await _service.LoadData<int, dynamic>("[dbo].[sp_InsertCompanyQuotation_P_API]", CompanyQuotationID);
               
                
                return new RspModel
                {
                    Status = 1,
                    Description = "Quotation added successfully!"
                };

            
        }
        public async Task<RspModel> SaveQuotation(CompanyQuotationList quotation)
        {
            RspModel model = new();

            if (quotation == null)
            {
                return new RspModel { Status = 0, Description = "Invalid quotation data!" };
            }
            try
            {
                var parameters = new
                {
                    quotation.QuotationNo,
                    quotation.QuotationDate,
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
                    quotation.TotalDiscount,
                    quotation.TotalAmount,
                    quotation.TotalVAT,
                    quotation.GrandTotal,
                    quotation.StatusID,
                    quotation.UserID,
                    LastUpdatedDate = DateTime.UtcNow.AddMinutes(180),
                    quotation.LastUpdatedBy,
                    quotation.Notes
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
                            detail.ItemID,
                            detail.ItemName,
                            detail.ItemNameArabic,
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

                return new RspModel
                {
                    Status = 1,
                    Description = "Quotation added successfully!"
                };
            }
            catch (Exception ex)
            {
                return new RspModel
                {
                    Status = 0,
                    Description = $"Failed to add quotation! Error: {ex.Message}"
                };
            }
        }

        //public async Task<RspModel> SaveQuotation(CompanyQuotationList quotation)
        //{

        //    RspModel model = new RspModel();
        //    RspModel error = new RspModel();

        //    try
        //    {
        //        var parameters = new
        //        {
        //            quotation.QuotationNo,
        //            quotation.QuotationDate,
        //            quotation.SupplyDate,
        //            quotation.TaxNo,
        //            quotation.SellerName,
        //            quotation.SellerAddress,
        //            quotation.SellerVAT,
        //            quotation.SellerContact,
        //            quotation.BuyerName,
        //            quotation.BuyerAddress,
        //            quotation.BuyerContact,
        //            quotation.BuyerVAT,
        //            quotation.TotalDiscount,
        //            quotation.TotalAmount,
        //            quotation.TotalVAT,
        //            quotation.GrandTotal,
        //            quotation.StatusID,
        //            quotation.UserID,
        //            LastUpdatedDate = DateTime.UtcNow.AddMinutes(180),
        //            quotation.LastUpdatedBy,
        //            quotation.Notes
        //        };

        //        // Call stored procedure with Dapper
        //        var quotationID = await _service.LoadData<int, dynamic>("[dbo].[sp_InsertCompanyQuotation_P_API]", parameters);

        //        // Insert Quotation Details if ID is returned
        //        if (quotation.CompanyQuotationDetails?.Any() == true)
        //        {
        //            foreach (var detail in quotation.CompanyQuotationDetails)
        //            {
        //                var detailParams = new
        //                {
        //                    CompanyQuotationID = quotationID,
        //                    detail.ItemID,
        //                    detail.ItemName,
        //                    detail.ItemNameArabic,
        //                    detail.UnitPrice,
        //                    detail.Quantity,
        //                    detail.Price,
        //                    detail.Discount,
        //                    detail.TaxRate,
        //                    detail.TaxAmount,
        //                    detail.Total,
        //                    detail.StatusID
        //                };

        //                await _service.LoadData<int, dynamic>("[dbo].[sp_InsertCompanyQuotationDetail_P_API]", detailParams);
        //            }
        //        }

        //         model = new()
        //        {
        //            Status = 1,
        //            Description = "Quotation is added!"
        //        };
        //        return model;

        //        //return Ok(new { Success = true, Message = "Quotation inserted successfully!", QuotationID = quotationID });
        //    }
        //    catch (Exception ex)
        //    {
        //        error = new()
        //        {
        //            Status = 0,
        //            Description = "Failed to add quotation!"
        //        };
        //        return error;
        //    }

        //    return model;
        //}
        public async Task EditQuotation(CompanyQuotationList customer) =>
            await _service.SaveData<dynamic>("[dbo].[sp_UpdateCustomer_P_API]",
                new { ParamTable1 = JsonConvert.SerializeObject(customer) });

        
    }
}
