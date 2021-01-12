/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component that sends a chat message to the server.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using BirdWarsTest.InputComponents.EventArguments;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component that sends a chat message to the server.
	/// </summary>
	public class SendChatMessageInputComponent : InputComponent
	{
		/// <summary>
		/// Creates default chat event arguments and data.
		/// </summary>
		/// <param name="handlerIn"></param>
		public SendChatMessageInputComponent( StateHandler handlerIn )
		{
			chatEvents = new ChatMessageArgs();
			handler = handlerIn;
			sentMessage = false;
			timer = 0;
			Click += SendMessage;
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
		/// Updates the message timer if a message has been sent and checks whether
		/// the user clicked on the objects button texture or pressed enter. If so, it gets the message
		/// information from it's respective game object and sends it to the server.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) 
		{
			UpdateTimer();
			if( !sentMessage && state.IsKeyDown( Keys.Enter ) )
			{
				sentMessage = !sentMessage;
				chatEvents.Message = ( ( WaitingRoomState )gameState ).GameObjects[ 2 ].Input.GetTextWithoutVisualCharacter();
				( ( WaitingRoomState )gameState ).GameObjects[ 2 ].Input.ClearText();
				Click?.Invoke( this, chatEvents );
			}

			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			if( mouseRectangle.Intersects( gameObject.GetRectangle() ) )
			{
				if( currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed )
				{
					chatEvents.Message = ( ( WaitingRoomState )gameState ).GameObjects[ 2 ].Input.GetText();
					( ( WaitingRoomState )gameState ).GameObjects[ 2 ].Input.ClearText();
					Click?.Invoke( this, chatEvents );
				}
			}
		}

		private void SendMessage( Object sender, ChatMessageArgs chatEvents )
		{
			if( !chatEvents.Message.Equals( "" ) )
			{
				handler.networkManager.SendChatMessage( chatEvents.Message );
				chatEvents.Message = "";
			}
		}

		private void UpdateTimer()
		{
			if( sentMessage )
			{
				timer += 1;
				if (timer >= 10)
				{
					timer = 0;
					sentMessage = !sentMessage;
				}
			}
		}

		private event EventHandler< ChatMessageArgs > Click;
		private ChatMessageArgs chatEvents;
		private readonly StateHandler handler;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		private bool sentMessage;
		private int timer;
	}
}