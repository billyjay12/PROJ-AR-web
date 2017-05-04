using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ARMS_W.GLOBALS;
using ARMS_W.Class;


namespace ARMS_W.Controllers
{
    public class ConfigController : Controller
    {
        //
        // GET: /Config/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserRole() 
        {
            return View();
        }

        public string DeleteUserRole(string desigid) 
        {
            SQLTransaction mt_trans = new SQLTransaction();

            try
            {
                mt_trans.StartTransaction();
                mt_trans.DeleteFrom("apprvrDesig", new Dictionary<string, object>() { { "desigid", desigid } });
                mt_trans.Committransaction();

                return SActionResult.Success;
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return SActionResult.Error + ex.Message;
            }
        }

        public string AddUserRole(
            string roleid,
            string username,
            string email,
            string region,
            string brand,
            string channel,
            string area,
            string slpcode
        ) {
            string Fname = "", counterid = "";
            // get name
            DataTable userHeader = SqlDbHelper.getDataDT("select * from userheader where username='" + username + "'");
            SQLTransaction mt_trans = new SQLTransaction();

            foreach (DataRow itm in userHeader.Rows) 
            {
                Fname = itm["firstname"].ToString() + " " + itm["lastname"].ToString();
                counterid = itm["counterId"].ToString();
            }

            if(region == "LUZON")
            {
                region = "LZ";
            }
            else if (region == "VISMIN")
            {
                region = "VM";
            }
            else 
            {
                region = "";
            }
            
            try 
            {
                mt_trans.StartTransaction();

                mt_trans.CommandText = InsertNew_apprvrDesig(
                        Convert.ToInt32(roleid),
                        Fname,
                        email,
                        region,
                        Convert.ToInt32(counterid),
                        brand,
                        channel,
                        area
                    );

                // UPDATE THE USERHEADER.SLPCODE
                mt_trans.UpdateTo("userheader", new Dictionary<string, object>()
                { 
                    {"slpcode", slpcode == "" ? null : slpcode }
                }, new Dictionary<string, object>() { 
                    {"username", username}
                });

                mt_trans.Committransaction();

                return SActionResult.Success;
            }
            catch (Exception ex) 
            {
                mt_trans.RollbackTransaction();
                return SActionResult.Error + ex.Message;
            }
        }

        private string InsertNew_apprvrDesig(
            Int32 roleID,
            string name,
            string email, 
            string branch,
            Int32 counterId,
            string brand, 
            string channel, 
            string area
        ) {
            return "" +
                "INSERT INTO apprvrDesig " +
                "(roleID " +
                ",name " +
                ",email " +
                ",branch " +
                ",counterId " +
                ",brand " +
                ",channel " +
                ",area) " +
                "VALUES " +
                "(" + roleID + " " +
                ",'" + name + "' " +
                ",'" + email + "' " +
                ",'" + branch + "' " +
                "," + counterId + " " +
                ",'" + brand + "' " +
                ",'" + channel + "' " +
                ",'" + area + "') " +
                "";
        }

        public ActionResult ChangesPassword() 
        {
            return View();
        }

        public string UpdateUserPassword(string NewPassword) 
        {

            // get old password
            DataTable userHeader = SqlDbHelper.getDataDT("select * from userHeader where username = '" + Session["username"].ToString() + "'");
            foreach (DataRow itm in userHeader.Rows) 
            {
                if (itm["userPass"].ToString() == Encryption.StringEncrypter(NewPassword)) 
                {
                    return SActionResult.Error + "NEW PASSWORD CANNOT BE THE SAME \nAS THE OLD PASSWORD";
                }
            }

            SQLTransaction mtrans = new SQLTransaction();
            try 
            {
                mtrans.StartTransaction();

                mtrans.UpdateTo("userHeader", 
                new Dictionary<string, object>() { 
                    {"userPass", Encryption.StringEncrypter(NewPassword) },
                    {"LastPasswordChange", DateTime.Now.ToString("MM/dd/yyyy")}
                }, new Dictionary<string, object>() { 
                    {"userName", Session["username"].ToString() }
                });

                mtrans.Committransaction();
                return SActionResult.Success;
            }
            catch (Exception ex) 
            {
                mtrans.RollbackTransaction();
                return SActionResult.Error;
            }
        }

    }
}
