using Lidgren.Network;
using BirdWarsTest.States;
using BirdWarsTest.GameObjects;
using BirdWarsTest.Database;
using System;
using System.Collections.Generic;

namespace BirdWarsTest.Network
{
	public interface INetworkManager : IDisposable
	{
		void Login( string email, string password );

		void Logout();

		LoginSession GetLoginSession();

		void RegisterUser( string nameIn, string lastNameIn, string usernameIn,
						   string emailIn, string passwordIn );

		void SendPasswordChangeMessage( string emailIn );

		void SendChatMessage( string message );

		void SendPlayerStateChangeMessage( GameObject player );

		void SendSpawnBoxMessage( List< GameObject > boxes );

		void SendSpawnConsumablesMessage( List< GameObject > consumables );

		void SendBoxDamageMessage( int boxIndex, int damage );

		void SendPlayerAttackMessage( Identifiers localPlayerIndex );

		void SendPickedUpItemMessage( int itemIndex );

		void SendSpawnGrenadeMessage( GameObject grenade );

		void SendUpdateRemainingTimeMessage( float remainingTime );

		void SendRoundFinishedMessage( int remainingRoundTime );

		void UpdatePassword( string code, string password );

		bool IsHost();

		void Connect();

		void Disconnect();

		NetIncomingMessage ReadMessage();

		void Recycle( NetIncomingMessage im );

		NetOutgoingMessage CreateMessage();

		void ProcessMessages( StateHandler handler );

		NetConnectionStatus GetConnectionState();

		void CreateRound();

		void StartRound();

		void JoinRound();

		void LeaveRound();
	}
}