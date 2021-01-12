/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent to all clients by server indicating them to spawn
consumable items at the specified locations.
*********************************************/
using BirdWarsTest.GameObjects;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent to all clients by server indicating them to spawn
	/// consumable items at the specified locations.
	/// </summary>
	public class SpawnConsumablesMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public SpawnConsumablesMessage( NetIncomingMessage incomingMessage )
		{
			Identifiers = new int[ 4 ];
			ObjectPositions = new Vector2[ 4 ];
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates a message from the input consumable item list.
		/// </summary>
		/// <param name="consumablesIn">COnsumale item list</param>
		public SpawnConsumablesMessage( List< GameObject > consumablesIn )
		{
			Identifiers = new int[ 4 ];
			ObjectPositions = new Vector2[ 4 ];
			for( int i = 0; i < 4; i++ )
			{
				Identifiers[ i ] = ( int )consumablesIn[ i ].Identifier;
				ObjectPositions[ i ] = consumablesIn[ i ].Position;
			}
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.SpawnConsumablesMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			for( int i = 0; i < 4; i++ )
			{
				Identifiers[ i ] = incomingMessage.ReadInt32();
				ObjectPositions[ i ] = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
			}
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			for( int i = 0; i < 4; i++ )
			{
				outgoingMessage.Write( Identifiers[ i ] );
				outgoingMessage.Write( ObjectPositions[ i ].X );
				outgoingMessage.Write( ObjectPositions[ i ].Y );
			}
		}

		///<value>The list consumable item types</value>
		public int [] Identifiers { get; private set; }

		///<value>The list of item positions</value>
		public Vector2 [] ObjectPositions { get; private set; }
	}
}