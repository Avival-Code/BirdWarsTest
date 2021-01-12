/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent to all clients and server to indicate that the game round has started.
*********************************************/
using Lidgren.Network;
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent to all clients and server to indicate that the game round has started.
	/// </summary>
	public class StartRoundMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public StartRoundMessage( NetIncomingMessage incomingMessage )
		{
			playerUsernameList = new string[ 8 ];
			playerPositions = new List< Vector2 >();
			EmptyUsernameFill();
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates a message from the list of player usernames in the 
		/// game rounds. Server sends this message to itself.
		/// </summary>
		/// <param name="usernames">Player usernames in game round</param>
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

		/// <summary>
		/// Creates a message from the list of players in the game round and
		/// their positions on the game world.
		/// </summary>
		/// <param name="usernames">Player usernames</param>
		/// <param name="players">Player objects</param>
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
					playerPositions.Add( new Vector2( players[ i ].Position.X, players[ i ].Position.Y ) );
				}
			}
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.StartRoundMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
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

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
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

		private void EmptyUsernameFill()
		{
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = "";
			}
		}

		private void EmptyPositionFill()
		{
			for( int i = 0; i < 8; i++ )
			{
				playerPositions.Add( new Vector2( 0.0f, 0.0f ) );
			}
		}

		private List<Vector2> playerPositions;
		private string[] playerUsernameList;
	}
}