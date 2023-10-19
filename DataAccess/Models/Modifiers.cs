
namespace DataAccess.Models
{
	public class Modifiers
	{
		public int ID { get; set; }
		public string? Name { get; set; }
		public string? ArabicName { get; set; }
		public string? Description { get; set; }
		public string? Type { get; set; }
		public string? Barcode { get; set; }
		public string? SKU { get; set; }
		public string? Image { get; set; }
		public float Price { get; set; }
		public int? DisplayOrder { get; set; }
		public string? LastUpdatedBy { get; set; }
		public System.DateTime? LastUpdatedDate { get; set; }
		public int? StatusID { get; set; }
		public System.DateTime? CreatedOn { get; set; }
		public string? CreatedBy { get; set; }
		public int? UserID { get; set; }
	}
}
