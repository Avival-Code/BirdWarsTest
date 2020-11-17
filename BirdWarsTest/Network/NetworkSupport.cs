using System;
using Lidgren.Network;
using BirdWarsTest.Database;
using BirdWarsTest.States;

namespace BirdWarsTest.Network
{
	class NetworkSupport
	{
		public void ProcessMessages( INetworkManager networkManager, StateHandler handler )
		{
			NetIncomingMessage incomingMessage;
			while( ( incomingMessage = networkManager.ReadMessage() ) != null )
			{
				switch( incomingMessage.MessageType )
				{
					case NetIncomingMessageType.ConnectionApproval:
						CheckLogin( networkManager, incomingMessage );
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
								if( !networkManager.IsHost() )
								{
									if( incomingMessage.SenderConnection.RemoteHailMessage != null )
									Console.WriteLine( "The remote hail message is not null" );
								}
								Console.WriteLine("{0} Connected", incomingMessage.SenderEndPoint);
								break;
							case NetConnectionStatus.Disconnected:
								Console.WriteLine( "{0} Disconnected", incomingMessage.SenderEndPoint );
								break;
							case NetConnectionStatus.RespondedAwaitingApproval:
								incomingMessage.SenderConnection.Approve();
								break;
						}
						break;
				}
				networkManager.Recycle( incomingMessage );
			}
		}

		public void CheckLogin( INetworkManager networkManager, NetIncomingMessage incomingMessage )
		{
			if( networkManager.IsHost() )
			{
				User tempUser = networkManager.GetUser( incomingMessage.ReadString(),
												        incomingMessage.ReadString() );
				if( tempUser != null )
				{
					NetOutgoingMessage outgoingMessage = networkManager.CreateMessage();
					outgoingMessage.Write( "Testing" );

					incomingMessage.SenderConnection.Approve( outgoingMessage );
				}
				else
				{
					incomingMessage.SenderConnection.Deny("Invalid Credencials");
				}
			}
		}
	}
}
