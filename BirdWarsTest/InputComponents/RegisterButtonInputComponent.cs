/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component used to send user information to the server
so that a new user and account can be created.
*********************************************/
using BirdWarsTest.States;
using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents.EventArguments;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component used to send user information to the server
	/// so that a new user and account can be created.
	/// </summary>
	public class RegisterButtonInputComponent : InputComponent
	{
		/// <summary>
		/// Sets the statehandler reference and creates default register
		/// event arguments.
		/// </summary>
		/// <param name="handlerIn"></param>
		public RegisterButtonInputComponent( StateHandler handlerIn )
		{
			registerEvents = new RegisterEventArgs();
			validator = new StringValidator();
			handler = handlerIn;
			Click += Register;
		}

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and game time.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="gameTime">Current game time.</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and keyboard state.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="state">Current keyboard state.</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		/// <summary>
		/// Checks if the user clicked on the gameObjects texture and if so
		/// calls the network manager's RegisterUser method.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
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
					Click?.Invoke( this, registerEvents );
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
			if( !validator.IsPasswordValid( registerEvents.Password ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.PasswordInvalid ) );
			}
			if( !validator.IsPasswordAndConfirmEqual( registerEvents.Password, registerEvents.ConfirmPassword  ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.PasswordsDoNotMatch ) );
			}
		}

		///<value>Input component event handler</value>
		public event EventHandler< RegisterEventArgs > Click;
		private RegisterEventArgs registerEvents;
		private readonly StateHandler handler;
		private readonly StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
	}
}