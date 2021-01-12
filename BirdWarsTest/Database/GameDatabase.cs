/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Class that stores Data Access Objects used to retrieve users
and accounts.
*********************************************/
using System.Collections.Generic;

namespace BirdWarsTest.Database
{
	/// <summary>
	/// Class that stores Data Access Objects used to retrieve users
	/// and accounts.
	/// </summary>
	public class GameDatabase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public GameDatabase()
		{
			Users = new UserDAO();
			Accounts = new AccountDAO();
		}

		/// <summary>
		/// Updates user password. Takes the email of the user soliciting
		/// password change.
		/// </summary>
		/// <param name="email">String. Email of the user.</param>
		/// <param name="newPassword">String. The new password.</param>
		public void UpdateUserPassword( string email, string newPassword )
		{
			var user = Users.Read( email );
			if( user != null )
			{
				Users.Update( user.UserId, user.Names, user.LastName, user.Username, user.Email, newPassword );
			}
		}

		/// <summary>
		/// Updates the user and accont information.
		/// </summary>
		/// <param name="userIn">New user data.</param>
		/// <param name="accountIn">New account data.</param>
		public void UpdateUserInformation( User userIn, Account accountIn )
		{
			Users.Update( userIn.UserId, userIn.Names, userIn.LastName, userIn.Username, userIn.Email, userIn.Password );
			Accounts.Update( userIn.UserId, accountIn.TotalMatchesPlayed, accountIn.MatchesWon, accountIn.MatchesLost,
							 accountIn.MatchesSurvived, accountIn.Money, accountIn.Seconds );
		}

		/// <summary>
		/// Checks if any of the registered users are using the same 
		/// userame.
		/// </summary>
		/// <param name="username">The username to look for.</param>
		/// <returns></returns>
		public bool DoesUsernameExist( string username )
		{
			bool usernameExists = false;
			List< User > listOfUsers = Users.ReadAll();
			foreach( var user in listOfUsers )
			{
				if( user.Username.Equals( username ) )
				{
					usernameExists = true;
				}
			}
			return usernameExists;
		}

		/// <Value>UserDAO</Value>
		public UserDAO Users { get; set; }

		/// <Value>AccountDAO</Value>
		public AccountDAO Accounts { get; set; }
	}
}