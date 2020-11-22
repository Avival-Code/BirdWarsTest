﻿using BirdWarsTest.Database;
using BirdWarsTest.Network.Messages;
using BirdWarsTest.States;
using Lidgren.Network;
using System;
using System.Net;

namespace BirdWarsTest.Network
{
	public class ClientNetworkManager : INetworkManager
	{
		public ClientNetworkManager()
		{
			userSession = new LoginSession();
			Connect();
		}

		public void Login( string email, string password )
		{
			SendMessage( new LoginRequestMessage( email, password ) );
		}

		public void Logout()
		{
			userSession.Logout();
		}

		public void Connect()
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

			netClient = new NetClient(config);
			netClient.Start();

			netClient.Connect( new IPEndPoint( NetUtility.Resolve( "127.0.0.1" ), Convert.ToInt32( "14242" ) ) );
		}

		public void Connect( string email, string password ) { }

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

		public User GetUser( string email, string password ) { return null; }

		public NetConnectionStatus GetConnectionState()
		{
			return netClient.ConnectionStatus;
		}

		public bool IsHost()
		{
			return false;
		}

		public void ProcessMessages( StateHandler handler )
		{
			NetIncomingMessage incomingMessage;
			while( ( incomingMessage = ReadMessage() ) != null )
			{
				switch( incomingMessage.MessageType )
				{
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
								var message = incomingMessage.SenderConnection.RemoteHailMessage;
								if( message != null )
								{
									Console.WriteLine( message.ReadString() );
									handler.ChangeState( StateTypes.MainMenuState );
								}
								break;
							case NetConnectionStatus.Disconnected:
								Console.WriteLine( incomingMessage.ReadString() );
								Console.WriteLine( "{0} Disconnected", incomingMessage.SenderEndPoint );
								break;
							case NetConnectionStatus.RespondedAwaitingApproval:
								break;
						}
						break;
					case NetIncomingMessageType.Data:
						var gameMessageType = ( GameMessageTypes )incomingMessage.ReadByte();
						switch( gameMessageType )
						{
							case GameMessageTypes.LoginResultMessage:
								HandleLoginResultMessage( incomingMessage, handler );
								break;
							case GameMessageTypes.JoinRoundRequestResultMessage:
								HandleJoinRoundRequestResultMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.RoundStateChangedMessage:
								HandleRoundStateChangedMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.ChatMessage:
								HandleChatMessage( handler, incomingMessage );
								break;
						}
						break;
				}
				Recycle( incomingMessage );
			}
		}

		public void RegisterUser( string nameIn, string lastNameIn, string usernameIn, string emailIn, string passwordIn )
		{
			SendMessage( new RegisterUserMessage( new User( nameIn, lastNameIn, usernameIn, emailIn, passwordIn ) ) );
		}

		private void HandleLoginResultMessage( NetIncomingMessage incomingMessage, StateHandler handler )
		{
			var loginResult = incomingMessage.ReadBoolean();
			if( loginResult )
			{
				incomingMessage.ReadString();
				userSession.Login( new User( incomingMessage.ReadInt32(), incomingMessage.ReadString(), incomingMessage.ReadString(),
											 incomingMessage.ReadString(), incomingMessage.ReadString(), incomingMessage.ReadString() ),
								   new Account( incomingMessage.ReadInt32(), incomingMessage.ReadInt32(), incomingMessage.ReadInt32(), 
												incomingMessage.ReadInt32(), incomingMessage.ReadInt32(), incomingMessage.ReadInt32(), 
												incomingMessage.ReadInt32() ) );
				handler.ChangeState( StateTypes.MainMenuState );
			}
			Console.WriteLine( incomingMessage.ReadString() );
		}

		private void HandleJoinRoundRequestResultMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			if( incomingMessage.ReadBoolean() )
			{
				handler.ChangeState( StateTypes.WaitingRoomState );
				( ( WaitingRoomState )handler.GetCurrentState() ).UsernameManager.HandleRoundStateChangeMessage( incomingMessage );
			}
		}

		private void HandleRoundStateChangedMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			( ( WaitingRoomState )handler.GetCurrentState() ).UsernameManager.HandleRoundStateChangeMessage( incomingMessage );
		}

		private void HandleChatMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			( ( WaitingRoomState )handler.GetCurrentState() ).MessageManager.
				HandleChatMessage( incomingMessage.ReadString(), incomingMessage.ReadString(), userSession.currentUser.username );
		}

		public void CreateRound() {}
		
		public void JoinRound()
		{
			SendMessage( new JoinRoundRequestMessage( userSession.currentUser.username ) );
		}

		public void SendChatMessage( string message )
		{
			SendMessage( new ChatMessage( userSession.currentUser.username, message ) );
		}

		public LoginSession GetLoginSession()
		{
			return userSession;
		}

		private NetClient netClient;
		private LoginSession userSession;
		private bool isDisposed;
	}
}