/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles the username boxes and displays the rounds players
usernames.
*********************************************/
using BirdWarsTest.GraphicComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lidgren.Network;
using System.Collections.Generic;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	/// <summary>
	/// Handles the username boxes and displays the rounds players
	/// usernames.
	/// </summary>
	public class ChatUsernameManager
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ChatUsernameManager()
		{
			gameObjects = new List< GameObject >();
		}

		/// <summary>
		/// Creates username box game objects.
		/// </summary>
		/// <param name="content">Game contentManager.</param>
		public void AddObjects( Microsoft.Xna.Framework.Content.ContentManager content )
		{
			for( int i = 0; i < 8; i++ )
			{
				gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Decorations/UsernameBorder200x50", "" ),
												 null, Identifiers.Decoration,
												 new Vector2( 25.0f, 50.0f + ( 55.0f * i ) ) ) );
			}
		}

		/// <summary>
		/// Clears the username box game objects.
		/// </summary>
		public void ClearObjects()
		{
			gameObjects.Clear();
		}

		/// <summary>
		/// Handles the incoming RoundStateChangedMessage.
		/// </summary>
		/// <param name="incomingMessage">The incoming message.</param>
		public void HandleRoundStateChangeMessage( NetIncomingMessage incomingMessage )
		{
			for( int i = 0; i < 8; i++ )
			{
				( ( ButtonGraphicsComponent )gameObjects[ i ].Graphics ).Text = incomingMessage.ReadString(); 
			}
		}

		/// <summary>
		/// Sets the usernames in each chat username box.
		/// </summary>
		/// <param name="incomingMessage">The incoming round created message.</param>
		public void HandleRoundCreatedMessage( NetIncomingMessage incomingMessage )
		{
			for( int i = 0; i < 8; i++ )
			{
				( ( ButtonGraphicsComponent )gameObjects[ i ].Graphics ).Text = incomingMessage.ReadString();
			}
		}

		/// <summary>
		/// Draws the chat box gameobjects and player usernames.
		/// </summary>
		/// <param name="batch">Game spritebatch</param>
		public void Render( ref SpriteBatch batch )
		{
			foreach( var objects in gameObjects )
			{
				objects.Render( ref batch );
			}
		}

		private List<GameObject> gameObjects;
	}
}