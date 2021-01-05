using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	public enum GameMessageTypes
	{
		ChatMessage,
		JoinRoundRequestMessage,
		JoinRoundRequestResultMessage,
		LoginRequestMessage,
		LoginResultMessage,
		LeaveRoundMessage,
		registerUserMessage,
		RoundCreatedMessage,
		RoundStateChangedMessage,
		PasswordResetMessage,
		SolicitPasswordResetmessage,
		StartRoundMessage,
		PlayerStateChangeMessage,
		SpawnBoxMessage,
		BoxDamageMessage,
		PlayerAttackMessage,
		SpawnConsumablesMessage,
		PickedUpItemMessage,
		SpawnGrenadeMessage,
		UpdateRoundTimeMessage,
		UpdateUserStatisticsMessage,
		RoundFinishedMessage
	}
}
