/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent from the client to the server which holds
the necessary information for a password reset.
*********************************************/
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent from the client to the server which holds
	/// the necessary information for a password reset.
	/// </summary>
	public class PasswordResetMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public PasswordResetMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates the message form the user input.
		/// </summary>
		/// <param name="codeIn">Recieved code</param>
		/// <param name="emailIn">User email</param>
		/// <param name="passwordIn">New user password</param>
		public PasswordResetMessage( string codeIn, string emailIn, string passwordIn )
		{
			Code = codeIn;
			Email = emailIn;
			Password = passwordIn;
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.PasswordResetMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			Code = incomingMessage.ReadString();
			Email = incomingMessage.ReadString();
			Password = incomingMessage.ReadString();
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( Code );
			outgoingMessage.Write( Email );
			outgoingMessage.Write( Password );
		}

		///<value>The user's recieved code</value>
		public string Code { get; private set; }

		///<value>The user email</value>
		public string Email { get; private set; }

		///<value>The new password</value>
		public string Password { get; private set; }
	}
}