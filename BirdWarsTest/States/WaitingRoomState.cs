﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.GameObjects.ObjectManagers;
using System.Collections.Generic;

namespace BirdWarsTest.States
{
	class WaitingRoomState : GameState
	{
		public WaitingRoomState( Microsoft.Xna.Framework.Content.ContentManager newContent,
								 GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics, 
								 ref INetworkManager networkManagerIn, int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			gameWindow = gameWindowIn;
			gameObjects = new List< GameObject >();
			usernameManager = new ChatUsernameManager();
		}

		public override void Init( StateHandler handler ) 
		{
			usernameManager.AddObjects( content );
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( content ), null, Identifiers.Background,
						     new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new DecorationGraphicsComponent( content, "TextAreas/ChatBoard530x480" ), null,
											 Identifiers.TextArea, new Vector2( 250.0f, 20.0f ) ) );
			gameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content, "TextAreas/ChatTextArea530x35" ),
											 new TextAreaInputComponent( gameWindow ), 
											 Identifiers.TextArea, new Vector2( 250.0f, 520.0f ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Players", "Fonts/MainFont_S20" ), null,
											 Identifiers.TextGraphics, new Vector2( 70.0f, 20.0f ) ) );

			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button2", "Start Round"), null, 
											 Identifiers.Button2, 
											 new Vector2( 25.0f, usernameManager.gameObjects[ usernameManager.gameObjects.Count - 1 ].position.Y + 60 ) ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button2", "Leave" ), null, 
											 Identifiers.Button2,
											 new Vector2( 25.0f, gameObjects[ gameObjects.Count - 1 ].position.Y + 35 ) ) );
			
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			networkManager.ProcessMessages( handler );
			foreach( var objects in gameObjects )
			{
				objects.Update( state );
			}
		}

		public override void Render( ref SpriteBatch batch ) 
		{
			foreach( var objects in gameObjects )
			{
				objects.Render( ref batch );
			}
			usernameManager.Render( ref batch );
		}

		private GameWindow gameWindow;
		private List< GameObject > gameObjects;
		public ChatUsernameManager usernameManager;
	}
}