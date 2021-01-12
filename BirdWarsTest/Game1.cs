/********************************************
Programmer: Monogame Framework
Date: January 10, 2021

File Description:
The main game class. Handles update and render calls,
stores the main game classes.
*********************************************/
using BirdWarsTest.States;
using BirdWarsTest.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest
{
	/// <summary>
	/// The main game class. Handles update and render calls,
	///stores the main game classes.
	/// </summary>
	public class Game1 : Game
	{
		/// <summary>
		/// Constructs the game instance with the specified network
		/// manager type.
		/// </summary>
		/// <param name="networkManagerIn"></param>
		public Game1( INetworkManager networkManagerIn )
		{
			_graphics = new GraphicsDeviceManager( this );
			Content.RootDirectory = "Content";
			stateHandler = new StateHandler( Content, Window, ref _graphics, networkManagerIn );
			IsMouseVisible = true;
		}

		/// <summary>
		/// Initialized internal components.
		/// </summary>
		protected override void Initialize()
		{
			//Window.Title = "Bird Wars";
			base.Initialize();
		}

		/// <summary>
		/// Sets the graphics device to the spritebatch and initializes
		/// states.
		/// </summary>
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch( GraphicsDevice );
			stateHandler.InitializeInitialState();
		}

		/// <summary>
		/// Unloads all necessary game assets.
		/// </summary>
		protected override void UnloadContent()
		{
			base.UnloadContent();
		}

		/// <summary>
		/// Handles the update calls in all states for every frame.
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void Update( GameTime gameTime )
		{
			if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed || 
				 Keyboard.GetState().IsKeyDown( Keys.Escape ) )
				Exit();

			stateHandler.GetCurrentState().UpdateLogic( stateHandler, Keyboard.GetState(), gameTime );

			base.Update( gameTime );
		}

		/// <summary>
		/// Draws all gameobjects to the screen.
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void Draw( GameTime gameTime )
		{
			GraphicsDevice.Clear( Color.Black );

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