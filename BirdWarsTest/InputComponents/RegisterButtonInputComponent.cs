using BirdWarsTest.States;
using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents.EventArguments;
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
			handler = handlerIn;
			isHovering = false;
			click += Register;
		}

		private void Register( Object sender, RegisterEventArgs registerEvents )
		{
			if( IsPasswordValid() )
			{
				handler.networkManager.RegisterUser( registerEvents.name, registerEvents.lastNames,
													 registerEvents.username, registerEvents.email, registerEvents.password );
				ClearEvents();
			}
		}

		private bool IsPasswordValid()
		{
			return registerEvents.password.Equals( registerEvents.confirmPassword );
		}

		private void ClearEvents()
		{
			registerEvents.name = "";
			registerEvents.lastNames = "";
			registerEvents.username = "";
			registerEvents.email = "";
			registerEvents.password = "";
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			isHovering = false;
			if ( mouseRectangle.Intersects( gameObject.GetRectangle() ) )
			{
				isHovering = true;
				if( currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed )
				{
					registerEvents.name = ( ( UserRegistryState )gameState ).gameObjects[ 5 ].input.GetText();
					registerEvents.lastNames = ( ( UserRegistryState )gameState ).gameObjects[ 7 ].input.GetText();
					registerEvents.username = ( ( UserRegistryState )gameState ).gameObjects[ 9 ].input.GetText();
					registerEvents.email = ( ( UserRegistryState )gameState ).gameObjects[ 11 ].input.GetText();
					registerEvents.password = ( ( UserRegistryState )gameState ).gameObjects[ 13 ].input.GetText();
					registerEvents.confirmPassword = ( ( UserRegistryState )gameState ).gameObjects[ 15 ].input.GetText();
					click?.Invoke( this, registerEvents );
				}
			}
		}

		public event EventHandler< RegisterEventArgs > click;
		private RegisterEventArgs registerEvents;
		private StateHandler handler;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		public bool clicked;
		private bool isHovering;
	}
}
