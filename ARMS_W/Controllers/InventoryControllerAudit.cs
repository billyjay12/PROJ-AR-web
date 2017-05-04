using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARMS_W.SkelClass;
using ARMS_W.Class;
using ARMS_W.GLOBALS;
using ARMS_W.Objects;

namespace ARMS_W.Controllers
{
    public partial class InventoryController : Controller
    {
        public ActionResult CreateNewInventoryCountAudit(string InventoryCountId)
        {
            ViewData["InventoryCountId"] = InventoryCountId;
            return View();
        }

        public ActionResult ForAuditInventoryCount()
        {
            return View();
        }

        public ActionResult InventoryCountAuditDetails(string InventoryCountAuditId)
        {
            ViewData["InventoryCountAuditId"] = InventoryCountAuditId;
            return View();
        }

        public ActionResult AuditInventoryCountList()
        {
            return View();
        }

        public JsonResult getForAuditInventoryCountList()
        {
            var ajx_res = new AjxResult();

            var current_user = new _User(Session["username"].ToString());

            try
            {
                var list = UserDefineFunctions.InventoryCount.getForAuditInventory();

                ajx_res.data = new { list = list };
            }
            catch (Exception ex)
            {
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
            }

            return Json(ajx_res);
        }

        public JsonResult getAuditInventoryList()
        {
            var ajx_res = new AjxResult();

            var current_user = new _User(Session["username"].ToString());

            try
            {
                var list = UserDefineFunctions.InventoryCount.getAuditInventoryList();

                ajx_res.data = new { list = list };
            }
            catch (Exception ex)
            {
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
            }

            return Json(ajx_res);
        }

        public JsonResult getForAuditInventoryDetails(string inventoryCountId)
        {
            var ajx_res = new AjxResult();
            ajx_res.iserror = true;

            try
            {
                if (Session["username"] == null) throw new Exception("Session Expired");

                var current_user = new _User(Session["username"].ToString());

                var getDocumentStatusMessage = UserDefineFunctions.InventoryCount.getDocumentStatus(inventoryCountId);

                ajx_res.data = new
                {
                    inventoryHdr = UserDefineFunctions.InventoryCount.getInventoryDetails(inventoryCountId)
                    //docstatus = getDocumentStatusMessage,
                    //permission = UserDefineFunctions.InventoryCount.getPermission(getDocumentStatusMessage.roleId, current_user)
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

        public JsonResult saveAuditInventoryCount(SkelClass.page_param.inventoryCountAudit_save page_param)
        {
            var ajx_res = new AjxResult();
            var sql_trans = new SQLTransaction();

            var current_user = new _User(Session["username"].ToString());
            ajx_res.iserror = true;

            var inventoryId_GeneratedCode = GenerateNewCode("AUDIT", "InventoryCountAuditHdr", "inventoryCountAuditId");
           
            try
            {
                if (Session["username"] == null) throw new Exception("Session Expired!");

                sql_trans.StartTransaction();

                //SAVE AUDIT INVENTORY COUNT
                #region Save to InventoryCountHdr [Inventory Count Audit Header and Details]

                sql_trans.InsertTo("InventoryCountAuditHdr",
                        new Dictionary<string, object>() { 
                        {"inventoryCountAuditId",inventoryId_GeneratedCode }
                        ,{"referenceInventoryCountId", page_param.inventoryCountId }
                        ,{"date",DateTime.Now}
                        ,{"remarks",page_param.remarks}
                        ,{"auditedBy",current_user.EmployeeIdNo}
                    });

                foreach (var itm in page_param.inventoryAudit_details)
                {
                    sql_trans.InsertTo("InventoryCountAuditDtl",
                            new Dictionary<string, object>(){
                            {"inventoryCountAuditId",inventoryId_GeneratedCode},
                            {"lineId",itm.lineId},
                            {"itemCode",itm.itemCode},
                            {"actualCount",itm.actualCount},
                            {"remarks",itm.remarks}
                        });
                }

                #endregion

                sql_trans.UpdateTo("InventoryCountHdr", new Dictionary<string, object> { { "Audited", "T" } },
                                                          new Dictionary<string, object> { { "inventoryCountId", page_param.inventoryCountId } });

                sql_trans.Committransaction();
                ajx_res.iserror = false;
            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                ajx_res.message = ex.Message;
                ajx_res.iserror = true;
            }



            return Json(ajx_res);
        }

        public JsonResult getInventoryCountAuditDetails(string inventoryCountAuditId)
        {
            var ajx_res = new AjxResult();
            ajx_res.iserror = true;

            try
            {
                if (Session["username"] == null) throw new Exception("Session Expired");

                var current_user = new _User(Session["username"].ToString());

                ajx_res.data = new
                {
                    inventoryHdr = UserDefineFunctions.InventoryCount.getInventoryCountAuditDetails(inventoryCountAuditId)
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


    }
}
