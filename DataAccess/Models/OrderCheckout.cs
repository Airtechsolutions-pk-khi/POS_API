
namespace DataAccess.Models
{
    public class OrderCheckout<T>
	{
		public int ID { get; set; }
		public int? OrderID { get; set; }
		public int? LocationID { get; set; }
		public int? CustomerID { get; set; }
		public int? TransactionNo { get; set; }
		public int? OrderNo { get; set; }
		public int? PaymentMode { get; set; }
		public float? DiscountPercent { get; set; }
		public int? DiscountType { get; set; }
		public float? AmountDiscount { get; set; }
		public float? ItemAmountDiscount { get; set; }
		public float? AmountTotal { get; set; }
		public float? GrandTotal { get; set; }
		public float? Tax { get; set; }
	}
}
