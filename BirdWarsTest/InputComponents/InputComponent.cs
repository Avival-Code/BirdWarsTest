using System;
using System.Collections.Generic;
using System.Text;
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	abstract class InputComponent
	{
		abstract public void HandleInput( KeyboardState state, GameObject gameObject );

		protected Command moveUpButton = null;
		protected Command moveDownButton = null;
		protected Command moveLeftButton = null;
		protected Command moveRightButton = null;
	}
}
