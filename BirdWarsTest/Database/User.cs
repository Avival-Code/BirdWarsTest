namespace BirdWarsTest.Database
{
	public class User
	{
		public User()
		{
			userId = 0;
			names = "";
			lastName = "";
			username = "";
			email = "";
			password = "";
		}

		public User( User user )
		{
			userId = user.userId;
			names = user.names;
			lastName = user.lastName;
			username = user.username;
			email = user.email;
			password = user.password;
		}

		public User( int userId_In, string name_In, string lastName_In, string username_In,
					 string email_In, string password_In )
		{
			userId = userId_In;
			names = name_In;
			lastName = lastName_In;
			username = username_In;
			email = email_In;
			password = password_In;
		}

		public User( string name_In, string lastName_In, string username_In,
					 string email_In, string password_In )
		{
			userId = 0;
			names = name_In;
			lastName = lastName_In;
			username = username_In;
			email = email_In;
			password = password_In;
		}

		public int userId;
		public string names;
		public string lastName;
		public string username;
		public string email;
		public string password;
	}
}