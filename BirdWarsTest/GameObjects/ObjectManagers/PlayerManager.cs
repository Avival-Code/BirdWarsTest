using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.HealthComponents;
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
						localPlayerIndex = i;
						Players.Add( new GameObject( new PlayerTestGraphicsComponent( content ), new LocalPlayerInputComponent( handler ),
													 new HealthComponent(), ( Identifiers )playerIdentifier,
													 positionGenerator.GetAPosition() ) );
						playerIdentifier++;
					}
					else
					{
						Players.Add( new GameObject( new PlayerTestGraphicsComponent( content ), new ExternalPlayerInputComponent(),
													 new HealthComponent(), ( Identifiers )playerIdentifier,
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
						localPlayerIndex = i;
						Players.Add( new GameObject( new PlayerTestGraphicsComponent( content ), new LocalPlayerInputComponent( handler ),
													 new HealthComponent(), ( Identifiers )playerIdentifier,
													 new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
						playerIdentifier++;
					}
					else
					{
						Players.Add( new GameObject( new PlayerTestGraphicsComponent( content ), new ExternalPlayerInputComponent(),
													 new HealthComponent(), ( Identifiers )playerIdentifier,
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

		public GameObject GetLocalPlayer()
		{
			return Players[ localPlayerIndex ];
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

		public void Update( GameState gameState, KeyboardState state, GameTime gameTime )
		{
			foreach( var player in Players )
			{
				player.Update( state, gameState, gameTime );
			}
		}

		public void Render( ref SpriteBatch batch, Rectangle cameraBounds )
		{
			foreach( var objects in Players )
			{
				objects.Render( ref batch, cameraBounds );
			}
		}

		public List< GameObject > Players { get; private set; }

		public bool CreatedPlayers { get; private set; }

		private int localPlayerIndex;
		private PositionGenerator positionGenerator;
	}
}