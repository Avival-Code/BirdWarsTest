/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Simple arguments used to send messages
*********************************************/
using System;

namespace BirdWarsTest.InputComponents.EventArguments
{
	/// <summary>
	/// Simple arguments used to send messages
	/// </summary>
	public class PasswordChangeEventArgs : EventArgs
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		public PasswordChangeEventArgs()
		{
			Code = "";
			Email = "";
			Password = "";
		}

		/// <summary>
		/// Resets arguments to empty string values.
		/// </summary>
		public void ResetArgs()
		{
			Code = "";
			Email = "";
			Password = "";
		}

		/// <value>Message code.</value>
		public string Code { get; set; }

		/// <value>String email</value>
		public string Email { get; set; }

		/// <value>String password</value>
		public string Password { get; set; }
	}
}