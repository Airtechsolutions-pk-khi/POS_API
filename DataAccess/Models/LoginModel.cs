namespace DataAccess.Models
{
	public class LoginModel
	{
		public string? Token { get; set; }
		public SubUser? SubUserData { get; set; }
		public IEnumerable<Location>? Locations { get; set; }
	}
    public class AdminLoginModel
    {
        public string? Token { get; set; }
        public User? UserData { get; set; }
        public IEnumerable<Location>? Locations { get; set; }
    }
}
