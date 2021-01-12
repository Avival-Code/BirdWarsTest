/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent to all clients by server to update the current state
of the game round.
*********************************************/
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent to all clients by server to update the current state
	/// of the game round.
	/// </summary>
	public class RoundStateChangedMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public RoundStateChangedMessage( NetIncomingMessage incomingMessage )
		{
			playerUsernameList = new string[ 8 ];
			EmptyFill();
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates a message from the list of players in the game round.
		/// </summary>
		/// <param name="usernames">List of players</param>
		public RoundStateChangedMessage( string [] usernames )
		{
			playerUsernameList = new string[ 8 ];
			EmptyFill();
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = usernames[ i ];
			}
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get{ return GameMessageTypes.RoundStateChangedMessage; }
		}


		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = incomingMessage.ReadString();
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
			}
		}

		private void EmptyFill()
		{
			for ( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = "";
			}
		}

		private string [] playerUsernameList;
	}
}