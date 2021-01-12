/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message to the client by the server telling it if it's
join round request was accepted or denied.
*********************************************/
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message to the client by the server telling it if it's
	/// join round request was accepted or denied.
	/// </summary>
	public class JoinRoundRequestResultMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public JoinRoundRequestResultMessage( NetIncomingMessage incomingMessage )
		{
			playerUsernameList = new string[ 8 ];
			EmptyFill();
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates a game message from a string of player usernames already
		/// in the game round.
		/// </summary>
		/// <param name="approvedIn">Approved or denied bool</param>
		/// <param name="usernames">The list of player usernames</param>
		public JoinRoundRequestResultMessage( bool approvedIn, string [] usernames )
		{
			playerUsernameList = new string[ 8 ];
			EmptyFill();
			approved = approvedIn;
			for ( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = usernames[ i ];
			}
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get{ return GameMessageTypes.JoinRoundRequestResultMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			approved = incomingMessage.ReadBoolean();
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
			outgoingMessage.Write( approved );
			for( int i = 0; i < 8; i++ )
			{
				outgoingMessage.Write( playerUsernameList[ i ] );
			}
		}

		private void EmptyFill()
		{
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = "";
			}
		}

		private bool approved;
		private string [] playerUsernameList;
	}
}