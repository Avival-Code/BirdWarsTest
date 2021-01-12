/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Class that handles connecting to the MySqlServer.
*********************************************/
using System;
using System.IO;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace BirdWarsTest.Database
{
	/// <summary>
	/// Class that handles connecting to the MySqlServer.
	/// </summary>
	public class Connection
	{
		/// <summary>
		/// Loads required login variables from file.
		/// </summary>
		public Connection()
		{
			LoadLoginInformation();
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

		private void LoadLoginInformation()
		{
			string fileName;
			string filePath;
			string[] tempStrings;

			try
			{
				fileName = @"Connection.txt";
				filePath = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location), fileName );
				tempStrings = File.ReadAllLines( filePath );

				if( !string.IsNullOrEmpty( tempStrings[ 0 ] ) )
				{
					server = tempStrings[ 0 ];
					user = tempStrings[ 1 ];
					database = tempStrings[ 2 ];
					port = tempStrings[ 3 ];
					password = tempStrings[ 4 ];
				}
			}
			catch( FileNotFoundException e )
			{
				Console.WriteLine(e.Message);
			}
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