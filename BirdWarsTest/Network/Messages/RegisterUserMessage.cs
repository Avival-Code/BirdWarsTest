using Lidgren.Network;
using BirdWarsTest.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	class RegisterUserMessage : IGameMessage
	{
		public RegisterUserMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		public RegisterUserMessage( User newUser )
		{
			name = newUser.names;
			lastNames = newUser.lastName;
			username = newUser.username;
			email = newUser.email;
			password = newUser.password;
		}
		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.registerUserMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			name = incomingMessage.ReadString();
			lastNames = incomingMessage.ReadString();
			username = incomingMessage.ReadString();
			email = incomingMessage.ReadString();
			password = incomingMessage.ReadString();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( name );
			outgoingMessage.Write( lastNames );
			outgoingMessage.Write( username );
			outgoingMessage.Write( email );
			outgoingMessage.Write( password );
		}

		public string name;
		public string lastNames;
		public string username;
		public string email;
		public string password;
	}
}
