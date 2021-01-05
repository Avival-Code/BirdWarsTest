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
			if( gameDatabase.Users.Read( email, password ) != null )
			{
				var user = gameDatabase.Users.Read( email, password );
				UserSession.Login( user, gameDatabase.Accounts.Read( user.UserId ) );
				Console.WriteLine( "Login credentials approved." );
			}
			else
			{
				Console.WriteLine( "Invalid login credentials" );
			}
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
								HandleStartRoundMessage( handler, incomingMessage );
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
							case GameMessageTypes.registerUserMessage:
								HandleRegisterUserMessage( incomingMessage );
								break;
							case GameMessageTypes.JoinRoundRequestMessage:
								HandleJoinRoundRequestMessage( incomingMessage );
								break;
							case GameMessageTypes.ChatMessage:
								HandleChatMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.SolicitPasswordResetmessage:
								HandleSolicitPasswordResetMessage( incomingMessage );
								break;
							case GameMessageTypes.PasswordResetMessage:
								HandlePasswordResetMessage( incomingMessage );
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
						}
						break;
				}
				Recycle( incomingMessage );
			}
		}

		private void HandleLoginRequestMessages( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			var user = gameDatabase.Users.Read( incomingMessage.ReadString(),
											    incomingMessage.ReadString() );
			if( user != null )
			{
				LoginResultMessage loginResult = new LoginResultMessage( true, handler.StringManager.GetString( StringNames.LoginApproved ),
																		 user, gameDatabase.Accounts.Read( user.UserId ) );
				NetOutgoingMessage outgoingMessage = CreateMessage();
				outgoingMessage.Write( ( byte )loginResult.messageType );
				loginResult.Encode( outgoingMessage );

				netServer.SendMessage( outgoingMessage, incomingMessage.SenderConnection, 
									   NetDeliveryMethod.ReliableUnordered );
			}
			else
			{
				LoginResultMessage loginResult = new LoginResultMessage( false, handler.StringManager.GetString( StringNames.LoginDenied ), 
																		 new User(), new Account() );
				NetOutgoingMessage outgoingMessage = CreateMessage();
				outgoingMessage.Write( ( byte )loginResult.messageType );
				loginResult.Encode( outgoingMessage );

				netServer.SendMessage( outgoingMessage, incomingMessage.SenderConnection,
									   NetDeliveryMethod.ReliableUnordered );
			}
		}

		private void HandleRegisterUserMessage( NetIncomingMessage incomingMessage )
		{
			var user = new User( incomingMessage.ReadString(), incomingMessage.ReadString(),
								 incomingMessage.ReadString(), incomingMessage.ReadString(),
								 incomingMessage.ReadString() );
			gameDatabase.Users.Create( user );
			var userWithId = gameDatabase.Users.Read( user.Email, user.Password );
			gameDatabase.Accounts.Create( new Account( 0, userWithId.UserId, 0, 0, 0, 0, 0 ) );
			emailManager.SendEmailMessage( userWithId.Names, userWithId.Email, "Registration", 
										   ( "Thank you for completing the registration process! Your account " +
										   "has been created!" ) );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( "Registration successfull." );
			incomingMessage.SenderConnection.SendMessage( outgoingMessage,  NetDeliveryMethod.ReliableUnordered, 
														  incomingMessage.SequenceChannel );
		}

		private void HandleSolicitPasswordResetMessage( NetIncomingMessage incomingMessage )
		{
			SendPasswordChangeMessage( incomingMessage.ReadString() );
		}

		private void HandlePasswordResetMessage( NetIncomingMessage incomingMessage )
		{
			UpdatePassword( incomingMessage.ReadString(), incomingMessage.ReadString() );
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
			Console.WriteLine( "Recieved Create round message." );
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

		private void HandleStartRoundMessage( StateHandler handler, NetIncomingMessage incomingMessage )
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
			string username = incomingMessage.ReadString();
			string message = incomingMessage.ReadString();
			( ( WaitingRoomState )handler.GetCurrentState() ).MessageManager.HandleChatMessage(
					username, message, UserSession.CurrentUser.Username );

			ChatMessage newMessage = new ChatMessage( username, message );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )newMessage.messageType );
			newMessage.Encode( outgoingMessage );

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

			( ( PlayState )handler.GetCurrentState() ).PlayerManager.HandlePlayerStateChangeMessage( incomingMessage, 
																									 stateChangeMessage );
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

			( ( PlayState )handler.GetCurrentState() ).ItemManager.HandleBoxDamageMessage( boxDamageMessage );
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

			( ( PlayState )handler.GetCurrentState() ).PlayerManager.HandlePlayerAttackMessage( playerAttackMessage );
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

			( ( PlayState )handler.GetCurrentState() ).ItemManager.HandlePickedUpItemMessage( pickedUpItemMessage.ItemIndex );
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

			( ( PlayState )handler.GetCurrentState() ).ItemManager.HandleSpawnGrenadeMessage( grenadeMessage.Position, 
																							  grenadeMessage.Direction, 
																							  grenadeMessage.GrenadeSpeed );
		}

		public void RegisterUser( string nameIn, string lastNameIn, string usernameIn, string emailIn, string passwordIn )
		{
			gameDatabase.Users.Create( new User( nameIn, lastNameIn, usernameIn, emailIn, passwordIn ) );
			var user = gameDatabase.Users.Read( emailIn, passwordIn );
			gameDatabase.Accounts.Create( new Account( 0, user.UserId, 0, 0, 0, 0, 0 ) );
			emailManager.SendEmailMessage( user.Names, user.Email, "Registration",
										   ( "Thank you for completing the registration process! Your account " +
										     "has been created!" ) );
		}

		public void SendPasswordChangeMessage( string emailIn )
		{
			var user = gameDatabase.Users.Read( emailIn );
			if( user != null )
			{
				ChangeManager.SetChangeCode( emailIn );
				emailManager.SendEmailMessage( user.Names, user.Email, "Password Reset", 
											   "Your password reset code is: " + ChangeManager.ChangeCode + "." );
			}
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

		public void UpdatePassword( string code, string password )
		{
			if( ChangeManager.PasswordChangeWasSolicited && code.Equals( ChangeManager.ChangeCode.ToString() ) )
			{
				var user = gameDatabase.Users.Read( ChangeManager.TargetUserEmail );
				gameDatabase.Users.Update( user.UserId, user.Names, user.LastName, user.Username, user.Email, password );
			}
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