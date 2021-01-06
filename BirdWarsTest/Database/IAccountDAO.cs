using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	interface IAccountDAO
	{
		public bool Create( Account account );
		public List< Account > ReadAll();
		public Account Read( int userId );
		public bool Update( int userId, int totalMatchesPlayed, int matchesWon, int matchesLost, 
							int matchesSurvived, int money, int seconds );
		public bool Delete( int userId );
	}
}
