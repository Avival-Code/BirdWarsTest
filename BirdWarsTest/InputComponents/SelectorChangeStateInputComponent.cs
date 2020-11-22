using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.InputComponents
{
	class SelectorChangeStateInputComponent : InputComponent
	{
		public SelectorChangeStateInputComponent( StateHandler handlerIn, StateTypes changeStateIn )
		{
			handler = handlerIn;
			changeState = changeStateIn;
		}
		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			handler.ChangeState( changeState );
		}

		private StateHandler handler;
		private StateTypes changeState;
	}
}