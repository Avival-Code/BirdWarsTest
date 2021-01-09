using Lidgren.Network;
using BirdWarsTest.Database;

namespace BirdWarsTest.Network.Messages
{
	public class RegisterUserMessage : IGameMessage
	{
		public RegisterUserMessage( NetIncomingMessage incomingMessage )
		{
			User = new User();
			Decode( incomingMessage );
		}

		public RegisterUserMessage( User newUser )
		{
			User = new User( newUser );
			name = newUser.Names;
			lastNames = newUser.LastName;
			username = newUser.Username;
			email = newUser.Email;
			password = newUser.Password;
		}
		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.RegisterUserMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			name = incomingMessage.ReadString();
			lastNames = incomingMessage.ReadString();
			username = incomingMessage.ReadString();
			email = incomingMessage.ReadString();
			password = incomingMessage.ReadString();
			User = new User( name, lastNames, username, email, password );
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( name );
			outgoingMessage.Write( lastNames );
			outgoingMessage.Write( username );
			outgoingMessage.Write( email );
			outgoingMessage.Write( password );
		}

		public User User { get; private set; }
		private string name;
		private string lastNames;
		private string username;
		private string email;
		private string password;
	}
}