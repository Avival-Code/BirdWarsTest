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
			boxIndex = boxIndexIn;
			damage = damageIn;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.BoxDamageMessage; }
		}
		public void Decode( NetIncomingMessage incomingMessage )
		{
			boxIndex = incomingMessage.ReadInt32();
			damage = incomingMessage.ReadInt32();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( boxIndex );
			outgoingMessage.Write( damage );
		}

		private int boxIndex;
		private int damage;
	}
}