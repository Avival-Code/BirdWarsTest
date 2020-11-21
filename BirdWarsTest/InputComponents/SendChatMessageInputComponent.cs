using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using BirdWarsTest.InputComponents.EventArguments;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

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

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) 
		{
			if( !sentMessage && state.IsKeyDown( Keys.Enter ) )
			{
				sentMessage = !sentMessage;
				chatEvents.message = ( ( WaitingRoomState )gameState ).GameObjects[ 2 ].input.GetText();
				( ( WaitingRoomState )gameState ).GameObjects[ 2 ].input.ClearText();
				click?.Invoke( this, chatEvents );
			}
			UpdateTimer();
		}

		private void SendMessage( Object sender, ChatMessageArgs chatEvents )
		{
			if( !chatEvents.message.Equals( "" ) )
			{
				handler.networkManager.SendChatMessage(chatEvents.message);
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
		private bool sentMessage;
		private int timer;
	}
}
