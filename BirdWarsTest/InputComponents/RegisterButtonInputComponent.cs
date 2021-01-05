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
			click += Register;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

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
					GetRegisterValues( gameState );
					click?.Invoke( this, registerEvents );
				}
			}
		}
		private void Register( Object sender, RegisterEventArgs registerEvents )
		{
			CheckRegisterInfo( handler, registerEvents );
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

		private void CheckRegisterInfo( StateHandler handler, RegisterEventArgs registerEvents )
		{
			CheckPasswords( handler, registerEvents );
			CheckEmail( handler, registerEvents );
			CheckUsername( handler, registerEvents );
			CheckLastNames( handler, registerEvents );
			CheckName( handler, registerEvents );
		}

		private void CheckName( StateHandler handler, RegisterEventArgs registerEvents )
		{
			if( !validator.IsNameValid( registerEvents.Name ) )
			{
				( ( UserRegistryState )handler.GetCurrentState() ).SetErrorMessage( 
					handler.StringManager.GetString( StringNames.NameInvalid ) );
			}
		}

		private void CheckLastNames( StateHandler handler, RegisterEventArgs registerEvents )
		{
			if( !validator.AreLastNamesValid( registerEvents.LastNames ) )
			{
				( ( UserRegistryState )handler.GetCurrentState() ).SetErrorMessage( 
					handler.StringManager.GetString( StringNames.LastNamesInvalid ) );
			}
		}

		private void CheckUsername( StateHandler handler, RegisterEventArgs registerEvents )
		{
			if( !validator.IsUsernameValid( registerEvents.Username ) )
			{
				( ( UserRegistryState )handler.GetCurrentState() ).SetErrorMessage( 
					handler.StringManager.GetString( StringNames.UsernameInvalid ) );
			}
		}

		private void CheckEmail( StateHandler handler, RegisterEventArgs registerEvents )
		{
			if( !validator.IsEmailValid( registerEvents.Email ) )
			{
				( ( UserRegistryState )handler.GetCurrentState() ).SetErrorMessage( 
					handler.StringManager.GetString( StringNames.EmailInvalid ) );
			}
		}

		private void CheckPasswords( StateHandler handler, RegisterEventArgs registerEvents )
		{
			if( !validator.IsPasswordValid( registerEvents.Name ) )
			{
				( ( UserRegistryState )handler.GetCurrentState() ).SetErrorMessage( 
					handler.StringManager.GetString( StringNames.PasswordInvalid ) );
			}
			if( !validator.IsPasswordValid( registerEvents.Name ) )
			{
				( ( UserRegistryState )handler.GetCurrentState() ).SetErrorMessage( 
					handler.StringManager.GetString( StringNames.PasswordsDoNotMatch ) );
			}
		}

		public event EventHandler< RegisterEventArgs > click;
		private RegisterEventArgs registerEvents;
		private StateHandler handler;
		private StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
	}
}