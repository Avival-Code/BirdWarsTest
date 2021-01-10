using Lidgren.Network;
using BirdWarsTest.GameObjects;

namespace BirdWarsTest.Network.Messages
{
	public class PlayerIsDeadMessage : IGameMessage
	{
		public PlayerIsDeadMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public PlayerIsDeadMessage( Identifiers playerId )
		{
			PlayerId = playerId;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.PlayerIsDeadMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			PlayerId = ( Identifiers )incomingMessage.ReadInt32();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( ( int )PlayerId );
		}

		public Identifiers PlayerId { get; private set; }
	}
}