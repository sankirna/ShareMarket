using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using ShareMarket.BusinessLogic.Libs;

namespace ShareMarket.BusinessLogic.Message
{
    public partial class EmailSender
    {
        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="fromAddress">From address</param>
        /// <param name="fromName">From display name</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses ist</param>
        public static void SendEmail(string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null)
        {
            SendEmail(subject, body, new MailAddress(fromAddress, fromName), new MailAddress(toAddress, toName), bcc, cc);
        }

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="from">From address</param>
        /// <param name="to">To address</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses ist</param>
        public static void SendEmail(string subject, string body,
            MailAddress from, MailAddress to,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null)
        {
            var message = new MailMessage();
            message.From = from;
            message.To.Add(to);
            if (null != bcc)
            {
                foreach (var address in bcc.Where(bccValue => !String.IsNullOrWhiteSpace(bccValue)))
                {
                    message.Bcc.Add(address.Trim());
                }
            }
            if (null != cc)
            {
                foreach (var address in cc.Where(ccValue => !String.IsNullOrWhiteSpace(ccValue)))
                {
                    message.CC.Add(address.Trim());
                }
            }
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Host = GlobalLib.SettingValue("Email.Smtp.Host");

                int port = 0;
                int.TryParse(GlobalLib.SettingValue("Email.Smtp.Port"), out port);
                smtpClient.Port = port;

                bool enableSsl = false;
                bool.TryParse(GlobalLib.SettingValue("Email.Smtp.EnableSsl"), out enableSsl);
                smtpClient.EnableSsl = enableSsl;

                string username = GlobalLib.SettingValue("Email.Smtp.Host.UserName");
                string password = GlobalLib.SettingValue("Email.Smtp.Host.Password");
                smtpClient.Credentials = new NetworkCredential(username, password);

                smtpClient.Send(message);
            }
        }
    }
}
