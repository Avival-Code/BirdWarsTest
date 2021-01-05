using System;

namespace BirdWarsTest.InputComponents.EventArguments
{
	public class LoginEventArgs : EventArgs
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