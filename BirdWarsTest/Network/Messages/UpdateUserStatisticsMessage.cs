using BirdWarsTest.Database;
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	class UpdateUserStatisticsMessage : IGameMessage
	{
		public UpdateUserStatisticsMessage( NetIncomingMessage incomingMessage )
		{
			User = new User();
			Account = new Account();
			Decode( incomingMessage );
		}

		public UpdateUserStatisticsMessage( User userIn, Account accountIn )
		{
			User = new User( userIn );
			Account = new Account( accountIn );
		}

		public GameMessageTypes messageType
		{
			get { return GameMessageTypes.UpdateUserStatisticsMessage; }
		}

		public void Decode( NetIncomingMessage incomingMessage )
		{
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

		public User User { get; private set; }
		public Account Account { get; private set; }
	}
}