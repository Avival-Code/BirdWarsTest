using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.States
{
	class WaitingRoomState : GameState
	{
		public WaitingRoomState( Microsoft.Xna.Framework.Content.ContentManager newContent,
								 ref GraphicsDeviceManager newGraphics, int width_in, 
								 int height_in )
			:
			base( newContent, ref newGraphics, width_in, height_in )
		{}

		public override void Init() {}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput() {}

		public override void UpdateLogic( KeyboardState state ) {}

		public override void Render( ref SpriteBatch batch ) {}
	}
}