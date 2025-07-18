using DataAccess.Data.DataModel;

namespace DataAccess.Models
{
    public class TableTreeDto
    {
        public int ID { get; set; }

        public string TableID { get; set; }

        public int? FloorID { get; set; }

        public string TableName { get; set; }

        public int? Capacity { get; set; }

        public string TableType { get; set; }

        public int? TableNo { get; set; }

        public int? x { get; set; }

        public int? y { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public int? StatusID { get; set; }
        public int TableStatus { get; set; }
        public List<WaiterDto> Waiters { get; set; } = new List<WaiterDto>();
    }
}
