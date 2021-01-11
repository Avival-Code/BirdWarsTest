﻿namespace BirdWarsTest.Database
{
	public class LoginSession
	{
		public LoginSession()
		{
			CurrentUser = null;
			CurrentAccount = null;
			IsLoggedIn = false;
		}

		public LoginSession( User userIn, GameDatabase database )
		{
			CurrentUser = userIn;
			CurrentAccount = database.Accounts.Read( CurrentUser.UserId );
			IsLoggedIn = true;
		}

		public void Login( User userIn, Account accountIn )
		{
			CurrentUser = userIn;
			CurrentAccount = accountIn;
			IsLoggedIn = true;
		}

		public void Logout()
		{
			IsLoggedIn = false;
			CurrentUser = null;
			CurrentAccount = null;
		}

		public void UpdateRoundStatistics( bool isLocalPlayerDead, bool didLocalPlayerWin,
										   int remainingRoundTime )
		{
			CurrentAccount.TotalMatchesPlayed += 1;
			if( didLocalPlayerWin && !isLocalPlayerDead )
			{
				CurrentAccount.MatchesWon += 1;
			}
			else if( !isLocalPlayerDead && !didLocalPlayerWin )
			{
				CurrentAccount.MatchesSurvived += 1;
			}
			else if( isLocalPlayerDead && !didLocalPlayerWin )
			{
				CurrentAccount.MatchesLost += 1;
			}
			SetShortestTime( remainingRoundTime );
		}

		private void SetShortestTime( int remainingRoundTime )
		{
			int roundTime = CalculateRoundTime( remainingRoundTime );
			if ( CurrentAccount.Seconds <= roundTime )
			{
				CurrentAccount.Seconds = roundTime;
			}
		}

		private int CalculateRoundTime( int remainingRoundTime )
		{
			return 300 - remainingRoundTime;
		}

		public bool IsLoggedIn { get; set; }
		public User CurrentUser { get; set; }
		public Account CurrentAccount { get; set; }
	}
}