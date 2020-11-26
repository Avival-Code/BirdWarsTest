using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.InputComponents.EventArguments
{
	class PasswordChangeEventArgs : EventArgs
	{
		public PasswordChangeEventArgs()
		{
			Code = "";
			Password = "";
		}

		public void ResetArgs()
		{
			Code = "";
			Password = "";
		}

		public string Code { get; set; }
		public string Password { get; set; }
	}
}
