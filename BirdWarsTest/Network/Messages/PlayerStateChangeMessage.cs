using Lidgren.Network;
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.Network.Messages
{
	class PlayerStateChangeMessage : IGameMessage
	{
		public PlayerStateChangeMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public PlayerStateChangeMessage( GameObject player )
		{
			Id = player.Identifier;
			Position = player.Position;
			Velocity = player.Input.GetVelocity();
			MessageTime = NetTime.Now;
		}

		public GameMessageTypes messageType
		{
			get{ return GameMessageTypes.PlayerStateChangeMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			Id = ( Identifiers )incomingMessage.ReadInt32();
			MessageTime = incomingMessage.ReadDouble();
			Position = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
			Velocity = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( ( int )Id );
			outgoingMessage.Write( MessageTime );
			outgoingMessage.Write( Position.X );
			outgoingMessage.Write( Position.Y );
			outgoingMessage.Write( Velocity.X );
			outgoingMessage.Write( Velocity.Y );
		}

		public Identifiers Id { get; private set; }
		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; private set; }
		public double MessageTime { get; private set; }
	}
}