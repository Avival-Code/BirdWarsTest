using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.States
{
	class PlayState : GameState
	{
		public PlayState( int width_in, int height_in )
		{
			stateWidth = width_in;
			stateHeight = height_in;
		}

		public override void Init( Microsoft.Xna.Framework.Content.ContentManager newContent,
								   ref GraphicsDeviceManager newGraphics ) {}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput() {}

		public override void UpdateLogic( KeyboardState state ) {}

		public override void Render( ref SpriteBatch batch) {}
	}
}