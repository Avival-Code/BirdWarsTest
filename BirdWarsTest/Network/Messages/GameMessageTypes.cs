/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Used to keep track of all game message types.
*********************************************/
namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Used to keep track of all game message types.
	/// </summary>
	public enum GameMessageTypes
	{
		/// <summary>
		/// Chat message value
		/// </summary>
		ChatMessage,
		/// <summary>
		/// Join Round Request message value
		/// </summary>
		JoinRoundRequestMessage,
		/// <summary>
		/// Join Round Request Result message value
		/// </summary>
		JoinRoundRequestResultMessage,
		/// <summary>
		/// Login Request message value
		/// </summary>
		LoginRequestMessage,
		/// <summary>
		/// Login Result message value
		/// </summary>
		LoginResultMessage,
		/// <summary>
		/// Leave Round message value
		/// </summary>
		LeaveRoundMessage,
		/// <summary>
		/// Register user message value
		/// </summary>
		RegisterUserMessage,
		/// <summary>
		/// Round created message value
		/// </summary>
		RoundCreatedMessage,
		/// <summary>
		/// Round state changed message value
		/// </summary>
		RoundStateChangedMessage,
		/// <summary>
		/// Password reset message value
		/// </summary>
		PasswordResetMessage,
		/// <summary>
		/// Password Reset result message value
		/// </summary>
		PasswordResetResultMessage,
		/// <summary>
		/// Solicit password reset message value
		/// </summary>
		SolicitPasswordResetMessage,
		/// <summary>
		/// Solicit password result message value
		/// </summary>
		SolicitPasswordResultMessage,
		/// <summary>
		/// Start round message value
		/// </summary>
		StartRoundMessage,
		/// <summary>
		/// Player state change message value
		/// </summary>
		PlayerStateChangeMessage,
		/// <summary>
		/// Spawn box message value
		/// </summary>
		SpawnBoxMessage,
		/// <summary>
		/// Box damage message value
		/// </summary>
		BoxDamageMessage,
		/// <summary>
		/// Player attack message value
		/// </summary>
		PlayerAttackMessage,
		/// <summary>
		/// Spawn consumables message value
		/// </summary>
		SpawnConsumablesMessage,
		/// <summary>
		/// Picked up item message value
		/// </summary>
		PickedUpItemMessage,
		/// <summary>
		/// Spawn Grenade message value
		/// </summary>
		SpawnGrenadeMessage,
		/// <summary>
		/// Update Round Time message value
		/// </summary>
		UpdateRoundTimeMessage,
		/// <summary>
		/// Update User statistics message value
		/// </summary>
		UpdateUserStatisticsMessage,
		/// <summary>
		/// Round Finished message value
		/// </summary>
		RoundFinishedMessage,
		/// <summary>
		/// Registration result message value
		/// </summary>
		RegistrationResultMessage,
		/// <summary>
		/// Player is dead message value
		/// </summary>
		PlayerIsDeadMessage,
		/// <summary>
		/// Exit waiting room message value
		/// </summary>
		ExitWaitingRoomMessage,
		/// <summary>
		/// Test connection message value
		/// </summary>
		TestConnectionMessage,
		/// <summary>
		/// Ban player message value
		/// </summary>
		BanPlayerMessage,
		/// <summary>
		/// Adjusted player state change message value
		/// </summary>
		AdjustedPlayerStateChangeMessage
	}
}