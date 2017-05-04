using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARMS_W.Class;
using ARMS_W.Models;
using System.Data.OleDb;
using System.IO;
using System.Data;


namespace ARMS_W.Controllers
{
    public class UserProfileController : Controller
    {
        //
        // GET: /UserProfile/Create
        

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult AddNewUser()
        {
            return View();
        }

        public ActionResult ListOfUsers()
        {
            return View();
        }

        [HttpPost]
        public string AddUser(
                   string idNo,
                   string status,
                   string lname,
                   string fname,
                   string position,
                   string emailAdd,
                   string userName,
                   string password,
                   string area,
                   string territory,
                   string region
                   )
        {

            SQLTransaction mt_trans = new SQLTransaction();
            try
            {
                mt_trans.StartTransaction();
                mt_trans.CommandText = SqlQueryHelper.InsertUserHeader(
                      idNo,
                      status,
                      lname,
                      fname,
                      position,
                      emailAdd,
                      userName,
                      Encryption.StringEncrypter(password),
                      area,
                      territory,
                      region
                   );


                mt_trans.Committransaction();
                return "00:";
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;

            }


        }

        public string CheckUser(string idNo)
        {
            string user = "";
            string msg = "";
            string strquery = "SELECT empIdNo from userHeader WHERE empIdNo='" + idNo + "'";
            OleDbDataReader greader = SqlDbHelper.getData(strquery);
            // check if ID exist and assign value to br_no
            try
            {
                if (greader.Read())
                {
                    user = greader.GetValue(0).ToString();
                }

                if (user == idNo)
                {
                    msg = "Already a user! Please choose another.";

                }
                else
                {
                    msg = "Not a user.";

                }
                return "00:" + msg;
            }
            catch (Exception ex)
            {

                return "01:" + ex.Message;
            }

        }

        public JsonResult UpdateStatus(string empidno, string status)
        {
            ARMSTestEntities db = new ARMSTestEntities();


            userHeader data = db.userHeaders.Single(p => p.empIdNo == empidno);

            data.status = status;
            try
            {
                db.SaveChanges();

                return Json(new { message = "Successfully updated." });
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.InnerException.Message });
            }
            return Json(new { message = "Error" });
        }

    }
}

