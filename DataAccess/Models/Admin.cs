
namespace DataAccess.Models
{
	public class Admin

	{
		public string? LocationName { get; set; }
		public string? Type { get; set; }
		public double? GrossSales { get; set; } = 0;
		public double? CreditOrderDue { get; set; } = 0;
		public double? TotalDiscount { get; set; } = 0;
		public double? ServiceCharges { get; set; } = 0;
		public double? TaxPercent { get; set; } = 0;
		public double? TaxAmount { get; set; } = 0;
		public double? RefundAmount { get; set; } = 0;
		public double? NetSales { get; set; } = 0;
        public int? CashOrder { get; set; } = 0;
        public double? ItemDiscountAmount { get; set; } = 0;
        public double? CashOrderAmount { get; set;} = 0;
        public int? CreditOrder { get; set; } = 0;
		public double? CreditOrderAmount { get; set; } = 0;
		public int? CardOrder { get; set; } = 0;
		public double? CardOrderAmount { get; set; } = 0;
		public int? MultiOrder { get; set; } = 0;
		public int? VoidOrder { get; set; } = 0;
		public int? TotalOrders { get; set; } = 0;
		public double? Checkout { get; set; } = 0;
		public double? CheckoutOrders { get; set; } = 0;
         
        public double? Delivery { get; set; } = 0;
		public double? TotalExpanse { get; set; } = 0;
		public double? DeliveryOrders { get; set; } = 0;
		public double? Pickup { get; set; } = 0;
		 	 
        public double? WebCash { get; set; } = 0;		 
		public double? WebCard { get; set; } = 0;		 
        public decimal? VisaTotal { get; set; } = 0;
        public decimal? MasterTotal { get; set; } = 0;
        public decimal? StcpayTotal { get; set; } = 0;        
        public decimal? BenefitPayTotal { get; set; } = 0;
        public decimal? TalabatTotal { get; set; } = 0;
        public decimal? JahezTotal { get; set; } = 0;
        public decimal? AhlanTotal { get; set; } = 0;
        public double? Takeaway { get; set; } = 0;      

        //fill Order type counter
        public double? CounterSellCount { get; set; } = 0;
        public double? CounterSellAmount { get; set; } = 0;
        public double? DeliveryCount { get; set; } = 0;
        public double? DeliveryAmount { get; set; } = 0;
        public double? TakeawayCount { get; set; } = 0;
        public double? TakeawayAmount { get; set; } = 0;
        public double? PickUpCount { get; set; } = 0;
        public double? PickUpAmount { get; set; } = 0;
        public double? WebCashCount { get; set; } = 0;
        public double? WebCashAmount { get; set; } = 0;
        public double? WebCardCount { get; set; } = 0;
        public double? WebCardAmount { get; set; } = 0;

        //Stock Report

        public string ItemName { get; set; } = "";
        public Nullable<double> ItemPrice { get; set; } = 0;
        public string Barcode { get; set; } = "";
        public Nullable<double> CostPrice { get; set; } = 0;
        public string StoreName { get; set; } = "";
        public Nullable<double> MinimumStock { get; set; } = 0;
        public Nullable<double> CurrentStock { get; set; } = 0;
        public Nullable<double> OpeningStock { get; set; } = 0;
        public Nullable<double> StockSold { get; set; } = 0;
        public Nullable<double> ReturnStock { get; set; } = 0;
    }

    public class User
    {
        public int ID { get; set; }

        public int? CityID { get; set; }

        public int? CountryID { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Image { get; set; }

        public string Password { get; set; }

        public string Company { get; set; }

        public string BusinessType { get; set; }

        public string Email { get; set; }

        public string ContactNo { get; set; }

        public string Address { get; set; }

        public string Website { get; set; }

        public bool? Subscribe { get; set; }

        public int? RoleID { get; set; }

        public int? TimeZoneID { get; set; }

        public string Company_Code { get; set; }

        public string NIC { get; set; }

        public string Designation { get; set; }

        public double? BasicSalary { get; set; }

        public bool? WorkingStatus { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string UserType { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int? StatusID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string States { get; set; }

        public string Zipcode { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string CompanyURL { get; set; }

        public string CompanyDescription { get; set; }

        public int? IsActivate { get; set; }

        public string BusinessKey { get; set; }

    }
    
}
