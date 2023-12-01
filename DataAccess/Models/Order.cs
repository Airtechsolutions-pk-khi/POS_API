
namespace DataAccess.Models
{
    public class Order<T>
	{
		public int ID { get; set; }
		public string? CustomerID { get; set; }
		public int? LocationID { get; set; }
		public int? TransactionNo { get; set; }
		public int? OrderNo { get; set; }
		public int? TableNo { get; set; }
		public int? WaiterNo { get; set; }
		public int? OrderTakerID { get; set; }
		public string? OrderType { get; set; }
		public int? GuestCount { get; set; }
		public string? Address { get; set; }
		public string? NearestPlace { get; set; }
		public int? AgentID { get; set; }
		public string? AgentName { get; set; }
		public string? DeliveryTime { get; set; }
		public double? Points { get; set; }
		public string? OrderMode { get; set; }
		public int? StatusID { get; set; }
		public int? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDate { get; set; }
		public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
		public DateTime? OrderDate { get; set; } = DateTime.UtcNow;
		public int? CreatedBy { get; set; }
		public bool? IsAvailiable { get; set; }
		public string? CounterType { get; set; }
		public string? Country { get; set; }
		public string? ContactNo { get; set; }
		public int? DeliveryStatus { get; set; }
		public string? FbrInvoiceNumber { get; set; }
		public bool? IsOrderFbr { get; set; }
		public string? FbrStatus { get; set; }
		public double? GrandTotal { get; set; }
		public double? AmountTotal { get; set; }
		public double? AmountDiscount { get; set; }
		public double? ServiceCharges { get; set; }
		public double? Tax { get; set; }
		public string? PaymentType { get; set; }
		public double? CashPayment { get; set; }
		public double? CardPayment { get; set; }
		public double? CreditPayment { get; set; }
		public bool? IsCheckout { get; set; }
		public int? PaymentMode { get; set; }
		public double? AmountPaid { get; set; }
		public double? DiscountPercent { get; set; }
		public int? DiscountType { get; set; }
		public double? ItemDiscountAmount { get; set; }
		public string? CardNumber { get; set; }
		public string? CardHolderName { get; set; }
		public string? CardType { get; set; }
		public bool? IsPartial { get; set; }
		public double? PartialAmount { get; set; }
		public double? FbrAmount { get; set; }
		public string? FbrInvoiceResponse { get; set; }
		public string? DiscountReason { get; set; }
		public string? DeliveryAddress { get; set; }
		public IEnumerable<T>? Items { get; set; }
	}

	public class OrderReturn
	{
		public int OrderID { get; set; }
		public string? OrderType { get; set; }
        public DateTime? OrderCreatedDT { get; set; }
        public string? CashierName { get; set; }
        public string? PaymentType { get; set; }
        public string? CompanyName { get; set; }
        public string? CRN { get; set; }
        public string? TaxID { get; set; }
        public string? Email { get; set; }
        public string? CounterType { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? LocationName { get; set; }
        public DateTime? CreatedOn { get; set; }
		public decimal? Total { get; set; }
		public decimal? AmountDiscount { get; set; }
		public decimal? VATper { get; set; }
		public int OrderNo { get; set; }
		public int TransactionNo { get; set; }		
		public IEnumerable<OrderDetail>? Items { get; set; }
		public double? GrandTotal { get; set; }
		public double? Tax { get; set; }

	}
    public class OrderDetailIDReturn
    {
        public int? OrderDetailID { get; set; }
        

    }
}
