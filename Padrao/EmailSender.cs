using System.Net;
using System.Net.Mail;

namespace Oficina7
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {


            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("fukuta2003@hotmail.com", "fpc250272#"),
                
            };

            var msg = new MailMessage(from: "fukuta2003@hotmail.com",
                                to: email,
                                subject,
                                message
                                );

            msg.IsBodyHtml = true;

            return client.SendMailAsync(msg);
        }
    }
}
