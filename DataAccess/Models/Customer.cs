
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
	public class Customer
	{
		public int? ID { get; set; }
		public int? CountryID { get; set; }
		public int? CityID { get; set; }
		public int? UserID { get; set; }
		public string? FullName { get; set; }

		[EmailAddress]
		public string? Email { get; set; }
		public string? DOB { get; set; }
		public string? Gender { get; set; }
        public string? NationalID { get; set; }

        [Phone]
		public string? Mobile { get; set; }
		public bool? IsEmail { get; set; }
		public bool? IsSms { get; set; }
		public bool? IsActive { get; set; }
		public string? Address { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyID { get; set; }
        public string? VATNo { get; set; }
        public string? CustomerType { get; set; }

        [Range(1, 3)]
		public int? StatusID { get; set; } = 1;
		public decimal? RedeemPoints { get; set; }
		public decimal? CurrentPoint { get; set; }
		public int? LocationID { get; set; }
	}
}
