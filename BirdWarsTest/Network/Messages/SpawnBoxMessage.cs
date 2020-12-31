using BirdWarsTest.GameObjects;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BirdWarsTest.Network.Messages
{
	class SpawnBoxMessage : IGameMessage
	{
		public SpawnBoxMessage( NetIncomingMessage incomingMessage )
		{
			boxCount = 0;
			positions = new List< Vector2 >();
			Decode( incomingMessage );
		}

		public SpawnBoxMessage( List< GameObject > boxes )
		{
			boxCount = boxes.Count;
			positions = new List< Vector2 >();
			foreach( var box in boxes )
			{
				positions.Add( box.Position );
			}
		}

		public SpawnBoxMessage( GameObject box )
		{
			boxCount = 1;
			positions = new List< Vector2 >();
			positions.Add( box.Position );
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.SpawnBoxMessage; } 
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			boxCount = incomingMessage.ReadInt32();
			for( int i = 0; i < boxCount; i++ )
			{
				positions.Add( new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) );
			}
		}

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