/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component used to change the current state.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component used to change the current state.
	/// </summary>
	public class SelectorChangeStateInputComponent : InputComponent
	{
		/// <summary>
		/// Sets the statehandler reference and target state.
		/// </summary>
		/// <param name="handlerIn"></param>
		/// <param name="changeStateIn"></param>
		public SelectorChangeStateInputComponent( StateHandler handlerIn, StateTypes changeStateIn )
		{
			handler = handlerIn;
			changeState = changeStateIn;
		}

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and game time.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="gameTime">Current game time.</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and keyboard state.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="state">Current keyboard state.</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		/// <summary>
		/// Changes the current game state to the target state.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			handler.ChangeState( changeState );
		}

		private readonly StateHandler handler;
		private readonly StateTypes changeState;
	}
}