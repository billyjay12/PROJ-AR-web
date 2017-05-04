using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARMS_W.Class;
using System.Data.OleDb;
using System.IO;
using System.Data;

namespace ARMS_W.Controllers
{
    public class UseAcessController : Controller
    {
        //
        // GET: /UseAcess/

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public string USERacess(
            Int64 roleID,
            string accessRights,
            string docType
           
            
           )
        {
            SQLTransaction mt_trans = new SQLTransaction();
            string newdocType = "";         
            
           

            try
            {

                mt_trans.StartTransaction();


                if (docType == "Account Creation (Project/TW)") { newdocType = "1"; }

                else if (docType == "Account Creation (Lead)") { newdocType = "2"; }
                else if (docType == "Account Creation (One-Time Customer)") { newdocType = "3"; }
                else if (docType == "Account Creation (Regular Customer)") { newdocType = "4"; }
                else if (docType == "Business Review") { newdocType = "5"; }
                else if (docType == "Marketing Program") { newdocType = "6"; }
                else if (docType == "Marketing Request") { newdocType = "7"; }
                else if (docType == "Meeting Minutes & Agreements") { newdocType = "8"; }
                else if (docType == "eMAT") { newdocType = "9"; }



                mt_trans.CommandText = SqlQueryHelper.InsertUsserAccess(

                             roleID,
                             Convert.ToInt64(newdocType),
                             accessRights

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
    }
}
