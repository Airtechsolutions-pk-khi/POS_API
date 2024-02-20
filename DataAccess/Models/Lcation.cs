using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class Location
	{
		public int LocationID { get; set; }
        public int CountryID { get; set; }
        public int CityID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public int? TimeZoneID { get; set; }
        public int? LicenseID { get; set; }
        public decimal? DeliveryCharges { get; set; }
        public decimal? MinOrderAmount { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int? StatusID { get; set; }
        public string? CompanyCode { get; set; }
        public string? ImageURL { get; set; }
            
        public string? TAXID { get; set; }
        public string? CRN { get; set; }
        public string? FooterNotes { get; set; }
        public string? UserType { get; set; }
        public int? Tax { get; set; }
    }
}
