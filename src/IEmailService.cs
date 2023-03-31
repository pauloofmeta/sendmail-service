
using ServiceSendEmail.Models;

namespace ServiceSendEmail.Abstracts
{
	public interface IEmailService
	{
		void Send(EmailRequest request);
	}
}
