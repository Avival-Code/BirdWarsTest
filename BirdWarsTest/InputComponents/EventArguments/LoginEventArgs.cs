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
	public class LoginEventArgs : EventArgs
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public LoginEventArgs()
		{
			Email = "";
			Password = "";
		}

		/// <summary>
		/// Resets arguments to empty string values.
		/// </summary>
		public void ResetArgs()
		{
			Email = "";
			Password = "";
		}

		/// <value>String email.</value>
		public string Email { get; set; }

		/// <value>String email.</value>
		public string Password { get; set; }
	}
}