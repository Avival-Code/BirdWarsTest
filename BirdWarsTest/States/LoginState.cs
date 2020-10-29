using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;

namespace BirdWarsTest.States
{
	class LoginState : GameState
	{
		public LoginState( int width_in, int height_in ) 
		{
			stateWidth = width_in;
			stateHeight = height_in;
		}

		public override void Init( Microsoft.Xna.Framework.Content.ContentManager newContent,
								   ref GraphicsDeviceManager newGraphics )
		{
			content = newContent;
			graphics = newGraphics;
			logo = new GameObject( new LoginLogoGraphicsComponent( content ), null, 
								   Identifiers.Player, stateWidth, stateHeight );

			background = new GameObject( new SolidRectGraphicsComponent( content ), null,
										 Identifiers.Player, new Vector2( 0.0f, 0.0f ) );
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
		}

		private GameObject logo;
		private GameObject background;
	}
}