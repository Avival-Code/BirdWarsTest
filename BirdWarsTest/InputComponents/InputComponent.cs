﻿using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	abstract class InputComponent
	{
		abstract public void HandleInput( GameObject gameObject, KeyboardState state );

		abstract public void HandleInput( GameObject gameObject, KeyboardState state, LoginState loginState );

		public virtual string GetText()
		{
			return "";
		}

		protected Command moveUpButton = null;
		protected Command moveDownButton = null;
		protected Command moveLeftButton = null;
		protected Command moveRightButton = null;
	}
}
