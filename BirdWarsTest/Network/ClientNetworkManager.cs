using BirdWarsTest.Database;
using BirdWarsTest.GameObjects;
using BirdWarsTest.Network.Messages;
using BirdWarsTest.States;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Net;

namespace BirdWarsTest.Network
{
	public class ClientNetworkManager : INetworkManager
	{
		public ClientNetworkManager()
		{
			UserSession = new LoginSession();
			Connect();
		}

		public void Login( string email, string password )
		{
			SendMessage( new LoginRequestMessage( email, password ) );
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
						}
						break;
				}
				Recycle( incomingMessage );
			}
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
				HandleChatMessage( incomingMessage.ReadString(), incomingMessage.ReadString(), UserSession.CurrentUser.Username );
		}

		private void HandleStartRoundMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			handler.ChangeState( StateTypes.PlayState );
			( ( PlayState )handler.GetCurrentState() ).PlayerManager.CreatePlayers( handler.GetCurrentState().Content, handler,
																					incomingMessage, UserSession.CurrentUser.Username );
		}

		public void HandlePlayerStateChangeMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PlayerStateChangeMessage stateChangeMessage = new PlayerStateChangeMessage( incomingMessage );
			( ( PlayState )handler.GetCurrentState() ).PlayerManager.HandlePlayerStateChangeMessage( incomingMessage,
																									 stateChangeMessage );
		}

		public void HandleSpawnBoxMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			( ( PlayState )handler.GetCurrentState() ).ItemManager.HandleSpawnBoxMessage( incomingMessage );
		}

		public void HandleSpawnConsumablesMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			( ( PlayState )handler.GetCurrentState() ).ItemManager.HandleSpawnConsumablesMessage( incomingMessage );
		}

		public void HandleBoxDamageMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			BoxDamageMessage boxDamageMessage = new BoxDamageMessage( incomingMessage );
			( ( PlayState )handler.GetCurrentState() ).ItemManager.HandleBoxDamageMessage( boxDamageMessage );
		}

		public void HandlePlayerAttackMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			PlayerAttackMessage playerAttackMessage = new PlayerAttackMessage( incomingMessage );
			( ( PlayState )handler.GetCurrentState() ).PlayerManager.HandlePlayerAttackMessage( playerAttackMessage );
		}

		public void HandlePickedUpItemMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			( ( PlayState )handler.GetCurrentState() ).ItemManager.HandlePickedUpItemMessage( incomingMessage.ReadInt32() );
		}

		private void HandleSpawnGrenadeMessage( StateHandler handler, NetIncomingMessage incomingMessage )
		{
			SpawnGrenadeMessage grenadeMessage = new SpawnGrenadeMessage( incomingMessage );
			( ( PlayState )handler.GetCurrentState() ).ItemManager.HandleSpawnGrenadeMessage( grenadeMessage.Position, 
																							  grenadeMessage.Direction, 
																							  grenadeMessage.GrenadeSpeed );
		}

		public void RegisterUser( string nameIn, string lastNameIn, string usernameIn, string emailIn, string passwordIn )
		{
			SendMessage( new RegisterUserMessage( new User( nameIn, lastNameIn, usernameIn, emailIn, passwordIn ) ) );
		}

		public void CreateRound() {}

		public void StartRound() {}

		public void JoinRound()
		{
			SendMessage( new JoinRoundRequestMessage( UserSession.CurrentUser.Username ) );
		}

		public void LeaveRound()
		{
			SendMessage( new LeaveRoundMessage( UserSession.CurrentUser.Username ) );
		}

		public void SendChatMessage( string message )
		{
			SendMessage( new ChatMessage( UserSession.CurrentUser.Username, message ) );
		}

		public void SendPasswordChangeMessage( string emailIn ) 
		{
			SendMessage( new SolicitPasswordResetMessage( emailIn ) );
		}

		public void SendPlayerStateChangeMessage( GameObject player )
		{
			SendMessage( new PlayerStateChangeMessage( player ) );
		}

		public void SendSpawnBoxMessage( List< GameObject > boxes ) {}

		public void SendSpawnConsumablesMessage( List< GameObject > consumables ) {}

		public void SendBoxDamageMessage( int boxIndex, int damage )
		{
			SendMessage( new BoxDamageMessage( boxIndex, damage ) );
		}

		public void SendPlayerAttackMessage( Identifiers localPlayerIndex )
		{
			SendMessage( new PlayerAttackMessage( localPlayerIndex ) );
		}

		public void SendPickedUpItemMessage( int itemIndex )
		{
			SendMessage( new PickedUpItemMessage( itemIndex ) );
		}

		public void SendSpawnGrenadeMessage( GameObject grenade )
		{
			SendMessage( new SpawnGrenadeMessage( grenade ) );
		}

		public void UpdatePassword( string code, string password ) 
		{
			SendMessage( new PasswordResetMessage( code, password ) );
		}

		public LoginSession UserSession { get; private set; }
		private NetClient netClient;
		private bool isDisposed;
	}
}