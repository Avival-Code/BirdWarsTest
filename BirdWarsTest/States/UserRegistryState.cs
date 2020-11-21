using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.Network;
using System.Collections.Generic;

namespace BirdWarsTest.States
{
	class UserRegistryState : GameState
	{
		public UserRegistryState( Microsoft.Xna.Framework.Content.ContentManager newContent,
								  GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics,
								  ref INetworkManager networkManagerIn, int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in,height_in )
		{
			gameWindow = gameWindowIn;
			gameObjects = new List< GameObject >();
		}
		
		public override void Init( StateHandler handler ) 
		{
			gameObjects.Clear();
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( content ), null,
										 Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new RegisterBoxGraphicsComponent( content ), null,
										 Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button2", "Register" ),
											 new RegisterButtonInputComponent( handler ), Identifiers.Button1, 
											 new Vector2( 70.0f, 425.0f ) ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button2", "Cancel" ),
											 new ChangeStateInputComponent( handler, StateTypes.LoginState ),
										  Identifiers.Button1, new Vector2( 220.0f, 425.0f ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Name", "Fonts/MainFont_S10" ), null,
										   Identifiers.TextArea, stateWidth, gameObjects[ 1 ].Position.Y + 50 ) );
			gameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										  Identifiers.TextArea, stateWidth,
										  ( gameObjects[ 4 ].Position.Y + 20 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Last Name", "Fonts/MainFont_S10" ), null,
										   Identifiers.TextArea, stateWidth, gameObjects[ 5 ].Position.Y + 35 ) );
			gameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										  Identifiers.TextArea, stateWidth,
										  ( gameObjects[ 6 ].Position.Y + 20 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent(content, "Username", "Fonts/MainFont_S10" ), null,
										   Identifiers.TextArea, stateWidth, gameObjects[ 7 ].Position.Y + 35 ) );
			gameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										  Identifiers.TextArea, stateWidth,
										  ( gameObjects[ 8 ].Position.Y + 20 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Email", "Fonts/MainFont_S10" ), null,
										   Identifiers.TextArea, stateWidth, gameObjects[ 9 ].Position.Y + 35 ) );
			gameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										  Identifiers.TextArea, stateWidth,
										  ( gameObjects[ 10 ].Position.Y + 20 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent(content, "Password", "Fonts/MainFont_S10" ), null,
										   Identifiers.TextArea, stateWidth, gameObjects[ 11 ].Position.Y + 35 ) );
			gameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										  Identifiers.TextArea, stateWidth,
										  ( gameObjects[ 12 ].Position.Y + 20 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Confirm Password", "Fonts/MainFont_S10" ), null,
										   Identifiers.TextArea, stateWidth, gameObjects[ 13 ].Position.Y + 35 ) );
			gameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										  Identifiers.TextArea, stateWidth,
										  ( gameObjects[ 14 ].Position.Y + 20 ) ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			networkManager.ProcessMessages( handler );

			foreach( var objects in gameObjects )
				objects.Update( state, this );
		}

		public override void Render( ref SpriteBatch batch ) 
		{
			foreach( var objects in gameObjects )
				objects.Render( ref batch );
		}

		private GameWindow gameWindow;
		public List< GameObject > gameObjects;
	}
}