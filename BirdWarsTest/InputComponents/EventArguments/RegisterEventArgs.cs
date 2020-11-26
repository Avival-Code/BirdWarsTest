using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.InputComponents.EventArguments
{
	class RegisterEventArgs : EventArgs
	{
		public RegisterEventArgs()
		{
			Name = "";
			LastNames = "";
			Username = "";
			Email = "";
			Password = "";
			ConfirmPassword = "";
		}

		public void ClearRegisterArgs()
		{
			Name = "";
			LastNames = "";
			Username = "";
			Email = "";
			Password = "";
			ConfirmPassword = "";
		}

		public string Name { get; set; }
		public string LastNames { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}