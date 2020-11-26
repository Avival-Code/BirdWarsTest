namespace BirdWarsTest.Database
{
	public class Account
	{
		public Account()
		{
			AccountId = 0;
			UserId = 0;
			TotalMatchesPlayed = 0;
			MatchesWon = 0;
			MatchesLost = 0;
			MatchesSurvived = 0;
			Money = 0;
		}
		public Account( int accountId_In, int userId_In )
		{
			AccountId = accountId_In;
			UserId = userId_In;
			TotalMatchesPlayed = 0;
			MatchesWon = 0;
			MatchesLost = 0;
			MatchesSurvived = 0;
			Money = 0;
		}

		public Account( int accountId_In, int userId_In, int totalMatchesPlayedIn, 
						int matchesWon_In, int matchesLost_In, int matchesSurvived_In, int money_In )
		{
			AccountId = accountId_In;
			UserId = userId_In;
			TotalMatchesPlayed = totalMatchesPlayedIn;
			MatchesWon = matchesWon_In;
			MatchesLost = matchesLost_In;
			MatchesSurvived = matchesSurvived_In;
			Money = money_In;
		}

		public int AccountId { get; set; }
		public int UserId { get; set; }
		public int TotalMatchesPlayed { get; set; }
		public int MatchesWon { get; set; }
		public int MatchesLost { get; set; }
		public int MatchesSurvived { get; set; }
		public int Money { get; set; }
	}
}