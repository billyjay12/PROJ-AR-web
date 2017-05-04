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
using System.Text;

namespace ARMS_W.Controllers
{
    public partial class CalendarController : Controller
    {

        [HttpPost]
        public JsonResult UpdateCallreports(SkelClass.page_param.CoverageHdr p_param)
        {

            SQLTransaction sql_trans = new SQLTransaction();
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();


            try {

                sql_trans.StartTransaction();
                ARMS_W.Objects.Event.CoverageHdr call_report = new Objects.Event.CoverageHdr();

                string event_id="";
                try{
                    event_id = DATABASE.CoverageHdrs.Single(o=> o.EmpIdNo == p_param.EmpIdNo && o.Year==p_param.Year && o.Month == p_param.Month).EventID; 
                }
                catch(Exception ex)
                {
                
                
                }

                Interface.Event.CoverageHdr coverage_from_db = null;

                //List<SkelClass.Globals.db_changes> qry_changes = (List<SkelClass.Globals.db_changes>);
                List<SkelClass.Globals.db_changes> changes_in_data = new List<SkelClass.Globals.db_changes>();
                try
                {
                  //  coverage_from_db = UserDefineFunctions.Application.GetCoverageDetails(p_param.EventID, p_param.Day, p_param.Month, p_param.Year, p_param.AccountCode);
                    coverage_from_db = UserDefineFunctions.Application.GetCoverageDetails(event_id == "" ? p_param.EventID : event_id, p_param.Day, p_param.Month, p_param.Year, p_param.AccountCode);
                }
                catch (Exception ex)
                {
                    coverage_from_db = null;
                }

                        
                 /**List<SkelClass.Globals.db_changes> qry_changes = (List<SkelClass.Globals.db_changes>)
                     (from coverage_hdr in DATABASE.CoverageHdrs
                      from coverage_dtls in DATABASE.CoverageDtls
                      from coverage_dtl1 in DATABASE.CoverageDtl1
                      where coverage_hdr.EventID == coverage_dtls.EventID
                      && **/


                //List<SkelClass.Globals.db_changes> qry_changes = (List<SkelClass.Globals.db_changes>)
                // (from coverage.

                //List<SkelClass.Globals.db_changes> qry_changes = (List<SkelClass.Globals.db_changes>)
               //LoyaltyLib.Interface.Maestro.MemberHdr member_hdr_from_db = LoyaltyLib.Application.Maestro.GetMaestroCustomernfo(page_param.MaestroID);
              //  Interface.Event.CoverageHdr coverage_dtl_db=  UserDefineFunctions.Application.GetCoverageDetails(p_param.EventID,p_param.Day,p_param.Month,p_param.Year,p_param.AccountCode);

                 // ARMS_W.Interface.Event.CoverageHdr coverage_dtl_db = UserDefineFunctions.Application.GetCoverageDetails(p_param.EventID,p_param.Day,p_param.Month,p_param.Year,p_param.AccountCode);


                /**var qry_exist = (from cvrgehrd in DATABASE.CoverageHdrs
                                 from cvrgedtl in DATABASE.CoverageDtls
                                 where cvrgehrd.EventID == event_id
                                 && cvrgehrd.Year == p_param.Year && cvrgehrd.Month == p_param.Month
                                 && cvrgedtl.Day== p_param.Day && cvrgedtl.AccountCode == p_param.AccountCode
                                 &&cvrgehrd.EmpIdNo == p_param.EmpIdNo
                 
                                 select new { 
                                 
                                   cvrgehrd.EventID ,
                                   cvrgedtl.LineNum,
                                   cvrgedtl.AccountCode,
                                   cvrgedtl.Day
                                 }).ToList(); **/

                var qry_exist = (from a in DATABASE.CoverageDtls
                                 from b in DATABASE.CoverageHdrs
                                 where a.EventID == event_id && a.EventID == event_id
                                 && b.Year == p_param.Year && b.Month == p_param.Month
                                 && a.AccountCode == p_param.AccountCode && b.EmpIdNo == p_param.EmpIdNo
                                 && a.Day == p_param.Day
                                 select new {

                                     a.EventID,
                                     a.LineNum,
                                     a.AccountCode,
                                     a.Day
                                 
                                 }).ToList();

                if (coverage_from_db != null)
                {

                    #region HEADER OF DETAILS

                    if (coverage_from_db.ContactPerson != p_param.ContactPerson)
                    {

                        coverage_from_db.ContactPerson = p_param.ContactPerson;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Contact Person",
                            PrevValue = coverage_from_db.ContactPerson,
                            NewValue = p_param.ContactPerson

                        });
                    }

                    if (coverage_from_db.ContactPersonNo != p_param.ContactPersonNo)
                    {

                        coverage_from_db.ContactPersonNo = p_param.ContactPersonNo;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Contact Number",
                            PrevValue = coverage_from_db.ContactPersonNo,
                            NewValue = p_param.ContactPersonNo

                        });
                    }

                    if (coverage_from_db.HotelContactNum != p_param.HotelContactNum)
                    {

                        coverage_from_db.HotelContactNum = p_param.HotelContactNum;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Hotel Contact No.",
                            PrevValue = coverage_from_db.HotelContactNum,
                            NewValue = p_param.HotelContactNum

                        });
                    }

                    if (coverage_from_db.HotelName != p_param.HotelName)
                    {

                        coverage_from_db.HotelName = p_param.HotelName;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Hotel Name",
                            PrevValue = coverage_from_db.HotelName,
                            NewValue = p_param.HotelName

                        });
                    }

                    if (coverage_from_db.Numvisit != p_param.Numvisit)
                    {

                        coverage_from_db.Numvisit = p_param.Numvisit;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Frequency of Visit",
                            PrevValue = coverage_from_db.Numvisit.ToString(),
                            NewValue = p_param.Numvisit.ToString()

                        });
                    }

                  /**  if (coverage_from_db.Tmein != p_param.Tmein)
                    {

                        coverage_from_db.Tmein = p_param.Tmein;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Time In",
                            PrevValue = coverage_from_db.Tmein.ToString(),
                            NewValue = p_param.Tmein.ToString()

                        });
                    }

                    if (coverage_from_db.Tmeout != p_param.Tmeout)
                    {

                        coverage_from_db.Tmeout = p_param.Tmeout;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Time Out",
                            PrevValue = coverage_from_db.Tmeout.ToString(),
                            NewValue = p_param.Tmeout.ToString()

                        });
                    }**/




                    #endregion


                    #region COLLECTION CHECK BOX

                    if (coverage_from_db.cFullCollection != p_param.cFullCollection)
                    {

                        coverage_from_db.cFullCollection = p_param.cFullCollection;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Full Collection",
                            PrevValue = coverage_from_db.cFullCollection,
                            NewValue = p_param.cFullCollection

                        });
                    }

                    if (coverage_from_db.cNoCollection != p_param.cNoCollection)
                    {

                        coverage_from_db.cNoCollection = p_param.cNoCollection;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "No Collection",
                            PrevValue = coverage_from_db.cNoCollection,
                            NewValue = p_param.cNoCollection

                        });
                    }

                    if (coverage_from_db.cPartialCollection != p_param.cPartialCollection)
                    {

                        coverage_from_db.cPartialCollection = p_param.cPartialCollection;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Partial Collection",
                            PrevValue = coverage_from_db.cPartialCollection,
                            NewValue = p_param.cPartialCollection

                        });
                    }

                    #endregion

                    #region MONTH, DAY, YEAR

                    if (coverage_from_db.Month != p_param.Month)
                    {

                        coverage_from_db.Month = p_param.Month;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Month",
                            PrevValue = coverage_from_db.Month,
                            NewValue = p_param.Month

                        });
                    }

                    if (coverage_from_db.Day != p_param.Day)
                    {

                        coverage_from_db.Day = p_param.Day;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Day",
                            PrevValue = coverage_from_db.Day.ToString(),
                            NewValue = p_param.Day.ToString()

                        });
                    }


                    if (coverage_from_db.Year != p_param.Year)
                    {

                        coverage_from_db.Year = p_param.Year;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Year",
                            PrevValue = coverage_from_db.Year.ToString(),
                            NewValue = p_param.Year.ToString()

                        });
                    }

                    #endregion

                    #region  ACCOUNTCODE, EVENTID, LINENUM


                    if (coverage_from_db.AccountCode != p_param.AccountCode)
                    {

                        coverage_from_db.AccountCode = p_param.AccountCode;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Accountcode",
                            PrevValue = coverage_from_db.AccountCode,
                            NewValue = p_param.AccountCode

                        });
                    }

                    if (coverage_from_db.EventID != p_param.EventID)
                    {

                        coverage_from_db.EventID = p_param.EventID;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "EventID",
                            PrevValue = coverage_from_db.EventID,
                            NewValue = p_param.EventID

                        });
                    }

                    if (coverage_from_db.LineNum != p_param.LineNum)
                    {

                        coverage_from_db.LineNum = p_param.LineNum;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "LineNum",
                            PrevValue = coverage_from_db.LineNum,
                            NewValue = p_param.LineNum

                        });
                    }


                    #endregion

                    #region MSDE

                    if (coverage_from_db.StoreChecking != p_param.StoreChecking)
                    {

                        coverage_from_db.StoreChecking = p_param.StoreChecking;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Store Checking",
                            PrevValue = coverage_from_db.StoreChecking,
                            NewValue = p_param.StoreChecking

                        });
                    }


                    if (coverage_from_db.StoreCheckingResult != p_param.StoreCheckingResult)
                    {

                        coverage_from_db.StoreCheckingResult = p_param.StoreCheckingResult;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Store Checking Result",
                            PrevValue = coverage_from_db.StoreCheckingResult,
                            NewValue = p_param.StoreCheckingResult

                        });
                    }







                    #endregion

                    #region SALES

                    if (coverage_from_db.CompetitorActivities != p_param.CompetitorActivities)
                    {

                        coverage_from_db.CompetitorActivities = p_param.CompetitorActivities;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Competitor Activities",
                            PrevValue = coverage_from_db.CompetitorActivities,
                            NewValue = p_param.CompetitorActivities

                        });
                    }

                    if (coverage_from_db.Orders != p_param.Orders)
                    {

                        coverage_from_db.Orders = p_param.Orders;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Orders",
                            PrevValue = coverage_from_db.Orders,
                            NewValue = p_param.Orders

                        });
                    }

                    if (coverage_from_db.NextCallDate != p_param.NextCallDate)
                    {

                        coverage_from_db.NextCallDate = p_param.NextCallDate;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Next Call Date",
                            PrevValue = coverage_from_db.NextCallDate,
                            NewValue = p_param.NextCallDate

                        });
                    }

                    if (coverage_from_db.OtherInformation != p_param.OtherInformation)
                    {

                        coverage_from_db.OtherInformation = p_param.OtherInformation;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Other Information",
                            PrevValue = coverage_from_db.OtherInformation,
                            NewValue = p_param.OtherInformation

                        });
                    }

                    if (coverage_from_db.Attachment != p_param.Attachment)
                    {

                        coverage_from_db.Attachment = p_param.Attachment;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "P.O Attachment",
                            PrevValue = coverage_from_db.Attachment,
                            NewValue = p_param.Attachment

                        });
                    }



                    #endregion

                    #region Customer Service

                    if (coverage_from_db.IssuesAndConcerns != p_param.IssuesAndConcerns)
                    {

                        coverage_from_db.IssuesAndConcerns = p_param.IssuesAndConcerns;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Issues and Concerns",
                            PrevValue = coverage_from_db.IssuesAndConcerns,
                            NewValue = p_param.IssuesAndConcerns

                        });
                    }

                    if (coverage_from_db.Delivery != p_param.Delivery)
                    {

                        coverage_from_db.Delivery = p_param.Delivery;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Delivery",
                            PrevValue = coverage_from_db.Delivery,
                            NewValue = p_param.Delivery

                        });
                    }

                    if (coverage_from_db.Orders != p_param.Orders)
                    {

                        coverage_from_db.Orders = p_param.Orders;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Orders",
                            PrevValue = coverage_from_db.Orders,
                            NewValue = p_param.Orders

                        });
                    }

                    if (coverage_from_db.SummaryLackingItems != p_param.SummaryLackingItems)
                    {

                        coverage_from_db.SummaryLackingItems = p_param.SummaryLackingItems;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Summary of Lacking Items",
                            PrevValue = coverage_from_db.SummaryLackingItems,
                            NewValue = p_param.SummaryLackingItems

                        });
                    }


                    if (coverage_from_db.Remarks != p_param.Remarks)
                    {

                        coverage_from_db.Remarks = p_param.Remarks;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Remarks",
                            PrevValue = coverage_from_db.Remarks,
                            NewValue = p_param.Remarks

                        });
                    }

                    if (coverage_from_db.Recommendation != p_param.Recommendation)
                    {

                        coverage_from_db.Recommendation = p_param.Recommendation;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Recommendation",
                            PrevValue = coverage_from_db.Recommendation,
                            NewValue = p_param.Recommendation

                        });
                    }

                    if (coverage_from_db.TimeTable != p_param.TimeTable)
                    {

                        coverage_from_db.TimeTable = p_param.TimeTable;

                        changes_in_data.Add(new Globals.db_changes()
                        {
                            FieldName = "Time Table",
                            PrevValue = coverage_from_db.TimeTable,
                            NewValue = p_param.TimeTable

                        });
                    }




                    #endregion

                }






                call_report.qryExist = qry_exist.Count();
                call_report.cFullCollection = p_param.cFullCollection;
                call_report.cNoCollection = p_param.cNoCollection;
                call_report.cPartialCollection =p_param.cPartialCollection;
                call_report.Month = p_param.Month;
                call_report.Day = p_param.Day;
                call_report.Year = p_param.Year;
                call_report.AccountCode = p_param.AccountCode;
                call_report.EventID = event_id;
                call_report.LineNum = p_param.LineNum == null ? DATABASE.CoverageDtls.Single(o => o.EventID == event_id && o.Day == p_param.Day && o.AccountCode == p_param.AccountCode).LineNum : p_param.LineNum;
                call_report.StoreCheckingResult = p_param.StoreCheckingResult;
                call_report.Delivery = p_param.Delivery;
                call_report.Orders = p_param.Orders;
                call_report.SummaryLackingItems = p_param.SummaryLackingItems;
                call_report.Recommendation = p_param.Recommendation;
                call_report.TimeTable = p_param.TimeTable;
                call_report.Remarks = p_param.Remarks;
                call_report.CompetitorActivities = p_param.CompetitorActivities;
                call_report.WithOrder = p_param.WithOrder;
                call_report.NextCallDate = p_param.NextCallDate;
                call_report.OtherInformation = p_param.OtherInformation;
                call_report.IssuesAndConcerns = p_param.IssuesAndConcerns;
                call_report.StoreChecking = p_param.StoreChecking;
                /**call_report.Tmein = p_param.Tmein;
                call_report.Tmeout = p_param.Tmeout;**/
                call_report.Numvisit = p_param.Numvisit;
                call_report.ContactPerson = p_param.ContactPerson;
                call_report.ContactPersonNo = p_param.ContactPersonNo;
                call_report.HotelContactNum = p_param.HotelContactNum;
                call_report.HotelName = p_param.HotelName;
                call_report.ColPostDatedCheck = p_param.ColPostDatedCheck;
                call_report.ColDatedCheck = p_param.ColDatedCheck;
                call_report.ColTotal = p_param.ColTotal;
                call_report.ColRemarks = p_param.ColRemarks;


                if (p_param.collection_list != null)
                {
                    foreach (var itm in p_param.collection_list)
                    {
                        call_report.collection_list.Add(new ARMS_W.Objects.Event.collections() {
                        
                            ObjectiveCode = itm.ObjectiveCode,
                            Brand = itm.Brand,
                            Amount = itm.Amount,
                            ActualAmount = itm.ActualAmount,
                            Remarks = itm.Remarks

                        });
                    
                    }
                
                }

                if(p_param.uncollection_list!=null)
                {
                   foreach (var itm in p_param.uncollection_list)
                    {
                        call_report.uncollection_list.Add(new ARMS_W.Objects.Event.collections() {
                        
                            ObjectiveCode = itm.ObjectiveCode,
                            Brand = itm.Brand,
                            Amount = itm.Amount,
                            ActualAmount = itm.ActualAmount,
                            Remarks = itm.Remarks

                        });
                    
                    }
                
                }

                if (p_param.merchandising_list != null)
                {

                    foreach (var itm in p_param.merchandising_list)
                    {
                        call_report.merchandising_list.Add(new ARMS_W.Objects.Event.merchandising() {
                        
                            ObjectiveCode = itm.ObjectiveCode,
                            Productpresented = itm.Productpresented,
                            counterclerk = itm.counterclerk,
                            CounterClerkNo = itm.CounterClerkNo,
                            Remarks = itm.Remarks
                        
                        });
                    
                    }
                
                }


                if (p_param.unmerchandising_list != null)
                {

                    foreach (var itm in p_param.unmerchandising_list)
                    {
                        call_report.unmerchandising_list.Add(new ARMS_W.Objects.Event.merchandising()
                        {

                            ObjectiveCode = itm.ObjectiveCode,
                            Productpresented = itm.Productpresented,
                            counterclerk = itm.counterclerk,
                            CounterClerkNo = itm.CounterClerkNo,
                            Remarks = itm.Remarks

                        });

                    }

                }

                //sales
                if (p_param.sales_list != null)
                {
                    foreach (var itm in p_param.sales_list)
                    {
                        call_report.sales_list.Add(new ARMS_W.Objects.Event.sales()
                        {

                            ObjectiveCode = itm.ObjectiveCode,
                            Brand = itm.Brand,
                            Amount = itm.Amount,
                            ActualAmount = itm.ActualAmount,
                            Remarks = itm.Remarks

                        });

                    }

                }

                if (p_param.unsales_list != null)
                {
                    foreach (var itm in p_param.unsales_list)
                    {
                        call_report.unsales_list.Add(new ARMS_W.Objects.Event.sales()
                        {

                            ObjectiveCode = itm.ObjectiveCode,
                            Brand = itm.Brand,
                            Amount = itm.Amount,
                            ActualAmount = itm.ActualAmount,
                            Remarks = itm.Remarks,
                            dtlsRrmks = itm.dtlsRrmks

                        });

                    }

                }

                if (p_param.customersrv_list != null)
                {

                    foreach (var itm in p_param.customersrv_list)
                    {
                        string obj_code = "";

                        try
                        {
                             obj_code = DATABASE.ObjectiveHdrs.Single(p => p.objectiveDesc == itm.Objdesc).objectiveCode;
                        }
                        catch (Exception ex)
                        {

                        }
                        call_report.customersrv_list.Add(new ARMS_W.Objects.Event.customersrvc() {

                            ObjectiveCode = obj_code,
                            Brand = itm.Brand,
                            Amount = itm.Amount

                        });
                    
                    }
                }


                if (changes_in_data.Count > 0)
                    DataChanges.LogChanges(ref sql_trans, changes_in_data, (int)Globals.InfoType.CalendarEvent, Session["username"].ToString(),p_param.LineNum);
                call_report.UpdateDBCallReport(ref sql_trans);

                sql_trans.Committransaction();
                return Json(SActionResult.Success);



            
            
            }
            catch (Exception ex)
            {

                sql_trans.RollbackTransaction();
                return Json(SActionResult.Error + ex.Message);
            
            }


        }


        #region UPDATE CHANGES STATUS

        [HttpPost]
        public JsonResult UpdateChanges(SkelClass.page_param.CoverageHdr p_param)
        {
            SQLTransaction sql_trans = new SQLTransaction();
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            
            int DoctypeId;
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

            try{
                var current_user = new _User(Session["username"].ToString());
                
                sql_trans.StartTransaction();
                
                ARMS_W.Objects.Event.CoverageHdr update = new Objects.Event.CoverageHdr();
                DoctypeId = (int)ARMS_W.SkelClass.Globals.InfoType.CalendarEvent;

                update.EventID = p_param.EventID;
                update.EmpIdNo = p_param.EmpIdNo;
                update.Month = p_param.Month;
                update.Year = p_param.Year;

                List<ARMS_W.Objects.Event.changesdtls> qry = new List<ARMS_W.Objects.Event.changesdtls>();

                if (p_param.Acct_dtls != null)
                {
                    foreach (var itm in p_param.Acct_dtls)
                    {
                        //update.Acct_dtls.Add(new ARMS_W.Objects.Event.changesdtls() {

                        //    AccountCode = itm.AccountCode,
                        //    Day = itm.Day
                        
                        
                        //});

                        qry.AddRange((from page in DATABASE.CoverageDtls
                               where page.EventID == p_param.EventID
                               && page.AccountCode == itm.AccountCode
                               && page.Day == itm.Day
                               select new ARMS_W.Objects.Event.changesdtls {
                                  AccountCode =page.AccountCode,
                                  Day = page.Day,
                                  cAcctStatus = (int)page.AcctStatus,
                                  RmrkChanges = itm.RmrkChanges,
                                    linenum = page.LineNum
                               }).ToList());

                    
                    }

                    foreach (var item in qry)
                    {
                        
                        update.Acct_dtls.Add(new ARMS_W.Objects.Event.changesdtls() {
                        
                            AccountCode = item.AccountCode,
                            Day = item.Day,
                            cAcctStatus = item.cAcctStatus,
                            RmrkChanges = item.RmrkChanges,
                             linenum=item.linenum
                        
                        });    
                    
                    }
                
                }

             

                if (act_type == Globals.DocActionType.Approve)
                {
                    update.UpdateDocStatusChanges(ref sql_trans, p_param.EventID, act_type.ToString(),current_user.UserName);
                }
                if (act_type == Globals.DocActionType.ReturnToRequestor)
                {
                    update.UpdateDocStatusChanges(ref sql_trans, p_param.EventID, act_type.ToString(), current_user.UserName);
                }

                if (act_type == Globals.DocActionType.Disapprove)
                {
                    update.UpdateDocStatusChanges(ref sql_trans, p_param.EventID, act_type.ToString(), current_user.UserName);
                }

               sql_trans.Committransaction();
                
               
                
                
                return Json(SActionResult.Success);

            
            }
            catch(Exception ex)
            {

                sql_trans.RollbackTransaction();
                return Json(SActionResult.Error + ex.Message);
            }


        
        }

        #endregion


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public string getLogChangesAccount(string LineNum)
        {
            //LineNum = "SEQ000001";
            List<Globals.db_changes_hdr> log_changes = DataChanges.GetLogChanges(Globals.InfoType.CalendarEvent, LineNum);

             StringBuilder table_builder = new StringBuilder();  
            string a_style = "font-weight:bold; font-size:11px; background:#ededed; padding:2px;";
            string b_style = "font-weight:bold; font-size:11px; background:#f8f8f8; padding:2px;";
            string c_style = "font-size:11px; padding:2px;";

            log_changes = log_changes.OrderByDescending(c => c.log_datetime).ToList();

            table_builder.Append("<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\" >");
            foreach (var itm in log_changes)
            {
                // user name and time stamp
                table_builder.Append("<tr>");
                table_builder.Append("<td style=\"" + a_style + "\" colspan=\"3\">").Append(itm.username).Append(": ").Append(itm.log_datetime.ToString()).Append("</td>");
                table_builder.Append("</tr>");

                // detail header
                table_builder.Append("<tr>");
                table_builder.Append("<td style=\"" + b_style + "\"> &nbsp; </td><td style=\"" + b_style + "\">Prev. Value</td><td style=\"" + b_style + "\">New Value</td>");
                table_builder.Append("</tr>");

                // details
                foreach (var itm_sub in itm.logs)
                {

                    table_builder.Append("<tr>");
                    table_builder.Append("<td style=\"" + c_style + "\">").Append(itm_sub.FieldName).Append("</td>");
                    table_builder.Append("<td style=\"" + c_style + "\">").Append(itm_sub.PrevValue).Append("</td>");
                    table_builder.Append("<td style=\"" + c_style + "\">").Append(itm_sub.NewValue).Append("</td>");
                    table_builder.Append("</tr>");
                }

                table_builder.Append("<tr>");
                table_builder.Append("<td colspan=\"3\">&nbsp;</td>");
                table_builder.Append("</tr>");
            }
            table_builder.Append("</table>");

            return table_builder.ToString();
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public string getRoutechangesbyAccount(string LineNum)
        {
            //string[] month = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            StringBuilder table_builder = new System.Text.StringBuilder();

             var route_changes = DataChanges.GetRouteChangesbyAccount(Globals.InfoType.CalendarEvent, LineNum).OrderByDescending(p => p.DateTimeStamp).ToList();

            string a_style = "font-weight:bold; font-size:11px; background:#ededed; padding:2px;";

            // db = db.OrderByDescending(o => o.DateTimeStamp).ToList();
            table_builder.Append("<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\" >");
            table_builder.Append("<tr>");
            table_builder.Append("<td style=\"" + a_style + "\"> Approver/User </td>");
            table_builder.Append("<td style=\"" + a_style + "\"> Position </td>");
            table_builder.Append("<td style=\"" + a_style + "\"> Date </td>");
            table_builder.Append("<td style=\"" + a_style + "\"> Action </td>");
            table_builder.Append("<td style=\"" + a_style + "\"> Remarks </td>");
            table_builder.Append("</tr>");
            foreach (var itm in route_changes)
            {
                // user name and time stamp
                table_builder.Append("<tr>");
                table_builder.Append("<td>").Append(itm.UserName).Append("</td>");

                table_builder.Append("<td>").Append(itm.PositionName).Append("</td>");

                table_builder.Append("<td>").Append(itm.DateTimeStamp).Append("</td>");

                table_builder.Append("<td>").Append(itm.Action).Append("</td>");

                table_builder.Append("<td>").Append(itm.Remarks).Append("</td>");
                table_builder.Append("</tr>");
            }

            table_builder.Append("</table>");
            return table_builder.ToString();
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public string getVisitLogsByAccount(string LineNum)
        {
            //string[] month = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            //StringBuilder table_builder = new System.Text.StringBuilder();

            //var visit_logs = Objects.MiscFunctions.GetVisitLogs(LineNum).OrderBy(p => p.time).ToList();

            //string a_style = "font-weight:bold; font-size:11px; background:#ededed; padding:2px;";

            //// db = db.OrderByDescending(o => o.DateTimeStamp).ToList();
            //table_builder.Append("<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\" >");
            //table_builder.Append("<tr>");
            //table_builder.Append("<td style=\"" + a_style + "\"> Time </td>");
            //table_builder.Append("<td style=\"" + a_style + "\"> Address </td>");
            //table_builder.Append("<td style=\"" + a_style + "\"> IN/OUT </td>");
            //table_builder.Append("<td style=\"" + a_style + "\"> Longitude </td>");
            //table_builder.Append("<td style=\"" + a_style + "\"> Latitude </td>");
            //table_builder.Append("<td style=\"" + a_style + "\"> Planned/Unplanned </td>");
            //table_builder.Append("</tr>");
            //foreach (var itm in visit_logs)
            //{
            //    // user name and time stamp
            //    table_builder.Append("<tr>");
            //    table_builder.Append("<td>").Append(itm.time).Append("</td>");

            //    table_builder.Append("<td>").Append(itm.address).Append("</td>");

            //    table_builder.Append("<td>").Append(itm.inout).Append("</td>");

            //    table_builder.Append("<td>").Append(itm.longitude).Append("</td>");

            //    table_builder.Append("<td>").Append(itm.latitude).Append("</td>");
            //    table_builder.Append("<td>").Append(itm.isPlanned).Append("</td>");
            //    table_builder.Append("</tr>");
            //}

            //table_builder.Append("</table>");


            StringBuilder table_builder = new StringBuilder(2000);

            // the same as getAnnualTargetLogChanges
            var visit_logs = Objects.MiscFunctions.GetVisitLogs(LineNum).OrderBy(p => p.datetime).OrderBy(o => o.datetime).ToList();

            string a_style = "border:1px solid #ededed; font-size:11px; font-family:arial;";
            string b_style = "font-weight:bold; background:#f4f4f4; font-size:12px; font-family:arial;";
            string c_style = "font-weight:bold; font-size:12px;";
            string ahref = "";
            table_builder.Append("<table cellspacing=\"0\" cellpadding=\"3\" border=\"0\" style=\"" + a_style + "\" >");

            table_builder.Append("<tr style=\"" + c_style + "\" >");
            table_builder.Append("<td align=\"left\" colspan=\"5\" ><b>Visited Location</b></td>");
            table_builder.Append("</tr>");

            table_builder.Append("<tr style=\"" + b_style + "\" >");
            table_builder.Append("<td align=\"center\">Address</td>");
            table_builder.Append("<td align=\"center\">Time</td>");
            table_builder.Append("<td align=\"center\">Action</td>");
            table_builder.Append("<td align=\"center\">Map </td>");
            table_builder.Append("</tr>");

            foreach (var itm in visit_logs)
            {
                ahref = "<a href=\"https://maps.google.com/maps?z=12&t=m&q=loc:" + itm.latitude + "+" + itm.longitude + "\" target=\"new_window\" style=\"text-decoration:none;\">VIEW MAP</a>";

                table_builder.Append("<tr>");

                table_builder.Append("<td>");
                table_builder.Append(itm.address);
                table_builder.Append("</td>");

                table_builder.Append("<td>");
                table_builder.Append(itm.datetime.ToString());
                table_builder.Append("</td>");

                table_builder.Append("<td>");
                table_builder.Append(itm.inout);
                table_builder.Append("</td>");
               
                table_builder.Append("<td>");
                table_builder.Append(ahref);
                table_builder.Append("</td>");

                table_builder.Append("</tr>");
            }
            table_builder.Append("</table>");

            return table_builder.ToString();
        }


        //[HttpPost]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult saveLocation(SkelClass.page_param.CallReportLocation page_param)
        {
            var LineNum = String.Empty;
            var isPlanned = String.Empty;
            var ajx_res = new AjxResult();
            var sql_trans = new SQLTransaction();
            var DATABASE = new Models.ARMSTestEntities();
            ajx_res.iserror = true;

            var coverageDtls = new Dictionary<string, object>();

            if (page_param.act_type == "CHECKIN")
            {
                coverageDtls["CheckInAddress"] = page_param.Address;
                coverageDtls["CheckInTime"] = page_param.Time.ToString();
                coverageDtls["LatitudeInAddress"] = page_param.Latitude;
                coverageDtls["LongitudeInAddress"] = page_param.Longitude;
            }
            else //if action type is checkout
            {
                coverageDtls["CheckOutAddress"] = page_param.Address;
                coverageDtls["CheckOutTime"] = page_param.Time.ToString();
                coverageDtls["LongitudeOutAddress"] = page_param.Longitude;
                coverageDtls["LatitudeOutAddress"] = page_param.Latitude;
            }

            try
            {
                sql_trans.StartTransaction();

                page_param.EventID = DATABASE.CoverageHdrs.SingleOrDefault(o => o.EmpIdNo == page_param.empId && o.Year == page_param.eventYear && o.Month == page_param.eventMonth).EventID;

                var lineNum_qry = from a in DATABASE.CoverageDtls
                                  where a.EventID == page_param.EventID && a.Day == page_param.eventDay && a.AccountCode == page_param.acctCode
                                  select new { a.LineNum,a.isPlanned };
                foreach (var itm in lineNum_qry)
                {
                    LineNum = itm.LineNum;
                    isPlanned = itm.isPlanned;
                    break;
                }

                page_param.LineNum = LineNum == null || LineNum == "" ? page_param.LineNum : LineNum;

                if (page_param.LineNum == null || page_param.LineNum == "")// throw new Exception("Line no. not found!");
                {
                    page_param.LineNum = GenerateNewCode("SEQ", "CoverageDtls", "LineNum");
                   

                    coverageDtls["EventID"] = page_param.EventID;
                    coverageDtls["LineNum"] = page_param.LineNum;
                    coverageDtls["Day"] = page_param.eventDay;
                    coverageDtls["AccountCode"] = page_param.acctCode;
                    coverageDtls["ContactPerson"] = page_param.contactPerson;
                    coverageDtls["ContactPersonNo"] = page_param.contactPersonNo;
                    coverageDtls["IsPlanned"] = "F";
                    coverageDtls["IsDeleted"] = "F";
                    coverageDtls["IsAnEdit"] = "F";
                    coverageDtls["AcctStatus"] = "0";
                    coverageDtls["hasCallreport"] = "F";
                    coverageDtls["DateEncoded"] = DateTime.Now;

                    sql_trans.InsertTo("CoverageDtls", coverageDtls);
                    ajx_res.isplanned = false;
                }
                else
                {
                    sql_trans.UpdateTo("CoverageDtls", coverageDtls, new Dictionary<string, object>() { { "LineNum", page_param.LineNum } });
                    ajx_res.isplanned = true;
                }

                sql_trans.InsertTo("VisitLogs", new Dictionary<string, object>{
                                                        {"EventID", page_param.EventID },
                                                        {"LineNum", page_param.LineNum },
                                                        {"Time", page_param.Time.ToString() },
                                                        {"Address", page_param.Address },
                                                        {"Latitude", page_param.Latitude },
                                                        {"Longitude", page_param.Longitude },
                                                        {"INOUT", page_param.act_type == "CHECKIN"?"IN":"OUT" },
                                                        {"isPlanned", page_param.act_type =="CHECKIN"? (ajx_res.isplanned?"T":"F"):isPlanned }
                                                    });

                sql_trans.Committransaction();                

                ajx_res.iserror = false;
            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
            }
            return Json(ajx_res);
        }
    }
}
