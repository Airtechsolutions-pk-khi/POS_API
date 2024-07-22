
namespace DataAccess.Models
{
	public class SalesSummaryOT
    {
        public string OrderType { get; set; } = "";
        public double Value { get; set; } = 0;
        public Nullable<int> Count { get; set; } = 0;
    }
}
