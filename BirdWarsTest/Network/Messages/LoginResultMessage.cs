using Lidgren.Network;
using BirdWarsTest.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	class LoginResultMessage : IGameMessage
	{
		public LoginResultMessage( NetIncomingMessage incomingMessage )
		{
			user = new User();
			Decode( incomingMessage );
		}

		public LoginResultMessage( bool result, string reasonIn, User userIn )
		{
			user = new User();
			loginRequestResult = result;
			reason = reasonIn;
			user = userIn;
		}

		public GameMessageTypes messageType
		{
			get{ return GameMessageTypes.LoginResultMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			loginRequestResult = incomingMessage.ReadBoolean();
			reason = incomingMessage.ReadString();
			user.userId = incomingMessage.ReadInt32();
			user.names = incomingMessage.ReadString();
			user.lastName = incomingMessage.ReadString();
			user.username = incomingMessage.ReadString();
			user.email = incomingMessage.ReadString();
			user.password = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( loginRequestResult );
			outgoingMessage.Write( reason );
			outgoingMessage.Write( user.userId );
			outgoingMessage.Write( user.names );
			outgoingMessage.Write( user.lastName );
			outgoingMessage.Write( user.username );
			outgoingMessage.Write( user.email );
			outgoingMessage.Write( user.password );
		}

		public bool loginRequestResult;
		public string reason;
		private User user;
	}
}
