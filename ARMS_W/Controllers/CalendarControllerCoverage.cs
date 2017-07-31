using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARMS_W.Class;
using ARMS_W.GLOBALS;
using ARMS_W.SkelClass;
using ARMS_W.Objects;
using ARMS_W.Models;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Text;

using System.Text.RegularExpressions;
using Newtonsoft.Json;



namespace ARMS_W.Controllers
{

    public partial class CalendarController : Controller
    {
        

        #region SAVE COVERAGE PLAN
        [HttpPost]
        public JsonResult SaveCoveragePlan(page_param.CoverageHdr p_param)
        {
            SQLTransaction sql_trans = new SQLTransaction();
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            string obj_code = "";
            bool isExist = false;
            string qry_evnt_id = "";
            int DoctypeId;
            int DocstatusId = 0;

            Globals.DocActionType act_type = Globals.DocActionType.Approve;
            //C.DocActionType act_type = LoyaltyLib.DocActionType.Approve;
            if (p_param.action_type == "SaveAndSend")
                act_type = Globals.DocActionType.SaveAndSend;
            else if (p_param.action_type == "APPROVE")
                act_type = Globals.DocActionType.Approve;
            else if (p_param.action_type == "DISAPPROVE")
                act_type = Globals.DocActionType.Disapprove;
            else if (p_param.action_type == "RETURN_TO_REQUESTOR")
                act_type = Globals.DocActionType.ReturnToRequestor;
            else if (p_param.action_type == "" || p_param.action_type == null)
                act_type = Globals.DocActionType.None;

            try 
            {
                sql_trans.StartTransaction();

                var current_user = new _User(Session["username"].ToString());

                ARMS_W.Objects.Event.CoverageHdr new_coverage = new Objects.Event.CoverageHdr();
                DoctypeId = (int)ARMS_W.SkelClass.Globals.InfoType.CalendarEvent;
                isExist = ARMS_W.UserDefineFunctions.CalendarEvent.SoCalendar.IsExists(p_param.EmpIdNo, p_param.Year.ToString(), p_param.Month);
                new_coverage.isExist = isExist;
                if (isExist == true)
                { qry_evnt_id = DATABASE.CoverageHdrs.Single(p => p.EmpIdNo == p_param.EmpIdNo && p.Year == p_param.Year && p.Month == p_param.Month).EventID; }
                else { qry_evnt_id = null; }



                new_coverage.EventID = qry_evnt_id;
                new_coverage.EmpIdNo = p_param.EmpIdNo;
                new_coverage.Day = p_param.Day;
                new_coverage.Month = p_param.Month;
                new_coverage.Year = p_param.Year;
                new_coverage.DoctypeId = DoctypeId;
                new_coverage.DocumentStatusId = DocstatusId;
                new_coverage.AccountCode = p_param.AccountCode;
                new_coverage.ContactPerson = p_param.ContactPerson;
                new_coverage.ContactPersonNo = p_param.ContactPersonNo;
                new_coverage.StoreChecking = p_param.StoreChecking;
                new_coverage.IssuesAndConcerns = p_param.IssuesAndConcerns;
                new_coverage.HotelName = p_param.HotelName;
                new_coverage.HotelContactNum = p_param.HotelContactNum;
                new_coverage.Attachment = p_param.Attachment;
                new_coverage.ColPostDatedCheck = p_param.ColPostDatedCheck;
                new_coverage.ColDatedCheck = p_param.ColDatedCheck;
                new_coverage.ColTotal = p_param.ColTotal;
                new_coverage.ColRemarks = p_param.ColRemarks;


                if (p_param.collection_list != null)
                {
                    foreach (var itm in p_param.collection_list)
                    {
                        try
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(p => p.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex)
                        { 
                        
                        }


                        new_coverage.collection_list.Add(new ARMS_W.Objects.Event.collections() {

                            ObjectiveCode = obj_code,
                           Brand = itm.Brand,
                           Amount = itm.Amount
                        
                        });

                        
                    }
                
                }

                if (p_param.merchandising_list != null)
                {
                    foreach (var itm in p_param.merchandising_list)
                    {
                        try
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(p => p.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex)
                        {

                        }

                        new_coverage.merchandising_list.Add(new ARMS_W.Objects.Event.merchandising() {

                            ObjectiveCode = obj_code,
                            Brand = itm.Brand,
                            Amount = itm.Amount,
                            Productpresented = itm.Productpresented,
                            CounterClerkNo = itm.CounterClerkNo,
                            counterclerk = itm.counterclerk
                        
                        });

                    }
 
                
                }

                if (p_param.sales_list != null)
                {
                    foreach (var itm in p_param.sales_list)
                    {
                        try
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(p => p.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex)
                        {

                        }
                        new_coverage.sales_list.Add(new ARMS_W.Objects.Event.sales()
                        {

                            ObjectiveCode = obj_code,
                            Brand = itm.Brand,
                            Amount = itm.Amount,
                            dtlsRrmks=itm.dtlsRrmks

                        });
                    
                    }
                
                }

                if (p_param.customersrv_list != null)
                {
                    foreach (var itm in p_param.customersrv_list)
                    {
                        try
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(p => p.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex)
                        {

                        }

                        new_coverage.customersrv_list.Add(new ARMS_W.Objects.Event.customersrvc()
                        {

                            ObjectiveCode = obj_code,
                            Brand = itm.Brand,
                            Amount = itm.Amount

                        });

                    }


                }

                if (act_type == Globals.DocActionType.None)
                {

                    new_coverage.UpdateToDBCoverage(ref sql_trans);
                    

                }

                if (act_type == Globals.DocActionType.Approve || act_type == Globals.DocActionType.SaveAndSend)
                {



                    new_coverage.UpdateDocStatus(ref sql_trans, p_param.EventID, act_type.ToString(), current_user.UserName, isExist, p_param.ApprvrRrmks);
                    new_coverage.insertRouteRmrks(ref sql_trans, p_param.EventID, p_param.ApprvrRrmks);
                }

                if (act_type == Globals.DocActionType.ReturnToRequestor)
                {



                    new_coverage.UpdateDocStatus(ref sql_trans, p_param.EventID, act_type.ToString(), current_user.UserName, isExist, p_param.ApprvrRrmks);
                    new_coverage.insertRouteRmrks(ref sql_trans, p_param.EventID, p_param.ApprvrRrmks);
                }

                sql_trans.Committransaction();
                return Json(SActionResult.Success);

            
            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                return Json(SActionResult.Error + ex.Message);

            }


        }


        #region GET COVERAGE PLAN BY DATE

        [HttpPost]
        public JsonResult GetCoverageInfobyDate(string Eventmonth, int Eventday, int Eventyear, string soId)
        {
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;

            try
            {
                ajx_result.iserror = false;
                var coveragebydate = UserDefineFunctions.CalendarEvent.SoCalendar.GetCoverageInfo(Eventmonth, Eventday, Eventyear, soId);
                
                ajx_result.data = new
                {
                    coverage = coveragebydate
                };
            }

            catch (Exception ex)
            {

                ajx_result.iserror = true;
                ajx_result.message = ex.Message;
            }
            return Json(ajx_result);
        }

        //an over load method taken from original implementation above
        [HttpPost]
        public JsonResult GetCoverageInfo(string EventId, string Eventmonth, int Eventday, int Eventyear, string soId, string accoutcode)
        {
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;
            var DATABASE = new Models.ARMSTestEntities();
            string LineNum=null;
            int getNumberOfVisits=0;
            try {
                EventId = EventId == "AUTOLOOKUP" ? DATABASE.CoverageHdrs.Single(o => o.Year == Eventyear && o.Month == Eventmonth && o.EmpIdNo == soId).EventID : EventId;

                LineNum = DATABASE.CoverageDtls.Single(p => p.EventID == EventId && p.Day == Eventday && p.AccountCode == accoutcode).LineNum;
                getNumberOfVisits = DATABASE.CoverageHdrs.Where(o => o.Month == Eventmonth && o.Year == Eventyear && o.EmpIdNo == soId).Single().CoverageDtls.Where(o => o.AccountCode == accoutcode && o.IsDeleted != "T" && o.hasCallreport == "T").Select(o => o.Numvisit).Count() + 1;
                DATABASE.Dispose();
            }
            catch (Exception ex)
            {
                LineNum = null;
                DATABASE.Dispose();
            }

            try
            {

                ajx_result.iserror = false;
                //var next_inventory_count = GetNextInventoryCountGetInventoryperSo(int.Parse(Eventmonth), Eventyear, soId);
                DateTime? coverage_date = new DateTime(Eventyear, int.Parse(Eventmonth), Eventday, 0, 0, 0).Date;
                var coveragebydate = UserDefineFunctions.Application.GetCoverageInfo(EventId, Eventmonth,  Eventday,  Eventyear,  soId, accoutcode);

                //var qry = UserDefineFunctions.Application.getCoverageCallReportAccount(Eventmonth, Eventday, Eventyear, soId, acctCode);
                var collection_objective_lookupfromsap = UserDefineFunctions.Application.BalanceLookUpFromSAP(accoutcode);
                var total_amount_collection = collection_objective_lookupfromsap.Sum(o => o.balance);
                

                ajx_result.data = new
                {
                    coverage = coveragebydate,
                    inventory_objective = UserDefineFunctions.Application.getInventoryCountObjective(coverage_date,accoutcode,soId),
                    routechanges =this.getRoutechangesbyAccount(LineNum) ,
                    collection_objective_lookupfromsap = collection_objective_lookupfromsap,
                    total_collection_objective_lookupfromsap = total_amount_collection,
                    number_of_visits = getNumberOfVisits
                   // hasNextInventoryCount = UserDefineFunctions.Application.hasNextInventoryCount(coverage_date, accoutcode, soId),
                   // inventorycountId = UserDefineFunctions.Application.getInventoryCount(coverage_date,accoutcode,soId)
                };
            }

            catch (Exception ex)
            {

                ajx_result.iserror = true;
                ajx_result.message = ex.Message;
            }
            return Json(ajx_result);
        }

        #endregion

        public JsonResult GetCoverageInfobyDates(string Eventmonth, int Eventday, int Eventyear, string soId)
        {
            string Eventid;
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> Events = ARMS_W.UserDefineFunctions.Application.GetCoverageInfo(Eventmonth, Eventday, Eventyear, soId);

            return Json(Events);
        }


        #endregion


        #region DELETE COVERAGE PLAN
        [HttpPost]
        public JsonResult DELETECoveragePlan(page_param.CoverageHdr p_param)
        {
            SQLTransaction sql_trans = new SQLTransaction();
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            int DoctypeId;
            int DocstatusId = 0;

            Globals.DocActionType act_type = Globals.DocActionType.Approve;
            //C.DocActionType act_type = LoyaltyLib.DocActionType.Approve;
            if (p_param.action_type == "DELETE")
                act_type = Globals.DocActionType.Delete;
           

            try
            {
                sql_trans.StartTransaction();
                ARMS_W.Objects.Event.CoverageHdr new_coverage = new Objects.Event.CoverageHdr();
                DoctypeId = (int)ARMS_W.SkelClass.Globals.InfoType.CalendarEvent;

               // p_param.LineNum = DATABASE.CoverageHdrs.Single(o => o.EmpIdNo == p_param.EmpIdNo && o.Month == p_param.Month && o.Year == p_param.Year).CoverageDtls.Single(o => o.Day == p_param.Day).LineNum;

                var qry = (from a in DATABASE.CoverageHdrs
                           from b in DATABASE.CoverageDtls
                           where a.EmpIdNo == p_param.EmpIdNo && a.Year == p_param.Year && a.Month == p_param.Month
                            && a.EventID == b.EventID && b.Day == p_param.Day && p_param.AccountCode == b.AccountCode
                           select new { a.EventID, b.LineNum }).Single();

                p_param.EventID = qry.EventID;
                p_param.LineNum = qry.LineNum;

                new_coverage.EventID = p_param.EventID;
                new_coverage.EmpIdNo = p_param.EmpIdNo;
                new_coverage.Day = p_param.Day;
                new_coverage.Month = p_param.Month;
                new_coverage.Year = p_param.Year;
                new_coverage.DoctypeId = DoctypeId;
                new_coverage.AccountCode = p_param.AccountCode;
                new_coverage.LineNum = p_param.LineNum;





                if (act_type == Globals.DocActionType.Delete)
                {

                    new_coverage.UpdateDeletion(ref sql_trans);

                }


                sql_trans.Committransaction();
                return Json(SActionResult.Success);


            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                return Json(SActionResult.Error + ex.Message);

            }


        }


        #region GET COVERAGE PLAN BY DATE

       /** [HttpPost]
        public JsonResult GetCoverageInfobyDate(string Eventmonth, int Eventday, int Eventyear, string soId)
        {
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;

            try
            {
                ajx_result.iserror = false;
                var coveragebydate = UserDefineFunctions.CalendarEvent.SoCalendar.GetCoverageInfo(Eventmonth, Eventday, Eventyear, soId);

                ajx_result.data = new
                {
                    coverage = coveragebydate

                };
            }

            catch (Exception ex)
            {

                ajx_result.iserror = true;
                ajx_result.message = ex.Message;
            }
            return Json(ajx_result);
        }**/

        //an over load method taken from original implementation above
      /**  [HttpPost]
        public JsonResult GetCoverageInfo(string EventId, string Eventmonth, int Eventday, int Eventyear, string soId, string accoutcode)
        {
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;

            try
            {
                ajx_result.iserror = false;
                var coveragebydate = UserDefineFunctions.Application.GetCoverageInfo(EventId, Eventmonth, Eventday, Eventyear, soId, accoutcode);

                ajx_result.data = new
                {
                    coverage = coveragebydate

                };
            }

            catch (Exception ex)
            {

                ajx_result.iserror = true;
                ajx_result.message = ex.Message;
            }
            return Json(ajx_result);
        }**/

        #endregion

       /** public JsonResult GetCoverageInfobyDates(string Eventmonth, int Eventday, int Eventyear, string soId)
        {
            string Eventid;
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> Events = ARMS_W.UserDefineFunctions.Application.GetCoverageInfo(Eventmonth, Eventday, Eventyear, soId);

            return Json(Events);
        }*/


        #endregion


        public JsonResult GetSOAccountCode(string Eventmonth, int Eventday, int Eventyear, string soId, string isplanned)
        {

            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> Accounts = ARMS_W.UserDefineFunctions.Application.GetSOAccountCode(soId,Eventmonth, Eventday, Eventyear);
            
            return Json(Accounts);
        
        }

        public JsonResult GetSOAccountCode2(string Eventmonth, int Eventday, int Eventyear, string soId, string isplanned)
        {

            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            List<string[]> Accounts = ARMS_W.UserDefineFunctions.Application.GetSOAccountCode2(soId, Eventmonth, Eventday, Eventyear);

            return Json(Accounts);

        }




        #region GET EVENTS BY DATE

        [HttpPost]
        // [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetCoverageByDate(string userId, string month, int year, string viewtype, int day = 1)
        {
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;

            try
            {
                ajx_result.iserror = false;
                //temporary commentout
                //  var events = UserDefineFunctions.CalendarEvent.SoCalendar.GetCoverageInfo(userId, month, year,viewtype);
                var events = UserDefineFunctions.CalendarEvent.SoCalendar.GetAllDayEvents(userId, month, year, viewtype,day);
                var doc_stat = UserDefineFunctions.CalendarEvent.SoCalendar.GetCoverageDocumentStatus(userId, month, year);
                var Eventid = UserDefineFunctions.CalendarEvent.SoCalendar.getCoverageId(userId, month, year);
                var nfalse = UserDefineFunctions.CalendarEvent.SoCalendar.GetforComplainceFalse(Eventid, userId, month, year);
                var ntrue = UserDefineFunctions.CalendarEvent.SoCalendar.GetforComplainceTrue(Eventid, userId, month, year);
                var allplanned = UserDefineFunctions.CalendarEvent.SoCalendar.Getallforcallreport(Eventid, userId, month, year);
                var remarks = "";
                ajx_result.data = new
                {
                    info = events,
                    docstatus = doc_stat,
                    EventId = Eventid,
                    nfalse = nfalse,
                    ntrue = ntrue,
                    allplanned = allplanned,
                };

            }
            catch (Exception ex)
            {

                ajx_result.iserror = true;
                ajx_result.message = ex.Message;
            }
            return Json(ajx_result);

        }
        #endregion
       [HttpPost]
       public JsonResult GetAllDayEvents(string userId, string month, int year, int day)
       {
           AjxResult ajx_res = new AjxResult();
           ajx_res.iserror = true;

           try
           {
               ajx_res.data = new
               {
                   AllDayEvents = UserDefineFunctions.CalendarEvent.SoCalendar.GetAllDayEvents(userId, month, year,day),
                   docstatus = UserDefineFunctions.CalendarEvent.SoCalendar.GetCoverageDocumentStatus(userId, month, year)
               };

               ajx_res.iserror = false;
           }
           catch (Exception ex)
           {
               ajx_res.iserror = true;
               ajx_res.message = ex.Message;
           }

           return Json(ajx_res);
       }


        public JsonResult GetListofSO(string ASMID)
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> so = ARMS_W.UserDefineFunctions.Application.GetListofSOEventperASM(ASMID);


            return Json(so);
        }


        public JsonResult GetChangesForApprovalbySO(string ASMID)
        {
            string[] monthname = { "", "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };

            AjxResult ajx_res = new AjxResult();
            ajx_res.iserror = true;



            string username = Session["username"].ToString();
       //     Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> so = ARMS_W.UserDefineFunctions.Application.GetListofCoveragechangesbySO(ASMID,username);


            return Json(so);
        
        }

        public JsonResult getListOfEvents(string asm_id,string filter_by)
        {
            string username = Session["username"].ToString();

            List<string[]> list = ARMS_W.UserDefineFunctions.Application.GetListofSOEventperASM(asm_id, filter_by,username);
            return Json(list);
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetEventList(string COMMAND, string PAGENO,string status, string SearchType = null, string DocStatusDescription = null, int rec_per_page = 0, string asm_id = null, string keyword=null)
        {
            string[] monthname = { "", "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };

            AjxResult ajx_res = new AjxResult();
            ajx_res.iserror = true;

            int total_records = 0,
                int_total_pages = 0,
                start_rec = 0;

            decimal flt_total_pages = 0;
            List<string[]> list = new List<string[]>();
            List<string[]> new_list = new List<string[]>();

            try
            {
                string username = Session["username"].ToString();

                //GET LIST OF EVENTS
                if (status == "CHANGES_FOR_APPROVAL")
                    new_list = ARMS_W.UserDefineFunctions.Application.GetListofCoveragechangesbySO(asm_id, username);
                else
                    new_list = ARMS_W.UserDefineFunctions.Application.GetListofSOEventperASM(asm_id, status, username);

                new_list = new_list.Select(o => new string[]{
                    o[0], 
                    o[1],
                    o[2],
                    monthname[Convert.ToInt32(o[3])],
                    o[4],
                    o[5]
                }).ToList();

                List<string[]> tmp_res = new List<string[]>();

                if (keyword != null)
                {
                    var searched_list = ARMS_W.UserDefineFunctions.Application.searhAll(new_list, keyword);
                    new_list = searched_list;
                }

                total_records = new_list.Count();

                flt_total_pages = Convert.ToDecimal(total_records) / Convert.ToDecimal(rec_per_page);
                int_total_pages = (int)Math.Ceiling(flt_total_pages);

                PAGENO = rec_per_page >= total_records ? "1" : PAGENO;

                start_rec = (Convert.ToInt32(PAGENO) * rec_per_page) - rec_per_page;

                tmp_res = new_list.Skip(start_rec).Take(rec_per_page).ToList();

                ajx_res.data = new
                {
                    list = tmp_res,
                    total_records = total_records,
                    int_total_pages = int_total_pages,
                    current_page_no = Convert.ToInt32(PAGENO)
                };

                ajx_res.iserror = false;
            }
            catch (Exception ex)
            {
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
            }

            return Json(ajx_res);
        }


        [HttpPost]
        public JsonResult SaveCoveragePlanChanges(page_param.CoverageHdr p_param)
        {
            SQLTransaction sql_trans = new SQLTransaction();
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            string obj_code = "";
            bool isExist = false;
            string qry_evnt_id = "";
            int DoctypeId;
            int DocstatusId = 0;

            Globals.DocActionType act_type = Globals.DocActionType.Approve;
            //C.DocActionType act_type = LoyaltyLib.DocActionType.Approve;
            if (p_param.action_type == "APPROVE")
                act_type = Globals.DocActionType.Approve;
            else if (p_param.action_type == "DISAPPROVE")
                act_type = Globals.DocActionType.Disapprove;
            else if (p_param.action_type == "RETURN_TO_REQUESTOR")
                act_type = Globals.DocActionType.ReturnToRequestor;
            else if (p_param.action_type == "" || p_param.action_type == null)
                act_type = Globals.DocActionType.None;

            try
            {
                sql_trans.StartTransaction();
                var current_user = new _User(Session["username"].ToString());
                ARMS_W.Objects.Event.CoverageHdr new_coverage = new Objects.Event.CoverageHdr();
                DoctypeId = (int)ARMS_W.SkelClass.Globals.InfoType.CalendarEvent;
                isExist = ARMS_W.UserDefineFunctions.CalendarEvent.SoCalendar.IsExists(p_param.EmpIdNo, p_param.Year.ToString(), p_param.Month);
                new_coverage.isExist = isExist;
                if (isExist == true)
                { 
                    qry_evnt_id = DATABASE.CoverageHdrs.Single(p => p.EmpIdNo == p_param.EmpIdNo && p.Year == p_param.Year && p.Month == p_param.Month).EventID;
                    DocstatusId = (int)DATABASE.CoverageHdrs.Single(p => p.EmpIdNo == p_param.EmpIdNo && p.Year == p_param.Year && p.Month == p_param.Month).DocumentStatusId;
                }
                else 
                { 
                    qry_evnt_id = null; 
                }



                new_coverage.EventID = qry_evnt_id;
                new_coverage.EmpIdNo = p_param.EmpIdNo;
                new_coverage.Day = p_param.Day;
                new_coverage.Month = p_param.Month;
                new_coverage.Year = p_param.Year;
                new_coverage.DoctypeId = DoctypeId;
                new_coverage.DocumentStatusId = DocstatusId;
                new_coverage.AccountCode = p_param.AccountCode;
                new_coverage.ContactPerson = p_param.ContactPerson;
                new_coverage.ContactPersonNo = p_param.ContactPersonNo;
                new_coverage.StoreChecking = p_param.StoreChecking;
                new_coverage.IssuesAndConcerns = p_param.IssuesAndConcerns;
                new_coverage.HotelName = p_param.HotelName;
                new_coverage.HotelContactNum = p_param.HotelContactNum;
                new_coverage.isDeleted = p_param.isDeleted;
                new_coverage.Attachment = p_param.Attachment;
                new_coverage.ColPostDatedCheck = p_param.ColPostDatedCheck;
                new_coverage.ColDatedCheck = p_param.ColDatedCheck;
                new_coverage.ColTotal = p_param.ColTotal;
                new_coverage.ColRemarks = p_param.ColRemarks;



                if (p_param.collection_list != null)
                {
                    foreach (var itm in p_param.collection_list)
                    {
                        try
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(p => p.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex)
                        {

                        }

                        new_coverage.collection_list.Add(new Objects.Event.collections()
                        {
                            ObjectiveCode = obj_code,
                            Amount = itm.Amount
                        });

                    }

                }

                if (p_param.merchandising_list != null)
                {
                    foreach (var itm in p_param.merchandising_list)
                    {
                        try
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(p => p.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex)
                        {

                        }

                        new_coverage.merchandising_list.Add(new ARMS_W.Objects.Event.merchandising()
                        {

                            ObjectiveCode = obj_code,
                            Brand = itm.Brand,
                            Amount = itm.Amount,
                            Productpresented = itm.Productpresented,
                            CounterClerkNo = itm.CounterClerkNo,
                            counterclerk = itm.counterclerk

                        });

                    }


                }

                if (p_param.sales_list != null)
                {
                    foreach (var itm in p_param.sales_list)
                    {
                        try
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(p => p.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex)
                        {

                        }
                        new_coverage.sales_list.Add(new ARMS_W.Objects.Event.sales()
                        {

                            ObjectiveCode = obj_code,
                            Brand = itm.Brand,
                            Amount = itm.Amount,
                            dtlsRrmks = itm.dtlsRrmks

                        });

                    }

                }

                if (p_param.customersrv_list != null)
                {
                    foreach (var itm in p_param.customersrv_list)
                    {
                        try
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(p => p.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex)
                        {

                        }

                        new_coverage.customersrv_list.Add(new ARMS_W.Objects.Event.customersrvc()
                        {

                            ObjectiveCode = obj_code,
                            Brand = itm.Brand,
                            Amount = itm.Amount

                        });

                    }


                }

                if (act_type == Globals.DocActionType.None)
                {

                    new_coverage.UpdateToDBCoverageChanges(ref sql_trans,current_user.UserName);

                }

                if (act_type == Globals.DocActionType.Approve)
                {



                    new_coverage.UpdateDocStatus(ref sql_trans, p_param.EventID, act_type.ToString(), current_user.UserName, isExist, p_param.Remarks);
                }

                if (act_type == Globals.DocActionType.ReturnToRequestor)
                {



                    new_coverage.UpdateDocStatus(ref sql_trans, p_param.EventID, act_type.ToString(),current_user.UserName,isExist,p_param.Remarks);
                }

                sql_trans.Committransaction();
                return Json(SActionResult.Success);


            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                return Json(SActionResult.Error + ex.Message);

            }


        }


        public JsonResult getstatus(string soId,string Eventmonth, int Eventyear)
        {
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;

            try
            {
                ajx_result.iserror = false;
               // int roleid = UserDefineFunctions.Application.getRole(HttpContext.Session["username"].ToString());
                var coveragebydate = UserDefineFunctions.Application.getstatus(soId, Eventmonth, Eventyear);//,roleid);

                ajx_result.data = new
                {
                    coveragestatus = coveragebydate

                };
            }

            catch (Exception ex)
            {

                ajx_result.iserror = true;
                ajx_result.message = ex.Message;
            }
            return Json(ajx_result);
        }


        public JsonResult getEventId(string soId, string Eventmonth, int Eventyear)
        {
            string EventId = ARMS_W.UserDefineFunctions.Application.getEventId(soId, Eventmonth, Eventyear);

            return Json(EventId);
        
        }


        [HttpPost]
        public JsonResult GetChangesCoveragebySo(string soId, string month, int year, string EventId)
        {
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;

            try
            {
                ajx_result.iserror = false;
                var changes = UserDefineFunctions.Application.GetChangesDtlsbySO(soId, month, year, EventId);
                ajx_result.data = new
                {
                    coverage = changes
                };




            }
            catch (Exception ex)
            {

                ajx_result.iserror = true;
                ajx_result.message = ex.Message;
            }
            return Json(ajx_result);

        }


        #region ATTACHMENT

        //[HttpPost]
        //public string UploadAfcndoFileAttachment(string tempfolder)
        //{
        //    string res = "0";

        //    HttpFileCollectionBase File = Request.Files;

        //    string[] file_extensions = { ".pdf", ".xls", ".xlsx" };

        //    try
        //    {
        //        if (Array.IndexOf(file_extensions, Path.GetExtension(File[0].FileName.ToLower())) > -1)
        //        {
        //            string new_tmp_directory =   .Application.Afficionado.AfcndoUploadTempDirectory + tempfolder + "\\";

        //            string new_file_name = Path.GetFileName(File[0].FileName);

        //            Directory.CreateDirectory(new_tmp_directory);

        //            File[0].SaveAs(new_tmp_directory + new_file_name);

        //        }
        //        else
        //        {
        //            res = "1";
        //        }

        //        #region DELAYER
        //        string temp = "";

        //        for (int i = 0; i < 10000; i++)
        //        {
        //            temp = temp + Convert.ToString(i);
        //        }
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        res = "1";
        //    }

        //    return res;
        //}


        [HttpPost]

        public string UploadFileAttachment(string tempfolder)
        {
            string res="0";

            HttpFileCollectionBase File = Request.Files;
            string[] file_extensions = { ".pdf", ".xls", ".xlsx",".doc",".docx", ".PDF", ".XLS", ".XSLX",".DOC",".DOCX",
                                       ".gif", ".jpg", ".bmp", ".png", ".jpeg", ".GIF", ".JPG", ".BMP", ".PNG", ".JPEG"};

            try {

                if (Array.IndexOf(file_extensions, Path.GetExtension(File[0].FileName.ToLower())) > -1)
                {

                    string new_tmp_directory = ARMS_W.UserDefineFunctions.Application.UploadTempDirectory + tempfolder + "\\";
                    string new_file_name = Path.GetFileName(File[0].FileName);
                    Directory.CreateDirectory(new_tmp_directory);
                    File[0].SaveAs(new_tmp_directory + new_file_name);
                }

                else 
                {

                    res = "1";

                }

                #region DELAYER

                string temp = "";

                for (int i = 0; i < 10000; i++)
                {
                    temp = temp + Convert.ToString(i);
                
                }

                #endregion

            }
            catch(Exception ex)
            {

                res = "1";
            }


            return res;
        
        }


        public ActionResult DownLoadFile(string filename, int DoctypeId)
        {
            string str_path = "";
            //switch ((Globals.InfoType)DocTypeId)
            switch ((Globals.InfoType)DoctypeId)
            {
                case Globals.InfoType.CalendarEvent:
                    str_path = UserDefineFunctions.Application.UploadTempDirectory
                        //+ docid + "\\" + filename;
                         + "TESTING\\" +  filename;
                        // + "TESTING\\" + docid + "\\" + filename;
                    break;
             
            }

            if (System.IO.File.Exists(str_path))
            {
                string ext_file = System.IO.Path.GetExtension(filename).Replace(".", "");

                string MimeType = File(str_path.Replace("%26", "&"), "application/" + ext_file, filename).ContentType;
                return File(str_path.Replace("%26", "&"), MimeType, filename);
            }
            else
            {
                return View();
            }

        }

        #endregion



        //[HttpPost]
        //public JsonResult GetChangesCoveragebySo(string soId, string month, int year, string EventId)
        //{
        //    SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
        //    ajx_result.iserror = true;

        //    try
        //    {
        //        ajx_result.iserror = false;
        //        var changes = UserDefineFunctions.Application.GetChangesDtlsbySO(soId, month, year, EventId);
        //        ajx_result.data = new
        //        {
        //            coverage = changes
        //        };




        //    }
        //    catch (Exception ex)
        //    {

        //        ajx_result.iserror = true;
        //        ajx_result.message = ex.Message;
        //    }
        //    return Json(ajx_result);

        //}



        public JsonResult GetInventoryperSo(int month, int year, string soId)
        { 
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> so = UserDefineFunctions.Application.GetInventoryperSo(month, year, soId);
            return Json(so);
        }



        [HttpPost]
        public JsonResult GetNextInventoryCountGetInventoryperSo(int month, int year, string soId)
        {
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;

            try
            {
                ajx_result.iserror = false;
                var nextcountdate = UserDefineFunctions.Application.GetNextScheduleInventoryCount(month, year, soId);
                ajx_result.data = new
                {
                    InvCoverage = nextcountdate
                };




            }
            catch (Exception ex)
            {

                ajx_result.iserror = true;
                ajx_result.message = ex.Message;
            }
            return Json(ajx_result);

        }


        public JsonResult SaveScheduledinventory(int month, int year, string soId )
        {
            SQLTransaction sql_trans = new SQLTransaction();
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ARMS_W.Objects.Event.CoverageHdr new_coverage = new Objects.Event.CoverageHdr();
            try
            {
                sql_trans.StartTransaction();
                var nextcountdate = UserDefineFunctions.Application.GetNextScheduleInventoryCount(month, year, soId);
                Int32 begginibg_linenum = Convert.ToInt32(this.GetLineNumCode().Replace("SEQ", ""));


                foreach (var itm in nextcountdate)
                {
                    //new_coverage.AccountCode = itm.acctCode;
                    //new_coverage.ContactPerson = itm.Contactperson;
                    //new_coverage.ContactPersonNo = itm.ContactpersonNo;
                    //new_coverage.Day = itm.Day;
                    //new_coverage.ObjectiveCode = "INV";

                    string n_month = itm.month.ToString();

                    new_coverage.nextinventory.Add(new ARMS_W.Objects.Event.nextInventory()
                    {


                        AccountCode = itm.acctCode,
                        ContactPerson = itm.Contactperson,
                        ContactPersonNo = itm.ContactpersonNo,
                        Day = itm.Day,
                        ObjectiveCode = "INV"/**this is hardcode to save inventory count**/
                    });

                    if (DATABASE.CoverageDtls.Any(p => p.AccountCode == itm.acctCode && p.Day == itm.Day & p.CoverageHdr.Month == n_month & p.CoverageHdr.Year == itm.year && p.CoverageHdr.EmpIdNo == itm.empIdNo))
                    {
                        String eventID = DATABASE.CoverageDtls.Single(p => p.AccountCode == itm.acctCode & p.Day == itm.Day & p.CoverageHdr.Month == n_month).EventID;
                        String LineNum = DATABASE.CoverageDtls.Single(p => p.AccountCode == itm.acctCode & p.Day == itm.Day & p.CoverageHdr.Month == n_month).LineNum;
                        DATABASE.CoverageDtl1.AddObject(new Models.CoverageDtl1()
                        {
                            EventID = eventID,
                            LineNum = LineNum,
                            Day = itm.Day,
                            ObjectiveCode = "INV"


                        });
                    }
                    else
                    {
                        
                        string new_linenum = string.Format("{0:000000}", "SEQ" + string.Format("{0:000000}", begginibg_linenum));
                        String eventID = DATABASE.CoverageHdrs.Single(p => p.Month == n_month & p.Year == itm.year & p.EmpIdNo == itm.empIdNo).EventID;
                        DATABASE.CoverageDtls.AddObject(new Models.CoverageDtl()
                        {
                            EventID = eventID,
                            LineNum = new_linenum,
                            Day = itm.Day,
                            AccountCode = itm.acctCode,
                            ContactPerson = itm.Contactperson,
                            ContactPersonNo = itm.ContactpersonNo,
                            isPlanned ="T",
                            IsAnEdit ="F",
                            IsDeleted="F"
                            




                        });
                        DATABASE.CoverageDtl1.AddObject(new Models.CoverageDtl1()
                        {
                            EventID = eventID,
                            LineNum = new_linenum,
                            Day = itm.Day,
                            ObjectiveCode = "INV"

                        });

                        begginibg_linenum++;
                    }

                }
                    DATABASE.SaveChanges();

                
              
                sql_trans.Committransaction();
                return Json(SActionResult.Success);
            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                return Json(SActionResult.Error + ex.Message);
            
            }

        }


        private string GetLineNumCode()
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            var qry = (from eventhdr in DATABASE.CoverageDtls
                       where eventhdr.LineNum.StartsWith("SEQ")
                       select new { eventhdr.LineNum }).ToList();


            List<int> list_of_id = new List<int>();
            if (qry.Count > 0)
                foreach (var itm in qry)
                {
                    list_of_id.Add(Convert.ToInt32(itm.LineNum.Replace("SEQ", "")));

                }
            DATABASE.Dispose();
            if (list_of_id.Count > 0)
            {
                int largest_id = list_of_id.Max() + 1;
                return "SEQ" + string.Format("{0:000000}", largest_id);


            }

            else
            {

                return "SEQ000000";


            }




        }


        public JsonResult getyearincoverage()
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> year = UserDefineFunctions.Application.getYear();
            return Json(year);
        
        }



        [AuthorizeUsr]
        public string GetFilteredList(string _str_data, string par1 = "", string par2 = "", string tArea = "", string array1 = "")
        {
            string strquery = "", str_region = "", str_channel = "", str_area = "";
            _User oUsr = new _User(HttpContext.Session["username"].ToString());

            foreach (_Roles rls in oUsr.Roles)
            {
                foreach (string region_name in rls.Region)
                {
                    if (str_region != "") str_region = str_region + ",";
                    str_region = "'" + region_name + "'";
                }

                foreach (string channel_name in rls.Channel)
                {
                    if (str_channel != "") str_channel = str_channel + ",";
                    str_channel = "'" + channel_name + "'";
                }

                foreach (string area_name in rls.Area)
                {
                    if (str_area != "") str_area = str_area + ",";
                    str_area = "'" + area_name + "'";
                }
            }



            if (_str_data == "ListofCoverageyear")
            {
                strquery = @"
                            SELECT DISTINCT Year FROM COVERAGEHDR ORDER BY Year DESC
                                ";

            }


            try
            {
                DataTable tmp_table = SqlDbHelper.getDataDT(strquery);
                return "00:" + StringHelper.ConvertDataTableToString(tmp_table);
            }
            catch (Exception ex)
            {
                return "01:" + ex.Message;
            }
        }



        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetBrand()
        {

            List<string[]> brand_list = UserDefineFunctions.Application.getBrand();

            return Json(brand_list);

        }


        /* added by billy jay delima*/
        public string UploadExcelDataCoveragePlan(string EventMonth, int EventDay, int EventYear)
        {
            //   List<string[]> data = new List<string[]>();
            var coveragedtls = new List<UserDefineFunctions.Application.coverageplans>();
            var TempUploadCoveragePlan = new UserDefineFunctions.Application.TempUploadCoveragePlan();
            long counter_id = 0;

            HttpFileCollectionBase File = Request.Files;
            string[] file_extensions = { ".xlsx", ".xls" };
            string upload_file_extension = Path.GetExtension(File[0].FileName.ToLower());
            var sql_trans = new SQLTransaction();

            try
            {
                sql_trans.StartTransaction();

                var current_user = new _User(Session["username"].ToString());
                if (Array.IndexOf(file_extensions, upload_file_extension) > -1)
                {
                    string new_tmp_directory = Server.MapPath("..\\UPLOAD_TEMPFOLDER");
                    string new_file_name = Path.GetFileName(File[0].FileName);
                    string str_full_filename = new_tmp_directory + "\\" + new_file_name;

                    Directory.CreateDirectory(new_tmp_directory);
                    File[0].SaveAs(str_full_filename);

                    #region QUERY
                    // ACCOUNTCODE,CONTACTPERSON,CONTACTNUM,HOTELNAME,HOTELNUM,OBJECTIVE,BRAND,PLANNEDAMOUNT,COUNTECLERK,PRODUCTPRESENTED,COUNTERCLERKNO,STORECHECKING,ISSUESANDCONCERN 
                    DataTable qry = ExcelReader.getExclData12(
                        @" SELECT *
                            FROM [Sheet1$]
                            WHERE [ACCOUNTCODE] IS NOT NULL AND [OBJECTIVE] IS NOT NULL
                        ",
                        str_full_filename);

                    #endregion

                    foreach (DataRow itm in qry.Rows)
                    {
                        coveragedtls.Add(new UserDefineFunctions.Application.coverageplans()
                        {
                            AccountCode = itm["ACCOUNTCODE"].ToString(),
                            ContactPerson = itm["CONTACTPERSON"].ToString(),
                            ContactPersonNo = itm["CONTACTNUM"].ToString(),
                            //HotelName = itm["HOTELNAME"].ToString(),
                            //HotelNum = itm["HOTELNUM"].ToString(),
                            //StoreChecking = itm["STORECHECKING"].ToString(),
                            IssuesAndConcerns = itm["ISSUESANDCONCERN"].ToString(),
                            ObjectiveCode = itm["OBJECTIVE"].ToString(),
                            PlannedAmount = itm["PLANNEDAMOUNT"].ToString(),
                            Brand = itm["BRAND"].ToString(),
                            //CounterClerk = itm["COUNTERCLERK"].ToString(),
                            //ProductPresented = itm["PRODUCTPRESENTED"].ToString(),
                            //CounterClerkNo = itm["COUNTERCLERKNO"].ToString()
                        });
                    }

                    var verify_inputs_message = UserDefineFunctions.CalendarEvent.SoCalendar.verify_inputs(coveragedtls, current_user.EmployeeIdNo, EventMonth, EventDay, EventYear);

                    if (verify_inputs_message != "VALID") { throw new Exception(verify_inputs_message); }

                    


                    counter_id = generateTempUploadCoverage_pk();
                    TempUploadCoveragePlan.upload_data = new List<UserDefineFunctions.Application.coverageplans>();
                    TempUploadCoveragePlan.upload_data = coveragedtls;


                    string serialized_data = Newtonsoft.Json.JsonConvert.SerializeObject(TempUploadCoveragePlan);
                    sql_trans.CommandText = QueryBuilder.InsertTo(
                        "TempUploadCoveragePlan",
                        new Dictionary<string, object>()
                            { 
                               {"counter_id",counter_id},
                               {"DataChanges", serialized_data},
                               {"uploadingType","DAILY"}
                           });

                    sql_trans.Committransaction();
                    //res = JsonConvert.SerializeObject(data);

                }
            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                return ex.Message;
            }
            return counter_id.ToString();
        }

        private long generateTempUploadCoverage_pk()
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            
            var _qry = DATABASE.TempUploadCoveragePlans.Select(o => o.counter_id).ToList();

            if (_qry.Count() <= 0)
                return 1;
            long max = _qry.Max();

            DATABASE.Dispose();

            return max + 1;
        }

        public JsonResult getTemporaryUploadData(int counterid)
        {
            AjxResult res = new AjxResult();
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<UserDefineFunctions.Application.CoverageHdrTmp> coveragedetails = new List<UserDefineFunctions.Application.CoverageHdrTmp>();

            var tempUploadData = new UserDefineFunctions.Application.TempUploadCoveragePlan();
            tempUploadData.upload_data = new List<UserDefineFunctions.Application.coverageplans>();

            res.iserror = true;

            try
            {
                var qry_uploaddata = (from a in DATABASE.TempUploadCoveragePlans
                                      where a.counter_id == counterid
                                      select a).SingleOrDefault();

                if (qry_uploaddata == null) throw new Exception("counter id not found");
                tempUploadData = (UserDefineFunctions.Application.TempUploadCoveragePlan)DataChanges.JsonToObject<UserDefineFunctions.Application.TempUploadCoveragePlan>(qry_uploaddata.DataChanges);

                res.data = new
                {
                    collection = UserDefineFunctions.CalendarEvent.SoCalendar.AssignDataToVariables(tempUploadData.upload_data.Where(o => o.ObjectiveCode == "C").ToList()),
                    merchandise = UserDefineFunctions.CalendarEvent.SoCalendar.AssignDataToVariables(tempUploadData.upload_data.Where(o => o.ObjectiveCode == "M").ToList()),
                    customerservice = UserDefineFunctions.CalendarEvent.SoCalendar.AssignDataToVariables(tempUploadData.upload_data.Where(o => o.ObjectiveCode == "CS").ToList()),
                    sales = UserDefineFunctions.CalendarEvent.SoCalendar.AssignDataToVariables(tempUploadData.upload_data.Where(o => o.ObjectiveCode == "S").ToList()),
                };

                res.iserror = false;
            }
            catch (Exception ex)
            {
                res.iserror = true;
                res.message = ex.Message;
            }

            return Json(res);
        }

        public string DeleteUploadPreview(int counter_id)
        {
            var DATABASE = new Models.ARMSTestEntities();
            string res;
            try
            {
                var qry = DATABASE.TempUploadCoveragePlans.Where(o => o.counter_id == counter_id).Single();

                DATABASE.TempUploadCoveragePlans.DeleteObject(qry);
                res = DATABASE.SaveChanges().ToString();

            }
            catch (Exception ex)
            {
                res = ex.Message;
                throw;
            }

            return res;
        }

        [HttpPost]
        public JsonResult SaveUploadPreview(page_param.coverageplan_save_upload page_param_)
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            AjxResult ajx_res = new AjxResult();
            var sql_trans = new SQLTransaction();
            List<page_param.coverageplan_save_upload.account_codes> coverage_plans = new List<page_param.coverageplan_save_upload.account_codes>();

            string event_id="";

            try
            {
                var isExist = ARMS_W.UserDefineFunctions.CalendarEvent.SoCalendar.IsExists(page_param_.so_id, page_param_.event_year.ToString(), page_param_.event_month);

                if (isExist)
                    event_id = DATABASE.CoverageHdrs.Where(o => o.Year == page_param_.event_year && o.Month == page_param_.event_month && o.EmpIdNo == page_param_.so_id).Single().EventID;
                else
                {
                    event_id = GenerateNewCode("EVNT", "CoverageHdr", "EventID");

                    sql_trans.InsertTo("CoverageHdr", new Dictionary<string, object>() {
                        {"EventID",event_id},
                        {"EmpIdNo",page_param_.so_id},
                        {"Year",page_param_.event_year},
                        {"Month",page_param_.event_month},
                        {"DoctypeId",(int)ARMS_W.SkelClass.Globals.InfoType.CalendarEvent},
                        {"DocumentStatusId","0"}
                        });

                }
                string LineNum = GenerateNewCode("SEQ", "CoverageDtls", "LineNum");

                coverage_plans = UserDefineFunctions.CalendarEvent.SoCalendar.arrange_data_page_param(page_param_);

                sql_trans.StartTransaction();

                //workaround to save coverage plan
                foreach (var coverage in coverage_plans)
                {
                    sql_trans.InsertTo("CoverageDtls", new Dictionary<string, object>(){
                            {"EventID",event_id},
                            {"LineNum",LineNum},
                            {"Day",page_param_.event_day},
                            {"AccountCode",coverage.account_code},
                            {"ContactPerson",coverage.contact_person},
                            {"ContactPersonNo",coverage.contact_person_no},
                            {"HotelName",coverage.hotel_name},
                            {"HotelContactNumber",coverage.hotel_num},
                            {"IssuesAndConcerns",coverage.issues_and_concerns},
                            {"StoreChecking",coverage.store_checking},
                            {"AcctStatus",0},
                            {"isPlanned","T"},
                            {"IsAnEdit","F"},
                            {"IsDeleted","F"},
                            {"hasCallreport","F"}
                     });

                    foreach (var objectives in coverage.list_objectives)
                    {
                        sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>()
                        {
                            {"EventID",event_id},
                            {"LineNum",LineNum},
                            {"Day",page_param_.event_day},
                            {"ObjectiveCode",objectives.objective_code},
                            {"CounterClerk",objectives.counter_clerk},
                            {"Brand",objectives.brand},
                            {"CounterClerkNo",objectives.counter_clerk_no},
                            {"PlannedAmount",objectives.planned_amount},
                            {"ProductPresented",objectives.product_presented}
                        });
                    }

                    LineNum = GenerateNewCode(LineNum);
                }



                var res = DeleteUploadPreview(page_param_.counter_id);

                sql_trans.Committransaction();
            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
                throw;
            }

            return Json(ajx_res);
        }

        private string GenerateNewCode(string initCode, string tableName, string ID_fieldName)
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

        private string GenerateNewCode(string code)
        {
            return Regex.Replace(code, @"[^A-Z]+", String.Empty) + string.Format("{0:000000}", int.Parse(Regex.Replace(code, @"[^\d]", String.Empty)) + 1);
        }

        /* MONTHLY COVERAGE UPLOAD */

        public string UploadExcelDataCoveragePlan_Monthly(string EventMonth, string empIdNo, int EventYear)
        {
            DateTime curr_date = DateTime.Now;
            AjxResult ajx_result = new AjxResult();

            //   List<string[]> data = new List<string[]>();
            var coveragedtls = new List<UserDefineFunctions.Application.coverageplans>();
            var TempUploadCoveragePlan = new UserDefineFunctions.Application.TempUploadCoveragePlan();
            long counter_id = 0;
            string isErrorWithNoTrans = "";
            HttpFileCollectionBase File = Request.Files;
            string[] file_extensions = { ".xlsx", ".xls" };
            string upload_file_extension = Path.GetExtension(File[0].FileName.ToLower());
            var sql_trans = new SQLTransaction();

            try
            {
                

                var current_user = new _User(Session["username"].ToString());
                if (Array.IndexOf(file_extensions, upload_file_extension) > -1)
                {
                    string new_tmp_directory = Server.MapPath("..\\UPLOAD_TEMPFOLDER");
                    string new_file_name = Path.GetFileName(File[0].FileName);
                    string str_full_filename = new_tmp_directory + "\\" + new_file_name;

                    Directory.CreateDirectory(new_tmp_directory);
                    File[0].SaveAs(str_full_filename);

                    #region QUERY
                    // ACCOUNTCODE,CONTACTPERSON,CONTACTNUM,HOTELNAME,HOTELNUM,OBJECTIVE,BRAND,PLANNEDAMOUNT,COUNTECLERK,PRODUCTPRESENTED,COUNTERCLERKNO,STORECHECKING,ISSUESANDCONCERN 
                    DataTable qry = ExcelReader.getExclData12(
                        //                        @" SELECT *
                        //                            FROM [Sheet1$]
                        //                            WHERE [ACCOUNTCODE] IS NOT NULL AND [OBJECTIVE] IS NOT NULL
                        //                        ",
                            @" SELECT *
                            FROM [Sheet1$]
                            WHERE [ACCOUNTCODE] IS NOT NULL 
                        ",
                        str_full_filename);

                    #endregion

                    foreach (DataRow itm in qry.Rows)
                    {
                        coveragedtls.Add(new UserDefineFunctions.Application.coverageplans()
                        {
                            Day = validate_field(itm["DAY"].ToString()),
                            AccountCode = validate_accountcode(itm["ACCOUNTCODE"].ToString(), empIdNo),
                            ContactPerson = validate_field(itm["CONTACTPERSON"].ToString()),
                            //ContactPersonNo = validate_field(itm["CONTACTPERSONNO"].ToString()),
                            ContactPersonNo = validate_contactnum(validate_field(itm["CONTACTNUM"].ToString())),
                            //HotelName = itm["HOTELNAME"].ToString(),    
                            //HotelNum = itm["HOTELNUM"].ToString(),
                            //StoreChecking = itm["STORECHECKING"].ToString(),
                            IssuesAndConcerns = itm["ISSUESANDCONCERN"].ToString(),
                            ObjectiveCode = validate_objectivecode(itm["AMOUNT"].ToString(), itm["BRAND"].ToString(), itm["DETAILS"].ToString()),//itm["OBJECTIVE"].ToString(),
                            PlannedAmount = validate_amount(itm["AMOUNT"].ToString(), itm["BRAND"].ToString()),

                            Brand = validate_brand(itm["BRAND"].ToString(), itm["AMOUNT"].ToString(), "S"),

                            DetailRemarks = validate_details(itm["DETAILS"].ToString(), itm["AMOUNT"].ToString(), itm["BRAND"].ToString())
                            //CounterClerk = validate_data(itm["OBJECTIVE"].ToString(), itm["COUNTERCLERK"].ToString(), "M"),
                            //ProductPresented = validate_data(itm["OBJECTIVE"].ToString(), itm["PRODUCTPRESENTED"].ToString(), "M"),
                            //CounterClerkNo = validate_data(itm["OBJECTIVE"].ToString(), itm["COUNTERCLERKNO"].ToString(), "M")
                        });
                    }

                    var verify_inputs_message = "VALID";

                    foreach (var itms in coveragedtls)
                    {
                        if (EventYear < curr_date.Year)
                        {
                            isErrorWithNoTrans = "T";
                            throw new Exception("Error uploading past Year");
                        }
                        else if (Convert.ToInt32(EventMonth) < curr_date.Month)
                        {
                            isErrorWithNoTrans = "T";
                            throw new Exception("Error uploading past Month");
                        }
                        else if (Convert.ToInt32(EventMonth) == curr_date.Month && Convert.ToInt32(EventMonth) == curr_date.Month && Convert.ToInt32(itms.Day) < curr_date.Day)
                        {
                            isErrorWithNoTrans = "T";
                            throw new Exception("Error uploading past Day");
                        }
                    }

                    sql_trans.StartTransaction();
                    var isExist = ARMS_W.UserDefineFunctions.CalendarEvent.SoCalendar.IsExists(current_user.EmployeeIdNo, EventYear.ToString(), EventMonth);
                    if (isExist)
                        verify_inputs_message = UserDefineFunctions.CalendarEvent.SoCalendar.verify_inputs(coveragedtls, current_user.EmployeeIdNo, EventMonth, EventYear);
                    else

                        if (verify_inputs_message != "VALID") { throw new Exception(verify_inputs_message); }


                    counter_id = generateTempUploadCoverage_pk();
                    TempUploadCoveragePlan.upload_data = new List<UserDefineFunctions.Application.coverageplans>();
                    TempUploadCoveragePlan.upload_data = coveragedtls;

                    string serialized_data = Newtonsoft.Json.JsonConvert.SerializeObject(TempUploadCoveragePlan);
                    sql_trans.CommandText = QueryBuilder.InsertTo(
                        "TempUploadCoveragePlan",
                        new Dictionary<string, object>()
                            { 
                               {"counter_id",counter_id},
                               {"DataChanges", serialized_data},
                               {"uploadingType","MONTHLY"}
                           });

                    sql_trans.Committransaction();
                    //res = JsonConvert.SerializeObject(data);
                }
            }
            catch (Exception ex)
            {
                if (isErrorWithNoTrans == "T")
                {
                    return ex.Message;
                }
                else
                {
                    sql_trans.RollbackTransaction();
                    return ex.Message;
                }
            }
            return counter_id.ToString();
        }

        private static string validate_data(string obj_code, string value, string valid_obj_code)
        {
            if (valid_obj_code.Contains("|"))
            {
               // bool isValid = true;
                var split_str = valid_obj_code.Split('|');
                foreach (var itm in split_str)
                {
                    if (obj_code == itm)
                        return value;

                }
                return value == "" ? "" : "invalid data(" + value + ")";
            }
            return valid_obj_code == obj_code ? value : (value == "" ? "" : "invalid data(" + value + ")");
        }

        private static string validate_contactnum(string contactnum)
        {
            string success = null;
            var checkifnumber = Regex.IsMatch(contactnum, @"^\d+$");

            if (checkifnumber == true)
            {
                success = contactnum;
            }
            else
            {
                success = "invalid (Numbers only)";
            }
            return success;
        }

        public static string validate_details(string details, string plannedamt, string brand)
        {
            string success=null;
            if (details != "")
            {
                if (brand == "")
                {
                    success = "invalid (Brand is required)";
                }
                else if (plannedamt == "")
                {
                    success = "invalid (Amount is required)";
                }
                else if (brand == "" && plannedamt == "")
                {
                    success = "invalid (Brand and Amount are required)";
                }
                else
                {
                    success = details;
                }
            }
            else
            {
                success = "";
            }
            return success;
        }

        private static string validate_amount(string value, string brand)
        {
            string success = null;

            var checkifnumber = Regex.IsMatch(value, @"^\d+$");

            if (checkifnumber == true)
            {   
                if (value != null)
                {
                    if (brand != "")
                    {
                        success = value;
                    }

                    else
                    {
                        success = "invalid (Brand is required (" + value + ")";
                    }
                }
                else if (value == "")
                {
                    if (brand != "")
                    {
                        success = "invalid (Amount is required)";
                    }
                }
            }
            else
            {
                if (brand == "" && value == "")
                {
                    success = "";
                }
                else if (brand != "" && value=="")
                {
                    success = "invalid (Amount is required)";
                }
                else
                {
                    success = "invalid number(" + value + ")";
                }
            }
            return success;
        }

        public static string validate_objectivecode(string plannedamt, string brand, string details)
        {
            string success=null;
            if (plannedamt != "" || brand!="" || details!="")
            {
                success = "S";
            }
            else
            {
                success = null;
            }
            return success;
        }

        private static string validate_brand(string brand, string plannedamt, string objectivecode)
        {
            string success = null;
            //var query = SqlDbHelper.getDataDT("select distinct brand from armsII_vw_itemMasterFile where brand='" + brand + "'");
            var query = SqlDbHelper.getDataDT("select fldvalue from app_vw_itemBrand where fldvalue='" + brand + "'");

            if (brand != "")
            {
                if (query.Rows != null)
                {
                    foreach (DataRow itm in query.Rows)
                    {
                        if (itm["fldvalue"].ToString() == "")
                            return success = "invalid brand(" + itm["fldvalue"].ToString() + ")";
                        else
                            return success = itm["fldvalue"].ToString();
                    }
                }
                
                     return success == null ? "invalid brand(" + brand + ")" : success;
                
            }
            //else if (plannedamt != "" && brand == "")
            //{
            //    return success = "invalid (BRAND REQUIRED)";
            //}
            return success = "";
        }

        private static string validate_accountcode(string acctcode, string empidno) 
        {
                string success = null;
                var query = SqlDbHelper.getDataDT("select * from arms2_vw_actveBusPrtnr where cardcode = '" + acctcode + "' and empidno='" + empidno + "'");

                if (query.Rows != null)
                {
                    foreach (DataRow itm in query.Rows)
                    {
                        if (itm["CardCode"].ToString() == "")
                            return success = "invalid Code(" + itm["CardCode"].ToString() + ")";
                        else
                            return success = itm["CardCode"].ToString();
                    }
                }
                return success == null ? "invalid Code(" + acctcode + ")" : success;

        }

        private static string validate_field(string field)
        {
            //string requiredfeild = null;
            if (field == null || field == "")
                return field = "(Required Field)";
            else
                return field;
        }

        public JsonResult getTemporaryUploadData_Monthly(int counterid, int eventMonth, int eventYear)
        {
            AjxResult res = new AjxResult();
            ARMSTestEntities DATABASE = new ARMSTestEntities();
            List<UserDefineFunctions.Application.CoverageHdrTmp> coveragedetails = new List<UserDefineFunctions.Application.CoverageHdrTmp>();

            var tempUploadData = new UserDefineFunctions.Application.TempUploadCoveragePlan();
            tempUploadData.upload_data = new List<UserDefineFunctions.Application.coverageplans>();

            res.iserror = true;

            try
            {
                var qry_uploaddata = (from a in DATABASE.TempUploadCoveragePlans
                                      where a.counter_id == counterid
                                      select a).SingleOrDefault();

                if (qry_uploaddata == null) throw new Exception("counter id not found");
                tempUploadData = (UserDefineFunctions.Application.TempUploadCoveragePlan)DataChanges.JsonToObject<UserDefineFunctions.Application.TempUploadCoveragePlan>(qry_uploaddata.DataChanges);

                //fullcalendarEvents
                res.data = new
                {
                    list = UserDefineFunctions.CalendarEvent.SoCalendar.reFormatData(tempUploadData.upload_data, eventMonth, eventYear).ToList()
                };

                res.iserror = false;
            }
            catch (Exception ex)
            {
                res.iserror = true;
                res.message = ex.Message;
            }

            return Json(res);
        }

        private static string validateDateForBatchUpload(string eventid, int day, string accountcode, string month, int year, string empidno)
        {
            ARMSTestEntities db = new ARMSTestEntities();

            var qry = SqlDbHelper.getDataDT("select accountcode from coveragehdr a inner join coveragedtls b on a.eventid=b.eventid where a.month=" + month + " and a.year=" + year + " and a.empidno='" + empidno + "' and b.day=" + day + " and a.eventid='"+eventid+"'");

            var qry1 = db.CoverageHdrs.Join(db.CoverageDtls, a => a.EventID, b => b.EventID, (a, b) => new { a, b }).Where(c => c.a.EventID == eventid && c.a.Month == month && c.a.Year == year && c.a.EmpIdNo == empidno && c.b.Day == day).Select(p=>p.b.AccountCode).ToList();

            if (!qry1.Contains(accountcode))
            {
                return "IN";
            }

            //foreach (DataRow item in qry.Rows)
            //{
            //    if (item["accountcode"].ToString().Contains(accountcode))
            //        return "IN";
            //    else if(item["accountcode"].ToString() == accountcode)
            //        return "IN";
            //    else
            //        return "OUT";
            //}
            return "failed";
        }

        

        public JsonResult SaveUploadedMonthlyCoveragePlan(page_param.save_uploaded_monthly_coverage page_param)
        {
            AjxResult ajx_res = new AjxResult();
            SQLTransaction sql_trans = new SQLTransaction();
            string errormodule="";
            List<SkelClass.Globals.db_changes> changes_in_data = new List<SkelClass.Globals.db_changes>();
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            //ajx_res.iserror = true;
            bool isExist = false;
            string EventID = null;
            DateTime EventDate = new DateTime();

            userHeader account_owner = DATABASE.userHeaders.Single(p => p.empIdNo == page_param.EmpIdNo);

            int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            page_param.Month = months[int.Parse(page_param.Month)].ToString();

            string username = Session["username"].ToString();
            var user = DATABASE.userHeaders.Where(p => p.userName == username).Single().userName;
            int role = getRole(Session["username"].ToString());

            List<string> acctcode = page_param.list.Select(p => p.account_code).ToList();

            try
            {
                var field_error_checking = page_param.list;
                foreach (var itm in field_error_checking)
                {
                    if (itm.account_code.Contains("invalid"))
                    {
                        throw new Exception("Invalid account code.");
                    }

                    if (itm.contact_person_no == "(REQUIRED FIELD)"
                        || itm.contact_person == "(REQUIRED FIELD)"
                        || itm.account_code == ""
                        || itm.account_code == null
                        || itm.contact_person == ""
                        || itm.contact_person == null
                        || itm.contact_person_no == ""
                        || itm.contact_person_no == null
                        || itm.contact_person_no == "invalid (NUMBERS ONLY)")
                    {
                        throw new Exception("Please review required fields.");
                    }


                    foreach (var list in itm.list_objectives)
                    {
                        if (list.objective_code == "S")
                        {
                            if (list.brand != "")
                            {
                                if (list.brand.Contains("invalid"))
                                {
                                    throw new Exception("unable to proceed, error input brand");
                                }
                            }

                            if (list.planned_amount != "")
                            {
                                if (list.planned_amount.Contains("invalid"))
                                {
                                    throw new Exception("unable to proceed, error input amount");
                                }
                            }
                        }

                    }
                }

                isExist = ARMS_W.UserDefineFunctions.CalendarEvent.SoCalendar.IsExists(page_param.EmpIdNo, page_param.Year.ToString(), page_param.Month);

                EventID = isExist ?
                    DATABASE.CoverageHdrs.Single(p => p.EmpIdNo == page_param.EmpIdNo && p.Year == page_param.Year && p.Month == page_param.Month).EventID : GenerateNewCode("EVNT", "CoverageHdr", "EventID");

                List<string> date = page_param.list.Select(p => p.start).Distinct().ToList();
                try
                {
                    sql_trans.StartTransaction();

                    foreach (var itm in date)
                    {
                        EventDate = Convert.ToDateTime(itm);
                        sql_trans.DeleteFromWithIsNull("CoverageDtls", new Dictionary<string, object>(){
                                                        {"EventID",EventID},
                                                        {"Day",EventDate.Day},
                                                        {"isPlanned","T"},
                                                        {"hasCallreport","F"}
                        }, new Dictionary<string, object>() { { "CheckInTime", null } });
                    }
                    //sql_trans.RollbackTransaction();
                    sql_trans.Committransaction();
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                try
                {
                     DateTime curr_date = DateTime.Now;

                        if (curr_date.Year <= Convert.ToInt32(page_param.Year))
                        {
                            if (curr_date.Month <= Convert.ToInt32(page_param.Month))
                            {
                                if (curr_date.Day <= EventDate.Day)
                                {
                                    //var datecode = validateDateForBatchUpload(EventID, EventDate.Day, itm2.acctcode, page_param.Month, page_param.Year, page_param.EmpIdNo);

                                    //if (datecode == "IN")
                                    //{
                                    //    throw new Exception("Duplicate accountcode detected.");
                                    //}
                                }
                                else
                                {
                                    throw new Exception("Error uploading past dates.");
                                }
                            }
                            else
                            {
                                throw new Exception("Error uploading past month.");
                            }
                        }
                        else
                        {
                            throw new Exception("Error uploading past year.");
                        }
                }
                catch (Exception ex)
                {
                    errormodule = "1";
                    throw new Exception(ex.Message);
                }

                var accountcode = page_param.list.Select(p => new { acctcode = p.account_code, day = p.start }).ToList();
                
                foreach (var item in date)
                {
                    EventDate = Convert.ToDateTime(item);
                    var qry1 = DATABASE.CoverageHdrs.Join(DATABASE.CoverageDtls, a => a.EventID, b => b.EventID, (a, b) => new { a, b })
                        .Where(c => c.a.EventID == EventID && c.a.Month == page_param.Month && c.a.Year == page_param.Year && c.a.EmpIdNo == page_param.EmpIdNo && c.b.Day == EventDate.Day && acctcode.Contains(c.b.AccountCode))
                        .Select(p => new { acctcode = p.b.AccountCode, day = p.b.Day })
                        .ToList();

                    foreach (var item2 in qry1)
                    {
                        CoverageDtl container = new CoverageDtl();
                        List<CoverageDtl> data = DATABASE.CoverageDtls.Where(p => p.CoverageHdr.EventID == EventID && p.CoverageHdr.Month == page_param.Month && p.CoverageHdr.Year == page_param.Year && p.CoverageHdr.EmpIdNo == page_param.EmpIdNo && p.Day == item2.day && p.AccountCode == item2.acctcode && p.isPlanned == "F").ToList();
                        foreach (var item3 in data)
                        {
                            item3.isPlanned = "T";
                            try
                            {
                                DATABASE.SaveChanges();
                            }
                            catch
                            {
                                ajx_res.iserror = true;
                                errormodule = "1";
                                throw new Exception("Error updating plan");
                            }
                        }
                    }
                }

                Dictionary<string, object> coverageDtl = new Dictionary<string, object>();
                DateTime curr_Date = DateTime.Now;

                var qry2 = DATABASE.CoverageHdrs.Join(DATABASE.CoverageDtls, a => a.EventID, b => b.EventID, (a, b) => new { a, b })
                        .Where(c => c.a.EventID == EventID && c.a.Month == page_param.Month && c.a.Year == page_param.Year && c.a.EmpIdNo == page_param.EmpIdNo && c.b.Day == curr_Date.Day)
                        .Select(p => 
                            p.b.AccountCode
                        )
                        .ToList();

                var pageparam = page_param.list.Select(p => new
                {
                    acctcode = p.account_code
                })
                .ToList();

                sql_trans.StartTransaction();


                    if (!isExist)
                        sql_trans.InsertTo("CoverageHdr", new Dictionary<string, object>(){
                                                        {"EventID",EventID},
                                                        {"EmpIdNo",page_param.EmpIdNo},
                                                        {"Year",page_param.Year},
                                                        {"Month",page_param.Month},
                                                        {"DocTypeId",(int)Globals.InfoType.CalendarEvent},
                                                        {"DocumentStatusId","4"}
                    });


                    string LineNum = GenerateNewCode("SEQ", "CoverageDtls", "LineNum");

                    foreach (var day in date)
                    {
                        //string LineNum = GenerateNewCode("SEQ", "CoverageDtls", "LineNum");
                        // coverageDtl["EventID"] = qry_evnt_id;
                        EventDate = Convert.ToDateTime(day);
                        if (EventDate.Day == curr_Date.Day)
                        {
                            //string LineNum2 = GenerateNewCode("SEQ", "CoverageDtls", "LineNum");
                            var page_params = page_param.list.Where(p => !qry2.Contains(p.account_code) && Convert.ToDateTime(p.start).Day==curr_Date.Day).ToList();
                            foreach (var item2 in page_params)
                            {
                                sql_trans.InsertTo("CoverageDtls", new Dictionary<string, object>(){
                                    {"EventID",EventID},
                                    {"LineNum",LineNum},
                                    {"Day",EventDate.Day},
                                    {"AccountCode",item2.account_code},
                                    {"ContactPerson",item2.contact_person},
                                    {"ContactPersonNo",item2.contact_person_no},
                                    {"HotelName",item2.hotel_name},
                                    {"HotelContactNumber",item2.hotel_num},
                                    {"IssuesAndConcerns",item2.issues_and_concerns},
                                    {"StoreChecking",item2.store_checking},
                                    {"AcctStatus",0},
                                    {"isPlanned","T"},
                                    {"IsAnEdit","F"},
                                    {"IsDeleted","F"},
                                    {"hasCallreport","F"}});

                                foreach (var objectives in item2.list_objectives)
                                {
                                    if (objectives.objective_code != null)
                                    {
                                        sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>(){
                                            {"EventID",EventID},
                                            {"LineNum",LineNum},
                                            {"Day",EventDate.Day},
                                            //{"ObjectiveCode",objectives.objective_code},
                                            {"ObjectiveCode", objectives.objective_code},
                                            {"CounterClerk",objectives.counter_clerk},
                                            {"Brand",objectives.brand},
                                            {"CounterClerkNo",objectives.counter_clerk_no},
                                            {"PlannedAmount",Convert.ToDecimal(removeComma(objectives.planned_amount))},
                                            {"ProductPresented",objectives.product_presented},
                                            {"detailRemarks", objectives.details_remark}});
                                    }
                                }
                                LineNum = GenerateNewCode(LineNum);
                            }
                            
                        }
                    }

                    var excludecurrdate = page_param.list.Where(p => !Convert.ToString(curr_Date.Day).Contains(Convert.ToString(Convert.ToDateTime(p.start).Day))).ToList();

                    foreach (var itm in excludecurrdate)
                    {
                        DateTime EventDates = Convert.ToDateTime(itm.start);
                        sql_trans.InsertTo("CoverageDtls", new Dictionary<string, object>(){
                                {"EventID",EventID},
                                {"LineNum",LineNum},
                                {"Day",EventDates.Day},
                                {"AccountCode",itm.account_code},
                                {"ContactPerson",itm.contact_person},
                                {"ContactPersonNo",itm.contact_person_no},
                                {"HotelName",itm.hotel_name},
                                {"HotelContactNumber",itm.hotel_num},
                                {"IssuesAndConcerns",itm.issues_and_concerns},
                                {"StoreChecking",itm.store_checking},
                                {"AcctStatus",0},
                                {"isPlanned","T"},
                                {"IsAnEdit","F"},
                                {"IsDeleted","F"},
                                {"hasCallreport","F"}});

                        foreach (var objectives in itm.list_objectives)
                        {
                            if (objectives.objective_code != null)
                            {
                                sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>(){
                                        {"EventID",EventID},
                                        {"LineNum",LineNum},
                                        {"Day",EventDate.Day},
                                        //{"ObjectiveCode",objectives.objective_code},
                                        {"ObjectiveCode", objectives.objective_code},
                                        {"CounterClerk",objectives.counter_clerk},
                                        {"Brand",objectives.brand},
                                        {"CounterClerkNo",objectives.counter_clerk_no},
                                        {"PlannedAmount",Convert.ToDecimal(removeComma(objectives.planned_amount))},
                                        {"ProductPresented",objectives.product_presented},
                                        {"detailRemarks", objectives.details_remark}});
                            }
                        }
                        LineNum = GenerateNewCode(LineNum);
                    }

                if (ajx_res.iserror == false)
                    if (!isExist)
                    {
                        SaveRouteChanges(sql_trans, Globals.DocActionType.Created, user, 0, 4, EventID, "Batch Upload for " + account_owner.firstName + " " + account_owner.lastName + "", role);
                    }
                    else
                    {
                        SaveRouteChanges(sql_trans, Globals.DocActionType.Update, user, 0, 4, EventID, "MCP Revise for " + account_owner.firstName + " " + account_owner.lastName + "", role);
                    }
                //sql_trans.RollbackTransaction();
                sql_trans.Committransaction();
                ajx_res.iserror = false;
            }
            catch (Exception ex)
            {
                if (errormodule == "1")
                {
                    ajx_res.iserror = true;
                    ajx_res.message = ex.Message;
                }
                else
                {
                    ajx_res.iserror = true;
                    ajx_res.message = ex.Message;
                    sql_trans.RollbackTransaction();
                }
            }
            finally
            {
                var res = DeleteUploadPreview(page_param.counter_id);
            }

            return Json(ajx_res);
        }

        public int getRole(string username)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var qry = (from user in DATABASE.userHeaders
                       from approverDesig in DATABASE.apprvrDesigs
                       where user.userName == username
                       && user.counterId == approverDesig.counterId
                       select approverDesig);
            if (qry.Select(p => p.roleID).Contains(5)) return 5;
            else if (qry.Select(p => p.roleID).Contains(8)) return 8;
            else if (qry.Select(p => p.roleID).Contains(2)) return 2;
            else return 17;
        }

        private static void SaveRouteChanges(Class.SQLTransaction sql_trans, Globals.DocActionType act_type, string username, int next_id, int docstat_id, string DocId, string remarks, int roleid)
        {
            sql_trans.InsertTo("RouteChanges", new Dictionary<string, object>() { 
                    {"DocTypeId",  (int)Globals.InfoType.CalendarEvent}
                    , {"ActionType", (int)act_type}
                    , {"DocStatusId", next_id}
                    , {"UserName", username.ToUpper()}
                    , {"PrevDocStatusId",docstat_id}
                    , {"Remarks",remarks}
                    , {"TimeStamp", DateTime.Now}
                    , {"DocId", DocId}
                    , {"RoleCode", MiscFunctions.GetRoleCode((int)Globals.InfoType.CalendarEvent,docstat_id,roleid) }
                    });

        }

        private string removeComma(string val)
        {

            return val.Contains(",") ? val.Replace(",", String.Empty) : val;

        }

        public int DeleteUploadedMonthlyCoveragePlan(int counter_id)
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            SQLTransaction sql_trans = new SQLTransaction();
            int success = 0;
            try
            {
                sql_trans.StartTransaction();
                sql_trans.DeleteFrom("TempUploadCoveragePlan", new Dictionary<string, object>()
                                                                {
                                                                    {"counter_id",counter_id}
                                                                });
                sql_trans.Committransaction();

                success = 0;
            }
            catch (Exception)
            {
                sql_trans.RollbackTransaction();
                success = -1;
                throw;
            }

            return success;
        }
       
        /*code end  billy jay delima*/

        public string GetItemList(string _str_data = "", string keyword = "", string _accountcode = "", string SoId = "", string month = "", int day = 0, int year = 0)
        {
            StringBuilder table_builder = new StringBuilder();

            List<page_param.SoAccount> _listData = new List<page_param.SoAccount>();
            List<page_param.SoAccount> _listDataincoverage = new List<page_param.SoAccount>();


            _listData = UserDefineFunctions.Application.GetSOAccountCode3(SoId, month, day, year);
            _listDataincoverage = UserDefineFunctions.Application.getAccountinday(SoId, month, day, year);


            //var res = _brand == null ? _listData.Take(10) : _listData.Where(o => o.brand == _brand);
            var res = _accountcode == null ? _listData.Take(15) : _listData.Where(o => o.CardCode.Contains(_accountcode.ToUpper())).ToList();


            var filter_keyword = from a in res
                                 where a.CardCode.Contains(keyword.ToUpper())
                                 select a;

            foreach (var itm in filter_keyword)
                foreach (var itm2 in _listDataincoverage)
                    if (itm.CardCode == itm2.CardCode)
                        table_builder.Append("<div style='font-weight:bold;'><a href=\"javascript:;\" val1=\"" + itm.CardCode + "\" val2=\"" + itm.CardCode + "\" val3=\"" + itm.CardName + "\" val4=\"" + itm.Address + "\" val5=\"" + itm.acctClassfxn + "\" val6=\"" + itm.CntctPrsn + "\" val7=\"" + itm.Phone1 + "\">" + itm.CardCode + "  </a></div>");
                    else
                        table_builder.Append("<div><a href=\"javascript:;\" val1=\"" + itm.CardCode + "\" val2=\"" + itm.CardCode + "\" val3=\"" + itm.CardName + "\" val4=\"" + itm.Address + "\" val5=\"" + itm.acctClassfxn + "\" val6=\"" + itm.CntctPrsn + "\" val7=\"" + itm.Phone1 + "\">" + itm.CardCode + " </a></div>");

            return table_builder.ToString();
        }
               [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public String lookUpAccount(string Eventmonth, int Eventday, int Eventyear, string soId, string obj, string isplanned)
        {
            StringBuilder sb = new StringBuilder();

            //var DATABASE = new Models.ARMSTestEntities();
            //var style1 = "font-weight:bold; color:Red;";
            //var style2 = string.Empty;
           // if (obj == "txt_acctcode")
            if (obj == "txt_accountName")
            {
                //var Accounts = UserDefineFunctions.Application.GetSOAccountCode3(soId, Eventmonth, Eventday, Eventyear);
                var Accounts = UserDefineFunctions.Application.LookUpAccount(soId, Eventmonth, Eventday, Eventyear);
                var n_Accounts = UserDefineFunctions.Application.getAccountinday(soId, Eventmonth, Eventday, Eventyear);


                //if (n_Accounts.Count() >= 1)
                //{
                //    sb.Append("<option style=\"" + ((DATABASE.CoverageHdrs.Single(o => o.EmpIdNo == soId && o.Month == Eventmonth && o.Year == Eventyear).CoverageDtls.Any(o => o.Day == Eventday && o.AccountCode == "CVMATIMCO")) ? style1 : style2).ToString() + "\" value=\"MATIMCO INC. (CEBU)\" code=\"CVMATIMCO\" Addrs=\"Highway Estancia, Mandaue City 6014 Cebu, Philippines\" contactper=\"\" phone=\"\" classfxn=\"\" hotelname=\"\" hotelContact=\"\">MATIMCO INC. (CEBU)</option>");


                //    if (DATABASE.CoverageHdrs.Single(o => o.EmpIdNo == soId && o.Month == Eventmonth && o.Year == Eventyear).CoverageDtls.Any(o => o.Day == Eventday && o.AccountCode == "CVMATIMCO"))
                //    {
                //        sb.Append("<option style=\"font-weight:bold; color:Red;\" value=\"MATIMCO INC. (CEBU)\" code=\"CVMATIMCO\" Addrs=\"Highway Estancia, Mandaue City 6014 Cebu, Philippines\" contactper=\"\" phone=\"\" classfxn=\"\" hotelname=\"\" hotelContact=\"\">MATIMCO INC. (CEBU)</option>");
                //    }
                //    else
                //        sb.Append("<option value=\"MATIMCO INC. (CEBU)\" code=\"CVMATIMCO\" Addrs=\"Highway Estancia, Mandaue City 6014 Cebu, Philippines\" contactper=\"\" phone=\"\" classfxn=\"\" hotelname=\"\" hotelContact=\"\">MATIMCO INC. (CEBU)</option>");

                //    if (DATABASE.CoverageHdrs.Single(o => o.EmpIdNo == soId && o.Month == Eventmonth && o.Year == Eventyear).CoverageDtls.Any(o => o.Day == Eventday && o.AccountCode == "CLMATIMCO"))
                //    {
                //        sb.Append("<option style=\"font-weight:bold; color:Red;\" value=\"MATIMCO INC. (QUEZON CITY)\" code=\"CLMATIMCO\" Addrs=\"83 Old Samson Road Balintawak 1106 Quezon City, Philippines \" contactper=\"\" phone=\"\" classfxn=\"\" hotelname=\"\" hotelContact=\"\">MATIMCO INC. (QUEZON CITY)</option>");
                //    }
                //    else
                //        sb.Append("<option value=\"MATIMCO INC. (QUEZON CITY)\" code=\"CLMATIMCO\" Addrs=\"83 Old Samson Road Balintawak 1106 Quezon City, Philippines \" contactper=\"\" phone=\"\" classfxn=\"\" hotelname=\"\" hotelContact=\"\">MATIMCO INC. (QUEZON CITY)</option>");
                //}

                //if (!n_Accounts.Any(o => o.CardCode == "CVMATIMCO"))
                //    sb.Append("<option value=\"MATIMCO INC. (CEBU)\" code=\"CVMATIMCO\" Addrs=\"Highway Estancia, Mandaue City 6014 Cebu, Philippines\" contactper=\"\" phone=\"\" classfxn=\"\" hotelname=\"\" hotelContact=\"\">MATIMCO INC. (CEBU)</option>");
                //if (!n_Accounts.Any(o => o.CardCode == "CLMATIMCO"))
                //    sb.Append("<option value=\"MATIMCO INC. (QUEZON CITY)\" code=\"CLMATIMCO\" Addrs=\"83 Old Samson Road Balintawak 1106 Quezon City, Philippines \" contactper=\"\" phone=\"\" classfxn=\"\" hotelname=\"\" hotelContact=\"\">MATIMCO INC. (QUEZON CITY)</option>");
              

                foreach (var itm2 in n_Accounts)
                {
                    sb.Append("<option style=\"font-weight:bold; color:Red;\" value=\"" + itm2.CardName + "\" code=\"" + itm2.CardCode + "\" Addrs=\"" + itm2.Address + "\" contactper=\"" + itm2.CntctPrsn + "\" phone=\"" + itm2.Phone1 + "\" classfxn=\"" + itm2.acctClassfxn + "\" hotelname=\"" + itm2.HotelName + "\" hotelContact=\"" + itm2.HotelContactNum + "\">" + itm2.CardName + "</option>");

                }
                foreach (var itm in Accounts)
                {

                    sb.Append("<option value=\"" + itm.CardName + "\" code=\"" + itm.CardCode + "\" Addrs=\"" + itm.Address + "\" contactper=\"" + itm.CntctPrsn + "\" phone=\"" + itm.Phone1 + "\" classfxn=\"" + itm.acctClassfxn + "\">" + itm.CardName + "</option>");
                }

                
            }

            //if (obj == "txt_cr_accountCode")
            if (obj == "txt_cr_accountname")
            {
                if (isplanned == "T")
                {
                    var Accounts = ARMS_W.UserDefineFunctions.Application.LookUpAccount(soId, Eventmonth, Eventday, Eventyear);

                    foreach (var itm in Accounts)
                    {

                        sb.Append("<option value=\"" + itm.CardName + "\" code=\"" + itm.CardCode + "\" Addrs=\"" + itm.Address + "\" contactper=\"" + itm.CntctPrsn + "\" phone=\"" + itm.Phone1 + "\" classfxn=\"" + itm.acctClassfxn + "\">" + itm.CardName + "</option>");
                    }
                    //sb.Append("<option value=\"MATIMCO INC. (CEBU)\" code=\"CVMATIMCO\" Addrs=\"Highway Estancia, Mandaue City 6014 Cebu, Philippines\" contactper=\"\" phone=\"\" classfxn=\"\" hotelname=\"\" hotelContact=\"\">MATIMCO INC. (CEBU)</option>");
                   // sb.Append("<option value=\"MATIMCO INC. (QUEZON CITY)\" code=\"CLMATIMCO\" Addrs=\"83 Old Samson Road Balintawak 1106 Quezon City, Philippines \" contactper=\"\" phone=\"\" classfxn=\"\" hotelname=\"\" hotelContact=\"\">MATIMCO INC. (QUEZON CITY)</option>");
             
                }

                else
                {
                    //string accountName = string.Empty;
                    //string accountAddr = string.Empty;
                    var Accounts = ARMS_W.UserDefineFunctions.Application.LookupItemisCoveragebydate(Eventmonth, Eventday, Eventyear, soId);
                    foreach (var itm in Accounts)
                    {
                       // accountName = itm.AccountName == null ? UserDefineFunctions.Application.getAccountName(itm.AccountCode) : itm.AccountName;
                      //  accountAddr = itm.AccountAddress == null ? UserDefineFunctions.Application.getAccountAddress(itm.AccountCode) : itm.AccountAddress;
                        sb.Append("<option value=\"" + itm.AccountName + "\" code=\"" + itm.AccountCode + "\" Addrs=\"" + itm.AccountAddress + "\" contactper=\"" + itm.ContactPerson + "\" phone=\"" + itm.ContactPersonNo + "\" classfxn=\"" + itm.AccountClass + "\" hotelname=\"" + itm.HotelName + "\" hotelContact=\"" + itm.HotelNum + "\">" + itm.AccountName + "</option>");
                    }

                }

            }
            

            return sb.ToString() ;

        }

        public JsonResult getSpecificAccountCodeInfo(string Eventmonth, int Eventday, int Eventyear, string soId, string acctCode)
        {
            AjxResult ajx_res = new AjxResult();
            var Accounts = ARMS_W.UserDefineFunctions.Application.LookupItemisCoveragebydate(Eventmonth, Eventday, Eventyear, soId);
            ajx_res.data = new{  list = Accounts.Where(o => o.AccountCode == acctCode).ToList()   };


            return Json(ajx_res);
        }

        public JsonResult getSpecificAccoutCodeInfo_Revise(string EventId, string Eventmonth, int Eventday, int Eventyear, string soId, string acctCode)
        {
            AjxResult ajx_res = new AjxResult();
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
           // string LineNum = "";
            DateTime? coverage_date = new DateTime(Eventyear, int.Parse(Eventmonth), Eventday, 0, 0, 0).Date;
            //var coveragebydate = UserDefineFunctions.Application.GetCoverageInfo(EventId, Eventmonth, Eventday, Eventyear, soId, acctCode);

            //try
            //{
            //    LineNum = DATABASE.CoverageDtls.Single(p => p.EventID == EventId && p.Day == Eventday && p.AccountCode == acctCode).LineNum;
            //    DATABASE.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    LineNum = null;
            //    DATABASE.Dispose();
            //}

            try
            {
                //var qry = UserDefineFunctions.Application.getCoverageCallReportAccount(Eventmonth, Eventday, Eventyear, soId, acctCode);
                var getNumberOfVisits = DATABASE.CoverageHdrs.Where(o => o.Month == Eventmonth && o.Year == Eventyear && o.EmpIdNo == soId).Single().CoverageDtls.Where(o => o.AccountCode == acctCode && o.IsDeleted != "T" && o.hasCallreport == "T").Count() + 1;
                var collection_objective_lookupfromsap = UserDefineFunctions.Application.BalanceLookUpFromSAP(acctCode);
                var total_amount_collection = collection_objective_lookupfromsap.Sum(o => o.balance);


                ajx_res.data = new
                {
                    coverage = UserDefineFunctions.Application.getCoverageCallReportAccount(Eventmonth, Eventday, Eventyear, soId, acctCode),
                    accountinfo = UserDefineFunctions.Application.AccountInfo(Eventmonth, Eventday, Eventyear, soId, acctCode),
                    inventory_objective = UserDefineFunctions.Application.getInventoryCountObjective(coverage_date, acctCode, soId),
                    collection_objective_lookupfromsap = collection_objective_lookupfromsap,
                    total_collection_objective_lookupfromsap = total_amount_collection,
                    number_of_visits = getNumberOfVisits
                };
            }
            catch (Exception ex)
            {
                ajx_res.message = ex.Message;
            }
           

            return Json(ajx_res);
        }




               //public JsonResult getObjectiveCode(string EventId, string Eventmonth, int Eventday, int Eventyear, string soId)
               //{

               //    Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
               //    List<page_param.CoverageHdr> ObjectiveCode = ARMS_W.UserDefineFunctions.Application.getObjectiveCode(EventId, Eventmonth, Eventday, Eventyear, soId);

               //    return Json(ObjectiveCode);

               //}


               [HttpPost]
               public JsonResult getObjectiveCode(string EventId, string Eventmonth, int Eventday, int Eventyear, string soId, string Acctcode)
               {
                   var DATABASE = new Models.ARMSTestEntities();
                   SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
                   ajx_result.iserror = true;

                   try
                   {
                       ajx_result.iserror = false;
                       var getNumberOfVisits = DATABASE.CoverageHdrs.Where(o => o.Month == Eventmonth && o.Year == Eventyear && o.EmpIdNo == soId).Single().CoverageDtls.Where(o => o.AccountCode == Acctcode && o.IsDeleted != "T" && o.hasCallreport == "T").Count() + 1;
                       var Objectived = ARMS_W.UserDefineFunctions.Application.getObjectiveCode(EventId, Eventmonth, Eventday, Eventyear, soId, Acctcode);
                       ajx_result.data = new
                       {
                           Objectives = Objectived,
                           NumberOfVisits = getNumberOfVisits
                       };



                       DATABASE.Dispose();
                   }
                   catch (Exception ex)
                   {

                       ajx_result.iserror = true;
                       ajx_result.message = ex.Message;
                   }
                   return Json(ajx_result);

               }


               [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
               public String lookUpAccountASM(string Eventmonth, int Eventday, int Eventyear, string soId, string obj)
               {
                   StringBuilder sb = new StringBuilder();

                   if (obj == "txt_vw_acctcode")
                   {
                       var Accounts = ARMS_W.UserDefineFunctions.Application.LookupItemisCoveragebydate(Eventmonth, Eventday, Eventyear, soId);
                       foreach (var itm in Accounts)
                       {

                           sb.Append("<option value=\"" + itm.AccountCode + "\" name=\"" + itm.AccountName + "\" Addrs=\"" + itm.AccountAddress + "\" contactper=\"" + itm.ContactPerson + "\" phone=\"" + itm.ContactPersonNo + "\" classfxn=\"" + itm.AccountClass + "\" hotelname=\"" + itm.HotelName + "\" hotelContact=\"" + itm.HotelNum + "\" EventId=\"" + itm.EventID + "\" months=\"" + itm.Month + "\" year=\"" + itm.Year + "\" day=\"" + itm.Day + "\" soId=\"" + itm.EmpIdNo + "\">" + itm.AccountCode + "</option>");
                       }  

                   }

                   if (obj == "txt_vwcr_accountCode")
                   {
                       var Accounts = ARMS_W.UserDefineFunctions.Application.LookupItemisCoveragebydate(Eventmonth, Eventday, Eventyear, soId);
                       foreach (var itm in Accounts)
                       {

                           sb.Append("<option value=\"" + itm.AccountCode + "\" name=\"" + itm.AccountName + "\" Addrs=\"" + itm.AccountAddress + "\" contactper=\"" + itm.ContactPerson + "\" phone=\"" + itm.ContactPersonNo + "\" classfxn=\"" + itm.AccountClass + "\" hotelname=\"" + itm.HotelName + "\" hotelContact=\"" + itm.HotelNum + "\" EventId=\"" + itm.EventID + "\" months=\"" + itm.Month + "\" year=\"" + itm.Year + "\" day=\"" + itm.Day + "\" soId=\"" + itm.EmpIdNo + "\">" + itm.AccountCode + "</option>");
                       }

                   }

                  


                   return sb.ToString();

               }

               public JsonResult getTRFfromSAP(string EventId, string Eventmonth, int Eventday, int Eventyear, string soId, string acctCode)
               {
                   AjxResult ajx_res = new AjxResult();
                   Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
                   try
                   {
                       //var qry = UserDefineFunctions.Application.getCoverageCallReportAccount(Eventmonth, Eventday, Eventyear, soId, acctCode);
                       var collection_objective_lookupfromsap = UserDefineFunctions.Application.BalanceLookUpFromSAP(acctCode);
                       var total_amount_collection = collection_objective_lookupfromsap.Sum(o => o.balance);


                       ajx_res.data = new
                       {
                           collection_objective_lookupfromsap = collection_objective_lookupfromsap,
                           total_collection_objective_lookupfromsap = total_amount_collection
                       };
                   }
                   catch (Exception ex)
                   {
                       ajx_res.message = ex.Message;
                   }


                   return Json(ajx_res);
               }


    }
}
                                            
