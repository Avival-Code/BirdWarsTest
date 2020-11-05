using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;

namespace BirdWarsTest.States
{
	class LoginState : GameState
	{
		public LoginState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						   ref GraphicsDeviceManager newGraphics, int width_in, 
						   int height_in ) 
			:
			base( newContent, ref newGraphics, width_in, height_in )
		{}

		public override void Init()
		{
			background = new GameObject( new SolidRectGraphicsComponent( content ), null,
										 Identifiers.Player, new Vector2( 0.0f, 0.0f ) );
			logo = new GameObject( new LoginLogoGraphicsComponent( content ), null, 
								   Identifiers.Player, stateWidth, 15 );
			board = new GameObject( new LoginBoxGraphicsComponent( content ), null, 
								    Identifiers.Player, stateWidth, 195 );
			loginButton = new GameObject( new ButtonGraphicsComponent( content, "Login" ), null,
										  Identifiers.Player, new Vector2( 
										  ( board.position.X + 40 ), 
										  ( board.position.Y + 155 ) ) );
			registerButton = new GameObject( new ButtonGraphicsComponent( content, "Register" ), null,
										  Identifiers.Player, new Vector2(
										  ( board.position.X + 160 ),
										  ( board.position.Y + 155 ) ) );
			passwordButton = new GameObject( new ButtonGraphicsComponent( content, "Lost Password?" ), null,
										  Identifiers.Player, stateWidth, 
										  board.position.Y + 190 );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput() {}

		public override void UpdateLogic( KeyboardState state ) 
		{
			HandleInput();
		}

		public override void Render( ref SpriteBatch sprites ) 
		{
			background.Render( ref sprites );
			logo.Render( ref sprites );
			board.Render( ref sprites );
			loginButton.Render( ref sprites );
			registerButton.Render( ref sprites );
			passwordButton.Render( ref sprites );
		}

		private GameObject logo;
		private GameObject background;
		private GameObject board;
		private GameObject loginButton;
		private GameObject registerButton;
		private GameObject passwordButton;
	}
}