using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARMS_W.Class;
using ARMS_W.GLOBALS;
using System.Data;
using ARMS_W.SkelClass;

namespace ARMS_W.Controllers
{
    public class MasterFileController : Controller
    {
        //
        // GET: /MasterFile/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GTAccountMasterFile()
        {
            return View();
        }

        #region GT ACCOUNT TAGGING PARETO

        public JsonResult UpdateGTAccount(string _acctCode,bool _Pareto)//SkelClass.page_param.gtaccount_pareto page_param)
        {
            SQLTransaction sql_trans = new SQLTransaction();
                       
            try
            {
                sql_trans.StartTransaction();

                    sql_trans.UpdateTo("customerHeader",
                        new Dictionary<string, object>() { 
                        {"Pareto", _Pareto==true?"Y":"N" }
                    }
                        , new Dictionary<string, object>() { { "acctCode", _acctCode } });

                sql_trans.Committransaction();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                sql_trans.RollbackTransaction();
                throw new Exception("Failed updating..!");
            }
            return Json("");
        }

        [HttpPost]
        public JsonResult GetDetails()
        {
           

            var data = new
            {
               accountList = UserDefineFunctions.GTAccountMasterFile.getAccounts()//,
               //itemmasterfile = UserDefineFunctions.GTAccountMasterFile.getAccounts()
            };

            return Json(data);
        }

        public JsonResult getPareto(string selecteditem = "")
        {
            List<string> pareto = new List<string>();
            pareto.Add("Pareto");
            pareto.Add("Non-Pareto");
            return Json(pareto);
        }

        #endregion


    }

}
