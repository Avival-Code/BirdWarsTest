using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.InputComponents.EventArguments
{
	class RegisterEventArgs : EventArgs
	{
		public string name = "";
		public string lastNames = "";
		public string username = "";
		public string email = "";
		public string password = "";
		public string confirmPassword = "";
	}
}
