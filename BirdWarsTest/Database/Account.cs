using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	class Account
	{
		public Account( int accountId_In, int userId_In )
		{
			accountId = accountId_In;
			userId = userId_In;
			matchesWon = 0;
			matchesLost = 0;
			matchesSurvived = 0;
			money = 0;
		}

		public Account( int accountId_In, int userId_In, int matchesWon_In, int matchesLost_In,
						int matchesSurvived_In, int money_In )
		{
			accountId = accountId_In;
			userId = userId_In;
			matchesWon = matchesWon_In;
			matchesLost = matchesLost_In;
			matchesSurvived = matchesSurvived_In;
			money = money_In;
		}

		public int accountId;
		public int userId;
		public int matchesWon;
		public int matchesLost;
		public int matchesSurvived;
		public int money;
	}
}
