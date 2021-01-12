﻿/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Message used to distribute user chat messages 
*********************************************/
using Lidgren.Network;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Message used to distribute user chat messages 
	/// </summary>
	public class ChatMessage : IGameMessage
	{
		/// <summary>
		/// Creates the game message from an incoming message
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public ChatMessage( NetIncomingMessage incomingMessage )
		{
			Decode( incomingMessage );
		}

		/// <summary>
		/// Creates the game message from a username input
		/// </summary>
		/// <param name="senderUsernameIn">Sender username input</param>
		/// <param name="messageIn">Message body input</param>
		public ChatMessage( string senderUsernameIn, string messageIn )
		{
			SenderUsername = senderUsernameIn;
			Message = messageIn;
		}

		/// <summary>
		/// Returns the message type
		/// </summary>
		public GameMessageTypes MessageType
		{
			get{ return GameMessageTypes.ChatMessage; }
		}

		/// <summary>
		/// Decodes the incoming message data.
		/// </summary>
		/// <param name="incomingMessage">The incoming message</param>
		public void Decode( NetIncomingMessage incomingMessage )
		{
			SenderUsername = incomingMessage.ReadString();
			Message = incomingMessage.ReadString();
		}

		/// <summary>
		/// Writes the current message data to an outgoing message.
		/// </summary>
		/// <param name="outgoingMessage">The target outgoing message</param>
		public void Encode( NetOutgoingMessage outgoingMessage )
		{
			outgoingMessage.Write( SenderUsername );
			outgoingMessage.Write( Message );
		}

		///<value>The username of the player who sent the message</value>
		public string SenderUsername { get; private set; }

		///<value>The message body</value>
		public string Message { get; private set; }
	}
}