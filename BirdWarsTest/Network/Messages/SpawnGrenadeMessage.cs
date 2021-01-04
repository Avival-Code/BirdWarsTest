using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.Network.Messages
{
	public class SpawnGrenadeMessage : IGameMessage
	{
		public SpawnGrenadeMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public SpawnGrenadeMessage( GameObject gameObject )
		{
			Position = gameObject.Position;
			Direction = ResetDirectionValues( ( ( GrenadeInputComponent )gameObject.Input ).Direction );
			GrenadeSpeed = gameObject.Input.GetObjectSpeed();
		}

		public GameMessageTypes messageType
		{
			get{ return GameMessageTypes.SpawnGrenadeMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			Position = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
			Direction = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
			GrenadeSpeed = incomingMessage.ReadFloat();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( Position.X );
			outgoingMessage.Write( Position.Y );
			outgoingMessage.Write( Direction.X );
			outgoingMessage.Write( Direction.Y );
			outgoingMessage.Write( GrenadeSpeed );
		}

		private Vector2 ResetDirectionValues( Vector2 direction )
		{
			Vector2 resetDirection = new Vector2( 0.0f, 0.0f );

			if( direction.Y < 0 && direction.X == 0 )
			{
				resetDirection = new Vector2( 0.0f, -1.0f );
			}

			if( direction.Y > 0 && direction.X == 0 )
			{
				resetDirection = new Vector2( 0.0f, 1.0f );
			}

			if( direction.X < 0 && direction.Y == 0 )
			{
				resetDirection = new Vector2( -1.0f, 0.0f );
			}

			if( direction.X > 0 && direction.Y == 0 )
			{
				resetDirection = new Vector2( 1.0f, 0.0f );
			}

			return resetDirection;
		}

		public Vector2 Position { get; private set; }
		public Vector2 Direction { get; private set; }
		public float GrenadeSpeed { get; private set; }
	}
}