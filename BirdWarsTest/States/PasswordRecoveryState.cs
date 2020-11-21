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
	class PasswordRecoveryState : GameState
	{
		public PasswordRecoveryState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						   GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics,
						   ref INetworkManager networkManagerIn, int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			gameObjects = new List< GameObject >();
			gameWindow = gameWindowIn;
		}

		public override void Init( StateHandler handler ) 
		{
			gameObjects.Clear();
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( content ), null,
										 Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new MenuBoxGraphicsComponent( content ), null,
										 Identifiers.PasswordBox, new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Recover Password", "Fonts/MainFont_S10" ),
							                 null, Identifiers.TextGraphics, stateWidth, 50.0f ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Email", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics, stateWidth, 
											 gameObjects[ 2 ].Position.Y + 50.0f ) );
			gameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     gameObjects[ 3 ].Position.Y + 20 ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button2", "Send Code" ),
											 null, Identifiers.Button1, stateWidth, 
										     gameObjects[ 4 ].Position.Y + 40.0f ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Code", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics, stateWidth,
											 gameObjects[ 5 ].Position.Y + 60.0f ) );
			gameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
											 Identifiers.TextArea, stateWidth,
											 gameObjects[ 6 ].Position.Y + 20 ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "New Password", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics, stateWidth,
											 gameObjects[ 7 ].Position.Y + 35.0f ) );
			gameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
											 Identifiers.TextArea, stateWidth,
											 gameObjects[ 8 ].Position.Y + 20 ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button2", "Reset" ),
											 null, Identifiers.Button1, 
											 new Vector2( gameObjects[ 9 ].Position.X - 30,
														  gameObjects[ 9 ].Position.Y + 50.0f ) ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button2", "Cancel" ),
											 new ChangeStateInputComponent( handler, StateTypes.LoginState ),
										     Identifiers.Button1, 
											 new Vector2( gameObjects[ 10 ].Position.X + 160.0f,
												          gameObjects[ 9 ].Position.Y + 50.0f ) ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			foreach( var objects in gameObjects )
				objects.Update( state );
		}

		public override void Render( ref SpriteBatch sprites ) 
		{
			foreach( var objects in gameObjects )
				objects.Render( ref sprites );
		}

		private List< GameObject > gameObjects;
		private GameWindow gameWindow;
	}
}
