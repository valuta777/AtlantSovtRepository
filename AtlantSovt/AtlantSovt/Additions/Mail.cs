using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace AtlantSovt.Additions
{
    class Mail
    {
        public static void Send()
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("atlantsovt.logs@gmail.com");
                mail.To.Add(new MailAddress("atlantsovt.logs@gmail.com"));
                mail.Subject = DateTime.Now.ToString();
                string filePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Log\\" + Environment.MachineName + "." + DateTime.Now.ToShortDateString() + ".log").Replace("\\bin\\Release", "");
                if (!Directory.Exists(filePath))
                {
                    mail.Attachments.Add(new Attachment(filePath));
                }
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("atlantsovt.logs@gmail.com".Split('@')[0], "atlantsovt");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }
    }
}
