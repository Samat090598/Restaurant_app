using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Restaurant_app
{
    public class EmailService
    {
        public static async Task SendEmailAsync(string pdf)
        {
            if (File.Exists(pdf))
            {
                MailAddress from = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["mailAddress"],
                    System.Configuration.ConfigurationManager.AppSettings["displayName"]);
                //Здесь вводит почту на которую нужно отправить сообщение
                MailAddress to = new MailAddress("muratsamat090598@gmail.com");
                using (MailMessage message = new MailMessage(from, to))
                {
                    message.Attachments.Add(new Attachment(pdf));
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.mail.ru",
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        EnableSsl = true,
                        Credentials = new NetworkCredential(from.Address, 
                            System.Configuration.ConfigurationManager.AppSettings["mailPassword"]),
                        Timeout = 20000,
                    };
                    await smtp.SendMailAsync(message);
                    Console.WriteLine("Письмо отправлено");    
                }
                // Удалил Pdf Файл
                if (File.Exists(pdf))
                {
                    File.Delete(pdf);
                }
            }
        }
    }
}