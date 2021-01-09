using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class PasswordResetMessage : IGameMessage
	{
		public PasswordResetMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public PasswordResetMessage( string codeIn, string emailIn, string passwordIn )
		{
			Code = codeIn;
			Email = emailIn;
			Password = passwordIn;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.PasswordResetMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			Code = incomingMessage.ReadString();
			Email = incomingMessage.ReadString();
			Password = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( Code );
			outgoingMessage.Write( Email );
			outgoingMessage.Write( Password );
		}

		public string Code { get; private set; }
		public string Email { get; private set; }
		public string Password { get; private set; }
	}
}