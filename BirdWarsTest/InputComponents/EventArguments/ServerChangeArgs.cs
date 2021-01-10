using System;

namespace BirdWarsTest.InputComponents.EventArguments
{
	public class ServerChangeArgs : EventArgs
	{
		public ServerChangeArgs()
		{
			Address = "";
			Port = "";
		}

		public void ResetArgs()
		{
			Address = "";
			Port = "";
		}

		public string Address { get; set; }
		public string Port { get; set; }
	}
}