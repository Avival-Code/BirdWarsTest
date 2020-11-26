namespace BirdWarsTest.Database
{
	public class User
	{
		public User()
		{
			UserId = 0;
			Names = "";
			LastName = "";
			Username = "";
			Email = "";
			Password = "";
		}

		public User( User user )
		{
			UserId = user.UserId;
			Names = user.Names;
			LastName = user.LastName;
			Username = user.Username;
			Email = user.Email;
			Password = user.Password;
		}

		public User( int userId_In, string name_In, string lastName_In, string username_In,
					 string email_In, string password_In )
		{
			UserId = userId_In;
			Names = name_In;
			LastName = lastName_In;
			Username = username_In;
			Email = email_In;
			Password = password_In;
		}

		public User( string name_In, string lastName_In, string username_In,
					 string email_In, string password_In )
		{
			UserId = 0;
			Names = name_In;
			LastName = lastName_In;
			Username = username_In;
			Email = email_In;
			Password = password_In;
		}

		public int UserId { get; set; }
		public string Names { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}