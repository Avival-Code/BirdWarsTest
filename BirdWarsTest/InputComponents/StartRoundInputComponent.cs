using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	class StartRoundInputComponent : InputComponent
	{
		public StartRoundInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			clicked = false;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state )
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			if( mouseRectangle.Intersects( gameObject.GetRectangle() ) && !clicked )
			{
				if( currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed )
				{
					clicked = true;
					handler.networkManager.StartRound();
				}
			}
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) {}

		private StateHandler handler;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		private bool clicked;
	}
}