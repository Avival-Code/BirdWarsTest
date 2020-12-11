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
	class UserRegistryState : GameState
	{
		public UserRegistryState( Microsoft.Xna.Framework.Content.ContentManager newContent,
								  GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics,
								  ref INetworkManager networkManagerIn, int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			gameWindow = gameWindowIn;
			GameObjects = new List< GameObject >();
		}
		
		public override void Init( StateHandler handler, StringManager stringManager ) 
		{
			GameObjects.Clear();
			GameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null,
										     Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new RegisterBoxGraphicsComponent( Content ), null,
										     Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Registration", "Fonts/MainFont_S15" ),
											 null, Identifiers.TextGraphics, stateWidth, GameObjects[ 1 ].Position.Y + 50 ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", "Register" ),
											 new RegisterButtonInputComponent( handler ), Identifiers.Button1, 
											 new Vector2( 70.0f, 425.0f ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", "Cancel" ),
											 new ButtonChangeStateInputComponent( handler, StateTypes.LoginState ),
										     Identifiers.Button1, new Vector2( 220.0f, 425.0f ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Name", "Fonts/MainFont_S10" ), null,
										     Identifiers.TextArea, stateWidth, GameObjects[ 2 ].Position.Y + 35 ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										    ( GameObjects[ 5 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Last Name", "Fonts/MainFont_S10" ), null,
										     Identifiers.TextArea, stateWidth, GameObjects[ 6 ].Position.Y + 35 ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 7 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Username", "Fonts/MainFont_S10" ), null,
										     Identifiers.TextArea, stateWidth, GameObjects[ 8 ].Position.Y + 35 ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 9 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Email", "Fonts/MainFont_S10" ), null,
										     Identifiers.TextArea, stateWidth, GameObjects[ 10 ].Position.Y + 35 ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
									   	     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 11 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Password", "Fonts/MainFont_S10" ), null,
										     Identifiers.TextArea, stateWidth, GameObjects[ 12 ].Position.Y + 35 ) );
			GameObjects.Add( new GameObject( new PasswordAreaGraphicsComponent( Content ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 13 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Confirm Password", "Fonts/MainFont_S10" ), null,
										     Identifiers.TextArea, stateWidth, GameObjects[ 14 ].Position.Y + 35 ) );
			GameObjects.Add( new GameObject( new PasswordAreaGraphicsComponent( Content ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 15 ].Position.Y + 20 ) ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			networkManager.ProcessMessages( handler );

			foreach( var objects in GameObjects )
				objects.Update( state, this );
		}

		public override void UpdateLogic( StateHandler handler, KeyboardState state, GameTime gameTime )
		{
			UpdateLogic( handler, state );
		}

		public override void Render( ref SpriteBatch batch ) 
		{
			foreach( var objects in GameObjects )
				objects.Render( ref batch );
		}

		public List<GameObject> GameObjects { get; set; }

		private GameWindow gameWindow;
	}
}