using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class ExitWaitingRoomMessage : IGameMessage
	{
		public ExitWaitingRoomMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public ExitWaitingRoomMessage()
		{

		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.ExitWaitingRoomMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{

		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{

		}
	}
}