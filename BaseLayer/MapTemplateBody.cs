using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender.BaseLayer
{
    public static class MapTemplateBody
    {
        public static string CreateEmailBody(string sUserName, string sTitle, string sMessage)
        {
            string body = string.Empty;
            using (StreamReader oReader = new StreamReader(@"Templates/MailTemplate.html"))
            {
                body = oReader.ReadToEnd();
            }

            body = body.Replace("{UserName}", sUserName); //replacing the required things  

            body = body.Replace("{Title}", sTitle);

            body = body.Replace("{message}", sMessage);

            return body;

        }
    }
}
