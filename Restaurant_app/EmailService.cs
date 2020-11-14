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
                using (MailMessage message = new MailMessage(from, to))
                {
                    message.Attachments.Add(new Attachment(Directory.GetCurrentDirectory() + "\\" + _pdf));
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.mail.ru",
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        EnableSsl = true,
                        Credentials = new NetworkCredential(from.Address, "Dj#d_9A-bqBmg_R"),
                        Timeout = 20000,
                    };
                    await smtp.SendMailAsync(message);
                    Console.WriteLine("Письмо отправлено");    
                }
                // Удалил Pdf Файл
                if (File.Exists(_pdf))
                {
                    File.Delete(_pdf);
                }
            }
        }
    }
}