using Lidgren.Network;
using BirdWarsTest.Database;

namespace BirdWarsTest.Network.Messages
{
	public class LoginResultMessage : IGameMessage
	{
		public LoginResultMessage( NetIncomingMessage incomingMessage )
		{
			User = new User();
			Account = new Account();
			Decode( incomingMessage );
		}

		public LoginResultMessage( bool result, string reasonIn, User userIn, Account userAccountIn )
		{
			User = new User();
			Account = new Account();
			LoginRequestResult = result;
			Reason = reasonIn;
			User = userIn;
			Account = userAccountIn;
		}

		public GameMessageTypes messageType
		{
			get{ return GameMessageTypes.LoginResultMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
			LoginRequestResult = incomingMessage.ReadBoolean();
			Reason = incomingMessage.ReadString();
			GetUserInfo( incomingMessage );
			GetAccountInfo( incomingMessage );
		}

		private void GetUserInfo( NetIncomingMessage incomingMessage )
		{
			User.UserId = incomingMessage.ReadInt32();
			User.Names = incomingMessage.ReadString();
			User.LastName = incomingMessage.ReadString();
			User.Username = incomingMessage.ReadString();
			User.Email = incomingMessage.ReadString();
			User.Password = incomingMessage.ReadString();
		}

		private void GetAccountInfo( NetIncomingMessage incomingMessage )
		{
			Account.AccountId = incomingMessage.ReadInt32();
			Account.UserId = incomingMessage.ReadInt32();
			Account.TotalMatchesPlayed = incomingMessage.ReadInt32();
			Account.MatchesWon = incomingMessage.ReadInt32();
			Account.MatchesSurvived = incomingMessage.ReadInt32();
			Account.MatchesLost = incomingMessage.ReadInt32();
			Account.Money = incomingMessage.ReadInt32();
		}

		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( LoginRequestResult );
			outgoingMessage.Write( Reason );
			SetUserInfo( outgoingMessage );
			SetAccountInfo( outgoingMessage );
		}

		private void SetUserInfo( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( User.UserId );
			outgoingMessage.Write( User.Names );
			outgoingMessage.Write( User.LastName );
			outgoingMessage.Write( User.Username );
			outgoingMessage.Write( User.Email );
			outgoingMessage.Write( User.Password );
		}

		private void SetAccountInfo( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( Account.AccountId );
			outgoingMessage.Write( Account.UserId );
			outgoingMessage.Write( Account.TotalMatchesPlayed );
			outgoingMessage.Write( Account.MatchesWon );
			outgoingMessage.Write( Account.MatchesSurvived );
			outgoingMessage.Write( Account.MatchesLost );
			outgoingMessage.Write( Account.Money );
		}

		public bool LoginRequestResult { get; private set; }
		public string Reason { get; private set; }
		public User User { get; private set; }
		public Account Account { get; private set; }
	}
}