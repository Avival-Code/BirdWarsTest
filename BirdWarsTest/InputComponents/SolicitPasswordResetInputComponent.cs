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
			isHovering = false;
			click += SendPasswordChangeCode;
		}

		private void SendPasswordChangeCode( object sender, CodeEventArgs events )
		{
			if( !string.IsNullOrEmpty( events.Email ) && validator.IsEmailValid( events.Email ) )
			{
				handler.networkManager.SendPasswordChangeMessage( events.Email );
				events.ResetArgs();
			}
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			isHovering = false;
			if( mouseRectangle.Intersects( gameObject.GetRectangle() ) )
			{
				isHovering = true;
				if( currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed )
				{
					codeEvents.Email = ( ( PasswordRecoveryState )gameState ).GameObjects[ 4 ].input.GetText();
					( ( PasswordRecoveryState )gameState ).GameObjects[ 4 ].input.ClearText();
					click?.Invoke( this, codeEvents );
				}
			}
		}

		private StateHandler handler;
		private event EventHandler< CodeEventArgs > click;
		private CodeEventArgs codeEvents;
		private StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		public bool clicked;
		private bool isHovering;
	}
}
