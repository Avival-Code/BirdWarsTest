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

		public LoginResultMessage( bool result, string reasonIn, User userIn, Account userAccountIn )
		{
			user = new User();
			loginRequestResult = result;
			reason = reasonIn;
			user = userIn;
			userAccount = userAccountIn;
		}

		public GameMessageTypes messageType
		{
			get{ return GameMessageTypes.LoginResultMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			loginRequestResult = incomingMessage.ReadBoolean();
			reason = incomingMessage.ReadString();
			GetUserInfo( incomingMessage );
			GetAccountInfo( incomingMessage );
		}

		private void GetUserInfo( NetIncomingMessage incomingMessage )
		{
			user.userId = incomingMessage.ReadInt32();
			user.names = incomingMessage.ReadString();
			user.lastName = incomingMessage.ReadString();
			user.username = incomingMessage.ReadString();
			user.email = incomingMessage.ReadString();
			user.password = incomingMessage.ReadString();
		}

		private void GetAccountInfo( NetIncomingMessage incomingMessage )
		{
			userAccount.accountId = incomingMessage.ReadInt32();
			userAccount.userId = incomingMessage.ReadInt32();
			userAccount.totalMatchesPlayed = incomingMessage.ReadInt32();
			userAccount.matchesWon = incomingMessage.ReadInt32();
			userAccount.matchesSurvived = incomingMessage.ReadInt32();
			userAccount.matchesLost = incomingMessage.ReadInt32();
			userAccount.money = incomingMessage.ReadInt32();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( loginRequestResult );
			outgoingMessage.Write( reason );
			SetUserInfo( outgoingMessage );
			SetAccountInfo( outgoingMessage );
		}

		private void SetUserInfo( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( user.userId );
			outgoingMessage.Write( user.names );
			outgoingMessage.Write( user.lastName );
			outgoingMessage.Write( user.username );
			outgoingMessage.Write( user.email );
			outgoingMessage.Write( user.password );
		}

		private void SetAccountInfo( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( userAccount.accountId );
			outgoingMessage.Write( userAccount.userId );
			outgoingMessage.Write( userAccount.totalMatchesPlayed );
			outgoingMessage.Write( userAccount.matchesWon );
			outgoingMessage.Write( userAccount.matchesSurvived );
			outgoingMessage.Write( userAccount.matchesLost );
			outgoingMessage.Write( userAccount.money );
		}

		public bool loginRequestResult;
		public string reason;
		private User user;
		private Account userAccount;
	}
}