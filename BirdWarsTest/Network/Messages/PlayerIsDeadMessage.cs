/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Used to alert all clients and server which player has died.
*********************************************/
using Lidgren.Network;
using BirdWarsTest.GameObjects;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Used to alert all clients and server which player has died.
	/// </summary>
	public class PlayerIsDeadMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public PlayerIsDeadMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates a message from the target player Id
		/// </summary>
		/// <param name="playerId">Target player Id</param>
		public PlayerIsDeadMessage( Identifiers playerId )
		{
			PlayerId = playerId;
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.PlayerIsDeadMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			PlayerId = ( Identifiers )incomingMessage.ReadInt32();
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( ( int )PlayerId );
		}

		///<value>Target player Id</value>
		public Identifiers PlayerId { get; private set; }
	}
}