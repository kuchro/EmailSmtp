using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Sender.BaseLayer
{
    public class Emailer
    {
        private string SmtpServerAddress; 
        private int SmtpPortServer;
        private string User; 
        private string Password;

        public Emailer()
        {
            this.SmtpServerAddress= ConfigurationManager.AppSettings["smtp-server"];
            this.SmtpPortServer = int.Parse(ConfigurationManager.AppSettings["smtp-port"]);
            this.User= ConfigurationManager.AppSettings["user"]; 
            this.Password= ConfigurationManager.AppSettings["password"];
        }


        public Emailer SendEmail( List<string> AddressCollection, string Sender, string Subject,string Message)
        {
            try
            {
                MailMessage oMail = new MailMessage();
                oMail.Sender = new MailAddress(Sender);
                AddressCollection.ForEach(delegate(string Address) { oMail.To.Add(Address); });
                oMail.IsBodyHtml = true;
                oMail.Priority = MailPriority.Normal;
                oMail.BodyEncoding = Encoding.UTF8;

                oMail.Body = MapTemplateBody.CreateEmailBody("there!",Subject,Message);
                oMail.Subject = Subject;
                ConfigSmpt(oMail);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this;
        }

        private Emailer ConfigSmpt(MailMessage Mail)
        {
            try
            {
                WebMail.From = Mail.Sender.ToString();
                WebMail.SmtpServer = SmtpServerAddress;
                WebMail.SmtpPort = SmtpPortServer;
                WebMail.EnableSsl = true;
                WebMail.UserName = User;
                WebMail.Password = Password;
                WebMail.SmtpUseDefaultCredentials = false;
                WebMail.Send(Mail.To.ToString(),Mail.Subject,Mail.Body);

            }
            catch (SmtpException ex)
            {
                throw ex;
            }
            return this;
        }
    }
}
