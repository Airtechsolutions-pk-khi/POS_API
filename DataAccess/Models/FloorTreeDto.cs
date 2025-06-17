namespace DataAccess.Models
{
    public class FloorTreeDto
    {
        public int ID { get; set; }

        public int? LocationID { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public int? StatusID { get; set; }

        public List<TableTreeDto> Tables { get; set; } = new List<TableTreeDto>();
    }
}
