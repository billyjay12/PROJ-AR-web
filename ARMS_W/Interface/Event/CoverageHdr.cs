using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARMS_W;

namespace ARMS_W.Interface.Event
{
   public interface CoverageHdr
    {

        string EventID { get; set; }
        int Year { get; set; }
        string Month { get; set; }
        int Day { get; set; }
        string EmpIdNo { get; set; }
        int DoctypeId { get; set; }
        int DocumentStatusId { get; set; }
        string action_type { get; set; }
        string LineNum { get; set; }
        string AccountCode { get; set; }
        string ObjectiveCode { get; set; }
        string ContactPerson { get; set; }
        string ContactPersonNo { get; set; }
        bool isExist { get; set; }
        string StoreChecking { get; set; }
        string IssuesAndConcerns { get; set; }
        string StoreCheckingResult { get; set; }
        string Delivery { get; set; }
        string Orders { get; set; }
        string SummaryLackingItems { get; set; }
        string Recommendation { get; set; }
        string TimeTable { get; set; }
        string Remarks { get; set; }
        string CompetitorActivities { get; set; }
        string WithOrder { get; set; }
        string NextCallDate { get; set; }
        string OtherInformation { get; set; }
        string ProductPresentationResult { get; set; }
        string cFullCollection { get; set; }
        string cPartialCollection { get; set; }
        string cNoCollection { get; set; }
        string IsAnEdit { get; set; }
        string RmrkChanges { get; set; }
        //string FileAttachment { get; set; }
        string hasCallreport { get; set; }
        List<collections> collection_list { get; set; }
        List<merchandising> merchandising_list { get; set; }
        List<sales> sales_list { get; set; }
        int qryExist { get; set; }
        string isPlanned { get; set; }
        DateTime Tmein { get; set; }
        DateTime Tmeout { get; set; }
        int Numvisit { get; set; }
        string HotelName { get; set; }
        string HotelContactNum { get; set; }
        int AcctStatus { get; set; }
        string isDeleted { get;set;}
        string Attachment { get; set; }
        string ApprvrRrmks { get; set; }

        decimal? ColPostDatedCheck { get; set; }
        decimal? ColDatedCheck { get; set; }
        decimal? ColTotal { get; set; }
        string ColRemarks { get; set; }


        List<customersrvc> customersrv_list { get; set; }
        List<collections> uncollection_list { get; set; }
        List<sales> unsales_list { get; set; }
        List<changesdtls> Acct_dtls { get; set; }
        List<merchandising> unmerchandising_list { get; set; }
   
       
       


        string UpdateToDBCoverage(ref Class.SQLTransaction sql_trans);
        string UpdateDBCallReport(ref Class.SQLTransaction sql_trans);
        string UpdateToDBCoverageChanges(ref Class.SQLTransaction sql_trans,string username);
        string UpdateToDBNextInventoryCount(ref Class.SQLTransaction sql_trans);
       // string UpdateDocumentStatus(ref ARMS_W.Class.SQLTransaction sql_trans, string EventID, string action_type);
        int UpdateDocStatus(ref ARMS_W.Class.SQLTransaction sql_trans, string EventID, string action_type, string username, bool isExist, string remarks);
        string UpdateDeletion(ref Class.SQLTransaction sql_trans);
        string insertRouteRmrks(ref Class.SQLTransaction sql_trans ,string EventID, string Remarks); 
    }



   public interface collections
   {
       string Objdesc { get; set; }
       string ObjectiveCode { get; set; }
       string Brand { get; set; }
       string Amount { get; set; }
       string ActualAmount { get; set; }
       string Remarks { get; set; }

   }

   public interface merchandising
   {
       string Objdesc { get; set; }
       string ObjectiveCode { get; set; }
       string Brand { get; set; }
       string Amount { get; set; }
       string Productpresented { get; set; }
       string counterclerk { get; set; }
       string CounterClerkNo { get; set; }
       string Remarks { get; set; }

   }
   public interface sales
   {
       string Objdesc { get; set; }
       string ObjectiveCode { get; set; }
       string Brand { get; set; }
       string Amount { get; set; }
       string ActualAmount { get; set; }
       string Remarks { get; set; }
       string dtlsRrmks { get; set; }


   }

   public interface customersrvc
   {
       string Objdesc { get; set; }
       string ObjectiveCode { get; set; }
       string Brand { get; set; }
       string Amount { get; set; }

   }

   public interface changesdtls
   {

        int Day { get; set; }
        string AccountCode { get; set; }
        int cAcctStatus { get; set; }
        string RmrkChanges { get; set; }
        string linenum { get; set; }
        

   }

   public interface nextInventory
   {
       string AccountCode { get; set; }
       string ContactPerson { get; set; }
       string ContactPersonNo { get; set; }
       int Day { get; set; }
       string ObjectiveCode { get; set; }
   
   }




}




