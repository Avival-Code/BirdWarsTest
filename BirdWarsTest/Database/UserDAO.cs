using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	public class UserDAO : IUserDAO
	{
		public bool Create( User user )
		{
			bool wasCreated = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "INSERT INTO Users( name, lastNames, username, email, password ) " +
										  "VALUES ( @name, @lastNames, @username, @email, @password );";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
				MySqlParameter[] parameters = new MySqlParameter[ 5 ];
				parameters[ 0 ] = new MySqlParameter( "@name", user.names );
				parameters[ 1 ] = new MySqlParameter( "@lastNames", user.lastName );
				parameters[ 2 ] = new MySqlParameter( "@username", user.username );
				parameters[ 3 ] = new MySqlParameter( "@email", user.email );
				parameters[ 4 ] = new MySqlParameter( "@password", user.password );
				foreach( var parameter in parameters )
				{
					command.Parameters.Add( parameter );
				}

				command.ExecuteNonQuery();
				wasCreated = true;
				Console.WriteLine( "User created succesfully!" );
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
				string mySqlCommandText = "DELETE FROM Users WHERE userId = @userId";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
				MySqlParameter parameter = new MySqlParameter( "@userId", userId );
				command.Parameters.Add( parameter );

				command.ExecuteNonQuery();
				wasDeleted = true;
				Console.WriteLine( "User deleted succesfully!" );
			}
			catch( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasDeleted;
		}

		public User Read( string email, string password )
		{
			User temp = null;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string MySqlCommandText = "SELECT * FROM Users WHERE email = @email AND password = @password";
				MySqlCommand command = new MySqlCommand( MySqlCommandText, connection.connection );
				MySqlParameter [] parameters = new MySqlParameter[ 2 ];
				parameters[ 0 ] = new MySqlParameter( "@email", email );
				parameters[ 1 ] = new MySqlParameter( "@password", password );
				command.Parameters.Add( parameters[ 0 ] );
				command.Parameters.Add( parameters[ 1 ] );
				MySqlDataReader reader = command.ExecuteReader();

				if( reader.HasRows  && reader.Read() )
				{
					temp = new User( reader.GetInt32( 0 ), reader.GetString( 1 ), reader.GetString( 2 ), reader.GetString( 3 ),
									 reader.GetString( 4 ), reader.GetString( 5 ) );
				}
				else
				{
					Console.WriteLine( "No users found." );
				}
				reader.Close();
			}
			catch( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return temp;
		}

		public List< User > ReadAll()
		{
			List< User > users = new List< User >();
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "SELECT * FROM Users";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
				MySqlDataReader reader = command.ExecuteReader();

				while( reader.HasRows && reader.Read() )
				{
					users.Add( new User( reader.GetInt32( 0 ), reader.GetString( 1 ), reader.GetString( 2 ), reader.GetString( 3 ),
						                 reader.GetString( 4 ), reader.GetString( 5 ) ) );
					reader.NextResult();
				}
				reader.Close();
			}
			catch( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return users;
		}

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
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
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
				Console.WriteLine( "User Updated Successfully!" );
			}
			catch( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasUpdated;
		}
	}
}