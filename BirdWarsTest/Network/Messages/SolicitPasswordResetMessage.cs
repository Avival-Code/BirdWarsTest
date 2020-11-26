using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	class SolicitPasswordResetMessage : IGameMessage
	{
		public SolicitPasswordResetMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public SolicitPasswordResetMessage( string emailIn )
		{
			email = emailIn;
		}

		public GameMessageTypes messageType 
		{ 
			get { return GameMessageTypes.SolicitPasswordResetmessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			email = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( email );
		}

		private string email;
	}
}