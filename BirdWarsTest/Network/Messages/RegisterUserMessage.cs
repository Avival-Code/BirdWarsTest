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
			name = newUser.Names;
			lastNames = newUser.LastName;
			username = newUser.Username;
			email = newUser.Email;
			password = newUser.Password;
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

		private string name;
		private string lastNames;
		private string username;
		private string email;
		private string password;
	}
}
