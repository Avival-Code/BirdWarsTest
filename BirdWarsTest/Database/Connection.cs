/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Class that handles connecting to the MySqlServer.
*********************************************/
using System;
using MySql.Data.MySqlClient;

namespace BirdWarsTest.Database
{
	/// <summary>
	/// Class that handles connecting to the MySqlServer.
	/// </summary>
	public class Connection
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Connection()
		{
			server = "server=localhost;";
			user = "user=root;";
			database = "database=BirdWars;";
			port = "port=3306;";
			password = "password=Pollito12Con23Papas4512345;";
		}

		/// <summary>
		/// Constructor used to specify a specific MySqlserver connection.
		/// </summary>
		/// <param name="server_In">Ip address of the server.</param>
		/// <param name="user_In">Server username.</param>
		/// <param name="database_In">Name of the desired database.</param>
		/// <param name="port_In">Port input parameter.</param>
		/// <param name="password_In">Database password input parameter.</param>
		public Connection( string server_In, string user_In, string database_In, string port_In, 
						   string password_In )
		{
			server = "server=" + server_In + ";";
			user = "user=" + user_In + ";";
			database = "database=" + database_In + ";";
			port = "port=" + port_In + ";";
			password = "password=" + password_In + ";";
		}

		/// <summary>
		/// Starts a connection to the MySqlserver.
		/// </summary>
		public void StartConnection()
		{
			try
			{
				string connectionString = server + user + database + port + password;
				DatabaseConnection = new MySqlConnection( connectionString );
				DatabaseConnection.Open();
			}
			catch( MySqlException exception)
			{
				Console.WriteLine( exception.Message );
			}
		}

		/// <summary>
		/// Stops the current MySqlserver connection.
		/// </summary>
		public void StopConnection()
		{
			try
			{
				DatabaseConnection.Close();
			}
			catch( MySqlException exception )
			{
				Console.WriteLine( exception.Message );
			}
		}

		/// <Value>Variable that stores connection.</Value>
		public MySqlConnection DatabaseConnection { get; private set; }
		private string server;
		private string user;
		private string database;
		private string port;
		private string password;
	}
}