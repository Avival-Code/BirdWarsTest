using System;

namespace BirdWarsTest.InputComponents.EventArguments
{
	public class PasswordChangeEventArgs : EventArgs
	{
		public PasswordChangeEventArgs()
		{
			Code = "";
			Email = "";
			Password = "";
		}

		public void ResetArgs()
		{
			Code = "";
			Email = "";
			Password = "";
		}

		public string Code { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}