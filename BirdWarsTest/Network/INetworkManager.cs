using Lidgren.Network;
using BirdWarsTest.Database;
using BirdWarsTest.States;
using System;

namespace BirdWarsTest.Network
{
	public interface INetworkManager : IDisposable
	{
		void Login( string email, string password );

		void RegisterUser( string nameIn, string lastNameIn, string usernameIn,
						   string emailIn, string passwordIn );

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

		void JoinRound();

		void SendChatMessage( string message );
	}
}