using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace BirdWarsTest.Network.Messages
{
	public class EmailManager
	{
		public EmailManager()
		{
			senderName = "BirdWarsAdmin";
			senderEmail = "NoReply.BirdwarsAdmn@gmail.com";
			senderPassword = "12@testEmail?";
			server = "smtp.gmail.com";
			port = 465;
		}

		public MimeMessage CreateMessage( string recipientName, string recipientEmail, string subject,
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

		public void SendEmailMessage( string recipientName, string recipientEmail, string subject,
									  string body )
		{
			ConfigureSMTPAndSend( CreateMessage( recipientName, recipientEmail, subject, body ) );
		}

		public void ConfigureSMTPAndSend( MimeMessage message )
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

		private string senderName;
		private string senderEmail;
		private string senderPassword;
		private string server;
		private int port;
	}
}
