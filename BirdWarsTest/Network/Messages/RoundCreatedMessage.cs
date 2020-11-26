using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	class RoundCreatedMessage : IGameMessage
	{
		public RoundCreatedMessage( NetIncomingMessage incomingMessage )
		{
			playerUsernameList = new string[ 8 ];
			EmptyFill();
			Decode( incomingMessage );
		}

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
		public GameMessageTypes messageType
		{
			get{ return GameMessageTypes.RoundCreatedMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			RoundCreated = incomingMessage.ReadBoolean();
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = incomingMessage.ReadString();
			}
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( RoundCreated );
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

		public bool RoundCreated { get; private set; }

		private string [] playerUsernameList;
	}
}
