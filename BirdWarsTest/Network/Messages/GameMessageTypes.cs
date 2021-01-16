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
		/// 
		/// </summary>
		ChatMessage,
		/// <summary>
		/// 
		/// </summary>
		JoinRoundRequestMessage,
		/// <summary>
		/// 
		/// </summary>
		JoinRoundRequestResultMessage,
		/// <summary>
		/// 
		/// </summary>
		LoginRequestMessage,
		/// <summary>
		/// 
		/// </summary>
		LoginResultMessage,
		/// <summary>
		/// 
		/// </summary>
		LeaveRoundMessage,
		/// <summary>
		/// 
		/// </summary>
		RegisterUserMessage,
		/// <summary>
		/// 
		/// </summary>
		RoundCreatedMessage,
		/// <summary>
		/// 
		/// </summary>
		RoundStateChangedMessage,
		/// <summary>
		/// 
		/// </summary>
		PasswordResetMessage,
		/// <summary>
		/// 
		/// </summary>
		PasswordResetResultMessage,
		/// <summary>
		/// 
		/// </summary>
		SolicitPasswordResetMessage,
		/// <summary>
		/// 
		/// </summary>
		SolicitPasswordResultMessage,
		/// <summary>
		/// 
		/// </summary>
		StartRoundMessage,
		/// <summary>
		/// 
		/// </summary>
		PlayerStateChangeMessage,
		/// <summary>
		/// 
		/// </summary>
		SpawnBoxMessage,
		/// <summary>
		/// 
		/// </summary>
		BoxDamageMessage,
		/// <summary>
		/// 
		/// </summary>
		PlayerAttackMessage,
		/// <summary>
		/// 
		/// </summary>
		SpawnConsumablesMessage,
		/// <summary>
		/// 
		/// </summary>
		PickedUpItemMessage,
		/// <summary>
		/// 
		/// </summary>
		SpawnGrenadeMessage,
		/// <summary>
		/// 
		/// </summary>
		UpdateRoundTimeMessage,
		/// <summary>
		/// 
		/// </summary>
		UpdateUserStatisticsMessage,
		/// <summary>
		/// 
		/// </summary>
		RoundFinishedMessage,
		/// <summary>
		/// 
		/// </summary>
		RegistrationResultMessage,
		/// <summary>
		/// 
		/// </summary>
		PlayerIsDeadMessage,
		/// <summary>
		/// 
		/// </summary>
		ExitWaitingRoomMessage,
		/// <summary>
		/// 
		/// </summary>
		TestConnectionMessage,
		/// <summary>
		/// 
		/// </summary>
		BanPlayerMessage,
		/// <summary>
		/// 
		/// </summary>
		AdjustedPlayerStateChangeMessage
	}
}