using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using BirdWarsTest.InputComponents.EventArguments;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	class SolicitPasswordResetInputComponent : InputComponent
	{
		public SolicitPasswordResetInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			codeEvents = new CodeEventArgs();
			validator = new StringValidator();
			click += SendPasswordChangeCode;
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
					codeEvents.Email = ( ( PasswordRecoveryState )gameState ).GameObjects[ 4 ].Input.GetText();
					( ( PasswordRecoveryState )gameState ).GameObjects[ 4 ].Input.ClearText();
					click?.Invoke( this, codeEvents );
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

		private StateHandler handler;
		private event EventHandler< CodeEventArgs > click;
		private CodeEventArgs codeEvents;
		private StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
	}
}