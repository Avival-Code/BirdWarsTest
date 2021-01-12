/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent to all clients to syncronize game round time
with server's round time.
*********************************************/
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent to all clients to syncronize game round time
	/// with server's round time.
	/// </summary>
	public class UpdateRoundTimeMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public UpdateRoundTimeMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates a message from the remaining round time and current
		/// nettime.
		/// </summary>
		/// <param name="remainingRoundTime">Round time</param>
		public UpdateRoundTimeMessage( float remainingRoundTime )
		{
			MessageTime = NetTime.Now;
			RemainingRoundTime = remainingRoundTime;
		}


		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.UpdateRoundTimeMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			MessageTime = incomingMessage.ReadDouble();
			RemainingRoundTime = incomingMessage.ReadFloat();
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( MessageTime );
			outgoingMessage.Write( RemainingRoundTime );
		}

		///<value>The time the message wwas created</value>
		public double MessageTime { get; private set; }

		///<value>The remaining round time</value>
		public float RemainingRoundTime { get; private set; }
	}
}