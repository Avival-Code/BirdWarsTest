using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	class UserDAO : IUserDAO
	{
		public bool Create( User user )
		{
			bool wasCreated = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				MySqlCommand command = new MySqlCommand();
				command.Connection = connection.connection;
				command.CommandText = " INSERT INTO Usuarios( name, lastNames, email, password ) VALUES ('"
									+ user.names + "', '" + user.lastName + "', '" + user.email + "', '"
									+ user.password + "' );";
				command.ExecuteNonQuery();
				wasCreated = true;
			}
			catch( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			return wasCreated;
		}

		public bool Delete( int userId )
		{
			throw new NotImplementedException();
		}

		public Inventory Read( int userId )
		{
			throw new NotImplementedException();
		}

		public List<Inventory> ReadAll()
		{
			throw new NotImplementedException();
		}

		public bool Update( int userId, string names, string lastName, string email, string password )
		{
			throw new NotImplementedException();
		}
	}
}
