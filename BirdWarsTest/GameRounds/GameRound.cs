using Lidgren.Network;
using System.Collections.Generic;

namespace BirdWarsTest.GameRounds
{
	public class GameRound
	{
		public GameRound()
		{
			Created = false;
		}

		public void CreateRound( string serverUsername )
		{
			PlayerConnections = new List< NetConnection >();
			playerUsernames = new List< string >();
			Created = true;
			playerUsernames.Add( serverUsername );
		}

		public void DestroyRound()
		{
			PlayerConnections = null;
			playerUsernames = null;
			Created = false;
		}

		public void AddPlayer( string username, NetConnection playerConnection )
		{
			if( Created && RoomAvailable() )
			{
				playerUsernames.Add( username );
				PlayerConnections.Add( playerConnection );
			}
		}

		public void RemovePlayer( NetConnection playerConnection, string username )
		{
			PlayerConnections.Remove( playerConnection );
			playerUsernames.Remove( username );
		}

		public int GetPlayerCount()
		{
			return PlayerConnections.Count;
		}

		public bool RoomAvailable()
		{
			return ( maxPlayers - playerUsernames.Count ) > 0;
		}

		public string [] GetPlayerUsernames()
		{
			string [] temp = new string[ 8 ];
			for( int i = 0; i < 8; i++ )
			{
				if( i < playerUsernames.Count )
				{
					temp[ i ] = playerUsernames[ i ];
				}
				else
				{
					temp[ i ] = "";
				}
			}
			return temp;
		}

		public List< NetConnection > PlayerConnections { get; private set; }

		public bool Created { get; private set; }

		private List< string > playerUsernames;
		private const int maxPlayers = 8;
	}
}