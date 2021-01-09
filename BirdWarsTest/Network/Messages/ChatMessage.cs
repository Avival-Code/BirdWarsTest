using Lidgren.Network;

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
			SenderUsername = senderUsernameIn;
			Message = messageIn;
		}
		public GameMessageTypes messageType
		{
			get{ return GameMessageTypes.ChatMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			SenderUsername = incomingMessage.ReadString();
			Message = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( SenderUsername );
			outgoingMessage.Write( Message );
		}

		public string SenderUsername { get; private set; }
		public string Message { get; private set; }
	}
}