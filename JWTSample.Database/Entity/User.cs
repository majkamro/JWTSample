namespace JWTSample.Database.Entity
{
	public class User
	{
		public int Id { get; set; }
		public string UId { get; set; }
		public string Username { get; set; }
		public string passwordHash { get; set; }
		public string FullName { get; set; }
		public string Role { get; set; }
	}
}
