
using ServiceSendEmail.Models;

namespace ServiceSendEmail.Abstractions
{
	public interface IEmailService
	{
		void Send(EmailRequest request);
	}
}
