
namespace DataAccess.Models
{
	public class StockReport
    {
        public string ItemName { get; set; } = "";
        public Nullable<double> ItemPrice { get; set; } = 0;
        public string Barcode { get; set; } = "";
        public Nullable<double> CostPrice { get; set; } = 0;
        public string StoreName { get; set; } = "";
        public Nullable<double> MinimumStock { get; set; } = 0;
        public Nullable<double> CurrentStock { get; set; } = 0;
        public Nullable<double> OpeningStock { get; set; } = 0;
        public Nullable<double> StockSold { get; set; } = 0;
        public Nullable<double> ReturnStock { get; set; } = 0;
    }
}
