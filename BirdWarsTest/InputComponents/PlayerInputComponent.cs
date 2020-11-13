using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	class PlayerInputComponent : InputComponent
	{
		public PlayerInputComponent()
		{
			moveUpButton = new MoveUpCommand();
			moveDownButton = new MoveDownCommand();
			moveLeftButton = new MoveLeftCommand();
			moveRightButton = new MoveRightCommand();
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state )
		{
			DiagonalInput( gameObject, state );
			NormalInput( gameObject, state );
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state, LoginState loginState ) 
		{
			HandleInput( gameObject, state );
		}

		public void NormalInput( GameObject gameObject, KeyboardState state )
		{
			if( state.IsKeyDown( Keys.Up ) ) moveUpButton.Execute( gameObject );
			else if( state.IsKeyDown( Keys.Down ) ) moveDownButton.Execute( gameObject );
			else if( state.IsKeyDown( Keys.Left ) ) moveLeftButton.Execute( gameObject );
			else if( state.IsKeyDown( Keys.Right ) ) moveRightButton.Execute( gameObject );
		}

		public void DiagonalInput( GameObject gameObject, KeyboardState state )
		{
			if( state.IsKeyDown( Keys.Up ) && state.IsKeyDown( Keys.Right ) )
			{
				moveUpButton.Execute( gameObject );
				moveRightButton.Execute( gameObject );
			}
			else if( state.IsKeyDown( Keys.Up ) && state.IsKeyDown( Keys.Left ) )
			{
				moveUpButton.Execute( gameObject );
				moveLeftButton.Execute( gameObject );
			}
			else if( state.IsKeyDown( Keys.Down ) && state.IsKeyDown( Keys.Right ) )
			{
				moveDownButton.Execute( gameObject );
				moveRightButton.Execute( gameObject );
			}
			else if( state.IsKeyDown( Keys.Down ) && state.IsKeyDown( Keys.Left ) )
			{
				moveDownButton.Execute( gameObject );
				moveLeftButton.Execute( gameObject );
			}
		}
	}
}
