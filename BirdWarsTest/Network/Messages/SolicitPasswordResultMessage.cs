using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class SolicitPasswordResultMessage : IGameMessage
	{
		public SolicitPasswordResultMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public SolicitPasswordResultMessage( string result )
		{
			Message = result;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.SolicitPasswordResultMessage; }
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