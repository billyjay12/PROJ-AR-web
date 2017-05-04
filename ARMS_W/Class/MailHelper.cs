using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace ARMS_W.Class
{
    public class MailHelper
    {
        public static int SendMail(string strFrom, string strTo, string strSubject, string strBody)
        {
            MailMessage mmail = new MailMessage(strFrom, strTo, strSubject, strBody);
            SmtpClient smptsrv = new SmtpClient("192.168.10.14", 25);
            smptsrv.Credentials = new System.Net.NetworkCredential("administrator", "matimco");
                   
            try
            {
                smptsrv.Send(mmail);
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}