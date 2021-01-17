/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Stores the player ID, position, velocity and adjusted player lag time value.
*********************************************/
using BirdWarsTest.GameObjects;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Stores the player ID, position, velocity and adjusted player lag time value.
	/// </summary>
	public class AdjustedPlayerStateChangeMessage : IGameMessage
	{
		/// <summary>
		/// Creates an AdjustedPlayerStateChange message from a 
		/// NetIncomingMessage.
		/// </summary>
		/// <param name="incomingMessage">The incoming message.</param>
		public AdjustedPlayerStateChangeMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates an AdjustedPlayerStateChange message from gameObject values.
		/// </summary>
		/// <param name="idIn">The Game object's Id</param>
		/// <param name="positionIn">The Game object's position</param>
		/// <param name="velocityIn">The Game object's velocity</param>
		/// <param name="clientDelayIn">The Game object's delayTime</param>
		public AdjustedPlayerStateChangeMessage( Identifiers idIn, Vector2 positionIn, Vector2 velocityIn,
												 float clientDelayIn )
		{
			Id = idIn;
			Position = positionIn;
			Velocity = velocityIn;
			ClientDelayTime = clientDelayIn;
			MessageTime = NetTime.Now;
		}

		/// <summary>
		/// Returns the type of Game message.
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.AdjustedPlayerStateChangeMessage; }
		}

		/// <summary>
		/// Decodes the information stored in a NetIncoming message.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			Id = ( Identifiers )incomingMessage.ReadInt32();
			Position = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
			Velocity = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
			ClientDelayTime = incomingMessage.ReadFloat();
			MessageTime = incomingMessage.ReadDouble();
		}

		/// <summary>
		/// Encodes this message's values to a NetOutgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The outgoing message.</param>
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