using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Network.Messages
{
	public enum GameMessageTypes
	{
		LoginRequestMessage,
		LoginResultMessage,
		registerUserMessage,
		RoundCreatedMessage,
		JoinRoundRequestMessage,
		JoinRoundRequestResultMessage,
		RoundStateChangedMessage
	}
}
