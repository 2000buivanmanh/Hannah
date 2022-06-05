using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace ClassLibrary1.MailHelper
{
     public  class GuiMail
    {
        public void SendMailMaCapCha (string GuiTu, string MatKhau, string GuiToi, string NoiDung)
        {
            var fromEmailAddress = GuiTu;
            var fromEmailDisplayName = "Hannahlibrary Mã xác nhận";
            var fromEmailPassword = MatKhau;
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = NoiDung;
            MailMessage messages = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(GuiToi));
            messages.Subject = "Mã xác nhận của bạn";
            messages.IsBodyHtml = true;
            messages.Body = body;

            var client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(messages);
        }

        public void SendMailPhanHoi(string GuiTu, string MatKhau, string TieuDeMail, string GuiToi, string TieuDe, string NoiDung)
        {
            var fromEmailAddress = GuiTu;
            var fromEmailDisplayName = TieuDeMail;
            var fromEmailPassword = MatKhau;

            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = NoiDung;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(GuiToi));
            message.Subject = TieuDe;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }
    }
}
