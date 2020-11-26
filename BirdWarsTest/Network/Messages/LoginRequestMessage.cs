using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	class LoginRequestMessage : IGameMessage
	{
		public LoginRequestMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public LoginRequestMessage( string emailIn, string passwordIn )
		{
			email = emailIn;
			password = passwordIn;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.LoginRequestMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			email = incomingMessage.ReadString();
			password = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( email );
			outgoingMessage.Write( password );
		}

		private string email;
		private string password;
	}
}
