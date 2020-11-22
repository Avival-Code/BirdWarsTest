﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	public class AccountDAO : IAccountDAO
	{
		public bool Create( Account account )
		{
			bool wasCreated = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "INSERT INTO Account( userId, totalMatchesPlayed, matchesWon, matchesLost, " +
										  "matchesSurvived, money ) VALUES ( @userId, @totalMatchesPlayed, @matchesWon, " +
										  "@matchesLost, @matchesSurvived, @money );";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
				MySqlParameter[] parameters = new MySqlParameter[ 6 ];
				parameters[ 0 ] = new MySqlParameter( "@userId", account.userId );
				parameters[ 1 ] = new MySqlParameter( "@totalMatchesPlayed", account.totalMatchesPlayed );
				parameters[ 2 ] = new MySqlParameter( "@matchesWon", account.matchesWon );
				parameters[ 3 ] = new MySqlParameter( "@matchesLost", account.matchesLost );
				parameters[ 4 ] = new MySqlParameter( "@matchesSurvived", account.matchesSurvived );
				parameters[ 5 ] = new MySqlParameter( "@money", account.money );
				foreach( var parameter in parameters )
				{
					command.Parameters.Add(parameter);
				}

				command.ExecuteNonQuery();
				wasCreated = true;
				Console.WriteLine( "Account created succesfully!" );
			}
			catch( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasCreated;
		}

		public bool Delete( int userId )
		{
			bool wasDeleted = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "DELETE FROM Account WHERE userId = @userId";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
				MySqlParameter parameter = new MySqlParameter( "@userId", userId );
				command.Parameters.Add( parameter );

				command.ExecuteNonQuery();
				wasDeleted = true;
				Console.WriteLine( "Account deleted succesfully!" );
			}
			catch( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasDeleted;
		}

		public Account Read( int userId )
		{
			Account temp = null;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string MySqlCommandText = "SELECT * FROM Account WHERE userId = @userId";
				MySqlCommand command = new MySqlCommand( MySqlCommandText, connection.connection );
				MySqlParameter parameter = new MySqlParameter( "@userId", userId );
				command.Parameters.Add( parameter );
				MySqlDataReader reader = command.ExecuteReader();

				if( reader.HasRows && reader.Read() )
				{
					temp = new Account( reader.GetInt32( 0 ), reader.GetInt32( 1 ), reader.GetInt32( 2 ), reader.GetInt32( 3 ),
									    reader.GetInt32( 4 ), reader.GetInt32( 5 ), reader.GetInt32( 6 ) );
				}
				else
				{
					Console.WriteLine( "No accounts found." );
				}
				reader.Close();
			}
			catch ( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return temp;
		}

		public List< Account > ReadAll()
		{
			List< Account > accounts = new List< Account >();
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "SELECT * FROM Account";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
				MySqlDataReader reader = command.ExecuteReader();

				while( reader.HasRows && reader.Read() )
				{
					accounts.Add( new Account( reader.GetInt32( 0 ), reader.GetInt32( 1 ), reader.GetInt32( 2 ), reader.GetInt32( 3 ),
										       reader.GetInt32( 4 ), reader.GetInt32( 5 ), reader.GetInt32( 6 ) ) );
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

		public bool Update( int userId, int totalMatchesPlayed,int matchesWon, 
							int matchesLost, int matchesSurvived, int money )
		{
			bool wasUpdated = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "UPDATE Account SET totalMatchesPlayed = @totalMatchesPlayed, matchesWon = @matchesWon" +
										  ", matchesLost = @matchesLost, matchesSurvived = @matchesSurvived, " +
										  "money = @money WHERE userId = @userId";
				MySqlCommand command = new MySqlCommand(mySqlCommandText, connection.connection);
				MySqlParameter[] parameters = new MySqlParameter[ 5 ];
				parameters[ 0 ] = new MySqlParameter( "@totalMatchesPlayed", totalMatchesPlayed );
				parameters[ 0 ] = new MySqlParameter( "@matchesWon", matchesWon );
				parameters[ 1 ] = new MySqlParameter( "@matchesLost", matchesLost );
				parameters[ 2 ] = new MySqlParameter( "@matchesSurvived", matchesSurvived );
				parameters[ 3 ] = new MySqlParameter( "@money", money );
				parameters[ 4 ] = new MySqlParameter( "@userId", userId );
				foreach ( var parameter in parameters )
				{
					command.Parameters.Add( parameter );
				}

				command.ExecuteNonQuery();
				wasUpdated = true;
				Console.WriteLine( "User Updated Successfully!" );
			}
			catch ( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasUpdated;
		}
	}
}
