/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent to server to indicating it that the game round 
has been created.
*********************************************/
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent to server to indicating it that the game round 
	/// has been created.
	/// </summary>
	public class RoundCreatedMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public RoundCreatedMessage( NetIncomingMessage incomingMessage )
		{
			playerUsernameList = new string[ 8 ];
			EmptyFill();
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates a message with input bool and list of
		/// usernames in player round.
		/// </summary>
		/// <param name="roundCreatedIn"></param>
		/// <param name="usernames"></param>
		public RoundCreatedMessage( bool roundCreatedIn, string [] usernames )
		{
			playerUsernameList = new string[ 8 ];
			EmptyFill();
			RoundCreated = roundCreatedIn;
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
			get{ return GameMessageTypes.RoundCreatedMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			RoundCreated = incomingMessage.ReadBoolean();
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
			outgoingMessage.Write( RoundCreated );
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

		///<value>Boo indicating that the round has been created</value>
		public bool RoundCreated { get; private set; }
		private string [] playerUsernameList;
	}
}