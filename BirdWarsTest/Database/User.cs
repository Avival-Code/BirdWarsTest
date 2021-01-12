/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Stores necessary user information.
*********************************************/
namespace BirdWarsTest.Database
{
	/// <summary>
	/// Stores necessary user information
	/// </summary>
	public class User
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public User()
		{
			UserId = 0;
			Names = "";
			LastName = "";
			Username = "";
			Email = "";
			Password = "";
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="user">An existing user instance.</param>
		public User( User user )
		{
			UserId = user.UserId;
			Names = user.Names;
			LastName = user.LastName;
			Username = user.Username;
			Email = user.Email;
			Password = user.Password;
		}

		/// <summary>
		/// Creates a new user with entered data.
		/// </summary>
		/// <param name="userId_In">An integer value.</param>
		/// <param name="name_In">A string value.</param>
		/// <param name="lastName_In">A string value.</param>
		/// <param name="username_In">A string value.</param>
		/// <param name="email_In">A string value.</param>
		/// <param name="password_In">A string value.</param>
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

		/// <summary>
		/// Creates a new user instance with no valid Id.
		/// </summary>
		/// <param name="name_In">A string value.</param>
		/// <param name="lastName_In">A string value.</param>
		/// <param name="username_In">A string value.</param>
		/// <param name="email_In">A string value.</param>
		/// <param name="password_In">A string value.</param>
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

		/// <Value>UserId</Value>
		public int UserId { get; set; }

		/// <Value>User names.</Value>
		public string Names { get; set; }

		/// <Value>User last names.</Value>
		public string LastName { get; set; }

		/// <Value>User username.</Value>
		public string Username { get; set; }

		/// <Value>User email.</Value>
		public string Email { get; set; }

		/// <Value>User password.</Value>
		public string Password { get; set; }
	}
}