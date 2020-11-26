using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.InputComponents.EventArguments
{
	class LoginEventArgs : EventArgs
	{
		public LoginEventArgs()
		{
			Email = "";
			Password = "";
		}
		public void ResetArgs()
		{
			Email = "";
			Password = "";
		}

		public string Email { get; set; }
		public string Password { get; set; }
	}
}