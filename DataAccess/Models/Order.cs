
namespace DataAccess.Models
{
    public class Order<T>
	{
		public int ID { get; set; }
        public int? OrderID { get; set; }
        public string? CustomerID { get; set; }
		public int? LocationID { get; set; } 
		public int? TransactionNo { get; set; }
		public int? OrderNo { get; set; }
		public int? TableNo { get; set; } = 0;
		public int? WaiterNo { get; set; } = 0;
		public int? OrderTakerID { get; set; } = 0;
        public string? OrderType { get; set; } = "";
        public int? GuestCount { get; set; } = 0;
        public string? Address { get; set; } = "";
        public string? NearestPlace { get; set; } = "";
		public int? AgentID { get; set; } = 0;
		public string? AgentName { get; set; } = "";
		public string? DeliveryTime { get; set; } = "";
		public double? Points { get; set; } = 0;	
        public string? OrderMode { get; set; } = "";
		public int? StatusID { get; set; } = 0;
		public int? LastUpdatedBy { get; set; } = 0;
        public int? Mode { get; set; } = 0;
        public DateTime? LastUpdatedDate { get; set; } = DateTime.UtcNow.AddMinutes(180);
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow.AddMinutes(180);
		public DateTime? OrderDate { get; set; } = DateTime.UtcNow.AddMinutes(180);
		public int? CreatedBy { get; set; }= 0;
		public bool? IsAvailiable { get; set; }
		public string? CounterType { get; set; } = "";
		public string? Country { get; set; } = "";
		public string? ContactNo { get; set; } = "";
		public int? DeliveryStatus { get; set; }
        public decimal? DiscountOnItem { get; set; }
        public string? FbrInvoiceNumber { get; set; } = "";
		public bool? IsOrderFbr { get; set; } 
		public string? FbrStatus { get; set; } = "";
		public double? GrandTotal { get; set; } = 0;
        public double? RefundAmount { get; set; } = 0;
        public double? AmountTotal { get; set; } = 0;
        public double? TotalRefund { get; set; } = 0;
        public double? AmountDiscount { get; set; } = 0;
		public double? ServiceCharges { get; set; } = 0;
		public double? Tax { get; set; } = 0;
		public string? PaymentType { get; set; } = "";
		public double? CashPayment { get; set; } = 0;
		public double? CardPayment { get; set; } = 0;
		public double? CreditPayment { get; set; } = 0;
		public bool? IsCheckout { get; set; }
		public int? PaymentMode { get; set; } = 0;
		public double? AmountPaid { get; set; } = 0;		
        public double? DiscountPrice { get; set; } = 0;
        public int? DiscountType { get; set; } = 0;
		public double? ItemDiscountAmount { get; set; } = 0;
		public string? CardNumber { get; set; } = "";
        public string? ReferenceNo { get; set; } = "";
        public string? CardHolderName { get; set; } = "";
		public string? CardType { get; set; } = "";
        public decimal? Cashback { get; set; }
        public bool? IsPartial { get; set; }
		public double? PartialAmount { get; set; }= 0;
		public double? FbrAmount { get; set; } = 0;
		public string? FbrInvoiceResponse { get; set; } = "";
		public string? DiscountReason { get; set; } = "";
		public string? DeliveryAddress { get; set; } = "";
        public string? FullName { get; set; } = "";
        public string? Mobile { get; set; } = "";
        public string? CustomerAddress { get; set; } = "";      
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
        public double? ItemDiscountAmount { get; set; } = 0;
        public string? CRN { get; set; }
        public string? TaxID { get; set; }
        public string? Email { get; set; }
        public string? CounterType { get; set; }
        public double? DiscountPercent { get; set; } = 0;
        public double? DiscountPrice { get; set; } = 0;
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? LocationName { get; set; }
        public DateTime? CreatedOn { get; set; }
		public double? Total { get; set; }
		public decimal? AmountDiscount { get; set; }
        public decimal? DiscountOnItem { get; set; }
        public decimal? Cashback { get; set; }
        public decimal? VATper { get; set; }
		public int OrderNo { get; set; }
		public int TransactionNo { get; set; }				
		public double? GrandTotal { get; set; }
		public double? Tax { get; set; }
        public double? ServiceCharges { get; set; }
        public string? FullName { get; set; }
        public string? Mobile { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CashPayment { get; set; }
        public string? CardPayment { get; set; }
        public string? CardType { get; set; }
        public string? CardNumber { get; set; }
        public string? ReferenceNo { get; set; }
        public IEnumerable<OrderDetail>? Items { get; set; }
    }
    public class OrderDetailIDReturn
    {
        public int? OrderDetailID { get; set; }
        

    }
}
