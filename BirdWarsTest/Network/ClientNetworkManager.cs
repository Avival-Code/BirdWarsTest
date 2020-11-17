using BirdWarsTest.Database;
using BirdWarsTest.Network.Messages;
using Lidgren.Network;
using System;
using System.Net;

namespace BirdWarsTest.Network
{
	public class ClientNetworkManager : INetworkManager
	{
		public ClientNetworkManager()
		{
			var config = new NetPeerConfiguration( "BirdWars" )
			{
				//SimulatedMinimumLatency = 0.2f;
				//SimulatedLoss = 0.1f;
			};

			config.EnableMessageType( NetIncomingMessageType.WarningMessage );
			config.EnableMessageType( NetIncomingMessageType.VerboseDebugMessage );
			config.EnableMessageType( NetIncomingMessageType.ErrorMessage );
			config.EnableMessageType( NetIncomingMessageType.Error );
			config.EnableMessageType( NetIncomingMessageType.DebugMessage );
			config.EnableMessageType( NetIncomingMessageType.ConnectionApproval );
			config.EnableMessageType( NetIncomingMessageType.StatusChanged );

			netClient = new NetClient( config );
		}

		public void Login( string email, string password )
		{
			Connect( email, password );
		}

		public void Connect() {}

		public void Connect( string email, string password )
		{
			netClient.Start();
			NetOutgoingMessage approval = CreateMessage();
			approval.Write( email );
			approval.Write( password );

			netClient.Connect( new IPEndPoint( NetUtility.Resolve( "127.0.0.1" ), Convert.ToInt32( "14242" ) ), approval );
		}

		public NetOutgoingMessage CreateMessage()
		{
			return netClient.CreateMessage();
		}

		public void Disconnect()
		{
			netClient.Disconnect( "Bye" );
		}

		public void Dispose()
		{
			Dispose( true );
		}

		public NetIncomingMessage ReadMessage()
		{
			return netClient.ReadMessage();
		}

		public void Recycle( NetIncomingMessage im )
		{
			netClient.Recycle( im );
		}

		public void SendMessage( IGameMessage gameMessage )
		{
			NetOutgoingMessage outgoingMessage = netClient.CreateMessage();
			outgoingMessage.Write( ( byte )gameMessage.messageType );
			gameMessage.Encode( outgoingMessage );

			netClient.SendMessage( outgoingMessage, NetDeliveryMethod.ReliableUnordered );
		}

		private void Dispose( bool disposing )
		{
			if( !isDisposed )
			{
				if( disposing )
				{
					Disconnect();
				}
				isDisposed = true;
			}
		}

		public User GetUser(string email, string password) { return null; }

		public NetConnectionStatus GetConnectionState()
		{
			return netClient.ConnectionStatus;
		}

		public bool IsHost()
		{
			return false;
		}

		private NetClient netClient;
		private bool isDisposed;
	}
}
