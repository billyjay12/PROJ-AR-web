using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARMS_W.Interface.Event
{
   public interface EventHdr
    {
        //HEADER HERE
         string EventID { get; set; }
         int Year { get; set; }
         string Month { get; set; }
         int Day { get; set; }
         string EmpIdNo { get; set; }
         bool isExist { get; set; }
         int DoctypeId { get; set; }
         int DocumentStatusId { get; set; }
         string action_type { get; set; }
         string LineNum { get; set; }
         
         List<collection> collection_list { get; set; }
         List<Merchandise> msde_list { get; set; }
         List<Sales> sales_list { get; set; }
         List<CustomerSrvc> customersrvc_list { get; set; }

        string UpdateToDB(ref Class.SQLTransaction sql_trans);
        //int UpdateDocStatus(ref  Class.SQLTransaction sql_trans, SkelClass.Globals.DocActionType action_type);
        int UpdateDocStatus(ref ARMS_W.Class.SQLTransaction sql_trans, string EventID, string action_type);

    }


   public interface collection
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
   }

   public interface Merchandise
   {
        string Objdesc { get; set; }
        string ObjectiveCode { get; set; }
        string AccountCode { get; set; }
        string AccountName { get; set; }
        string AccountAddress { get; set; }
        string AccountClass { get; set; }
        string ContactPerson { get; set; }

   }

   public interface Sales
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

   }

   public interface CustomerSrvc
   {
        string Objdesc { get; set; }
        string AccountCode { get; set; }
        string AccountName { get; set; }
        string AccountAddress { get; set; }
        string AccountClass { get; set; }
        string ContactPerson { get; set; }
        string ContactPersonNo { get; set; }
        string ObjectiveCode { get; set; }

   }


}
