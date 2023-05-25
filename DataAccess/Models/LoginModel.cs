using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class LoginModel
	{
		public string? Token { get; set; }
		public SubUser? SubUserData { get; set; }
		public IEnumerable<Location>? Locations { get; set; }
		//public User? UserData { get; set; }
	}
}
