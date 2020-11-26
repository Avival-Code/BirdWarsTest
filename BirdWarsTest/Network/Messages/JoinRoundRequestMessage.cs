using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	class JoinRoundRequestMessage : IGameMessage
	{
		public JoinRoundRequestMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public JoinRoundRequestMessage( string usernameIn )
		{
			username = usernameIn;
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.JoinRoundRequestMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			username = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( username );
		}

		private string username;
	}
}