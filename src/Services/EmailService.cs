using ServiceSendEmail.Abstractions;
using ServiceSendEmail.Models;
using System.Net;
using System.Net.Mail;

namespace ServiceSendEmail.Services
{
	public class EmailService : IEmailService
	{
		private const string SmtpHostNameKey = "SMTP_HOST";
		private const string SmtpHostPortKey = "SMTP_PORT";
		private const string SmtpUserKey = "SMTP_USER";
		private const string SmtpPassKey = "SMTP_PASS";

		public void Send(EmailRequest request)
		{
			if (request.Invalid)
				throw new Exception("The email request is invalid!");

			var message = new MailMessage
			{
				IsBodyHtml = true,
				Body = request.Body,
				Subject = request.Subject,
				From = new MailAddress(request.FromEmail)
			};
			message.To.Add(request.ToEmail);

			using var client = GetSmtpClient();
			client.Credentials = GetCredentials();
			client.EnableSsl = true;
			client.Send(message);
		}

		private static SmtpClient GetSmtpClient()
		{
			var hostName = Environment.GetEnvironmentVariable(SmtpHostNameKey);
			var hostPort = int.Parse(Environment.GetEnvironmentVariable(SmtpHostPortKey) ?? "0");

			return new SmtpClient(hostName, hostPort);
		}

		private static NetworkCredential GetCredentials()
		{
			var user = Environment.GetEnvironmentVariable(SmtpUserKey);
			var pass = Environment.GetEnvironmentVariable(SmtpPassKey);

			return new NetworkCredential(user, pass);
		}
	}
}
