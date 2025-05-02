
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
	public class Expense
	{
		public int ExpenseID { get; set; }
		public int? ExpenseTypeID { get; set; }
        public int? PaymentTypeID { get; set; }
        public int? LocationID { get; set; }		 
		public string? Name { get; set; }
        public string? ExpenseNo { get; set; }
        public string? InvoiceReferenceNo { get; set; }
        public float? Amount { get; set; }
		public string? Date { get; set; }
		public string? Reason { get; set; }
        public string? Contact { get; set; }
        public string? Description { get; set; }
        public string? PaymentMethod { get; set; }
        public int? StatusID { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? LastUpdatedDate { get; set; }
        public string? CategoryName { get; set; } = "";

    }
}
