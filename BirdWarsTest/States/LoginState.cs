using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;

namespace BirdWarsTest.States
{
	class LoginState : GameState
	{
		public LoginState() {}

		public override void Init( Microsoft.Xna.Framework.Content.ContentManager newContent, Vector2 screenSize ) 
		{
			content = newContent;
			test = new GameObject( Identifiers.Player, 0.0f, 0.0f, null,
								   new LoginLogoGraphicsComponent( content ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void Enter() {}

		public override void HandleInput() {}

		public override void UpdateLogic( KeyboardState state ) 
		{
			HandleInput();
			test.Update( state );
		}

		public override void Render( ref SpriteBatch sprites ) 
		{
			test.Render( ref sprites );
		}

		private GameObject test;
	}
}
