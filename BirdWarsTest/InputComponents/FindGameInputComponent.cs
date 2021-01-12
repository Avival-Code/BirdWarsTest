/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Alerts the network manager to send a Join Round Request
message to the server.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Alerts the network manager to send a Join Round Request
	/// message to the server.
	/// </summary>
	public class FindGameInputComponent : InputComponent
	{
		/// <summary>
		/// Handles the input recieved based on the current game object
		/// and game time.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="gameTime">Current game time.</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		/// <summary>
		/// Handles the input recieved based on the current game object
		/// and keyboard state.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="state">Current keyboard state.</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		/// <summary>
		/// Calls the network manager's join roind message.
		/// </summary>
		/// <param name="gameObject">Current game object</param>
		/// <param name="state">Current keyboard state</param>
		/// <param name="gameState">Current game state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			( ( MainMenuState )gameState ).NetworkManager.JoinRound();
		}
	}
}