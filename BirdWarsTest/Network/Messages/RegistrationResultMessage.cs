using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class RegistrationResultMessage : IGameMessage
	{
		public RegistrationResultMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public RegistrationResultMessage( string messageIn )
		{
			Message = messageIn;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.RegistrationResultMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			Message = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( Message );
		}

		public string Message { get; private set; }
	}
}