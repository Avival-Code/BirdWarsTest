using System;
using System.Collections.Generic;

namespace BirdWarsTest.Database
{
	public class PasswordChangeManager
	{
		public PasswordChangeManager()
		{
			changeCodes = new List< int >();
			targetUserEmails = new List< string >();
		}

		public void SetChangeCode( string userEmail )
		{
			var random = new Random();
			changeCodes.Add( random.Next( lowerLimit, upperLimit ) );
			targetUserEmails.Add( userEmail );
		}

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

		public void ResetPassword(  )
		{

		}

		public void ResetManager()
		{
			changeCodes.Clear();
			targetUserEmails.Clear();
		}

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

		public List< int > changeCodes;
		public List< string > targetUserEmails;
		private const int lowerLimit = 1000;
		private const int upperLimit = 10000;
	}
}