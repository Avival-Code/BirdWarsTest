using BirdWarsTest.Database;
using BirdWarsTest.Network.Messages;
using Lidgren.Network;
using System;

namespace BirdWarsTest.Network
{
	public class ServerNetworkManager : INetworkManager
	{
		public bool Login( string email, string password )
		{
			bool LoggedIn = false;
			User tempUser = users.Read( email, password );
			if( tempUser != null )
			{
				Connect();
				LoggedIn = true;
				Console.WriteLine( "Welcome " + tempUser.names + "!" );
			}
			else
			{
				Console.WriteLine( "Invalid Credentials" );
			}

			return LoggedIn;
		}

		public void Connect()
		{
			var config = new NetPeerConfiguration( "BirdWars" )
			{
				Port = Convert.ToInt32( "14242" ),
				//SimulatedMinimumLatency = 0.2f,
				//SimulatedLoss = 0.1f
			};

			config.EnableMessageType( NetIncomingMessageType.WarningMessage );
			config.EnableMessageType( NetIncomingMessageType.VerboseDebugMessage );
			config.EnableMessageType( NetIncomingMessageType.ErrorMessage );
			config.EnableMessageType( NetIncomingMessageType.Error );
			config.EnableMessageType( NetIncomingMessageType.DebugMessage );
			config.EnableMessageType( NetIncomingMessageType.ConnectionApproval );

			netServer = new NetServer( config );
			netServer.Start();
		}

		public NetOutgoingMessage CreateMessage()
		{
			return netServer.CreateMessage();
		}

		public void Disconnect()
		{
			netServer.Shutdown( "Bye" );
		}

		public void Dispose()
		{
			this.Dispose( true );
		}

		public NetIncomingMessage ReadMessage()
		{
			return netServer.ReadMessage();
		}

		public void Recycle( NetIncomingMessage im )
		{
			netServer.Recycle( im );
		}

		public void SendMessage( IGameMessage gameMessage )
		{
			NetOutgoingMessage outgoingMessage = netServer.CreateMessage();
			outgoingMessage.Write( ( byte )gameMessage.messageType );
			gameMessage.Encode( outgoingMessage );

			netServer.SendToAll( outgoingMessage, NetDeliveryMethod.ReliableUnordered );
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

		private UserDAO users = new UserDAO();
		private NetServer netServer;
		private bool isDisposed;
	}
}