using System;

namespace BirdWarsTest.InputComponents.EventArguments
{
	public class CodeEventArgs : EventArgs
	{
		public CodeEventArgs()
		{
			Email = "";
		}

		public void ResetArgs()
		{
			Email = "";
		}

		public string Email { get; set; }
	}
}