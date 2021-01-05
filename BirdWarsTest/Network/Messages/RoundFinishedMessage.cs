using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class RoundFinishedMessage : IGameMessage
	{
		public RoundFinishedMessage()
		{}

		public RoundFinishedMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.RoundFinishedMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage ) {}

		public void Encode( NetOutgoingMessage outgoingMessage ) {}
	}
}