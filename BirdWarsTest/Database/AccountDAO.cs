/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Account Data Access Object used to retrieve accounts
from the MySql Database.
*********************************************/
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BirdWarsTest.Database
{
	/// <summary>
	/// Account Data Access Object used to retrieve accounts
	/// from the MySql Database.
	/// </summary>
	public class AccountDAO : IAccountDAO
	{
		/// <summary>
		/// Creates a new instance of Account in the database.
		/// </summary>
		/// <param name="account">An existing account instance.</param>
		/// <returns>Bool indicating method success or failure.</returns>
		public bool Create( Account account )
		{
			bool wasCreated = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "INSERT INTO Account( userId, totalMatchesPlayed, matchesWon, matchesLost, " +
										  "matchesSurvived, money, seconds ) VALUES ( @userId, @totalMatchesPlayed, @matchesWon, " +
										  "@matchesLost, @matchesSurvived, @money, @seconds );";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.DatabaseConnection );
				MySqlParameter[] parameters = new MySqlParameter[ 7 ];
				parameters[ 0 ] = new MySqlParameter( "@userId", account.UserId );
				parameters[ 1 ] = new MySqlParameter( "@totalMatchesPlayed", account.TotalMatchesPlayed );
				parameters[ 2 ] = new MySqlParameter( "@matchesWon", account.MatchesWon );
				parameters[ 3 ] = new MySqlParameter( "@matchesLost", account.MatchesLost );
				parameters[ 4 ] = new MySqlParameter( "@matchesSurvived", account.MatchesSurvived );
				parameters[ 5 ] = new MySqlParameter( "@money", account.Money );
				parameters[ 6 ] = new MySqlParameter( "@seconds", account.Seconds );
				foreach ( var parameter in parameters )
				{
					command.Parameters.Add(parameter);
				}

				command.ExecuteNonQuery();
				wasCreated = true;
				Console.WriteLine( "Account created succesfully!" );
			}
			catch( MySqlException exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasCreated;
		}

		/// <summary>
		/// Deletes an account from the database.
		/// </summary>
		/// <param name="userId">An integer value representing a userId.</param>
		/// <returns>Bool indicating methos success or failure.</returns>
		public bool Delete( int userId )
		{
			bool wasDeleted = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "DELETE FROM Account WHERE userId = @userId";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.DatabaseConnection );
				MySqlParameter parameter = new MySqlParameter( "@userId", userId );
				command.Parameters.Add( parameter );

				command.ExecuteNonQuery();
				wasDeleted = true;
				Console.WriteLine( "Account deleted succesfully!" );
			}
			catch( MySqlException exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasDeleted;
		}

		/// <summary>
		/// Gets account information that matches the userId
		/// entered as a parameter.
		/// </summary>
		/// <param name="userId">An integer value representing a userId.</param>
		/// <returns>An account instance.</returns>
		public Account Read( int userId )
		{
			Account temp = null;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string MySqlCommandText = "SELECT * FROM Account WHERE userId = @userId";
				MySqlCommand command = new MySqlCommand( MySqlCommandText, connection.DatabaseConnection );
				MySqlParameter parameter = new MySqlParameter( "@userId", userId );
				command.Parameters.Add( parameter );
				MySqlDataReader reader = command.ExecuteReader();

				if( reader.HasRows && reader.Read() )
				{
					temp = new Account( reader.GetInt32( 0 ), reader.GetInt32( 1 ), reader.GetInt32( 2 ), reader.GetInt32( 3 ),
									    reader.GetInt32( 4 ), reader.GetInt32( 5 ), reader.GetInt32( 6 ), reader.GetInt32( 7 ) );
				}
				else
				{
					Console.WriteLine( "No accounts found." );
				}
				reader.Close();
			}
			catch( MySqlException exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return temp;
		}

		/// <summary>
		/// Gets a list of all accounts in the database.
		/// </summary>
		/// <returns>A list with all accounts.</returns>
		public List< Account > ReadAll()
		{
			List< Account > accounts = new List< Account >();
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "SELECT * FROM Account";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.DatabaseConnection );
				MySqlDataReader reader = command.ExecuteReader();

				while( reader.HasRows && reader.Read() )
				{
					accounts.Add( new Account( reader.GetInt32( 0 ), reader.GetInt32( 1 ), reader.GetInt32( 2 ), reader.GetInt32( 3 ),
										       reader.GetInt32( 4 ), reader.GetInt32( 5 ), reader.GetInt32( 6 ), reader.GetInt32( 7 ) ) );
					reader.NextResult();
				}
				reader.Close();
			}
			catch( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return accounts;
		}

		/// <summary>
		/// Updates a specific account that matches the userId parameter.
		/// </summary>
		/// <param name="userId">An integer value representing a userId.</param>
		/// <param name="totalMatchesPlayed">An integer value.</param>
		/// <param name="matchesWon">An integer value.</param>
		/// <param name="matchesLost">An integer value.</param>
		/// <param name="matchesSurvived">An integer value.</param>
		/// <param name="money">An integer value.</param>
		/// <param name="seconds">An integer value.</param>
		/// <returns>Bool indicating method success or failure/</returns>
		public bool Update( int userId, int totalMatchesPlayed,int matchesWon, int matchesLost, 
							int matchesSurvived, int money, int seconds )
		{
			bool wasUpdated = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "UPDATE Account SET totalMatchesPlayed = @totalMatchesPlayed, matchesWon = @matchesWon" +
										  ", matchesLost = @matchesLost, matchesSurvived = @matchesSurvived, " +
										  "money = @money, seconds = @seconds WHERE userId = @userId";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.DatabaseConnection );
				MySqlParameter [] parameters = new MySqlParameter[ 7 ];
				parameters[ 0 ] = new MySqlParameter( "@totalMatchesPlayed", totalMatchesPlayed );
				parameters[ 1 ] = new MySqlParameter( "@matchesWon", matchesWon );
				parameters[ 2 ] = new MySqlParameter( "@matchesLost", matchesLost );
				parameters[ 3 ] = new MySqlParameter( "@matchesSurvived", matchesSurvived );
				parameters[ 4 ] = new MySqlParameter( "@money", money );
				parameters[ 5 ] = new MySqlParameter( "@seconds", seconds );
				parameters[ 6 ] = new MySqlParameter( "@userId", userId );
				foreach ( var parameter in parameters )
				{
					command.Parameters.Add( parameter );
				}

				command.ExecuteNonQuery();
				wasUpdated = true;
				Console.WriteLine( "User Updated Successfully!" );
			}
			catch ( MySqlException exception )
			{
				Console.WriteLine( exception.Message );
				Console.Write( exception.StackTrace );
			}

			connection.StopConnection();
			return wasUpdated;
		}
	}
}