/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent to the server by the client asking it to 
validate user credentials and allow login.
*********************************************/
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent to the server by the client asking it to 
	/// validate user credentials and allow login.
	/// </summary>
	public class LoginRequestMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public LoginRequestMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates a game message from the login credentials provided
		/// by user.
		/// </summary>
		/// <param name="emailIn">User email</param>
		/// <param name="passwordIn">User password</param>
		public LoginRequestMessage( string emailIn, string passwordIn )
		{
			Email = emailIn;
			Password = passwordIn;
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.LoginRequestMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			Email = incomingMessage.ReadString();
			Password = incomingMessage.ReadString();
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( Email );
			outgoingMessage.Write( Password );
		}

		///<value>The user email</value>
		public string Email { get; private set; }

		///<value>The user password</value>
		public string Password { get; private set; }
	}
}