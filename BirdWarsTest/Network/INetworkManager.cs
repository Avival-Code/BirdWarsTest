using Lidgren.Network;
using BirdWarsTest.Database;
using BirdWarsTest.States;
using BirdWarsTest.GameObjects;
using System;

namespace BirdWarsTest.Network
{
	public interface INetworkManager : IDisposable
	{
		void Login( string email, string password );

		void Logout();

		void RegisterUser( string nameIn, string lastNameIn, string usernameIn,
						   string emailIn, string passwordIn );

		void SendPasswordChangeMessage( string emailIn );

		void SendChatMessage( string message );

		void SendPlayerStateChangeMessage( GameObject player );

		void UpdatePassword( string code, string password );

		bool IsHost();

		void Connect();

		void Disconnect();

		NetIncomingMessage ReadMessage();

		void Recycle( NetIncomingMessage im );

		NetOutgoingMessage CreateMessage();

		void ProcessMessages( StateHandler handler );

		NetConnectionStatus GetConnectionState();

		LoginSession GetLoginSession();

		void CreateRound();

		void StartRound();

		void JoinRound();

		void LeaveRound();
	}
}