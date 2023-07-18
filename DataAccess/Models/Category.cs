
namespace DataAccess.Models
{
	public class Category
	{
		public int ID { get; set; }
		public int LocationID { get; set; }
		public string? Name { get; set; }
		public string? ArabicName { get; set; }
		public string? Description { get; set; }
		public string? ArabicDescription { get; set; }
		public string? Image { get; set; }
		public int DisplayOrder { get; set; }
		public bool? SortByAlpha { get; set; }
		public string? LastUpdatedBy { get; set; }
		public System.DateTime? LastUpdatedDate { get; set; }
		public int? StatusID { get; set; }
		public System.DateTime? CreatedOn { get; set; }
		public string? CreatedBy { get; set; }
		public Location? Location { get; set; }
		public List<SubCategory>? SubCategories { get; set; } = new List<SubCategory>();

	}
}
