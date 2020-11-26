using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	interface IUserDAO
	{
		public bool Create( User user );
		public List< User > ReadAll();
		public User Read( string email );
		public User Read( string email, string password );
		public bool Update( int userId, string names, string lastName, string username, 
							string email, string password );
		public bool Delete( int userId );
	}
}
