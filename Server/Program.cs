using BirdWarsTest;
using BirdWarsTest.Network;

namespace Server
{
	class Program
	{
		static void Main( string[] args )
		{
			using( var game = new Game1( new ServerNetworkManager() ) )
			{
				game.Run();
			}
		}
	}
}
