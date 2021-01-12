/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Used to update player state on all clients and server.
*********************************************/
using Lidgren.Network;
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Used to update player state on all clients and server.
	/// </summary>
	public class PlayerStateChangeMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public PlayerStateChangeMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates a message from a player gameobject
		/// </summary>
		/// <param name="player">The player object</param>
		public PlayerStateChangeMessage( GameObject player )
		{
			Id = player.Identifier;
			Position = player.Position;
			Velocity = player.Input.GetVelocity();
			MessageTime = NetTime.Now;
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get{ return GameMessageTypes.PlayerStateChangeMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			Id = ( Identifiers )incomingMessage.ReadInt32();
			MessageTime = incomingMessage.ReadDouble();
			Position = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
			Velocity = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( ( int )Id );
			outgoingMessage.Write( MessageTime );
			outgoingMessage.Write( Position.X );
			outgoingMessage.Write( Position.Y );
			outgoingMessage.Write( Velocity.X );
			outgoingMessage.Write( Velocity.Y );
		}

		///<value>Target player Id</value>
		public Identifiers Id { get; private set; }

		///<value>The player position</value>
		public Vector2 Position { get; set; }

		///<value>The player velocity</value>
		public Vector2 Velocity { get; private set; }

		///<value>The time message was created</value>
		public double MessageTime { get; private set; }
	}
}