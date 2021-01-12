/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
The client network manager handles all messages and matters 
relating to the server and it's connection.
*********************************************/
using BirdWarsTest.Database;
using BirdWarsTest.GameObjects;
using BirdWarsTest.Network.Messages;
using BirdWarsTest.Utilities;
using BirdWarsTest.States;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Net;

namespace BirdWarsTest.Network
{
	/// <summary>
	/// The client network manager handles all messages and matters 
	/// relating to the server and it's connection.
	/// </summary>
	public class ClientNetworkManager : INetworkManager
	{
		/// <summary>
		/// Creates an instance of the client manager which tries
		/// to connect to the server at a local location.
		/// </summary>
		public ClientNetworkManager()
		{
			UserSession = new LoginSession();
			Connect();
		}

		/// <summary>
		/// Sends a LoginRequestMessage to the server.
		/// </summary>
		/// <param name="email">User email</param>
		/// <param name="password">User password</param>
		public void Login( string email, string password )
		{
			SendMessage( new LoginRequestMessage( email, password ) );
		}

		/// <summary>
		/// Closes the current user session.
		/// </summary>
		public void Logout()
		{
			UserSession.Logout();
		}

		/// <summary>
		/// The client tries to connect to the server at the specified
		/// ip address and port.
		/// </summary>
		/// <param name="handler">Game statehandler</param>
		/// <param name="address">Ip addres</param>
		/// <param name="port">Port</param>
		public void ConnectToSpecificServer( StateHandler handler, string address, string port )
		{
			netClient.Disconnect( "" );
			netClient.Connect( new IPEndPoint( NetUtility.Resolve( address ), Convert.ToInt32( port ) ) );
		}

		/// <summary>
		/// Return the current user session.
		/// </summary>
		/// <returns></returns>
		public LoginSession GetLoginSession()
		{
			return UserSession;
		}

		/// <summary>
		/// Configures the netclient and attempts to connect to the server
		/// on a local address.
		/// </summary>
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

			netClient = new NetClient( config );
			netClient.Start();

			netClient.Connect( new IPEndPoint( NetUtility.Resolve( "127.0.0.1" ), Convert.ToInt32( "14242" ) ) );
		}

		/// <summary>
		/// Creates and returns a Netoutgoing message.
		/// </summary>
		/// <returns>Creates and returns a Netoutgoing message.</returns>
		public NetOutgoingMessage CreateMessage()
		{
			return netClient.CreateMessage();
		}

		/// <summary>
		/// Client disconnects from the server.
		/// </summary>
		public void Disconnect()
		{
			netClient.Disconnect( "" );
		}

		/// <summary>
		/// Method included in framework
		/// </summary>
		public void Dispose()
		{
			Dispose( true );
		}

		/// <summary>
		/// Reads an incoming message sent by the server.
		/// </summary>
		/// <returns></returns>
		public NetIncomingMessage ReadMessage()
		{
			return netClient.ReadMessage();
		}

		/// <summary>
		/// Recycles the incoming messages that are consumed.
		/// </summary>
		/// <param name="im"></param>
		public void Recycle( NetIncomingMessage im )
		{
			netClient.Recycle( im );
		}

		/// <summary>
		/// Sends a message to the server.
		/// </summary>
		/// <param name="gameMessage"></param>
		public void SendMessage( IGameMessage gameMessage )
		{
			NetOutgoingMessage outgoingMessage = netClient.CreateMessage();
			outgoingMessage.Write( ( byte )gameMessage.MessageType );
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

		/// <summary>
		/// Return the network manager connection status.
		/// </summary>
		/// <returns></returns>
		public NetConnectionStatus GetConnectionState()
		{
			return netClient.ConnectionStatus;
		}

		/// <summary>
		/// Checks if this instance on network manager is the server
		/// </summary>
		/// <returns></returns>
		public bool IsHost()
		{
			return false;
		}

		/// <summary>
		/// Method processes the incoming messages sent by the server.
		/// </summary>
		/// <param name="handler">Game statehandler.</param>
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
								Console.WriteLine( "Connected to {0}", incomingMessage.SenderEndPoint );
								SendMessage( new TestConnectionMessage( "" ) );
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
							case GameMessageTypes.TestConnectionMessage:
								HandleTestConnectionMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.LoginResultMessage:
								HandleLoginResultMessage( handler, incomingMessage );
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
							case GameMessageTypes.StartRoundMessage:
								HandleStartRoundMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.PlayerStateChangeMessage:
								HandlePlayerStateChangeMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.SpawnBoxMessage:
								HandleSpawnBoxMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.BoxDamageMessage:
								HandleBoxDamageMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.PlayerAttackMessage:
								HandlePlayerAttackMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.SpawnConsumablesMessage:
								HandleSpawnConsumablesMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.SpawnGrenadeMessage:
								HandleSpawnGrenadeMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.PickedUpItemMessage:
								HandlePickedUpItemMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.UpdateRoundTimeMessage:
								HandleUpdateRemainingTimeMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.RoundFinishedMessage:
								HandleRoundFinishedMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.RegistrationResultMessage:
								HandleRegistrationResultMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.SolicitPasswordResultMessage:
								HandleSolicitPasswordResultMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.PasswordResetResultMessage:
								HandlePasswordResetResultMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.PlayerIsDeadMessage:
								HandlePlayerDiedMessage( handler, incomingMessage );
								break;
							case GameMessageTypes.ExitWaitingRoomMessage:
								HandleExitWaitingRoomMessage( handler, incomingMessage );
								break;
						}
						break;
				}
				Recycle( incomingMessage );
			}
		}

		private void HandleTestConnectionMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			TestConnectionMessage testMessage = new TestConnectionMessage( incomingMessage );
			( ( OptionsState )handler.GetState( StateTypes.OptionsState ) ).SetMessage( testMessage.Result );
		}

		private void HandleLoginResultMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			LoginResultMessage resultMessage = new LoginResultMessage( incomingMessage );
			if( resultMessage.LoginRequestResult )
			{
				UserSession.Login( new User( resultMessage.User ), new Account( resultMessage.Account ) );
				handler.ChangeState( StateTypes.MainMenuState );
			}
			else
			{
				( ( LoginState )handler.GetCurrentState() ).SetErrorMessage( resultMessage.Reason );
			}
		}

		private void HandleRegistrationResultMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			RegistrationResultMessage resultMessage = new RegistrationResultMessage( incomingMessage );
			if( string.Equals( resultMessage.Message, handler.StringManager.GetString( StringNames.RegistrationSuccessful ) ) )
			{
				( ( UserRegistryState )handler.GetCurrentState() ).SetMessage( resultMessage.Message );
				handler.GetCurrentState().ClearTextAreas();
			}
			else
			{
				( ( UserRegistryState )handler.GetCurrentState() ).SetErrorMessage( resultMessage.Message );
			}
		}

		private void HandleSolicitPasswordResultMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			SolicitPasswordResultMessage resultMessage = new SolicitPasswordResultMessage( incomingMessage );
			if( string.Equals( resultMessage.Message, handler.StringManager.GetString( StringNames.PasswordEmailSuccess ) ) )
			{
				( ( PasswordRecoveryState )handler.GetCurrentState() ).SetMessage( resultMessage.Message );
			}
			else
			{
				( ( PasswordRecoveryState )handler.GetCurrentState() ).SetErrorMessage( resultMessage.Message );
			}
		}

		private void HandlePasswordResetResultMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PasswordResetResultMessage resultMessage = new PasswordResetResultMessage( incomingMessage );
			if( resultMessage.Result.Equals( handler.StringManager.GetString( StringNames.PasswordResetSuccessful ) ) )
			{
				( ( PasswordRecoveryState )handler.GetCurrentState() ).SetMessage( resultMessage.Result );
				handler.GetCurrentState().ClearTextAreas();
			}
			else
			{
				( ( PasswordRecoveryState )handler.GetCurrentState() ).SetErrorMessage( resultMessage.Result );
			}
		}

		private void HandleJoinRoundRequestResultMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			if( incomingMessage.ReadBoolean() )
			{
				handler.ChangeState( StateTypes.WaitingRoomState );
				( ( WaitingRoomState )handler.GetCurrentState() ).UsernameManager.HandleRoundStateChangeMessage( incomingMessage );
			}
		}

		private void HandleExitWaitingRoomMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			handler.ChangeState( StateTypes.MainMenuState );
		}

		private void HandleRoundStateChangedMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			( ( WaitingRoomState )handler.GetCurrentState() ).UsernameManager.HandleRoundStateChangeMessage( incomingMessage );
		}

		private void HandleChatMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			( ( WaitingRoomState )handler.GetState( StateTypes.WaitingRoomState ) ).MessageManager.
				HandleChatMessage( incomingMessage.ReadString(), incomingMessage.ReadString(), UserSession.CurrentUser.Username );
		}

		private void HandleStartRoundMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			handler.ChangeState( StateTypes.PlayState );
			( ( PlayState )handler.GetCurrentState() ).PlayerManager.CreatePlayers( handler.GetCurrentState().Content, handler,
																					incomingMessage, UserSession.CurrentUser.Username );
		}

		private void HandlePlayerStateChangeMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PlayerStateChangeMessage stateChangeMessage = new PlayerStateChangeMessage( incomingMessage );
			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).PlayerManager.HandlePlayerStateChangeMessage( incomingMessage,
																												    stateChangeMessage );
		}

		private void HandleSpawnBoxMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).ItemManager.HandleSpawnBoxMessage( incomingMessage );
		}

		private void HandleSpawnConsumablesMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).ItemManager.HandleSpawnConsumablesMessage( incomingMessage );
		}

		private void HandleBoxDamageMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			BoxDamageMessage boxDamageMessage = new BoxDamageMessage( incomingMessage );
			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).ItemManager.HandleBoxDamageMessage( boxDamageMessage );
		}

		private void HandlePlayerAttackMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PlayerAttackMessage playerAttackMessage = new PlayerAttackMessage( incomingMessage );
			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).PlayerManager.HandlePlayerAttackMessage( playerAttackMessage );
		}

		private void HandlePickedUpItemMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PickedUpItemMessage itemMessage = new PickedUpItemMessage( incomingMessage );
			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).ItemManager.HandlePickedUpItemMessage( 
																					itemMessage.ItemIndex );
		}

		private void HandleSpawnGrenadeMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			SpawnGrenadeMessage grenadeMessage = new SpawnGrenadeMessage( incomingMessage );
			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).ItemManager.HandleSpawnGrenadeMessage( grenadeMessage.Position, 
																											 grenadeMessage.Direction, 
																										     grenadeMessage.GrenadeSpeed );
		}

		private void HandleUpdateRemainingTimeMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			UpdateRoundTimeMessage timeMessage = new UpdateRoundTimeMessage( incomingMessage );
			float timeDelay = ( float )( NetTime.Now - incomingMessage.SenderConnection.GetLocalTime( timeMessage.MessageTime ) );
			float newTimeWithDelay = timeMessage.RemainingRoundTime - timeDelay;
			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).DisplayManager.HandleUpdateRoundTimeMessage( newTimeWithDelay );
		}

		private void HandleRoundFinishedMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			RoundFinishedMessage endMessage = new RoundFinishedMessage( incomingMessage );
			bool isLocalPlayerDead = ( ( PlayState )handler.GetCurrentState() ).PlayerManager.GetLocalPlayer().Health.IsDead();
			bool didLocalPlayerWin = ( ( PlayState )handler.GetCurrentState() ).PlayerManager.DidLocalPlayerWin();
			UserSession.UpdateRoundStatistics( isLocalPlayerDead, didLocalPlayerWin, endMessage.RemainingRoundTime );
			handler.ChangeState( StateTypes.WaitingRoomState );

			UpdateUserStatisticsMessage updateMessage = new UpdateUserStatisticsMessage( UserSession.CurrentUser, 
																						 UserSession.CurrentAccount );
			NetOutgoingMessage outgoingMessage = CreateMessage();
			outgoingMessage.Write( ( byte )updateMessage.MessageType );
			updateMessage.Encode( outgoingMessage );

			netClient.SendMessage( outgoingMessage, NetDeliveryMethod.ReliableUnordered );
		}

		private void HandlePlayerDiedMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PlayerIsDeadMessage deathMessage = new PlayerIsDeadMessage( incomingMessage );
			( ( PlayState )handler.GetState( StateTypes.PlayState ) ).PlayerManager.HandlePlayerDiedMessage( deathMessage.PlayerId );
		}

		/// <summary>
		/// Sends a RegisterUserMessage to server to create a new user
		/// in the database.
		/// </summary>
		/// <param name="nameIn">User name</param>
		/// <param name="lastNameIn">User last names</param>
		/// <param name="usernameIn"> User username</param>
		/// <param name="emailIn">User email</param>
		/// <param name="passwordIn">User password</param>
		public void RegisterUser( string nameIn, string lastNameIn, string usernameIn, string emailIn, string passwordIn )
		{
			SendMessage( new RegisterUserMessage( new User( nameIn, lastNameIn, usernameIn, emailIn, passwordIn ) ) );
		}

		/// <summary>
		/// Method unused by client.
		/// </summary>
		public void CreateRound() {}

		/// <summary>
		/// Method unused by client.
		/// </summary>
		public void StartRound() {}

		/// <summary>
		/// Sends a JoinRoundRequestMessage to server.
		/// </summary>
		public void JoinRound()
		{
			SendMessage( new JoinRoundRequestMessage( UserSession.CurrentUser.Username ) );
		}

		/// <summary>
		/// Sends a LeaveRoundMessage to the server
		/// </summary>
		public void LeaveRound()
		{
			SendMessage( new LeaveRoundMessage( UserSession.CurrentUser.Username ) );
		}

		/// <summary>
		/// Sends a chat message to the server
		/// </summary>
		/// <param name="message"></param>
		public void SendChatMessage( string message )
		{
			SendMessage( new ChatMessage( UserSession.CurrentUser.Username, message ) );
		}

		/// <summary>
		/// Sends a SolicitPasswordResetMessage to the server.
		/// </summary>
		/// <param name="emailIn"></param>
		public void SendPasswordChangeMessage( string emailIn ) 
		{
			SendMessage( new SolicitPasswordResetMessage( emailIn ) );
		}

		/// <summary>
		/// Sends a PlayerStateChangedMessage to the server
		/// </summary>
		/// <param name="player"></param>
		public void SendPlayerStateChangeMessage( GameObject player )
		{
			SendMessage( new PlayerStateChangeMessage( player ) );
		}

		/// <summary>
		/// Method unused by client
		/// </summary>
		/// <param name="boxes"></param>
		public void SendSpawnBoxMessage( List< GameObject > boxes ) {}

		/// <summary>
		/// Method unused by client
		/// </summary>
		/// <param name="consumables"></param>
		public void SendSpawnConsumablesMessage( List< GameObject > consumables ) {}

		/// <summary>
		/// Sends a BoxDamageMessage to server.
		/// </summary>
		/// <param name="boxIndex">Target box index</param>
		/// <param name="damage">Damage sustained</param>
		public void SendBoxDamageMessage( int boxIndex, int damage )
		{
			SendMessage( new BoxDamageMessage( boxIndex, damage ) );
		}

		/// <summary>
		/// Sends a PlayerAttackMessage to server
		/// </summary>
		/// <param name="localPlayerIndex">local player index</param>
		public void SendPlayerAttackMessage( Identifiers localPlayerIndex )
		{
			SendMessage( new PlayerAttackMessage( localPlayerIndex ) );
		}

		/// <summary>
		/// Sends a PickedUpIte message to server
		/// </summary>
		/// <param name="itemIndex">Item index</param>
		public void SendPickedUpItemMessage( int itemIndex )
		{
			SendMessage( new PickedUpItemMessage( itemIndex ) );
		}

		/// <summary>
		/// Sends s SpawnGrenadeMessage to server
		/// </summary>
		/// <param name="grenade"></param>
		public void SendSpawnGrenadeMessage( GameObject grenade )
		{
			SendMessage( new SpawnGrenadeMessage( grenade ) );
		}

		/// <summary>
		/// Method unused by client
		/// </summary>
		/// <param name="remainingTime"></param>
		public void SendUpdateRemainingTimeMessage( float remainingTime ) {}

		/// <summary>
		/// Method unused by client
		/// </summary>
		/// <param name="remainingRoundTime"></param>
		public void SendRoundFinishedMessage( int remainingRoundTime ) {}

		/// <summary>
		/// Sends a PlayerIsDeadMessage to server.
		/// </summary>
		/// <param name="playerId"></param>
		public void SendPlayerDiedMessage( Identifiers playerId )
		{
			SendMessage( new PlayerIsDeadMessage( playerId ) );
		}

		/// <summary>
		/// Sends a PasswordReset message to server.
		/// </summary>
		/// <param name="code">Generated code</param>
		/// <param name="email">user email</param>
		/// <param name="password">new password</param>
		public void UpdatePassword( string code, string email, string password ) 
		{
			SendMessage( new PasswordResetMessage( code, email, password ) );
		}

		///<value>The current user session.</value>
		public LoginSession UserSession { get; private set; }
		private NetClient netClient;
		private bool isDisposed;
	}
}