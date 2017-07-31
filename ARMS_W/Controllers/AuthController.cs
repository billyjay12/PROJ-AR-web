using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ARMS_W.Class;
using ARMS_W.GLOBALS;


namespace ARMS_W.Controllers
{
    public class AuthController : Controller
    {
        //
        // GET: /Auth/
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult IsValidUser (string usrname, string psswrd) 
		{
            SQLTransaction mt_trans = new SQLTransaction();

            // RETRIEIVE THE NUMBER OF ERRORS
            if (IsLocked(usrname) == true)
            {
                return Json("Lock");
            }
            Session.Timeout = 120;
            System.Data.DataTable mtable = SqlDbHelper.getDataDT("select * from userHeader where status = 'ACTIVE' and userName='" + usrname + "' and userPass='" + Encryption.StringEncrypter(psswrd) + "'");
            if (mtable.Rows.Count > 0)
            {
                foreach (System.Data.DataRow Item in mtable.Rows)
                { 
                    Session["username"] = usrname;
                    Session["userposition"] = Item["position"].ToString();
                    Session["userregion"] = Item["region"].ToString();
                    Session["userarea"] = Item["area"].ToString();
                    Session["userid"] = Item["empIdNo"].ToString();
                    Session["userchannel"] = Item["channel"].ToString();
                    Session["InputedUname"] = Item["firstName"].ToString() +" "+ Item["lastName"].ToString();

                }
                /*
                 * CHEAT CODES BELOW/WORKAROUND
                */
                 Session["userposition2"] = GetUserPosition(Session["userposition"].ToString());
                // UPDATE userHeader.ErrorAttemp
                 try 
                 {
                     mt_trans.StartTransaction();

                     mt_trans.UpdateTo("userheader", 
                        new Dictionary<string, object>() { 
                            {"errorattemp", 0}
                        }, new Dictionary<string, object>() { 
                            {"username", usrname}
                     });

                     mt_trans.Committransaction();
                 }
                 catch (Exception ex) 
                 {
                     mt_trans.RollbackTransaction();
                 }

                return Json("True");
            }
            else 
            {

                try
                {
                    mt_trans.StartTransaction();

                    mt_trans.CommandText = "update userheader set errorattemp=isnull(errorattemp,0)+1 where username = '" + usrname + "'";

                    mt_trans.Committransaction();
                }
                catch (Exception ex)
                {
                    mt_trans.RollbackTransaction();
                }

                // RETRIEIVE THE NUMBER OF ERRORS
                if (IsLocked(usrname) == true) 
                {
                    return Json("Lock");
                }

                return Json("False");
            }
		}

        public static string GetUserPosition(string db_usr_position) 
        {

            if (db_usr_position == "CSR")
            {
                return "csr";
            }
            else if (db_usr_position == "Finance Mgr.")
            {
               return "fnm";
            }
            else if (db_usr_position == "VP - TFI")
            {
                return "vptfi";
            }
            else if (db_usr_position == "Sales Director")
            {
               return "vpbsm";
            }
            else
            {
                return db_usr_position;
            }
        }

        private bool IsLocked(string usrname) 
        {
            // RETRIEIVE THE NUMBER OF ERRORS
            try
            {
                int ErrAttmp = 0;

                DataTable ErrorAttemp_tbl = SqlDbHelper.getDataDT("select isnull(errorattemp,0) as 'errorattemp' from userheader where username = '" + usrname + "'");
                foreach (DataRow itm in ErrorAttemp_tbl.Rows)
                {
                    ErrAttmp = Convert.ToInt16(itm["errorattemp"].ToString());
                }

                if (ErrAttmp > 4)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {

            }

            return false;
        }

        [HttpPost]
        public string NumDaysBeforeExp() 
        {
            string username = Session["username"].ToString();
            DataTable userHeader = null;

            try 
            {
                userHeader = SqlDbHelper.getDataDT("select datediff(d, LastPasswordChange, getdate()) as 'num_of_days' from userheader where username = '" + username + "'");

                foreach (DataRow itm in userHeader.Rows) 
                {
                    if (Convert.ToInt32(itm["num_of_days"].ToString()) >= (365 - 7)) 
                    {
                        // string num_days = (Convert.ToInt32(itm["num_of_days"].ToString()) - 90).ToString();
                        int num_days_left = 365 - Convert.ToInt32(itm["num_of_days"].ToString());
                        string msg = "";

                        if (num_days_left >= 0)
                        {
                            msg = "Your password will expire in " + num_days_left.ToString() + " day/s.";
                        }
                        else 
                        {
                            msg = "Your password already expired.";
                        }

                        return SActionResult.Success + msg;
                    }
                }

                return SActionResult.Error;
            }
            catch (Exception ex) 
            {
                return SActionResult.Error;
            }
        }

        [HttpPost]
        public string sendMailToUnlockAccount(string usrname, string psswrd, string dtls) 
        {
            var sql_trans = new SQLTransaction();
            DataTable userHeader = null;
            userHeader = SqlDbHelper.getDataDT("select * from userHeader where status = 'ACTIVE' and userName='" + usrname + "' and userPass='" + Encryption.StringEncrypter(psswrd) + "'");
            if (userHeader.Rows.Count <= 0) return "False";


            if (IsLocked(usrname))
            {
                try
                {

                    sql_trans.StartTransaction();

                    DataTable apprvrDesig = null;
                    apprvrDesig = SqlDbHelper.getDataDT("select distinct(email) from dbo.apprvrDesig where roleid=14");

                    var subject = "[ARMS] Request Unlock Account";
                    string mail_body = "USERNAME: " + usrname + "\nDetails: " + dtls + "\nClick here to unlock this account ---> " + AppHelper.Arms_Url + "?id=1&doctype=listusers";

                    foreach (DataRow itm in apprvrDesig.Rows)
                    {
                        //  MailHelper.SendMail("ARMS@matimco.com", itm["email"].ToString(), subject, mail_body);
                        sql_trans.InsertTo("SplEmails",
                                    new Dictionary<string, object>() { 
                        {"sFrom","ARMS@matimco.com" }
                        ,{"sTo",itm["email"].ToString()}
                        ,{"sCC",null}
                        ,{"sSubject",subject }
                        ,{"sMessage",mail_body}
                        });
                    }

                    sql_trans.Committransaction();
                }
                catch (Exception ex)
                {
                    sql_trans.RollbackTransaction();
                    return ex.InnerException.Message;
                }

                return "Lock";
            }
            else
                return "Unlock";

        }

        public String getWebUserpass(string username)
        {
            DataTable userHeader = null;
            userHeader = SqlDbHelper.getDataDT("select userPass from userHeader where status = 'ACTIVE' and userName='" + username + "'");
            if (userHeader.Rows.Count <= 0) return "False";

            try
            {
                foreach (DataRow itm in userHeader.Rows)
                {
                    return Encryption.Decrypt(itm["userPass"].ToString());
                }

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    }
}
