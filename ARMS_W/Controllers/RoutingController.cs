using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using ARMS_W.Class;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.UI.WebControls;
using System.Data;

namespace ARMS_W.Controllers
{
    public class routingController : Controller
    {
        //
        // GET: /routing/       

        int newDoctype;
        public ActionResult routingIndex()
        {
            return View();
        }

        public ActionResult Routing()
        {
          return View();
        }

        public ActionResult maintenance()
        {
            return View();
        }

        private string getCurrentState(int docType, string docNum)
        {
            newDoctype = 0;
            string _curState = "0";
            OleDbDataReader _creader;
            try
            {
                switch (docType)
                {
                    case 1: //account creation
                        string stateQuery = "SELECT status FROM customerHeader WHERE ccaNum=" + docNum;;
                        _creader = SqlDbHelper.getData(stateQuery);
                        
                        {
                          if (_creader.Read())
                              _curState = _creader.GetValue(0).ToString();                         
                         else                          
                             throw new FormatException("No Document State retrieve");                           
                        }
                        break;  
                      
                    case 2: //business review
                        string stateQuery1 = "SELECT status FROM busReview WHERE busReviewNo=" + docNum; 
                       _creader = SqlDbHelper.getData(stateQuery1);

                        {
                          if (_creader.Read())
                              _curState = _creader.GetValue(0).ToString();                             
                          else                    
                             throw new FormatException("No Document State retrieve");                    
                        }
                       break;

                    case 3: //marketing program
                       string stateQuery2 = "SELECT status FROM mrktProgram WHERE programNo=" + docNum;
                       _creader = SqlDbHelper.getData(stateQuery2);

                       {
                        if (_creader.Read())
                            _curState = _creader.GetValue(0).ToString();
                        else
                           throw new FormatException("No Document State retrieve");         
                       }
                       break;

                    case 4: //marketing request
                        string stateQuery3 = "SELECT status FROM marketRequest WHERE reqid=" + docNum;
                       _creader = SqlDbHelper.getData(stateQuery3);

                       {
                           if (_creader.Read())
                               _curState = _creader.GetValue(0).ToString();
                           else
                               throw new FormatException("No Document State retrieve");
                       }
                       break;

                    case 5: //meeting minutes and agreement
                        string stateQuery4 = "SELECT status FROM mtgMinutesAgreement WHERE agreeNo=" + docNum;
                       _creader = SqlDbHelper.getData(stateQuery4);

                       {
                           if (_creader.Read())
                               _curState = _creader.GetValue(0).ToString();
                           else
                               throw new FormatException("No Document State retrieve");
                       }
                       break;

                    default:// E-MAT
                        string stateQuery5 = "SELECT status FROM eMAT WHERE eMATno=" + docNum;
                       _creader = SqlDbHelper.getData(stateQuery5);

                       {
                           if (_creader.Read())
                               _curState = _creader.GetValue(0).ToString();
                           else
                               throw new FormatException("No Document State retrieve");
                       }
                       break;                       
                }

                _creader.Close();
                return _curState;
                
            }

            catch (Exception ex)
            {
                throw (ex);
            }         
          }

       
        // GET EMAIL OF THE NEXT APPROVER
        public string getNxtApprvrEmail(string roleID, string branch, string area, string channel)  
        {
                string strQuery = "EXEC mtc_recipientEmailAdd '" + roleID + "','" + branch + "','" + area + "','" + channel + "'";
                string recipientEmail = "";

                try
                {
                    OleDbDataReader tmpreader;
                    tmpreader = SqlDbHelper.getData(strQuery);
                    if (tmpreader.Read())
                    {
                        recipientEmail = tmpreader.GetValue(0).ToString();
                    }
                }

                catch (Exception ex)
                {
                    recipientEmail = ex.Message;
                }

                finally
                { }

                return recipientEmail;
            }


        // GET EMAIL OF THE NEXT APPROVER for marketing request
        public string getNxtApprvrEmails(string roleID, string branch, string area, string channel, string brand, int docType = 0)
        {
            Int64 rolenumber = Convert.ToInt64(roleID);
            //  string docnumber="7";

             string recipientEmail = "";
           // DataTable recipientEmail = null;
            string strQuery = "";
            strQuery = "EXEC mtc_recipientEmailAdd '" + roleID + "','" + branch + "','" + area + "','" + channel + "'";
              

                try
                {
                    OleDbDataReader tmpreader;
                    tmpreader = SqlDbHelper.getData(strQuery);
                    if (tmpreader.Read())
                    {
                        recipientEmail = tmpreader.GetValue(0).ToString();
                    }
                }

                catch (Exception ex)
                {
                    recipientEmail = ex.Message;
                }

                finally
                { }
            
            
           // }

                return recipientEmail;
            }
        

        //
        public DataTable getBrandManagerEmail(string brand, string roleID)
        {

            DataTable BrandManageremail = null;
            string strQuery = "";

            if (brand == "ALL BRANDS")
            {

                strQuery = "SELECT email FROM apprvrDesig where roleID='9' and brand in('GUDWOOD','MATWOOD','WEATHERWOOD','PCW','TRUSSWOOD')";
                 BrandManageremail = SqlDbHelper.getDataDT(strQuery);
                
            }

            else if (brand == "MATWOOD"){
                strQuery = "SELECT email FROM apprvrDesig where roleID='9' and brand='MATWOOD'";
                BrandManageremail = SqlDbHelper.getDataDT(strQuery);
            
            
            }

            else if(brand=="GUDWOOD"){
                strQuery = "SELECT email FROM apprvrDesig where roleID='9' and brand='GUDWOOD'";
                BrandManageremail = SqlDbHelper.getDataDT(strQuery);

            
            }

            else if (brand == "WEATHERWOOD")
            {
                strQuery = "SELECT email FROM apprvrDesig where roleID='9' and brand='WEATHERWOOD'";
                BrandManageremail = SqlDbHelper.getDataDT(strQuery);


            }

            else if (brand == "PCW")
            {
                strQuery = "SELECT email FROM apprvrDesig where roleID='9' and brand='PCW'";
                BrandManageremail = SqlDbHelper.getDataDT(strQuery);


            }

            else if (brand == "TRUSSWOOD")
            {
                strQuery = "SELECT email FROM apprvrDesig where roleID='9' and brand='TRUSSWOOD'";
                BrandManageremail = SqlDbHelper.getDataDT(strQuery);


            }
            return BrandManageremail;
        }
        

        // GET EMAIL OF THE PREVIOUS APPOVERS
        public DataTable getRecipientEmail(string branch, string area, string channel, string status)
        {
             
            DataTable emailList = null; 
                      
                string strQuery = "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                   "and b.rolecode in ('csr') and  left(a.branch,1) = '" + channel.Substring(0, 1) + "' union " +
                                   "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                   "and b.rolecode in ('ca','chm') and a.channel = '" + channel + "' union "+
                                   "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                   "and b.rolecode = 'asm' and a.area = '" + area + "' union " +
                                   "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                   "and b.rolecode in ('vpbsm', 'ssm','ssgm')";
                
            string group = " group by email";

                if (status == "0")
                {
                    strQuery = "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                   "and b.rolecode in ('csr') and  left(a.branch,1) = '" + channel.Substring(0, 1) + "' union " +
                                   "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                   "and b.rolecode in ('ca','chm') and a.channel = '" + channel + "' union " +
                                   "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                   "and b.rolecode = 'asm' and a.area = '" + area + "' union " +
                                   "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                   "and b.rolecode in ('vpbsm')" + group;
                    emailList = SqlDbHelper.getDataDT(strQuery);
                }

                if (status == "1" || status == "2" || status == "3" || status == "4")
                {
                    strQuery = strQuery + group;
                    emailList = SqlDbHelper.getDataDT(strQuery);
                }

                if (status == "5" || status == "6")
                {

                    strQuery = strQuery + " union select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                "and b.rolecode = 'fnm' and left(a.branch,1) = '" + channel.Substring(0, 1) + "'" + group;
                    emailList = SqlDbHelper.getDataDT(strQuery);
                }

                if (status == "7" || status == "8")
                {

                    strQuery = "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                               "and b.rolecode in ('csr', 'fnm') and  left(a.branch,1) = '" + channel.Substring(0, 1) + "' union " +
                               "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                   "and b.rolecode in ('ca','chm') and a.channel = '" + channel + "' union " +
                               "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                               "and b.rolecode = 'asm' and  a.area = '" + area + "' union select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                               "and b.rolecode = 'vptfi' union select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                               "and b.rolecode in('ssm','ssgm')" + group;

                    emailList = SqlDbHelper.getDataDT(strQuery);

                }
                if (status == "9" || status == "10")
                {

                    strQuery = strQuery + " union select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                "and b.rolecode = 'vptfi' union select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                "and b.rolecode in ('fnm') and  left(a.branch,1) = '" + channel.Substring(0, 1) + "' union "+
                                "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                "and b.rolecode in ('ca','chm') and a.channel = '" + channel + "'"+ group;

                    emailList = SqlDbHelper.getDataDT(strQuery);

                }
                if (status == "11")
                {

                    strQuery = strQuery + " union select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                "and b.rolecode in ('vptfi', ceo) union select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                "and b.rolecode in ('fnm') and left(a.branch,1) = '" + channel.Substring(0, 1) + "' union " +
                                "select a.email from apprvrDesig a, apprvrRole b, userHeader c where a.roleid = b.roleid and c.counterid = a.counterid " +
                                "and b.rolecode in ('ca','chm') and a.channel = '" + channel + "'" + group;

                    emailList = SqlDbHelper.getDataDT(strQuery);

                }
          
           
         
          return emailList;

        }


        public DataTable getPrevapprvrEmail(string branch, string area, string channel, string status, string brand)
        {
            DataTable prevemailList = null;
            string strQuery = "";

            if (status == "2") {
                strQuery = "SELECT email FROM apprvrDesig where roleID='53' and channel='"+channel+"'";
                prevemailList = SqlDbHelper.getDataDT(strQuery);
            }

            else if(status=="4"){

                strQuery = "SELECT email FROM apprvrDesig where roleID='53' and channel='"+channel+"' union  SELECT email FROM apprvrDesig where roleID='2' and area='"+area+"'";
                prevemailList = SqlDbHelper.getDataDT(strQuery);
            
            }
            else if(status=="6"){

                strQuery = "SELECT email FROM apprvrDesig where roleID='53' and channel='" + channel + "' union  SELECT email FROM apprvrDesig where roleID='2' and area='" + area + "' union SELECT email from apprvrDesig where roleID='53' and channel='" + channel + "'";
                prevemailList = SqlDbHelper.getDataDT(strQuery);
            
            }

            else if (status == "8") {

                strQuery = "SELECT email FROM apprvrDesig where roleID='53' and channel='" + channel + "' union  SELECT email FROM apprvrDesig where roleID='2' and area='" + area + "' union SELECT email from apprvrDesig where roleID='53' and channel='" + channel + "' union SELECT email from apprvrDesig where roleID='5'";
                prevemailList = SqlDbHelper.getDataDT(strQuery);
            
            }

            else if (status == "10" && brand == "ALL BRANDS")
            {

                strQuery = "SELECT email FROM apprvrDesig where roleID='53' and channel='" + channel + "' union  SELECT email FROM apprvrDesig where roleID='2' and area='" + area + "' union SELECT email from apprvrDesig where roleID='53' and channel='" + channel + "' union SELECT email from apprvrDesig where roleID='5' union SELECT email from apprvrDesig where roleID='9' and brand in('GUDWOOD','MATWOOD','WEATHERWOOD','PCW','TRUSSWOOD')";
                prevemailList = SqlDbHelper.getDataDT(strQuery);

            }

            else if (status == "10" && brand != "ALL BRANDS")
            {
                strQuery = "SELECT email FROM apprvrDesig where roleID='53' and channel='" + channel + "' union  SELECT email FROM apprvrDesig where roleID='2' and area='" + area + "' union SELECT email from apprvrDesig where roleID='53' and channel='" + channel + "' union SELECT email from apprvrDesig where roleID='5' union SELECT email from apprvrDesig where roleID='9' and brand='"+brand+"'";
                prevemailList = SqlDbHelper.getDataDT(strQuery);

            }



            return prevemailList;
        }



        public string getArea(string acctCode) {
            SQLTransaction mt_trans = new SQLTransaction();
            string strquery = "SELECT area FROM customerHeader WHERE acctCode=(" + acctCode + ")";
            string area = "";
            OleDbDataReader greader = SqlDbHelper.getData(strquery);
            try
            {
                if (greader.Read())
                {
                    area = greader.GetValue(0).ToString();
                }
            }
            catch (Exception ex) {
                area = ex.Message;
            }
            
            return area;
        
        }

        public string getChannel(string acctCode)
        {
           
            // string strquery = "SELECT channel FROM mtc_vw_Channel WHERE CardCode='" + acctCode + "'";
            string strquery = "" +
                "select " +
                "(select (select descript from SAPSERVER.MATIMCO.dbo.oter b where b.territryid=a.parent) from SAPSERVER.MATIMCO.dbo.oter a where a.descript=b.area collate SQL_Latin1_General_CP850_CI_AS) as 'channel' " +
                "from customerHeader b where acctcode=(" + acctCode + ")" +
                "";
            string channel = "";
            OleDbDataReader greader = SqlDbHelper.getData(strquery);
            try
            {
                if (greader.Read())
                {
                    channel = greader.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                channel = ex.Message;
            }

            return channel;

        }

        public string getBrand(string documentNo)
        {
            SQLTransaction mt_trans = new SQLTransaction();
            string strquery = "SELECT brand FROM marktingRequest WHERE reqID=('" + documentNo + "')";
            string brand = "";
            OleDbDataReader greader = SqlDbHelper.getData(strquery);
            try
            {
                if (greader.Read())
                {
                    brand = greader.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                brand = ex.Message;
            }

            return brand;

        }
        
        /** For Business Review Email notification
        * 
        * **/
       
#region BusinessReviewEmailNotification
        // SEND NOTIFICATION TO PREVIOUS APPROVERS
        public void sendEmailToRecepients(string docLink, string title, DataTable recipient,string mailServer)
        {
            string mail_body = "For details, please click this link:  " + docLink;
            
            try
            {
                foreach (DataRow item in recipient.Rows)
                {
                    MailHelper.SendMail("ARMS@matimco.com", item["email"].ToString(), title, mail_body);
                }
            }
            catch (Exception ex)
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add("mayshelvillaruel@matimco.com");
                message.Subject = "Error in Email Sending from Routing Controller";
                message.From = new System.Net.Mail.MailAddress("ARMS@matimco.com");
                message.Body = "Error message: Error in sending Business Review e-mail notification. " + " " + ex.Message + ". " + recipient;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                smtp.Send(message); 
            }

        }

        // SEND NOTIFICATION TO NEXT APPROVER
        public void sendEmailToNxtApprvr(string docLink, string documentNo, string recipient, string mailServer)
        {

            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(recipient);
                message.Subject = " Business Review doc. no. " + documentNo + " is waiting for your approval";
                message.From = new System.Net.Mail.MailAddress("ARMS@matimco.com");
                message.Body = "For your approval, please click this link -->  " + docLink;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                smtp.Send(message);

            }
            catch (Exception ex)
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add("mayshelvillaruel@matimco.com");
                message.Subject = "Error in Email Sending from Routing Controller.";
                message.From = new System.Net.Mail.MailAddress("ARMS@matimco.com");
                message.Body = "Error message: Error in sending Business Review e-mail notification. " + " " + ex.Message + ". " + recipient;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                smtp.Send(message);
            }

        }

#endregion BusinessReviewEmailNotification

        // FOR E-MAT
        public void sendEmailToNxtEMATApprvr(string docLink, string documentNo, string recipient, string mailServer)
        {

            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(recipient);
                message.Subject = " E-MAT doc. no. " + documentNo + " is waiting for your approval";
                message.From = new System.Net.Mail.MailAddress("ARMS@matimco.com");
                message.Body = "For your approval, please click this link:  " + docLink;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                smtp.Send(message);

            }
            catch (Exception ex)
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add("billyjaydelima@matimco.com,hervieinoc@matimco.com");
                message.Subject = "Error in Email Sending from Routing Controller.";
                message.From = new System.Net.Mail.MailAddress("ARMS@matimco.com");
                message.Body = "Error message: " + documentNo + " " + ex.Message + ". " + recipient;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                smtp.Send(message);
            }

        }

        // FOR EMAT
        public void sendEmail(string docLink, string title, string recipient, string mailServer)
        {

            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(recipient);
                message.Subject = title;
                message.From = new System.Net.Mail.MailAddress("arms@matimco.com");
                message.Body = "For details on the approved document, you may click on this link: " + docLink;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                smtp.Send(message);

            }
            catch (Exception ex)
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add("billyjaydelima@matimco.com,hervieinoc@matimco.com");
                message.Subject = "Error in Email Sending from Routing Controller.";
                message.From = new System.Net.Mail.MailAddress("ARMS@matimco.com");
                message.Body = "Error message: "+ title +" " + ex.Message + ". " + recipient;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                smtp.Send(message);
            }

        }

        // FOR MARKETING REQUEST
        // SEND NOTIFICATION TO PREVIOUS APPROVERS
        public void sendEmailTopriorApp(string docLink, string title, DataTable recipient, string mailServer)
        {
            string mail_body = "To view the details of the disapproved request, please click on this link -->  " + docLink;
            try
            {

                foreach (DataRow item in recipient.Rows)
                {
                    MailHelper.SendMail("ARMS@matimco.com", item["email"].ToString(), title, mail_body);
                }
            }
            catch (Exception ex)
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add("billyjaydelima@matimco.com,hervieinoc@matimco.com");
                message.Subject = "Error in Email Sending from Routing Controller";
                message.From = new System.Net.Mail.MailAddress("arms@matimco.com");
                message.Body = "Error message: " + title + " " + ex.Message + ". " + recipient;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                smtp.Send(message);
            }

        }

        // ROUTING FOR E-MAT & BUSINESS REVIEW
        public void routeNext(int docType,string documentNo,string docLink, string branch,bool isApprove, string acctCode)
         
        {
            //isApprove to determine the tree path if it is Approve Path or Denied Path  {1 - approve 0 - otherwise}
            /*  docTypes:
             *   (1) - Account Creation (Project/TW)       (2) - Account Creation (Lead)       (3) - Account Creation (One-Time Customer)  
             *   (4) - Account Creation (Regular Customer) (5) - Business Review               (6) - Marketing Program 
             *   (7) - Marketing Request                   (8) - Meeting Minutes & Agreements  (9) - eMAT          
             */


            OleDbDataReader _creader;
            string _roleId="0";
            string _branch = "VM"; //no need to determine branch here,it is handled by Stored Procedure                                    
            string _strQuery = "EXEC mtc_nextRoute '"+ documentNo +"','"+ docType + "'," + isApprove;
            string _emailTitle = "";                                                         
            string channel = getChannel(acctCode);
            string area = getArea(acctCode);
            string brand = getBrand(documentNo);
           
            // FOR E-MAT
            if (docType == 9)
            {
                try
                {
                    _creader = SqlDbHelper.getData(_strQuery);

                    if (_creader.Read())
                    {
                        _roleId = _creader.GetValue(0).ToString();
                        _branch = _creader.GetValue(1).ToString();
                    }
                }
                catch (Exception)
                {
                    
                }
                finally 
                {
                    _emailTitle = "E-MAT doc. no. " + documentNo + " is waiting for your approval";
                    sendEmail(docLink, _emailTitle, getNxtApprvrEmail(_roleId, _branch, area, channel), "mail2.matimco.com ");              
                }

            }
            else if (docType == 5) // FOR BUSINESS REVIEW
            {

                string br_status = getDocStatus(documentNo);
                string _strModule = "Business Review";
               

                try
                {
                    _creader = SqlDbHelper.getData(_strQuery);

                    if (_creader.Read())
                    {
                        _roleId = _creader.GetValue(0).ToString();
                        _branch = _creader.GetValue(1).ToString();
                    }

                    if (br_status !=null)
                    {
                        DataTable recepient_email = getRecipientEmail(_branch, area, channel, br_status);
                        if (isApprove == true && br_status!="10")
                        {
                            int status = Convert.ToInt32(br_status) + 2;
                            string newStatus = status.ToString();
                            _emailTitle = _strModule + " doc. no. " + documentNo + " was approved and is waiting: " + AppHelper.BusReviewDocStateMsg(newStatus) + "";
                            sendEmailToRecepients(docLink, _emailTitle, recepient_email, "mail2.matimco.com ");
                            sendEmailToNxtApprvr(docLink, documentNo, getNxtApprvrEmail(_roleId, branch, area, channel), "mail2.matimco.com ");
                        }


                         else if (isApprove == false && br_status == "0")
                        {
                            int status = Convert.ToInt32(br_status) + 1;
                            string newStatus = status.ToString();
                            _emailTitle = _strModule + " doc. no. " + documentNo + " was approved and is waiting: " + AppHelper.BusReviewDocStateMsg(newStatus) + "";
                            sendEmailToRecepients(docLink, _emailTitle, recepient_email, "mail2.matimco.com ");
                            sendEmailToNxtApprvr(docLink, documentNo, getNxtApprvrEmail(_roleId, branch, area, channel), "mail2.matimco.com ");
                        }
                        else if (isApprove == false && br_status == "7")
                        {
                            int status = Convert.ToInt32(br_status);
                            status = status + 1;
                            string newStatus = status.ToString();
                            _emailTitle = _strModule + " doc. no. " + documentNo + " was disapproved and is waiting: " + AppHelper.BusReviewDocStateMsg(newStatus) + "";
                            sendEmailToRecepients(docLink, _emailTitle, recepient_email, "mail2.matimco.com ");
                            sendEmailToNxtApprvr(docLink, documentNo, getNxtApprvrEmail(_roleId, branch, area, channel), "mail2.matimco.com ");
                        }
                        else
                        {
                            _emailTitle = _strModule + " doc. no. " + documentNo + " was closed.";
                            sendEmailToRecepients(docLink, _emailTitle, recepient_email, "mail2.matimco.com ");
                        }
                    
                   }

                }
                catch (Exception)
                {
                    
                }
            }
            else if (docType == 7)// MARKETING REQUEST 
            {
                string mkt_status = getDocStatusMKT(documentNo);
              //  string _strModule = "Marketing Request";

                try
                {
                    _creader = SqlDbHelper.getData(_strQuery);

                    if (_creader.Read())
                    {
                        _roleId = _creader.GetValue(0).ToString();
                        _branch = _creader.GetValue(1).ToString();
                    }



                    if (isApprove == false)
                    {   
                        int status = Convert.ToInt32(mkt_status) + 1;
                        string newStatus = status.ToString();
                        DataTable EmailAdd = getPrevapprvrEmail(branch, area, channel, newStatus, brand);
                        _emailTitle = "Marketing Request no. " + documentNo + " was disapproved by " + AppHelper.MKTDocStateMsg(newStatus) + "";
                        sendEmailTopriorApp(docLink, _emailTitle, EmailAdd, "mail2.matimco.com ");

                    }

                    else
                    {

                        if (_roleId == "9")
                        {

                            DataTable recepient_email = getBrandManagerEmail(brand, _roleId);
                            _emailTitle = "Marketing Request no. " + documentNo + " is waiting for your approval.";
                            sendEmailToRecepients(docLink, _emailTitle, recepient_email, "mail2.matimco.com ");

                        }

                        else
                        {

                            _emailTitle = "Marketing Request no. " + documentNo + " is waiting for your approval.";
                            sendEmail(docLink, _emailTitle, getNxtApprvrEmails(_roleId, _branch, area, channel, brand, docType), "mail2.matimco.com ");
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
            else if (docType == 6)// MARKETING PROGRAM 
            {
                try
                {
                    _creader = SqlDbHelper.getData(_strQuery);

                    if (_creader.Read())
                    {
                        _roleId = _creader.GetValue(0).ToString();
                        _branch = _creader.GetValue(1).ToString();
                    }

                    _emailTitle = "Marketing Program no: '" + documentNo + "' is waiting for your approval.";
                    sendEmail(docLink, _emailTitle, getNxtApprvrEmail(_roleId, _branch, area, channel), "mail2.matimco.com ");
                }
                catch (Exception)
                {

                }
            }
           
          
        }

        public string getDocStatus(string docnum)
        {
            //routingController route = new routingController();
            string status = "";
            string strquery = "SELECT status from busReview WHERE busReviewNo = '" + docnum + "'";
            OleDbDataReader _creader;

            try
            {
                _creader = SqlDbHelper.getData(strquery);

                if (_creader.Read())
                {
                    status = _creader.GetValue(0).ToString();
                  
                }
            }
            catch (Exception ex)
            {
                return "01" + ex.Message;
            }
            return status;
        }


        public string getDocStatusMKT(string docnum)
        {
            //routingController route = new routingController();
            string status = "";
            string strquery = "SELECT status from marktingRequest WHERE reqID = '" + docnum + "'";
            OleDbDataReader _creader;

            try
            {
                _creader = SqlDbHelper.getData(strquery);

                if (_creader.Read())
                {
                    status = _creader.GetValue(0).ToString();

                }
            }
            catch (Exception ex)
            {
                return "01" + ex.Message;
            }
            return status;
        }


        [HttpPost]
        public string addRoleName(string roleName)
        {
            SQLTransaction mt_trans = new SQLTransaction();

            try
            {
                mt_trans.StartTransaction();
                mt_trans.CommandText = SqlQueryHelper.InsertRoleName(roleName);
                mt_trans.Committransaction();

                return "00:";
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;             
            }
        
        }

        [HttpPost]
        public string addApprvrDesig(string roleID,string branch,string channel, string area,string name,string email,string brand,string empID)
        {

            SQLTransaction mt_trans = new SQLTransaction();

            try
            {
                mt_trans.StartTransaction();
                mt_trans.CommandText = SqlQueryHelper.InsertApprvrDesig(roleID, branch, channel, area, name, email, brand, empID);
                mt_trans.Committransaction();

                return "00:";
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;
            }                              

        }

    }
    
}
