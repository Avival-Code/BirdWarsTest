/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Stores the players connected to the server, their usernames, 
list of banned players.
*********************************************/
using Lidgren.Network;
using System.Collections.Generic;

namespace BirdWarsTest.GameRounds
{
	/// <summary>
	/// Stores the players connected to the server, their usernames, 
	/// list of banned players.
	/// </summary>
	public class GameRound
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public GameRound()
		{
			PlayerConnections = new List< NetConnection >();
			playerUsernames = new List< string >();
			bannedPlayers = new List< string >();
			playerBanPetitions = new List< int >();
			Created = false;
			GameRoundStarted = false;
		}

		/// <summary>
		/// Starts a gameRound. Clears all objects and adds the
		/// server username the playersUsernames list.
		/// </summary>
		/// <param name="serverUsername">Server username</param>
		public void CreateRound( string serverUsername )
		{
			PlayerConnections.Clear();
			playerUsernames.Clear();
			playerBanPetitions.Clear();
			Created = true;
			playerUsernames.Add( serverUsername );
			playerBanPetitions.Add( 0 );
		}

		/// <summary>
		/// Destroys round.
		/// </summary>
		public void DestroyRound()
		{
			Created = false;
			GameRoundStarted = false;
		}

		/// <summary>
		/// Adds a player to the game round.
		/// </summary>
		/// <param name="username">Player username</param>
		/// <param name="playerConnection">Player connection</param>
		public void AddPlayer( string username, NetConnection playerConnection )
		{
			if( Created && RoomAvailable() && !IsBanned( username ) )
			{
				playerUsernames.Add( username );
				PlayerConnections.Add( playerConnection );
				playerBanPetitions.Add( 0 );
			}
		}

		/// <summary>
		/// Removes a player from the game round.
		/// </summary>
		/// <param name="username">Player username</param>
		public void RemovePlayer( string username )
		{
			for( int i = 0; i < playerUsernames.Count; i++ )
			{
				if( playerUsernames[ i ].Equals( username ) )
				{
					playerUsernames.RemoveAt( i );
					playerBanPetitions.RemoveAt( i );
					PlayerConnections.RemoveAt( i - 1 );
					ResetBanPetitions();
				}
			}
		}

		/// <summary>
		/// Removes a player from the gameRound.
		/// </summary>
		/// <param name="playerConnection">Player connection</param>
		/// <param name="username">Player username</param>
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

		/// <summary>
		/// Processes a chat message, checks if ban
		/// request was made and adds ban to list of bans.
		/// </summary>
		/// <param name="chatMessage">The chat message to parse</param>
		/// <param name="banMessage">The ban message.</param>
		/// <returns>Returns the name of the player to ban.</returns>
		public string DoBanRequest( string chatMessage, string banMessage )
		{
			if( IsBanRequestInChatMessage( chatMessage, banMessage ) )
			{
				AddBanToPlayerIndex( chatMessage );
			}
			return GetBannedPlayer();
		}

		/// <summary>
		/// Adds username to ban list.
		/// </summary>
		/// <param name="username">The banned player username</param>
		public void Ban( string username )
		{
			bannedPlayers.Add( username );
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

		/// <summary>
		/// Checks if player username is in banned list.
		/// </summary>
		/// <param name="username">The player username</param>
		/// <returns>bool</returns>
		public bool IsBanned( string username )
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

		/// <summary>
		/// Resets the number of bans in the gameRound.
		/// </summary>
		public void ResetBanPetitions()
		{
			for( int i = 0; i < playerBanPetitions.Count; i++ )
			{
				playerBanPetitions[ i ] = 0;
			}
		}

		/// <summary>
		/// Checks if there is room available in the game round.
		/// </summary>
		/// <returns></returns>
		public bool RoomAvailable()
		{
			return ( MaxPlayers - playerUsernames.Count ) > 0;
		}

		/// <summary>
		/// Gets the connection of the player that matches the username.
		/// </summary>
		/// <param name="username">Desired player username.</param>
		/// <returns>The connection of the player client.</returns>
		public NetConnection GetPlayerConnection( string username )
		{
			NetConnection temp = null;
			for( int i = 0; i < playerUsernames.Count; i++ )
			{
				if( playerUsernames[ i ].Equals( username ) )
				{
					temp = PlayerConnections[ i - 1 ];
				}
			}
			return temp;
		}

		/// <summary>
		/// Returns a list of player usernames.
		/// </summary>
		/// <returns>Returns a list of player usernames.</returns>
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

		/// <value>The list of player conenctions.</value>
		public List< NetConnection > PlayerConnections { get; private set; }
		private List< string > playerUsernames;
		private List< string > bannedPlayers;
		private List< int > playerBanPetitions;

		/// <value>bool indicating if the round has been created.</value>
		public bool Created { get; private set; }

		/// <value>bool indicating if the game round has started.</value>
		public bool GameRoundStarted { get; set; }
		private const int MaxPlayers = 8;
	}
}