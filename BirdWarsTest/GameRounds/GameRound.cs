using Lidgren.Network;
using System.Collections.Generic;

namespace BirdWarsTest.GameRounds
{
	public class GameRound
	{
		public GameRound()
		{
			PlayerConnections = new List< NetConnection >();
			playerUsernames = new List< string >();
			bannedPlayers = new List< string >();
			playerBanPetitions = new List< int >();
			Created = false;
		}

		public void CreateRound( string serverUsername )
		{
			PlayerConnections.Clear();
			playerUsernames.Clear();
			playerBanPetitions.Clear();
			Created = true;
			playerUsernames.Add( serverUsername );
		}

		public void DestroyRound()
		{
			Created = false;
		}

		public void AddPlayer( string username, NetConnection playerConnection )
		{
			if( Created && RoomAvailable() && !IsBanned( username ) )
			{
				playerUsernames.Add( username );
				PlayerConnections.Add( playerConnection );
				playerBanPetitions.Add( 0 );
			}
		}

		public void RemovePlayer( string username )
		{
			for( int i = 0; i < playerUsernames.Count; i++ )
			{
				if( playerUsernames[ i ].Equals( username ) )
				{
					playerUsernames.RemoveAt( i );
					playerBanPetitions.RemoveAt( i );
					PlayerConnections.RemoveAt( i );
					ResetBanPetitions();
				}
			}
		}

		public void RemovePlayer( NetConnection playerConnection, string username )
		{
			PlayerConnections.Remove( playerConnection );
			for( int i = 0; i < playerUsernames.Count; i++ )
			{
				if( playerUsernames[ i ].Equals( username ) )
				{
					playerUsernames.RemoveAt( i );
					playerBanPetitions.RemoveAt( i );
					ResetBanPetitions();
				}
			}
		}

		public string DoBanRequest( string chatMessage, string banMessage )
		{
			if( IsBanRequestInChatMessage( chatMessage, banMessage ) )
			{
				AddBanToPlayerIndex( chatMessage );
			}
			return GetBannedPlayer();
		}

		private string GetBannedPlayer()
		{
			string username = "";
			for( int i = 0; i < playerBanPetitions.Count; i++ )
			{
				if( playerBanPetitions[ i ] > playerUsernames.Count / 2 )
				{
					username = playerUsernames[ i ];
				}
			}
			return username;
		}

		private void AddBanToPlayerIndex( string chatMessage )
		{
			for( int i = 0; i < playerUsernames.Count; i++ )
			{
				if( !string.IsNullOrEmpty( playerUsernames[ i ] ) && chatMessage.Contains( playerUsernames[ i ] ) )
				{
					playerBanPetitions[ i ] += 1;
				}
			}
		}

		private bool IsBanRequestInChatMessage( string chatMessage, string banMessage )
		{
			return ( !string.IsNullOrEmpty( chatMessage ) && chatMessage.Contains( banMessage ) );
		}

		private bool IsBanned( string username )
		{
			bool isBanned = false;
			foreach( var name in bannedPlayers )
			{
				if( name.Equals( username ) )
				{
					isBanned = true;
				}
			}
			return isBanned;
		}

		public void ResetBanPetitions()
		{
			for( int i = 0; i < playerBanPetitions.Count; i++ )
			{
				playerBanPetitions[ i ] = 0;
			}
		}

		public int GetPlayerCount()
		{
			return PlayerConnections.Count;
		}

		public bool RoomAvailable()
		{
			return ( maxPlayers - playerUsernames.Count ) > 0;
		}

		public NetConnection GetPlayerConnection( string username )
		{
			NetConnection temp = null;
			for( int i = 0; i < playerUsernames.Count; i++ )
			{
				if( playerUsernames[ i ].Equals( username ) )
				{
					temp = PlayerConnections[ i ];
				}
			}
			return temp;
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
		private List< string > bannedPlayers;
		private List< int > playerBanPetitions;
		private const int maxPlayers = 8;
	}
}