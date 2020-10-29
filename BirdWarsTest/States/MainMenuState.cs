using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents;
using BirdWarsTest.GraphicComponents;

namespace BirdWarsTest.States
{
	class MainMenuState : GameState
	{
		public MainMenuState( int width_in, int height_in )
		{
			stateWidth = width_in;
			stateHeight = height_in;
		}

		public override void Init( Microsoft.Xna.Framework.Content.ContentManager newContent,
								   ref GraphicsDeviceManager newGraphics )
		{
			content = newContent;
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput() {}

		public override void UpdateLogic( KeyboardState state )
		{
			HandleInput();
		}

		public override void Render( ref SpriteBatch sprites ) {}
	}
}