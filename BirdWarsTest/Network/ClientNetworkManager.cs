using BirdWarsTest.Network.Messages;
using Lidgren.Network;
using System;
using System.Net;

namespace BirdWarsTest.Network
{
	public class ClientNetworkManager : INetworkManager
	{
		public bool Login( string username, string password )
		{
			bool loggedIn = false;
			return loggedIn;
		}
		public void Connect( string username, string password )
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

			netClient = new NetClient( config );
			netClient.Start();

			netClient.Connect( new IPEndPoint( NetUtility.Resolve( "127.0.0.1" ), Convert.ToInt32( "14242" ) ) );
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

		public void Connect()
		{
			throw new NotImplementedException();
		}

		private NetClient netClient;
		private bool isDisposed;
	}
}
