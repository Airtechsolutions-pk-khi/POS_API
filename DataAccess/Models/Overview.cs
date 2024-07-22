
namespace DataAccess.Models
{
	public class Overview
    {
         
        public Nullable<double> Orders { get; set; } = 0;        
        public Nullable<double> GrandTotal { get; set; } = 0;                
        public Nullable<double> NetSales { get; set; } = 0;
        public Nullable<double> TotalTax { get; set; } = 0;
       
    }
}
