using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class SolicitPasswordResetMessage : IGameMessage
	{
		public SolicitPasswordResetMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public SolicitPasswordResetMessage( string emailIn )
		{
			Email = emailIn;
		}

		public GameMessageTypes messageType 
		{ 
			get { return GameMessageTypes.SolicitPasswordResetMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			Email = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( Email );
		}

		public string Email { get; private set; }
	}
}