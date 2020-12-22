using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.Network;
using BirdWarsTest.Utilities;
using System.Collections.Generic;

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

		public override void Init( StateHandler handler, StringManager stringManager )
		{
			GameObjects.Clear();
			GameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null,
										     Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new LoginLogoGraphicsComponent( Content ), null, 
								             Identifiers.LoginLogo, stateWidth, 15 ) );
			GameObjects.Add( new GameObject( new LoginBoxGraphicsComponent( Content ), null, 
								             Identifiers.LoginBox1, stateWidth, 195 ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button", 
																		  stringManager.GetString( StringNames.Login ) ), 
											 new LoginButtonInputComponent( handler ), Identifiers.Button1, new Vector2( 
									   	     ( GameObjects[ 2 ].Position.X + 40 ), 
										     ( GameObjects[ 2 ].Position.Y + 155 ) ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button", 
																		  stringManager.GetString( StringNames.Register ) ), 
											 new ButtonChangeStateInputComponent( handler, StateTypes.UserRegistryState ),
										     Identifiers.Button1, new Vector2(
										     ( GameObjects[ 2 ].Position.X + 160 ),
										     ( GameObjects[ 2 ].Position.Y + 155 ) ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", 
																		  stringManager.GetString( StringNames.LostPassword ) ),
										     new ButtonChangeStateInputComponent( handler, StateTypes.PasswordRecoveryState ),
										     Identifiers.Button2, stateWidth,
										     GameObjects[ 2 ].Position.Y + 190 ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Email ),
																	    "Fonts/BabeFont_10" ), null, 
										      Identifiers.TextArea, stateWidth, GameObjects[ 2 ].Position.Y + 30 ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 6 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Password ), 
																		"Fonts/BabeFont_10"), null,
										      Identifiers.TextGraphics, stateWidth, GameObjects[ 2 ].Position.Y + 90 ) );
			GameObjects.Add( new GameObject( new PasswordAreaGraphicsComponent( Content ),
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

		public override void UpdateLogic(StateHandler handler, KeyboardState state, GameTime gameTime)
		{
			UpdateLogic( handler, state );
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