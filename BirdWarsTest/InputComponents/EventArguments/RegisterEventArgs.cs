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
	public class RegisterEventArgs : EventArgs
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public RegisterEventArgs()
		{
			Name = "";
			LastNames = "";
			Username = "";
			Email = "";
			Password = "";
			ConfirmPassword = "";
		}

		/// <summary>
		/// Resets arguments to empty string values.
		/// </summary>
		public void ClearRegisterArgs()
		{
			Name = "";
			LastNames = "";
			Username = "";
			Email = "";
			Password = "";
			ConfirmPassword = "";
		}

		/// <value>String email.</value>
		public string Name { get; set; }

		/// <value>String lastNames.</value>
		public string LastNames { get; set; }

		/// <value>String username.</value>
		public string Username { get; set; }

		/// <value>String email.</value>
		public string Email { get; set; }

		/// <value>String password.</value>
		public string Password { get; set; }

		/// <value>String confirm password.</value>
		public string ConfirmPassword { get; set; }
	}
}