using BirdWarsTest.Database;
using BirdWarsTest.Network.Messages;
using BirdWarsTest.States;
using Lidgren.Network;
using System;

namespace BirdWarsTest.Network
{
	public class ServerNetworkManager : INetworkManager
	{
		public ServerNetworkManager()
		{
			isLoggedIn = false;
			Connect();
		}

		public void Login( string email, string password )
		{
			User tempUser = users.Read( email, password );
			if( tempUser != null )
			{
				isLoggedIn = true;
				Console.WriteLine( "Welcome " + tempUser.names + "!" );
			}
			else
			{
				Console.WriteLine( "Invalid Credentials" );
			}
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

		public User GetUser( string email, string password )
		{
			return users.Read( email, password );
		}

		public NetConnectionStatus GetConnectionState()
		{
			return NetConnectionStatus.None;
		}

		public bool IsHost()
		{
			return true;
		}

		public void ProcessMessages( StateHandler handler )
		{
			NetIncomingMessage incomingMessage;
			while ( ( incomingMessage = ReadMessage() ) != null )
			{
				switch( incomingMessage.MessageType )
				{
					case NetIncomingMessageType.ConnectionApproval:
						CheckLogin( incomingMessage );
						break;

					case NetIncomingMessageType.VerboseDebugMessage:
					case NetIncomingMessageType.DebugMessage:
					case NetIncomingMessageType.WarningMessage:
					case NetIncomingMessageType.ErrorMessage:
						Console.WriteLine( incomingMessage.ReadString() );
						break;
					case NetIncomingMessageType.StatusChanged:
						switch ( ( NetConnectionStatus )incomingMessage.ReadByte() )
						{
							case NetConnectionStatus.Connected:
								Console.WriteLine("{0} Connected", incomingMessage.SenderEndPoint);
								break;
							case NetConnectionStatus.Disconnected:
								Console.WriteLine( "{0} Disconnected", incomingMessage.SenderEndPoint );
								break;
							case NetConnectionStatus.RespondedAwaitingApproval:
								break;
						}
						break;
				}
				Recycle( incomingMessage );
			}
		}

		private void CheckLogin( NetIncomingMessage incomingMessage )
		{
			if( users.Read( incomingMessage.ReadString(), 
							incomingMessage.ReadString() ) != null )
			{
				NetOutgoingMessage outgoingMessage = CreateMessage();
				outgoingMessage.Write( "Login credentials approved!" );

				incomingMessage.SenderConnection.Approve( outgoingMessage );
			}
			else
			{
				incomingMessage.SenderConnection.Deny( "Invalid Credentials" );
			}
		}

		public UserDAO users = new UserDAO();
		private NetServer netServer;
		public bool isLoggedIn;
		private bool isDisposed;
	}
}