﻿using BirdWarsTest.States;
using BirdWarsTest.Database;
using BirdWarsTest.Network.Messages;
using BirdWarsTest.GameRounds;
using Lidgren.Network;
using System;

namespace BirdWarsTest.Network
{
	public class ServerNetworkManager : INetworkManager
	{
		public ServerNetworkManager()
		{
			gameDatabase = new GameDatabase();
			gameRound = new GameRound();
			userSession = new LoginSession();
			Connect();
		}

		public void Login( string email, string password )
		{
			if( gameDatabase.users.Read( email, password ) != null )
			{
				userSession.Login( gameDatabase.users.Read( email, password ) );
				Console.WriteLine( "Login credentials approved." );
			}
			else
			{
				Console.WriteLine( "Invalid login credentials" );
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
			config.EnableMessageType( NetIncomingMessageType.UnconnectedData );

			netServer = new NetServer( config );
			netServer.Start();

			foreach( var connection in netServer.Connections )
			Console.WriteLine( connection );
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
								Console.WriteLine( "{0} Connected", incomingMessage.SenderEndPoint );
								break;
							case NetConnectionStatus.Disconnected:
								Console.WriteLine( "{0} Disconnected", incomingMessage.SenderEndPoint );
								break;
							case NetConnectionStatus.RespondedAwaitingApproval:
								incomingMessage.SenderConnection.Approve();
								break;
						}
						break;
					case NetIncomingMessageType.UnconnectedData:
						var unconnectedMessageType = ( GameMessageTypes )incomingMessage.ReadByte();
						switch( unconnectedMessageType )
						{
							case GameMessageTypes.RoundCreatedMessage:
								HandleRoundCreatedMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.RoundStateChangedMessage:
								HandleRoundStateChangedMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.ChatMessage:
								HandleChatMessage( handler, incomingMessage );
								break;
						}
						break;
					case NetIncomingMessageType.Data:
						var gameMessageType = ( GameMessageTypes )incomingMessage.ReadByte();
						switch( gameMessageType )
						{
							case GameMessageTypes.LoginRequestMessage:
								HandleLoginRequestMessages( incomingMessage );
								break;
							case GameMessageTypes.registerUserMessage:
								gameDatabase.users.Create( new User( incomingMessage.ReadString(), incomingMessage.ReadString(),
														   incomingMessage.ReadString(), incomingMessage.ReadString(),
														   incomingMessage.ReadString() ) );
								NetOutgoingMessage outgoingMessage = CreateMessage();
								outgoingMessage.Write( "Registration successfull." );
								incomingMessage.SenderConnection.SendMessage( outgoingMessage, 
										           NetDeliveryMethod.ReliableUnordered, incomingMessage.SequenceChannel );
								break;
							case GameMessageTypes.JoinRoundRequestMessage:
								HandleJoinRoundRequestMessage( incomingMessage );
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

		private void HandleLoginRequestMessages( NetIncomingMessage incomingMessage )
		{
			var user = gameDatabase.users.Read( incomingMessage.ReadString(),
											    incomingMessage.ReadString() );
			if( user != null )
			{
				LoginResultMessage loginResult = new LoginResultMessage( true, "Login Approved", user );
				NetOutgoingMessage outgoingMessage = CreateMessage();
				outgoingMessage.Write( ( byte )loginResult.messageType );
				loginResult.Encode( outgoingMessage );

				netServer.SendMessage( outgoingMessage, incomingMessage.SenderConnection, 
									   NetDeliveryMethod.ReliableUnordered );
			}
			else
			{
				LoginResultMessage loginResult = new LoginResultMessage( false, "Login credentials invalid", new User() );
				NetOutgoingMessage outgoingMessage = CreateMessage();
				outgoingMessage.Write( ( byte )loginResult.messageType );
				loginResult.Encode( outgoingMessage );

				netServer.SendMessage( outgoingMessage, incomingMessage.SenderConnection,
									   NetDeliveryMethod.ReliableUnordered );
			}
		}

		private void HandleRoundStateChangedMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			( ( WaitingRoomState )handler.GetCurrentState() ).UsernameManager.HandleRoundStateChangeMessage( incomingMessage );

			RoundStateChangedMessage newRoundState = new RoundStateChangedMessage( gameRound.GetPlayerUsernames() );
			NetOutgoingMessage updateMessage = CreateMessage();
			updateMessage.Write( ( byte )newRoundState.messageType );
			newRoundState.Encode( updateMessage );

			foreach( var connection in gameRound.PlayerConnections )
			{
				netServer.SendMessage( updateMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		private void HandleRoundCreatedMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			Console.WriteLine( "Recieved Create round message." );
			if( incomingMessage.ReadBoolean() )
			{
				handler.ChangeState( StateTypes.WaitingRoomState );
				( ( WaitingRoomState )handler.GetCurrentState() ).UsernameManager.HandleRoundCreatedMessage( incomingMessage );
			}
		}

		private void HandleJoinRoundRequestMessage( NetIncomingMessage incomingMessage )
		{
			if( gameRound.Created && gameRound.RoomAvailable() )
			{
				gameRound.AddPlayer( incomingMessage.ReadString(), incomingMessage.SenderConnection );
				RoundStateChangedMessage newRoundState = new RoundStateChangedMessage( gameRound.GetPlayerUsernames() );
				NetOutgoingMessage updateMessage = CreateMessage();
				updateMessage.Write( ( byte )newRoundState.messageType );
				newRoundState.Encode( updateMessage );

				netServer.SendUnconnectedToSelf( updateMessage );

				JoinRoundRequestResultMessage resultMessage = new JoinRoundRequestResultMessage( true, gameRound.GetPlayerUsernames() );
				NetOutgoingMessage outgoingMessage = CreateMessage();
				outgoingMessage.Write( ( byte )resultMessage.messageType );
				resultMessage.Encode( outgoingMessage );

				netServer.SendMessage( outgoingMessage, incomingMessage.SenderConnection, 
									   NetDeliveryMethod.ReliableUnordered );
			}
		}

		private void HandleChatMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			string username = incomingMessage.ReadString();
			string message = incomingMessage.ReadString();
			( ( WaitingRoomState )handler.GetCurrentState() ).MessageManager.HandleChatMessage(
					username, message, userSession.currentUser.username
				);

			ChatMessage newMessage = new ChatMessage( username, message );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )newMessage.messageType );
			newMessage.Encode( outgoingMessage );

			foreach( var connection in gameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		public void RegisterUser( string nameIn, string lastNameIn, string usernameIn, string emailIn, string passwordIn )
		{
			gameDatabase.users.Create( new User( nameIn, lastNameIn, usernameIn, emailIn, passwordIn ) );
		}

		public void CreateRound()
		{
			gameRound.CreateRound( userSession.currentUser.username );
			RoundCreatedMessage newRound = new RoundCreatedMessage( true, gameRound.GetPlayerUsernames() );
			NetOutgoingMessage updateMessage = CreateMessage();
			updateMessage.Write( ( byte )newRound.messageType );
			newRound.Encode( updateMessage );

			netServer.SendUnconnectedToSelf( updateMessage );
		}

		public void JoinRound() {}

		public void SendChatMessage( string message )
		{
			ChatMessage chatMessage = new ChatMessage( userSession.currentUser.username, message );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )chatMessage.messageType );
			chatMessage.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		private GameDatabase gameDatabase;
		private NetServer netServer;
		private GameRound gameRound;
		public LoginSession userSession;
		private bool isDisposed;
	}
}