using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents.EventArguments;
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
			loginEvents = new LoginEventArgs();
			changeState = StateTypes.MainMenuState;
			handler = handlerIn;
			isHovering = false;
			click += Login;
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		private void Login( object sender, LoginEventArgs loginEvents )
		{
			handler.networkManager.Login( loginEvents.email, loginEvents.password );
			loginEvents.email = "";
			loginEvents.password = "";
			if( handler.networkManager.IsHost() )
			{
				if( ( ( ServerNetworkManager )handler.networkManager ).isLoggedIn )
				{
					handler.ChangeState( StateTypes.MainMenuState );
				}
			}
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state, LoginState loginState )
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			isHovering = false;
			if( mouseRectangle.Intersects( gameObject.GetRectangle() ) )
			{
				isHovering = true;
				if (currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed)
				{
					loginEvents.email = loginState.gameObjects[ 7 ].input.GetText();
					loginEvents.password = loginState.gameObjects[ 9 ].input.GetText();
					click?.Invoke( this, loginEvents );
				}
			}
		}

		private StateHandler handler;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		public event EventHandler< LoginEventArgs > click;
		private LoginEventArgs loginEvents;
		private StateTypes changeState;
		public bool clicked;
		private bool isHovering;
	}
}