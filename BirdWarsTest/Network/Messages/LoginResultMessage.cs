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
			user.UserId = incomingMessage.ReadInt32();
			user.Names = incomingMessage.ReadString();
			user.LastName = incomingMessage.ReadString();
			user.Username = incomingMessage.ReadString();
			user.Email = incomingMessage.ReadString();
			user.Password = incomingMessage.ReadString();
		}

		private void GetAccountInfo( NetIncomingMessage incomingMessage )
		{
			userAccount.AccountId = incomingMessage.ReadInt32();
			userAccount.UserId = incomingMessage.ReadInt32();
			userAccount.TotalMatchesPlayed = incomingMessage.ReadInt32();
			userAccount.MatchesWon = incomingMessage.ReadInt32();
			userAccount.MatchesSurvived = incomingMessage.ReadInt32();
			userAccount.MatchesLost = incomingMessage.ReadInt32();
			userAccount.Money = incomingMessage.ReadInt32();
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
			outgoingMessage.Write( user.UserId );
			outgoingMessage.Write( user.Names );
			outgoingMessage.Write( user.LastName );
			outgoingMessage.Write( user.Username );
			outgoingMessage.Write( user.Email );
			outgoingMessage.Write( user.Password );
		}

		private void SetAccountInfo( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( userAccount.AccountId );
			outgoingMessage.Write( userAccount.UserId );
			outgoingMessage.Write( userAccount.TotalMatchesPlayed );
			outgoingMessage.Write( userAccount.MatchesWon );
			outgoingMessage.Write( userAccount.MatchesSurvived );
			outgoingMessage.Write( userAccount.MatchesLost );
			outgoingMessage.Write( userAccount.Money );
		}

		private bool loginRequestResult;
		private string reason;
		private User user;
		private Account userAccount;
	}
}