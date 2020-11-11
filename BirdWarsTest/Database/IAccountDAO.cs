using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	interface IAccountDAO
	{
		public bool Create( Account account );
		public List<Inventory> ReadAll();
		public Inventory Read( int accountId );
		public bool Update( int accountId, int userId, int matchesWon, int matchesLost, int matchesSurvived,
							int money );
		public bool Delete( int accountId );
	}
}
