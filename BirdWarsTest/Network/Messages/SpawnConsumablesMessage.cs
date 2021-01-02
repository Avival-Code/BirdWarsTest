using BirdWarsTest.GameObjects;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BirdWarsTest.Network.Messages
{
	public class SpawnConsumablesMessage : IGameMessage
	{
		public SpawnConsumablesMessage( NetIncomingMessage incomingMessage )
		{
			Identifiers = new int[ 4 ];
			ObjectPositions = new Vector2[ 4 ];
			Decode( incomingMessage );
		}

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

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.SpawnConsumablesMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			for( int i = 0; i < 4; i++ )
			{
				Identifiers[ i ] = incomingMessage.ReadInt32();
				ObjectPositions[ i ] = new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() );
			}
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			for( int i = 0; i < 4; i++ )
			{
				outgoingMessage.Write( Identifiers[ i ] );
				outgoingMessage.Write( ObjectPositions[ i ].X );
				outgoingMessage.Write( ObjectPositions[ i ].Y );
			}
		}

		public int [] Identifiers { get; private set; }
		public Vector2 [] ObjectPositions { get; private set; }
	}
}