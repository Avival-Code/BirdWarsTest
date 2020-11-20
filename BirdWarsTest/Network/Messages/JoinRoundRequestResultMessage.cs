using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	class JoinRoundRequestResultMessage : IGameMessage
	{
		public JoinRoundRequestResultMessage( NetIncomingMessage incomingMessage )
		{
			playerUsernameList = new string[ 8 ];
			EmptyFill();
			Decode( incomingMessage );
		}

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
		public GameMessageTypes messageType
		{
			get{ return GameMessageTypes.JoinRoundRequestResultMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			approved = incomingMessage.ReadBoolean();
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = incomingMessage.ReadString();
			}
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( approved );
			for( int i = 0; i < 8; i++ )
			{
				outgoingMessage.Write( playerUsernameList[ i ] );
			}
		}

		public void EmptyFill()
		{
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = "";
			}
		}

		public bool approved;
		public string [] playerUsernameList;
	}
}
