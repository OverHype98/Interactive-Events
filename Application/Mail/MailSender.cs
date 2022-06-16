using Domain;
using MailKit.Net.Smtp;
using MimeKit;

namespace Application.Mail
{
	public class MailSender : IMailSender
	{
		private MimeMessage Message{ get; set; }
		private string Email { get; set; }
		private string Password { get; set; }
		private const string WebsiteName = "Interactive Events";
		private SmtpClient Client { get; set; }
		public MailSender()
		{
			Message = new MimeMessage();
			Client = new SmtpClient();
			Email = "";
			Password = "";

		}

		private void ConnectClient()
		{
			Client.Connect("smtp.gmail.com", 465, true);
			Client.Authenticate(Email, Password);
		}

		private void DisposeClient()
		{
			Client.Disconnect(true);
			Client.Dispose();
		}

		private void SendEmail(MimeMessage message)
		{
			try
			{
				Client.Send(message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
	
		}

		public void SendEmailsForActivity(List<Tuple<string, string>> displayNameAndEmail, Activity activity)
		{
			ConnectClient();
			foreach (var item in displayNameAndEmail)
			{
				Message = new MimeMessage();
				Message.From.Add(new MailboxAddress(WebsiteName, Email));
				Message.To.Add(new MailboxAddress(item.Item1, item.Item2));
				Message.Subject = $"New Activity: {activity.Title}";

				Message.Body = new TextPart("plain")
				{
					Text = $@"Hello {item.Item1},
							
					A new activity of type {activity.Category} has been created
					Activity description: {activity.Description}"
				};
				SendEmail(Message);
			}
			DisposeClient();
		}

		public void SendAccountCreatedEmail(string displayName, string email)
		{
			ConnectClient();
		
			Message = new MimeMessage();
			Message.From.Add(new MailboxAddress(WebsiteName, Email));
			Message.To.Add(new MailboxAddress(displayName, email));
			Message.Subject = $"Account creation";

			Message.Body = new TextPart("plain")
			{
				Text = $@"Hello {displayName},			
				Your account has been created successfully"
			};
			SendEmail(Message);
			
			DisposeClient();
		}
	}

	
}
