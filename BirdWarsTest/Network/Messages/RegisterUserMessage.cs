/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent to server by client that holds all the necessary
information to create a new user account.
*********************************************/
using Lidgren.Network;
using BirdWarsTest.Database;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent to server by client that holds all the necessary
	/// information to create a new user account.
	/// </summary>
	public class RegisterUserMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public RegisterUserMessage( NetIncomingMessage incomingMessage )
		{
			User = new User();
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates the game message from a user
		/// </summary>
		/// <param name="newUser">The user to be created</param>
		public RegisterUserMessage( User newUser )
		{
			User = new User( newUser );
			name = newUser.Names;
			lastNames = newUser.LastName;
			username = newUser.Username;
			email = newUser.Email;
			password = newUser.Password;
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.RegisterUserMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			name = incomingMessage.ReadString();
			lastNames = incomingMessage.ReadString();
			username = incomingMessage.ReadString();
			email = incomingMessage.ReadString();
			password = incomingMessage.ReadString();
			User = new User( name, lastNames, username, email, password );
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( name );
			outgoingMessage.Write( lastNames );
			outgoingMessage.Write( username );
			outgoingMessage.Write( email );
			outgoingMessage.Write( password );
		}

		///<value>The user to be created</value>
		public User User { get; private set; }
		private string name;
		private string lastNames;
		private string username;
		private string email;
		private string password;
	}
}