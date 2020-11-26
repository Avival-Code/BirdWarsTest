using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	class PasswordResetMessage : IGameMessage
	{
		public PasswordResetMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public PasswordResetMessage( string codeIn, string passwordIn )
		{
			code = codeIn;
			password = passwordIn;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.PasswordResetMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			code = incomingMessage.ReadString();
			password = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( code );
			outgoingMessage.Write( password );
		}

		private string code;
		private string password;
	}
}
