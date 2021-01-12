/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component used to send a reset password request to
the server.
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
	/// Input component used to send a reset password requiest to
	/// the server.
	/// </summary>
	public class ResetPasswordInputComponent : InputComponent
	{
		/// <summary>
		/// Sets the statehandler reference and creates default password
		/// event arguments.
		/// </summary>
		/// <param name="handlerIn">Game statehandler</param>
		public ResetPasswordInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			validator = new StringValidator();
			passwordEvents = new PasswordChangeEventArgs();
			Click += ChangePassword;
		}

		private void ChangePassword( object sender, PasswordChangeEventArgs passwordEvents )
		{
			CheckPasswordArgs( passwordEvents );
			if( !string.IsNullOrEmpty( passwordEvents.Code ) && !string.IsNullOrEmpty( passwordEvents.Password ) &&
				!string.IsNullOrEmpty( passwordEvents.Email ) && validator.IsPasswordValid( passwordEvents.Password ) )
			{
				handler.networkManager.UpdatePassword( passwordEvents.Code, passwordEvents.Email, passwordEvents.Password );
				passwordEvents.ResetArgs();
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
		/// Checks if user clicked on button texture and if so, gets the password event
		/// arguments from their respective gameObjects and sends an update password request 
		/// to server.
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
					passwordEvents.Email = ( ( PasswordRecoveryState )gameState ).GameObjects[ 4 ].Input.GetTextWithoutVisualCharacter();
					passwordEvents.Code = ( ( PasswordRecoveryState )gameState ).GameObjects[ 7 ].Input.GetTextWithoutVisualCharacter();
					passwordEvents.Password = ( ( PasswordRecoveryState )gameState ).GameObjects[ 9 ].Input.GetTextWithoutVisualCharacter();
					Click?.Invoke( this, passwordEvents );
				}
			}
		}

		private void CheckPasswordArgs( PasswordChangeEventArgs passwordEvents )
		{
			CheckPassword( passwordEvents );
			CheckEmail( passwordEvents );
		}

		private void CheckEmail( PasswordChangeEventArgs passwordEvents )
		{
			if( !validator.IsEmailValid( passwordEvents.Email ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.EmailInvalid ) );
			}
		}

		private void CheckPassword( PasswordChangeEventArgs passwordEvents )
		{
			if( !validator.IsPasswordValid( passwordEvents.Password ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.PasswordInvalid ) );
			}
		}

		private readonly StateHandler handler;
		private event EventHandler< PasswordChangeEventArgs > Click;
		private PasswordChangeEventArgs passwordEvents;
		private readonly StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
	}
}