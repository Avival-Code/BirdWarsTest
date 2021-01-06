using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class RoundFinishedMessage : IGameMessage
	{
		public RoundFinishedMessage( int remainingRoundTimeIn )
		{
			RemainingRoundTime = remainingRoundTimeIn;
		}

		public RoundFinishedMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.RoundFinishedMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage ) 
		{
			RemainingRoundTime = incomingMessage.ReadInt32();
		}

		public void Encode( NetOutgoingMessage outgoingMessage ) 
		{
			outgoingMessage.Write( RemainingRoundTime );
		}

		public int RemainingRoundTime { get; private set; }
	}
}