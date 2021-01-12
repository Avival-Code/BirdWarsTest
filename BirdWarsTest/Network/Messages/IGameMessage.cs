/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
The Game message interface
*********************************************/
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// The Game message interface
	/// </summary>
	public interface IGameMessage
	{
		/// <summary>
		/// Returns the message type
		/// </summary>
		GameMessageTypes MessageType { get; }

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		void Encode( NetOutgoingMessage outgoingMessage );

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		void Decode( NetIncomingMessage incomingMessage );
	}
}