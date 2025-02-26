
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
	public class CompanyQuotationList
    {
        public int CompanyQuotationID { get; set; }
        public string QuotationNo { get; set; }
        public Nullable<System.DateTime> QuotationDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<System.DateTime> SupplyDate { get; set; }
        public string TaxNo { get; set; }
        public string SellerName { get; set; }
        public string SellerAddress { get; set; }
        public string SellerVAT { get; set; }
        public string SellerContact { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerContact { get; set; }
        public string BuyerVAT { get; set; }
        public string Notes { get; set; }
        public Nullable<double> TotalDiscount { get; set; }
        public Nullable<double> DiscountOnTotal { get; set; }
        public Nullable<double> TotalAmount { get; set; }
        public Nullable<double> SubTotal { get; set; }
        public Nullable<double> NetTotal { get; set; }
        public Nullable<double> TotalVAT { get; set; }
        public Nullable<double> GrandTotal { get; set; }
        public Nullable<double> DeliveryCharges { get; set; }
        public Nullable<double> ServiceCharges { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }       
        public List<CQuotationDetailList> CompanyQuotationDetails { get; set; } = new List<CQuotationDetailList>();
    }
}
