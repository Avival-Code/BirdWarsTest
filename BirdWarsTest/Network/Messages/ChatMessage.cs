using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	class ChatMessage : IGameMessage
	{
		public ChatMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public ChatMessage( string senderUsernameIn, string messageIn )
		{
			senderUsername = senderUsernameIn;
			message = messageIn;
		}
		public GameMessageTypes messageType
		{
			get{ return GameMessageTypes.ChatMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			senderUsername = incomingMessage.ReadString();
			message = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( senderUsername );
			outgoingMessage.Write( message );
		}

		public string Message { get; }
		public string SenderUsername { get; }

		private string senderUsername;
		private string message;
	}
}
