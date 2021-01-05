using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents.EventArguments;
using BirdWarsTest.Utilities;
using BirdWarsTest.Network;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using BirdWarsTest.States;

namespace BirdWarsTest.InputComponents
{
	class LoginButtonInputComponent : InputComponent
	{
		public LoginButtonInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			loginEvents = new LoginEventArgs();
			validator = new StringValidator();
			click += Login;
		}

		private void Login( object sender, LoginEventArgs loginEvents )
		{
			CheckLoginInfo( loginEvents );
			if( !string.IsNullOrEmpty( loginEvents.Password ) && validator.AreLoginArgsValid( loginEvents ) )
			{
				handler.networkManager.Login( loginEvents.Email, loginEvents.Password );
				loginEvents.ResetArgs();
				HostChangeState();
			}
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput(GameObject gameObject, KeyboardState state) {}

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
					loginEvents.Email = ( ( LoginState ) gameState ).GameObjects[ 7 ].Input.GetText();
					loginEvents.Password = ( ( LoginState ) gameState ).GameObjects[ 9 ].Input.GetText();
					click?.Invoke( this, loginEvents );
				}
			}
		}

		private void HostChangeState()
		{
			if( handler.networkManager.IsHost() )
			{
				if( ( ( ServerNetworkManager )handler.networkManager ).UserSession.IsLoggedIn )
				{
					handler.ChangeState( StateTypes.MainMenuState );
				}
				else
				{
					handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.LoginDenied ) );
				}
			}
		}

		private void CheckLoginInfo( LoginEventArgs loginEvents )
		{
			CheckPassword( loginEvents );
			CheckEmail( loginEvents );
		}

		private void CheckEmail( LoginEventArgs loginEvents )
		{
			if( !validator.IsEmailValid( loginEvents.Email ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.EmailInvalid ) );
			}
		}

		private void CheckPassword( LoginEventArgs loginEvents )
		{
			if( !validator.IsPasswordValid( loginEvents.Password ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.PasswordInvalid ) );
			}
		}

		private StateHandler handler;
		private event EventHandler< LoginEventArgs > click;
		private LoginEventArgs loginEvents;
		private StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
	}
}