using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Lidgren.Network;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	class ChatMessageManager
	{
		public ChatMessageManager( Microsoft.Xna.Framework.Content.ContentManager contentIn, 
								   Rectangle chatBoardBoundariesIn )
		{
			content = contentIn;
			gameObjects = new List< GameObject >();
			chatBoardBoundaries = chatBoardBoundariesIn;
		}

		public void HandleChatMessage( string incomingUsername, string message, string currentUsername )
		{
			ManageMessages();
			AddMessage( ( incomingUsername.Equals( currentUsername ) ), incomingUsername, message );
		}

		private void AddMessage( bool isFromOtherUser, string username, string message )
		{
			var yPosition = ( chatMessageTextureHeight + 5 );
			MoveMessagePositionsUp( yPosition );
			gameObjects.Add( 
				new GameObject( new ChatMessageGraphicsComponent( content, "TextAreas/ChatMessage500x68", 
							    username, message, isFromOtherUser ), null, Identifiers.ChatMessage, 
								new Vector2( chatBoardBoundaries.X + 13, chatBoardBoundaries.Height - yPosition ) ) );
		}

		private void ManageMessages()
		{
			if( gameObjects.Count > 5 )
			{
				gameObjects.RemoveAt( 0 );
			}
		}

		private void MoveMessagePositionsUp( float offset )
		{
			for( int i = 0; i < gameObjects.Count; i++ )
			{
				gameObjects[ i ].Position = new Vector2( gameObjects[ i ].Position.X, gameObjects[ i ].Position.Y - offset );
			}
		}

		public void Render( ref SpriteBatch batch )
		{
			foreach( var objects in gameObjects )
			{
				objects.Render( ref batch );
			}
		}

		private Microsoft.Xna.Framework.Content.ContentManager content;
		private List< GameObject > gameObjects;
		private Rectangle chatBoardBoundaries;
		private const int maxMessages = 7;
		private const int chatMessageTextureHeight = 68;
	}
}
