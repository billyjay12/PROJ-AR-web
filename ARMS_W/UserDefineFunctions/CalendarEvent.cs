using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARMS_W.SkelClass;

namespace ARMS_W.UserDefineFunctions
{
    public class CalendarEvent
    {        
        public partial class SoCalendar
        {
            public class EventColors
            {
               public const string Plan = "#92d050";
               public const string Deleted = "#f8f8f8";
               public const string Visited = "#ffc000";
               public const string Edited = "#ccffcc";
               public const string Unplanned = "#00BFFF";
            }

            public static bool IsExist(string EmpIdNo, string Year, string Month )
            {
                bool flag = false;
                ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

                Int32 p_year = Convert.ToInt32(Year);

                flag = DATABASE.EventHdrs.Count(o => o.EmpIdNo == EmpIdNo && o.Year == p_year && o.Month == Month) >= 1 ? true : false;

                return flag;
            }


            public static bool IsExists(string EmpIdNo, string Year, string Month)
            {

                bool flag = false;
                ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

                Int32 p_year = Convert.ToInt32(Year);

                flag = DATABASE.CoverageHdrs.Count(o => o.EmpIdNo == EmpIdNo && o.Year == p_year && o.Month == Month) >= 1 ? true : false;

                return flag;
            }



            public static dynamic GetDocumentStatus(string userId, string month, int year)
            {
                string status = "";

                Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

                var qry = (from evnt_hdr in DATABASE.EventHdrs
                           from aprvl_state in DATABASE.approvalStates
                           where evnt_hdr.DoctypeId == aprvl_state.docType
                           && evnt_hdr.EmpIdNo == userId && evnt_hdr.Month ==month
                           && evnt_hdr.Year == year && evnt_hdr.DocumentStatusId ==aprvl_state.stateID
                           select new {

                               status = aprvl_state.stateDesc
                           
                           
                           });

                foreach (var itm in qry)
                {
                    status = itm.status;
                
                }

                return status;
            
            }

            public static dynamic getEventId(string userId, string month, int year)
            {
                string EventId = "";

                Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

                var qry = (from evnt_hdr in DATABASE.EventHdrs
                           where evnt_hdr.EmpIdNo == userId
                           && evnt_hdr.Month == month
                           && evnt_hdr.Year == year
                           select new
                           {

                               EventId = evnt_hdr.EventID


                           });

                foreach (var itm in qry)
                {
                    EventId = itm.EventId;

                }

                return EventId;

            }


            public static dynamic GetEventInfo(string userId, string month, int year)
            {
                var Eventdate = new SkelClass.page_param.EventHdr();
                ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

                var res = new List<SkelClass.page_param.EventDtl>();

           

                    var qry = (from evntHdr in DATABASE.EventHdrs
                               from evntDtl in DATABASE.EventDtls
                               where evntHdr.EventID == evntDtl.EventID
                               && evntHdr.EmpIdNo == userId
                               && evntHdr.Month == month
                               && evntHdr.Year == year
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

                        res.Add(new SkelClass.page_param.EventDtl()
                        {
                            EventID = itm.EventID,
                            Year = itm.Year,
                            Month = itm.Month,
                            Day = itm.Day,
                            AccountCode = itm.AccountCode,
                            ObjectiveCode = itm.ObjectiveCode
                        });

                    }
                    DATABASE.Dispose();
                


                return res;
            }

            #region TEMP VARIABLES 


            public class EventHdrTmp
            {
               
                public string EventID { get; set; }
                public string EmpIdNo { get; set; }
                public Int32 Year { get; set; }
                public string Month { get; set; }
                public Int32 DocTypeId { get; set; }
                public Int32 DocumentstatusId { get; set; }
                public Int32 day { get; set; }
                public string AccountCode { get; set; }
                public string AccountName { get; set; }
                public string AccountAddress { get; set; }
                public string AccountClass { get; set; }
                public string ContactPerson { get; set; }
                public string ContactPersonNo { get; set; }
                public string ObjectiveCode { get; set; }
                public string StoreCheckingResult { get; set; }
                public string ProductPresentationResult { get; set; }
                public string TotalAmount { get; set; }
                public string CompetitorActivities { get; set; }
                public string WithOrder { get; set; }
                public string NextCallDate { get; set; }
                public string PONum { get; set; }
                public string IssuesAndConcerns { get; set; }
                public string Delivery { get; set; }
                public string orders { get; set; }
                public string SummaryLackingitems { get; set; }
                public string MobileNo { get; set; }
                public string Recommendation { get; set; }
                public string Timetable { get; set; }
                public string remarks { get; set; }
                public string brand { get; set; }
                public string Amount { get; set; }
                public string cFullCollection { get; set;}
                public string cPartialCollection { get; set;}
                public string cNoCollection { get; set; }
                public string CounterClerk { get; set; }
                public string LineNum { get; set; }
                public string OtherInformation { get; set; }
                public List<Subinfo> Sub_info { get; set;}


                public class Subinfo {

                    public string Brand { get; set; }
                    public string Amount { get; set; }
                    public string CounterClerk { get; set; }
                
                }

            }

            
            
            
            #endregion



            //public static dynamic GetEventInfobyDate(string Eventmonth, int Eventday, int Eventyear, string soId)
            //{
            //    var Eventdate = new SkelClass.page_param.EventHdr();
            //    ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            //    var res = new List<SkelClass.page_param.EventHdr>();

            //    var qry = (from view_evnt in DATABASE.armsII_vw_EventdtlsBySo
            //               where view_evnt.EmpIdNo == soId &&
            //               view_evnt.Month == Eventmonth && view_evnt.Year == Eventyear
            //               && view_evnt.day == Eventday
            //               select new { 
                           
            //                  view_evnt.EventID,
            //                  view_evnt.Year,
            //                  view_evnt.Month,
            //                  view_evnt.day,
            //                  view_evnt.EmpIdNo
                              
            //               });

            //    foreach (var itm in qry)
            //    {
            //        res.Add(new SkelClass.page_param.EventHdr() 
            //        {     
            //               EventID = itm.EventID,
            //               EmpIdNo = itm.EmpIdNo,
            //               Year = itm.Year,
            //               Month = itm.Month,
            //               Day = itm.day,

                    
                    
            //        });
                
            //    }


            //    return res;
            //}

            



            public static List<EventHdrTmp> GetEventInfobyDate(string Eventmonth, int Eventday, int Eventyear, string soId)
            {
                ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
                List<EventHdrTmp> Events = new List<EventHdrTmp>();

                var qry = (from evnt_vw in DATABASE.armsII_vw_EventdtlsBySo
                           where evnt_vw.EmpIdNo == soId
                           && evnt_vw.Month == Eventmonth && evnt_vw.Year == Eventyear
                           && evnt_vw.Day == Eventday
                           select new 
                           {

                               evnt_vw.EventId,
                               evnt_vw.EmpIdNo,
                               evnt_vw.Year,
                               evnt_vw.Month,
                               evnt_vw.DoctypeId,
                               evnt_vw.DocumentStatusId,
                               evnt_vw.Day,
                               evnt_vw.AccountCode,
                               evnt_vw.AccountName,
                               evnt_vw.AccountAddress,
                               evnt_vw.AccountClass,
                               evnt_vw.ContactPerson,
                               evnt_vw.ContactPersonNo,
                               evnt_vw.ObjectiveCode,
                               evnt_vw.StoreCheckingResult,
                               evnt_vw.ProductPresentationResult,
                               evnt_vw.TotalAmount,
                               evnt_vw.CompetitorActivities,
                               evnt_vw.WithOrder,
                               evnt_vw.NextCallDate,
                               evnt_vw.PONum,
                               evnt_vw.IssuesAndConcerns,
                               evnt_vw.Delivery,
                               evnt_vw.Orders,
                               evnt_vw.SummaryLackingItems,
                               evnt_vw.MobileNo,
                               evnt_vw.Recommendation,
                               evnt_vw.TimeTable,
                               evnt_vw.Remarks,
                               evnt_vw.Brand,
                               evnt_vw.Amount

                           
                           
                           }).ToList();


                foreach (var itm in qry)
                {
                    Events.Add(new EventHdrTmp() {
                        EventID = itm.EventId,
                        EmpIdNo = itm.EmpIdNo,
                        Year = itm.Year,
                        Month = itm.Month,
                        DocTypeId = (int)itm.DoctypeId,
                        DocumentstatusId = (int) itm.DocumentStatusId,
                        day = itm.Day,
                        AccountCode = itm.AccountCode,
                        AccountName = itm.AccountName,
                        AccountAddress = itm.AccountAddress,
                        AccountClass = itm.AccountClass,
                        ContactPerson = itm.ContactPerson,
                        ContactPersonNo = itm.ContactPersonNo,
                        ObjectiveCode = itm.ObjectiveCode,
                        StoreCheckingResult = itm.StoreCheckingResult,
                        ProductPresentationResult = itm.ProductPresentationResult,
                        TotalAmount = itm.TotalAmount.ToString(),
                        CompetitorActivities = itm.CompetitorActivities,
                        WithOrder = itm.WithOrder,
                        NextCallDate = itm.NextCallDate.ToString(),
                        PONum = itm.PONum,
                        IssuesAndConcerns = itm.IssuesAndConcerns,
                        Delivery = itm.Delivery,
                        orders = itm.Orders,
                        SummaryLackingitems = itm.SummaryLackingItems,
                        MobileNo = itm.MobileNo,
                        Recommendation = itm.Recommendation,
                        Timetable = itm.TimeTable,
                        remarks = itm.Remarks,
                        brand = itm.Brand,
                        Amount = itm.Amount.ToString(),

                    });
                
                }

                return Events;
            
            }



            public static List<EventHdrTmp> GetForCallreport(string Eventmonth, int Eventday, int Eventyear, string soId, string ObjectiveCode)
            {
                ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
                List<EventHdrTmp> Events = new List<EventHdrTmp>();

                var qry = (from evnt_vw in DATABASE.armsII_vw_CallReport
                           where evnt_vw.EmpIdNo == soId
                           && evnt_vw.Month == Eventmonth && evnt_vw.Year == Eventyear
                           && evnt_vw.Day == Eventday
                           && evnt_vw.ObjectiveCode == ObjectiveCode
                           select new {



                               evnt_vw.EventId,
                               evnt_vw.EmpIdNo,
                               evnt_vw.Year,
                               evnt_vw.Month,
                               evnt_vw.DoctypeId,
                               evnt_vw.DocumentStatusId,
                               evnt_vw.Day,
                               evnt_vw.AccountCode,
                               evnt_vw.AccountName,
                               evnt_vw.AccountAddress,
                               evnt_vw.AccountClass,
                               evnt_vw.ContactPerson,
                               evnt_vw.ContactPersonNo,
                               evnt_vw.ObjectiveCode,
                               evnt_vw.StoreCheckingResult,
                               evnt_vw.ProductPresentationResult,
                               evnt_vw.TotalAmount,
                               evnt_vw.CompetitorActivities,
                               evnt_vw.WithOrder,
                               evnt_vw.NextCallDate,
                               evnt_vw.PONum,
                               evnt_vw.IssuesAndConcerns,
                               evnt_vw.Delivery,
                               evnt_vw.Orders,
                               evnt_vw.SummaryLackingItems,
                               evnt_vw.MobileNo,
                               evnt_vw.Recommendation,
                               evnt_vw.TimeTable,
                               evnt_vw.Remarks,
                               evnt_vw.Brand,
                               evnt_vw.cFullCollection,
                               evnt_vw.cNoCollection,
                               evnt_vw.cPartialCollection,
                               evnt_vw.OtherInformation


                           
                           });

                foreach (var itm in qry)
                {
                    Events.Add(new EventHdrTmp()
                    {
                        EventID = itm.EventId,
                        EmpIdNo = itm.EmpIdNo,
                        Year = itm.Year,
                        Month = itm.Month,
                        DocTypeId = (int)itm.DoctypeId,
                        DocumentstatusId = (int)itm.DocumentStatusId,
                        day = itm.Day,
                        AccountCode = itm.AccountCode,
                        AccountName = itm.AccountName,
                        AccountAddress = itm.AccountAddress,
                        AccountClass = itm.AccountClass,
                        ContactPerson = itm.ContactPerson,
                        ContactPersonNo = itm.ContactPersonNo,
                        ObjectiveCode = itm.ObjectiveCode,
                        StoreCheckingResult = itm.StoreCheckingResult,
                        ProductPresentationResult = itm.ProductPresentationResult,
                        TotalAmount = itm.TotalAmount.ToString(),
                        CompetitorActivities = itm.CompetitorActivities,
                        WithOrder = itm.WithOrder,
                        NextCallDate = itm.NextCallDate.ToString(),
                        PONum = itm.PONum,
                        IssuesAndConcerns = itm.IssuesAndConcerns,
                        Delivery = itm.Delivery,
                        orders = itm.Orders,
                        SummaryLackingitems = itm.SummaryLackingItems,
                        MobileNo = itm.MobileNo,
                        Recommendation = itm.Recommendation,
                        Timetable = itm.TimeTable,
                        remarks = itm.Remarks,
                        brand = itm.Brand,
                        cFullCollection = itm.cFullCollection,
                        cNoCollection = itm.cNoCollection,
                        cPartialCollection = itm.cPartialCollection,
                        OtherInformation = itm.OtherInformation

                    });

                }

                return Events;

            
            }



            //NEW CALL REPORT CODE


            public static List<EventHdrTmp> GetListEventCallReport(string Eventmonth, int Eventday, int Eventyear, string EmpId)
            {
                ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
                List<EventHdrTmp> Events = new List<EventHdrTmp>();

                var qry = (from event_hdr in DATABASE.EventHdrs
                           from event_dtl in DATABASE.EventDtls
                           where event_hdr.EventID == event_dtl.EventID
                           && event_hdr.EmpIdNo == EmpId && event_hdr.Month == Eventmonth
                           && event_hdr.Year == Eventyear && event_dtl.Day == Eventday
                           select new {


                               event_hdr.EventID,
                               event_hdr.EmpIdNo,
                               event_hdr.Year,
                               event_hdr.Month,
                               event_dtl.Day,
                               event_dtl.AccountCode,
                               event_dtl.AccountName,
                               event_dtl.AccountAddress,
                               event_dtl.AccountClass,
                               event_dtl.ContactPerson,
                               event_dtl.ContactPersonNo,
                               event_dtl.ObjectiveCode,
                               event_dtl.StoreCheckingResult,
                               event_dtl.ProductPresentationResult,
                               event_dtl.TotalAmount,
                               event_dtl.CompetitorActivities,
                               event_dtl.WithOrder,
                               event_dtl.NextCallDate,
                               event_dtl.PONum,
                               event_dtl.IssuesAndConcerns,
                               event_dtl.Delivery,
                               event_dtl.Orders,
                               event_dtl.SummaryLackingItems,
                               event_dtl.MobileNo,
                               event_dtl.Recommendation,
                               event_dtl.TimeTable,
                               event_dtl.Remarks,
                               event_dtl.cFullCollection,
                               event_dtl.cNoCollection,
                               event_dtl.cPartialCollection,
                               event_dtl.OtherInformation,
                               event_dtl.LineNum
                           
                           });



                foreach (var itm in qry)
                {
                    Events.Add(new EventHdrTmp()
                    {
                        EventID = itm.EventID,
                        EmpIdNo = itm.EmpIdNo,
                        Year = itm.Year,
                        Month = itm.Month,
                        day = itm.Day,
                        AccountCode = itm.AccountCode,
                        AccountName = itm.AccountName,
                        AccountAddress = itm.AccountAddress,
                        AccountClass = itm.AccountClass,
                        ContactPerson = itm.ContactPerson,
                        ContactPersonNo = itm.ContactPersonNo,
                        ObjectiveCode = itm.ObjectiveCode,
                        StoreCheckingResult = itm.StoreCheckingResult,
                        ProductPresentationResult = itm.ProductPresentationResult,
                        TotalAmount = itm.TotalAmount.ToString(),
                        CompetitorActivities = itm.CompetitorActivities,
                        WithOrder = itm.WithOrder,
                        NextCallDate = itm.NextCallDate.ToString(),
                        PONum = itm.PONum,
                        IssuesAndConcerns = itm.IssuesAndConcerns,
                        Delivery = itm.Delivery,
                        orders = itm.Orders,
                        SummaryLackingitems = itm.SummaryLackingItems,
                        MobileNo = itm.MobileNo,
                        Recommendation = itm.Recommendation,
                        Timetable = itm.TimeTable,
                        remarks = itm.Remarks,
                        cFullCollection = itm.cFullCollection,
                        cNoCollection = itm.cNoCollection,
                        cPartialCollection = itm.cPartialCollection,
                        OtherInformation = itm.OtherInformation,
                        LineNum = itm.LineNum,
                        Sub_info = Sub_info(itm.EventID,itm.LineNum) 
                    });

                }

                return Events;

            }


           public static List<EventHdrTmp.Subinfo> Sub_info(string EventID, string LineNum)
            {
                ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
                List<EventHdrTmp.Subinfo> s_info = new List<EventHdrTmp.Subinfo>();

                var qry = (from sub in DATABASE.EventDtl1
                           where sub.EventID == EventID && sub.LineNum == LineNum
                           select new {
                           sub.Brand,
                           sub.Amount,
                           sub.CounterClerk
                           
                           });

                foreach (var itm in qry)
                {
                    s_info.Add(new EventHdrTmp.Subinfo() {
                      Brand = itm.Brand,
                      Amount = itm.Amount.ToString(),
                      CounterClerk = itm.CounterClerk

                    
                    }); 
                }


                return s_info;
           }



           /**
            * Controller and subclass is under the call CoverageHdr of interface and Object
            * Implementation of method under CoverageHdr Objectss and coverage interface
            * 
            * **/


           #region TEMP VARIABLE COVRAGE

           public class CoverageHdrTmp
           {

               public string EventID { get; set; }
               public string EmpIdNo { get; set; }
               public string Year { get; set; }
               public string Month { get; set; }
               public string DoctypeId { get; set; }
               public string LineNum { get; set; }
               public string Day { get; set; }
               public string AccountCode { get; set; }
               public string ContactPerson { get; set; }
               public string ContactPersonNo { get; set; }

               /* added by billy jay delima */

               public string AccountName { get; set; }
               public string AccountAddress { get; set; }
               public string HotelName { get; set; }
               public string HotelNum { get; set; }
               public decimal? _TotalAmount { get; set; }

               /* end*/

               public string StoreChecking { get; set; }
               public string StoreCheckingResult { get; set; }
               public string ProductPresentationResult { get; set; }
               public string TotalAmount { get; set; }
               public string CompetitorActivities { get; set; }
               public string WithOrder { get; set; }
               public string NextCallDate { get; set; }
               public string PONum { get; set; }
               public string IssuesAndConcerns { get; set; }
               public string Delivery { get; set; }
               public string Orders { get; set; }
               public string SummaryLackingItems { get; set; }
               public string MobileNo { get; set; }
               public string Recommendation { get; set; }
               public string TimeTable { get; set; }
               public string Remarks { get; set; }
               public string cFullCollection { get; set; }
               public string cPartialCollection { get; set; }
               public string cNoCollection { get; set; }
               public string OtherInformation { get; set; }
               public string ObjectiveCode { get; set; }
               public string Brand { get; set; }
               public string PlannedAmount { get; set; }
               public string ActualAmount { get; set; }
               public string CounterClerk { get; set; }
               public string ProductPresented { get; set; }
               public List<sub_coverage> Sub_coverage { get; set; }

               public class sub_coverage
               {
                   public string Brand { get; set; }
                   public string PlannedAmount { get; set; }
                   public string ActualAmount { get; set; }
                   public string CounterClerk { get; set; }
                   public string ProductPresented { get; set; }
                   public string Remarks { get; set; }
                   public string CounterClerkNo { get; set; }
                   public string dtlsrmks { get; set; }
                   
                   /*added by billy jay delima*/
                   public string ObjectiveCode { get; set; }
               
               }



           }

           #endregion



           public static List<CoverageHdrTmp> GetCoverageInfobyDate(string Eventmonth, int Eventday, int Eventyear, string soId)
           {
               ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
               List<CoverageHdrTmp> coverage = new List<CoverageHdrTmp>();

               var qry = (from cvrge_vw in DATABASE.arms2_vw_soCoverage
                          where cvrge_vw.EmpIdNo == soId
                            && cvrge_vw.Month == Eventmonth && cvrge_vw.Year == Eventyear
                            && cvrge_vw.Day == Eventday
                          select new
                          {
                              cvrge_vw.EventID,
                              cvrge_vw.EmpIdNo,
                              cvrge_vw.Year,
                              cvrge_vw.Month,
                              cvrge_vw.DoctypeId,
                              cvrge_vw.LineNum,
                              cvrge_vw.Day,
                              cvrge_vw.AccountCode,
                              cvrge_vw.ContactPerson,
                              cvrge_vw.ContactPersonNo,
                              cvrge_vw.StoreChecking,
                              cvrge_vw.StoreCheckingResult,
                              cvrge_vw.ProductPresentationResult,
                              cvrge_vw.TotalAmount,
                              cvrge_vw.CompetitorActivities,
                              cvrge_vw.WithOrder,
                              cvrge_vw.NextCallDate,
                              cvrge_vw.PONum,
                              cvrge_vw.IssuesAndConcerns,
                              cvrge_vw.Delivery,
                              cvrge_vw.Orders,
                              cvrge_vw.SummaryLackingItems,
                              cvrge_vw.MobileNo,
                              cvrge_vw.Recommendation,
                              cvrge_vw.TimeTable,
                              cvrge_vw.Remarks,
                              cvrge_vw.cFullCollection,
                              cvrge_vw.cPartialCollection,
                              cvrge_vw.cNoCollection,
                              cvrge_vw.OtherInformation,
                              cvrge_vw.ObjectiveCode,
                              cvrge_vw.Brand,
                              cvrge_vw.PlannedAmount,
                              cvrge_vw.ActualAmount,
                              cvrge_vw.CounterClerk


                          }).ToList();

               foreach (var itm in qry)
               {
                   coverage.Add(new CoverageHdrTmp()
                   {

                       EventID = itm.EventID,
                       EmpIdNo = itm.EmpIdNo,
                       Year = itm.Year.ToString(),
                       Month = itm.Month,
                       DoctypeId = itm.DoctypeId.ToString(),
                       LineNum = itm.LineNum,
                       Day = itm.Day.ToString(),
                       AccountCode = itm.AccountCode,
                       ContactPerson = itm.ContactPerson,
                       ContactPersonNo = itm.ContactPersonNo,
                       StoreChecking = itm.StoreChecking,
                       StoreCheckingResult = itm.StoreCheckingResult,
                       ProductPresentationResult = itm.ProductPresentationResult,
                       TotalAmount = itm.TotalAmount.ToString(),
                       CompetitorActivities = itm.CompetitorActivities,
                       WithOrder = itm.WithOrder,
                       NextCallDate = itm.NextCallDate.ToString(),
                       PONum = itm.PONum,
                       IssuesAndConcerns = itm.IssuesAndConcerns,
                       Delivery = itm.Delivery,
                       Orders = itm.Orders,
                       SummaryLackingItems = itm.SummaryLackingItems,
                       MobileNo = itm.MobileNo,
                       Recommendation = itm.Recommendation,
                       TimeTable = itm.TimeTable,
                       Remarks = itm.Remarks,
                       cFullCollection = itm.cFullCollection,
                       cPartialCollection = itm.cPartialCollection,
                       cNoCollection = itm.cNoCollection,
                       OtherInformation = itm.OtherInformation,
                       ObjectiveCode = itm.ObjectiveCode,
                       Brand = itm.Brand,
                       PlannedAmount = itm.PlannedAmount.ToString(),
                       ActualAmount = itm.ActualAmount.ToString(),
                       CounterClerk = itm.CounterClerk




                   });

               }


               return coverage;
           }



           public static List<CoverageHdrTmp> GetCoverageInfo(string Eventmonth, int Eventday, int Eventyear, string soId)
           {

               ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
               List<CoverageHdrTmp> coverage = new List<CoverageHdrTmp>();

               var qry = (from cvrgehdr in DATABASE.CoverageHdrs
                          from cvrgedtl in DATABASE.CoverageDtls
                          where cvrgehdr.EventID == cvrgedtl.EventID
                          select new {


                              cvrgehdr.EventID,
                              cvrgehdr.EmpIdNo,
                              cvrgehdr.Year,
                              cvrgehdr.Month,
                              cvrgehdr.DoctypeId,
                              cvrgehdr.DocumentStatusId,
                              cvrgedtl.LineNum,
                              cvrgedtl.Day,
                              cvrgedtl.AccountCode,
                              cvrgedtl.ContactPerson,
                              cvrgedtl.ContactPersonNo,
                              cvrgedtl.StoreChecking,
                              cvrgedtl.StoreCheckingResult,
                              cvrgedtl.ProductPresentationResult,
                              cvrgedtl.TotalAmount,
                              cvrgedtl.CompetitorActivities,
                              cvrgedtl.WithOrder,
                              cvrgedtl.NextCallDate,
                              cvrgedtl.PONum,
                              cvrgedtl.IssuesAndConcerns,
                              cvrgedtl.Delivery,
                              cvrgedtl.Orders,
                              cvrgedtl.SummaryLackingItems,
                              cvrgedtl.MobileNo,
                              cvrgedtl.Recommendation,
                              cvrgedtl.TimeTable,
                              cvrgedtl.Remarks,
                              cvrgedtl.cFullCollection,
                              cvrgedtl.cPartialCollection,
                              cvrgedtl.cNoCollection,
                              cvrgedtl.OtherInformation
                              


                          
                          }).ToList();


               foreach (var itm in qry)
               {
                   coverage.Add(new CoverageHdrTmp() {

                       EventID = itm.EventID,
                       EmpIdNo = itm.EmpIdNo,
                       Year = itm.Year.ToString(),
                       Month = itm.Month,
                       DoctypeId = itm.DoctypeId.ToString(),
                       LineNum = itm.LineNum,
                       Day = itm.Day.ToString(),
                       AccountCode = itm.AccountCode,
                       ContactPerson = itm.ContactPerson,
                       ContactPersonNo = itm.ContactPersonNo,
                       StoreChecking = itm.StoreChecking,
                       StoreCheckingResult = itm.StoreCheckingResult,
                       ProductPresentationResult = itm.ProductPresentationResult,
                       TotalAmount = itm.TotalAmount.ToString(),
                       CompetitorActivities = itm.CompetitorActivities,
                       WithOrder = itm.WithOrder,
                       NextCallDate = itm.NextCallDate.ToString(),
                       PONum = itm.PONum,
                       IssuesAndConcerns = itm.IssuesAndConcerns,
                       Delivery = itm.Delivery,
                       Orders = itm.Orders,
                       SummaryLackingItems = itm.SummaryLackingItems,
                       MobileNo = itm.MobileNo,
                       Recommendation = itm.Recommendation,
                       TimeTable = itm.TimeTable,
                       Remarks = itm.Remarks,
                       cFullCollection = itm.cFullCollection,
                       cPartialCollection = itm.cPartialCollection,
                       cNoCollection = itm.cNoCollection,
                       OtherInformation = itm.OtherInformation,
                       Sub_coverage = sub_coverages(itm.EventID, itm.LineNum)
                   
                   
                   });
               
               }

               return coverage;
           
           }


           public static List<CoverageHdrTmp.sub_coverage> sub_coverages(string EventID, string LineNum)
           {
               ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
               List<CoverageHdrTmp.sub_coverage> s_info = new List<CoverageHdrTmp.sub_coverage>();

               var qry = (from sub in DATABASE.CoverageDtl1
                          where sub.EventID == EventID && sub.LineNum == LineNum
                          select new
                          {
                              sub.Brand,
                              sub.PlannedAmount,
                              sub.ActualAmount,
                              sub.CounterClerk,
                              sub.ProductPresented,
                              sub.Remarks,
                              sub.CounterClerkNo,
                              sub.detailRemarks

                          });

               foreach (var itm in qry)
               {
                   s_info.Add(new CoverageHdrTmp.sub_coverage()
                   {
                       Brand = itm.Brand,
                       PlannedAmount = itm.PlannedAmount.ToString(),
                       CounterClerk = itm.CounterClerk,
                       ProductPresented = itm.ProductPresented,
                       Remarks = itm.Remarks,
                       CounterClerkNo = itm.CounterClerkNo,
                       dtlsrmks = itm.detailRemarks


                   });
               }


               return s_info;
           }



            //New code implemenation of coverage header

           public static dynamic GetCoverageInfo(string userId, string month, int year,string viewtype)
           {
               var Eventdate = new SkelClass.page_param.CoverageHdr();
               ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

               var res = new List<SkelClass.page_param.EventDtl>();



               if (viewtype.ToUpper() == "MONTHLY")
               {

                   var qry = (from evntHdr in DATABASE.CoverageHdrs
                              from evntDtl in DATABASE.CoverageDtls
                              where evntHdr.EventID == evntDtl.EventID
                              && evntHdr.EmpIdNo == userId
                              && evntHdr.Month == month
                              && evntHdr.Year == year
                              && evntDtl.AcctStatus != 7
                              select new
                              {
                                  evntHdr.EventID,
                                  evntHdr.Year,
                                  evntHdr.Month,
                                  evntDtl.Day,
                                  evntDtl.AccountCode,
                                  evntDtl.isPlanned,
                                  evntDtl.IsDeleted,
                                  evntDtl.IsAnEdit,
                                  evntDtl.AcctStatus,
                                  evntDtl.hasCallreport
                              }).Distinct().ToList();

                   foreach (var itm in qry)
                   {

                       res.Add(new SkelClass.page_param.EventDtl()
                       {
                           EventID = itm.EventID,
                           Year = itm.Year,
                           Month = itm.Month,
                           Day = itm.Day,
                           AccountCode = itm.AccountCode,
                           isPlanned = itm.isPlanned,
                           IsDeleted = itm.IsDeleted,
                           IsAnEdit = itm.IsAnEdit,
                           AcctStatus = (int)itm.AcctStatus,
                           hasCallreport = itm.hasCallreport

                       });

                   }
               }


               else {

                   var qry = (from evntHdr in DATABASE.CoverageHdrs
                              from evntDtl in DATABASE.CoverageDtls
                              where evntHdr.EventID == evntDtl.EventID
                              && evntHdr.EmpIdNo == userId
                              && evntHdr.Month == month
                              && evntHdr.Year == year
                              
                              select new
                              {
                                  evntHdr.EventID,
                                  evntHdr.Year,
                                  evntHdr.Month,
                                  evntDtl.Day,
                                  evntDtl.AccountCode,
                                  evntDtl.isPlanned,
                                  evntDtl.IsDeleted,
                                  evntDtl.IsAnEdit,
                                  evntDtl.AcctStatus,
                                  evntDtl.hasCallreport
                              }).Distinct().ToList();

                   foreach (var itm in qry)
                   {

                       res.Add(new SkelClass.page_param.EventDtl()
                       {
                           EventID = itm.EventID,
                           Year = itm.Year,
                           Month = itm.Month,
                           Day = itm.Day,
                           AccountCode = itm.AccountCode,
                           isPlanned = itm.isPlanned,
                           IsDeleted = itm.IsDeleted,
                           IsAnEdit = itm.IsAnEdit,
                           AcctStatus = (int)itm.AcctStatus,
                           hasCallreport = itm.hasCallreport

                       });

                   }
               
               
               
               }

               

               DATABASE.Dispose();



               return res;
           }

           public static dynamic GetCoverageDocumentStatus(string userId, string month, int year)
           {
               string status = "";
               int statusId = 0;

               Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

               var qry = (from cvrge_hdr in DATABASE.CoverageHdrs
                          from aprvl_state in DATABASE.approvalStates
                          where cvrge_hdr.DoctypeId == aprvl_state.docType
                          && cvrge_hdr.EmpIdNo == userId && cvrge_hdr.Month == month
                          && cvrge_hdr.Year == year && cvrge_hdr.DocumentStatusId == aprvl_state.stateID
                          select new
                          {

                              status = aprvl_state.stateDesc,
                              statusId = cvrge_hdr.DocumentStatusId
                              


                          });

               foreach (var itm in qry)
               {
                   status = itm.status;
                   statusId = (int)itm.statusId;

               }

               DATABASE.Dispose();

               return status;

           }

           public static dynamic getCoverageId(string userId, string month, int year)
           {
               string EventId = "";

               Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

               var qry = (from crvge_hdr in DATABASE.CoverageHdrs
                          where crvge_hdr.EmpIdNo == userId
                          && crvge_hdr.Month == month
                          && crvge_hdr.Year == year
                          select new
                          {

                              EventId = crvge_hdr.EventID


                          });

               foreach (var itm in qry)
               {
                   EventId = itm.EventId;

               }

               DATABASE.Dispose();

               return EventId;

           }



           public static dynamic GetforComplainceFalse(string Eventid, string userId, string month, int year)
           {

               Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

               int falsecount = 0;

               var qry = (from crvge_hdr in DATABASE.CoverageHdrs
                          from cvrge_dtl in DATABASE.CoverageDtls

                          where crvge_hdr.EventID == cvrge_dtl.EventID &&
                          crvge_hdr.EventID == Eventid && cvrge_dtl.EventID==Eventid &&
                          crvge_hdr.EmpIdNo == userId
                          //&& crvge_hdr.Month == month
                          //&& crvge_hdr.Year == year
                          && cvrge_dtl.hasCallreport !=null
                          && cvrge_dtl.isPlanned=="T"
                          && cvrge_dtl.IsDeleted =="F"
                          && cvrge_dtl.AcctStatus != 7
                          select new
                          {

                              hasCallreport = cvrge_dtl.hasCallreport


                          }).ToList();

               falsecount = qry.Count();

             
               DATABASE.Dispose();

               return falsecount;
           
           }


           public static dynamic GetforComplainceTrue(string Eventid,string userId, string month, int year)
           {

               Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

               int falsecount = 0;

               var qry = (from crvge_hdr in DATABASE.CoverageHdrs
                          from cvrge_dtl in DATABASE.CoverageDtls

                          where crvge_hdr.EventID == Eventid && cvrge_dtl.EventID == Eventid&& 
                          crvge_hdr.EmpIdNo == userId
                          && crvge_hdr.Month == month
                          && crvge_hdr.Year == year
                          && cvrge_dtl.hasCallreport == "T"
                          && cvrge_dtl.isPlanned == "T"
                          && cvrge_dtl.IsDeleted == "F"
                          
                          select new
                          {

                              hasCallreport = cvrge_dtl.hasCallreport


                          }).ToList();

               falsecount = qry.Count();


               DATABASE.Dispose();

               return falsecount;

           }

           public static dynamic Getallforcallreport(string Eventid, string userId, string month, int year)
           {
               Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

               int allcount = 0;

              /** var qry = (from crvge_hdr in DATABASE.CoverageHdrs
                          from cvrge_dtl in DATABASE.CoverageDtls
                          where crvge_hdr.EventID == Eventid &&
                          cvrge_dtl.EventID == Eventid && 
                          crvge_hdr.EmpIdNo == userId
                          && crvge_hdr.Month == month
                          && crvge_hdr.Year == year
                          && cvrge_dtl.isPlanned == "T"
                          && cvrge_dtl.IsDeleted == "F"
                          select new
                          {

                              isplanned = cvrge_dtl.isPlanned,
                              accoutcode = cvrge_dtl.AccountCode


                          }).ToList();**/

               var qry = (from dtls in DATABASE.CoverageDtls
                          where dtls.EventID == Eventid && dtls.isPlanned == "T"
                          && dtls.IsDeleted == "F"  && dtls.AcctStatus!=7
                          select new { accountcode = dtls.AccountCode }).ToList();

               allcount = qry.Count();


               DATABASE.Dispose();


               return allcount;
           
           }



            /* added by billy jay delima */

            public class customerheader
            {
                public string sapacctcode { get; set; }
                public string bussAdd { get; set; }
                public string acctName { get; set; }
            }

           public static List<CoverageHdrTmp> AssignDataToVariables(List<UserDefineFunctions.Application.coverageplans> upload_param)
           {
               var DATABASE = new Models.ARMSTestEntities();
               List<CoverageHdrTmp> res = new List<CoverageHdrTmp>();

               var accountcodes = upload_param.GroupBy(o => o.AccountCode).Select(grp => grp.First());

               string AccountAddress = "";
               string AccountName = "";
               foreach (var itm in accountcodes)
               {
                   try
                   {
                       AccountAddress = DATABASE.arms2_vw_customerheader_lookup.Where(o => o.SapAcctCode == itm.AccountCode).Single().bussAdd;//DATABASE.customerHeaders.Where(o => o.SapAcctCode == itm.AccountCode).Single().bussAdd;
                       AccountName = DATABASE.arms2_vw_customerheader_lookup.Where(o => o.SapAcctCode == itm.AccountCode).Single().acctName;
                   }
                   catch
                   {
                       AccountAddress = "";
                       AccountName = "";
                   }
                   res.Add(new CoverageHdrTmp()
                   {
                       AccountAddress = AccountAddress,
                       AccountName = AccountName,
                       AccountCode = itm.AccountCode,
                       ContactPerson = itm.ContactPerson,
                       ContactPersonNo = itm.ContactPersonNo,
                       //StoreChecking = itm.StoreChecking,
                       IssuesAndConcerns = itm.IssuesAndConcerns,
                       //HotelName = itm.HotelName,
                       //HotelNum = itm.HotelNum,
                       Sub_coverage = get_sub_coverages(upload_param, itm.AccountCode)
                   });

               }
               DATABASE.Dispose();
               return res;
           }

           public static List<page_param.fullcalendarEvents> reFormatData(List<UserDefineFunctions.Application.coverageplans> upload_param,int eventMonth,int eventYear)
           {
               var DATABASE = new Models.ARMSTestEntities();
               List<page_param.fullcalendarEvents> res = new List<page_param.fullcalendarEvents>();
               int id_counter = 1;
               
               var distinct_day = upload_param.Select(o => o.Day).Distinct();
               foreach (var day in distinct_day)
               {
                   var distinct_accounts = upload_param.Where(o => o.Day == day).GroupBy(o => o.AccountCode).Select(grp => grp.First());
                   foreach (var itm_account in distinct_accounts)
                   {
                       

                       res.Add(new page_param.fullcalendarEvents()
                       {
                           id = id_counter++,
                           account_code = itm_account.AccountCode,
                           contact_person = itm_account.ContactPerson,
                           contact_person_no = itm_account.ContactPersonNo,
                           //hotel_name = itm_account.HotelName,
                           //hotel_num = itm_account.HotelNum,
                           issues_and_concerns = itm_account.IssuesAndConcerns,
                           //store_checking = itm_account.StoreChecking,
                           allday = true,   
                           editable = true,
                           //backgroundColor = upload_param.Where(a => a.AccountCode == itm_account.AccountCode && a.Day == day).Any(o => o.Brand.Contains("invalid brand(") || o.ProductPresented.Contains("invalid data(") || o.AccountCode.Contains("invalid Code(") ||
                           //                                   o.CounterClerk.Contains("invalid data(") || o.CounterClerkNo.Contains("invalid data(") || o.PlannedAmount.Contains("invalid data(") || o.ContactPerson.Contains("(REQUIRED FIELD)") || o.ContactPersonNo.Contains("(REQUIRED FIELD)")) ? "#FF4F4F" : "#92d050",

                           backgroundColor = upload_param.Where(a => a.AccountCode == itm_account.AccountCode && a.Day == day).Any(o => o.Brand.Contains("invalid") || o.AccountCode.Contains("invalid Code(") || o.ContactPersonNo.Contains("invalid") ||
                           o.PlannedAmount.Contains("invalid data(") || o.ContactPerson.Contains("(REQUIRED FIELD)") || o.ContactPersonNo.Contains("(REQUIRED FIELD)") || o.PlannedAmount.Contains("invalid") || o.DetailRemarks.Contains("invalid")) ? "#FF4F4F" : "#92d050",

                           title = itm_account.AccountCode,//!DATABASE.arms2_vw_customerheader_lookup.Any(o => o.SapAcctCode == itm_account.AccountCode) ? itm_account.AccountCode : DATABASE.arms2_vw_customerheader_lookup.Single(o => o.SapAcctCode == itm_account.AccountCode).acctName,
                           start = new DateTime(eventYear, (eventMonth + 1), int.Parse(day)).ToShortDateString(),
                           end = new DateTime(eventYear, (eventMonth + 1), int.Parse(day)).ToShortDateString(),
                           list_objectives = (from a in upload_param
                                              where a.AccountCode == itm_account.AccountCode && a.Day == day
                                              select new page_param.fullcalendarEvents.account_objectives()
                                              {

                                                  brand = a.Brand,
                                                  //product_presented = a.ProductPresented,
                                                  //counter_clerk_no = a.CounterClerkNo,
                                                  //counter_clerk = a.CounterClerk,
                                                  planned_amount = a.PlannedAmount,
                                                  objective_code = a.ObjectiveCode==null?"":a.ObjectiveCode,//a.ObjectiveCode,
                                                  details_remark = a.DetailRemarks
                                              }).ToList()
                       });
                   }
               }


               return res;
           }

           public static List<CoverageHdrTmp.sub_coverage> get_sub_coverages(List<UserDefineFunctions.Application.coverageplans> upload_param, string acctCode="")
           {
               var res = new List<CoverageHdrTmp.sub_coverage>();

               foreach (var itm in upload_param.Where(o => o.AccountCode == acctCode))
               {
                   res.Add(new CoverageHdrTmp.sub_coverage()
                   {
                       ObjectiveCode = itm.ObjectiveCode,
                       Brand = itm.Brand,
                       PlannedAmount = itm.PlannedAmount,//.GetValueOrDefault().ToString(),
                       //CounterClerk = itm.CounterClerk,
                       //CounterClerkNo = itm.CounterClerkNo,
                       //ProductPresented = itm.ProductPresented
                   });
               }

               return res;
           }

           public static List<page_param.coverageplan_save_upload.account_codes> arrange_data_page_param(page_param.coverageplan_save_upload coverage_plan)
           {
               List<page_param.coverageplan_save_upload.account_codes> result = new List<page_param.coverageplan_save_upload.account_codes>();
               string issues_concerns = "";
               string store_chk = "";

               var distinct_accounts = coverage_plan.list_accounts.GroupBy(o => o.account_code).Select(grp => grp.First()).ToList(); //distinct all accounts
               foreach (var account in distinct_accounts)
               {
                   List<page_param.coverageplan_save_upload.account_codes.account_objectives> objectives = new List<page_param.coverageplan_save_upload.account_codes.account_objectives>();

                   foreach (var itm in coverage_plan.list_accounts.Where(o => o.account_code == account.account_code))
                   {
                       foreach (var objective in itm.list_objectives)
                       {
                           objectives.Add(new page_param.coverageplan_save_upload.account_codes.account_objectives()
                           {
                               objective_code = objective.objective_code,
                               brand = objective.brand,
                               counter_clerk = objective.counter_clerk,
                               counter_clerk_no = objective.counter_clerk_no,
                               planned_amount = objective.planned_amount,
                               product_presented = objective.product_presented
                           });
                       }
                   }
                   try
                   {
                       issues_concerns = "";
                       store_chk = "";
                       var str_issues = coverage_plan.list_accounts.Where(o => o.account_code == account.account_code).Select(o => new { o.issues_and_concerns, o.store_checking }).Distinct();
                       foreach (var itm in str_issues)
                       {
                           if (itm.issues_and_concerns != null && itm.issues_and_concerns != "" && itm.issues_and_concerns != "NULL") { issues_concerns = itm.issues_and_concerns; }
                           if (itm.store_checking != null && itm.store_checking != "" && itm.store_checking != "NULL")
                           {
                               store_chk = itm.store_checking;
                               break;
                           }
                       }
                   }
                   catch { }
                   result.Add(new page_param.coverageplan_save_upload.account_codes()
                   {
                       account_code = account.account_code,
                       contact_person = account.contact_person,
                       contact_person_no = account.contact_person_no,
                       hotel_name = account.hotel_name,
                       hotel_num = account.hotel_num,
                       issues_and_concerns = issues_concerns,
                       store_checking = store_chk,
                       list_objectives = objectives
                   });
               }
               return result;
           }

           public static string verify_inputs(List<UserDefineFunctions.Application.coverageplans> coverageplans, string empId, string EventMonth, int EventDay, int EventYear)
           {
               Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
               
               var active_bus_partner = DATABASE.arms2_vw_actveBusPrtnr.Where(p => p.empIdNo == empId).ToList();

               var EventID = DATABASE.CoverageHdrs.Where(o => o.EmpIdNo == empId && o.Month == EventMonth && o.Year == EventYear).Single().EventID;

               var brands = DATABASE.armsII_vw_itemMasterFile.Where(p => p.Brand != null || p.Brand != "").Select(o => new { o.Brand }).ToList().Distinct();
               var objective_codes = DATABASE.ObjectiveHdrs.Where(p => p.objectiveCode != null || p.objectiveCode != "").Select(o => new { o.objectiveCode }).ToList().Distinct();
               var existing_account_codes = DATABASE.CoverageDtls.Where(o => o.EventID == EventID && o.Day == EventDay && o.IsDeleted == "F").Select(o => new { o.AccountCode }).ToList();

               var accountcode_encoded = coverageplans.Where(p => p.AccountCode != null || p.AccountCode != "" || p.AccountCode != "NULL" || p.AccountCode != "null").Select(o => new { o.AccountCode }).ToList().Distinct();
               var brand_encoded = coverageplans.Where(p => p.Brand != null && p.Brand != "" && p.Brand.ToUpper()!="NULL" ).Select(o => new { o.Brand }).ToList().Distinct();
               var obj_code_encodeded = coverageplans.Where(p => p.ObjectiveCode != null && p.ObjectiveCode != "" && p.ObjectiveCode.ToUpper() != "NULL").Select(o => new { o.ObjectiveCode }).ToList().Distinct();

               DATABASE.Dispose();

               foreach (var itm in accountcode_encoded)
               {
                   if (active_bus_partner.Any(o => o.CardCode == itm.AccountCode))
                       continue;
                   else
                       return "Not all customer accounts are assign to this account."; //note: changing the return value. kindly change also the value in jLib.js [$.fn.uploadlink2]
               }

               foreach (var itm in brand_encoded)
               {
                   if(brands.Any(o=>o.Brand==itm.Brand))
                       continue;
                   else
                       return "One of the brand is invalid.";  //note: changing the return value. kindly change also the value in jLib.js [$.fn.uploadlink2]
               }

               foreach (var itm in obj_code_encodeded)
               {
                   if (objective_codes.Any(p => p.objectiveCode == itm.ObjectiveCode))
                       continue;
                   else
                       return "One of the objective code is invalid"; //note: changing the return value. kindly change also the value in jLib.js [$.fn.uploadlink2]
               }


               foreach (var itm in accountcode_encoded)
               {
                   if (existing_account_codes.Any(p => p.AccountCode == itm.AccountCode))
                        return "Customer account already exist."; //note: changing the return value. kindly change also the value in jLib.js [$.fn.uploadlink2]
                       //continue;
                  // else
                      
               }

               return  "VALID";

               //var da = (from a in DATABASE.arms2_vw_actveBusPrtnr
               //          where a.empIdNo == empId
               //          select new 
               //          {
               //              AccountCode = a.CardCode
               //          }).ToList();

               //var de = (from a in coverageplans
               //          select new 
               //          {
               //              AccountCode = a.AccountCode
               //          }).ToList();

               //var aaa = da.Except(de).ToList();

               //return aaa.Count() >= 1 ? false : true;//da.Except(coverageplans).Count() >= 1 ? false : true;


           }

           public static string verify_inputs(List<UserDefineFunctions.Application.coverageplans> coverageplans, string empId, string EventMonth, int EventYear)
           {
               Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

               var active_bus_partner = DATABASE.arms2_vw_actveBusPrtnr.Where(p => p.empIdNo == empId).ToList();
               
               var EventID = DATABASE.CoverageHdrs.Where(o => o.EmpIdNo == empId && o.Month == EventMonth && o.Year == EventYear).Single().EventID;

               var brands = DATABASE.armsII_vw_itemMasterFile.Where(p => p.Brand != null || p.Brand != "").Select(o => new { o.Brand }).ToList().Distinct();
               var objective_codes = DATABASE.ObjectiveHdrs.Where(p => p.objectiveCode != null || p.objectiveCode != "").Select(o => new { o.objectiveCode }).ToList().Distinct();
               var existing_account_codes = DATABASE.CoverageDtls.Where(o => o.EventID == EventID && o.IsDeleted == "F").Select(o => new { o.AccountCode }).ToList().Distinct();

               var accountcode_encoded = coverageplans.Where(p => p.AccountCode != null || p.AccountCode != "" || p.AccountCode != "NULL" || p.AccountCode != "null").Select(o => new { o.AccountCode }).ToList().Distinct();
               var brand_encoded = coverageplans.Where(p => p.Brand != null && p.Brand != "" && p.Brand.ToUpper() != "NULL").Select(o => new { o.Brand }).ToList().Distinct();
               var obj_code_encodeded = coverageplans.Where(p => p.ObjectiveCode != null && p.ObjectiveCode != "" && p.ObjectiveCode.ToUpper() != "NULL").Select(o => new { o.ObjectiveCode }).ToList().Distinct();

               //var invalid_accountcode=DATABASE.arms2_vw_actveBusPrtnr.Where(p=>p.CardCode




               DATABASE.Dispose();


               foreach (var itm in accountcode_encoded) 
               {
                   if (active_bus_partner.Any(o => o.CardCode == itm.AccountCode))
                       continue;
                   else
                       return "Not all customer accounts are assign to this account."; //note: changing the return value. kindly change also the value in jLib.js [$.fn.uploadlink2]
               }

               foreach (var itm in brand_encoded)
               {
                   if (brands.Any(o => o.Brand == itm.Brand))
                       continue;
                   else
                       return "One of the brand is invalid.";  //note: changing the return value. kindly change also the value in jLib.js [$.fn.uploadlink2]
               }

               foreach (var itm in obj_code_encodeded)
               {
                   if (objective_codes.Any(p => p.objectiveCode == itm.ObjectiveCode))
                       continue;
                   else
                       return "One of the objective code is invalid"; //note: changing the return value. kindly change also the value in jLib.js [$.fn.uploadlink2]
               }


               foreach (var itm in accountcode_encoded)
               {
                   if (existing_account_codes.Any(p => p.AccountCode == itm.AccountCode))
                       continue;
                   else
                       return "Customer account already exist."; //note: changing the return value. kindly change also the value in jLib.js [$.fn.uploadlink2]
               }

               return "VALID";
           }
           public static string verify_inputs_event_not_exist(List<UserDefineFunctions.Application.coverageplans> coverageplans, string empId, string EventMonth, int EventYear)
           {
               Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

               var active_bus_partner = DATABASE.arms2_vw_actveBusPrtnr.Where(p => p.empIdNo == empId).ToList();


               var brands = DATABASE.armsII_vw_itemMasterFile.Where(p => p.Brand != null || p.Brand != "").Select(o => new { o.Brand }).ToList().Distinct();
               var objective_codes = DATABASE.ObjectiveHdrs.Where(p => p.objectiveCode != null || p.objectiveCode != "").Select(o => new { o.objectiveCode }).ToList().Distinct();

               var accountcode_encoded = coverageplans.Where(p => p.AccountCode != null || p.AccountCode != "" || p.AccountCode != "NULL" || p.AccountCode != "null").Select(o => new { o.AccountCode }).ToList().Distinct();
               var brand_encoded = coverageplans.Where(p => p.Brand != null && p.Brand != "" && p.Brand.ToUpper() != "NULL").Select(o => new { o.Brand }).ToList().Distinct();
               var obj_code_encodeded = coverageplans.Where(p => p.ObjectiveCode != null && p.ObjectiveCode != "" && p.ObjectiveCode.ToUpper() != "NULL").Select(o => new { o.ObjectiveCode }).ToList().Distinct();

               DATABASE.Dispose();

               var isError = from a in accountcode_encoded
                             from b in active_bus_partner
                             where a.AccountCode == b.CardCode
                             select a;

               foreach (var itm in accountcode_encoded)
               {
                   if (active_bus_partner.Any(o => o.CardCode == itm.AccountCode))
                       continue;
                   else
                       return "Not all customer accounts are assign to this account."; //note: changing the return value. kindly change also the value in jLib.js [$.fn.uploadlink2]
               }

               foreach (var itm in brand_encoded)
               {
                   if (brands.Any(o => o.Brand == itm.Brand))
                       continue;
                   else
                       return "One of the brand is invalid.";  //note: changing the return value. kindly change also the value in jLib.js [$.fn.uploadlink2]
               }

               foreach (var itm in obj_code_encodeded)
               {
                   if (objective_codes.Any(p => p.objectiveCode == itm.ObjectiveCode))
                       continue;
                   else
                       return "One of the objective code is invalid"; //note: changing the return value. kindly change also the value in jLib.js [$.fn.uploadlink2]
               }


               return "VALID";
           }
            /* end */


           public string GetApprvrRemarks(string EventId)
           {
               string remarks = "";
               ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

               /**var qry = (from a in DATABASE.RouteRmrks
                          where a.EventID == EventId
                          select new
                          {

                          }).ToList().OrderByDescending()   **/

               //remarks = DATABASE.RouteRmrks.Single(p => p.EventID == EventId).EventID.OrderByDescendin

               //remarks = DATABASE.RouteRmrks


               return remarks;

           }

           public static string getEventID_(string soID, string month, int year)
           {
               var DATABASE = new Models.ARMSTestEntities();
               ARMS_W.Models.userHeader username = DATABASE.userHeaders.Single(p => p.empIdNo == soID);
               var qry = DATABASE.RouteChanges.Where(o => o.UserName == username.userName && o.Remarks.Contains("Batch")).Select(o => o.DocId).ToList();
               DATABASE.Dispose();

               return qry.LastOrDefault();
           }

           public class FullCalendarEvents
           {
               public string title { get; set; }
               public string id { get; set; }
               public string start { get; set; }
               public string end { get; set; }
               public bool allDay { get; set; }
               public string backgroundColor { get; set; }
               public string eventBorderColor { get; set; }
               public string textColor { get; set; }
               //public bool editable { get; set; }
               public List<Sales> salesdetails { get; set; }
               public decimal? totalSales { get; set; }
           }

           public class Sales
           {
               public string brand { get; set; }
               public decimal? amount { get; set; }
           }
           public static List<FullCalendarEvents> GetAllDayEvents(string userId,string month,int year,int day)
           {
               Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
               List<FullCalendarEvents> result = new List<FullCalendarEvents>();
               List<Sales> qry_2 = new List<Sales>();
               string bgColor="";
               decimal totalSales = 0;
               
               var qry = (from evntHdr in DATABASE.CoverageHdrs
                          from evntDtl in DATABASE.CoverageDtls
                          from cust in DATABASE.arms2_vw_customerheader_lookup//DATABASE.customerHeaders
                          where evntHdr.EventID == evntDtl.EventID
                          && evntDtl.AccountCode == cust.SapAcctCode
                          && evntHdr.EmpIdNo == userId
                          && evntHdr.Month == month
                          && evntHdr.Year == year
                          && evntDtl.Day == day

                          select new
                          {
                              evntHdr.EventID,
                              evntHdr.Year,
                              evntHdr.Month,
                              evntDtl.Day,
                              evntDtl.AccountCode,
                              evntDtl.isPlanned,
                              evntDtl.IsDeleted,
                              evntDtl.IsAnEdit,
                              evntDtl.AcctStatus,
                              evntDtl.hasCallreport,
                              evntDtl.LineNum,
                              cust.acctName
                          }).Distinct().ToList();

               foreach (var itm in qry)
               {
                   bgColor = GetColor(itm.isPlanned, itm.IsDeleted, itm.IsAnEdit, itm.hasCallreport, itm.AcctStatus);
                   var sales = (from eventDtl1 in DATABASE.CoverageDtl1
                                where eventDtl1.Day == itm.Day
                                && eventDtl1.EventID == itm.EventID
                               && eventDtl1.LineNum == itm.LineNum && eventDtl1.ObjectiveCode == "S"
                                select eventDtl1.PlannedAmount).Sum();

                   totalSales += (sales.HasValue ? sales.Value : 0);


                   qry_2 = (List<Sales>)(from eventDtl1 in DATABASE.CoverageDtl1
                                         where eventDtl1.Day == itm.Day
                                         && eventDtl1.EventID == itm.EventID
                                        && eventDtl1.LineNum == itm.LineNum && eventDtl1.ObjectiveCode == "S"
                                         select new Sales
                                         {
                                             brand = eventDtl1.Brand,
                                             amount = eventDtl1.PlannedAmount
                                         }).ToList();

                   if (bgColor != "")
                       result.Add(new FullCalendarEvents()
                       {
                          // title = "[" + itm.AccountCode + "] " + itm.acctName + " - Target Sales(" + (sales.HasValue ? sales.Value.ToString("#,0.00") : "0.00") + ")",
                           title = "[" + itm.AccountCode + "] " + itm.acctName,
                           start = new DateTime(itm.Year, int.Parse(itm.Month), itm.Day).ToShortDateString(),
                           backgroundColor = bgColor,
                           allDay = true,
                           // textColor = bgColor == EventColors.Unplanned || bgColor == EventColors.Plan || bgColor == EventColors.Visited ? "white" : "black",
                           // textColor = bgColor == EventColors.Deleted ? "black" : "white",
                           textColor = "black",
                           id = itm.AccountCode,
                           salesdetails = qry_2,
                           totalSales = sales 
                       });
               }

               if (qry.Count > 0)
               {
                   result.Add(new FullCalendarEvents()
                   {
                       title = "TOTAL TARGET SALES FOR THE DAY - (" + totalSales.ToString("#,0.00") + ")",
                       start = new DateTime(qry.First().Year, Convert.ToInt32(qry.First().Month), qry.First().Day).ToShortDateString(),
                       //backgroundColor = "white",
                       allDay = true
                   });
               }

               DATABASE.Dispose();
               return result;
           }

           public static List<FullCalendarEvents> GetAllDayEvents(string userId, string month, int year, string viewtype,int day = 1)
           {
               Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
               List<FullCalendarEvents> result = new List<FullCalendarEvents>();
               List<Sales> qry_2 = new List<Sales>();
               string bgColor = "";
               decimal totalSales = 0;
               if (viewtype.ToUpper() == "MONTHLY")
               {

                   var qry = (from evntHdr in DATABASE.CoverageHdrs
                              from evntDtl in DATABASE.CoverageDtls
                              where evntHdr.EventID == evntDtl.EventID
                              && evntHdr.EmpIdNo == userId
                              && evntHdr.Month == month
                              && evntHdr.Year == year
                              && evntDtl.AcctStatus != 7
                              select new
                              {
                                  evntHdr.EventID,
                                  evntHdr.Year,
                                  evntHdr.Month,
                                  evntDtl.Day,
                                  evntDtl.AccountCode,
                                  evntDtl.isPlanned,
                                  evntDtl.IsDeleted,
                                  evntDtl.IsAnEdit,
                                  evntDtl.AcctStatus,
                                  evntDtl.hasCallreport//,
                                 // evntDtl.CheckInTime
                              }).Distinct().ToList();


                   foreach (var itm in qry)
                   {
                       bgColor = GetColor(itm.isPlanned, itm.IsDeleted, itm.IsAnEdit, itm.hasCallreport, itm.AcctStatus);

                       if (bgColor != "")
                           result.Add(new FullCalendarEvents()
                           {
                               title = itm.AccountCode,
                              // start = itm.CheckInTime.HasValue ? itm.CheckInTime.Value.ToString() : new DateTime(itm.Year, int.Parse(itm.Month), itm.Day).ToShortDateString(),
                                start = new DateTime(itm.Year, int.Parse(itm.Month), itm.Day).ToShortDateString(),
                               backgroundColor = bgColor,
                               allDay = true,
                             //  allDay = itm.CheckInTime.HasValue ? false : true,
                               id = bgColor == EventColors.Edited && itm.AcctStatus == 7 ? "disapprove" : "",
                               // textColor = bgColor == EventColors.Unplanned || bgColor == EventColors.Plan || bgColor == EventColors.Visited ? "white" : "black",
                               //   textColor = bgColor == EventColors.Deleted ? "black" : "white",
                               textColor = "black"
                           });
                   }
               }
               else if (viewtype.ToUpper() == "DAILY")
               {
                   var qry = (from evntHdr in DATABASE.CoverageHdrs
                              from evntDtl in DATABASE.CoverageDtls
                              //from cust in DATABASE.customerHeaders
                              from cust in DATABASE.arms2_vw_customerheader_lookup
                              where evntHdr.EventID == evntDtl.EventID
                              && evntDtl.AccountCode == cust.SapAcctCode
                              && evntHdr.EmpIdNo == userId
                              && evntHdr.Month == month
                              && evntHdr.Year == year
                              && evntDtl.Day == day

                              select new
                              {
                                  evntHdr.EventID,
                                  evntHdr.Year,
                                  evntHdr.Month,
                                  evntDtl.Day,
                                  evntDtl.AccountCode,
                                  evntDtl.isPlanned,
                                  evntDtl.IsDeleted,
                                  evntDtl.IsAnEdit,
                                  evntDtl.AcctStatus,
                                  evntDtl.hasCallreport,
                                  cust.acctName,
                                  evntDtl.LineNum
                              }).Distinct().ToList();

                   foreach (var itm in qry)
                   {
                       bgColor = GetColor(itm.isPlanned, itm.IsDeleted, itm.IsAnEdit, itm.hasCallreport, itm.AcctStatus);
                       var sales = (from eventDtl1 in DATABASE.CoverageDtl1
                                    where eventDtl1.Day == itm.Day
                                    && eventDtl1.EventID == itm.EventID
                                   && eventDtl1.LineNum== itm.LineNum && eventDtl1.ObjectiveCode=="S"
                                    select eventDtl1.PlannedAmount).Sum();

                       qry_2 = (List<Sales>)(from eventDtl1 in DATABASE.CoverageDtl1
                                             where eventDtl1.Day == itm.Day
                                             && eventDtl1.EventID == itm.EventID
                                            && eventDtl1.LineNum == itm.LineNum && eventDtl1.ObjectiveCode == "S"
                                             select new Sales
                                             {
                                                 brand = eventDtl1.Brand,
                                                 amount = eventDtl1.PlannedAmount
                                             }).ToList();

                       totalSales += (sales.HasValue ? sales.Value : 0);
                       if (bgColor != "")
                           result.Add(new FullCalendarEvents()
                           {
                               //title = "[" + itm.AccountCode + "] " + itm.acctName + " - Target Sales(" + (sales.HasValue ? sales.Value.ToString("#,0.00") : "0.00") + ")",
                               title = "[" + itm.AccountCode + "] " + itm.acctName,
                               start = new DateTime(itm.Year, int.Parse(itm.Month), itm.Day).ToShortDateString(),
                               backgroundColor = bgColor,
                               allDay = true,
                               //  textColor = bgColor == EventColors.Unplanned || bgColor == EventColors.Plan || bgColor==EventColors.Visited ? "white" : "black",
                               //textColor = bgColor == EventColors.Deleted ? "black" : "white",
                               textColor = "black",
                               id = itm.AccountCode,
                               salesdetails = qry_2,
                               totalSales = sales
                           });
                   }
                   if (qry.Count > 0)
                   {
                       result.Add(new FullCalendarEvents()
                       {
                           title = "TOTAL TARGET SALES FOR THE DAY - (" + totalSales.ToString("#,0.00") + ")",
                           start = new DateTime(qry.First().Year, Convert.ToInt32(qry.First().Month), qry.First().Day).ToShortDateString(),
                           backgroundColor = "white",
                           allDay = true
                       });
                   }
               }
               else
               {

                   var qry = (from evntHdr in DATABASE.CoverageHdrs
                              from evntDtl in DATABASE.CoverageDtls
                              where evntHdr.EventID == evntDtl.EventID
                              && evntHdr.EmpIdNo == userId
                              && evntHdr.Month == month
                              && evntHdr.Year == year

                              select new
                              {
                                  evntHdr.EventID,
                                  evntHdr.Year,
                                  evntHdr.Month,
                                  evntDtl.Day,
                                  evntDtl.AccountCode,
                                  evntDtl.isPlanned,
                                  evntDtl.IsDeleted,
                                  evntDtl.IsAnEdit,
                                  evntDtl.AcctStatus,
                                  evntDtl.hasCallreport
                              }).Distinct().ToList();

                   foreach (var itm in qry)
                   {
                       bgColor = GetColor(itm.isPlanned, itm.IsDeleted, itm.IsAnEdit, itm.hasCallreport, itm.AcctStatus);

                       if (bgColor != "")
                           result.Add(new FullCalendarEvents()
                           {
                               title = itm.AccountCode,
                               start = new DateTime(itm.Year, int.Parse(itm.Month), itm.Day).ToShortDateString(),
                               backgroundColor = bgColor,
                               allDay = true,
                             //  textColor = bgColor == EventColors.Unplanned || bgColor == EventColors.Plan ? "white" : "black"
                               //textColor = bgColor == EventColors.Unplanned || bgColor == EventColors.Plan || bgColor == EventColors.Visited ? "white" : "black"
                               textColor="black"
                           });
                   }
               }
               
               DATABASE.Dispose();
               return result;
           }


           static string GetColor(string isPlanned, string isDeleted, string isAnEdit, string hasCallReport, int? acctStatus)
           {
               if (isPlanned == "T" && isDeleted == "F" && isAnEdit == "F" && hasCallReport == "F")
                   return EventColors.Plan;

               if (isDeleted == "T")
                   return EventColors.Deleted;

               if (hasCallReport == "T" && isDeleted == "F" && isPlanned == "T")
                   return EventColors.Visited;

               if (isAnEdit == "T" && hasCallReport == "F" && acctStatus != 7)
                   return EventColors.Edited;

               if (isPlanned == "F")
                   return EventColors.Unplanned;

               if (isAnEdit == "T" && hasCallReport == "F" && acctStatus == 7)
                   return EventColors.Edited;

               return "";
           }

           public static List<FullCalendarEvents> GetAllDayEvents_viewer(string userId, string month, int year, string viewtype, int day = 1)
           {
               Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
               List<FullCalendarEvents> result = new List<FullCalendarEvents>();
               List<Sales> qry_2 = new List<Sales>();
               string bgColor = "";
               decimal totalSales = 0;
               if (viewtype.ToUpper() == "MONTHLY")
               {

                   var qry = (from evntHdr in DATABASE.CoverageHdrs
                              from evntDtl in DATABASE.CoverageDtls
                              where evntHdr.EventID == evntDtl.EventID
                              && evntHdr.EmpIdNo == userId
                              && evntHdr.Month == month
                              && evntHdr.Year == year
                              && evntDtl.AcctStatus != 7
                              select new
                              {
                                  evntHdr.EventID,
                                  evntHdr.Year,
                                  evntHdr.Month,
                                  evntDtl.Day,
                                  evntDtl.AccountCode,
                                  evntDtl.isPlanned,
                                  evntDtl.IsDeleted,
                                  evntDtl.IsAnEdit,
                                  evntDtl.AcctStatus,
                                  evntDtl.hasCallreport,
                                  evntDtl.CheckInTime,
                                  evntDtl.CheckOutTime
                              }).Distinct().ToList();


                   foreach (var itm in qry)
                   {
                       bgColor = GetColor(itm.isPlanned, itm.IsDeleted, itm.IsAnEdit, itm.hasCallreport, itm.AcctStatus);

                       if (bgColor != "")
                           result.Add(new FullCalendarEvents()
                           {
                               title = itm.AccountCode,
                               start = itm.hasCallreport == "T" ?
                                                    (itm.CheckInTime.HasValue ? itm.CheckInTime.Value.ToString() : new DateTime(itm.Year, int.Parse(itm.Month), itm.Day).ToShortDateString()) 
                                                    : 
                                                    new DateTime(itm.Year, int.Parse(itm.Month), itm.Day).ToShortDateString(),
                               backgroundColor = bgColor,
                               allDay = itm.hasCallreport == "T" ? false : true,
                               //allDay = itm.CheckInTime.HasValue ? false : true,
                               id = bgColor == EventColors.Edited && itm.AcctStatus == 7 ? "disapprove" : "",
                               end = itm.hasCallreport == "T" ? 
                                                       (itm.CheckInTime.HasValue ? itm.CheckOutTime.GetValueOrDefault().ToString() : new DateTime(itm.Year, int.Parse(itm.Month), itm.Day).ToShortDateString()) 
                                                       :
                                                       new DateTime(itm.Year, int.Parse(itm.Month), itm.Day).ToShortDateString(),
                               textColor = "black"
                           });
                   }
               }
               else if (viewtype.ToUpper() == "DAILY")
               {
                   var qry = (from evntHdr in DATABASE.CoverageHdrs
                              from evntDtl in DATABASE.CoverageDtls
                              //from cust in DATABASE.customerHeaders
                              from cust in DATABASE.arms2_vw_customerheader_lookup
                              where evntHdr.EventID == evntDtl.EventID
                              && evntDtl.AccountCode == cust.SapAcctCode
                              && evntHdr.EmpIdNo == userId
                              && evntHdr.Month == month
                              && evntHdr.Year == year
                              && evntDtl.Day == day

                              select new
                              {
                                  evntHdr.EventID,
                                  evntHdr.Year,
                                  evntHdr.Month,
                                  evntDtl.Day,
                                  evntDtl.AccountCode,
                                  evntDtl.isPlanned,
                                  evntDtl.IsDeleted,
                                  evntDtl.IsAnEdit,
                                  evntDtl.AcctStatus,
                                  evntDtl.hasCallreport,
                                  cust.acctName,
                                  evntDtl.LineNum,
                                  evntDtl.CheckInTime,
                                  evntDtl.CheckOutTime
                              }).Distinct().ToList();

                   foreach (var itm in qry)
                   {
                       bgColor = GetColor(itm.isPlanned, itm.IsDeleted, itm.IsAnEdit, itm.hasCallreport, itm.AcctStatus);
                       var sales = (from eventDtl1 in DATABASE.CoverageDtl1
                                    where eventDtl1.Day == itm.Day
                                    && eventDtl1.EventID == itm.EventID
                                   && eventDtl1.LineNum == itm.LineNum && eventDtl1.ObjectiveCode == "S"
                                    select eventDtl1.PlannedAmount).Sum();

                       qry_2 = (List<Sales>)(from eventDtl1 in DATABASE.CoverageDtl1
                                             where eventDtl1.Day == itm.Day
                                             && eventDtl1.EventID == itm.EventID
                                            && eventDtl1.LineNum == itm.LineNum && eventDtl1.ObjectiveCode == "S"
                                             select new Sales
                                             {
                                                 brand = eventDtl1.Brand,
                                                 amount = eventDtl1.PlannedAmount
                                             }).ToList();

                       totalSales += (sales.HasValue ? sales.Value : 0);
                       if (bgColor != "")
                           result.Add(new FullCalendarEvents()
                           {
                               title = "[" + itm.AccountCode + "] " + itm.acctName,
                               start = itm.CheckInTime.HasValue ? itm.CheckInTime.Value.ToString() : new DateTime(itm.Year, int.Parse(itm.Month), itm.Day).ToShortDateString(),
                               backgroundColor = bgColor,
                               allDay = itm.CheckOutTime.HasValue ? false : true,
                               textColor = "black",
                               id = itm.AccountCode,
                               salesdetails = qry_2,
                               totalSales = sales,
                               end = itm.CheckOutTime.HasValue ? itm.CheckOutTime.Value.ToString() : new DateTime(itm.Year, int.Parse(itm.Month), itm.Day).ToShortDateString(),
                           });
                   }
                   if (qry.Count > 0)
                   {
                       result.Add(new FullCalendarEvents()
                       {
                           title = "TOTAL TARGET SALES FOR THE DAY - (" + totalSales.ToString("#,0.00") + ")",
                           start = new DateTime(qry.First().Year, Convert.ToInt32(qry.First().Month), qry.First().Day).ToShortDateString(),
                           backgroundColor = "white",
                           allDay = true
                       });
                   }
               }
               else
               {

                   var qry = (from evntHdr in DATABASE.CoverageHdrs
                              from evntDtl in DATABASE.CoverageDtls
                              where evntHdr.EventID == evntDtl.EventID
                              && evntHdr.EmpIdNo == userId
                              && evntHdr.Month == month
                              && evntHdr.Year == year

                              select new
                              {
                                  evntHdr.EventID,
                                  evntHdr.Year,
                                  evntHdr.Month,
                                  evntDtl.Day,
                                  evntDtl.AccountCode,
                                  evntDtl.isPlanned,
                                  evntDtl.IsDeleted,
                                  evntDtl.IsAnEdit,
                                  evntDtl.AcctStatus,
                                  evntDtl.hasCallreport
                              }).Distinct().ToList();

                   foreach (var itm in qry)
                   {
                       bgColor = GetColor(itm.isPlanned, itm.IsDeleted, itm.IsAnEdit, itm.hasCallreport, itm.AcctStatus);

                       if (bgColor != "")
                           result.Add(new FullCalendarEvents()
                           {
                               title = itm.AccountCode,
                               start = new DateTime(itm.Year, int.Parse(itm.Month), itm.Day).ToShortDateString(),
                               backgroundColor = bgColor,
                               allDay = true,
                               //  textColor = bgColor == EventColors.Unplanned || bgColor == EventColors.Plan ? "white" : "black"
                               //textColor = bgColor == EventColors.Unplanned || bgColor == EventColors.Plan || bgColor == EventColors.Visited ? "white" : "black"
                               textColor = "black"
                           });
                   }
               }

               DATABASE.Dispose();
               return result;
           }

           public class CalendarInfo
           {
               public string EventID { get; set; }
               public string Status { get; set; }
               public int TotalVisit { get; set; }
               public int TotalEvents { get; set; }
               public int TotalPlanned { get; set; }
               public int TotalUnplanned { get; set; }
               public int TotalEdited { get; set; }
               public int TotalCallEffective { get; set; }
               public int AverageCallEffective { get; set; }
           }

           public static CalendarInfo GetCalendarInfo(string userId, string month,int year)
           {
               var res = new CalendarInfo();
               var DATABASE = new Models.ARMSTestEntities();
               var approvalstates = DATABASE.approvalStates.Where(p => p.docType == (int)Globals.InfoType.CalendarEvent);

               approvalstates = approvalstates.GroupBy(o => o.stateID).Select(grp => grp.FirstOrDefault());

               var qry = (from a in DATABASE.CoverageHdrs
                          from b in DATABASE.CoverageDtls
                          from c in approvalstates
                          where a.EmpIdNo == userId && a.Month == month && a.Year == year
                            && a.EventID == b.EventID && a.DoctypeId==c.docType && c.stateID==a.DocumentStatusId
                            && b.IsDeleted!="T"
                          select new { b.EventID, b.LineNum, b.hasCallreport, b.isPlanned, b.IsAnEdit,c.stateDesc, b.AcctStatus});

               foreach (var itm in qry)
               {
                   res.EventID = itm.EventID;
                   res.Status = itm.stateDesc;

                   res.TotalEvents = qry.Count();
                   res.TotalEdited = qry.Count(o => o.IsAnEdit == "T");
                   res.TotalPlanned = qry.Count(o => o.isPlanned == "T");
                   res.TotalUnplanned = qry.Count(o => o.isPlanned != "T");
                   res.TotalVisit = qry.Count(o => o.hasCallreport == "T");

                   res.TotalCallEffective = qry.Count(o => o.isPlanned == "T" && o.hasCallreport == "T");

                 //  List<int> calleffective = new List<int> { res.TotalCallEffective, res.TotalPlanned };
                   var z = Convert.ToDouble(res.TotalCallEffective) / Convert.ToDouble(res.TotalPlanned);
                   res.AverageCallEffective = Convert.ToInt32(Math.Floor(z * 100.0));
                  // res.AverageCallEffective = calleffective.Average();
                   break;
               }

               DATABASE.Dispose();
               return res;
           }

            public class SalesMonitoringInfo
            {
                public string empIdNo {get;set;}
                public int slpCode {get;set;}
                public string gross {get;set;}
                public string cm { get; set; }
                public string unposted { get; set; }
                public string posted { get; set; }
                public string pending { get; set; }
                public string balanceorder { get; set; }
                public string netposted { get; set; }
                public int? noTransactingAccounts { get; set; }
                public string dateasof { get; set; }
                public bool isNull { get; set; }
            }

            public static SalesMonitoringInfo getSalesMonitoringInfo(string userId)
            {
                SalesMonitoringInfo result = new SalesMonitoringInfo();
                Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

           //     Models.mtc_vw_so_salesmonitoringdtl getData = DATABASE.mtc_vw_so_salesmonitoringdtl.SingleOrDefault(o => o.empIdNo == userId);
                Models.mtc_vw_uploadsalesreport getData = DATABASE.mtc_vw_uploadsalesreport.SingleOrDefault(o => o.empidno == userId);
                result.isNull = true;
                if (getData != null)
                {
                    result.empIdNo = getData.empidno;
                    result.slpCode = getData.slpcode;
                    //result.gross = getData.GROSS.Value.ToString("0.00");
                    //result.cm = getData.CM.Value.ToString("0.00");
                    //result.unposted = getData.UNPOSTED.Value.ToString("0.00");
                    //result.posted = getData.POSTED.Value.ToString("0.00");
                    //result.pending = getData.PENDING.Value.ToString("0.00");
                    //result.balanceorder = getData.BALANCE_ORDER.Value.ToString("0.00");
                    //result.netposted = getData.NET_POSTED.Value.ToString("0.00");
                    //result.noTransactingAccounts = getData.NoOfTransactingAccts.Value;
                    result.gross = getData.GROSS != 0 ? getData.GROSS.Value.ToString("#,###.00") : "0.00";
                    result.cm = getData.CM != 0 ? getData.CM.Value.ToString("#,###.00") : "0.00";
                    result.unposted = getData.UNPOSTED != 0 ? getData.UNPOSTED.Value.ToString("#,###.00") : "0.00";
                    result.posted = getData.POSTED != 0 ? getData.POSTED.Value.ToString("#,###.00") : "0.00";
                    result.pending = getData.PENDING != 0 ? getData.PENDING.Value.ToString("#,###.00") : "0.00";
                    result.balanceorder = getData.BALANCE_ORDER != 0 ? getData.BALANCE_ORDER.Value.ToString("#,###.00") : "0.00";
                    result.netposted = getData.NET_POSTED != 0 ? getData.NET_POSTED.Value.ToString("#,###.00") : "0.00";
                    result.noTransactingAccounts = getData.NoOfTransactingAccts.Value;
                    result.dateasof = getData.uploaddatetime.Value.ToShortDateString();
                    result.isNull = false;
                }

                return result;
            }

        }
        
    }
}
