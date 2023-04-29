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
                Credentials = new NetworkCredential("fukuta2003@hotmail.com", "fpc250272#")
            };

            return client.SendMailAsync(
                new MailMessage(from: "fukuta2003@hotmail.com",
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}
