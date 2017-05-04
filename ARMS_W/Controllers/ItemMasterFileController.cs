using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using System.IO;
using ARMS_W.SkelClass;
using ARMS_W.Class;
using ARMS_W.GLOBALS;
using ARMS_W.Objects;
using Newtonsoft.Json;

namespace ARMS_W.Controllers
{
    public class ItemMasterFileController : Controller
    {
        //
        // GET: /ItemMasterFile/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ItemMasterFile()
        {

            return View();
        }

        public ActionResult ListItemsAvailable()
        {
            return View();
        }



       // [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetDetails()
        {
           SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;

            try
            {
              ajx_result.iserror = false;
              var data = UserDefineFunctions.Application.getItmMasterFileDtls();

            ajx_result.data = new  {
            
               Itemmasterfile = data
            
            };

            
            }
            catch(Exception ex)
            {
            
                ajx_result.iserror = true;
                ajx_result.message = ex.Message;

            }
            

            return Json(ajx_result);
        }


        public JsonResult getItemsAvailable()
        {

            AjxResult res = new AjxResult();
            StringBuilder sb = new StringBuilder();
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            try
            {
                _User current_user = new _User(Session["username"].ToString());
                List<string> user_region = (from a in DATABASE.userHeaders
                                            from b in DATABASE.apprvrDesigs
                                            where a.counterId == b.counterId
                                                && a.empIdNo == current_user.EmployeeIdNo
                                            select b.branch == "LZ" ? "LUZON" : b.branch == "VM" ? "VISMIN" : "").ToList();

                if (current_user.Roles.Any(o => o.Position == "SPRUSER"))
                {
                    user_region.Add("LUZON");
                    user_region.Add("VISMIN");
                }


                user_region = user_region.Distinct().ToList();

                if (user_region.Contains("") && user_region.Count == 1)
                    throw new Exception("No branch assign in this user account. Contact your administrator");

                var qry = (from a in DATABASE.SapItemsAvailableQties
                           where user_region.Contains(a.whs)
                           select new
                           {
                               brand = a.U_Brand,
                               productgroup = a.ProductGrp,
                               itemcode = a.ItemCode,
                               itemname = a.ItemName,
                               whs = a.whs,
                               qty = a.qty,
                               dateasof = a.DateAsOf
                           }).ToList();

                foreach (var itm in qry)
                {
                    var qty = itm.qty.Value.ToString("G29");
                    sb.Append("<tr class=\"grid_hover\" clone=\"true\">");
                    sb.Append("<td>").Append(itm.brand).Append("</td>");
                    sb.Append("<td>").Append(itm.productgroup).Append("</td>");
                    sb.Append("<td>").Append(itm.itemcode).Append("</td>");
                    sb.Append("<td>").Append(itm.itemname).Append("</td>");
                    sb.Append("<td>").Append(itm.whs).Append("</td>");
                    sb.Append("<td>").Append(qty).Append("</td>");
                    sb.Append("</tr>");
                }


                res.data = new { table = sb.ToString(), dateasof = qry.First().dateasof.Value.ToString("dddd, dd MMMM yyyy hh:mm tt") };

                res.iserror = false;
            }
            catch (Exception ex)
            {
                res.iserror = true;
                res.message = ex.Message;
            }
            finally
            {
                DATABASE.Dispose();
            }

            return Json(res);
        }



    }
}
