using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class Customer
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public DateTime? DOB { get; set; }
		public string? Gender { get; set; }
		public string? Mobile { get; set; }
		public string? Address { get; set; }
		public int? StatusID { get; set; }
		public decimal? RedeemPoints { get; set; }
		public decimal? CurrentPoint { get; set; }
		public int? LocationID { get; set; }
	}
}
