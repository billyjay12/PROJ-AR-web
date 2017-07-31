using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARMS_W.Class;
using ARMS_W.GLOBALS;
using System.Data.OleDb;
using System.IO;
using System.Data;
using ARMS_W.SkelClass;
using System.Text.RegularExpressions;
using ARMS_W.Models;
using System.Text;

namespace ARMS_W.Controllers
{
    public partial class CalendarController : Controller
    {
        //
        // GET: /Calendar/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyCalendar(string soId, string year, string month)
        {
            ViewData["soId"] = soId;
            ViewData["month"] = month;
            ViewData["year"] = year;

            return View();
        }

        public ActionResult BlankPage() 
        {
            return View();
        }

        public ActionResult SoCalendar()
        {
            return View();
        }

        public ActionResult EventViewer()
        {

            return View();
        }

        public ActionResult ChangesForApproval() 
        {

            return View();
        
        }

        public ActionResult SOMonthlyCoverageEventBatchUpload()
        {
            return View();
        }

        public ActionResult ImageUploadAttachmentView()
        {
            return View();
        }

        public ActionResult CalendarView(string soId, string year, string month, string  EventId)
        {
            ViewData["soId"] = soId;
            ViewData["EventId"] = EventId;
            ViewData["month"] = month;
            ViewData["year"] = year;

            return View();
        }


        public ActionResult Memoview(string date, string day, string month, string year, string EventId, string soId, string acctCode)
        {

            ViewData["date"] = date;
            ViewData["day"] = day;
            ViewData["month"] = month;
            ViewData["year"] = year;
            //ViewData["EventId"] = EventId;
            ViewData["soId"] = soId;
            ViewData["acctCode"] = acctCode;


            return View();
        
        }

        

        public ActionResult Memo(string date, string day, string month, string year, string EventId, string soId, string acctCode)
        {
            string[] month_ = {"","JANUARY","FEBRUARY","MARCH","APRIL","MAY","JUNE","JULY","AUGUST","SEPTEMBER","OCTOBER","NOVEMBER","DECEMBER"};
            ViewData["date"] = date;
            ViewData["day"] = day;
            ViewData["month"] = month;
            ViewData["year"] = year;
            //ViewData["EventId"] = EventId;
            ViewData["soId"] = soId;
            ViewData["acctCode"] = acctCode;

            ViewData["enableCallReport"] = false;
            int int_year = int.Parse(year);
            var database = new Models.ARMSTestEntities();

          
            var qry = (from a in database.CoverageHdrs
                      where a.EmpIdNo == soId && a.Month == month && a.Year == int_year
                      select a).ToList();
            string action = string.Empty;


            foreach (var itm in qry)
            {

                if (itm.DocumentStatusId != 0)
                {
                    ViewData["enableCallReport"] = true;
                }

                action = acctCode == "" ? "Viewed Coverage Plan (" + month_[Int32.Parse(month)] + "/" + day + "/" + year + ")" : "Viewed Event Account Code:" + acctCode + " ( " + month_[Int32.Parse(month)] + "/" + day + "/" + year + ")";

                AppHelper.InsertActivityLog(
                  Session["username"].ToString(), action
                );
                break;
            }
           

            database.Dispose();

            //Session["date"] = date;
            return View();
        }

        public ActionResult WeeklyCalendar()
        {
            return View();
        }

        public ActionResult listofSoEvents()
        {
            return View();
        }

        public ActionResult ChangesDtls(string soId, string month, int year, string EventId, string SoName)
        {

            ViewData["month"] = month;
            ViewData["year"] = year;
            ViewData["EventId"] = EventId;
            ViewData["soId"] = soId;
            ViewData["SoName"] = SoName;

            return View();
        }

        public ActionResult MainCalendar()
        {
            return View();
        }

        public ActionResult CalendarViewer()
        {
            return View();
        }

        //inserted by billy jay delima
        public ActionResult UploadCoveragePreview(string counter_id, string event_year, string event_day, string event_month, string soId,string Eventdate)
        {
            ViewData["counter_id"] = counter_id;
            ViewData["event_year"] = event_year;
            ViewData["event_day"] = event_day;
            ViewData["event_month"] = event_month;
            ViewData["soId"] = soId;
            ViewData["Eventdate"] = Eventdate;
            return View();
        }

        public ActionResult UploadMonthlyCoveragePreview()
        {
            return View();
        }

        public ActionResult EventListApproveDisapprove()
        {
            return View();
        }

        public ActionResult EventListForASMApproval()
        {
            return View();
        }

        //end

        public JsonResult ListOfContactPerson(page_param.lookupContactPerson page_param)
        {
            List<string[]> res = new List<string[]>();
            var DATABASE = new Models.ARMSTestEntities();

           // var qry = (from a in DATABASE.CoverageHdrs
           //            from b in DATABASE.CoverageDtls
           //           where a.EventID == b.EventID && a.Month == page_param.Eventmonth
           //            && a.Year == page_param.Eventyear && a.EmpIdNo == page_param.soId && b.ContactPerson != null
           //            select new { b.ContactPerson, b.ContactPersonNo }).ToList();

            var qry = (from a in DATABASE.CoverageDtls
                       where a.AccountCode == page_param.acctCode
                        && a.ContactPerson != null
                       select new { a.ContactPerson, a.ContactPersonNo }).ToList();

            qry = qry.GroupBy(o => o.ContactPerson).Select(grp => grp.First()).OrderBy(o => o.ContactPerson.ToUpper()).ToList();

            foreach (var itm in qry)
            {
                res.Add(new string[] {
                        itm.ContactPerson.ToUpper(),
                        itm.ContactPersonNo
                    });
            }

            res=res.GroupBy(o=>o[0]).Select(grp => grp.First()).OrderBy(o => o[0].ToUpper()).ToList();

            DATABASE.Dispose();

            return Json(res);
        }

        #region TO REVIEW
        /**
        [HttpPost]
        public JsonResult SavePlanEvents(SkelClass.page_param.EventHdr p_param)
        {
            SQLTransaction mt_trans = new SQLTransaction();
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            string obj_code = "";

            try
            {
                mt_trans.StartTransaction();

                if (p_param.EventID == null || p_param.EventID == "")
                {
                    p_param.EventID = this.GetNewCode();
                }
                mt_trans.InsertTo("EventHdr", new Dictionary<string, object>() {
                {"EventID",p_param.EventID},
                {"EmpIdNo",p_param.EmpIdNo},
                {"Year",p_param.Year},
                {"Month",p_param.Month}
                });

                #region SAVING COLLECTION
                if (p_param.collection_list!=null)
                {


                    foreach (var itm in p_param.collection_list)
                    {
                        
                        
                       
                        try
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(p => p.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch(Exception ex)
                        {
                        
                        }

                        mt_trans.InsertTo("EventDtls", new Dictionary<string, object>() {
                        {"EventID", p_param.EventID},
                        {"Day",p_param.Day},
                        {"AccountCode",itm.AccountCode},
                        {"AccountName", itm.AccountName},
                        {"AccountAddress", itm.AccountAddress},
                        {"AccountClass",itm.AccountClass},
                        {"ObjectiveCode",obj_code},
                        {"ContactPerson",itm.ContactPerson},
                        {"ContactPersonNo", itm.ContactNumber}
                        
                        
                        });

                        mt_trans.InsertTo("EventDtl1", new Dictionary<string, object>() {
                        {"EventID", p_param.EventID},
                        {"ObjectiveCode",obj_code},
                        {"Brand", itm.Brand},
                        {"Amount",itm.Amount}
                        
                        });
                    
                    }

                }

                #endregion

                #region SAVING MERCHANDISING
                if (p_param.msde_list != null)
                {

                    foreach (var itm in p_param.msde_list)
                    {

                        try 
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(p => p.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch(Exception ex){
                        
                        }

                        mt_trans.InsertTo("EventDtls", new Dictionary<string, object>() {
                        {"EventID", p_param.EventID},
                        {"Day",p_param.Day},
                        {"AccountCode",itm.AccountCode},
                        {"AccountName",itm.AccountName},
                        {"AccountAddress", itm.AccountAddress},
                        {"AccountClass",itm.AccountClass},
                        {"ObjectiveCode",obj_code},
                        {"ContactPerson",itm.ContactPerson},
                        
                        }); 
                    
                    }

                }

                #endregion


                #region SAVING SALES

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

                        mt_trans.InsertTo("EventDtls", new Dictionary<string, object>() {
                        {"EventID", p_param.EventID},
                        {"Day",p_param.Day},
                        {"AccountCode",itm.AccountCode},
                        {"AccountName",itm.AccountName},
                        {"AccountAddress", itm.AccountAddress},
                        {"AccountClass",itm.AccountClass},
                        {"ObjectiveCode",obj_code},
                        {"ContactPerson",itm.ContactPerson},
                        {"ContactPersonNo", itm.ContactNumber}
                        
                        
                        });

                        mt_trans.InsertTo("EventDtl1", new Dictionary<string, object>() {
                        {"EventID", p_param.EventID},
                        {"ObjectiveCode",obj_code},
                        {"Brand", itm.Brand},
                        {"Amount",itm.Amount}
                        
                        });

                    }

                }


                #endregion

                #region SAVING COSTUMER SERVICE

                if (p_param.customersrvc_list != null)
                {

                    foreach (var itm in p_param.customersrvc_list)
                    {

                        try
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(p => p.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex)
                        {

                        }

                        mt_trans.InsertTo("EventDtls", new Dictionary<string, object>() {
                        {"EventID", p_param.EventID},
                        {"Day",p_param.Day},
                        {"AccountCode",itm.AccountCode},
                        {"AccountName",itm.AccountName},
                        {"AccountAddress", itm.AccountAddress},
                        {"AccountClass",itm.AccountClass},
                        {"ObjectiveCode",obj_code},
                        {"ContactPerson",itm.ContactPerson},
                        {"ContactPersonNo",itm.ContactPerson}
                        
                        });

                    }
                }


                #endregion

                mt_trans.Committransaction();
                return Json(SActionResult.Success);
            }
            catch (Exception ex)
            {

                //string errormsg = ex.InnerException.Message.ToString();
                mt_trans.RollbackTransaction();
                return Json(SActionResult.Error + ex.Message);
            }


           

        } **/

        #endregion

        #region SAVE PLAN EVENTS
        [HttpPost]
        public JsonResult SavePlanEvents(SkelClass.page_param.EventHdr p_param)
        {
            SQLTransaction sql_trans = new SQLTransaction();
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            string obj_code = "";
            bool isExist = false;
            string qry_evnt_id = "";
            //variavle holds for doctypeId
            int DoctypeId;
            //this variable force intialize document as Draft
            int Docstatus = 0;
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
               
                ARMS_W.Objects.Event.EventHdr new_event = new Objects.Event.EventHdr();
                DoctypeId = (int)ARMS_W.SkelClass.Globals.InfoType.CalendarEvent;
                isExist = ARMS_W.UserDefineFunctions.CalendarEvent.SoCalendar.IsExist(p_param.EmpIdNo, p_param.Year.ToString(), p_param.Month);
                new_event.isExist = isExist;
                if (isExist == true) { qry_evnt_id = DATABASE.EventHdrs.Single(p => p.EmpIdNo == p_param.EmpIdNo && p.Year == p_param.Year && p.Month == p_param.Month).EventID; } else { qry_evnt_id = null; }
                new_event.EventID = qry_evnt_id;
                new_event.EmpIdNo = p_param.EmpIdNo;
                new_event.Day = p_param.Day;
                new_event.Month = p_param.Month;
                new_event.Year = p_param.Year;
                new_event.DoctypeId = DoctypeId;
                new_event.DocumentStatusId = Docstatus;

                if(p_param.collection_list!=null)
                {
                foreach (var itm in p_param.collection_list)
                {
                    try 
                    {
                        obj_code = DATABASE.ObjectiveHdrs.Single(P => P.objectiveDesc == itm.Objdesc).objectiveCode;
                    }
                    catch (Exception ex) { }

                    new_event.collection_list.Add(new ARMS_W.Objects.Event.collection() 
                    {
                     AccountCode = itm.AccountCode,
                     AccountName = itm.AccountName,
                     AccountAddress = itm.AccountAddress,
                     AccountClass = itm.AccountClass,
                     ObjectiveCode = obj_code,
                     ContactPerson = itm.ContactPerson,
                     ContactNumber = itm.ContactNumber,
                     Brand = itm.Brand,
                     Amount = itm.Amount
                    
                    });
                
                }
            }

                if (p_param.msde_list != null) 
                {
                    foreach (var itm in p_param.msde_list)
                    {
                        try 
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(P => P.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex) { }

                       new_event.msde_list.Add(new ARMS_W.Objects.Event.Merchandise() 
                        {
                          AccountCode = itm.AccountCode,
                          AccountName = itm.AccountName,
                          AccountAddress= itm.AccountAddress,
                          AccountClass = itm.AccountClass,
                          ObjectiveCode = obj_code,
                          ContactPerson = itm.ContactPerson
                        
                        });
                    
                    }

                }

                if (p_param.sales_list != null)
                {
                    foreach (var itm in p_param.sales_list)
                    {
                        try
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(P => P.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex) { }

                        new_event.sales_list.Add(new ARMS_W.Objects.Event.Sales() 
                        {
                        
                          AccountCode = itm.AccountCode,
                          AccountName = itm.AccountName,
                          AccountAddress = itm.AccountAddress,
                          AccountClass = itm.AccountClass,
                          ObjectiveCode = obj_code,
                          ContactPerson = itm.ContactPerson,
                          ContactNumber = itm.ContactNumber,
                          Brand = itm.Brand,
                          Amount = itm.Amount
                        });
                    
                    }
                 
                      
                }

                if (p_param.customersrvc_list != null) 
                {
                    foreach (var itm in p_param.customersrvc_list)
                    {
                        try
                        {
                            obj_code = DATABASE.ObjectiveHdrs.Single(P => P.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex) { }

                        new_event.customersrvc_list.Add(new ARMS_W.Objects.Event.CustomerSrvc() 
                        {
                        
                              AccountCode = itm.AccountCode,
                              AccountName = itm.AccountName,
                              AccountAddress = itm.AccountAddress,
                              AccountClass = itm.AccountClass,
                              ObjectiveCode = obj_code,
                              ContactPerson = itm.ContactPerson,
                              ContactPersonNo = itm.ContactPersonNo
                        });

                    
                    }
                
                
                }

                if (act_type == Globals.DocActionType.None)
                {
                    new_event.UpdateToDB(ref sql_trans);
                }

                if (act_type == Globals.DocActionType.Approve)
                {

                 

                    new_event.UpdateDocStatus(ref sql_trans, p_param.EventID, act_type.ToString());
                }
                if (act_type == Globals.DocActionType.ReturnToRequestor)
                {
                    new_event.UpdateDocStatus(ref sql_trans, p_param.EventID, act_type.ToString());
                
                }
                   
                sql_trans.Committransaction();
                return Json(SActionResult.Success);



            
            }
            catch (Exception ex)
            { 
                 //string errormsg = ex.InnerException.Message.ToString();

                //string error = ex.InnerException.Message.ToString();
                sql_trans.RollbackTransaction();
                return Json(SActionResult.Error + ex.Message);
            
            }


           
        }
        #endregion

        #region GET EVENTS BY DATE

        [HttpPost]
        public JsonResult GetEventByDate(string userId, string month,int year)
        {
            string[] month_ = { "", "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;

            try 
            {
                ajx_result.iserror = false;
                var events = UserDefineFunctions.CalendarEvent.SoCalendar.GetEventInfo(userId, month, year);
                var doc_stat = UserDefineFunctions.CalendarEvent.SoCalendar.GetDocumentStatus(userId, month,year);
                var Eventid = UserDefineFunctions.CalendarEvent.SoCalendar.getEventId(userId, month, year);
                ajx_result.data = new  
                {
                    info = events,
                    docstatus =doc_stat,
                    EventId = Eventid
                };


                AppHelper.InsertActivityLog(
                 Session["username"].ToString(), "Viewed Monthly Work Plan Month of " + month_[Int32.Parse(month)] + ", " + year + ")"
               );
            
            }
            catch (Exception ex)
            {

                ajx_result.iserror = true;
                ajx_result.message = ex.Message;
            }
            return Json(ajx_result);
        
        }
        #endregion

        #region GET FOR CALL REPORT DTLS
        [HttpPost]
        public JsonResult GetForCallReport(string Eventmonth, int Eventday, int Eventyear, string soId, string ObjectiveCode)
        {
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;

            try
            {
                ajx_result.iserror = false;
                var events = UserDefineFunctions.CalendarEvent.SoCalendar.GetForCallreport(Eventmonth, Eventday, Eventyear, soId, ObjectiveCode);
                ajx_result.data = new
                {
                    info = events

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

        #region GET EVENT INFO BY DATE
        [HttpPost]
        public JsonResult GeteventInfobyDate(string Eventmonth, int Eventday, int Eventyear, string soId)
        {
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;

            try 
            {
                ajx_result.iserror = false;
                var eventsbydate = UserDefineFunctions.CalendarEvent.SoCalendar.GetEventInfobyDate(Eventmonth,Eventday,Eventyear,soId);

                ajx_result.data = new 
                {
                   events = eventsbydate
                
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




        #region Private Parts

        private string GetNewCode()
        {

            DataTable qry = SqlDbHelper.getDataDT("SELECT EventID FROM EventHdr");
            List<int> list_of_id = new List<int>();

            foreach (DataRow datarow in qry.Rows)
            {
                list_of_id.Add(Convert.ToInt32(datarow["EventID"].ToString().Replace("EVNT", "")));

            }
            if (list_of_id.Count > 0)
            {

                int largest_id = list_of_id.Max();
                return "EVNT" + string.Format("{0:0000000000}", largest_id + 1);
            
            }

            else
            {
                return "EVNT0000000000";
            }
        }

          #endregion

        #region GET SALES OFFICER LIST

        public JsonResult getSalesOfficerList(string userId)
        {
           
            List<string[]> SalesOfficer = ARMS_W.UserDefineFunctions.Application.getSO(userId).ToList();
           
            return Json(SalesOfficer);
        }

        #endregion

        #region GET EVENT LIST

        public JsonResult getEventList(string Eventmonth, int Eventday, int Eventyear, string soId, string ObjectiveCode)
        {
            string Eventid;
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> Events = ARMS_W.UserDefineFunctions.Application.getforCallreport(Eventmonth,Eventday,Eventyear,soId,ObjectiveCode);
            try 
            {
                Eventid = DATABASE.EventHdrs.Single(p => p.EmpIdNo == soId && p.Month == Eventmonth && p.Year == Eventyear).EventID; 
            }
            catch (Exception ex)
            {
                Eventid = "";
            }


            return Json(Events);
        }

        #endregion

        #region GET EVENT LIST

        public JsonResult getEventDtlList(string LineNum)
        {
            string Eventid;
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> Events = ARMS_W.UserDefineFunctions.Application.GetEventInfobyDateDtls(LineNum);

            return Json(Events);
        }

        #endregion


        #region WEEKLY CALENDAR

        [HttpPost]
        public JsonResult GetEventByDate_Weekly(string userId)
        {

            var ajx_res = new AjxResult();

            var Eventdate = new SkelClass.page_param.EventHdr();
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            var res = new List<page_param.EventDtl>();

            var fullcalendarevents = new List<page_param.fullcalendarEvents>();

            Nullable<DateTime> dateEvent = new DateTime();

            ajx_res.iserror = true;

            try
            {

                var qry = (from evntHdr in DATABASE.EventHdrs
                           from evntDtl in DATABASE.EventDtls
                           where evntHdr.EventID == evntDtl.EventID
                           && evntHdr.EmpIdNo == userId
                           select new
                           {
                               evntHdr.EventID,
                               evntHdr.Year,
                               evntHdr.Month,
                               evntDtl.Day,
                               evntDtl.AccountCode,
                               evntDtl.ObjectiveCode
                           }).ToList();


                foreach (var itm in qry)
                {
                    res.Add(new page_param.EventDtl()
                    {
                        EventID = itm.EventID,
                        Year = itm.Year,
                        Month = itm.Month,
                        Day = itm.Day,
                        AccountCode = itm.AccountCode,
                        ObjectiveCode = itm.ObjectiveCode



                    });

                    dateEvent = new DateTime(itm.Year, int.Parse(itm.Month), itm.Day);

                    fullcalendarevents.Add(new page_param.fullcalendarEvents()
                    {
                        title = itm.AccountCode,
                        start = dateEvent.Value.ToShortDateString(),
                        editable = true,
                        allday = true
                    });

                }

                ajx_res.iserror = false;

                ajx_res.data = new { fullcalendarEvents = fullcalendarevents };
            }
            catch (Exception ex)
            {
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
            }

            return Json(ajx_res);
        }

        #endregion


        [HttpPost]
        public JsonResult SaveCallReport(SkelClass.page_param.UpdateCallreport p_param)
        {

            SQLTransaction sql_trans = new SQLTransaction();
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();


            try 
            {
                sql_trans.StartTransaction();
                ARMS_W.Objects.Event.CallReport call_report = new Objects.Event.CallReport();

                string EventType="";
                string trimEventType = "";
              

                try 
                {
                    EventType = DATABASE.ObjectiveHdrs.Single(p => p.objectiveCode == p_param.ObjectiveCode).objectiveDesc;
                }
                catch (Exception ex)
                {
                    EventType = "";
                }
                trimEventType = Regex.Replace(EventType.ToString(), @"\s+", ""); //EventType.Replace(EventType,@"\s+","");

                #region IF COLLECTION
                if (Globals.EventType.Collection.ToString() == EventType)
                {
                    call_report.EventID = p_param.EventID;
                    call_report.AccountCode = p_param.AccountCode;
                    call_report.ObjectiveCode = p_param.ObjectiveCode;
                    call_report.Day = (int)p_param.Day;
                    call_report.cFullCollection = p_param.cFullCollection;
                    call_report.cPartialCollection = p_param.cPartialCollection;
                    call_report.cNoCollection = p_param.cNoCollection;
                    call_report.EventDesc = EventType;



                }
                #endregion

                #region IF Merchandise

                if (Globals.EventType.Merchandising.ToString() == EventType)
                {
                    call_report.EventID = p_param.EventID;
                    call_report.AccountCode = p_param.AccountCode;
                    call_report.ObjectiveCode = p_param.ObjectiveCode;
                    call_report.Day = (int)p_param.Day;
                    call_report.EventDesc = EventType;
                    call_report.StoreCheckingResult = p_param.StoreCheckingResult;
                    call_report.LineNum = p_param.LineNum;


                    if (p_param.Presented_list != null)
                    {
                        foreach (var itm in p_param.Presented_list)
                        {

                            call_report.Presented_list.Add(new ARMS_W.Objects.Event.presented {
                            
                                 Brand = itm.Brand,
                                 CounterClerk = itm.CounterClerk

                                    
                            
                            
                            });
                          

                        }


                    }


                }
                #endregion

                #region IF SALES

                if (Globals.EventType.Sales.ToString() == EventType)
                {

                    call_report.EventID = p_param.EventID;
                    call_report.AccountCode = p_param.AccountCode;
                    call_report.ObjectiveCode = p_param.ObjectiveCode;
                    call_report.Day = (int)p_param.Day;
                    call_report.EventDesc = EventType;
                    call_report.WithOrder = p_param.WithOrder;
                    call_report.OtherInformation = p_param.OtherInformation;
                    call_report.NextCalldate = p_param.NextCalldate;
                    call_report.CompetitorActivities = p_param.CompetitorActivities;

                }

                #endregion

                #region IF CUSTOMER SERVICE

                if (Globals.EventType.CustomerService.ToString() == trimEventType)
                {
                    call_report.EventID = p_param.EventID;
                    call_report.AccountCode = p_param.AccountCode;
                    call_report.ObjectiveCode = p_param.ObjectiveCode;
                    call_report.Day = (int)p_param.Day;
                    call_report.EventDesc = EventType;
                    call_report.IssuesAndConcerns = p_param.IssuesAndConcerns;
                    call_report.Delivery = p_param.Delivery;
                    call_report.Orders = p_param.Orders;
                    call_report.SummaryLackingItems = p_param.SummaryLackingItems;
                    call_report.Remarks = p_param.Remarks;
                    call_report.Recommendation = p_param.Recommendation;
                    call_report.TimeTable = p_param.TimeTable;
                    call_report.EventDesc = trimEventType;


                #endregion

                }



                call_report.UpdateToDB(ref sql_trans);

                sql_trans.Committransaction();
                return Json(SActionResult.Success);
            
            }

               
            catch(Exception ex)
            {

                sql_trans.RollbackTransaction();
                return Json(SActionResult.Error + ex.Message);
            }

        }


        [HttpPost]
        public JsonResult SaveCallReportHasForceObj(SkelClass.page_param.UpdateCallreportHasForceObjective p_param)
        {
            SQLTransaction sql_trans = new SQLTransaction();
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();


            try { 

                 sql_trans.StartTransaction();
                ARMS_W.Objects.Event.CallReport call_report = new Objects.Event.CallReport();

                string EventType = "";
                string trimEventType = "";
                string eventid = "";

                try { eventid = DATABASE.EventHdrs.Single(p => p.EmpIdNo == p_param.SoId && p.Month == p_param.Month && p.Year == p_param.Year).EventID; }
                catch (Exception ex)
                { eventid = ""; }

                call_report.EventID = eventid;
                call_report.Day = p_param.Day;
                if (p_param.forceObjective_list != null)
                {
                    foreach (var itm in p_param.forceObjective_list)
                    {
                        call_report.forceObjective_list.Add(new ARMS_W.Objects.Event.forceObjective() {
                        
                         Objdesc = itm.Objdesc,
                         ObjectiveCode = itm.ObjectiveCode,
                         AccountClass = itm.AccountClass,
                         AccountAddress =itm.AccountAddress,
                         AccountName = itm.AccountName,
                         ContactPerson = itm.ContactPerson,
                         ContactNumber = itm.ContactNumber,
                         cFullCollection = itm.cFullCollection,
                         cPartialCollection = itm.cPartialCollection,
                         cNoCollection =itm.cNoCollection,
                         AccountCode = itm.AccountCode,
                         Brand = itm.Brand,
                         Amount =itm.Amount
                        });
                    
                    }
                }

                call_report.UpdateToDB1(ref sql_trans);

                sql_trans.Committransaction();
                return Json(SActionResult.Success);
            
            }
            catch (Exception ex)
            {

                sql_trans.RollbackTransaction();
                return Json(SActionResult.Error + ex.Message);
            
            }

        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadAttachment(string EventID = null, string LineNum = null, string empId=null)
        {
            var DATABASE = new Models.ARMSTestEntities();
            string day, month, year, soId, date;

            var qry_hdr = DATABASE.CoverageHdrs.Single(p => p.EventID == EventID);

            var qry = DATABASE.CoverageDtls.Single(p => p.EventID == EventID && p.LineNum == LineNum);


            day = qry.Day.ToString();
            month = qry_hdr.Month;
            year = qry_hdr.Year.ToString();

            DateTime _date = new DateTime(qry_hdr.Year, int.Parse(qry_hdr.Month), qry.Day);
            date = _date.ToString();
            soId = empId;

            
            foreach (string inputTagName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[inputTagName];
                if (file.ContentLength > 0)
                {
                    string mimeType = Request.Files[inputTagName].ContentType;
                    Stream fileStream = Request.Files[inputTagName].InputStream;
                    string fileName = Path.GetFileName(Request.Files[inputTagName].FileName);
                    int fileLength = Request.Files[inputTagName].ContentLength;
                    byte[] fileData = new byte[fileLength];
                    fileStream.Read(fileData, 0, fileLength);


                    qry.FileAttachment = fileName;
                    qry.AttachmentContent = fileData;
                    qry.AttachmentType = mimeType;
                }
            }
            try
            {
                DATABASE.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(ex.InnerException.Message); }
            finally
            {
                DATABASE.Dispose();
            }
            return RedirectToAction("Memo", "Calendar", new { date = date, day = day, month = month, year = year, soId = soId });
        }

        public FileContentResult getAttachment(string EventID = null, string LineNum = null)
        {
            var DATABASE = new Models.ARMSTestEntities();

            var callrep = DATABASE.CoverageDtls.Single(o => o.EventID == EventID && o.LineNum == LineNum);

            byte[] filecontent = (byte[])callrep.AttachmentContent;
            string mimetype = callrep.AttachmentType,
                   filename = callrep.FileAttachment;

            DATABASE.Dispose();
            return File(filecontent, mimetype, filename);
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public string getSOMonthlyCoverageRouteChanges(string EventMonth, string EventYear, string EmpIDNo)
        {
            string[] month = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            System.Text.StringBuilder table_builder = new System.Text.StringBuilder(2000);

            var EventID = UserDefineFunctions.CalendarEvent.SoCalendar.getEventID_(EmpIDNo, month[Int16.Parse(EventMonth)], Int16.Parse(EventYear));
            var route_changes = DataChanges.GetRouteChanges(Globals.InfoType.CalendarEvent, EventID).OrderByDescending(c=>c.DateTimeStamp).ToList();

            string a_style = "font-weight:bold; font-size:11px; background:#ededed; padding:2px;";

            // db = db.OrderByDescending(o => o.DateTimeStamp).ToList();
            table_builder.Append("<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\" width=\"100%\" >");
            table_builder.Append("<tr>");
            table_builder.Append("<td style=\"" + a_style + "\"> Approver/User </td>");
            table_builder.Append("<td style=\"" + a_style + "\"> Position </td>");
            table_builder.Append("<td style=\"" + a_style + "\"> Date </td>");
            table_builder.Append("<td style=\"" + a_style + "\"> Action </td>");
            table_builder.Append("<td style=\"" + a_style + "\"> Remarks </td>");
            table_builder.Append("</tr>");
            foreach (var itm in route_changes.Where(p=>p.Remarks.Contains("Batch") || p.Remarks.Contains("MCP")))
            {
                // user name and time stamp
                table_builder.Append("<tr>");
                table_builder.Append("<td>").Append(itm.UserName).Append("</td>");

                // table_builder.Append("<td style=\"" + c_style + "\" >").Append("<b>Previous Doc Status: </b>" + itm.PrevDocStatus).Append("</td>");

                // table_builder.Append("<td style=\"" + c_style + "\" >").Append("<b>Current Doc Status: </b>" + itm.CurDocStatus).Append("</td>");

                table_builder.Append("<td>").Append(itm.PositionName).Append("</td>");

                table_builder.Append("<td>").Append(itm.DateTimeStamp).Append("</td>");

                table_builder.Append("<td>").Append(itm.Action).Append("</td>");

                table_builder.Append("<td>").Append(itm.Remarks).Append("</td>");
                table_builder.Append("</tr>");
            }

            table_builder.Append("</table>");
            return table_builder.ToString();
        }

        public JsonResult lookUpSalesOfficerEmployee(string EmpId)
        {
            var DATABASE = new Models.ARMSTestEntities();
            List<string[]> SOemployee = new List<string[]>();

           // var current_user = new _User(Session["username"].ToString());

            //int roleID = 0;

            //var userrole = (from a in DATABASE.apprvrDesigs
            //              from b in DATABASE.userHeaders
            //              from c in DATABASE.apprvrRoles
            //              where a.counterId == b.counterId && b.empIdNo == EmpId
            //                && a.roleID == c.roleID
            //              select new {c.roleID,c.roleCode});

            //if (userrole.Any(o => o.roleCode == "SPRUSER")) { roleID=userrole.Single(o=> o.roleCode == "SPRUSER").roleID;}
            //else if (userrole.Any(o => o.roleCode == "CHM")) { roleID = userrole.Single(o => o.roleCode == "CHM").roleID; }
            //else if (userrole.Any(o => o.roleCode == "ASM")) { roleID = userrole.Single(o => o.roleCode == "ASM").roleID; }
            //else if (userrole.Any(o => o.roleCode == "SO")) { roleID = userrole.Single(o => o.roleCode == "SO").roleID; }

           // var qry1 = (from a in DATABASE.CoverageHdrs
            //           join b in DATABASE.userHeaders on a.EmpIdNo equals b.empIdNo
             //          select new { a.EmpIdNo, b.firstName, b.lastName }).Distinct();

            /*
            var qry = (from a in DATABASE.apprvrDesigs
                        join b in DATABASE.userHeaders on a.counterId equals b.counterId
                        where a.roleID == 17
                        select new { b.empIdNo, b.firstName, b.lastName }).Distinct();

            foreach (var itm in qry)
            {
                SOemployee.Add(new string[]{
                    itm.empIdNo,
                    itm.lastName.ToUpper()+", "+itm.firstName.ToUpper()
                });
            }
            */

            var qry = UserDefineFunctions.Application.ListOfSalesOfficer(Session["userid"].ToString());
            var qry2 = from a in qry
                       where (from b in DATABASE.CoverageHdrs
                              select b.EmpIdNo).Contains(a.empIDNo)
                       select a;

            foreach(var itm in qry2)//UserDefineFunctions.Application.ListOfSalesOfficer(Session["userid"].ToString()))
            {
                SOemployee.Add(new string[]{
                    itm.empIDNo,
                    itm.empFullName
                });
            }



            SOemployee = SOemployee.OrderBy(o => o[1]).ToList();

            DATABASE.Dispose();
            return Json(SOemployee);
        }

        public JsonResult GetEventDetails(string userId, string month, int year, string viewtype, int day = 1)
        {
            var ajx_res = new AjxResult();
            try
            {
                ajx_res.data = new
                {
                    list_of_events = UserDefineFunctions.CalendarEvent.SoCalendar.GetAllDayEvents_viewer(userId, month, year, viewtype, day),
                    calendar_info = UserDefineFunctions.CalendarEvent.SoCalendar.GetCalendarInfo(userId, month, year),
                    sales_info = UserDefineFunctions.CalendarEvent.SoCalendar.getSalesMonitoringInfo(userId)
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

        public String GetSoList()
        {
            StringBuilder sb = new StringBuilder();
            var DATABASE = new Models.ARMSTestEntities();
            //List<Arms_vw_SOLookUps> so = db.Arms_vw_SOLookUps.Where(p => p.position=="SO" && p.status=="ACTIVE" &&p.EmpidNo==id).ToList();
            

            //var DATABASE = new ArmsMobile.Models.ARMSEntities();
            var qry = (from user in DATABASE.userHeaders
                       from approverDesig in DATABASE.apprvrDesigs
                       from apprvrRle in DATABASE.apprvrRoles
                       where user.status == "ACTIVE"
                       && apprvrRle.roleCode=="SO"
                       && user.counterId == approverDesig.counterId && apprvrRle.roleID == approverDesig.roleID
                       select user
                      
                       );

           // qry.Distinct(p => ).First();

            //List<userHeader> so = DATABASE.userHeaders.Where(p => p.empIdNo==).OrderBy(p => p.lastName).ToList();

            sb.Append("<option value=\"\" disabled selected>Choose SO Name</option>");
            foreach (var data in qry.OrderBy(p=>p.lastName).ToList())
            {
                sb.Append("<option value=\"" + data.empIdNo + "\">" + data.lastName.ToUpper()+", "+data.firstName.ToUpper() + "</option>");
            }
            return sb.ToString();
        }

        public class JQueryDataTableParamModel
        {
            public string sEcho { get; set; }
            public string sSearch { get; set; }
            public int iDisplayLength { get; set; }
            public int iDisplayStart { get; set; }
        }

        public JsonResult GetAjaxData(JQueryDataTableParamModel param)
        {
            using (var e = new ARMSTestEntities())
            {
                var totalRowsCount = new System.Data.Objects.ObjectParameter("TotalRowsCount", typeof(string));
                var filteredRowsCount = new System.Data.Objects.ObjectParameter("FilteredRowsCount", typeof(string));
                var data = e.armsmobile_proc_get_uploadattachment_Result(param.sSearch,
                               Convert.ToInt32(Request["iSortCol_0"]),
                               Request["sSortDir_0"],
                                param.iDisplayStart,
                                param.iDisplayStart + param.iDisplayLength,
                                totalRowsCount,
                                filteredRowsCount);

                var aaData = data.Select(d => new string[] { d.eventid, d.accountcodename, ConvertByteToStrings(d.po, d.pofiletype), ConvertByteToStrings(d.selfie, d.selfiefiletype), ConvertByteToStrings(d.warehouse, d.warehousefiletype), ConvertByteToStrings(d.competition, d.competitionfiletype) }).ToArray();



                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = aaData,
                    iTotalRecords = Convert.ToInt32(totalRowsCount.Value),
                    iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount.Value)
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public string ConvertByteToStrings(byte[] source, string filetype)
        {
            return source != null ? "<img src="+"data:"+filetype+";base64,"+ Convert.ToBase64String(source)+">" : null;
        }

    }
}
