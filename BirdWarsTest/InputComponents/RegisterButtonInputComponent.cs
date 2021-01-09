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
			CheckRegisterInfo( registerEvents );
			if( validator.AreRegisterArgsValid( registerEvents ) )
			{
				handler.networkManager.RegisterUser( registerEvents.Name, registerEvents.LastNames,
													 registerEvents.Username, registerEvents.Email, registerEvents.Password );
				registerEvents.ClearRegisterArgs();
			}
		}

		private void GetRegisterValues( GameState gameState )
		{
			registerEvents.Name = ( ( UserRegistryState )gameState ).GameObjects[ 6 ].Input.GetTextWithoutVisualCharacter();
			registerEvents.LastNames = ( ( UserRegistryState )gameState ).GameObjects[ 8 ].Input.GetTextWithoutVisualCharacter();
			registerEvents.Username = ( ( UserRegistryState )gameState ).GameObjects[ 10 ].Input.GetTextWithoutVisualCharacter();
			registerEvents.Email = ( ( UserRegistryState )gameState ).GameObjects[ 12 ].Input.GetTextWithoutVisualCharacter();
			registerEvents.Password = ( ( UserRegistryState )gameState ).GameObjects[ 14 ].Input.GetTextWithoutVisualCharacter();
			registerEvents.ConfirmPassword = ( ( UserRegistryState )gameState ).GameObjects[ 16 ].Input.GetTextWithoutVisualCharacter();
		}

		private void CheckRegisterInfo( RegisterEventArgs registerEvents )
		{
			CheckPasswords( registerEvents );
			CheckEmail( registerEvents );
			CheckUsername( registerEvents );
			CheckLastNames( registerEvents );
			CheckName( registerEvents );
		}

		private void CheckName( RegisterEventArgs registerEvents )
		{
			if( !validator.IsNameValid( registerEvents.Name ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.NameInvalid ) );
			}
		}

		private void CheckLastNames( RegisterEventArgs registerEvents )
		{
			if( !validator.AreLastNamesValid( registerEvents.LastNames ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.LastNamesInvalid ) );
			}
		}

		private void CheckUsername( RegisterEventArgs registerEvents )
		{
			if( !validator.IsUsernameValid( registerEvents.Username ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.UsernameInvalid ) );
			}
		}

		private void CheckEmail( RegisterEventArgs registerEvents )
		{
			if( !validator.IsEmailValid( registerEvents.Email ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.EmailInvalid ) );
			}
		}

		private void CheckPasswords( RegisterEventArgs registerEvents )
		{
			if( !validator.IsPasswordValid( registerEvents.Name ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.PasswordInvalid ) );
			}
			if( !validator.IsPasswordValid( registerEvents.Name ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.PasswordsDoNotMatch ) );
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