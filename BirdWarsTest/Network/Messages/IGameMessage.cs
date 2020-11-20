using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public interface IGameMessage
	{
		GameMessageTypes messageType { get; }

		void Encode( NetOutgoingMessage outgoingMessage );

		void Decode( NetIncomingMessage incomingMessage );
	}
}
