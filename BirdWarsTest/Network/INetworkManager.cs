using Lidgren.Network;
using System;

namespace BirdWarsTest.Network
{
	public interface INetworkManager : IDisposable
	{
		void Connect();
		void Disconnect();
		NetIncomingMessage ReadMessage();
		void Recycle( NetIncomingMessage im );
		NetOutgoingMessage CreateMessage();
	}
}