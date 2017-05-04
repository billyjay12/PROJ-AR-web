using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARMS_W.SkelClass;
using System.Text;
using ARMS_W.Objects;
using ARMS_W.Class;
using ARMS_W.GLOBALS;


namespace ARMS_W.Controllers
{
    public class SystemPageController : Controller
    {
        //
        // GET: /SystemPage/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListOfUser()
        {
            return View();
        }

        public ActionResult ActivityLogs()
        {
            return View();
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetListOfAppUsers()
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new AjxResult();
            var sb = new StringBuilder();
            res.iserror = true;

            try
            {
                var list_of_users = (from db_user in DATABASE.userHeaders
                           select new
                           {
                               empIdNo = db_user.empIdNo,
                               userName = db_user.userName,
                               userPass = db_user.userPass,
                               emailAdd = db_user.emailAdd,
                               errorAttemp = db_user.ErrorAttemp,
                               status = db_user.status
                           }).ToList();

                foreach (var itm in list_of_users)
                {
                    sb.Append("<tr>");
                    sb.Append("<td><a href=\"" + Url.Content("~/") + "UserProfile/Profile?empIdNo=" + itm.empIdNo + "\">" + itm.empIdNo + "</a></td>");
                   // sb.Append("<td><a href=\"javascript:;\" username=\"" + itm.userName + "\" empIdNo=\"" + itm.empIdNo + "\">" + itm.userName + "</a></td>");
                    sb.Append("<td>" + itm.userName + "</td>");
                    sb.Append("<td>" + itm.emailAdd + "</td>");
                    sb.Append("<td>" + itm.status + "</td>");
                    sb.Append("<td align=\"center\"><a href=\"javascript:;\" class=\"cls_reset\" username=\"" + itm.userName + "\" userpass=\"" + itm.userPass + "\" empIdNo=\"" + itm.empIdNo + "\">Password Recovery</a></td>");
                    sb.Append("<td align=\"center\"><a href=\"javascript:;\"  class=\"cls_lock\"  username=\"" + itm.userName + "\" errAtmpt=\"" + (itm.errorAttemp.HasValue ? itm.errorAttemp : (byte)0) + "\" empIdNo=\"" + itm.empIdNo + "\">" + (itm.errorAttemp >= 3 ? "Unlock" : "Lock") + "</a></td>");
                    sb.Append("</tr>");
                }



                res.data = new
                {
                   // list = list_of_users,
                    sb_list = sb.ToString()
                };

                res.iserror = false;
            }
            catch (Exception ex)
            {
                res.message = ex.InnerException.Message;
                res.iserror = true;
            }
            finally { DATABASE.Dispose(); }

            return Json(res);
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult ResetUserPassword(string username, string password)
        {
            var sql_trans = new SQLTransaction();
            var DATABASE = new Models.ARMSTestEntities();
            AjxResult ajx_result = new AjxResult();
            int success= 0;

            var user_entity = DATABASE.userHeaders.Single(o => o.userName == username);

            user_entity.userPass = Encryption.StringEncrypter(password);

            
            
            #region SEND EMAIL

            var subject = "[ARMS] Password Changed";
            string mail_body = "Username: " + user_entity.userName + "\nTemporary Password: " + password;

            //MailHelper.SendMail("ARMS@matimco.com", user_entity.emailAdd, subject, mail_body);
               
            #endregion
                   

            try
            {
                sql_trans.StartTransaction();

               
                success = DATABASE.SaveChanges();

                sql_trans.InsertTo("SplEmails",
                                new Dictionary<string, object>() { 
                        {"sFrom","ARMS@matimco.com" }
                        ,{"sTo",user_entity.emailAdd}
                        ,{"sCC",null}
                        ,{"sSubject",subject }
                        ,{"sMessage",mail_body}
                        });

                sql_trans.Committransaction();

                ajx_result.iserror = false;
            }
            catch (Exception ex)
            {
                success = 0;
                ajx_result.iserror = true;
                sql_trans.RollbackTransaction();
            }

            return Json(ajx_result);
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public String GeneratePassword()
        {
            String generated_password = String.Empty;


            generated_password = System.Web.Security.Membership.GeneratePassword(8 /* int length */, 0/* int numberOfNonAlphanumericCharacters */);

            return generated_password;
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public String DecryptPassword(string password)
        {
            return Encryption.Decrypt(password);
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult UnlockAccount(string username, string errAtmpt)
        {
            var sql_trans = new SQLTransaction();
            var DATABASE = new Models.ARMSTestEntities();
            AjxResult ajx_result = new AjxResult();
            int success = 0;
            bool forLock = false;

            try
            {
                forLock = int.Parse(errAtmpt) < 3 ? true : false;
            }
            catch { }

            var user_entity = DATABASE.userHeaders.Single(o => o.userName == username);


            user_entity.ErrorAttemp = forLock ? (byte)3 : (byte)0;

           
           #region SEND EMAIL

            var subject = forLock ? "[ARMS] Your Account has been Locked" : "[ARMS] Your Account has been unlocked!";
            string mail_body = "Have a nice day! :)";
            
            //MailHelper.SendMail("ARMS@matimco.com", user_entity.emailAdd, subject, mail_body);

           #endregion

            try
            {
                sql_trans.StartTransaction();
                success = DATABASE.SaveChanges();


                sql_trans.InsertTo("SplEmails",
                                new Dictionary<string, object>() { 
                        {"sFrom","ARMS@matimco.com" }
                        ,{"sTo",user_entity.emailAdd}
                        ,{"sCC",null}
                        ,{"sSubject",subject }
                        ,{"sMessage",mail_body}
                        });

                sql_trans.Committransaction();

                ajx_result.iserror = false;
            }
            catch (Exception ex)
            {
                success = 0;
                ajx_result.iserror = true;
                sql_trans.RollbackTransaction();
            }

            return Json(ajx_result);
        }

        public String getListOfActivities()
        {
            var DATABASE = new Models.ARMSTestEntities();
            var sb = new StringBuilder();

            DateTime datefrom = DateTime.Now.AddDays(-5),
                     dateto = DateTime.Now.AddDays(1);


           // var qry_activitylist = DATABASE.arms2_sp_ActivityLogsReport("N/A", DateTime.Now.AddDays(-15), DateTime.Now.AddDays(1));
            var qry_activitylist = (from a in DATABASE.arms2_vw_ActivityLogs
                                   where a.datetimestamp >= datefrom && a.datetimestamp <= dateto
                                   select a).ToList();


            foreach (var itm in qry_activitylist.OrderByDescending(o=>o.datetimestamp))
            {
                var ago = itm.datetimestamp.Value.Add(DateTime.Now.TimeOfDay);

                TimeSpan timeSpan = DateTime.Now - itm.datetimestamp.Value;

                //sb.Append("<tr>");
                //sb.Append("<td>").Append(itm.empName).Append("</td>");
                //sb.Append("<td>").Append(itm.datetimestamp).Append("</td>");
                //sb.Append("<td>").Append(itm.field).Append("</td>");
                //sb.Append("<td>").Append(itm.activity).Append("</td>");
                //sb.Append("<td>").Append(itm.acctName).Append("</td>");
                //sb.Append("<td>").Append(itm.bussAdd).Append("</td>");
                //sb.Append("<td>").Append(itm.Location).Append("</td>");
                //sb.Append("<td>").Append(calculatetime(timeSpan)).Append("</td>");
                //sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td><b>").Append(itm.empName).Append("</b>");
                sb.Append("<table cellpadding=\"3\" style=\"font-size:11px; color:#6b6b6b;\">");
                sb.Append("<tr><td style=\"padding-left:10px;\"><b>").Append(itm.acctName + " - ").Append(itm.bussAdd).Append("</b>");
                sb.Append("<br /> [").Append(itm.field).Append("] - ").Append(itm.activity).Append(" @ ").Append(itm.Location).Append(" - ").Append(itm.datetimestamp);
                sb.Append("<br /><i>").Append(calculatetime(timeSpan)).Append("</i></td></tr>");
                sb.Append("</table><hr/>");
                sb.Append("</td>");
                sb.Append("</tr>");
            }
            
            DATABASE.Dispose();

            return sb.ToString();
        }

        private string calculatetime(TimeSpan timespan)
        {
            double delta = Math.Abs(timespan.TotalSeconds);

            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            if (delta < 0)
            {
                return "not yet";
            }
            if (delta < 1 * MINUTE)
            {
                return timespan.Seconds == 1 ? "one second ago" : timespan.Seconds + " seconds ago";
            }
            if (delta < 2 * MINUTE) // 45*60
            {
                return "a minute ago";
            }
            if (delta < 45 * MINUTE)
            {
                return timespan.Minutes + " minutes ago";
            }
            
            if (delta < 90 * MINUTE)
            {
                return "an hour ago";
            }
            if (delta < 24 * HOUR)
            {
                return timespan.Hours + " hours ago";
            }
            if (delta < 48 * HOUR)
            {
                return "yesterday";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)timespan.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)timespan.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }

    }
}
