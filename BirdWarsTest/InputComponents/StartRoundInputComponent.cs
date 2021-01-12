/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input coponent used by server to start a game round.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input coponent used by server to start a game round.
	/// </summary>
	public class StartRoundInputComponent : InputComponent
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="handlerIn"></param>
		public StartRoundInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			clicked = false;
		}

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and game time.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="gameTime">Current game time.</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		/// <summary>
		/// Checks if the user clicked on the object's button texture and if so,
		/// calls the network manager StartRound method.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="state">Current keyboard state.</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state )
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			if( mouseRectangle.Intersects( gameObject.GetRectangle() ) && !clicked )
			{
				if( currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed )
				{
					clicked = true;
					handler.networkManager.StartRound();
				}
			}
		}

		/// <summary>
		/// Handles the input recieved based on the current game object state,
		/// current keyboard state and current game state.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) {}

		private readonly StateHandler handler;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		private bool clicked;
	}
}