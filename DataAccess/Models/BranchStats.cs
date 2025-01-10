
namespace DataAccess.Models
{
	public class BranchStats
    {
        public string BranchName { get; set; } = "";
        public double? CounterSale { get; set; } = 0.00;
        public int? OrderCount { get; set; } = 0;
    }
}
