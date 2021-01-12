/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles the creation and sending of email messages.
*********************************************/
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.IO;
using System.Reflection;

namespace BirdWarsTest.Network.Messages
{
	/// <summary>
	/// Handles the creation and sending of email messages.
	/// </summary>
	public class EmailManager
	{
		/// <summary>
		/// Creates an instance of the email maanger with the default
		/// values.
		/// </summary>
		public EmailManager()
		{
			senderName = "BirdWarsAdmin";
			server = "smtp.gmail.com";
			LoadLoginInformation();
			port = 465;
		}

		private void LoadLoginInformation()
		{
			string fileName;
			string filePath;
			string[] tempStrings;

			try
			{
				fileName = @"Email.txt";
				filePath = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), fileName );
				tempStrings = File.ReadAllLines( filePath );
				if( !string.IsNullOrEmpty( tempStrings[ 0 ] ) )
				{
					senderEmail = tempStrings[ 0 ];
					senderPassword = tempStrings[ 1 ];
				}
			}
			catch( FileNotFoundException e )
			{
				Console.WriteLine( e.Message );
			}
		}

		private MimeMessage CreateMessage( string recipientName, string recipientEmail, string subject,
										  string body )
		{
			var mailMessage = new MimeMessage();
			mailMessage.From.Add( new MailboxAddress( senderName, senderEmail ) );
			mailMessage.To.Add( new MailboxAddress( recipientName, recipientEmail ) );
			mailMessage.Subject = subject;
			mailMessage.Body = new TextPart( "plain" )
			{
				Text = body
			};
			return mailMessage;
		}

		/// <summary>
		/// Sends an email message to the specified email with the input 
		/// subject, body and recipient name.
		/// </summary>
		/// <param name="recipientName">Recipient</param>
		/// <param name="recipientEmail">Recipient email</param>
		/// <param name="subject">Email subject</param>
		/// <param name="body">Email message body</param>
		public void SendEmailMessage( string recipientName, string recipientEmail, string subject,
									  string body )
		{
			ConfigureSMTPAndSend( CreateMessage( recipientName, recipientEmail, subject, body ) );
		}

		private void ConfigureSMTPAndSend( MimeMessage message )
		{
			Console.WriteLine( "Sending email..." );
			try
			{
				using (var smtpClient = new SmtpClient())
				{
					smtpClient.ServerCertificateValidationCallback =
						(mysender, certificate, chain, sslpolicyerror) => { return true; };
					smtpClient.CheckCertificateRevocation = false;
					smtpClient.Connect( server, port, true );
					smtpClient.Authenticate( senderEmail, senderPassword );
					smtpClient.Send( message );
					smtpClient.Disconnect( true );
				}
			}
			catch( Exception exception )
			{
				Console.Write( exception.Message );
			}
		}

		private readonly string senderName;
		private string senderEmail;
		private string senderPassword;
		private readonly string server;
		private readonly int port;
	}
}
