using System;
using MySql.Data.MySqlClient;

namespace BirdWarsTest.Database
{
	class Connection
	{
		public Connection()
		{
			server = "server=localhost;";
			user = "user=root;";
			database = "database=BirdWars;";
			port = "port=3306;";
			password = "password=Pollito12Con23Papas4512345;";
		}

		public Connection( string server_In, string user_In, string database_In, string port_In, 
						   string password_In )
		{
			server = "server=" + server_In + ";";
			user = "user=" + user_In + ";";
			database = "database=" + database_In + ";";
			port = "port=" + port_In + ";";
			password = "password=" + password_In + ";";
		}

		public void StartConnection()
		{
			try
			{
				string connectionString = server + user + database + port + password;
				connection = new MySqlConnection( connectionString );
				connection.Open();
				Console.WriteLine( "Connection Successfull!" );
			}
			catch( Exception exception)
			{
				Console.WriteLine( "Connection Failed!" );
				Console.WriteLine( exception.ToString() );
			}
		}

		public void StopConnection()
		{
			try
			{
				connection.Close();
				Console.WriteLine( "Connection closed!" );
			}
			catch( Exception exception )
			{
				Console.WriteLine( exception.ToString() );
			}
		}

		public MySqlConnection connection;
		private string server;
		private string user;
		private string database;
		private string port;
		private string password;
	}
}