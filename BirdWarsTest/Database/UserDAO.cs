using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	class UserDAO : IUserDAO
	{
		public bool Create( User user )
		{
			throw new NotImplementedException();
		}

		public bool Delete( int userId )
		{
			throw new NotImplementedException();
		}

		public Inventory Read( int userId )
		{
			throw new NotImplementedException();
		}

		public List<Inventory> ReadAll()
		{
			throw new NotImplementedException();
		}

		public bool Update( int userId, string name, string lastName, string email, string password )
		{
			throw new NotImplementedException();
		}
	}
}
