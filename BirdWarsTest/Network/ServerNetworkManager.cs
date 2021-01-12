using BirdWarsTest.States;
using BirdWarsTest.Database;
using BirdWarsTest.Network.Messages;
/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
The server network manager handles distributing messages
to all clients, their messages, and user login, registration and
update.
*********************************************/
using BirdWarsTest.GameRounds;
using BirdWarsTest.GameObjects;
using BirdWarsTest.Utilities;
using Lidgren.Network;
using System;
using System.Collections.Generic;

namespace BirdWarsTest.Network
{
	/// <summary>
	/// The server network manager handles distributing messages
	/// to all clients, their messages, and user login, registration and
	/// update.
	/// </summary>
	public class ServerNetworkManager : INetworkManager
	{
		/// <summary>
		/// Configures the server network manager and instanciates
		/// all necessary classes.
		/// </summary>
		public ServerNetworkManager()
		{
			Connect();
			gameDatabase = new GameDatabase();
			GameRound = new GameRound();
			emailManager = new EmailManager();
			ChangeManager = new PasswordChangeManager();
			UserSession = new LoginSession();
		}

		/// <summary>
		/// Sends a loginRequest Message to self for processing.
		/// </summary>
		/// <param name="email">User email</param>
		/// <param name="password">User password</param>
		public void Login( string email, string password )
		{
			LoginRequestMessage loginMessage = new LoginRequestMessage( email, password );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )loginMessage.MessageType );
			loginMessage.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		/// <summary>
		/// Closes the current user session.
		/// </summary>
		public void Logout()
		{
			UserSession.Logout();
		}

		/// <summary>
		/// Method unused by setver network manager.
		/// This IS the server.
		/// </summary>
		/// <param name="handler"></param>
		/// <param name="address"></param>
		/// <param name="port"></param>
		public void ConnectToSpecificServer( StateHandler handler, string address, string port )
		{
			( ( OptionsState )handler.GetState( StateTypes.OptionsState ) ).SetMessage( 
							  handler.StringManager.GetString( StringNames.IAmTheServer ) );
		}

		/// <summary>
		/// Returns current user session.
		/// </summary>
		/// <returns>Returns current user session.</returns>
		public LoginSession GetLoginSession()
		{
			return UserSession;
		}

		/// <summary>
		/// Configures the server network manager and starts it.
		/// </summary>
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
		}

		/// <summary>
		/// Creates and return a NetOutgoingMessage
		/// </summary>
		/// <returns></returns>
		public NetOutgoingMessage CreateMessage()
		{
			return netServer.CreateMessage();
		}

		/// <summary>
		/// Disconnects the server.
		/// </summary>
		public void Disconnect()
		{
			netServer.Shutdown( "Bye" );
		}

		/// <summary>
		/// Framework method.
		/// </summary>
		public void Dispose()
		{
			this.Dispose( true );
		}

		/// <summary>
		/// Reads and returns any incoming messages.
		/// </summary>
		/// <returns></returns>
		public NetIncomingMessage ReadMessage()
		{
			return netServer.ReadMessage();
		}

		/// <summary>
		/// Recycles any consumed incoming Messages.
		/// </summary>
		/// <param name="im">The incoming message</param>
		public void Recycle( NetIncomingMessage im )
		{
			netServer.Recycle( im );
		}

		/// <summary>
		/// Sends a message to all client connections.
		/// </summary>
		/// <param name="gameMessage"></param>
		public void SendMessage( IGameMessage gameMessage )
		{
			NetOutgoingMessage outgoingMessage = netServer.CreateMessage();
			outgoingMessage.Write( ( byte )gameMessage.MessageType );
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

		/// <summary>
		/// Returns the netconnection status.
		/// </summary>
		/// <returns></returns>
		public NetConnectionStatus GetConnectionState()
		{
			return NetConnectionStatus.None;
		}

		/// <summary>
		/// Checks if the current instance is the host/server.
		/// </summary>
		/// <returns></returns>
		public bool IsHost()
		{
			return true;
		}

		/// <summary>
		/// Processes any incoming client or server messages.
		/// </summary>
		/// <param name="handler"></param>
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
							case GameMessageTypes.StartRoundMessage:
								HandleStartRoundMessage( handler );
								break;
							case GameMessageTypes.RoundFinishedMessage:
								HandleRoundFinishedMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.RegisterUserMessage:
								HandleSelfRegisterUserMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.LoginRequestMessage:
								HandleSelfLoginMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.SolicitPasswordResetMessage:
								HandleSelfSolicitPasswordResetMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.PasswordResetMessage:
								HandleSelfPasswordResetMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.ExitWaitingRoomMessage:
								HandleSelfExitWaitingRomMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.BanPlayerMessage:
								HandleBanMessage( handler, incomingMessage );
								break;
						}
						break;
					case NetIncomingMessageType.Data:
						var gameMessageType = ( GameMessageTypes )incomingMessage.ReadByte();
						switch( gameMessageType )
						{
							case GameMessageTypes.TestConnectionMessage:
								HandleTestConnectionMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.LoginRequestMessage:
								HandleLoginRequestMessages( handler, incomingMessage );
								break;
							case GameMessageTypes.RegisterUserMessage:
								HandleRegisterUserMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.JoinRoundRequestMessage:
								HandleJoinRoundRequestMessage( incomingMessage );
								break;
							case GameMessageTypes.ChatMessage:
								HandleChatMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.SolicitPasswordResetMessage:
								HandleSolicitPasswordResetMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.PasswordResetMessage:
								HandlePasswordResetMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.PlayerStateChangeMessage:
								HandlePlayerStateChangeMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.LeaveRoundMessage:
								HandleLeaveRoundMessage( incomingMessage );
								break;
							case GameMessageTypes.BoxDamageMessage:
								HandleBoxDamageMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.PlayerAttackMessage:
								HandlePlayerAttackMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.PickedUpItemMessage:
								HandlePickedUpItemMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.SpawnGrenadeMessage:
								HandleSpawnGrenadeMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.UpdateUserStatisticsMessage:
								HandleUpdateUserInformationMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.PlayerIsDeadMessage:
								HandlePlayerIsDeadMessage( handler, incomingMessage );
								break;
						}
						break;
				}
				Recycle( incomingMessage );
			}
		}

		private void HandleTestConnectionMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			TestConnectionMessage testMessage = new TestConnectionMessage( 
												handler.StringManager.GetString( StringNames.ConnectionSuccessful ) );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )testMessage.MessageType );
			testMessage.Encode( outgoingMessage );

			netServer.SendMessage( outgoingMessage, incomingMessage.SenderConnection, NetDeliveryMethod.ReliableUnordered );
		}

		private void HandleSelfLoginMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			LoginRequestMessage loginMessage = new LoginRequestMessage( incomingMessage );
			var user = gameDatabase.Users.Read( loginMessage.Email, loginMessage.Password );
			if( user != null && user.Password.Equals( loginMessage.Password ) )
			{
				UserSession.Login( user, gameDatabase.Accounts.Read( user.UserId ) );
				handler.ChangeState( StateTypes.MainMenuState );
			}
			else
			{
				( ( LoginState )handler.GetCurrentState() ).SetErrorMessage( 
							    handler.StringManager.GetString( StringNames.LoginDenied ) );
			}
		}

		private void HandleLoginRequestMessages( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			LoginRequestMessage loginMessage = new LoginRequestMessage( incomingMessage );
			var user = gameDatabase.Users.Read( loginMessage.Email, loginMessage.Password );
			LoginResultMessage loginResult;
			if( user != null && user.Password.Equals( loginMessage.Password ) )
			{
				loginResult = new LoginResultMessage( true, handler.StringManager.GetString( StringNames.LoginApproved ),
																		 user, gameDatabase.Accounts.Read( user.UserId ) );
			}
			else
			{
				loginResult = new LoginResultMessage( false, handler.StringManager.GetString( StringNames.LoginDenied ), 
																		 new User(), new Account() );
			}

			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )loginResult.MessageType );
			loginResult.Encode( outgoingMessage );

			netServer.SendMessage( outgoingMessage, incomingMessage.SenderConnection, NetDeliveryMethod.ReliableUnordered );
		}

		private void HandleSelfRegisterUserMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			RegisterUserMessage registerUser = new RegisterUserMessage( incomingMessage );
			if( gameDatabase.Users.Read( registerUser.User.Email ) == null )
			{
				if( !gameDatabase.DoesUsernameExist( registerUser.User.Username ) )
				{
					gameDatabase.Users.Create( registerUser.User );
					gameDatabase.Accounts.Create( new Account( 0, registerUser.User.UserId, 0, 0, 0, 0, 0, 300 ) );
					emailManager.SendEmailMessage( registerUser.User.Names, registerUser.User.Email,
												   handler.StringManager.GetString( StringNames.Registration ),
												   handler.StringManager.GetString( StringNames.EmailBodyMessage ) );
					( ( UserRegistryState )handler.GetCurrentState() ).SetMessage(
										   handler.StringManager.GetString( StringNames.RegistrationSuccessful ) );
					handler.GetCurrentState().ClearTextAreas();
				}
				else
				{
					( ( UserRegistryState )handler.GetCurrentState() ).SetErrorMessage(
									   handler.StringManager.GetString( StringNames.UsernameAlreadyExists ) );
				}
			}
			else
			{
				( ( UserRegistryState )handler.GetCurrentState() ).SetErrorMessage( 
									   handler.StringManager.GetString( StringNames.EmailAlreadyExists ) );
			}
		}

		private void HandleRegisterUserMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			RegisterUserMessage registerUser = new RegisterUserMessage( incomingMessage );
			RegistrationResultMessage resultMessage;
			if( gameDatabase.Users.Read( registerUser.User.Email ) == null )
			{
				if( !gameDatabase.DoesUsernameExist( registerUser.User.Username ) )
				{
					gameDatabase.Users.Create( registerUser.User );
					gameDatabase.Accounts.Create( new Account( 0, registerUser.User.UserId, 0, 0, 0, 0, 0, 300 ) );
					emailManager.SendEmailMessage( registerUser.User.Names, registerUser.User.Email,
												   handler.StringManager.GetString( StringNames.Registration ),
												   handler.StringManager.GetString( StringNames.EmailBodyMessage ) );
					resultMessage = new RegistrationResultMessage(
															  handler.StringManager.GetString( StringNames.RegistrationSuccessful ) );
				}
				else
				{
					resultMessage = new RegistrationResultMessage(
															  handler.StringManager.GetString( StringNames.UsernameAlreadyExists ) );
				}
			}
			else
			{
				resultMessage = new RegistrationResultMessage(
														  handler.StringManager.GetString( StringNames.EmailAlreadyExists ) );
			}

			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )resultMessage.MessageType );
			resultMessage.Encode( outgoingMessage );

			incomingMessage.SenderConnection.SendMessage( outgoingMessage, NetDeliveryMethod.ReliableUnordered,
														  incomingMessage.SequenceChannel );
		}

		private void HandleSelfSolicitPasswordResetMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			SolicitPasswordResetMessage solicitMessage = new SolicitPasswordResetMessage( incomingMessage );
			var user = gameDatabase.Users.Read( solicitMessage.Email );
			if( user != null )
			{
				ChangeManager.SetChangeCode( solicitMessage.Email );
				emailManager.SendEmailMessage( user.Names, user.Email, handler.StringManager.GetString( StringNames.PasswordReset ),
											   handler.StringManager.GetString( StringNames.PasswordResetEmailBody ) +
											   ChangeManager.GetResetCode( user.Email ) + "." );
				( ( PasswordRecoveryState )handler.GetCurrentState() ).SetMessage( 
										   handler.StringManager.GetString( StringNames.PasswordEmailSuccess ) );
			}
			else
			{
				( ( PasswordRecoveryState )handler.GetCurrentState() ).SetErrorMessage(
										   handler.StringManager.GetString( StringNames.PasswordEmailFailure ) );
			}
		}

		private void HandleSolicitPasswordResetMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			SolicitPasswordResetMessage solicitMessage = new SolicitPasswordResetMessage( incomingMessage );
			var user = gameDatabase.Users.Read( solicitMessage.Email );
			SolicitPasswordResultMessage resultMessage;
			if( user != null )
			{
				ChangeManager.SetChangeCode( solicitMessage.Email );
				emailManager.SendEmailMessage( user.Names, user.Email, handler.StringManager.GetString( StringNames.PasswordReset ),
											   handler.StringManager.GetString( StringNames.PasswordResetEmailBody ) +
											   ChangeManager.GetResetCode( user.Email ) + "." );
				resultMessage = new SolicitPasswordResultMessage(
											 handler.StringManager.GetString( StringNames.PasswordEmailSuccess ) );
			}
			else
			{
				resultMessage = new SolicitPasswordResultMessage(
											 handler.StringManager.GetString( StringNames.PasswordEmailFailure ) );
			}

			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )resultMessage.MessageType );
			resultMessage.Encode( outgoingMessage );

			netServer.SendMessage( outgoingMessage, incomingMessage.SenderConnection, NetDeliveryMethod.ReliableUnordered );
		}

		private void HandleSelfPasswordResetMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PasswordResetMessage resetMessage = new PasswordResetMessage( incomingMessage );
			var user = gameDatabase.Users.Read( resetMessage.Email );
			if (user != null && ChangeManager.PasswordChangeWasSolicited(resetMessage.Email))
			{
				if (ChangeManager.ResetPassword(gameDatabase, resetMessage.Email, resetMessage.Password, resetMessage.Code))
				{
					( ( PasswordRecoveryState )handler.GetCurrentState() ).SetMessage(
									handler.StringManager.GetString( StringNames.PasswordResetSuccessful ) );
					handler.GetCurrentState().ClearTextAreas();
				}
				else
				{
					( ( PasswordRecoveryState )handler.GetCurrentState() ).SetErrorMessage(
									handler.StringManager.GetString( StringNames.PasswordCodesDoNotMatch ) );
				}
			}
			else
			{
				( ( PasswordRecoveryState )handler.GetCurrentState() ).SetErrorMessage(
									handler.StringManager.GetString( StringNames.PasswordChangeNotAsked ) );
			}
		}

		private void HandlePasswordResetMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PasswordResetMessage resetMessage = new PasswordResetMessage( incomingMessage );
			var user = gameDatabase.Users.Read( resetMessage.Email );
			PasswordResetResultMessage resultMessage;
			if( user != null && ChangeManager.PasswordChangeWasSolicited( resetMessage.Email ) )
			{
				if( ChangeManager.ResetPassword( gameDatabase, resetMessage.Email, resetMessage.Password, resetMessage.Code ) )
				{
					resultMessage = new PasswordResetResultMessage( 
									handler.StringManager.GetString( StringNames.PasswordResetSuccessful ) );
				}
				else
				{
					resultMessage = new PasswordResetResultMessage(
									handler.StringManager.GetString( StringNames.PasswordCodesDoNotMatch ) );
				}
			}
			else
			{
				resultMessage = new PasswordResetResultMessage(
									handler.StringManager.GetString( StringNames.PasswordChangeNotAsked ) );
			}

			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )resultMessage.MessageType );
			resultMessage.Encode( outgoingMessage );

			netServer.SendMessage( outgoingMessage, incomingMessage.SenderConnection, NetDeliveryMethod.ReliableUnordered );
		}

		private void HandleRoundStateChangedMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			( ( WaitingRoomState )handler.GetCurrentState() ).UsernameManager.HandleRoundStateChangeMessage( incomingMessage );

			RoundStateChangedMessage newRoundState = new RoundStateChangedMessage( GameRound.GetPlayerUsernames() );
			NetOutgoingMessage updateMessage = CreateMessage();
			updateMessage.Write( ( byte )newRoundState.MessageType );
			newRoundState.Encode( updateMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( updateMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		private void HandleRoundCreatedMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			if( incomingMessage.ReadBoolean() )
			{
				handler.ChangeState( StateTypes.WaitingRoomState );
				( ( WaitingRoomState )handler.GetCurrentState() ).UsernameManager.HandleRoundCreatedMessage( incomingMessage );
			}
		}

		private void HandleJoinRoundRequestMessage( NetIncomingMessage incomingMessage )
		{
			if( GameRound.Created && GameRound.RoomAvailable() )
			{
				GameRound.AddPlayer( incomingMessage.ReadString(), incomingMessage.SenderConnection );
				RoundStateChangedMessage newRoundState = new RoundStateChangedMessage( GameRound.GetPlayerUsernames() );
				NetOutgoingMessage updateMessage = CreateMessage();
				updateMessage.Write( ( byte )newRoundState.MessageType );
				newRoundState.Encode( updateMessage );

				netServer.SendUnconnectedToSelf( updateMessage );

				JoinRoundRequestResultMessage resultMessage = new JoinRoundRequestResultMessage( true, GameRound.GetPlayerUsernames() );
				NetOutgoingMessage outgoingMessage = CreateMessage();
				outgoingMessage.Write( ( byte )resultMessage.MessageType );
				resultMessage.Encode( outgoingMessage );

				netServer.SendMessage( outgoingMessage, incomingMessage.SenderConnection, 
									   NetDeliveryMethod.ReliableUnordered );
			}
		}

		private void HandleLeaveRoundMessage( NetIncomingMessage incomingMessage )
		{
			GameRound.RemovePlayer( incomingMessage.SenderConnection, incomingMessage.ReadString() );
			RoundStateChangedMessage newRoundState = new RoundStateChangedMessage( GameRound.GetPlayerUsernames() );
			NetOutgoingMessage updateMessage = CreateMessage();
			updateMessage.Write( ( byte )newRoundState.MessageType );
			newRoundState.Encode( updateMessage );

			netServer.SendUnconnectedToSelf( updateMessage );
		}

		private void HandleStartRoundMessage( StateHandler handler )
		{
			handler.ChangeState( StateTypes.PlayState );
			( ( PlayState )handler.GetCurrentState() ).PlayerManager.CreatePlayers( handler.GetCurrentState().Content, handler,
																					GameRound.GetPlayerUsernames(),
																					UserSession.CurrentUser.Username );

			StartRoundMessage startMessage = new StartRoundMessage( GameRound.GetPlayerUsernames(),
											( ( PlayState )handler.GetCurrentState() ).PlayerManager.Players );
			NetOutgoingMessage outgoingStartMessage = CreateMessage();
			outgoingStartMessage.Write( ( byte )startMessage.MessageType );
			startMessage.Encode( outgoingStartMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingStartMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}

			( ( PlayState )handler.GetCurrentState() ).ItemManager.SpawnBoxes();
			SpawnBoxMessage spawnBoxesMessage = new SpawnBoxMessage( ( ( PlayState )handler.GetCurrentState() ).ItemManager.Boxes );
			NetOutgoingMessage outgoingBoxMessage = CreateMessage();
			outgoingBoxMessage.Write( ( byte )spawnBoxesMessage.MessageType );
			spawnBoxesMessage.Encode( outgoingBoxMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingBoxMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		private void HandleChatMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			ChatMessage chatMessage = new ChatMessage( incomingMessage );
			( ( WaitingRoomState )handler.GetCurrentState() ).MessageManager.HandleChatMessage(
								  chatMessage.SenderUsername, chatMessage.Message, UserSession.CurrentUser.Username );

			string playerToBan = GameRound.DoBanRequest( chatMessage.Message, handler.StringManager.GetString( StringNames.BanMessage ) );
			if ( !string.IsNullOrEmpty( playerToBan ) )
			{
				BanPlayerMessage banMessage = new BanPlayerMessage( playerToBan );
				NetOutgoingMessage outgoingBanMessage = CreateMessage();
				outgoingBanMessage.Write( ( byte )banMessage.MessageType );
				banMessage.Encode( outgoingBanMessage );

				netServer.SendUnconnectedToSelf( outgoingBanMessage );
			}

			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )chatMessage.MessageType );
			chatMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		private void HandleBanMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			BanPlayerMessage banMessage = new BanPlayerMessage( incomingMessage );
			if( !banMessage.Username.Equals( UserSession.CurrentUser.Username ) )
			{
				ExitWaitingRoomMessage leaveMessage = new ExitWaitingRoomMessage();
				NetOutgoingMessage outgoingMessage = CreateMessage();
				outgoingMessage.Write( ( byte )leaveMessage.MessageType );

				netServer.SendMessage( outgoingMessage, GameRound.GetPlayerConnection( banMessage.Username ),
									   NetDeliveryMethod.ReliableUnordered );

				GameRound.RemovePlayer( banMessage.Username );
				RoundStateChangedMessage roundChangedMessage = new RoundStateChangedMessage( GameRound.GetPlayerUsernames() );
				NetOutgoingMessage outgoingStateMessage = CreateMessage();
				outgoingStateMessage.Write( ( byte )roundChangedMessage.MessageType );
				roundChangedMessage.Encode( outgoingStateMessage );

				netServer.SendUnconnectedToSelf( outgoingStateMessage );
			}
			else
			{
				GameRound.ResetBanPetitions();
			}
		}

		private void HandlePlayerStateChangeMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PlayerStateChangeMessage stateChangeMessage = new PlayerStateChangeMessage( incomingMessage );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )stateChangeMessage.MessageType );
			stateChangeMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				if( connection != incomingMessage.SenderConnection )
				{
					netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
				}
			}

			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).PlayerManager.HandlePlayerStateChangeMessage( 
																				incomingMessage, stateChangeMessage );
		}

		private void HandleBoxDamageMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			BoxDamageMessage boxDamageMessage = new BoxDamageMessage( incomingMessage );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )boxDamageMessage.MessageType );
			boxDamageMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				if( connection != incomingMessage.SenderConnection )
				{
					netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
				}
			}

			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).ItemManager.HandleBoxDamageMessage( boxDamageMessage );
		}

		private void HandlePlayerAttackMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PlayerAttackMessage playerAttackMessage = new PlayerAttackMessage( incomingMessage );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )playerAttackMessage.MessageType );
			playerAttackMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				if( connection != incomingMessage.SenderConnection )
				{
					netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
				}
			}

			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).PlayerManager.HandlePlayerAttackMessage( playerAttackMessage );
		}

		private void HandlePickedUpItemMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PickedUpItemMessage pickedUpItemMessage = new PickedUpItemMessage( incomingMessage );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )pickedUpItemMessage.MessageType );
			pickedUpItemMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				if( connection != incomingMessage.SenderConnection )
				{
					netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
				}
			}

			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).ItemManager.HandlePickedUpItemMessage( 
																				pickedUpItemMessage.ItemIndex );
		}

		private void HandleSpawnGrenadeMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			SpawnGrenadeMessage grenadeMessage = new SpawnGrenadeMessage( incomingMessage );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )grenadeMessage.MessageType );
			grenadeMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				if( connection != incomingMessage.SenderConnection )
				{
					netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
				}
			}

			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).ItemManager.HandleSpawnGrenadeMessage( grenadeMessage.Position, 
																											 grenadeMessage.Direction, 
																										     grenadeMessage.GrenadeSpeed );
		}

		private void HandleRoundFinishedMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			RoundFinishedMessage endMessage = new RoundFinishedMessage( incomingMessage );
			bool isLocalPlayerDead = ( ( PlayState )handler.GetCurrentState() ).PlayerManager.GetLocalPlayer().Health.IsDead();
			bool didLocalPlayerWin = ( ( PlayState )handler.GetCurrentState() ).PlayerManager.DidLocalPlayerWin();
			UserSession.UpdateRoundStatistics( isLocalPlayerDead, didLocalPlayerWin, endMessage.RemainingRoundTime );
			gameDatabase.UpdateUserInformation( UserSession.CurrentUser, UserSession.CurrentAccount );
			handler.ChangeState( StateTypes.WaitingRoomState );

			RoundStateChangedMessage roundStateChanged = new RoundStateChangedMessage( GameRound.GetPlayerUsernames() );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )roundStateChanged.MessageType );
			roundStateChanged.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		private void HandleUpdateUserInformationMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			UpdateUserStatisticsMessage updateMessage = new UpdateUserStatisticsMessage( incomingMessage );
			gameDatabase.UpdateUserInformation( updateMessage.User, updateMessage.Account );
		}

		private void HandlePlayerIsDeadMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PlayerIsDeadMessage deathMessage = new PlayerIsDeadMessage( incomingMessage );
			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).PlayerManager.HandlePlayerDiedMessage( deathMessage.PlayerId );

			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )deathMessage.MessageType );
			deathMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				if( connection != incomingMessage.SenderConnection )
				{
					netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
				}
			}
		}

		private void HandleSelfExitWaitingRomMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			ExitWaitingRoomMessage exitMessage = new ExitWaitingRoomMessage( incomingMessage );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )exitMessage.MessageType );
			exitMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}

			GameRound.DestroyRound();
		}

		/// <summary>
		/// Sends a RegisterUserMessage to self for processing.
		/// </summary>
		/// <param name="nameIn">User name</param>
		/// <param name="lastNameIn">User last name</param>
		/// <param name="usernameIn">User username</param>
		/// <param name="emailIn">User email</param>
		/// <param name="passwordIn">User password</param>
		public void RegisterUser( string nameIn, string lastNameIn, string usernameIn, string emailIn, string passwordIn )
		{
			var user = new User( nameIn, lastNameIn, usernameIn, emailIn, passwordIn );
			RegisterUserMessage registerUserMessage = new RegisterUserMessage( user );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )registerUserMessage.MessageType );
			registerUserMessage.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		/// <summary>
		/// Sends a PasswordSOlicitMessage to self for processing
		/// </summary>
		/// <param name="emailIn">User email.</param>
		public void SendPasswordChangeMessage( string emailIn )
		{
			SolicitPasswordResetMessage solicitMessage = new SolicitPasswordResetMessage( emailIn );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )solicitMessage.MessageType );
			solicitMessage.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		/// <summary>
		/// Sends a PlayerStateChangeMessage to al clients with updated player
		/// state.
		/// </summary>
		/// <param name="player">The target player</param>
		public void SendPlayerStateChangeMessage( GameObject player )
		{
			PlayerStateChangeMessage stateChangeMessage = new PlayerStateChangeMessage( player );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )stateChangeMessage.MessageType );
			stateChangeMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		/// <summary>
		/// Sends a SpawnBoxMessage to all clients.
		/// </summary>
		/// <param name="boxes">List of boxes.</param>
		public void SendSpawnBoxMessage( List< GameObject > boxes )
		{
			SpawnBoxMessage spawnBoxMessage = new SpawnBoxMessage( boxes );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )spawnBoxMessage.MessageType );
			spawnBoxMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		/// <summary>
		/// Sends a SpawnConsumableMessage to all clients.
		/// </summary>
		/// <param name="consumables"></param>
		public void SendSpawnConsumablesMessage( List< GameObject > consumables )
		{
			SpawnConsumablesMessage spawnConsumablesMessage = new SpawnConsumablesMessage( consumables );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )spawnConsumablesMessage.MessageType );
			spawnConsumablesMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		/// <summary>
		/// Sends a BoxDamage message to all clients.
		/// </summary>
		/// <param name="boxIndex"></param>
		/// <param name="damage"></param>
		public void SendBoxDamageMessage( int boxIndex, int damage )
		{
			BoxDamageMessage boxDamageMessage = new BoxDamageMessage( boxIndex, damage );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )boxDamageMessage.MessageType );
			boxDamageMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		/// <summary>
		/// Sends a PlayerAttackMessage to all clients.
		/// </summary>
		/// <param name="localPlayerIndex"></param>
		public void SendPlayerAttackMessage( Identifiers localPlayerIndex )
		{
			PlayerAttackMessage attackMessage = new PlayerAttackMessage( localPlayerIndex );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )attackMessage.MessageType );
			attackMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		/// <summary>
		/// Sends a pickuedUpItemMessage to all clients.
		/// </summary>
		/// <param name="itemIndex"></param>
		public void SendPickedUpItemMessage( int itemIndex )
		{
			PickedUpItemMessage pickedUpItemMessage = new PickedUpItemMessage( itemIndex );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )pickedUpItemMessage.MessageType );
			pickedUpItemMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}


		/// <summary>
		/// Sends a SpawnGrenadeMessage to all clients.
		/// </summary>
		/// <param name="grenade"></param>
		public void SendSpawnGrenadeMessage( GameObject grenade )
		{
			SpawnGrenadeMessage grenadeMessage = new SpawnGrenadeMessage( grenade );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )grenadeMessage.MessageType );
			grenadeMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		/// <summary>
		/// Sends an UpdateRemainingTimeMessage to all clients
		/// </summary>
		/// <param name="remainingTime"></param>
		public void SendUpdateRemainingTimeMessage( float remainingTime )
		{
			UpdateRoundTimeMessage timeMessage = new UpdateRoundTimeMessage( remainingTime );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )timeMessage.MessageType );
			timeMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		/// <summary>
		/// Sends a RoundFinishedMessage to all clients.
		/// </summary>
		/// <param name="remainingRoundTime"></param>
		public void SendRoundFinishedMessage( int remainingRoundTime )
		{
			RoundFinishedMessage endRoundMessage = new RoundFinishedMessage( remainingRoundTime );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )endRoundMessage.MessageType );
			endRoundMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}

			RoundFinishedMessage serverEndRoundMessage = new RoundFinishedMessage( remainingRoundTime );
			NetOutgoingMessage serverOutgoingMessage = CreateMessage();
			serverOutgoingMessage.Write( ( byte )serverEndRoundMessage.MessageType );
			serverEndRoundMessage.Encode( serverOutgoingMessage );

			netServer.SendUnconnectedToSelf( serverOutgoingMessage );
		}

		/// <summary>
		/// Sends a PlayerDiedMessage to all clients.
		/// </summary>
		/// <param name="playerId"></param>
		public void SendPlayerDiedMessage( Identifiers playerId )
		{
			PlayerIsDeadMessage deathMessage = new PlayerIsDeadMessage( playerId );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )deathMessage.MessageType );
			deathMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		/// <summary>
		/// Sends a PasswordResetMessage to self for processing.
		/// </summary>
		/// <param name="code"></param>
		/// <param name="email"></param>
		/// <param name="password"></param>
		public void UpdatePassword( string code, string email, string password )
		{
			PasswordResetMessage resetMessage = new PasswordResetMessage( code, email, password );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )resetMessage.MessageType );
			resetMessage.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		/// <summary>
		/// Creates a game round and sends a RoundCreatedMessage to self for
		/// processing.
		/// </summary>
		public void CreateRound()
		{
			GameRound.CreateRound( UserSession.CurrentUser.Username );
			RoundCreatedMessage newRound = new RoundCreatedMessage( true, GameRound.GetPlayerUsernames() );
			NetOutgoingMessage updateMessage = CreateMessage();
			updateMessage.Write( ( byte )newRound.MessageType );
			newRound.Encode( updateMessage );

			netServer.SendUnconnectedToSelf( updateMessage );
		}

		/// <summary>
		/// Sends a StartRoundMessage to all clients.
		/// </summary>
		public void StartRound()
		{
			StartRoundMessage startMessage = new StartRoundMessage( GameRound.GetPlayerUsernames() );
			NetOutgoingMessage outgoingStartMessage = CreateMessage();
			outgoingStartMessage.Write( ( byte )startMessage.MessageType );
			startMessage.Encode( outgoingStartMessage );

			netServer.SendUnconnectedToSelf( outgoingStartMessage );
		}

		/// <summary>
		/// Method unused by servernetwork manager.
		/// </summary>
		public void JoinRound() {}

		/// <summary>
		/// Sends a ChatMessage to self for processing.
		/// </summary>
		/// <param name="message"></param>
		public void SendChatMessage( string message )
		{
			ChatMessage chatMessage = new ChatMessage( UserSession.CurrentUser.Username, message );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )chatMessage.MessageType );
			chatMessage.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		/// <summary>
		/// Sends en ExitWaitingRoomMessage to all clients and self for processing.
		/// </summary>
		public void LeaveRound() 
		{
			ExitWaitingRoomMessage exitMessage = new ExitWaitingRoomMessage();
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )exitMessage.MessageType );
			exitMessage.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		///<value>Manager that handles password resets</value>
		public PasswordChangeManager ChangeManager { get; private set; }

		///<value>The current gameRound.</value>
		public GameRound GameRound { get; set; }

		///<value>The current usersession.</value>
		public LoginSession UserSession { get; private set; }
		private NetServer netServer;
		private EmailManager emailManager;
		private GameDatabase gameDatabase;
		private bool isDisposed;
	}
}