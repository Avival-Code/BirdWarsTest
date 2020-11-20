﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using System.Collections.Generic;
using BirdWarsTest.Network;
using System;

namespace BirdWarsTest.States
{
	class LoginState : GameState
	{
		public LoginState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						   GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics, 
						   ref INetworkManager networkManagerIn,
						   int width_in, int height_in ) 
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			gameObjects = new List< GameObject >();
			support = new NetworkSupport();
			gameWindow = gameWindowIn;
		}

		public override void Init( StateHandler handler )
		{
			gameObjects.Clear();
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( content ), null,
										 Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new LoginLogoGraphicsComponent( content ), null, 
								   Identifiers.LoginLogo, stateWidth, 15 ) );
			gameObjects.Add( new GameObject( new LoginBoxGraphicsComponent( content ), null, 
								    Identifiers.LoginBox1, stateWidth, 195 ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button", "Login" ), 
											 new LoginButtonInputComponent( handler ), Identifiers.Button1, new Vector2( 
										  ( gameObjects[ 2 ].position.X + 40 ), 
										  ( gameObjects[ 2 ].position.Y + 155 ) ) ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button", "Register" ), 
											 new ChangeStateInputComponent( handler, StateTypes.UserRegistryState ),
										  Identifiers.Button1, new Vector2(
										  ( gameObjects[ 2 ].position.X + 160 ),
										  ( gameObjects[ 2 ].position.Y + 155 ) ) ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button2", "Lost Password?" ),
										     new ChangeStateInputComponent( handler, StateTypes.PasswordRecoveryState ),
										  Identifiers.Button2, stateWidth,
										  gameObjects[ 2 ].position.Y + 190 ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Username", "Fonts/MainFont_S10" ), null, 
										   Identifiers.TextArea, stateWidth, gameObjects[ 2 ].position.Y + 30 ) );
			gameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										  Identifiers.TextArea, stateWidth,
										  ( gameObjects[ 6 ].position.Y + 20 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Password", "Fonts/MainFont_S10" ), null,
										   Identifiers.TextGraphics, stateWidth, gameObjects[ 2 ].position.Y + 90 ) );
			gameObjects.Add( new GameObject( new PasswordAreaGraphicsComponent( content ),
											 new TextAreaInputComponent( gameWindow ),  Identifiers.PasswordArea, 
											 stateWidth, ( gameObjects[ 8 ].position.Y + 20 ) ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) 
		{
		}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			support.ProcessMessages( networkManager, handler );

			foreach( var objects in gameObjects  )
				objects.Update( state, this );
		}

		public override void Render( ref SpriteBatch sprites ) 
		{
			foreach( var objects in gameObjects )
				objects.Render( ref sprites );
		}

		public List< GameObject > gameObjects;
		private GameWindow gameWindow;
		private NetworkSupport support;
	}
}