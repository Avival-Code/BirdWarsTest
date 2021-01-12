/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Changes the current state to the state specified on
creation.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Changes the current state to the state specified on
	/// creation.
	/// </summary>
	public class ButtonChangeStateInputComponent : InputComponent
	{
		/// <summary>
		/// Sets the statehandler reference and the target state
		/// of the state change.
		/// </summary>
		/// <param name="handlerIn">Game statehandler</param>
		/// <param name="state">specified state</param>
		public ButtonChangeStateInputComponent( StateHandler handlerIn, StateTypes state )
		{
			handler = handlerIn;
			Click += ToOtherScreen;
			stateChange = state;
		}

		/// <summary>
		/// Handles the necessary information to determine if the 
		/// user has clicked on the button.
		/// </summary>
		/// <param name="gameObject">The gameObject</param>
		/// <param name="state">current keyboard state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state )
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			if( mouseRectangle.Intersects( gameObject.GetRectangle() ) )
			{
				if( currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed )
				{
					Click?.Invoke( this, new EventArgs() );
				}
			}
		}

		/// <summary>
		/// Handles the current input on every frame.
		/// </summary>
		/// <param name="gameObject">the GameObject</param>
		/// <param name="gameTime">Game time</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		/// <summary>
		/// Handles the current input on every frame.
		/// </summary>
		/// <param name="gameObject">the GameObject</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			HandleInput( gameObject, state );
		}

		private void ToOtherScreen( Object sender, System.EventArgs e )
		{
			handler.ChangeState( stateChange );
		}

		private readonly StateHandler handler;
		private event EventHandler Click;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		private readonly StateTypes stateChange;
	}
}