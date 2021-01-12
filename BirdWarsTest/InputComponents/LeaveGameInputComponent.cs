/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component that sends a message to the server
saying the client will leave the game round, then changes
state to the target state.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component that sends a message to the server
	/// saying the client will leave the game round, then changes
	/// state to the target state.
	/// </summary>
	public class LeaveGameInputComponent : InputComponent
	{
		/// <summary>
		/// Default constructor, sets the target state and game statehandler
		/// reference. Adds method to event handler.
		/// </summary>
		/// <param name="handlerIn">Game statehandler</param>
		/// <param name="state">The target state.</param>
		public LeaveGameInputComponent( StateHandler handlerIn, StateTypes state )
		{
			handler = handlerIn;
			Click += ToOtherScreen;
			stateChange = state;
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
		public override void HandleInput( GameObject gameObject, KeyboardState state ) 	{}


		/// <summary>
		/// Handes the input recieved based on the current game object state,
		/// current keyboard state and current game state.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			if( mouseRectangle.Intersects( gameObject.GetRectangle() ) )
			{
				if( currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed )
				{
					( ( WaitingRoomState )gameState ).NetworkManager.LeaveRound();
					Click?.Invoke( this, new EventArgs() );
				}
			}
		}

		private void ToOtherScreen( Object sender, System.EventArgs e )
		{
			handler.ChangeState( stateChange );
		}

		private readonly StateHandler handler;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		private event EventHandler Click;
		private readonly StateTypes stateChange;
	}
}