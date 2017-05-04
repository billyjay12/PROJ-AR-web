using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARMS_W.Interface.Event
{
   public interface CallReport
    {
         string cFullCollection { get; set; }
         string cPartialCollection { get; set; }
         string cNoCollection { get; set; }
         string EventID { get; set; }
         string AccountCode { get; set; }
         string ObjectiveCode { get;set;}
         int Year { get; set; }
         string Month { get; set; }
         int Day { get; set; }
         string EventDesc { get; set; }
         string StoreCheckingResult { get; set; }
         string LineNum { get; set; }
         string WithOrder { get; set; }
         string NextCalldate { get; set; }
         string OtherInformation { get; set; }
         string CompetitorActivities { get; set; }
         string IssuesAndConcerns { get; set; }
         string Delivery { get; set; }
         string Orders { get; set; }
         string SummaryLackingItems { get; set; }
         string Remarks { get; set; }
         string Recommendation { get; set; }
         string TimeTable { get; set; }
         string SoId { get; set; }
         List<presented> Presented_list { get; set; }
         List<forceObjective> forceObjective_list { get; set; }

         string UpdateToDB(ref Class.SQLTransaction sql_trans);
         string UpdateToDB1(ref Class.SQLTransaction sql_trans);


    }

   public interface presented  
   {
       string Brand { get; set; }
       string CounterClerk { get; set; }
   
   }

   public interface forceObjective
   {
       string Objdesc { get; set; }
       string AccountCode { get; set; }
       string AccountName { get; set; }
       string AccountAddress { get; set; }
       string AccountClass { get; set; }
       string ContactPerson { get; set; }
       string ContactNumber { get; set; }
       string ObjectiveCode { get; set; }
       string Brand { get; set; }
       string Amount { get; set; }
       string cFullCollection { get; set; }
       string cPartialCollection { get; set; }
       string cNoCollection { get; set; }
       string Remarks { get; set; }
   }
}
