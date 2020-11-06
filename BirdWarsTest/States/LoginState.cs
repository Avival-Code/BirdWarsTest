using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using System.Collections.Generic;

namespace BirdWarsTest.States
{
	class LoginState : GameState
	{
		public LoginState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						   GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics, 
						   int width_in, int height_in ) 
			:
			base( newContent, ref newGraphics, width_in, height_in )
		{
			gameObjects = new List< GameObject >();
			gameWindow = gameWindowIn;
		}

		public override void Init( StateHandler handler )
		{
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( content ), null,
										 Identifiers.Player, new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new LoginLogoGraphicsComponent( content ), null, 
								   Identifiers.Player, stateWidth, 15 ) );
			gameObjects.Add( new GameObject( new LoginBoxGraphicsComponent( content ), null, 
								    Identifiers.Player, stateWidth, 195 ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Login" ), 
											 new LoginButtonInputComponent(), Identifiers.Player, new Vector2( 
										  ( gameObjects[ 2 ].position.X + 40 ), 
										  ( gameObjects[ 2 ].position.Y + 155 ) ) ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Register" ), 
											 new RegisterButtonInputComponent( handler ),
										  Identifiers.Player, new Vector2(
										  ( gameObjects[ 2 ].position.X + 160 ),
										  ( gameObjects[ 2 ].position.Y + 155 ) ) ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Lost Password?" ), null,
										  Identifiers.Player, stateWidth,
										  gameObjects[ 2 ].position.Y + 190 ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Username" ), null, 
										   Identifiers.Player, stateWidth, gameObjects[ 2 ].position.Y + 30 ) );
			gameObjects.Add( new GameObject( new TextAreaGraphicsComponent( content ),
											 new TextAreaInputComponent( gameWindow ),
										  Identifiers.Player, stateWidth,
										  ( gameObjects[ 6 ].position.Y + 20 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Password" ), null,
										   Identifiers.Player, stateWidth, gameObjects[ 2 ].position.Y + 90 ) );
			gameObjects.Add( new GameObject( new PasswordAreaGraphicsComponent( content ),
											 new TextAreaInputComponent( gameWindow ),  Identifiers.Player, 
											 stateWidth, ( gameObjects[ 8 ].position.Y + 20 ) ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) 
		{
			foreach( var objects in gameObjects )
				objects.Update( state );
		}

		public override void UpdateLogic( KeyboardState state ) 
		{
			HandleInput( state );
		}

		public override void Render( ref SpriteBatch sprites ) 
		{
			foreach( var objects in gameObjects )
				objects.Render( ref sprites );
		}

		private List<GameObject> gameObjects;
		private GameWindow gameWindow;
	}
}