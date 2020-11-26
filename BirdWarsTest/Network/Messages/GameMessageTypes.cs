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
		registerUserMessage,
		RoundCreatedMessage,
		RoundStateChangedMessage,
		PasswordResetMessage,
		SolicitPasswordResetmessage
	}
}
