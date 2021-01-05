using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	class LeaveGameInputComponent : InputComponent
	{
		public LeaveGameInputComponent( StateHandler handlerIn, StateTypes state )
		{
			handler = handlerIn;
			click += ToOtherScreen;
			stateChange = state;
		}
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) 	{}

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
					( ( WaitingRoomState )gameState ).NetworkManager.LeaveRound();
					click?.Invoke( this, new EventArgs() );
				}
			}
		}

		private void ToOtherScreen( Object sender, System.EventArgs e )
		{
			handler.ChangeState( stateChange );
		}

		private StateHandler handler;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		private event EventHandler click;
		private StateTypes stateChange;
	}
}