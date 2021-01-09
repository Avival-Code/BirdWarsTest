using BirdWarsTest;
using BirdWarsTest.Network;

namespace Client
{
	class Program
	{
		static void Main( string[] args )
		{
			using( var game = new Game1( new ClientNetworkManager() ) )
			{
				game.Run();
			}
		}
	}
}