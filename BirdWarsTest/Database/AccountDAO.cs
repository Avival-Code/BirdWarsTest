using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	class AccountDAO : IAccountDAO
	{
		public bool Create( Account account )
		{
			throw new NotImplementedException();
		}

		public bool Delete( int accountId )
		{
			throw new NotImplementedException();
		}

		public Inventory Read( int accountId )
		{
			throw new NotImplementedException();
		}

		public List<Inventory> ReadAll()
		{
			throw new NotImplementedException();
		}

		public bool Update( int accountId, int userId, int matchesWon, int matchesLost, int matchesSurvived, int money )
		{
			throw new NotImplementedException();
		}
	}
}
