using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	class LoginResultMessage : IGameMessage
	{
		public LoginResultMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public LoginResultMessage( bool result, string reason )
		{
			loginRequestResult = result;
		}

		public GameMessageTypes messageType
		{
			get{ return GameMessageTypes.LoginResultMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			loginRequestResult = incomingMessage.ReadBoolean();
			reason = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( loginRequestResult );
			outgoingMessage.Write( reason );
		}

		public bool loginRequestResult;
		public string reason;
	}
}
