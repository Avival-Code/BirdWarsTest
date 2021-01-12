/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component used to terminate a user session
on the application.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component used to terminate a user session
	/// on the application.
	/// </summary>
	public class LogoutInputComponent : InputComponent
	{
		/// <summary>
		/// Sets the statehandler reference.
		/// </summary>
		/// <param name="handlerIn">Game statehandler</param>
		public LogoutInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
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
		/// Calls the network manager's logout method and changes satte to 
		/// the login state.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			( ( MainMenuState )gameState ).NetworkManager.Logout();
			handler.ChangeState( StateTypes.LoginState );
		}

		private readonly StateHandler handler;
	}
}