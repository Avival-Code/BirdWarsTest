/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Base abstract GameState class
*********************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;
using BirdWarsTest.Utilities;

namespace BirdWarsTest.States
{
	/// <summary>
	/// Base abstract GameState class
	/// </summary>
	public abstract class GameState
	{
		/// <summary>
		/// Initialized the state, sets the state width and height with
		/// the specified values.
		/// </summary>
		/// <param name="newContent">Game content Manager</param>
		/// <param name="newGraphics">Graphics Device Manager</param>
		/// <param name="networkManagerIn">Game network manager</param>
		/// <param name="stateWidth_in">Specified state width</param>
		/// <param name="stateHeight_in">Specified state height</param>
		protected GameState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						     ref GraphicsDeviceManager newGraphics, ref INetworkManager networkManagerIn,
							 int stateWidth_in, int stateHeight_in )
		{
			Content = newContent;
			graphics = newGraphics;
			networkManager = networkManagerIn;
			stateWidth = stateWidth_in;
			stateHeight = stateHeight_in;
			isInitialized = false;
		}

		/// <summary>
		/// Methos called before transitioning to state.
		/// Effects vay depending on child state class.
		/// </summary>
		/// <param name="handler">Game statehandler</param>
		/// <param name="stringManager">Game string manager</param>
		public abstract void Init( StateHandler handler, StringManager stringManager );

		/// <summary>
		/// Clears all gameobjects in state.
		/// </summary>
		public abstract void ClearContents();

		/// <summary>
		/// Sets state width and height. Calls Init method.
		/// </summary>
		/// <param name="handler"></param>
		/// <param name="stringManager"></param>
		public void Enter( StateHandler handler, StringManager stringManager )
		{
			graphics.PreferredBackBufferWidth = stateWidth;
			graphics.PreferredBackBufferHeight = stateHeight;
			graphics.ApplyChanges();
			Init( handler, stringManager );
		}

		/// <summary>
		/// Updates all necessary gameobjects in gamestate
		/// </summary>
		/// <param name="handler">game state handler</param>
		/// <param name="state">curren keyboard state</param>
		public abstract void UpdateLogic( StateHandler handler, KeyboardState state );

		/// <summary>
		/// Updates all necessary gameobjects in gamestate
		/// </summary>
		/// <param name="handler">game state handler</param>
		/// <param name="state">curren keyboard state</param>
		/// <param name="gameTime">game time</param>
		public abstract void UpdateLogic( StateHandler handler, KeyboardState state, GameTime gameTime );

		/// <summary>
		/// Draws the state gameObjects to the screen.
		/// </summary>
		/// <param name="sprites">Game Spritebatch</param>
		public abstract void Render( ref SpriteBatch sprites );

		/// <summary>
		/// Sets an error message to error message object.
		/// </summary>
		/// <param name="errorMessage">The value of the message.</param>
		public virtual void SetErrorMessage( string errorMessage ) {}

		/// <summary>
		/// Sets a normal message to the message object.
		/// </summary>
		/// <param name="message">The value of the message</param>
		public virtual void SetMessage( string message ) {}

		/// <summary>
		/// Clears text areas. Effects vary with child state.
		/// </summary>
		public virtual void ClearTextAreas() {}

		///<value>Game content manager reference</value>
		public Microsoft.Xna.Framework.Content.ContentManager Content { get; }

		///<value>Graphics device reference</value>
		protected GraphicsDeviceManager graphics;

		///<value>Game networkManager reference</value>
		protected INetworkManager networkManager;

		///<value>Becomes true if Init method has been called at least once.</value>
		protected bool isInitialized;

		///<value>State width</value>
		protected int stateWidth;

		///<value>State height</value>
		protected int stateHeight;
	}
}