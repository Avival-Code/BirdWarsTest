using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class LoginRequestMessage : IGameMessage
	{
		public LoginRequestMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public LoginRequestMessage( string emailIn, string passwordIn )
		{
			Email = emailIn;
			Password = passwordIn;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.LoginRequestMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			Email = incomingMessage.ReadString();
			Password = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( Email );
			outgoingMessage.Write( Password );
		}

		public string Email { get; private set; }
		public string Password { get; private set; }
	}
}