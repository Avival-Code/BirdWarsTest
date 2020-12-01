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
			isHovering = false;
			click += Login;
		}

		private void Login( object sender, LoginEventArgs loginEvents )
		{
			if( !string.IsNullOrEmpty( loginEvents.Password ) && validator.AreLoginArgsValid( loginEvents ) )
			{
				handler.networkManager.Login( loginEvents.Email, loginEvents.Password );
				loginEvents.ResetArgs();
				HostChangeState();
			}
		}

		public override void HandleInput(GameObject gameObject, KeyboardState state) { }

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
				if( ( ( ServerNetworkManager )handler.networkManager ).userSession.IsLoggedIn )
				{
					handler.ChangeState( StateTypes.MainMenuState );
				}
			}
		}

		private StateHandler handler;
		private event EventHandler< LoginEventArgs > click;
		private LoginEventArgs loginEvents;
		private StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		public bool clicked;
		private bool isHovering;
	}
}