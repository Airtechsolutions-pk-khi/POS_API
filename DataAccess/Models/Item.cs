﻿namespace DataAccess.Models
{
	public class Item
	{
		public int ID { get; set; }
        public int ItemID { get; set; }
        public int? OrderID { get; set; }
        public int? CategoryID { get; set; }
		public int? LocationID { get; set; }
		public int? SubCategoryID { get; set; }
		public string? CategoryName { get; set; } = "";
		public string? SubCategoryName { get; set; } = "";
		public int? UnitID { get; set; }
		public string? Name { get; set; } = "";
		public float? Quantity { get; set; }
        public double? DiscountPrice { get; set; }
		public string? ArabicName { get; set; } = "";
		public string? NameOnReceipt { get; set; } = "";
		public string? Description { get; set; } = "";
		public string? ArabicDescription { get; set; } = "";
		public string? Image { get; set; }
        public Nullable<bool> IsSelected { get; set; }
        public Nullable<double> UnitPrice { get; set; }
        public Nullable<double> RefundPrice { get; set; }
        public Nullable<double> RefundQuantity { get; set; }
        public string? Barcode { get; set; } = "";
		public string? SKU { get; set; } = "";
		public int? DisplayOrder { get; set; }
        public int? ItemMode { get; set; }
        public bool? IsOpenPrice { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? IsStock { get; set; }
        public double? Price { get; set; }
        public double? MinPrice { get; set; }
        public double? NewPrice { get; set; }
		public double? Cost { get; set; }
		public string? ItemType { get; set; }
		public string? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDate { get; set; } = DateTime.UtcNow.AddMinutes(180);
        public int? StatusID { get; set; }
		public DateTime? CreatedOn { get; set; } = DateTime.UtcNow.AddMinutes(180);
        public string? CreatedBy { get; set; } = "";
		public double? CurrentStockLevel { get; set; }
		public List<OrderModifierDetail?>? Modifiers { get; set; } = new List<OrderModifierDetail?>();
		
	}
	public class OrderModifierDetail
	{
		public int? OrderDetailID { get; set; }
		public int? ModifierID { get; set; }
		public decimal? Price { get; set; }
		public int? StatusID { get; set; }
		public string? Type { get; set; }
		public string? Name { get; set; }
	}
}
