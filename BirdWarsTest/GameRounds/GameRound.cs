using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.GameRounds
{
	class GameRound
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

		public void AddPlayer( string username, NetConnection playerConnection )
		{
			if( Created && RoomAvailable() )
			{
				playerUsernames.Add( username );
				PlayerConnections.Add( playerConnection );
			}
		}

		public void RemovePlayer( NetConnection playerConnection )
		{
			PlayerConnections.Remove( playerConnection );
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

		public bool Created { get; private set; }

		public List<NetConnection> PlayerConnections { get; private set; }

		private List< string > playerUsernames;
		private const int maxPlayers = 8;
	}
}