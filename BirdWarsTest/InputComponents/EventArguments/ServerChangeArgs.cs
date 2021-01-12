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
	public class ServerChangeArgs : EventArgs
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ServerChangeArgs()
		{
			Address = "";
			Port = "";
		}

		/// <summary>
		/// Resets arguments to empty string values.
		/// </summary>
		public void ResetArgs()
		{
			Address = "";
			Port = "";
		}

		/// <value>String Address.</value>
		public string Address { get; set; }

		/// <value>String port.</value>
		public string Port { get; set; }
	}
}