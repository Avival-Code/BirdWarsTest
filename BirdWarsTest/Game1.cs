using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest
{
	public class Game1 : Game
	{
		public Game1()
		{
			_graphics = new GraphicsDeviceManager( this );
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			stateHandler = new StateHandler();
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch( GraphicsDevice );
			stateHandler.InitializeStates( Content );
		}

		protected override void Update( GameTime gameTime )
		{
			if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed || 
				 Keyboard.GetState().IsKeyDown( Keys.Escape ) )
				Exit();

			KeyboardState currentKeyboardState = Keyboard.GetState();
			stateHandler.GetCurrentState().UpdateLogic( currentKeyboardState );

			base.Update(gameTime);
		}

		protected override void Draw( GameTime gameTime )
		{
			GraphicsDevice.Clear( Color.CornflowerBlue );

			_spriteBatch.Begin();

			stateHandler.GetCurrentState().Render( ref _spriteBatch );

			_spriteBatch.End();

			base.Draw( gameTime );
		}

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private StateHandler stateHandler;
	}
}
