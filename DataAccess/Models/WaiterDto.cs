using DataAccess.Data.DataModel;

namespace DataAccess.Models
{
    public class WaiterDto
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
    }
}
