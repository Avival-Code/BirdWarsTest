using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using BirdWarsTest.InputComponents.EventArguments;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	public class ResetPasswordInputComponent : InputComponent
	{
		public ResetPasswordInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			validator = new StringValidator();
			passwordEvents = new PasswordChangeEventArgs();
			click += ChangePassword;
		}

		private void ChangePassword( object sender, PasswordChangeEventArgs passwordEvents )
		{
			CheckPassword( passwordEvents );
			if( !string.IsNullOrEmpty( passwordEvents.Code ) && !string.IsNullOrEmpty( passwordEvents.Password ) &&
				validator.IsPasswordValid( passwordEvents.Password ) )
			{
				handler.networkManager.UpdatePassword( passwordEvents.Code, passwordEvents.Password );
				passwordEvents.ResetArgs();
			}
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

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
					passwordEvents.Code = ( ( PasswordRecoveryState )gameState ).GameObjects[ 7 ].Input.GetTextWithoutVisualCharacter();
					passwordEvents.Password = ( ( PasswordRecoveryState )gameState ).GameObjects[ 9 ].Input.GetTextWithoutVisualCharacter();
					( ( PasswordRecoveryState )gameState ).GameObjects[ 7 ].Input.ClearText();
					( ( PasswordRecoveryState )gameState ).GameObjects[ 9 ].Input.ClearText();
					click?.Invoke( this, passwordEvents );
				}
			}
		}

		private void CheckPassword( PasswordChangeEventArgs passwordEvents )
		{
			if( !validator.IsPasswordValid( passwordEvents.Password ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.PasswordInvalid ) );
			}
		}

		private StateHandler handler;
		private event EventHandler< PasswordChangeEventArgs > click;
		private PasswordChangeEventArgs passwordEvents;
		private StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
	}
}