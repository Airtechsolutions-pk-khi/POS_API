
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
	public class ExpenseType
    {
		public int ExpenseTypeID { get; set; }		 
		public int? LocationID { get; set; }		 
		public string? Name { get; set; }		 		 
		public string? Description { get; set; }		 
		public int? StatusID { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? LastUpdatedDate { get; set; }
		 
	}
}
