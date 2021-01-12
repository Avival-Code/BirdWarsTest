/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component that sends a message to the server so that
a code is sent via email to the user for a password change.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using BirdWarsTest.InputComponents.EventArguments;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component that sends a message to the server so that
	/// a code is sent via email to the user for a password change.
	/// </summary>
	public class SolicitPasswordResetInputComponent : InputComponent
	{
		/// <summary>
		/// Sets the staehandler reference and creates default code event 
		/// arguments.
		/// </summary>
		/// <param name="handlerIn">Game statehandler</param>
		public SolicitPasswordResetInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			codeEvents = new CodeEventArgs();
			validator = new StringValidator();
			Click += SendPasswordChangeCode;
		}

		private void SendPasswordChangeCode( object sender, CodeEventArgs codeEvents )
		{
			CheckEmail( codeEvents );
			if( !string.IsNullOrEmpty( codeEvents.Email ) && validator.IsEmailValid( codeEvents.Email ) )
			{
				handler.networkManager.SendPasswordChangeMessage( codeEvents.Email );
				codeEvents.ResetArgs();
			}
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
		/// Checks if user clicked on object's button texture and if so, sends
		/// a solicit password change emssage to server.
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
					codeEvents.Email = ( ( PasswordRecoveryState )gameState ).GameObjects[ 4 ].Input.GetTextWithoutVisualCharacter();
					Click?.Invoke( this, codeEvents );
				}
			}
		}

		private void CheckEmail( CodeEventArgs codeEvents )
		{
			if( !validator.IsEmailValid( codeEvents.Email ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.EmailInvalid ) );
			}
		}

		private readonly StateHandler handler;
		private event EventHandler< CodeEventArgs > Click;
		private CodeEventArgs codeEvents;
		private readonly StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
	}
}