﻿/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles drawing and updating of all gameObjects 
necessary for WaitingRoomState.
*********************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.GameObjects.ObjectManagers;
using BirdWarsTest.Utilities;
using System.Collections.Generic;

namespace BirdWarsTest.States
{
	/// <summary>
	/// Handles drawing and updating of all gameObjects 
	///necessary for WaitingRoomState.
	/// </summary>
	public class WaitingRoomState : GameState
	{
		/// <summary>
		/// Creates empty MainMenuState. Sets gamewindow reference and initializes
		/// gameObjects List.
		/// </summary>
		/// <param name="newContent">Game contentManager</param>
		/// <param name="gameWindowIn">Game window reference</param>
		/// <param name="newGraphics">Graphics device reference</param>
		/// <param name="networkManagerIn">Game network manager</param>
		/// <param name="width_in">State width</param>
		/// <param name="height_in">State height</param>
		public WaitingRoomState( Microsoft.Xna.Framework.Content.ContentManager newContent,
								 GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics, 
								 ref INetworkManager networkManagerIn, int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			gameWindow = gameWindowIn;
			GameObjects = new List< GameObject >();
			UsernameManager = new ChatUsernameManager();
			MessageManager = null;
		}

		/// <summary>
		/// Creates all state gameObjects.
		/// </summary>
		/// <param name="handler">Game state</param>
		/// <param name="stringManager">Game string manager</param>
		public override void Init( StateHandler handler, StringManager stringManager ) 
		{
			isInitialized = true;
			ClearContents();
			UsernameManager.AddObjects( Content );
			GameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null, 
											 Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new DecorationGraphicsComponent( Content, "TextAreas/ChatBoard530x480" ), 
											 null, Identifiers.TextArea, new Vector2( 250.0f, 20.0f ) ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextAreas/ChatTextArea530x35" ),
											 new TextAreaInputComponent( gameWindow ), 
											 Identifiers.TextArea, new Vector2( 250.0f, 520.0f ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Players ), 
																		"Fonts/BabeFont_22" ), 
											 null, Identifiers.TextGraphics, new Vector2( 65.0f, 20.0f ) ) );
			AddStateButtons( handler, stringManager );
			MessageManager = new ChatMessageManager( Content, stringManager, GameObjects[ 1 ].GetRectangle() );
		}

		/// <summary>
		/// Removes all gameObjects from state list.
		/// </summary>
		public override void ClearContents()
		{
			UsernameManager.ClearObjects();
			GameObjects.Clear();
			MessageManager = null;
		}

		/// <summary>
		/// Handles network incoming messages. Updates all gameObjects
		/// in state.
		/// </summary>
		/// <param name="handler">Game statehandler</param>
		/// <param name="state">current keyboard state</param>
		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			networkManager.ProcessMessages( handler );
			foreach( var objects in GameObjects )
			{
				objects.Update( state );
			}
			UpdateButtonLogic( state );
		}

		/// <summary>
		/// Handles network incoming messages. Updates all gameObjects
		/// in state.
		/// </summary>
		/// <param name="handler">Game statehandler</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameTime">GAme time</param>
		public override void UpdateLogic( StateHandler handler, KeyboardState state, GameTime gameTime )
		{
			UpdateLogic( handler, state );
		}

		/// <summary>
		/// Draws all gameObjects on the screen.
		/// </summary>
		/// <param name="batch">Game Spritebatch</param>
		public override void Render( ref SpriteBatch batch ) 
		{
			foreach( var objects in GameObjects )
			{
				objects.Render( ref batch );
			}
			MessageManager.Render( ref batch );
			UsernameManager.Render( ref batch );
		}

		private void AddStateButtons( StateHandler handler, StringManager stringManager )
		{
			if( networkManager.IsHost() )
			{
				GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2",
																			  stringManager.GetString( StringNames.StartRound ) ),
												 new StartRoundInputComponent( handler ), Identifiers.Button2,
												 new Vector2( 25.0f, 520.0f ) ) );
			}
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2",
																		  stringManager.GetString( StringNames.Leave ) ),
											 new LeaveGameInputComponent( handler, StateTypes.MainMenuState ), Identifiers.Button2,
											 new Vector2( 25.0f, 555.0f ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2",
																		  stringManager.GetString( StringNames.SendMessage  ) ), 
											 new SendChatMessageInputComponent( handler ), Identifiers.ChatMessageSender,
											 new Vector2( 645.0f, 555.0f ) ) );
		}

		private void UpdateButtonLogic( KeyboardState state )
		{
			if( networkManager.IsHost() )
			{
				GameObjects[ 5 ].Update( state, this );
				GameObjects[ 6 ].Update( state, this );
			}
			else
			{
				GameObjects[ 4 ].Update( state, this );
				GameObjects[ 5 ].Update( state, this );
			}
		}

		/// <summary>
		/// Exposes the state's protected networkmanager.
		/// </summary>
		public INetworkManager NetworkManager
		{
			get{ return networkManager; }
		}

		///<value>The list of state gameObjects</value>
		public List< GameObject > GameObjects { get; set; }

		///<value>ChatUsername manager</value>
		public ChatUsernameManager UsernameManager { get; set; }

		///<value>ChatMessage manager</value>
		public ChatMessageManager MessageManager { get; set; }

		private GameWindow gameWindow;

		///<value>Bool indicating if the state has been initialized.</value>
		public bool IsInitialized
		{
			get { return isInitialized; }
			private set {}
		}
	}
}