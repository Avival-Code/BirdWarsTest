using BirdWarsTest.GraphicComponents;
using BirdWarsTest.Utilities;
using Lidgren.Network;
using Microsoft.Xna.Framework;
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

		public void CreatePlayers( Microsoft.Xna.Framework.Content.ContentManager content, 
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
					}
					Players.Add( new GameObject( new PlayerTestGraphicsComponent( content ), null, ( Identifiers )playerIdentifier,
												 positionGenerator.GetAPosition() ) );
					playerIdentifier++;
				}
			}
			CreatedPlayers = true;
		}

		public void CreatePlayers( Microsoft.Xna.Framework.Content.ContentManager content,
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
					}
					Players.Add( new GameObject( new PlayerTestGraphicsComponent( content ), null, ( Identifiers )playerIdentifier,
												 new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
					playerIdentifier++;
				}
			}
			CreatedPlayers = true;
		}

		public GameObject GetLocalPlayer()
		{
			return Players[ localPlayerIndex ];
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