using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	public class BoxDamageMessage : IGameMessage
	{
		public BoxDamageMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public BoxDamageMessage( int boxIndexIn, int damageIn )
		{
			BoxIndex = boxIndexIn;
			Damage = damageIn;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.BoxDamageMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			BoxIndex = incomingMessage.ReadInt32();
			Damage = incomingMessage.ReadInt32();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( BoxIndex );
			outgoingMessage.Write( Damage );
		}

		public int BoxIndex { get; private set; }
		public int Damage { get; private set; }
	}
}