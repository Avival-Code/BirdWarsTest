/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Stores the current user session information.
*********************************************/
namespace BirdWarsTest.Database
{
	/// <summary>
	/// Stores the current user session information.
	/// </summary>
	public class LoginSession
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public LoginSession()
		{
			CurrentUser = null;
			CurrentAccount = null;
			IsLoggedIn = false;
		}

		/// <summary>
		/// Creates a user sesion that is logged in
		/// with entered user's stored information.
		/// </summary>
		/// <param name="userIn">An existing user instance.</param>
		/// <param name="database">The gamedatabase.</param>
		public LoginSession( User userIn, GameDatabase database )
		{
			CurrentUser = userIn;
			CurrentAccount = database.Accounts.Read( CurrentUser.UserId );
			IsLoggedIn = true;
		}

		/// <summary>
		/// Retrieves user information and sets state to logged in.
		/// </summary>
		/// <param name="userIn">An existing user instance.</param>
		/// <param name="accountIn">An existing account instance.</param>
		public void Login( User userIn, Account accountIn )
		{
			CurrentUser = userIn;
			CurrentAccount = accountIn;
			IsLoggedIn = true;
		}

		/// <summary>
		/// Removes user information and sets state to logged out.
		/// </summary>
		public void Logout()
		{
			IsLoggedIn = false;
			CurrentUser = null;
			CurrentAccount = null;
		}

		/// <summary>
		/// Updates account data of the current user.
		/// </summary>
		/// <param name="isLocalPlayerDead">Bool stating if player is dead or alive.</param>
		/// <param name="didLocalPlayerWin">Bool stating if local player won round.</param>
		/// <param name="remainingRoundTime">Time left in game round.</param>
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

		/// <Value>States if this session is logged in.</Value>
		public bool IsLoggedIn { get; private set; }

		/// <Value>The current logged in user instance.</Value>
		public User CurrentUser { get; private set; }

		/// <Value>The current account of logged in user instance.</Value>
		public Account CurrentAccount { get; private set; }
	}
}