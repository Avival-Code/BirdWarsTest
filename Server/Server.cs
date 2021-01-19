/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
The server application entry point.
*********************************************/
using BirdWarsTest;
using BirdWarsTest.Network;

namespace Server
{
	/// <summary>
	/// The server application entry point.
	/// </summary>
	public static class Server
	{
		/// <summary>
		/// The method that starts the Server application.
		/// </summary>
		public static void Main()
		{
			using( var game = new Game1( new ServerNetworkManager() ) )
			{
				game.Run();
			}
		}
	}
}