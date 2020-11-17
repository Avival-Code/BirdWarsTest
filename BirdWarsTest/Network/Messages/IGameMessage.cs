using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public interface IGameMessage
	{
		void Encode( NetOutgoingMessage outgoingMessage );

		void Decode( NetIncomingMessage incomingMessage );

		GameMessageTypes messageType { get; }
	}
}
