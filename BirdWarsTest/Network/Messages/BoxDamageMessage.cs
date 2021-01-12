/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message used to send the amount of damage an item box
has sustained.
*********************************************/
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message used to send the amount of damage an item box
	/// has sustained.
	/// </summary>
	public class BoxDamageMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public BoxDamageMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates the message from a box index and the damage it
		/// sustained.
		/// </summary>
		/// <param name="boxIndexIn">target box index</param>
		/// <param name="damageIn">Damage sustained</param>
		public BoxDamageMessage( int boxIndexIn, int damageIn )
		{
			BoxIndex = boxIndexIn;
			Damage = damageIn;
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.BoxDamageMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			BoxIndex = incomingMessage.ReadInt32();
			Damage = incomingMessage.ReadInt32();
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( BoxIndex );
			outgoingMessage.Write( Damage );
		}

		///<value>The target box index</value>
		public int BoxIndex { get; private set; }

		///<value>The damage sustained</value>
		public int Damage { get; private set; }
	}
}