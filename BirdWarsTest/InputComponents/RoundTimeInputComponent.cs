/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles and updates the round timer.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Handles and updates the round timer.
	/// </summary>
	public class RoundTimeInputComponent : InputComponent
	{
		/// <summary>
		/// Sets round time to 300 seconds and the statehandler 
		/// reference.
		/// </summary>
		/// <param name="handlerIn">Game statehandler</param>
		public RoundTimeInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			RemainingRoundTime = 300.0f;
		}

		/// <summary>
		/// Substracts elapsed game time from remaining game time and
		/// sends a message to all clients to syncronize their time if
		/// this input component belongs to server.
		/// </summary>
		/// <param name="gameObject">Current gameObject</param>
		/// <param name="gameTime">Game time</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) 
		{
			RemainingRoundTime -= ( float )gameTime.ElapsedGameTime.TotalSeconds;
			UpdateAllGameTimers();
		}

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and keyboard state.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="state">Current keyboard state.</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		/// <summary>
		/// Handles the input recieved based on the current game object state,
		/// current keyboard state and current game state.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) {}

		/// <summary>
		/// Returns the remaining game time in minutes.
		/// </summary>
		/// <returns>Returns the remaining game time in minutes.</returns>
		public override int GetRemainingMinutes()
		{
			return ( int )( RemainingRoundTime / 60 );
		}

		/// <summary>
		/// Returns the remaining seconds in a game round minute.
		/// </summary>
		/// <returns>Returns the remaining seconds in a game round minute.</returns>
		public override int GetRemainingSeconds()
		{
			return ( int )( RemainingRoundTime % 60 );
		}

		/// <summary>
		/// Sets the remaining round time to the specified time.
		/// </summary>
		/// <param name="remainingTime">Specified round time</param>
		public void SetRemainingRoundTime( float remainingTime )
		{
			RemainingRoundTime = remainingTime;
		}

		private void UpdateAllGameTimers()
		{
			if( handler.networkManager.IsHost() )
			{
				if( ( int )RemainingRoundTime >= 30 && ( int )RemainingRoundTime % 30 == 0 )
				{
					handler.networkManager.SendUpdateRemainingTimeMessage( RemainingRoundTime );
				}
			}
		}

		/// <summary>
		/// Checks if the the round time is depleated.
		/// </summary>
		/// <returns>bool indicating if game round is over.</returns>
		public bool IsRoundTimeOver()
		{
			return ( int )RemainingRoundTime <= 0;
		}

		private readonly StateHandler handler;

		///<value>The remaining round time</value>
		public float RemainingRoundTime { get; private set; }
	}
}