/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent to all clients by erver indicating them to spawn
item boxes at the locations specified.
*********************************************/
using BirdWarsTest.GameObjects;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent to all clients by erver indicating them to spawn
	/// item boxes at the locations specified.
	/// </summary>
	public class SpawnBoxMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public SpawnBoxMessage( NetIncomingMessage incomingMessage )
		{
			boxCount = 0;
			positions = new List< Vector2 >();
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates a message from a list of existing item boxes.
		/// </summary>
		/// <param name="boxes">The list of item boxes.</param>
		public SpawnBoxMessage( List< GameObject > boxes )
		{
			boxCount = boxes.Count;
			positions = new List< Vector2 >();
			foreach( var box in boxes )
			{
				positions.Add( box.Position );
			}
		}

		/// <summary>
		/// Creates a message from a single item box instance
		/// </summary>
		/// <param name="box">The target box</param>
		public SpawnBoxMessage( GameObject box )
		{
			boxCount = 1;
			positions = new List< Vector2 >();
			positions.Add( box.Position );
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.SpawnBoxMessage; } 
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			boxCount = incomingMessage.ReadInt32();
			for( int i = 0; i < boxCount; i++ )
			{
				positions.Add( new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) );
			}
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( boxCount );
			for( int i = 0; i < boxCount; i++ )
			{
				outgoingMessage.Write( positions[ i ].X );
				outgoingMessage.Write( positions[ i ].Y );
			}
		}

		private int boxCount;
		private List< Vector2 > positions;
	}
}