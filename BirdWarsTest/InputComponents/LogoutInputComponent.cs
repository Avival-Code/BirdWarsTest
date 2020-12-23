using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	class LogoutInputComponent : InputComponent
	{
		public LogoutInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			( ( MainMenuState )gameState ).NetworkManager.Logout();
			handler.ChangeState( StateTypes.LoginState );
		}

		private StateHandler handler;
	}
}