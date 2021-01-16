/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent by client to server asking to join a game
round.
*********************************************/
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent by client to server asking to join a game
	/// round.
	/// </summary>
	public class JoinRoundRequestMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public JoinRoundRequestMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates the game message from a username input
		/// </summary>
		/// <param name="usernameIn">Player username</param>
		public JoinRoundRequestMessage( string usernameIn )
		{
			Username = usernameIn;
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.JoinRoundRequestMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			Username = incomingMessage.ReadString();
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( Username );
		}

		public string Username { get; private set; }
	}
}