using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	interface IUserDAO
	{
		public bool Create( User user );
		public List<Inventory> ReadAll();
		public Inventory Read( int userId );
		public bool Update( int userId, string names, string lastName, string email, string password );
		public bool Delete( int userId );
	}
}
