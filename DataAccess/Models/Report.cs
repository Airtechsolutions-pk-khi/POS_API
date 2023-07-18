
namespace DataAccess.Models
{
	public class Report
	{
		public string? LocationName { get; set; }
		public string? Type { get; set; }
		public double? GrossSales { get; set; }
		public double? CreditOrderDue { get; set; }
		public double? TotalDiscount { get; set; }
		public double? ServiceCharges { get; set; }
		public double? TaxPercent { get; set; }
		public double? TaxAmount { get; set; }
		public double? RefundAmount { get; set; }
		public double? NetSales { get; set; }
		public int? CashOrder { get; set; }
		public double? ItemDiscountAmount { get; set; }
		public double? CashOrderAmount { get; set; }
		public int? CreditOrder { get; set; }
		public double? CreditOrderAmount { get; set; }
		public int? CardOrder { get; set; }
		public double? CardOrderAmount { get; set; }
		public int? MultiOrder { get; set; }
		public int? VoidOrder { get; set; }
		public int? TotalOrders { get; set; }
		public int? Checkout { get; set; }
		public int? CheckoutOrders { get; set; }
		public int? Delivery { get; set; }
		public double? TotalExpanse { get; set; }
		public int? DeliveryOrders { get; set; }
		public int? Pickup { get; set; }
		public int? PickupCount { get; set; }
		public int? TakeAway { get; set; }
		public int? TakeAwayCount { get; set; }
		public int? WebCash { get; set; }
		public int? WebCashCount { get; set; }
		public int? WebCard { get; set; }
		public int? WebCardCount { get; set; }
	}
}
