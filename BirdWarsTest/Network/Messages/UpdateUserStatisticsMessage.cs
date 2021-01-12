/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent to server to update the user account information.
*********************************************/
using BirdWarsTest.Database;
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent to server to update the user account information.
	/// </summary>
	public class UpdateUserStatisticsMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public UpdateUserStatisticsMessage( NetIncomingMessage incomingMessage )
		{
			User = new User();
			Account = new Account();
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates a essage from a user and their associated account.
		/// </summary>
		/// <param name="userIn">User</param>
		/// <param name="accountIn">Account</param>
		public UpdateUserStatisticsMessage( User userIn, Account accountIn )
		{
			User = new User( userIn );
			Account = new Account( accountIn );
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get { return GameMessageTypes.UpdateUserStatisticsMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
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

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
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

		///<value>The target user</value>
		public User User { get; private set; }

		///<value>The user's associated account</value>
		public Account Account { get; private set; }
	}
}