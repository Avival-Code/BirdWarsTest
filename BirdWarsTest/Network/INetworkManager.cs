using Lidgren.Network;
using BirdWarsTest.Database;
using BirdWarsTest.States;
using System;

namespace BirdWarsTest.Network
{
	public interface INetworkManager : IDisposable
	{
		void Login( string email, string password );

		bool IsHost();

		void Connect();

		void Disconnect();

		NetIncomingMessage ReadMessage();

		void Recycle( NetIncomingMessage im );

		NetOutgoingMessage CreateMessage();

		void ProcessMessages( StateHandler handler );

		User GetUser( string email, string password );

		NetConnectionStatus GetConnectionState();
	}
}