using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mail
{
	public interface IMailSender
	{
		public void SendEmailsForActivity (List<Tuple<string, string>> displayNameAndEmail, Activity activity);

		public void SendAccountCreatedEmail(string displayName, string email);

	}
}
