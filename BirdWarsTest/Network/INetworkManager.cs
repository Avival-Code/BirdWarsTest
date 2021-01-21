/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Interface class implemented by client and server network managers.
*********************************************/
using Lidgren.Network;
using BirdWarsTest.States;
using BirdWarsTest.GameObjects;
using BirdWarsTest.Database;
using System;
using System.Collections.Generic;

namespace BirdWarsTest.Network
{
	/// <summary>
	/// Interface class implemented by client and server network managers.
	/// </summary>
	public interface INetworkManager : IDisposable
	{
		/// <summary>
		/// Handles the login process
		/// </summary>
		/// <param name="email">User email</param>
		/// <param name="password">User password</param>
		void Login( string email, string password );

		/// <summary>
		/// Closes the current user session
		/// </summary>
		void Logout();

		/// <summary>
		/// Attempts to connect to server at specified port and ip address.
		/// </summary>
		/// <param name="handler"></param>
		/// <param name="address"></param>
		/// <param name="port"></param>
		void ConnectToSpecificServer( StateHandler handler, string address, string port );

		/// <summary>
		/// Returns current user session.
		/// </summary>
		/// <returns></returns>
		LoginSession GetLoginSession();

		/// <summary>
		/// Sends RegisteruserMessage to self or server.
		/// </summary>
		/// <param name="nameIn"></param>
		/// <param name="lastNameIn"></param>
		/// <param name="usernameIn"></param>
		/// <param name="emailIn"></param>
		/// <param name="passwordIn"></param>
		void RegisterUser( string nameIn, string lastNameIn, string usernameIn,
						   string emailIn, string passwordIn );

		/// <summary>
		/// Sends Password change message to self or server.
		/// </summary>
		/// <param name="emailIn"></param>
		void SendPasswordChangeMessage( string emailIn );

		/// <summary>
		/// Sends ChatMessage to self or server.
		/// </summary>
		/// <param name="message"></param>
		void SendChatMessage( string message );

		/// <summary>
		/// Sends PlayerStateChangeMessage to self or server.
		/// </summary>
		/// <param name="player"></param>
		void SendPlayerStateChangeMessage( GameObject player );

		/// <summary>
		/// Sends SpawnBoxMessage to self or server.
		/// </summary>
		/// <param name="boxes"></param>
		void SendSpawnBoxMessage( List< GameObject > boxes );

		/// <summary>
		/// Sends SpawnConsumableMessage to self or server.
		/// </summary>
		/// <param name="consumables"></param>
		void SendSpawnConsumablesMessage( List< GameObject > consumables );

		/// <summary>
		/// Sends a BoxDamageMessage to self or clients.
		/// </summary>
		/// <param name="localPlayerID"></param>
		/// <param name="boxIndex"></param>
		/// <param name="damage"></param>
		void SendBoxDamageMessage( Identifiers localPlayerID, int boxIndex, int damage );

		/// <summary>
		/// Sends a PlayerAttackMessage to self or clients.
		/// </summary>
		/// <param name="localPlayerIndex"></param>
		void SendPlayerAttackMessage( Identifiers localPlayerIndex );

		/// <summary>
		/// Sends a PickedUpItemMessage to self or clients.
		/// </summary>
		/// <param name="itemIndex"></param>
		void SendPickedUpItemMessage( int itemIndex );

		/// <summary>
		/// Sends a SpawnGrenadeMessage to self or clients.
		/// </summary>
		/// <param name="localPlayerId">The local player Id</param>
		/// <param name="grenade">The grenade object</param>
		void SendSpawnGrenadeMessage( Identifiers localPlayerId, GameObject grenade );

		/// <summary>
		/// Sends an UpdateRemainingTimeMessage to self or clients.
		/// </summary>
		/// <param name="remainingTime">Remaining round time.</param>
		void SendUpdateRemainingTimeMessage( float remainingTime );

		/// <summary>
		/// Sends a RoundFInishedMessage to self or clients.
		/// </summary>
		/// <param name="remainingRoundTime">Remaining round time.</param>
		void SendRoundFinishedMessage( int remainingRoundTime );

		/// <summary>
		/// Sends a PlayerDiedMessage to self or clients.
		/// </summary>
		/// <param name="playerId">The player Id</param>
		void SendPlayerDiedMessage( Identifiers playerId );

		/// <summary>
		/// Functionality varies on implementation.
		/// </summary>
		/// <param name="code"></param>
		/// <param name="email"></param>
		/// <param name="password"></param>
		void UpdatePassword( string code, string email, string password );

		/// <summary>
		/// Checks if this instance is the host/server.
		/// </summary>
		/// <returns>bool</returns>
		bool IsHost();

		/// <summary>
		/// Connects to the server
		/// </summary>
		void Connect();

		/// <summary>
		/// Disconnects to the server.
		/// </summary>
		void Disconnect();

		/// <summary>
		/// Reads and returns NetIncomingMessage
		/// </summary>
		/// <returns>Reads and returns NetIncomingMessage</returns>
		NetIncomingMessage ReadMessage();

		/// <summary>
		/// Recycles a consumed NetIncoming message.
		/// </summary>
		/// <param name="im">The netincoming message</param>
		void Recycle( NetIncomingMessage im );

		/// <summary>
		/// Creates and returns a NetOutgoing message.
		/// </summary>
		/// <returns>A netoutgoing message</returns>
		NetOutgoingMessage CreateMessage();

		/// <summary>
		/// Processes incoming server or client messages.
		/// </summary>
		/// <param name="handler">The game statehandler</param>
		void ProcessMessages( StateHandler handler );

		/// <summary>
		/// Returns the Netconnection status.
		/// </summary>
		/// <returns>The netconnection status.</returns>
		NetConnectionStatus GetConnectionState();

		/// <summary>
		/// Functionality varies on implementation.
		/// </summary>
		void CreateRound();

		/// <summary>
		/// Functionality varies on implementation.
		/// </summary>
		void StartRound();

		/// <summary>
		/// Functionality varies on implementation.
		/// </summary>
		void JoinRound();

		/// <summary>
		/// Functionality varies on implementation.
		/// </summary>
		void LeaveRound();
	}
}