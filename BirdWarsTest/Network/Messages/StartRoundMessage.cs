using Lidgren.Network;
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BirdWarsTest.Network.Messages
{
	public class StartRoundMessage : IGameMessage
	{
		public StartRoundMessage( NetIncomingMessage incomingMessage )
		{
			playerUsernameList = new string[ 8 ];
			playerPositions = new List< Vector2 >();
			EmptyUsernameFill();
			Decode( incomingMessage );
		}

		public StartRoundMessage( string [] usernames )
		{
			playerUsernameList = new string[ 8 ];
			playerPositions = new List< Vector2 >();
			EmptyUsernameFill();
			EmptyPositionFill();
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = usernames[ i ];
			}
		}

		public StartRoundMessage( string[] usernames, List< GameObject > players  )
		{
			playerUsernameList = new string[ 8 ];
			playerPositions = new List< Vector2 >();
			EmptyUsernameFill();
			for(int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = usernames[ i ];
				if( players.Count > 0 && i < players.Count )
				{
					playerPositions.Add(new Vector2(players[i].Position.X, players[i].Position.Y));
				}
			}
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.StartRoundMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			for( int i = 0; i < 8; i++ )
			{
				var username = incomingMessage.ReadString();
				playerUsernameList[ i ] = username;
				if( !string.IsNullOrEmpty( username ) )
				{
					playerPositions.Add( new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) );
				}
			}
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			for( int i = 0; i < 8; i++ )
			{
				outgoingMessage.Write( playerUsernameList[ i ] );
				if( playerPositions.Count > 0 && i < playerPositions.Count )
				{
					outgoingMessage.Write( playerPositions[ i ].X );
					outgoingMessage.Write( playerPositions[ i ].Y );
				}
			}
		}

		public void EmptyUsernameFill()
		{
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = "";
			}
		}

		public void EmptyPositionFill()
		{
			for( int i = 0; i < 8; i++ )
			{
				playerPositions.Add( new Vector2( 0.0f, 0.0f ) );
			}
		}

		private string [] playerUsernameList;
		private List< Vector2 > playerPositions;
	}
}