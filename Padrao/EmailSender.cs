using System.Net;
using System.Net.Mail;

namespace Padrao
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("seuemail@hotmail.com", "suasenha")
            };

            return client.SendMailAsync(
                new MailMessage(from: "seuemail@hotmail.com",
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}
