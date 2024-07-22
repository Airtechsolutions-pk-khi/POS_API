
namespace DataAccess.Models
{
	public class SalesSummary
    {
        public Nullable<double> GrossSales { get; set; } = 0;
        public Nullable<double> CreditOrderDue { get; set; } = 0;
        public Nullable<double> TotalDiscount { get; set; } = 0;
        public Nullable<double> ServiceCharges { get; set; } = 0;
        public Nullable<decimal> TaxPercent { get; set; }   
        public Nullable<double> TaxAmount { get; set; } = 0;
        public double RefundAmount { get; set; }
        public double OldInvRefundAmount { get; set; }
        public Nullable<double> TotalExpense { get; set; } = 0;
        public Nullable<double> NetSales { get; set; } = 0;
        public Nullable<int> CashOrder { get; set; } = 0;
        public Nullable<double> ItemDiscountAmount { get; set; } = 0;
        public Nullable<double> CashOrderAmount { get; set; } = 0;
        public Nullable<int> CreditOrder { get; set; } = 0;
        public Nullable<double> CreditOrderAmount { get; set; } = 0;
        public Nullable<int> CardOrder { get; set; } = 0;
        public Nullable<double> CardOrderAmount { get; set; } = 0;
        public Nullable<int> MultiOrder { get; set; } = 0;
        public Nullable<int> VoidOrder { get; set; } = 0;
        public Nullable<int> RefundOrder { get; set; } = 0;
        public Nullable<int> TotalOrders { get; set; } = 0;
        public Nullable<double> Checkout { get; set; } = 0;
        public Nullable<double> CheckoutOrders { get; set; } = 0;
        public Nullable<double> Delivery { get; set; } = 0;
        public Nullable<double> DeliveryOrders { get; set; } = 0;
        public Nullable<double> Pickup { get; set; } = 0;
        public Nullable<double> PickupCount { get; set; } = 0;
        public Nullable<double> Takeaway { get; set; } = 0;
        public Nullable<double> TakeawayCount { get; set; } = 0;
        public Nullable<double> WebCash { get; set; } = 0;
        public Nullable<double> WebCashCount { get; set; } = 0;
        public Nullable<double> WebCard { get; set; } = 0;
        public Nullable<double> WebCardCount { get; set; } = 0;
    }
}
