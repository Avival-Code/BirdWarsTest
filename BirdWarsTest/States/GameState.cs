using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.States
{
	abstract class GameState
	{
		public abstract void Init( Microsoft.Xna.Framework.Content.ContentManager newContent, 
								   ref GraphicsDeviceManager newGraphics );

		public abstract void Pause();
		public abstract void Resume();
		public void Enter( Microsoft.Xna.Framework.Content.ContentManager newContent,
								   ref GraphicsDeviceManager newGraphics )
		{
			newGraphics.PreferredBackBufferWidth = stateWidth;
			newGraphics.PreferredBackBufferHeight = stateHeight;
			newGraphics.ApplyChanges();
			Init( newContent, ref newGraphics );
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