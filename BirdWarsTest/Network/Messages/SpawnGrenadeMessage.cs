/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent all clients indicating them to spawn a grenade at the
specified position with the specified direction and speed.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent all clients indicating them to spawn a grenade at the
	/// specified position with the specified direction and speed.
	/// </summary>
	public class SpawnGrenadeMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public SpawnGrenadeMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates a message from the grenade object.
		/// </summary>
		/// <param name="gameObject"></param>
		public SpawnGrenadeMessage( Identifiers localPlayerIdIn, GameObject gameObject )
		{
			LocalPlayerId = localPlayerIdIn;
			Position = gameObject.Position;
			Direction = ResetDirectionValues( ( ( GrenadeInputComponent )gameObject.Input ).Direction );
			GrenadeSpeed = gameObject.Input.GetObjectSpeed();
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get{ return GameMessageTypes.SpawnGrenadeMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			LocalPlayerId = ( Identifiers )incomingMessage.ReadInt32();
			Position = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
			Direction = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
			GrenadeSpeed = incomingMessage.ReadFloat();
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( ( int )LocalPlayerId );
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

			if( direction.X < 0 && direction.Y < 0 )
			{
				resetDirection = new Vector2( -1.0f, -1.0f );
			}

			if( direction.X > 0 && direction.Y < 0 )
			{
				resetDirection = new Vector2( 1.0f, -1.0f );
			}

			if( direction.X < 0 && direction.Y > 0 )
			{
				resetDirection = new Vector2( -1.0f, 1.0f );
			}

			if( direction.X > 0 && direction.Y > 0 )
			{
				resetDirection = new Vector2( 1.0f, 1.0f );
			}

			return resetDirection;
		}

		///<value>The grenade position</value>
		public Vector2 Position { get; private set; }

		///<value>The grenade object direction</value>
		public Vector2 Direction { get; private set; }

		public Identifiers LocalPlayerId { get; private set; }

		///<value>The grenade object speed</value>
		public float GrenadeSpeed { get; private set; }
	}
}