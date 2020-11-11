namespace BirdWarsTest.Database
{
	class User
	{
		public User( int userId_In, string name_In, string lastName_In,
						string email_In, string password_In )
		{
			userId = userId_In;
			name = name_In;
			lastName = lastName_In;
			email = email_In;
			password = password_In;
		}

		public int userId;
		public string name;
		public string lastName;
		public string email;
		public string password;
	}
}