using System;

namespace BirdWarsTest.InputComponents.EventArguments
{
	public class PasswordChangeEventArgs : EventArgs
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