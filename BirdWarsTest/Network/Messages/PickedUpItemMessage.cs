using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class PickedUpItemMessage : IGameMessage
	{
		public PickedUpItemMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public PickedUpItemMessage( int itemIndexIn )
		{
			ItemIndex = itemIndexIn;
		}

		public GameMessageTypes messageType
		{
			get{ return GameMessageTypes.PickedUpItemMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			ItemIndex = incomingMessage.ReadInt32();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( ItemIndex );
		}

		public int ItemIndex { get; private set; }
	}
}