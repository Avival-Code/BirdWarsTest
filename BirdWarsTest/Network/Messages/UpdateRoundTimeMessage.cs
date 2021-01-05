using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class UpdateRoundTimeMessage : IGameMessage
	{
		public UpdateRoundTimeMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public UpdateRoundTimeMessage( float remainingRoundTime )
		{
			MessageTime = NetTime.Now;
			RemainingRoundTime = remainingRoundTime;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.UpdateRoundTimeMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			MessageTime = incomingMessage.ReadDouble();
			RemainingRoundTime = incomingMessage.ReadFloat();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( MessageTime );
			outgoingMessage.Write( RemainingRoundTime );
		}

		public double MessageTime { get; private set; }
		public float RemainingRoundTime { get; private set; }
	}
}