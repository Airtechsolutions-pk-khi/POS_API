using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class Table
	{
		public int ID { get; set; }
		public int FloorID { get; set; }
		public string? Name { get; set; }
		public string? Covers { get; set; }
		public string? Type { get; set; }
		public string? LastUpdatedBy { get; set; }
		public string? LastUpdatedDate { get; set; }
		public string? CreatedOn { get; set; }
		public string? CreatedBy { get; set; }
		public int StatusID { get; set; }
	}
}
