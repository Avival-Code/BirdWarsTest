/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Class that handles password reset code generation,
validation and password reset.
*********************************************/
using System;
using System.Collections.Generic;

namespace BirdWarsTest.Database
{
	/// <summary>
	/// Class that handles password reset code generation,
	/// validation and password reset.
	/// </summary>
	public class PasswordChangeManager
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PasswordChangeManager()
		{
			changeCodes = new List< int >();
			targetUserEmails = new List< string >();
		}

		/// <summary>
		/// Sets a random code for password reset email.
		/// </summary>
		/// <param name="userEmail">The email of the user that solicited the password change.</param>
		public void SetChangeCode( string userEmail )
		{
			var random = new Random();
			changeCodes.Add( random.Next( LowerLimit, UpperLimit ) );
			targetUserEmails.Add( userEmail );
		}

		/// <summary>
		/// Checks if the entered code value equals the generated code value
		/// and resets password if true.
		/// </summary>
		/// <param name="gameDatabase">Current database.</param>
		/// <param name="email">The email of the user that solicited the password change.</param>
		/// <param name="password">The user's new password</param>
		/// <param name="code">The confirmation code to check against.</param>
		/// <returns>bool indicating method success or failure.</returns>
		public bool ResetPassword( GameDatabase gameDatabase, string email, string password, string code )
		{
			bool passwordWasReset = false;
			if( int.Parse( code ) == GetResetCode( email ) )
			{
				gameDatabase.UpdateUserPassword( email, password );
				passwordWasReset = true;
				RemoveResetCodeAndEmail( email );
			}
			return passwordWasReset;
		}

		/// <summary>
		/// Returns the generated code for the introduced email.
		/// </summary>
		/// <param name="email">user email.</param>
		/// <returns>The randomly generated code.</returns>
		public int GetResetCode( string email )
		{
			int resetCode = 0;
			for( int i = 0; i < targetUserEmails.Count; i++ )
			{
				if( email.Equals( targetUserEmails[ i ] ) )
				{
					resetCode = changeCodes[ i ];
				}
			}
			return resetCode;
		}

		private void RemoveResetCodeAndEmail( string email )
		{
			for( int i = 0; i < targetUserEmails.Count; i++ )
			{
				if( email.Equals( targetUserEmails[ i ] ) )
				{
					targetUserEmails.RemoveAt( i );
					changeCodes.RemoveAt( i );
				}
			}
		}

		/// <summary>
		/// Removes all password reset petitions.
		/// </summary>
		public void ResetManager()
		{
			changeCodes.Clear();
			targetUserEmails.Clear();
		}

		/// <summary>
		/// Checks if a user solicited a password reset.
		/// </summary>
		/// <param name="userEmail">The user email.</param>
		/// <returns>bool that indicates if the user solicited a password reset.</returns>
		public bool PasswordChangeWasSolicited( string userEmail )
		{
			bool isInList = false;
			foreach( var email in targetUserEmails )
			{
				if( userEmail.Equals( email ) )
				{
					isInList = true;
				}
			}
			return isInList;
		}

		private List< int > changeCodes;
		private List< string > targetUserEmails;
		private const int LowerLimit = 1000;
		private const int UpperLimit = 10000;
	}
}