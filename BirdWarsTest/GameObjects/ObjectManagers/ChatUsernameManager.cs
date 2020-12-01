using BirdWarsTest.GraphicComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	class ChatUsernameManager
	{
		public ChatUsernameManager()
		{
			gameObjects = new List< GameObject >();
		}

		public void AddObjects( Microsoft.Xna.Framework.Content.ContentManager content )
		{
			for( int i = 0; i < 8; i++ )
			{
				gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Decorations/UsernameBorder200x50", "" ),
												 null, Identifiers.Decoration,
												 new Vector2( 25.0f, 50.0f + ( 55.0f * i ) ) ) );
			}
		}

		public void HandleRoundStateChangeMessage( NetIncomingMessage incomingMessage )
		{
			for( int i = 0; i < 8; i++ )
			{
				( ( ButtonGraphicsComponent )gameObjects[ i ].Graphics ).Text = incomingMessage.ReadString(); 
			}
		}

		public void HandleRoundCreatedMessage( NetIncomingMessage incomingMessage )
		{
			for( int i = 0; i < 8; i++ )
			{
				( ( ButtonGraphicsComponent )gameObjects[ i ].Graphics ).Text = incomingMessage.ReadString();
			}
		}

		public void Render( ref SpriteBatch batch )
		{
			foreach( var objects in gameObjects )
			{
				objects.Render( ref batch );
			}
		}

		public List< GameObject > gameObjects;
	}
}
