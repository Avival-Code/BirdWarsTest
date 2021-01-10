using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class BanPlayerMessage : IGameMessage
	{
		public BanPlayerMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public BanPlayerMessage( string usernameIn )
		{
			Username = usernameIn;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.BanPlayerMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			Username = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( Username );
		}

		public string Username { get; private set; }
	}
}