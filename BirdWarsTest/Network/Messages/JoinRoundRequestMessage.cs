﻿using Lidgren.Network;

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