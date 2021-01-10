using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using BirdWarsTest.InputComponents.EventArguments;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	public class ConnectToServerInputComponent : InputComponent
	{
		public ConnectToServerInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			serverEvents = new ServerChangeArgs();
			validator = new StringValidator();
			click += ConnectToServer;
		}

		private void ConnectToServer( object sender, ServerChangeArgs serverEvents )
		{
			CheckServerArgs( serverEvents );
			if( !string.IsNullOrEmpty( serverEvents.Address ) && !string.IsNullOrEmpty( serverEvents.Port ) )
			{
				handler.networkManager.ConnectToSpecificServer( handler, serverEvents.Address, serverEvents.Port );
			}
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

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
					click?.Invoke( this, serverEvents );
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

		private StateHandler handler;
		private event EventHandler< ServerChangeArgs > click;
		private ServerChangeArgs serverEvents;
		private StringValidator validator;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
	}
}