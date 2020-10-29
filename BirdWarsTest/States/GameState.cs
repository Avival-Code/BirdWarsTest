using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.States
{
	abstract class GameState
	{
		protected GameState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						     ref GraphicsDeviceManager newGraphics, int stateWidth_in,
							 int stateHeight_in )
		{
			content = newContent;
			graphics = newGraphics;
			stateWidth = stateWidth_in;
			stateHeight = stateHeight_in;
		}

		public abstract void Init();

		public abstract void Pause();
		public abstract void Resume();
		public void Enter()
		{
			graphics.PreferredBackBufferWidth = stateWidth;
			graphics.PreferredBackBufferHeight = stateHeight;
			graphics.ApplyChanges();
			Init();
		}

		public abstract void HandleInput();
		public abstract void UpdateLogic( KeyboardState state );
		public abstract void Render( ref SpriteBatch sprites );

		public void ChangeState( StateHandler stateHandler, StateTypes state )
		{
			stateHandler.ChangeState( state );
		}

		protected Microsoft.Xna.Framework.Content.ContentManager content;
		protected GraphicsDeviceManager graphics;
		protected int stateWidth;
		protected int stateHeight;
	}
}