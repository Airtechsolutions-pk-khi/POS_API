
namespace DataAccess.Models
{
	public class BranchStats
    {
         
        public string BranchName { get; set; } = "";        
        public Nullable<double> CounterSale { get; set; } = 0;                
        public int? OrderCount { get; set; } = 0;
        
       
    }
}
