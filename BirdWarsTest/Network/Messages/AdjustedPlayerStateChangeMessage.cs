using BirdWarsTest.GameObjects;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.Network.Messages
{
	public class AdjustedPlayerStateChangeMessage : IGameMessage
	{
		public AdjustedPlayerStateChangeMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public AdjustedPlayerStateChangeMessage( Identifiers idIn, Vector2 positionIn, Vector2 velocityIn,
												 float clientDelayIn )
		{
			Id = idIn;
			Position = positionIn;
			Velocity = velocityIn;
			ClientDelayTime = clientDelayIn;
			MessageTime = NetTime.Now;
		}

		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.AdjustedPlayerStateChangeMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			Id = ( Identifiers )incomingMessage.ReadInt32();
			Position = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
			Velocity = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
			ClientDelayTime = incomingMessage.ReadFloat();
			MessageTime = incomingMessage.ReadDouble();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( ( int )Id );
			outgoingMessage.Write( Position.X );
			outgoingMessage.Write( Position.Y );
			outgoingMessage.Write( Velocity.X );
			outgoingMessage.Write( Velocity.Y );
			outgoingMessage.Write( ClientDelayTime );
			outgoingMessage.Write( MessageTime );
		}

		///<value>Target player Id</value>
		public Identifiers Id { get; private set; }

		///<value>The player position</value>
		public Vector2 Position { get; set; }

		///<value>The player velocity</value>
		public Vector2 Velocity { get; private set; }

		///<value>The time message was created</value>
		public double MessageTime { get; private set; }

		///<value>Calculated client delay time</value>
		public float ClientDelayTime { get; private set; }
	}
}