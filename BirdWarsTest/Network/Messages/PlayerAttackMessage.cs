using BirdWarsTest.GameObjects;
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class PlayerAttackMessage : IGameMessage
	{
		public PlayerAttackMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public PlayerAttackMessage( Identifiers playerIndexIn )
		{
			PlayerIndex = playerIndexIn;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.PlayerAttackMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			PlayerIndex = ( Identifiers )incomingMessage.ReadInt32();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( ( int )PlayerIndex );
		}

		public Identifiers PlayerIndex { get; private set; }
	}
}