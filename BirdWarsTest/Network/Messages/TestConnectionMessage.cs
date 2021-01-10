using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class TestConnectionMessage : IGameMessage
	{
		public TestConnectionMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public TestConnectionMessage( string resultIn )
		{
			Result = resultIn;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.TestConnectionMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			Result = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( Result );
		}

		public string Result { get; private set; }
	}
}