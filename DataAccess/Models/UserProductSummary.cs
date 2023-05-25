using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class UserProductSummary
	{
		public int ID { get; set; }
		public string? Name { get; set; }
		public double? ItemQuantity { get; set; }
		public double? SaleRevenue { get; set; }
		public double? Cost { get; set; }
		public double? DiscountPrice { get; set; }
		public double? Profit { get; set; }
		public string? Barcode { get; set; }
		public string? SKU { get; set; }
		public string? UserName { get; set; }
	}
}
