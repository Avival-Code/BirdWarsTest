using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	class LeaveRoundMessage : IGameMessage
	{
		public LeaveRoundMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public LeaveRoundMessage( string username_In )
		{
			username = username_In;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.LeaveRoundMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			username = incomingMessage.ReadString();
		}

		public void Encode (NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( username );
		}

		private string username;
	}
}