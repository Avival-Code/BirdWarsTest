using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;
using BirdWarsTest.Utilities;

namespace BirdWarsTest.States
{
	public abstract class GameState
	{
		protected GameState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						     ref GraphicsDeviceManager newGraphics, ref INetworkManager networkManagerIn,
							 int stateWidth_in, int stateHeight_in )
		{
			Content = newContent;
			graphics = newGraphics;
			networkManager = networkManagerIn;
			stateWidth = stateWidth_in;
			stateHeight = stateHeight_in;
		}

		public abstract void Init( StateHandler handler, StringManager stringManager );

		public abstract void Pause();
		public abstract void Resume();
		public abstract void ClearContents();

		public void Enter( StateHandler handler, StringManager stringManager )
		{
			graphics.PreferredBackBufferWidth = stateWidth;
			graphics.PreferredBackBufferHeight = stateHeight;
			graphics.ApplyChanges();
			Init( handler, stringManager );
		}

		public abstract void HandleInput( KeyboardState state );
		public abstract void UpdateLogic( StateHandler handler, KeyboardState state );
		public abstract void UpdateLogic( StateHandler handler, KeyboardState state, GameTime gameTime );
		public abstract void Render( ref SpriteBatch sprites );

		public void ChangeState( StateHandler stateHandler, StateTypes state )
		{
			stateHandler.ChangeState( state );
		}

		public virtual void SetErrorMessage( string errorMessage ) {}

		public Microsoft.Xna.Framework.Content.ContentManager Content { get; }

		protected GraphicsDeviceManager graphics;
		protected INetworkManager networkManager;
		protected int stateWidth;
		protected int stateHeight;
	}
}