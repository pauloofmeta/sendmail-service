using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.DependencyInjection;
using ServiceSendEmail.Abstracts;
using ServiceSendEmail.Extensions;
using ServiceSendEmail.Models;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ServiceSendEmail;

public class Function
{
	private readonly IServiceProvider _serviceProvider;

	public Function()
	{
		_serviceProvider = new ServiceCollection()
			.AddServices()
			.BuildServiceProvider();
	}

	/// <summary>
	/// Function that send email of message sqs
	/// </summary>
	/// <param name="sqsEvent"></param>
	/// <param name="context"></param>
	/// <returns></returns>
	public string FunctionHandler(SQSEvent sqsEvent, ILambdaContext context)
    {
		Console.WriteLine("Start send email...");

		var messageBody = sqsEvent.Records.FirstOrDefault()?.Body ?? string.Empty;

		try
		{
			var request = JsonSerializer.Deserialize<EmailRequest>(messageBody);
			using var scope = _serviceProvider.CreateScope();
			var service = scope.ServiceProvider.GetRequiredService<IEmailService>();
			service.Send(request!);
			Console.WriteLine("Email sended with success!");
		}
		catch (Exception ex)
		{
			Console.WriteLine("An error occurred while sending email:");
			Console.WriteLine(ex.ToString());
		}
		
		return $"Email sended.";
    }
}
