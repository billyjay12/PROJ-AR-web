using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARMS_W.Class;
using ARMS_W.GLOBALS;

namespace ARMS_W.Controllers
{
    public class HomeController : Controller
    {

		public ActionResult Index()
        {
            // check if already logged in

            if (Session["username"] != null && Request.QueryString["id"] != null && Request.QueryString["doctype"] != null) 
            { 

                // redirect 
                string docid, doctype;

                if (Session["username"].ToString() != "" && Request.QueryString["id"].ToString() != "" && Request.QueryString["doctype"].ToString() != "")
                {

                    if (Request.QueryString["id"].ToString() != "" && Request.QueryString["doctype"].ToString() != "")
                    {
                        docid = Request.QueryString["id"].ToString();
                        doctype = Request.QueryString["doctype"].ToString();

                        if (doctype == "cca")
                        {
                            string ccanum = docid;
                            return RedirectToAction("AccountsDetails", "Document", new { ccanum = ccanum });
                        }

                        if (doctype == "lcd")
                        {
                            string requestid = docid;
                            return RedirectToAction("LeadAccountsDetails", "Document", new { requestid = requestid });
                        }

                        if (doctype == "ldb")
                        {
                            string requestid = docid;
                            return RedirectToAction("LeadDbDetails", "Document", new { requestid = requestid });
                        }

                        if (doctype == "mma")
                        {
                            string agreeno = docid;
                            return RedirectToAction("MMAgreementDetails", "MMAgreement", new { agreeno = agreeno });
                        }

                        if (doctype == "ematlink")
                        {
                            string eMATno = docid;
                            return RedirectToAction("eMATDetails", "eMAT", new { eMATno = eMATno });
                        }

                        if (doctype == "mkr")
                        {
                            string reqID = docid;
                            return RedirectToAction("marketingReqDetails", "MrktngRequest", new { reqID = reqID });
                        }

                        if (doctype == "brw")
                        {
                            //string busrnum = docid; //commented by billy jay delima
                            string busReviewNo = docid;
                            return RedirectToAction("BusinessReviewDetails", "BusinessReview", new { busReviewNo = busReviewNo });
                        }

                        if (doctype == "invcount")
                        {
                            string InventoryCountId = docid;
                            return RedirectToAction("InventoryCountDetails", "Inventory", new { InventoryCountId = InventoryCountId });
                        }

                        if (doctype == "newIC")
                        {
                            string acctCode = docid;
                            return RedirectToAction("CreateNewInventoryCount", "Inventory", new { acctCode = acctCode });
                        }

                        if (doctype == "calendar")
                        {
                            var current_user = new _User(Session["username"].ToString());

                            string month = Request.QueryString["Month"].ToString();
                            string year = Request.QueryString["Year"].ToString();
                            string EventId = docid;

                            return RedirectToAction("SoCalendar", "Calendar", new { month = month, year = year, EventId = EventId,soId=current_user.EmployeeIdNo });
                        }

                        if (doctype == "clndr")
                        {
                            var current_user = new _User(Session["username"].ToString());
                            string year = Request.QueryString["Year"].ToString();
                            string EventId = Request.QueryString["EventId"].ToString();
                            string month = Request.QueryString["Month"].ToString();
                            string soId = Request.QueryString["soId"].ToString();

                            return RedirectToAction("CalendarView", "Calendar", new { month = month, year = year, EventId = EventId, soId = soId });
                        
                        }

                    }
                }
            }

            return View("Login");
        }

		public ActionResult Login()
        {
            return View();
        }

		public ActionResult Test () 
		{
			return View();
		}
        
        public ActionResult Error() 
        {
            return View();
        }

        public ActionResult Logout() 
        {
            Session.Abandon();
            return View("Login");
        }

        public string mError() {
            return SActionResult.Error + "Session Expired!";
        }

        public ActionResult CallHome() 
        {
            return View();
        }

    }
}
