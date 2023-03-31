
namespace ServiceSendEmail.Models
{
	public class EmailRequest
	{
		public string FromEmail { get; set; } = string.Empty;
		public string ToEmail { get; set; } = string.Empty;
		public string Subject { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;

		public bool Invalid => 
			string.IsNullOrEmpty(FromEmail) ||
			string.IsNullOrEmpty(ToEmail) ||
			string.IsNullOrEmpty(Subject) ||
			string.IsNullOrEmpty(Body);
	}
}
