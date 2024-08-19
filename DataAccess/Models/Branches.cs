
namespace DataAccess.Models
{
	public class Branches
    {
        public int? BranchLimit { get; set; }
        public int? DaysRemaining { get; set; }
        public string Name { get; set; } = "";
        public string ContactNo { get; set; } = "";
        public string Address { get; set; } = "";
        public string RegistrationNo { get; set; } = "";
        public string VatNo { get; set; } = "";
    }
}
