using System;
using System.Collections.Generic;
using System.Text;

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
