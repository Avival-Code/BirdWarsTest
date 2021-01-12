/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
User Data Access Object.
*********************************************/
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BirdWarsTest.Database
{
	/// <summary>
	/// User Data Access Object.
	/// </summary>
	public class UserDAO : IUserDAO
	{
		/// <summary>
		/// Creates a new instance of user in the database.
		/// </summary>
		/// <param name="user">An existing user instance.</param>
		/// <returns>bool indicating method success or failure.</returns>
		public bool Create( User user )
		{
			bool wasCreated = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "INSERT INTO Users( name, lastNames, username, email, password ) " +
										  "VALUES ( @name, @lastNames, @username, @email, @password );";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.DatabaseConnection );
				MySqlParameter[] parameters = new MySqlParameter[ 5 ];
				parameters[ 0 ] = new MySqlParameter( "@name", user.Names );
				parameters[ 1 ] = new MySqlParameter( "@lastNames", user.LastName );
				parameters[ 2 ] = new MySqlParameter( "@username", user.Username );
				parameters[ 3 ] = new MySqlParameter( "@email", user.Email );
				parameters[ 4 ] = new MySqlParameter( "@password", user.Password );
				foreach( var parameter in parameters )
				{
					command.Parameters.Add( parameter );
				}

				command.ExecuteNonQuery();
				wasCreated = true;
			}
			catch( MySqlException exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasCreated;
		}

		/// <summary>
		/// Deletes a user from the database.
		/// </summary>
		/// <param name="userId">The Id of the desired user.</param>
		/// <returns>bool indicating method success or failure.</returns>
		public bool Delete( int userId )
		{
			bool wasDeleted = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "DELETE FROM Users WHERE userId = @userId";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.DatabaseConnection );
				MySqlParameter parameter = new MySqlParameter( "@userId", userId );
				command.Parameters.Add( parameter );

				command.ExecuteNonQuery();
				wasDeleted = true;
				Console.WriteLine( "User deleted succesfully!" );
			}
			catch( MySqlException exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasDeleted;
		}

		/// <summary>
		/// Retrieves a user from the database using the email
		/// entered.
		/// </summary>
		/// <param name="email">The desired user email.</param>
		/// <returns>bool indicating method success or failure.</returns>
		public User Read( string email )
		{
			User temp = null;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string MySqlCommandText = "SELECT * FROM Users WHERE email = @email";
				MySqlCommand command = new MySqlCommand( MySqlCommandText, connection.DatabaseConnection );
				MySqlParameter parameter = new MySqlParameter( "@email", email );
				command.Parameters.Add( parameter );
				MySqlDataReader reader = command.ExecuteReader();

				if ( reader.HasRows && reader.Read() )
				{
					temp = new User( reader.GetInt32( 0 ), reader.GetString( 1 ), reader.GetString( 2 ), reader.GetString( 3 ),
									 reader.GetString( 4 ), reader.GetString( 5 ) );
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
		/// Retrieves a user from the database whose email and
		/// password match the entered values.
		/// </summary>
		/// <param name="email">The desired user email.</param>
		/// <param name="password">The desired user password.</param>
		/// <returns>bool indicating method success or failure.</returns>
		public User Read( string email, string password )
		{
			User temp = null;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string MySqlCommandText = "SELECT * FROM Users WHERE email = @email AND password = @password";
				MySqlCommand command = new MySqlCommand( MySqlCommandText, connection.DatabaseConnection );
				MySqlParameter [] parameters = new MySqlParameter[ 2 ];
				parameters[ 0 ] = new MySqlParameter( "@email", email );
				parameters[ 1 ] = new MySqlParameter( "@password", password );
				command.Parameters.Add( parameters[ 0 ] );
				command.Parameters.Add( parameters[ 1 ] );
				MySqlDataReader reader = command.ExecuteReader();

				if( reader.HasRows && reader.Read() )
				{
					temp = new User( reader.GetInt32( 0 ), reader.GetString( 1 ), reader.GetString( 2 ), reader.GetString( 3 ),
									 reader.GetString( 4 ), reader.GetString( 5 ) );
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
		/// Retrieves a list of all the users in the database.
		/// </summary>
		/// <returns>A list of users.</returns>
		public List< User > ReadAll()
		{
			List< User > users = new List< User >();
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "SELECT * FROM Users";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.DatabaseConnection );
				MySqlDataReader reader = command.ExecuteReader();

				while( reader.HasRows && reader.Read() )
				{
					users.Add( new User( reader.GetInt32( 0 ), reader.GetString( 1 ), reader.GetString( 2 ), reader.GetString( 3 ),
						                 reader.GetString( 4 ), reader.GetString( 5 ) ) );
					reader.NextResult();
				}
				reader.Close();
			}
			catch( MySqlException exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return users;
		}

		/// <summary>
		/// Updates a user in the database whose id matches the entered
		/// userId value.
		/// </summary>
		/// <param name="userId">The desired user's Id.</param>
		/// <param name="names">A string value.</param>
		/// <param name="lastName">A string value.</param>
		/// <param name="username">A string value.</param>
		/// <param name="email">A string value.</param>
		/// <param name="password">A string value.</param>
		/// <returns>bool indicating method success or failure.</returns>
		public bool Update( int userId, string names, string lastName, string username, 
							string email, string password )
		{
			bool wasUpdated = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "UPDATE Users SET name = @names, lastNames = @lastNames, username = @username, " +
										  "email = @email, password = @password WHERE userId = @userId";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.DatabaseConnection );
				MySqlParameter [] parameters = new MySqlParameter[ 6 ];
				parameters[ 0 ] = new MySqlParameter( "@names", names );
				parameters[ 1 ] = new MySqlParameter( "@lastNames", lastName );
				parameters[ 2 ] = new MySqlParameter( "@username", username );
				parameters[ 3 ] = new MySqlParameter( "@email", email );
				parameters[ 4 ] = new MySqlParameter( "@password", password );
				parameters[ 5 ] = new MySqlParameter( "@userId", userId );
				foreach( var parameter in parameters )
				{
					command.Parameters.Add( parameter );
				}

				command.ExecuteNonQuery();
				wasUpdated = true;
			}
			catch( MySqlException exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasUpdated;
		}
	}
}