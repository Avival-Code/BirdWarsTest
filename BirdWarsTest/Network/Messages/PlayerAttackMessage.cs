/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Used to alert all clients and server about which player
has attacked.
*********************************************/
using BirdWarsTest.GameObjects;
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Used to alert all clients and server about which player
	/// has attacked.
	/// </summary>
	public class PlayerAttackMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public PlayerAttackMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates the game message from the target player index
		/// </summary>
		/// <param name="playerIndexIn">Player index</param>
		public PlayerAttackMessage( Identifiers playerIndexIn )
		{
			PlayerIndex = playerIndexIn;
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.PlayerAttackMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			PlayerIndex = ( Identifiers )incomingMessage.ReadInt32();
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( ( int )PlayerIndex );
		}

		///<value>Target player index</value>
		public Identifiers PlayerIndex { get; private set; }
	}
}