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
	public class ChatMessageArgs : EventArgs
	{
		/// <value>String message.</value>
		public string Message { get; set; }
	}
}