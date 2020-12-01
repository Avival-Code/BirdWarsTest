using BirdWarsTest.States;
using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents.EventArguments;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	class RegisterButtonInputComponent : InputComponent
	{
		public RegisterButtonInputComponent( StateHandler handlerIn )
		{
			registerEvents = new RegisterEventArgs();
			validator = new StringValidator();
			handler = handlerIn;
			isHovering = false;
			click += Register;
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
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
					GetRegisterValues( gameState );
					click?.Invoke( this, registerEvents );
				}
			}
		}
		private void Register( Object sender, RegisterEventArgs registerEvents )
		{
			if( validator.AreRegisterArgsValid( registerEvents ) )
			{
				handler.networkManager.RegisterUser( registerEvents.Name, registerEvents.LastNames,
													 registerEvents.Username, registerEvents.Email, registerEvents.Password );
				registerEvents.ClearRegisterArgs();
			}
		}

		private void GetRegisterValues( GameState gameState )
		{
			registerEvents.Name = ( ( UserRegistryState )gameState ).GameObjects[ 6 ].Input.GetText();
			registerEvents.LastNames = ( ( UserRegistryState )gameState ).GameObjects[ 8 ].Input.GetText();
			registerEvents.Username = ( ( UserRegistryState )gameState ).GameObjects[ 10 ].Input.GetText();
			registerEvents.Email = ( ( UserRegistryState )gameState ).GameObjects[ 12 ].Input.GetText();
			registerEvents.Password = ( ( UserRegistryState )gameState ).GameObjects[ 14 ].Input.GetText();
			registerEvents.ConfirmPassword = ( ( UserRegistryState )gameState ).GameObjects[ 16 ].Input.GetText();
		}

		public event EventHandler< RegisterEventArgs > click;
		private RegisterEventArgs registerEvents;
		private StateHandler handler;
		private StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		public bool clicked;
		private bool isHovering;
	}
}