/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles the creation, update and display of player
game objects.
*********************************************/
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.HealthComponents;
using BirdWarsTest.AttackComponents;
using BirdWarsTest.Utilities;
using BirdWarsTest.Network;
using BirdWarsTest.Network.Messages;
using BirdWarsTest.States;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	/// <summary>
	/// Handles the creation, update and display of player
	/// game objects.
	/// </summary>
	public class PlayerManager
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PlayerManager()
		{
			Players = new List< GameObject >();
			positionGenerator = new PositionGenerator();
			CreatedPlayers = false;
			GrenadeAmount = 0;
			sentLocalPlayerIsDeadMessage = false;
		}

		/// <summary>
		/// Creates the players that are in the game round.
		/// </summary>
		/// <param name="content">Game content manager.</param>
		/// <param name="handler">game statehandler.</param>
		/// <param name="usernames">The list of player usernames.</param>
		/// <param name="localUsername">The local player username.</param>
		public void CreatePlayers( Microsoft.Xna.Framework.Content.ContentManager content, StateHandler handler,
								   string [] usernames, string localUsername )
		{
			int playerIdentifier = ( int )Identifiers.Player1;
			for ( int i = 0; i < 8; i++ )
			{
				if( !string.IsNullOrEmpty( usernames[ i ] ) )
				{
					if( usernames[ i ].Equals( localUsername ) )
					{
						LocalPlayerIndex = i;
						Players.Add( new GameObject( new PlayerGraphicsComponent( content ), new LocalPlayerInputComponent( handler ),
													 new HealthComponent(), new PlayerAttackComponent(), ( Identifiers )playerIdentifier,
													 positionGenerator.GetAPosition() ) );
						playerIdentifier++;
					}
					else
					{
						Players.Add( new GameObject( new PlayerGraphicsComponent( content ), new ExternalPlayerInputComponent(),
													 new HealthComponent(), new PlayerAttackComponent(), ( Identifiers )playerIdentifier,
													 positionGenerator.GetAPosition() ) );
						playerIdentifier++;
					}
				}
			}
			CreatedPlayers = true;
		}

		/// <summary>
		/// Creates the players written in the incoming game message.
		/// </summary>
		/// <param name="content">Game content manager.</param>
		/// <param name="handler">Game state handler.</param>
		/// <param name="incomingMessage">THe incoming message.</param>
		/// <param name="localUsername">The local player username.</param>
		public void CreatePlayers( Microsoft.Xna.Framework.Content.ContentManager content, StateHandler handler,
								   NetIncomingMessage incomingMessage, string localUsername )
		{
			int playerIdentifier = ( int )Identifiers.Player1;
			for( int i = 0; i < 8; i++ )
			{
				var username = incomingMessage.ReadString();
				if( !string.IsNullOrEmpty( username ) )
				{
					if( username.Equals( localUsername ) )
					{
						LocalPlayerIndex = i;
						Players.Add( new GameObject( new PlayerGraphicsComponent( content ), new LocalPlayerInputComponent( handler ),
													 new HealthComponent(), new PlayerAttackComponent(), ( Identifiers )playerIdentifier,
													 new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
						playerIdentifier++;
					}
					else
					{
						Players.Add( new GameObject( new PlayerGraphicsComponent( content ), new ExternalPlayerInputComponent(),
													 new HealthComponent(), new PlayerAttackComponent(), ( Identifiers )playerIdentifier,
													 new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
						playerIdentifier++;
					}
				}
			}
			CreatedPlayers = true;
		}

		/// <summary>
		/// Updates remote player positions using incoming
		/// player state change messages.
		/// </summary>
		/// <param name="incomingMessage">The incoming PlayerStateChangeMessage.</param>
		/// <param name="stateChangeMessage">The incoming PlayerStateChangeMessage.</param>
		public void HandlePlayerStateChangeMessage( NetIncomingMessage incomingMessage, 
													PlayerStateChangeMessage stateChangeMessage )
		{
			if( GetLocalPlayer().Identifier != stateChangeMessage.Id )
			{
				var timeDelay = ( float )( NetTime.Now - incomingMessage.SenderConnection.GetLocalTime( stateChangeMessage.MessageTime ) );

				GameObject player = GetPlayer( stateChangeMessage.Id );

				if( player.Input.GetLastUpdateTime() < stateChangeMessage.MessageTime )
				{
					player.Position = stateChangeMessage.Position += stateChangeMessage.Velocity * timeDelay;
					player.Input.SetVelocity( stateChangeMessage.Velocity );
					player.Input.SetLastUpdateTime( stateChangeMessage.MessageTime );
				}
			}
		}

		public void HandleAdjustedPlayerStateChangeMessage( NetIncomingMessage incomingMessage, 
													AdjustedPlayerStateChangeMessage adjustedMessage )
		{
			if( GetLocalPlayer().Identifier != adjustedMessage.Id )
			{
				var timeDelay = ( float )( NetTime.Now - incomingMessage.SenderConnection.GetLocalTime( adjustedMessage.MessageTime ) );
				timeDelay += adjustedMessage.ClientDelayTime;

				GameObject player = GetPlayer( adjustedMessage.Id );

				if( player.Input.GetLastUpdateTime() < adjustedMessage.MessageTime )
				{
					player.Position = adjustedMessage.Position += adjustedMessage.Velocity * timeDelay;
					player.Input.SetVelocity( adjustedMessage.Velocity );
					player.Input.SetLastUpdateTime( adjustedMessage.MessageTime );
				}
			}
		}

		/// <summary>
		/// Does player attack.
		/// </summary>
		/// <param name="attackMessage">The incoming PlayerAttackMessage.</param>
		public void HandlePlayerAttackMessage( PlayerAttackMessage attackMessage )
		{
			if( GetLocalPlayer().Identifier != attackMessage.PlayerIndex )
			{
				GameObject player = GetPlayer( attackMessage.PlayerIndex );
				player.Attack.DoAttack();
			}
		}

		/// <summary>
		/// Kills the player in the message.
		/// </summary>
		/// <param name="playerId">The incoming PlayerDiedMessage.</param>
		public void HandlePlayerDiedMessage( Identifiers playerId )
		{
			if( GetLocalPlayer().Identifier != playerId )
			{
				GetPlayer( playerId ).Health.TakeFullDamage();
			}
		}

		/// <summary>
		/// Returns the gameObject that corresponds to
		/// the current user.
		/// </summary>
		/// <returns>The local player gameObject.</returns>
		public GameObject GetLocalPlayer()
		{
			return Players[ LocalPlayerIndex ];
		}

		/// <summary>
		/// Get a player from the list of players.
		/// </summary>
		/// <param name="id">The id of the player object.</param>
		/// <returns>The player gameObject.</returns>
		public GameObject GetPlayer( Identifiers id )
		{
			foreach( var player in Players )
			{
				if( player.Identifier == id )
				{
					return player;
				}
			}
			return null;
		}

		/// <summary>
		/// Updates the gameobjects, handles player attacks and checks
		/// if the local player is dead.
		/// </summary>
		/// <param name="gameState">The current game state.</param>
		/// <param name="gameTime">Elapsed game time.</param>
		/// <param name="state">The keyboard state.</param>
		/// <param name="mapBounds">The game world area rectangle.</param>
		/// <param name="networkManager">Game network manager.</param>
		public void Update( GameState gameState, GameTime gameTime, KeyboardState state, Rectangle mapBounds,
							INetworkManager networkManager )
		{
			HandlePlayerAttacks();
			foreach( var player in Players )
			{
				player.Update( state, gameState, gameTime );
				ImposeMapBoundaryLimits( player, mapBounds );
			}
			CheckIfLocalPlayerDied( networkManager );
		}

		private void ImposeMapBoundaryLimits( GameObject player, Rectangle mapBounds )
		{
			if( player.Position.X < mapBounds.Left )
			{
				player.Position = new Vector2( mapBounds.Left, player.Position.Y );
			}
			if( player.Position.Y < mapBounds.Top )
			{
				player.Position = new Vector2( player.Position.X, mapBounds.Top );
			}
			if( player.GetRectangle().Right > mapBounds.Right )
			{
				player.Position = new Vector2( mapBounds.Right - player.Graphics.GetTextureSize().X, player.Position.Y );
			}
			if( player.GetRectangle().Bottom > mapBounds.Bottom )
			{
				player.Position = new Vector2( player.Position.X, mapBounds.Bottom - player.Graphics.GetTextureSize().Y );
			}
		}

		private void HandlePlayerAttacks()
		{
			for( int i = 0; i < Players.Count; i++ )
			{
				if( i != LocalPlayerIndex )
				{
					if( Players[ i ].Attack.IsAttacking &&
						Players[ i ].Attack.GetAttackRectangle( Players[ i ] ).Intersects( 
																GetLocalPlayer().Health.GetPlayerHitBox( GetLocalPlayer() ) ) )
					{
						GetLocalPlayer().Health.TakeDamage( Players[ i ].Attack.Damage );
					}
				}
			}
		}

		/// <summary>
		/// Draws the objects that are within the camera render
		/// bounds to the screen.
		/// </summary>
		/// <param name="batch">Game spritebatch.</param>
		/// <param name="cameraRenderBounds">The camera render area rectangle.</param>
		/// <param name="cameraBounds">The camera area rectangle.</param>
		public void Render( ref SpriteBatch batch, Rectangle cameraRenderBounds, Rectangle cameraBounds )
		{
			foreach( var player in Players )
			{
				if( !player.Health.IsDead() && cameraRenderBounds.Intersects( player.GetRectangle() ) )
				{
					player.Render( ref batch, cameraBounds );
				}
			}
		}

		/// <summary>
		/// Removes all gameobjects and resets manager.
		/// </summary>
		public void ClearAllPlayers()
		{
			Players.Clear();
			CreatedPlayers = false;
			LocalPlayerIndex = 0;
			GrenadeAmount = 0;
		}

		/// <summary>
		/// Checks if the local player is alive and all other
		/// players are not.
		/// </summary>
		/// <returns>bool indicating if player won or not.</returns>
		public bool DidLocalPlayerWin()
		{
			bool otherPlayersAreDead = false;
			for( int i = 0; i < Players.Count; i++ )
			{
				if( i != LocalPlayerIndex )
				{
					otherPlayersAreDead = Players[ i ].Health.IsDead();
				}
			}
			return ( !GetLocalPlayer().Health.IsDead() && otherPlayersAreDead );
		}

		private void CheckIfLocalPlayerDied( INetworkManager networkManager )
		{
			if( !sentLocalPlayerIsDeadMessage && GetLocalPlayer().Health.IsDead() )
			{
				sentLocalPlayerIsDeadMessage = true;
				networkManager.SendPlayerDiedMessage( GetLocalPlayer().Identifier );
			}
		}

		/// <summary>
		/// Returns the number of players whose health > 0.
		/// </summary>
		/// <returns></returns>
		public int GetNumberOfPlayersStillAlive()
		{
			int playersStillAlive = 0;
			foreach( var player in Players )
			{
				if( !player.Health.IsDead() )
				{
					playersStillAlive += 1;
				}
			}
			return playersStillAlive;
		}

		/// <value>The list of player objects.</value>
		public List< GameObject > Players { get; private set; }

		private PositionGenerator positionGenerator;

		/// <value>bool indicating if players have been created.</value>
		public bool CreatedPlayers { get; private set; }

		/// <value>The index of the object that corresponds to the current user.</value>
		public int LocalPlayerIndex { get; private set; }

		/// <value>The number of grenades in inventory.</value>
		public int GrenadeAmount { get; set; }
		private bool sentLocalPlayerIsDeadMessage;
	}
}