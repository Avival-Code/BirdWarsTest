/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Stores variables necessary for game statistics.
*********************************************/
namespace BirdWarsTest.Database
{
	/// <summary>
	/// Stores variables necessary for game statistics.
	/// </summary>
	public class Account
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
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

		/// <summary>
		/// Copy constructor that takes another account
		/// as input.
		/// </summary>
		/// <param name="accountIn">An existing account.</param>
		public Account( Account accountIn )
		{
			AccountId = accountIn.AccountId;
			UserId = accountIn.UserId;
			TotalMatchesPlayed = accountIn.TotalMatchesPlayed;
			MatchesWon = accountIn.MatchesWon;
			MatchesLost = accountIn.MatchesLost;
			MatchesSurvived = accountIn.MatchesSurvived;
			Money = accountIn.Money;
			Seconds = accountIn.Seconds;
		}

		/// <summary>
		/// Constructor that takes an accountId and a
		/// userId as input.
		/// </summary>
		/// <param name="accountId_In">The instance Id.</param>
		/// <param name="userId_In">The user Id associated with this account.</param>
		public Account( int accountId_In, int userId_In )
		{
			AccountId = accountId_In;
			UserId = userId_In;
			TotalMatchesPlayed = 0;
			MatchesWon = 0;
			MatchesLost = 0;
			MatchesSurvived = 0;
			Money = 0;
			Seconds = 0;
		}

		/// <summary>
		/// Constructor to fill all variables ina account.
		/// </summary>
		/// <param name="accountId_In">The instance Id.</param>
		/// <param name="userId_In">The user Id associated with this account.</param>
		/// <param name="totalMatchesPlayedIn">An integer value.</param>
		/// <param name="matchesWon_In">An integer value.</param>
		/// <param name="matchesLost_In">An integer value.</param>
		/// <param name="matchesSurvived_In">An integer value.</param>
		/// <param name="money_In">An integer value.</param>
		/// <param name="secondsIn">An integer value.</param>
		public Account( int accountId_In, int userId_In, int totalMatchesPlayedIn, int matchesWon_In, int matchesLost_In, 
						int matchesSurvived_In, int money_In, int secondsIn )
		{
			AccountId = accountId_In;
			UserId = userId_In;
			TotalMatchesPlayed = totalMatchesPlayedIn;
			MatchesWon = matchesWon_In;
			MatchesLost = matchesLost_In;
			MatchesSurvived = matchesSurvived_In;
			Money = money_In;
			Seconds = secondsIn;
		}

		/// <summary>
		/// Add money acquired in game to total.
		/// </summary>
		/// <param name="amount">An integer value. The amount of money acquired.</param>
		public void AddMoney( int amount )
		{
			Money += amount;
		}

		/// <Value>The AccountId Property of AttackComponent.</Value>
		public int AccountId { get; set; }
		/// <Value>The UserId Property of AttackComponent.</Value>
		public int UserId { get; set; }
		/// <Value>The TotalMatchesPlayed Property of Account.</Value>
		public int TotalMatchesPlayed { get; set; }
		/// <Value>The MatchesWon Property of Account.</Value>
		public int MatchesWon { get; set; }
		/// <Value>The MatchesLost Property of Account.</Value>
		public int MatchesLost { get; set; }
		/// <Value>The MatchesSurvived Property of Account.</Value>
		public int MatchesSurvived { get; set; }
		/// <Value>The Money Property of Account.</Value>
		public int Money { get; set; }
		/// <Value>The Seconds Property of Account.</Value>
		public int Seconds { get; set; }
	}
}