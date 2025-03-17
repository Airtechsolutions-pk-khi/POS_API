
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class CQuotationDetailList
    {
        public int CQuotationDetailID { get; set; }
        public int CompanyQuotationID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemNameArabic { get; set; }
        public string Description { get; set; }
        public Nullable<double> UnitPrice { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<double> TaxRate { get; set; }
        public Nullable<double> TaxAmount { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<int> StatusID { get; set; }

    }
}
