namespace DataAccess.Models
{
	public class Item
	{
		public int ID { get; set; }
		public int? CategoryID { get; set; }
		public int? LocationID { get; set; }
		public int? SubCategoryID { get; set; }
		public string? CategoryName { get; set; }
		public string? SubCategoryName { get; set; }
		public int? UnitID { get; set; }
		public string? Name { get; set; }
		public int? Quantity { get; set; }
		//public double? DiscountPrice { get; set; }
		public string? ArabicName { get; set; }
		public string? NameOnReceipt { get; set; }
		public string? Description { get; set; }
		public string? ArabicDescription { get; set; }
		public string? Image { get; set; }
		public string? Barcode { get; set; }
		public string? SKU { get; set; }
		public int? DisplayOrder { get; set; }
		//public bool? SortByAlpha { get; set; }
		public double? Price { get; set; }
		public double? NewPrice { get; set; }
		public double? Cost { get; set; }
		public string? ItemType { get; set; }
		public string? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDate { get; set; }
		public int? StatusID { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string? CreatedBy { get; set; }
		public double? CurrentStockLevel { get; set; }
		//public bool? IsVATApplied { get; set; }
		//public bool? IsFeatured { get; set; }
		//public bool? IsStockOut { get; set; }
	}
}
