/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Starts the process connect to a remote server.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using BirdWarsTest.InputComponents.EventArguments;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Starts the process connect to a remote server.
	/// </summary>
	public class ConnectToServerInputComponent : InputComponent
	{
		/// <summary>
		/// Sets the statehandler and creates empty server arguments.
		/// </summary>
		/// <param name="handlerIn"></param>
		public ConnectToServerInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			serverEvents = new ServerChangeArgs();
			validator = new StringValidator();
			Click += ConnectToServer;
		}

		private void ConnectToServer( object sender, ServerChangeArgs serverEvents )
		{
			CheckServerArgs( serverEvents );
			if( !string.IsNullOrEmpty( serverEvents.Address ) && !string.IsNullOrEmpty( serverEvents.Port ) &&
				validator.IsAddressValid( serverEvents.Address ) && validator.IsPortValid( serverEvents.Port ) )
			{
				handler.networkManager.ConnectToSpecificServer( handler, serverEvents.Address, serverEvents.Port );
			}
		}

		/// <summary>
		/// Handles the current input on every frame.
		/// </summary>
		/// <param name="gameObject">the GameObject</param>
		/// <param name="gameTime">Game time</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		/// <summary>
		/// Handles the current input on every frame.
		/// </summary>
		/// <param name="gameObject">the GameObject</param>
		/// <param name="state">current keyboard state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		/// <summary>
		/// Handles the necessary information to determine if the 
		/// user has clicked on the button.
		/// </summary>
		/// <param name="gameObject">The gameObject</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) 
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var mouseRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);

			if( mouseRectangle.Intersects( gameObject.GetRectangle() ) )
			{
				if (currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed)
				{
					serverEvents.Address = ( ( OptionsState )gameState ).GameObjects[ 4 ].Input.GetTextWithoutVisualCharacter();
					serverEvents.Port = ( ( OptionsState )gameState ).GameObjects[ 6 ].Input.GetTextWithoutVisualCharacter();
					Click?.Invoke( this, serverEvents );
				}
			}
		}

		private void CheckServerArgs( ServerChangeArgs serverEvents )
		{
			CheckPort( serverEvents.Port );
			CheckAddress( serverEvents.Address );
		}

		private void CheckAddress( string address )
		{
			if( !validator.IsAddressValid( address ) )
			{
				( ( OptionsState )handler.GetCurrentState() ).SetErrorMessage( 
								  handler.StringManager.GetString( StringNames.AddressInvalid ) );
			}
		}

		private void CheckPort( string port )
		{
			if( !validator.IsPortValid( port ) )
			{
				( ( OptionsState )handler.GetCurrentState() ).SetErrorMessage( 
								  handler.StringManager.GetString( StringNames.PortInvalid ) );
			}
		}

		private readonly StateHandler handler;
		private event EventHandler< ServerChangeArgs > Click;
		private ServerChangeArgs serverEvents;
		private readonly StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
	}
}