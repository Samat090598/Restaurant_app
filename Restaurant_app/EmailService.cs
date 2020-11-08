using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Restaurant_app
{
    public class EmailService
    {
        private const string _pdf = "Index.PDF";
        
        public static async Task SendEmailAsync()
        {
            if (File.Exists(_pdf))
            {
                MailAddress from = new MailAddress("example2008@inbox.ru", "Example");
                //Здесь вводит почту на которую нужно отправить сообщение
                MailAddress to = new MailAddress("muratsamat090598@gmail.com");
                MailMessage m = new MailMessage(from, to);
                m.Attachments.Add(new Attachment(Directory.GetCurrentDirectory() + "\\" + _pdf));
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.mail.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(from.Address, "Dj#d_9A-bqBmg_R"),
                    Timeout = 20000,
                };
                await smtp.SendMailAsync(m);
                Console.WriteLine("Письмо отправлено");
            }
        }
    }
}