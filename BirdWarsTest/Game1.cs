using BirdWarsTest.States;
using BirdWarsTest.Network;
using BirdWarsTest.Database;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Lidgren.Network;
using System;

namespace BirdWarsTest
{
	public class Game1 : Game
	{
		public Game1( INetworkManager networkManagerIn )
		{
			_graphics = new GraphicsDeviceManager( this );
			Content.RootDirectory = "Content";
			stateHandler = new StateHandler( Content, Window, ref _graphics, networkManagerIn );
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			//Window.Title = "Bird Wars";
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch( GraphicsDevice );
			stateHandler.InitializeStates();
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();
		}

		protected override void Update( GameTime gameTime )
		{
			if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed || 
				 Keyboard.GetState().IsKeyDown( Keys.Escape ) )
				Exit();

			stateHandler.GetCurrentState().UpdateLogic( stateHandler, Keyboard.GetState() );

			base.Update( gameTime );
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