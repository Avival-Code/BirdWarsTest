using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.HealthComponents;
using BirdWarsTest.AttackComponents;
using BirdWarsTest.Utilities;
using BirdWarsTest.Network.Messages;
using BirdWarsTest.States;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	class PlayerManager
	{
		public PlayerManager()
		{
			Players = new List< GameObject >();
			positionGenerator = new PositionGenerator();
			CreatedPlayers = false;
		}

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
						Players.Add( new GameObject( new PlayerTestGraphicsComponent( content ), new LocalPlayerInputComponent( handler ),
													 new HealthComponent(), new LocalPlayerAttackComponent(), ( Identifiers )playerIdentifier,
													 positionGenerator.GetAPosition() ) );
						playerIdentifier++;
					}
					else
					{
						Players.Add( new GameObject( new PlayerTestGraphicsComponent( content ), new ExternalPlayerInputComponent(),
													 new HealthComponent(), new ExternalPlayerAttackComponent(), ( Identifiers )playerIdentifier,
													 positionGenerator.GetAPosition() ) );
						playerIdentifier++;
					}
				}
			}
			CreatedPlayers = true;
		}

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
						Players.Add( new GameObject( new PlayerTestGraphicsComponent( content ), new LocalPlayerInputComponent( handler ),
													 new HealthComponent(), new LocalPlayerAttackComponent(), ( Identifiers )playerIdentifier,
													 new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
						playerIdentifier++;
					}
					else
					{
						Players.Add( new GameObject( new PlayerTestGraphicsComponent( content ), new ExternalPlayerInputComponent(),
													 new HealthComponent(), new ExternalPlayerAttackComponent(), ( Identifiers )playerIdentifier,
													 new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
						playerIdentifier++;
					}
				}
			}
			CreatedPlayers = true;
		}

		public void HandlePlayerStateChangeMessage( NetIncomingMessage incomingMessage )
		{
			var message = new PlayerStateChangeMessage( incomingMessage );
			var timeDelay = ( float )( NetTime.Now - incomingMessage.SenderConnection.GetLocalTime( message.MessageTime ) );

			GameObject player = GetPlayer( message.Id );

			if( player.Input.GetLastUpdateTime() < message.MessageTime )
			{
				player.Position = message.Position += message.Velocity * timeDelay;
				player.Input.SetVelocity( message.Velocity );
				player.Input.SetLastUpdateTime( message.MessageTime );
			}
		}

		public void HandlePlayerAttackMessage( NetIncomingMessage incomingMessage )
		{
			var message = new PlayerAttackMessage( incomingMessage );
			GameObject player = GetPlayer( message.PlayerIndex );
			player.Attack.DoAttack();
		}

		public GameObject GetLocalPlayer()
		{
			return Players[ LocalPlayerIndex ];
		}

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

		public void Update( GameState gameState, KeyboardState state, GameTime gameTime, Rectangle mapBounds )
		{
			HandlePlayerAttacks();
			foreach( var player in Players )
			{
				player.Update( state, gameState, gameTime );
				ImposeMapBoundaryLimits( player, mapBounds );
			}
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
						Players[ i ].Attack.GetAttackRectangle( Players[ i ] ).Intersects( GetLocalPlayer().GetRectangle() ) )
					{
						GetLocalPlayer().Health.TakeDamage( Players[ i ].Attack.Damage );
					}
				}
			}
		}

		public void Render( ref SpriteBatch batch, Rectangle cameraRenderBounds, Rectangle cameraBounds )
		{
			foreach( var player in Players )
			{
				if( cameraRenderBounds.Intersects( player.GetRectangle() ) )
				{
					player.Render( ref batch, cameraBounds );
				}
			}
		}

		public List< GameObject > Players { get; private set; }
		private PositionGenerator positionGenerator;
		public bool CreatedPlayers { get; private set; }
		public int LocalPlayerIndex { get; private set; }
	}
}