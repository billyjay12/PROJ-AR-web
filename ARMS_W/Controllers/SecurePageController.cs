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
    public class SecurePageController : Controller
    {
        //
        // GET: /SecurePage/
        [AuthorizeUsr]
        public ActionResult Index()
        {
            string docid, doctype;

            // check if current user is FSP
            _User Ousr = new _User( Session["username"].ToString() );

            // cached the user's info
            Session["Ousr"] = Ousr;

            // use the session,... cast to _User
            _User _Ours = (_User)Session["Ousr"];

            Session["isFSP"] = "FALSE";
            if (Ousr.HasPositionsOf(new string[] { "SO", "ASM" }))
            {
                Session["isFSP"] = "TRUE";
            }

            // check if required to change a password
            if(Request.QueryString["id"] != null && Request.QueryString["doctype"] != null )
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

                    if (doctype == "lcd" || doctype == "ldi")
                    {
                        string requestid = docid;
                        return RedirectToAction("LeadAccountsDetails", "Document", new { requestid = requestid });
                    }

                    if (doctype == "ldb")
                    {
                        string requestid = docid;
                        return RedirectToAction("LeadDbDetails", "LeadDb", new { requestid = requestid });
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
                        //string busrnum = docid; --commented by billy jay delima
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

                    if (doctype == "clndr")
                    {
                        string EventId = docid;
                        return RedirectToAction("CalendarView", "Calendar", new { EventId = EventId });
                    
                    }

                    if (doctype == "listusers")
                    {
                        string EventId = docid;
                        return RedirectToAction("ListOfUser", "SystemPage");

                    }

                }
            }
            
            return View();
        }

        [AuthorizeUsr]
        [HttpPost]
        public string IsActive(string username) 
        {
            DataTable mtable = SqlDbHelper.getDataDT("select loginattemp, LastPasswordChange from userheader where username='" + username + "' --and (loginattemp is null or loginattemp < 1)");
            foreach (DataRow itm in mtable.Rows) 
            {
                if (itm["loginattemp"].ToString() != "" && itm["loginattemp"].ToString() != "0")
                {
                    // loginattemp = 1
                    if (DateTime.Now.Subtract(Convert.ToDateTime(itm["LastPasswordChange"].ToString())).Days > 89)
                    {
                        // UpdateLoginAttemp(username);
                    }
                    else 
                    {
                        return "false";
                    }
                }
                else 
                {
                    // loginattemp = 0 || loginattemp = null
                    return "true";
                }
            }

            return "p_true";
        }

        private void UpdateLoginAttemp(string username) 
        {
            SQLTransaction mtrans = new SQLTransaction();
            try 
            {
                mtrans.StartTransaction();

                mtrans.UpdateTo("userHeader", 
                new Dictionary<string, object>() { 
                    {"LoginAttemp", null}
                }, 
                new Dictionary<string, object>() { 
                    {"userName", username}
                });

                mtrans.Committransaction();
            }
            catch (Exception ex) 
            {
                mtrans.RollbackTransaction();
            }
        }

        [AuthorizeUsr]
        [HttpPost]
        public string SaveNewPassword(string oldpassword, string newPassword, string username) 
        {
            SQLTransaction mt_trans = new SQLTransaction();
            string old_password = "";

            DataTable tbl_old_password = SqlDbHelper.getDataDT("select userPass from userHeader where userName = '" + username + "'");
            foreach (DataRow itm in tbl_old_password.Rows) 
            {
                old_password = itm["userPass"].ToString();
            }

            if (old_password == newPassword) 
            {
                return SActionResult.Error + "NEW PASSWORD CANNOT BE THE SAME \nAS THE OLD PASSWORD!";
            }

            try 
            {
                mt_trans.StartTransaction();

                mt_trans.UpdateTo("userHeader",
                new Dictionary<string, object>() { 
                    {"userPass", Encryption.StringEncrypter(newPassword)}
                    ,{ "LoginAttemp", 1 }
                    ,{ "LastPasswordChange", DateTime.Now.ToString("MM/dd/yyyy") }
                }, new Dictionary<string, object>() { 
                    {"userName", username}
                });

                mt_trans.Committransaction();

                return SActionResult.Success;
            }
            catch (Exception ex) 
            {
                mt_trans.RollbackTransaction();
                return SActionResult.Error;
            }
        }

        public string UpdateUser(string username) 
        {
            SQLTransaction mt_trans = new SQLTransaction();

            try
            {
                mt_trans.StartTransaction();

                // ALSO UPDATE THE userHeader.LoginAttemp
                mt_trans.CommandText = "Update userHeader set LoginAttemp=1 where username='" + username + "'";

                mt_trans.Committransaction();

                return SActionResult.Success;
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return SActionResult.Error;
            }
        }

        public ActionResult FramedSetted() 
        {
            string docid = "", doctype = "", doccontroller = "", docview = "", docidname = "";
            try
            {
                // check if current user is FSP
                _User Ousr = new _User(Session["username"].ToString());

                // cached the user's info
                Session["Ousr"] = Ousr;

                // use the session,... cast to _User
                _User _Ours = (_User)Session["Ousr"];
                
                Session["isFSP"] = "FALSE";
                if (Ousr.HasPositionsOf(new string[] { "SO", "ASM" }))
                {
                    Session["isFSP"] = "TRUE";
                }
            }
            catch { }

            return View();
        }

        public ActionResult MainMenus() 
        {
            /* test code only */
            //var DATABASE = new Models.ARMSTestEntities();

            //var current_user = new _User(Session["username"].ToString());

            //var modules = from a in DATABASE.AppModules
            //          select a;

            //var roleAccess = (from a in DATABASE.AppRoleAccesses.ToList()
            //           from b in DATABASE.apprvrRoles.ToList()
            //           from c in current_user.Roles.AsEnumerable()
            //           where a.roleID == b.roleID && b.roleCode == c.Position
            //           select a).ToList();

            //var sb = new System.Text.StringBuilder();

            //int module_level = 0,
            //    _sub_menu_level = 1;



            //ViewData["menu_builder"] = menu_builder(modules.Where(o => o.ParentAppModuleId == null), ref sb, ref module_level, modules, ref _sub_menu_level, roleAccess);

            //DATABASE.Dispose();
            // end of test code

            return View();
        }

        public ActionResult TopFrame() 
        {
            return View();
        }

        public ActionResult BlankDestinationPage() 
        {
            return View();
        }

        public JsonResult getSalesInfo(string userId)
        {
            _User cuurent_user = (_User)Session["Ousr"];
            string userId_ = cuurent_user.EmployeeIdNo;
            var ajx_res = new SkelClass.AjxResult();
            try
            {
                ajx_res.data = new
                {
                    sales_info = UserDefineFunctions.CalendarEvent.SoCalendar.getSalesMonitoringInfo(userId_)
                };

                ajx_res.iserror = false;
            }
            catch (Exception ex)
            {
                ajx_res.message = ex.Message;
                ajx_res.iserror = true;
            }

            return Json(ajx_res);
        }

        /*
        private string menu_builder(IQueryable<Models.AppModule> qry, ref System.Text.StringBuilder sb, ref int module_level, IQueryable<Models.AppModule> modules, ref int _sub_menu_level, List<Models.AppRoleAccess> roleAccess)
        {
            string name_class_1 = "";
            string name_class_2 = "";
            string name_class_3 = "";
            string name_class_4 = "";

            foreach (var itm in qry)
            {
                if (roleAccess.Any(o => o.AppModuleId == itm.AppModuleId))
                {
                    name_class_1 = module_level == 0 ? "menu" : "menu_" + module_level.ToString();
                    name_class_2 = module_level == 0 ? "menu_title" : "menu_title_" + module_level.ToString();
                    name_class_3 = module_level == 0 ? "sub_menu_holder" : "sub_menu_holder_" + module_level.ToString();
                    name_class_4 = module_level == 0 ? "sub_menu" : "sub_menu_" + module_level.ToString();

                    if (itm.AppModuleLink == null)
                    {
                        sb.Append("<div class=\"" + name_class_1 + "\">");
                        sb.Append("<div class=\"" + name_class_2 + "\">").Append(itm.AppModuleName).Append("</div>");
                        sb.Append("<div class=\"" + name_class_3 + "\">");
                        _sub_menu_level = module_level;
                    }
                    else
                        sb.Append("<div class=\"" + (_sub_menu_level == 0 ? "sub_menu" : "sub_menu_" + (_sub_menu_level).ToString()) + "\"><a href=\"" + Url.Content("~/" + itm.AppModuleLink) + "\" target=\"content_frameset\">").Append(itm.AppModuleName).Append("</a></div>");

                    if (modules.Any(o => o.ParentAppModuleId == itm.AppModuleId))
                    {
                        module_level++;
                        menu_builder(modules.Where(o => o.ParentAppModuleId == itm.AppModuleId), ref sb, ref module_level, modules, ref _sub_menu_level, roleAccess);
                        module_level--;
                        _sub_menu_level--;
                    }

                    if (itm.AppModuleLink == null)
                    {
                        sb.Append("</div>");
                        sb.Append("</div>");
                    }
                }
            }

            return sb.ToString();

        }
        */
    }
}
