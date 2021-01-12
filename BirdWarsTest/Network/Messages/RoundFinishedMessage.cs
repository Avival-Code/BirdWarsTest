/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent to all clients by server to indicate that 
the game round has finished.
*********************************************/
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent to all clients by server to indicate that 
	/// the game round has finished.
	/// </summary>
	public class RoundFinishedMessage : IGameMessage
	{

		/// <summary>
		/// Creates the game message from teh remaining round time.
		/// </summary>
		/// <param name="remainingRoundTimeIn"></param>
		public RoundFinishedMessage( int remainingRoundTimeIn )
		{
			RemainingRoundTime = remainingRoundTimeIn;
		}

		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public RoundFinishedMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.RoundFinishedMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage ) 
		{
			RemainingRoundTime = incomingMessage.ReadInt32();
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage ) 
		{
			outgoingMessage.Write( RemainingRoundTime );
		}

		///<value>The remaining round time as an integer</value>
		public int RemainingRoundTime { get; private set; }
	}
}