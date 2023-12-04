
namespace DataAccess.Models
{
	public class OrderDetail
	{
		public int OrderID { get; set; }
        public int ID { get; set; }
        public int OrderDetailID { get; set; }
		public int ItemID { get; set; }
		public string? TransactionNo { get; set; }
		public string? OrderNo { get; set; }
		public string? Name { get; set; }
		public string? ItemName { get; set; }
		public string? Image { get; set; }
		public int? Quantity { get; set; }
		public decimal? Price { get; set; }
		public decimal? DiscountPrice { get; set; }
		public decimal? PriceWithVAT { get; set; }
		public decimal? Cost { get; set; }
		public decimal? TotalCost { get; set; }
		public int? LoyaltyPoints { get; set; }
		public int? CurrentQty { get; set; }
		public bool? IsComplementory { get; set; }
		//public string? OrderMode { get; set; }
		public int? StatusID { get; set; }
		public string? LastUpdateBy { get; set; }
		public DateTime? LastUpdateDT { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string? CreatedBy { get; set; }
		public string? ItemType { get; set; }
		public int? DealID { get; set; }
		public bool? IsVATApplied { get; set; }
		public string? ItemCode { get; set; }
		public string? Description { get; set; }
		public IEnumerable<OrderModifierDetail?>? Modifiers { get; set; }
	}
}
