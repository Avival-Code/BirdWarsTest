using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class PasswordResetResultMessage : IGameMessage
	{
		public PasswordResetResultMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public PasswordResetResultMessage( string resultIn )
		{
			Result = resultIn;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.PasswordResetResultMessage; }
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