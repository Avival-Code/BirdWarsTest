using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using BirdWarsTest.InputComponents.EventArguments;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	public class SendChatMessageInputComponent : InputComponent
	{
		public SendChatMessageInputComponent( StateHandler handlerIn )
		{
			chatEvents = new ChatMessageArgs();
			handler = handlerIn;
			sentMessage = false;
			timer = 0;
			click += SendMessage;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) 
		{
			UpdateTimer();
			if( !sentMessage && state.IsKeyDown( Keys.Enter ) )
			{
				sentMessage = !sentMessage;
				chatEvents.Message = ( ( WaitingRoomState )gameState ).GameObjects[ 2 ].Input.GetTextWithoutVisualCharacter();
				( ( WaitingRoomState )gameState ).GameObjects[ 2 ].Input.ClearText();
				click?.Invoke( this, chatEvents );
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
					click?.Invoke( this, chatEvents );
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

		private event EventHandler< ChatMessageArgs > click;
		private ChatMessageArgs chatEvents;
		private StateHandler handler;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		private bool sentMessage;
		private int timer;
	}
}