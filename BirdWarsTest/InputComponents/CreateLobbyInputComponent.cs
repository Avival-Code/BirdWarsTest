/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component used to alert network manager who is the host
to create a game round.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component used to alert network manager who is the host
	/// to create a game round.
	/// </summary>
	public class CreateLobbyInputComponent : InputComponent
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
		/// Calls the network manager Create round method.
		/// </summary>
		/// <param name="gameObject">Game Object</param>
		/// <param name="state">current keyboard state.</param>
		/// <param name="gameState">current game state.</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			( ( MainMenuState )gameState ).NetworkManager.CreateRound();
		}
	}
}