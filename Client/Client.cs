/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
The client application entry point.
*********************************************/
using BirdWarsTest;
using BirdWarsTest.Network;

namespace Client
{
	/// <summary>
	/// The client application entry point.
	/// </summary>
	public static class Client
	{
		/// <summary>
		/// The method that starts the Client application.
		/// </summary>
		public static void Main()
		{
			using( var game = new Game1( new ClientNetworkManager() ) )
			{
				game.Run();
			}
		}
	}
}