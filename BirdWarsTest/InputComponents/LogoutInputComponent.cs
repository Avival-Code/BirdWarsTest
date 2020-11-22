using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.InputComponents
{
	class LogoutInputComponent : InputComponent
	{
		public LogoutInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			( ( MainMenuState )gameState ).NetworkManager.Logout();
			handler.ChangeState( StateTypes.LoginState );
		}

		private StateHandler handler;
	}
}