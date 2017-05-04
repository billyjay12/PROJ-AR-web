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
using System.Net;

namespace ARMS_W.Controllers
{
    public partial class InventoryController : Controller
    {
        //
        // GET: /Inventory/


        public static string DetermineCompName(string IP)
        {
            IPAddress myIP = IPAddress.Parse(IP);
            IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
            List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
            return compName.First();
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateNewInventoryCount(string acctCode)
        {
            ViewData["acctCode"] = acctCode??"";
            return View();
        }

        public ActionResult InventoryCountList()
        {
            return View();
        }


       
        public ActionResult InventoryCountDetails(string InventoryCountId)
        {
            ViewData["InventoryCountId"] = InventoryCountId;
            return View();
        }

        public ActionResult InventoryCountDetailsForSO(string InventoryCountId)
        {
            ViewData["InventoryCountId"] = InventoryCountId;
            return View();
        }

        public string GetItemList(string _str_data = "", string keyword = "", string _brand="", DateTime? prevCountDate=null, DateTime? actualCountDate=null, string acctCode="")
        {
            StringBuilder table_builder = new StringBuilder();

            List<page_param.itemFile> _listData = new List<page_param.itemFile>();

            if (prevCountDate == null || actualCountDate==null)
                _listData = UserDefineFunctions.Application.getListOfItemCode();
            else
                _listData = UserDefineFunctions.Application.getListOfItemCode(acctCode, prevCountDate, actualCountDate);

            var res = _brand == null ? _listData.Take(10) : _listData.Where(o => o.brand == _brand);

            var filter_keyword = from a in res
                                 where a.itemCodeDesc.Contains(keyword.ToUpper())
                                 select a;

            foreach(var itm in filter_keyword)
                table_builder.Append("<div><a href=\"javascript:;\" val1=\"" + itm.itemCode + "\" val2=\"" + itm.itemCode + "\" val3=\"" + itm.itemDesc+ "\" val4=\"" + itm.prodGrp + "\" val5=\"" + itm.itemPrice + "\" val6=\"" + itm.sellin + "\">" + itm.itemCodeDesc + " </a></div>");

            return table_builder.ToString();
        }
        
        public JsonResult GetItemList1(string Brand, DateTime? prevCountDate, DateTime? actualCountDate, string acctCode)
        {
                    var ajx_res = new AjxResult();
            var qry = new List<page_param.itemFile>();

            if (prevCountDate == null)
                qry = UserDefineFunctions.Application.getListOfItemCode();
            else
                qry = UserDefineFunctions.Application.getListOfItemCode(acctCode, prevCountDate, actualCountDate);

            var res = Brand == null ? qry.Take(10) : qry.Where(o => o.brand == Brand);

            ajx_res.data = new { listArr = res };
            return Json(ajx_res);
        }

        public String lookUpBrand()
        {
            var sb = new StringBuilder();
            try
            {
                var Brand = UserDefineFunctions.Application.getListOfBrand().ToList();

                foreach (var itm in Brand)
                {
                    sb.Append("<option value=\"" + itm.brand + "\" brand=\"" + itm.brand + "\">" + itm.brand + "</option>");
                }

            }
            finally { }
            return sb.ToString();
        }

        public String itemfilterbybrandLookUp(string Brand,DateTime? prevCountDate, DateTime? actualCountDate, string acctCode)
        {
            var list = new List<string[]>();

            StringBuilder sb = new StringBuilder();

            var itemmasterfileList = new List<page_param.itemFile>();
                
            if (prevCountDate == null)
                itemmasterfileList = UserDefineFunctions.Application.getListOfItemCode();
            else
                itemmasterfileList = UserDefineFunctions.Application.getListOfItemCode(acctCode, prevCountDate, actualCountDate);

            var res = Brand == null ? itemmasterfileList : itemmasterfileList.Where(o => o.brand == Brand);

            foreach (var itm in res)
            {
                sb.Append("<option value=\"" + itm.itemDesc + "\" code=\"" + itm.itemCode + "\" brand=\"" + itm.brand + "\" prodgrp=\"" + itm.prodGrp + "\" amt=\"" + itm.itemPrice + "\" sellin=\"" + itm.sellin + "\">" + itm.itemCodeDesc + "</option>");
            }


            return sb.ToString();
        }

        //public JsonResult itemMasterFileList(DateTime? prevCountDate, DateTime? actualCountDate, string acctCode)
        //{
        //    var ajx_res = new AjxResult();
        //    var res = new List<page_param.itemFile>();

        //    if (prevCountDate == null)
        //        res = UserDefineFunctions.Application.getListOfItemCode();
        //    else
        //        res = UserDefineFunctions.Application.getListOfItemCode(acctCode, prevCountDate, actualCountDate);

        //    ajx_res.data = new { listArr = res };
        //    return Json(ajx_res);
        //}

        public JsonResult lookUpGroup()
        {
            List<string[]> Groups = UserDefineFunctions.Application.getListOfGroup().ToList();
            return Json(Groups);
        }

        public JsonResult lookUpItemCode()
        {
            List<string[]> Products = new List<string[]>();
            var res = UserDefineFunctions.Application.getListOfItemCode().ToList();

            foreach (var itm in res)
            {
                Products.Add(new string[] {
                    itm.itemCode,
                    itm.itemDesc,
                    itm.brand,
                    itm.prodGrp,
                    itm.itemCodeDesc,
                    itm.itemPrice.HasValue?itm.itemPrice.Value.ToString():"0",
                });
            }

            return Json(Products);
        }

        public String lookUpItemCode1(DateTime? prevCountDate,DateTime? actualCountDate,string acctCode)
        {
            var list = new List<string[]>();
            var res = UserDefineFunctions.Application.getListOfItemCode();

            StringBuilder sb = new StringBuilder();

            if (prevCountDate == null)
            {
                res = UserDefineFunctions.Application.getListOfItemCode();

                foreach (var itm in res)
                {
                    sb.Append("<option value=\"" + itm.itemDesc + "\" code=\"" + itm.itemCode + "\" brand=\"" + itm.brand + "\" prodgrp=\"" + itm.prodGrp + "\" amt=\"" + itm.itemPrice + "\">" + itm.itemCodeDesc + "</option>");
                }
            }
            else
            {
                res = UserDefineFunctions.Application.getListOfItemCode(acctCode, prevCountDate, actualCountDate);

                foreach (var itm in res)
                {
                    sb.Append("<option value=\"" + itm.itemDesc + "\" code=\"" + itm.itemCode + "\" brand=\"" + itm.brand + "\" prodgrp=\"" + itm.prodGrp + "\" amt=\"" + itm.itemPrice + "\" sellin=\"" + itm.sellin + "\">" + itm.itemCodeDesc + "</option>");
                }
            }

            return sb.ToString();
        }

        //public JsonResult lookUpSalesOfficerEmployee()
        //{
        //    List<string[]> SOemployee = UserDefineFunctions.Application.getListOfSalesOfficerEmployee().ToList();
        //    return Json(SOemployee);
        //}

        public String lookUpSalesOfficerEmployee()
        {
            //List<string[]> SOemployee = UserDefineFunctions.Application.getListOfSalesOfficerEmployee().ToList();
            var SOemployee = UserDefineFunctions.Application.getListOfSalesOfficerEmployee();
            StringBuilder sb = new StringBuilder();

            foreach (var itm in SOemployee)
            {
                sb.Append("<option value=\"" + itm.empFullName + "\" code=\"" + itm.empIDNo + "\">" + itm.empFullName + "</option>");
            }

            return sb.ToString();
        }

        public JsonResult lookUpAccoutCode()
        {
            List<string[]> accounts = new List<string[]>();
            var current_user = new _User(Session["username"].ToString());
            accounts = UserDefineFunctions.Application.getListOfAccountCode(current_user.EmployeeIdNo);

            return Json(accounts);
        }

        [HttpPost]
        public JsonResult getAccountInfo(string acctCode)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new AjxResult();
            List<string[]> accounts = new List<string[]>();
            res.iserror = false;    
            try
            {
                var current_user = new _User(Session["username"].ToString());

                var db_accounts = (from a in DATABASE.arms2_vw_inventoryScheduleAccounts
                                   where a.acctCode == acctCode && a.empIdNo == current_user.EmployeeIdNo
                                   select a).ToList();
                

                if (db_accounts.Count <= 0) { throw new Exception("Log-in account doesn't have the permission to count this customer account"); }

                foreach (var itm in db_accounts)
                {
                    accounts.Add(new string[] {
                        itm.acctCode,
                        itm.acctName,
                        itm.acctCode+ ", "+itm.acctName,
                        itm.pareto,
                        itm.ccaNum,
                        itm.areaname,
                        itm.bussAdd,
                        itm.territoryName
                    });
                }

                res.data = new { accounts = accounts };

            }
            catch (Exception ex) { res.iserror = true; res.message = ex.Message; }

            DATABASE.Dispose();

            return Json(res);
        }

        public JsonResult lookUpOutlet(string TMPLookUpType)
        {
            List<string[]> outlets = new List<string[]>();
            try
            {
                var current_user = new _User(Session["username"].ToString());
               

                if (current_user.Roles.Any(o => o.Position == "SPRUSER"))
                    outlets = UserDefineFunctions.Application.getListOfOutlet(TMPLookUpType);
                else
                    outlets = UserDefineFunctions.Application.getListOfOutlet(TMPLookUpType, current_user.EmployeeIdNo);
            }
            catch { }
            return Json(outlets);
        }

        public JsonResult lookUpWhsIncharge(string TMPLookUpType)
        {
            var acctCode = TMPLookUpType;

            List<string[]> whsincharge = UserDefineFunctions.Application.getListOfWhsIncharge(acctCode).ToList();
            return Json(whsincharge);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult saveInventoryCount(SkelClass.page_param.inventoryCount page_param)
        {
            var ajx_res = new AjxResult();
            var sql_trans = new SQLTransaction();

            var current_user = new _User(Session["username"].ToString());
            ajx_res.iserror = true;

            int start_docstatid = 1;
            int next_id = 1;

            Globals.DocActionType act_type = Globals.DocActionType.Approve;
            
            if (page_param.act_type == "SAVE_AS_DRAFT")
                act_type = Globals.DocActionType.SaveAsDraft;
            else if (page_param.act_type == "SAVE")
                act_type = Globals.DocActionType.Save;
            else
                act_type = Globals.DocActionType.None;

            var inventoryId_GeneratedCode = page_param.inventoryCountId == null ? GenerateNewCode("IC", "InventoryCountHdr", "inventoryCountId") : page_param.inventoryCountId;
            var whsId_GeneratedCode = page_param.whsInchargeID == null ? GenerateNewCode("WHSIN", "custOutletIncharge", "whsInchargeID") : page_param.whsInchargeID;

            string EventID_GeneratedCode = GenerateNewCode("EVNT", "CoverageHdr", "EventID");
            string lineNum_GeneratedCode = GenerateNewCode("SEQ", "CoverageDtls", "LineNum");
     
            var nextlineId_GeneratedCode = GenerateNewCode("NIC", "nextInventoryCountHdr", "lineId");

            var inventoryCount_from_db = UserDefineFunctions.InventoryCount.getInventoryCountHdr(page_param.inventoryCountId);

            try
            {
                sql_trans.StartTransaction();

                if (Session["username"] == null) throw new Exception("Session Expired!");

                sql_trans.DeleteFrom("InventoryCountDtl", new Dictionary<string, object>() { { "inventoryCountId", page_param.inventoryCountId } });
                sql_trans.DeleteFrom("InventoryCountHdr", new Dictionary<string, object>() { { "inventoryCountId", page_param.inventoryCountId } });

                if (page_param.whsInchargeID != null || page_param.whsInchargeID != "")
                {
                    if (page_param.newWhsIncharge == true)
                    {
                        //SAVE to custOutletIncharge
                        #region  Save to custOutletIncharge [warehouse incharge detail]

                        sql_trans.InsertTo("custOutletIncharge",
                                new Dictionary<string, object>() { 
                        {"whsInchargeID",whsId_GeneratedCode }
                        ,{"acctCode",page_param.acctCode}
                        ,{"whsInchargeFirstName", page_param.whs_details.whsInchargeFirstName }
                       // ,{"custOutletsID",page_param.custOutletsID}
                        ,{"whsInchargeMiddleName",page_param.whs_details.whsInchargeMiddleName }
                        ,{"whsInchargeLastName",page_param.whs_details.whsInchargeLastName}
                        ,{"whsInchargeContactNo",page_param.whs_details.whsInchargeContactNo}
                        });

                        #endregion
                    }
                }

                page_param.inventoryCountStatus = "HIT";
                if (page_param.prevCountDate.HasValue)
                    page_param.inventoryCountStatus = DateTime.Now <= page_param.actualCountEndValidDate ? "HIT" : "MISS";

                //SAVE INVENTORY COUNT
                #region Save to InventoryCountHdr [Inventory Count Header and Details]

                sql_trans.InsertTo("InventoryCountHdr",
                        new Dictionary<string, object>() { 
                        {"inventoryCountId",inventoryId_GeneratedCode }
                        ,{"docTypeId", (int)Globals.InfoType.InventoryCountHdr }
                        ,{"whsInchargeID",whsId_GeneratedCode}
                        ,{"empId",page_param.empId }
                        ,{"acctCode",page_param.acctCode}
                        ,{"prevCountDate",page_param.prevCountDate}
                        ,{"dateTimeStamp",DateTime.Now}
                        ,{"nextCountDate",page_param.nextCountDate}
                        ,{"countRange",page_param.countRange}
                        ,{"totalAmount",page_param.totalAmount}
                        ,{"remarks",page_param.remarks}
                        ,{"documentstatusid",start_docstatid}                        
                        ,{"actualCountDate",page_param.actualCountDate}
                        ,{"inventoryCountStatus",page_param.inventoryCountStatus}
                        ,{"actualCountEndValidDate", page_param.actualCountEndValidDate}
                        ,{"Audited","F"}
                    });

                foreach (var itm in page_param.inventorycount_list)
                {
                    sql_trans.InsertTo("InventoryCountDtl",
                            new Dictionary<string, object>(){
                            {"inventoryCountId",inventoryId_GeneratedCode},
                            {"lineId",itm.lineId},
                            {"itemCode",itm.itemCode},
                            {"ssr",itm.ssr},
                            {"begNvPcs",itm.begNvPcs},
                            {"netSellIn",itm.sellIn},
                            {"endNvPcs",itm.endNvPcs},
                            {"netSellinOnHand",itm.netOnHand},
                            {"actualSellOutPcs",itm.actualSellOutPcs},
                            {"actualSelloutAmt",itm.actualSellOutAmt},
                            {"variancePcs",getVariancePcs(itm.forecastFTMpcs0.HasValue?itm.forecastFTMpcs0.Value:0,itm.actualSellOutPcs.HasValue?itm.actualSellOutPcs.Value:0)},
                            {"varianceAmt", getVarianceAmount(itm.forecastFTMamt0.HasValue?itm.forecastFTMamt0.Value:0, itm.actualSellOutAmt.HasValue?itm.actualSellOutAmt.Value:0)},
                            {"salesForecastPcs0",itm.forecastFTMpcs0},
                            {"salesForecastAmt0",itm.forecastFTMamt0},
                            {"salesForecastPcs1",itm.forecastFTMpcs1},
                            {"salesForecastAmt1",itm.forecastFTMamt1},
                            {"salesForecastPcs2",itm.forecastFTMpcs2},
                            {"salesForecastAmt2",itm.forecastFTMamt2},
                            {"salesForecastPcs3",itm.forecastFTMpcs3},
                            {"salesForecastAmt3",itm.forecastFTMamt3},
                            {"remarks",itm.remarks}
                        });
                }

                #endregion

                if (act_type == Globals.DocActionType.SaveAsDraft)
                {
                    next_id = 0;
                }
                else if (act_type == Globals.DocActionType.Save)
                {
                    next_id = 1;

                    if (inventoryCount_from_db.documentstatusid == 1)// determine if inventory count is RETURN or NEW
                        page_param.inventoryCountStatus = inventoryCount_from_db.inventoryCountStatus;
                    else
                    {
                        page_param.inventoryCountStatus = DateTime.Now <= page_param.actualCountEndValidDate ? "HIT" : "MISS";
                    }

                    int success = UserDefineFunctions.InventoryCount.saveToCoveragePlan(ref sql_trans, page_param.empId, page_param.acctCode, page_param.StartCountDate, page_param.actualCountDate.Value, page_param.inventoryCountStatus, lineNum_GeneratedCode, EventID_GeneratedCode, inventoryId_GeneratedCode);
                    if (success != 0)
                    {
                        throw new Exception("Error saving coverage plan schedule");
                    }
                }

                #region SAVE Next inventory count

                var nextInvCount = UserDefineFunctions.InventoryCount.getNextInventoryCount(page_param.acctCode);
                //delete current next count detail
                sql_trans.DeleteFrom("nextInventoryCountDtl", new Dictionary<string, object>() { { "lineId", nextInvCount.lineId } });

                //delete current next count header
                sql_trans.DeleteFrom("nextInventoryCountHdr", new Dictionary<string, object>() { { "lineId", nextInvCount.lineId } });

                var nxtInventoryCountHdr = new page_param.nextInventoryCount();

                nxtInventoryCountHdr.acctCode = page_param.acctCode;
                //nxtInventoryCountHdr.custOutletsID = page_param.custOutletsID;
                nxtInventoryCountHdr.lineId = nextlineId_GeneratedCode;
                nxtInventoryCountHdr.prevCountDate = page_param.actualCountDate;

                bool isSunday = page_param.nextCountDate.Value.ToString("ddd").ToUpper() == "SUN" ? true : false;
                if (isSunday) { page_param.nextCountDate = page_param.nextCountDate.Value.AddDays(1); }
                //page_param.nextCountDate = 

                //save for next count header
                sql_trans.InsertTo("nextInventoryCountHdr",
                       new Dictionary<string, object>() { 
                            {"lineId",nxtInventoryCountHdr.lineId }
                            ,{"acctCode",nxtInventoryCountHdr.acctCode}
                            ,{"prevCountDate",nxtInventoryCountHdr.prevCountDate}
                            ,{"startCountDate",page_param.nextCountDate}
                        });

                foreach (var itm in page_param.inventorycount_list)
                {
                    //save for next count
                    sql_trans.InsertTo("nextInventoryCountDtl",
                            new Dictionary<string, object>(){
                            {"lineId",nxtInventoryCountHdr.lineId},
                            {"itemCode",itm.itemCode},
                            {"ssr",itm.ssr},
                            {"begNvPcs",itm.endNvPcs},
                            {"salesForecastPcs0",itm.forecastFTMpcs1},
                            {"salesForecastAmt0",itm.forecastFTMamt1},
                            {"salesForecastPcs1",itm.forecastFTMpcs2},
                            {"salesForecastAmt1",itm.forecastFTMamt2},
                            {"salesForecastPcs2",itm.forecastFTMpcs3},
                            {"salesForecastAmt2",itm.forecastFTMamt3}
                            });
                }

                #endregion


                #region SAVE ROUTE CHANGES

                sql_trans.InsertTo("RouteChanges", new Dictionary<string, object>() { 
                    {"DocTypeId",  (int)Globals.InfoType.InventoryCountHdr}
                    , {"ActionType", (int)act_type}
                    , {"DocStatusId", next_id}
                    , {"UserName", current_user.UserName.ToUpper()}
                    , {"PrevDocStatusId",inventoryCount_from_db.documentstatusid}
                    , {"Remarks", page_param.appvr_remarks}
                    , {"TimeStamp", DateTime.Now}
                    , {"DocId", inventoryId_GeneratedCode}
                    , {"RoleCode", MiscFunctions.GetRoleCode((int)Globals.InfoType.InventoryCountHdr,inventoryCount_from_db.documentstatusid==0?start_docstatid:inventoryCount_from_db.documentstatusid) }
                    });
                
                #endregion

                sql_trans.UpdateTo("InventoryCountHdr", new Dictionary<string, object> { { "documentstatusid", next_id } },
                                                          new Dictionary<string, object> { { "inventoryCountId", inventoryId_GeneratedCode } });


                ajx_res.data = new { new_id = inventoryId_GeneratedCode };
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

        public JsonResult UpdateInventoryCount(page_param.inventoryCount page_param)
        {
            var ajx_res = new AjxResult();
            var sql_trans = new SQLTransaction();
           
           
            Globals.DocActionType act_type = Globals.DocActionType.Approve;
            var current_user = new _User(Session["username"].ToString());

            int next_id = 0;
            int current_id = 0;

            if (page_param.act_type == "UPDATE")
                act_type = Globals.DocActionType.Update;
            else if (page_param.act_type == "RETURN_TO_REQUESTOR")
                act_type = Globals.DocActionType.ReturnToRequestor;
            else if (page_param.act_type == "SAVE_AS_DRAFT")
                act_type = Globals.DocActionType.SaveAsDraft;
            else if (page_param.act_type == "" || page_param.act_type == null)
                act_type = Globals.DocActionType.None;

            try
            {
                sql_trans.StartTransaction();

                var inventoryCount_from_db = UserDefineFunctions.InventoryCount.getInventoryCountHdr(page_param.inventoryCountId);
                var nextInventoryCountHdr_from_db = UserDefineFunctions.InventoryCount.getNextInventoryCount(page_param.acctCode);
                current_id = inventoryCount_from_db.documentstatusid;

                page_param.inventoryCountStatus = "HIT";
                if (page_param.prevCountDate.HasValue)
                    page_param.inventoryCountStatus = DateTime.Now <= page_param.actualCountEndValidDate ? "HIT" : "MISS";

                //SAVE INVENTORY COUNT
                if (act_type != Globals.DocActionType.ReturnToRequestor)
                {
                    #region Save to InventoryCountHdr [Inventory Count Header and Details]

                    sql_trans.UpdateTo("InventoryCountHdr",
                            new Dictionary<string, object>() { 
                        {"whsInchargeID",inventoryCount_from_db.whsInchargeID}
                        ,{"dateTimeStamp",DateTime.Now}
                        ,{"remarks",page_param.remarks}
                        ,{"inventoryCountStatus",inventoryCount_from_db.inventoryCountStatus}
                    }, new Dictionary<string, object>() { { "inventoryCountId", inventoryCount_from_db.inventoryCountId } });

                    sql_trans.DeleteFrom("InventoryCountDtl", new Dictionary<string, object>() { { "inventoryCountId", inventoryCount_from_db.inventoryCountId } });

                    foreach (var itm in page_param.inventorycount_list)
                    {
                        sql_trans.InsertTo("InventoryCountDtl",
                                new Dictionary<string, object>(){
                            {"inventoryCountId",inventoryCount_from_db.inventoryCountId },
                            {"lineId",itm.lineId},
                            {"itemCode",itm.itemCode},
                            {"ssr",itm.ssr},
                            {"begNvPcs",itm.begNvPcs},
                            {"netSellIn",itm.sellIn},
                            {"endNvPcs",itm.endNvPcs},
                            {"actualSellOutPcs",itm.actualSellOutPcs},
                            {"actualSelloutAmt",itm.actualSellOutAmt},
                            {"variancePcs",getVariancePcs(itm.forecastFTMpcs0.HasValue?itm.forecastFTMpcs0.Value:0,itm.actualSellOutPcs.HasValue?itm.actualSellOutPcs.Value:0)},
                            {"varianceAmt", getVarianceAmount( itm.forecastFTMamt0.HasValue?itm.forecastFTMamt0.Value:0, itm.actualSellOutAmt.HasValue?itm.actualSellOutAmt.Value:0)},
                            {"salesForecastPcs0",itm.forecastFTMpcs0},
                            {"salesForecastAmt0",itm.forecastFTMamt0},
                            {"salesForecastPcs1",itm.forecastFTMpcs1},
                            {"salesForecastAmt1",itm.forecastFTMamt1},
                            {"salesForecastPcs2",itm.forecastFTMpcs2},
                            {"salesForecastAmt2",itm.forecastFTMamt2},
                            {"salesForecastPcs3",itm.forecastFTMpcs3},
                            {"salesForecastAmt3",itm.forecastFTMamt3},
                            {"remarks",itm.remarks}
                        });
                    }

                    #endregion
                }

                if (act_type == Globals.DocActionType.SaveAsDraft)
                {
                    next_id = inventoryCount_from_db.documentstatusid;
                }
                else if (act_type == Globals.DocActionType.Update)
                {
                    /*
                      (document status)
                        0	- Draft by Sales Officer
                        1	- Saved
                     */
                        next_id = current_id + 1;

                        var nextInvCount = UserDefineFunctions.InventoryCount.getNextInventoryCount(page_param.acctCode);

                        #region update Next inventory count


                       // var nextInvCount = UserDefineFunctions.InventoryCount.getNextInventoryCount(page_param.acctCode, page_param.custOutletsID);
                        //delete current next count detail
                        sql_trans.DeleteFrom("nextInventoryCountDtl", new Dictionary<string, object>() { { "lineId", nextInventoryCountHdr_from_db.lineId } });
           

                        foreach (var itm in page_param.inventorycount_list)
                        {
                            //save for next count
                            sql_trans.InsertTo("nextInventoryCountDtl",
                                    new Dictionary<string, object>(){
                                {"lineId",nextInventoryCountHdr_from_db.lineId},
                                {"itemCode",itm.itemCode},
                                {"ssr",itm.ssr},
                                {"begNvPcs",itm.endNvPcs},
                                {"salesForecastPcs0",itm.forecastFTMpcs1},
                                {"salesForecastAmt0",itm.forecastFTMamt1},
                                {"salesForecastPcs1",itm.forecastFTMpcs2},
                                {"salesForecastAmt1",itm.forecastFTMamt2},
                                {"salesForecastPcs2",itm.forecastFTMpcs3},
                                {"salesForecastAmt2",itm.forecastFTMamt3}
                            });
                        }

                        #endregion
                }

                //update document status
                sql_trans.UpdateTo("inventoryCountHdr", new Dictionary<string, object> { { "DocumentStatusId", next_id } },
                                                        new Dictionary<string, object> { { "inventoryCountId", inventoryCount_from_db.inventoryCountId } });

                #region SAVE ROUTE CHANGES

                sql_trans.InsertTo("RouteChanges", new Dictionary<string, object>() { 
                    {"DocTypeId",  (int)Globals.InfoType.InventoryCountHdr}
                    , {"ActionType", (int)act_type}
                    , {"DocStatusId", next_id}
                    , {"PrevDocStatusId", inventoryCount_from_db.documentstatusid}
                    , {"UserName", current_user.UserName}
                    , {"Remarks", page_param.appvr_remarks}
                    , {"TimeStamp", DateTime.Now}
                    , {"DocId", inventoryCount_from_db.inventoryCountId}
                    , {"RoleCode", MiscFunctions.GetRoleCode((int)Globals.InfoType.InventoryCountHdr,inventoryCount_from_db.documentstatusid) }
                    });

                #endregion

                sql_trans.Committransaction();
            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
            }

            return Json(ajx_res);
        }

        public JsonResult getDetails()
        {
            var ajx_res = new AjxResult();
            string strQuery = null;
            var date = DateTime.Now;

            ajx_res.iserror = true;

            try
            {
                var current_user = new _User(Session["username"].ToString());
                if (Session["username"].ToString() == null) throw new Exception("Session Expired!");


                var countingday = UserDefineFunctions.DayCycle.getDayCycle();

                strQuery = "select * from mtc_vw_User_Role_Details where empIDNo = '" + current_user.EmployeeIdNo + "'";
                var employeeDetails = new page_param.employeeDetail();
                DataTable dt_emp_from_db = null;
                dt_emp_from_db = SqlDbHelper.getDataDT(strQuery);
                #region LOAD DATA

                foreach (DataRow itm in dt_emp_from_db.Rows)
                {
                    employeeDetails.empIDNo = itm["empIDNo"].ToString();
                    employeeDetails.empFirstName = itm["firstName"].ToString();
                    employeeDetails.empLastName = itm["lastName"].ToString();
                }

                #endregion

                ajx_res.data = new { list = countingday, empDetails = employeeDetails };

                ajx_res.iserror = false;
            }
            catch (Exception ex)
            {
                ajx_res.message = ex.Message;
                ajx_res.iserror = true;
            }

            return Json(ajx_res);
        }

        public JsonResult getInventoryCountList()
        {
            var ajx_res = new AjxResult();

            try
            {
                var current_user = new _User(Session["username"].ToString());
                var list = new List<page_param.inventoryCountList>();

                if (current_user.Roles.Any(o => o.Position.ToUpper() == "SPRUSER"))
                    list = UserDefineFunctions.InventoryCount.getInventoryCountList(true);
                else if (current_user.Roles.Any(o => o.Position.ToUpper() == "CHM"))
                    list = UserDefineFunctions.InventoryCount.getInventoryCountList_ASMSO(current_user.EmployeeIdNo);
                else if (current_user.Roles.Any(o => o.Position.ToUpper() == "SO" || o.Position.ToUpper() == "ASMSO"))
                    list = UserDefineFunctions.InventoryCount.getInventoryCountList(current_user.EmployeeIdNo);
                else
                    list = UserDefineFunctions.InventoryCount.getInventoryCountList(false);

                ajx_res.data = new { list = list };
            }
            catch (Exception ex)
            {
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
            }

            return Json(ajx_res);
        }

        public JsonResult getInventoryDetails(string inventoryCountId)
        {
            var ajx_res = new AjxResult();
            ajx_res.iserror = true;

            try
            {
                if (Session["username"] == null) throw new Exception("Session Expired");

                var current_user = new _User(Session["username"].ToString());

                var getDocumentStatusMessage = UserDefineFunctions.InventoryCount.getDocumentStatus(inventoryCountId);
                var invCountDetails = UserDefineFunctions.InventoryCount.getInventoryDetails(inventoryCountId);
                string IP = Request.UserHostName;
                IP = HttpContext.Request.UserHostName.ToString();
                string compName = DetermineCompName(IP);
                ajx_res.data = new
                {
                    inventoryHdr = invCountDetails,
                    docstatus = getDocumentStatusMessage,
                    permission = UserDefineFunctions.InventoryCount.getPermission(getDocumentStatusMessage.roleId, current_user),
                    iptest = compName
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

        public JsonResult getNextCountInventory(string acctcode)
        {
            var ajx_res = new AjxResult();
            var date = DateTime.Now;
            ajx_res.iserror = true;
            acctcode = acctcode.Trim();
            try
            {
                if (Session["username"] == null) throw new Exception("Session Expired!");

                var current_user = new _User(Session["username"].ToString());
                var permission = new Globals.cUserPermission();

                var list_nextInventoryCount = new List<page_param.nextInventoryCount.nextInventoryCountDetail>();
                var nextInventoryCount = UserDefineFunctions.InventoryCount.getNextInventoryCount(acctcode);
                
                var countingday = UserDefineFunctions.DayCycle.getDayCycle();

                //var start_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, countingday.startDayOfTheMonth);

                if (nextInventoryCount.prevCountDate != null)
                {
                    nextInventoryCount.startCountDate = nextInventoryCount.startCountDate.HasValue ? nextInventoryCount.startCountDate : null;
                    /*
                     * add start count date with the range day cycle (base on cycleday Maintenance)
                     */
                    nextInventoryCount.endCountDate = nextInventoryCount.startCountDate.Value.AddDays(countingday.rangeDayCycle);


                    nextInventoryCount.actualCountStartValidDate = nextInventoryCount.startCountDate.Value.AddDays(countingday.rangeDayCycle * -1);
                    nextInventoryCount.actualCountEndValidDate = nextInventoryCount.startCountDate.Value.AddDays(countingday.rangeDayCycle);

                    //check if valid counting date is greater than current date to verify allowSave
                    permission._AllowSave = DateTime.Now > nextInventoryCount.actualCountStartValidDate ? true : false;
                }
                else
                    permission._AllowSave = true;

                ajx_res.data = new
                {
                    header = nextInventoryCount,
                    list = nextInventoryCount.nextInventory_detail,
                    permission = permission
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

        public JsonResult getNextCountAndCountRange(DateTime? actualCountDate, DateTime? prevCountDate, DateTime? actualCountEndValidDate,string acctCode)
        {
            var res = new AjxResult();
            res.iserror = true;
            try
            {
                
                var list_nextInventoryCount = new List<page_param.nextInventoryCount.nextInventoryCountDetail>();
                var countingday = UserDefineFunctions.DayCycle.getDayCycle();

                if (prevCountDate.HasValue)
                {
                    var nextInventoryCount = UserDefineFunctions.InventoryCount.getNextInventoryCount(acctCode);
                    var listOfItemSellIn = UserDefineFunctions.Application.getListOfItemCode(acctCode, prevCountDate, actualCountDate);

                    var qry = (from a in listOfItemSellIn
                               from b in nextInventoryCount.nextInventory_detail
                               where b.itemCode == a.itemCode
                               select new { a, b });

                    foreach (var itm in qry)
                    {
                        list_nextInventoryCount.Add(new page_param.nextInventoryCount.nextInventoryCountDetail()
                        {
                            itemCode = itm.b.itemCode,
                            itemDesc = itm.b.itemDesc,
                            itemPrice = itm.b.itemPrice,
                            lineId = itm.b.lineId,
                            begNvPcs = itm.b.begNvPcs,
                            brand = itm.b.brand,
                            forecastFTMamt0 = itm.b.forecastFTMamt0,
                            forecastFTMamt1 = itm.b.forecastFTMamt1,
                            forecastFTMamt2 = itm.b.forecastFTMamt2,
                            forecastFTMpcs0 = itm.b.forecastFTMpcs0,
                            forecastFTMpcs1 = itm.b.forecastFTMpcs1,
                            forecastFTMpcs2 = itm.b.forecastFTMpcs2,
                            prodGrp = itm.b.prodGrp,
                            netSellIn = itm.a.sellin,
                            ssr = itm.b.ssr??""
                        });
                    }

                }
                res.data = new
                {
                    nextCountDate = actualCountDate.Value.AddDays(countingday.DayCycleCount),
                    countRange = prevCountDate.HasValue ? actualCountDate.Value.Subtract(prevCountDate.Value).Days : 0,
                    inventoryCountStatus = prevCountDate.HasValue ? DateTime.Now <= actualCountEndValidDate.Value.Date ? "HIT" : "MISS" : "HIT",
                    list = list_nextInventoryCount,
                };

                res.iserror = false;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
                res.iserror = true;
            }

            return Json(res);
        }

        private string GenerateNewCode(string initCode, string tableName , string ID_fieldName)
        {
            DataTable qry = SqlDbHelper.getDataDT("select " + ID_fieldName + " from " + tableName + " where left(" + ID_fieldName + "," + initCode.Length + ")='" + initCode + "'");

            List<int> list_of_id = new List<int>();
            foreach (DataRow itm in qry.Rows)
            {
                if (initCode == "")
                    list_of_id.Add(Convert.ToInt32(itm[ID_fieldName].ToString()));
                else
                    list_of_id.Add(Convert.ToInt32(itm[ID_fieldName].ToString().Replace(initCode, "")));
            }

            if (list_of_id.Count > 0)
            {
                int largest_id = list_of_id.Max();
                return initCode + string.Format("{0:000000}", largest_id + 1);
            }
            else
            {
                return initCode + "000000";
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public string getInventoryCountRouteChanges(string InventoryCountId)
        {

            List<Globals.db_route_changes> route_changes = MiscFunctions.GetRouteChanges(Globals.InfoType.InventoryCountHdr, InventoryCountId);
          
            StringBuilder table_builder = new StringBuilder(2000);

            route_changes = route_changes.OrderByDescending(c => c.DateTimeStamp).ToList();
            string a_style = "border:1px solid #ededed; font-size:11px; font-family:arial;";
            string b_style = "font-weight:bold; background:#f4f4f4; font-size:12px; font-family:arial;";

            table_builder.Append("<table cellspacing=\"0\" cellpadding=\"3\" border=\"0\" style=\"" + a_style + "\" >");

            table_builder.Append("<tr style=\"" + b_style + "\" >");
            table_builder.Append("<td align=\"center\"><b>Approver/User</b></td>");
            table_builder.Append("<td align=\"center\"><b>Position</b></td>");
            table_builder.Append("<td align=\"center\"><b>Date</b></td>");
            table_builder.Append("<td align=\"center\"><b>Action</b></td>");
            table_builder.Append("<td align=\"center\"><b>Remarks</b></td>");
            table_builder.Append("</tr>");

            foreach (var route_change in route_changes)
            {
                table_builder.Append("<tr>");

                table_builder.Append("<td>");
                table_builder.Append(route_change.UserName);
                table_builder.Append("</td>");

                table_builder.Append("<td>");
                table_builder.Append(route_change.PositionName);
                table_builder.Append("</td>");

                table_builder.Append("<td>");
                table_builder.Append(route_change.DateTimeStamp.ToString("MM/dd/yyyy h:mm:ss tt"));
                table_builder.Append("</td>");

                table_builder.Append("<td>");
                table_builder.Append(route_change.Action.ToString());
                table_builder.Append("</td>");

                table_builder.Append("<td>");
                table_builder.Append(route_change.Remarks);
                table_builder.Append("</td>");

                table_builder.Append("</tr>");
            }
            table_builder.Append("</table>");

            return table_builder.ToString();
        }

        public string UploadExcelData()
        {
            StringBuilder sb = new StringBuilder();
            List<string[]> data = new List<string[]>();
            string res = "0";

            var inventory_list = new List<page_param.inventoryCount.inventoryCountdetails>();

            HttpFileCollectionBase File = Request.Files;
            string[] file_extensions = { ".xlsx", ".xls" };
            string upload_file_extension = Path.GetExtension(File[0].FileName.ToLower());

            try
            {
                if (Array.IndexOf(file_extensions, upload_file_extension) > -1)
                {
                    string new_tmp_directory = Server.MapPath("..\\UPLOAD_TEMPFOLDER");
                    string new_file_name = Path.GetFileName(File[0].FileName);
                    string str_full_filename = new_tmp_directory + "\\" + new_file_name;

                    Directory.CreateDirectory(new_tmp_directory);
                    File[0].SaveAs(str_full_filename);

                    #region QUERY

                    DataTable qry = ExcelReader.getExclData12(
                        @" SELECT 
                               BRAND,PROD_GRP,ITEM_CODE,ITEM_DESCRIPTION,IDEAL_INV,BEG_INV,NETSELLIN,END_INV,ACT_SELLOUT_PCS,ACT_SELLOUT_AMT,FORECAST0_PCS,FORECAST0_AMT,FORECAST1_PCS,FORECAST1_AMT,FORECAST2_PCS,FORECAST2_AMT,FORECAST3_PCS,FORECAST3_AMT,REMARKS
                            FROM [Sheet1$]
                            WHERE [BRAND] IS NOT NULL 
                            AND [PROD_GRP] IS NOT NULL AND [ITEM_CODE] IS NOT NULL AND [ITEM_DESCRIPTION] IS NOT NULL 
                            AND [IDEAL_INV] IS NOT NULL AND [BEG_INV] IS NOT NULL AND [NETSELLIN] IS NOT NULL AND [END_INV] IS NOT NULL
                            AND [ACT_SELLOUT_PCS] IS NOT NULL AND [ACT_SELLOUT_AMT] IS NOT NULL AND [FORECAST0_PCS] IS NOT NULL
                            AND [FORECAST0_AMT] IS NOT NULL AND [FORECAST1_PCS] IS NOT NULL AND [FORECAST1_AMT] IS NOT NULL 
                            AND [FORECAST2_PCS] IS NOT NULL AND [FORECAST2_AMT] IS NOT NULL AND [FORECAST3_PCS] IS NOT NULL
                            AND [FORECAST3_AMT] IS NOT NULL AND [REMARKS] IS NOT NULL
                        ",
                        str_full_filename);

                    #endregion

                    foreach (DataRow itm in qry.Rows)
                    {
                        data.Add(new string[] { 
                            itm["BRAND"].ToString(),
                            itm["PROD_GRP"].ToString(),
                            itm["ITEM_CODE"].ToString(),
                            itm["ITEM_CODE"].ToString(),
                            itm["ITEM_DESCRIPTION"].ToString(),
                            itm["IDEAL_INV"].ToString(),
                            itm["BEG_INV"].ToString(),
                            itm["NETSELLIN"].ToString(),
                            itm["END_INV"].ToString(),
                            itm["ACT_SELLOUT_PCS"].ToString(),
                            itm["ACT_SELLOUT_AMT"].ToString(),
                            itm["FORECAST0_PCS"].ToString(),
                            itm["FORECAST0_AMT"].ToString(),
                            itm["FORECAST1_PCS"].ToString(),
                            itm["FORECAST1_AMT"].ToString(),
                            itm["FORECAST2_PCS"].ToString(),
                            itm["FORECAST2_AMT"].ToString(),
                            itm["FORECAST3_PCS"].ToString(),
                            itm["FORECAST3_AMT"].ToString(),
                            itm["REMARKS"].ToString()
                        });

                        inventory_list.Add(new page_param.inventoryCount.inventoryCountdetails()
                        {
                            lineId = (int?)itm["BRAND"],
                            itemCode = itm["ITEM_CODE"].ToString(),
                            ssr = itm["IDEAL_INV"].ToString(),
                            begNvPcs = (int?)itm["BEG_INV"],
                            sellIn = (int?)itm["NETSELLIN"],
                            endNvPcs = (int?)itm["END_INV"],
                            actualSellOutAmt = (int?)itm["ACT_SELLOUT_PCS"],
                            actualSellOutPcs = (int?)itm["ACT_SELLOUT_AMT"],
                            forecastFTMamt0 = (int?)itm["FORECAST0_PCS"],
                            forecastFTMpcs0 = (int?)itm["FORECAST0_AMT"],
                            forecastFTMamt1 = (int?)itm["FORECAST1_PCS"],
                            forecastFTMpcs1 = (int?)itm["FORECAST1_AMT"],
                            forecastFTMamt2 = (int?)itm["FORECAST2_PCS"],
                            forecastFTMpcs2 = (int?)itm["FORECAST2_AMT"],
                            forecastFTMamt3 = (int?)itm["FORECAST3_PCS"],
                            forecastFTMpcs3 = (int?)itm["FORECAST1_AMT"],
                            remarks = itm["REMARKS"].ToString(),
                            brand = itm["BRAND"].ToString(),
                            prodGrp = itm["PROD_GRP"].ToString(),
                            itemDesc = itm["ITEM_DESCRIPTION"].ToString()
                        });

                        //sb.Append("<tr clone=\"true\">");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["BRAND"].ToString() + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["BRAND"].ToString() + "\" style=\"width:60px; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["PROD_GRP"].ToString() + "\" style=\"width:70px; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["ITEM_CODE"].ToString() + "\" style=\"width:120px; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["ITEM_DESCRIPTION"].ToString() + "\" style=\"width:250px; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["IDEAL_INV"].ToString() + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["BEG_INV"].ToString() + "\" style=\"width:60px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["NETSELLIN"].ToString() + "\" style=\"width:60px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["END_INV"].ToString() + "\" style=\"width:60px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["ACT_SELLOUT_PCS"].ToString() + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["ACT_SELLOUT_AMT"].ToString() + "\" style=\"width:80px;background-color:#efefef; text-align:right\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["FORECAST0_PCS"].ToString() + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["FORECAST0_AMT"].ToString() + "\" style=\"width:80px; background-color:#efefef; text-align:right\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["FORECAST1_PCS"].ToString() + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["FORECAST1_AMT"].ToString() + "\" style=\"width:80px; background-color:#efefef; text-align:right\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["FORECAST2_PCS"].ToString() + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["FORECAST2_AMT"].ToString() + "\" style=\"width:80px;background-color:#efefef; text-align:right\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["FORECAST3_PCS"].ToString() + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["FORECAST3_AMT"].ToString() + "\" style=\"width:80px;  background-color:#efefef; text-align:right\" readonly=\"readonly\" /></td>");
                        //sb.Append("<td><input type=\"text\" value=\"" + itm["REMARKS"].ToString() + "\" style=\"background-color:#efefef\" readonly=\"readonly\" /></td>");
                        //sb.Append("</tr>");
                    }

                    //var duplicate = from a in data
                    //                group a by a into grouped
                    //                where grouped.Count() > 1
                    //                select grouped.Key;

                    //if (duplicate.Count() > 1)
                    //    throw new Exception("Duplicate Item");

                    res = JsonConvert.SerializeObject(inventory_list);
                   // res = JsonConvert.SerializeObject(sb);
                }
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        


        private decimal getVarianceAmount(decimal amount1, decimal amount2)
        {
            return amount1 - amount2;
        }
        private int getVariancePcs(int pcs1, int pcs2)
        {
            return pcs1 - pcs2;
        }

    }
}
