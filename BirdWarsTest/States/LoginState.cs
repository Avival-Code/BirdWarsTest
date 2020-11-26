using Microsoft.Xna.Framework;
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
			GameObjects = new List< GameObject >();
			gameWindow = gameWindowIn;
		}

		public override void Init( StateHandler handler )
		{
			GameObjects.Clear();
			GameObjects.Add( new GameObject( new SolidRectGraphicsComponent( content ), null,
										     Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new LoginLogoGraphicsComponent( content ), null, 
								             Identifiers.LoginLogo, stateWidth, 15 ) );
			GameObjects.Add( new GameObject( new LoginBoxGraphicsComponent( content ), null, 
								             Identifiers.LoginBox1, stateWidth, 195 ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button", "Login" ), 
											 new LoginButtonInputComponent( handler ), Identifiers.Button1, new Vector2( 
									   	     ( GameObjects[ 2 ].Position.X + 40 ), 
										     ( GameObjects[ 2 ].Position.Y + 155 ) ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button", "Register" ), 
											 new ButtonChangeStateInputComponent( handler, StateTypes.UserRegistryState ),
										     Identifiers.Button1, new Vector2(
										     ( GameObjects[ 2 ].Position.X + 160 ),
										     ( GameObjects[ 2 ].Position.Y + 155 ) ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button2", "Lost Password?" ),
										     new ButtonChangeStateInputComponent( handler, StateTypes.PasswordRecoveryState ),
										     Identifiers.Button2, stateWidth,
										     GameObjects[ 2 ].Position.Y + 190 ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Email", "Fonts/MainFont_S10" ), null, 
										      Identifiers.TextArea, stateWidth, GameObjects[ 2 ].Position.Y + 30 ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 6 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Password", "Fonts/MainFont_S10" ), null,
										      Identifiers.TextGraphics, stateWidth, GameObjects[ 2 ].Position.Y + 90 ) );
			GameObjects.Add( new GameObject( new PasswordAreaGraphicsComponent( content ),
											 new TextAreaInputComponent( gameWindow ),  Identifiers.PasswordArea, 
											 stateWidth, ( GameObjects[ 8 ].Position.Y + 20 ) ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) 
		{
		}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			networkManager.ProcessMessages( handler );

			foreach( var objects in GameObjects  )
				objects.Update( state, this );
		}

		public override void Render( ref SpriteBatch sprites ) 
		{
			foreach( var objects in GameObjects )
				objects.Render( ref sprites );
		}

		public List<GameObject> GameObjects { get; set; }

		private GameWindow gameWindow;
	}
}