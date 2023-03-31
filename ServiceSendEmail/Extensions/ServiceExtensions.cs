using Microsoft.Extensions.DependencyInjection;
using ServiceSendEmail.Abstracts;
using ServiceSendEmail.Services;

namespace ServiceSendEmail.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services) =>
			services.AddScoped<IEmailService, EmailService>();
	}
}
