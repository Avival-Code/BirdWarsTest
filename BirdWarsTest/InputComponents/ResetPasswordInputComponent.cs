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
			isHovering = false;
			click += ChangePassword;
		}

		private void ChangePassword( object sender, PasswordChangeEventArgs events )
		{
			if( !string.IsNullOrEmpty( events.Code ) && !string.IsNullOrEmpty( events.Password ) &&
				validator.IsPasswordValid( events.Password ) )
			{
				handler.networkManager.UpdatePassword( events.Code, events.Password );
				events.ResetArgs();
			}
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			isHovering = false;
			if (mouseRectangle.Intersects(gameObject.GetRectangle()))
			{
				isHovering = true;
				if (currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed)
				{
					passwordEvents.Code = ( ( PasswordRecoveryState )gameState ).GameObjects[ 7 ].Input.GetText();
					passwordEvents.Password = ( ( PasswordRecoveryState )gameState ).GameObjects[ 9 ].Input.GetText();
					( ( PasswordRecoveryState )gameState ).GameObjects[ 7 ].Input.ClearText();
					( ( PasswordRecoveryState )gameState ).GameObjects[ 9 ].Input.ClearText();
					click?.Invoke( this, passwordEvents );
				}
			}
		}

		private StateHandler handler;
		private event EventHandler< PasswordChangeEventArgs > click;
		private PasswordChangeEventArgs passwordEvents;
		private StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		public bool clicked;
		private bool isHovering;
	}
}