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
			roundCreated = roundCreatedIn;
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
			roundCreated = incomingMessage.ReadBoolean();
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[ i ] = incomingMessage.ReadString();
			}
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( roundCreated );
			for( int i = 0; i < 8; i++ )
			{
				outgoingMessage.Write( playerUsernameList[ i ] );
			}
		}

		public void EmptyFill()
		{
			for( int i = 0; i < 8; i++ )
			{
				playerUsernameList[i] = "";
			}
		}

		public bool RoundCreated
		{
			get{ return roundCreated; }
		}

		private bool roundCreated;
		private string [] playerUsernameList;
	}
}
