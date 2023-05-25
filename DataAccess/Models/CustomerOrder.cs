using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class CustomerOrder
	{
		public int CustomerOrderID { get; set; }
		public string? Address { get; set; }
		public string? NearestPlace { get; set; }
		public string? Country { get; set; }
		public string? City { get; set; }
		public string? ContactNo { get; set; }
		public int OrderID { get; set; }
		public System.DateTime? DeliveryDate { get; set; }
		public string? DeliveryTime { get; set; }
		public string? CustomerName { get; set; }
		public int? CustomerID { get; set; }
		public string? Latitude { get; set; }
		public string? Longitude { get; set; }
		public string? PlaceType { get; set; }
		public System.DateTime? LastUpdatedDate { get; set; }
		public int? LastUpdatedBy { get; set; }
		public string? Email { get; set; }
		public string? CardNotes { get; set; }
		public string? SelectedTime { get; set; }
		public string? SenderName { get; set; }
		public string? SenderEmail { get; set; }
		public string? SenderContact { get; set; }
		public string? CouponCode { get; set; }
		public int? ShippingStatus { get; set; }
		public int? LocationID { get; set; }
		public int? TransactionNo { get; set; }
		public int? OrderNo { get; set; }
		public int? TableNo { get; set; }
		public int? WaiterNo { get; set; }
		public int? GuestCount { get; set; }
		public string? DeliveryAddress { get; set; }
		public int? StatusID { get; set; }
		public int? DeliveryStatus { get; set; }
		public decimal? AmountTotal { get; set; }
		public decimal? GrandTotal { get; set; }
		public int? ItemId { get; set; }
		public string? Name { get; set; }
		public string? ItemType { get; set; }
		public int? Quantity { get; set; }
		public decimal? Price { get; set; }
		public decimal? DiscountPrice { get; set; }
		public decimal? Total { get; set; }
	}
}
