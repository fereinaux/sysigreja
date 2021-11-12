using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public EmailSettings _emailSettings { get; }

        public void SendEmail(string email, string subject, string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Realidade")
                };

                mail.To.Add(new MailAddress(toEmail));

                mail.Subject = "Realidade - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //

                using (var smtp = new SmtpClient(_emailSettings.PrimaryDomain))
                {
                    smtp.EnableSsl = true;
                    smtp.Port = _emailSettings.PrimaryPort;
                    smtp.UseDefaultCredentials = false;
                    smtp.Timeout = 2000000;

                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);

                    smtp.Send(mail);
                }
            }
            catch (Exception)
            {

            }
        }

    }
}