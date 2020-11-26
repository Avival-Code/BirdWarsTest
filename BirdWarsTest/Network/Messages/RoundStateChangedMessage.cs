using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	class RoundStateChangedMessage : IGameMessage
	{
		public RoundStateChangedMessage( NetIncomingMessage incomingMessage )
		{
			playerUsernameList = new string[ 8 ];
			EmptyFill();
			Decode( incomingMessage );
		}

		public RoundStateChangedMessage( string [] usernames )
		{
			playerUsernameList = new string[ 8 ];
			EmptyFill();
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = usernames[ i ];
			}
		}
		public GameMessageTypes messageType
		{
			get{ return GameMessageTypes.RoundStateChangedMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = incomingMessage.ReadString();
			}
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			for( int i = 0; i < 8; i++ )
			{
				outgoingMessage.Write( playerUsernameList[ i ] );
			}
		}

		public void EmptyFill()
		{
			for ( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = "";
			}
		}

		private string [] playerUsernameList;
	}
}
