using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace BirdWarsTest.InputComponents
{
	class LoginButtonInputComponent : InputComponent
	{
		public LoginButtonInputComponent()
		{
			isHovering = false;
			click += Login;
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state )
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			isHovering = false;
			if( mouseRectangle.Intersects( gameObject.getRectangle() ) )
			{
				isHovering = true;
				if( currentMouseState.LeftButton == ButtonState.Released && 
					previousMouseState.LeftButton == ButtonState.Pressed )
				{
					click?.Invoke( this, new EventArgs() );
				}
			}
		}

		private void Login( object sender, System.EventArgs e )
		{
			Console.WriteLine( "Clicked" );
		}

		private MouseState currentMouseState;
		private MouseState previousMouseState;
		public event EventHandler click;
		public bool clicked;
		private bool isHovering;
	}
}