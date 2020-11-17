using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;

namespace BirdWarsTest.States
{
	abstract class GameState
	{
		protected GameState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						     ref GraphicsDeviceManager newGraphics, ref INetworkManager networkManagerIn,
							 int stateWidth_in, int stateHeight_in )
		{
			content = newContent;
			graphics = newGraphics;
			networkManager = networkManagerIn;
			stateWidth = stateWidth_in;
			stateHeight = stateHeight_in;
		}

		public abstract void Init( StateHandler handler );

		public abstract void Pause();
		public abstract void Resume();
		public void Enter( StateHandler handler )
		{
			graphics.PreferredBackBufferWidth = stateWidth;
			graphics.PreferredBackBufferHeight = stateHeight;
			graphics.ApplyChanges();
			Init( handler );
		}

		public abstract void HandleInput( KeyboardState state );
		public abstract void UpdateLogic( StateHandler handler, KeyboardState state );
		public abstract void Render( ref SpriteBatch sprites );

		public void ChangeState( StateHandler stateHandler, StateTypes state )
		{
			stateHandler.ChangeState( state );
		}

		protected Microsoft.Xna.Framework.Content.ContentManager content;
		protected GraphicsDeviceManager graphics;
		protected INetworkManager networkManager;
		protected int stateWidth;
		protected int stateHeight;
	}
}