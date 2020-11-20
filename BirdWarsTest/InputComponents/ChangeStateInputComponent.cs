using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	class ChangeStateInputComponent : InputComponent
	{
		public ChangeStateInputComponent( StateHandler handlerIn, StateTypes state )
		{
			handler = handlerIn;
			isHovering = false;
			click += ToOtherScreen;
			stateChange = state;
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state )
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
					click?.Invoke(this, new EventArgs());
				}
			}
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			HandleInput( gameObject, state );
		}

		private void ToOtherScreen( Object sender, System.EventArgs e )
		{
			handler.ChangeState( stateChange );
		}

		private StateHandler handler;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		public event EventHandler click;
		private StateTypes stateChange;
		public bool clicked;
		private bool isHovering;
	}
}