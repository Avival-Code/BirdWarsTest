using System;

namespace BirdWarsTest.InputComponents.EventArguments
{
	public class ChatMessageArgs : EventArgs
	{
		public string Message { get; set; }
	}
}