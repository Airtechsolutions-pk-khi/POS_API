namespace DataAccess.Models
{
	public class RspModel
	{
		public int? Status { get; set; }
        public string? Description { get; set; }

    }

    public class RspModelQ
    {
        public int? Status { get; set; }
        public string? Description { get; set; }
        public string? QuotationNo { get; set; }

    }

    public class RspModelCus
    {
        public int? Status { get; set; }
        public string? Description { get; set; }
        public QuotationResponse Data { get; set; } // Use QuotationResponse instead of Customer
    }
    public class QuotationResponse
    {
        public int QuotationID { get; set; }
        public Customer Customer { get; set; }
    }
}
