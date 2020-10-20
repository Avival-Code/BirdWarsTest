using System;
using System.Collections.Generic;
using System.Text;
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	class PlayerInputComponent: InputComponent
	{
		public PlayerInputComponent()
		{
			moveUpButton = new MoveUpCommand();
			moveDownButton = new MoveDownCommand();
			moveLeftButton = new MoveLeftCommand();
			moveRightButton = new MoveRightCommand();
		}

		public override void HandleInput( KeyboardState state, GameObject gameObject )
		{
			if ( state.IsKeyDown( Keys.Up ) ) moveUpButton.execute( gameObject );
			else if ( state.IsKeyDown( Keys.Down ) ) moveDownButton.execute( gameObject );
			else if ( state.IsKeyDown( Keys.Left ) ) moveLeftButton.execute( gameObject );
			else if ( state.IsKeyDown( Keys.Right ) ) moveRightButton.execute( gameObject );
		}
	}
}
