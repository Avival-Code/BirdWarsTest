using BirdWarsTest.States;
using BirdWarsTest.Database;
using BirdWarsTest.Network.Messages;
using BirdWarsTest.GameRounds;
using BirdWarsTest.GameObjects;
using BirdWarsTest.Utilities;
using Lidgren.Network;
using System;
using System.Collections.Generic;

namespace BirdWarsTest.Network
{
	public class ServerNetworkManager : INetworkManager
	{
		public ServerNetworkManager()
		{
			Connect();
			gameDatabase = new GameDatabase();
			GameRound = new GameRound();
			emailManager = new EmailManager();
			ChangeManager = new PasswordChangeManager();
			UserSession = new LoginSession();
		}

		public void Login( string email, string password )
		{
			LoginRequestMessage loginMessage = new LoginRequestMessage( email, password );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )loginMessage.messageType );
			loginMessage.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		public void Logout()
		{
			UserSession.Logout();
		}

		public LoginSession GetLoginSession()
		{
			return UserSession;
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
						}
						break;
					case NetIncomingMessageType.Data:
						var gameMessageType = ( GameMessageTypes )incomingMessage.ReadByte();
						switch( gameMessageType )
						{
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
			outgoingMessage.Write( ( byte )loginResult.messageType );
			loginResult.Encode( outgoingMessage );

			netServer.SendMessage( outgoingMessage, incomingMessage.SenderConnection, NetDeliveryMethod.ReliableUnordered );
		}

		private void HandleSelfRegisterUserMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			RegisterUserMessage registerUser = new RegisterUserMessage( incomingMessage );
			if( gameDatabase.Users.Read( registerUser.User.Email ) == null )
			{
				gameDatabase.Users.Create( registerUser.User );
				gameDatabase.Accounts.Create( new Account( 0, registerUser.User.UserId, 0, 0, 0, 0, 0, 300 ) );
				emailManager.SendEmailMessage( registerUser.User.Names, registerUser.User.Email,
											   handler.StringManager.GetString( StringNames.Registration),
											   handler.StringManager.GetString( StringNames.EmailBodyMessage) );
				( ( UserRegistryState )handler.GetCurrentState() ).SetMessage( 
									   handler.StringManager.GetString( StringNames.RegistrationSuccessful ) );
				handler.GetCurrentState().ClearTextAreas();
			}
			else
			{
				( ( UserRegistryState )handler.GetCurrentState() ).SetErrorMessage( 
									   handler.StringManager.GetString( StringNames.RegistrationFailed ) );
			}
		}

		private void HandleRegisterUserMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			RegisterUserMessage registerUser = new RegisterUserMessage( incomingMessage );
			RegistrationResultMessage resultMessage;
			if( gameDatabase.Users.Read( registerUser.User.Email ) == null )
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
														  handler.StringManager.GetString( StringNames.RegistrationFailed ) );
			}

			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )resultMessage.messageType );
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
			outgoingMessage.Write( ( byte )resultMessage.messageType );
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
			outgoingMessage.Write( ( byte )resultMessage.messageType );
			resultMessage.Encode( outgoingMessage );

			netServer.SendMessage( outgoingMessage, incomingMessage.SenderConnection, NetDeliveryMethod.ReliableUnordered );
		}

		private void HandleRoundStateChangedMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			( ( WaitingRoomState )handler.GetCurrentState() ).UsernameManager.HandleRoundStateChangeMessage( incomingMessage );

			RoundStateChangedMessage newRoundState = new RoundStateChangedMessage( GameRound.GetPlayerUsernames() );
			NetOutgoingMessage updateMessage = CreateMessage();
			updateMessage.Write( ( byte )newRoundState.messageType );
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
				updateMessage.Write( ( byte )newRoundState.messageType );
				newRoundState.Encode( updateMessage );

				netServer.SendUnconnectedToSelf( updateMessage );

				JoinRoundRequestResultMessage resultMessage = new JoinRoundRequestResultMessage( true, GameRound.GetPlayerUsernames() );
				NetOutgoingMessage outgoingMessage = CreateMessage();
				outgoingMessage.Write( ( byte )resultMessage.messageType );
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
			updateMessage.Write( ( byte )newRoundState.messageType );
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
			outgoingStartMessage.Write( ( byte )startMessage.messageType );
			startMessage.Encode( outgoingStartMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingStartMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}

			( ( PlayState )handler.GetCurrentState() ).ItemManager.SpawnBoxes();
			SpawnBoxMessage spawnBoxesMessage = new SpawnBoxMessage( ( ( PlayState )handler.GetCurrentState() ).ItemManager.Boxes );
			NetOutgoingMessage outgoingBoxMessage = CreateMessage();
			outgoingBoxMessage.Write( ( byte )spawnBoxesMessage.messageType );
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

			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )chatMessage.messageType );
			chatMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		private void HandlePlayerStateChangeMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PlayerStateChangeMessage stateChangeMessage = new PlayerStateChangeMessage( incomingMessage );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )stateChangeMessage.messageType );
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
			outgoingMessage.Write( ( byte )boxDamageMessage.messageType );
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
			outgoingMessage.Write( ( byte )playerAttackMessage.messageType );
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
			outgoingMessage.Write( ( byte )pickedUpItemMessage.messageType );
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
			outgoingMessage.Write( ( byte )grenadeMessage.messageType );
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
			outgoingMessage.Write( ( byte )roundStateChanged.messageType );
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
			outgoingMessage.Write( ( byte )deathMessage.messageType );
			deathMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				if( connection != incomingMessage.SenderConnection )
				{
					netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
				}
			}
		}

		public void RegisterUser( string nameIn, string lastNameIn, string usernameIn, string emailIn, string passwordIn )
		{
			var user = new User( nameIn, lastNameIn, usernameIn, emailIn, passwordIn );
			RegisterUserMessage registerUserMessage = new RegisterUserMessage( user );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )registerUserMessage.messageType );
			registerUserMessage.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		public void SendPasswordChangeMessage( string emailIn )
		{
			SolicitPasswordResetMessage solicitMessage = new SolicitPasswordResetMessage( emailIn );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )solicitMessage.messageType );
			solicitMessage.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		public void SendPlayerStateChangeMessage( GameObject player )
		{
			PlayerStateChangeMessage stateChangeMessage = new PlayerStateChangeMessage( player );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )stateChangeMessage.messageType );
			stateChangeMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		public void SendSpawnBoxMessage( List< GameObject > boxes )
		{
			SpawnBoxMessage spawnBoxMessage = new SpawnBoxMessage( boxes );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )spawnBoxMessage.messageType );
			spawnBoxMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		public void SendSpawnConsumablesMessage( List< GameObject > consumables )
		{
			SpawnConsumablesMessage spawnConsumablesMessage = new SpawnConsumablesMessage( consumables );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )spawnConsumablesMessage.messageType );
			spawnConsumablesMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		public void SendBoxDamageMessage( int boxIndex, int damage )
		{
			BoxDamageMessage boxDamageMessage = new BoxDamageMessage( boxIndex, damage );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )boxDamageMessage.messageType );
			boxDamageMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		public void SendPlayerAttackMessage( Identifiers localPlayerIndex )
		{
			PlayerAttackMessage attackMessage = new PlayerAttackMessage( localPlayerIndex );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )attackMessage.messageType );
			attackMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		public void SendPickedUpItemMessage( int itemIndex )
		{
			PickedUpItemMessage pickedUpItemMessage = new PickedUpItemMessage( itemIndex );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )pickedUpItemMessage.messageType );
			pickedUpItemMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		public void SendSpawnGrenadeMessage( GameObject grenade )
		{
			SpawnGrenadeMessage grenadeMessage = new SpawnGrenadeMessage( grenade );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )grenadeMessage.messageType );
			grenadeMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		public void SendUpdateRemainingTimeMessage( float remainingTime )
		{
			UpdateRoundTimeMessage timeMessage = new UpdateRoundTimeMessage( remainingTime );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )timeMessage.messageType );
			timeMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		public void SendRoundFinishedMessage( int remainingRoundTime )
		{
			RoundFinishedMessage endRoundMessage = new RoundFinishedMessage( remainingRoundTime );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )endRoundMessage.messageType );
			endRoundMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}

			RoundFinishedMessage serverEndRoundMessage = new RoundFinishedMessage( remainingRoundTime );
			NetOutgoingMessage serverOutgoingMessage = CreateMessage();
			serverOutgoingMessage.Write( ( byte )serverEndRoundMessage.messageType );
			serverEndRoundMessage.Encode( serverOutgoingMessage );

			netServer.SendUnconnectedToSelf( serverOutgoingMessage );
		}

		public void SendPlayerDiedMessage( Identifiers playerId )
		{
			PlayerIsDeadMessage deathMessage = new PlayerIsDeadMessage( playerId );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )deathMessage.messageType );
			deathMessage.Encode( outgoingMessage );

			foreach( var connection in GameRound.PlayerConnections )
			{
				netServer.SendMessage( outgoingMessage, connection, NetDeliveryMethod.ReliableUnordered );
			}
		}

		public void UpdatePassword( string code, string email, string password )
		{
			PasswordResetMessage resetMessage = new PasswordResetMessage( code, email, password );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )resetMessage.messageType );
			resetMessage.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		public void CreateRound()
		{
			GameRound.CreateRound( UserSession.CurrentUser.Username );
			RoundCreatedMessage newRound = new RoundCreatedMessage( true, GameRound.GetPlayerUsernames() );
			NetOutgoingMessage updateMessage = CreateMessage();
			updateMessage.Write( ( byte )newRound.messageType );
			newRound.Encode( updateMessage );

			netServer.SendUnconnectedToSelf( updateMessage );
		}

		public void StartRound()
		{
			StartRoundMessage startMessage = new StartRoundMessage( GameRound.GetPlayerUsernames() );
			NetOutgoingMessage outgoingStartMessage = CreateMessage();
			outgoingStartMessage.Write( ( byte )startMessage.messageType );
			startMessage.Encode( outgoingStartMessage );

			netServer.SendUnconnectedToSelf( outgoingStartMessage );
		}

		public void JoinRound() {}

		public void SendChatMessage( string message )
		{
			ChatMessage chatMessage = new ChatMessage( UserSession.CurrentUser.Username, message );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )chatMessage.messageType );
			chatMessage.Encode( outgoingMessage );

			netServer.SendUnconnectedToSelf( outgoingMessage );
		}

		public void LeaveRound() {}

		public PasswordChangeManager ChangeManager { get; private set; }
		public GameRound GameRound { get; set; }
		public LoginSession UserSession { get; private set; }

		private NetServer netServer;
		private EmailManager emailManager;
		private GameDatabase gameDatabase;
		private bool isDisposed;
	}
}