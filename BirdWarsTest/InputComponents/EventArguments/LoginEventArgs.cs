using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.InputComponents.EventArguments
{
	class LoginEventArgs : EventArgs
	{
		public string email = "";
		public string password = "";
	}
}
