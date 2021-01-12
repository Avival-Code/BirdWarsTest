/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component used to login to a user account.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents.EventArguments;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using BirdWarsTest.States;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component used to login to a user account.
	/// </summary>
	public class LoginButtonInputComponent : InputComponent
	{
		/// <summary>
		/// Sets the statehandler reference, creates the default login
		/// event arguments and string validator.
		/// </summary>
		/// <param name="handlerIn">Game statehandler</param>
		public LoginButtonInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			loginEvents = new LoginEventArgs();
			validator = new StringValidator();
			Click += Login;
		}

		private void Login( object sender, LoginEventArgs loginEvents )
		{
			CheckLoginInfo( loginEvents );
			if( !string.IsNullOrEmpty( loginEvents.Password ) && validator.AreLoginArgsValid( loginEvents ) )
			{
				handler.networkManager.Login( loginEvents.Email, loginEvents.Password );
				loginEvents.ResetArgs();
			}
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
		public override void HandleInput(GameObject gameObject, KeyboardState state) {}

		/// <summary>
		/// Checks if the user clicked on the object's button texture and if so
		/// gets the login arguments from their respective objects and calls the network
		/// manager Login method.
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
					loginEvents.Email = ( ( LoginState ) gameState ).GameObjects[ 7 ].Input.GetTextWithoutVisualCharacter();
					loginEvents.Password = ( ( LoginState ) gameState ).GameObjects[ 9 ].Input.GetTextWithoutVisualCharacter();
					Click?.Invoke( this, loginEvents );
				}
			}
		}

		private void CheckLoginInfo( LoginEventArgs loginEvents )
		{
			CheckPassword( loginEvents );
			CheckEmail( loginEvents );
		}

		private void CheckEmail( LoginEventArgs loginEvents )
		{
			if( !validator.IsEmailValid( loginEvents.Email ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.EmailInvalid ) );
			}
		}

		private void CheckPassword( LoginEventArgs loginEvents )
		{
			if( !validator.IsPasswordValid( loginEvents.Password ) )
			{
				handler.GetCurrentState().SetErrorMessage( handler.StringManager.GetString( StringNames.PasswordInvalid ) );
			}
		}

		private readonly StateHandler handler;
		private event EventHandler< LoginEventArgs > Click;
		private LoginEventArgs loginEvents;
		private readonly StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
	}
}