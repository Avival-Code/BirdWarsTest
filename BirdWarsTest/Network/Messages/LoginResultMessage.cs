/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message sent to the client by the server telling it if
login was approved or denied.
*********************************************/
using Lidgren.Network;
using BirdWarsTest.Database;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message sent to the client by the server telling it if
	/// login was approved or denied.
	/// </summary>
	public class LoginResultMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public LoginResultMessage( NetIncomingMessage incomingMessage )
		{
			User = new User();
			Account = new Account();
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates the message from the login result bool, the reason of 
		/// failed login, the user and account of the approved login credentials.
		/// </summary>
		/// <param name="result">Approved or denied bool</param>
		/// <param name="reasonIn">The reason why request was denied</param>
		/// <param name="userIn">User</param>
		/// <param name="userAccountIn">Account</param>
		public LoginResultMessage( bool result, string reasonIn, User userIn, Account userAccountIn )
		{
			User = new User();
			Account = new Account();
			LoginRequestResult = result;
			Reason = reasonIn;
			User = userIn;
			Account = userAccountIn;
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get{ return GameMessageTypes.LoginResultMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
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

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
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

		///<value>The result of the login request</value>
		public bool LoginRequestResult { get; private set; }

		///<value>The reason for denial (if denied)</value>
		public string Reason { get; private set; }

		///<value>The user of the login credentials</value>
		public User User { get; private set; }

		///<value>The account of the login credentials</value>
		public Account Account { get; private set; }
	}
}