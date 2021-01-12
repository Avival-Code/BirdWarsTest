/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles the creation, organization, display and removal
of chat messages used in WaitingRoomState.
*********************************************/
using BirdWarsTest.Utilities;
using BirdWarsTest.GraphicComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	/// <summary>
	/// Handles the creation, organization, display and removal
	///of chat messages used in WaitingRoomState.
	/// </summary>
	public class ChatMessageManager
	{
		/// <summary>
		/// Creates an isntance of ChatMessageManager.
		/// </summary>
		/// <param name="contentIn">Game contentManager</param>
		/// <param name="stringManager">Game string manager.</param>
		/// <param name="chatBoardBoundariesIn">Chat box area rectangle.</param>
		public ChatMessageManager( Microsoft.Xna.Framework.Content.ContentManager contentIn, 
								   StringManager stringManager, Rectangle chatBoardBoundariesIn )
		{
			content = contentIn;
			gameObjects = new List< GameObject >();
			chatBoardBoundaries = chatBoardBoundariesIn;
			AddMessage( true, stringManager.GetString( StringNames.ServerUsername ), 
						stringManager.GetString( StringNames.ServerMessage ) );
		}

		/// <summary>
		/// Handles the values from an incoming chatMessage.
		/// </summary>
		/// <param name="incomingUsername">The username of the user that sent the message.</param>
		/// <param name="message">The message body.</param>
		/// <param name="currentUsername">The username if the current user.</param>
		public void HandleChatMessage( string incomingUsername, string message, string currentUsername )
		{
			ManageMessages();
			AddMessage( ( incomingUsername.Equals( currentUsername ) ), incomingUsername, message );
		}

		private void AddMessage( bool isFromOtherUser, string username, string message )
		{
			var yPosition = ( ChatMessageTextureHeight + 5 );
			MoveMessagePositionsUp( yPosition );
			gameObjects.Add( 
				new GameObject( new ChatMessageGraphicsComponent( content, "TextAreas/ChatMessage500x68", 
							    username, message, isFromOtherUser ), null, Identifiers.ChatMessage, 
								new Vector2( chatBoardBoundaries.X + 13, chatBoardBoundaries.Height - yPosition ) ) );
		}

		private void ManageMessages()
		{
			if( gameObjects.Count > MaxMessages )
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

		/// <summary>
		/// Draws the chat message game objects on the screen.
		/// </summary>
		/// <param name="batch">Game spritebatch.</param>
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
		private const int MaxMessages = 5;
		private const int ChatMessageTextureHeight = 68;
	}
}