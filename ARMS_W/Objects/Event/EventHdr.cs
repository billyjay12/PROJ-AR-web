using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using ARMS_W.Class;
using ARMS_W.SkelClass;

namespace ARMS_W.Objects.Event
{
    /**
     *  @MATIMCO INC.
     *  Event Header
     *  
     * Implementation of functions which stores data 
     * from page to the database
     * 
     * Author: Hervie T. Inoc
     * Copyright(c) 2013 MATIMCO INCORPORATED
     * Date Created: April 18, 2013
     * 
     * for Modification purposes if arise
     * 
     * Modified By:
     * Date Modified:
     * 
     * **/


    //HEADER AREA
    public class EventHdr : ARMS_W.Interface.Event.EventHdr
    {

        public string EventID { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public int Day { get; set; }
        public string EmpIdNo { get; set; }
        public bool isExist { get; set; }
        public int DoctypeId { get; set; }
        public int DocumentStatusId { get; set; }
        public string LineNum { get; set; }
        public string action_type { get; set; }
        public List<ARMS_W.Interface.Event.collection> collection_list { get; set; }
        public List<ARMS_W.Interface.Event.Merchandise> msde_list { get; set; }
        public List<ARMS_W.Interface.Event.Sales> sales_list { get; set; }
        public List<ARMS_W.Interface.Event.CustomerSrvc> customersrvc_list { get; set; }

        public EventHdr()
        {
            this.collection_list = new List<Interface.Event.collection>();
            this.msde_list = new List<Interface.Event.Merchandise>();
            this.sales_list = new List<Interface.Event.Sales>();
            this.customersrvc_list = new List<Interface.Event.CustomerSrvc>();
        
        }

        // FUNCTIONS
        #region UPDATE TO DB
        public string UpdateToDB(ref ARMS_W.Class.SQLTransaction sql_trans)
        {
            //Work Around in Getting the code of event plan 
            string obj_code = null; ;
            //initializing model connector
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            if (isExist != true)
            {
                string tmp_id = this.EventID == null ? this.GetNewCode() : this.EventID;
                this.EventID = tmp_id;

          }

            #region SAVING HEADER
            // Inserting data from page to header

           if (isExist!=true)
            {
                sql_trans.InsertTo("EventHdr", new Dictionary<string, object>() {
                {"EventID",this.EventID},
                {"EmpIdNo",this.EmpIdNo},
                {"Year",this.Year},
                {"Month",this.Month},
                {"DoctypeId",this.DoctypeId},
                {"DocumentStatusId",this.DocumentStatusId}
                });
            }

            #endregion

            #region SAVING COLLECTION
            if (this.collection_list != null && this.collection_list.Count!=0)
            {
                var list1 = new List<Interface.Event.collection>();
                list1 = collection_list.GroupBy(o => o.AccountCode).Select(grp => grp.First()).ToList();            
                
                string tmp_eventid = "", tmp_Accountcode = "", tmp_objc = "";
                Int32 beggining_linenum = Convert.ToInt32(this.GetLineNumCode().Replace("SEQ",""));

                foreach (collection p_collection in list1)
                {
                    string tmp_linenum = null;

                    int isExistindtls =0;

                    try { isExistindtls = DATABASE.EventDtls.Count(p => p.AccountCode == p_collection.AccountCode && p.Day == this.Day && p.ObjectiveCode == p_collection.ObjectiveCode && p.EventID == this.EventID);
                    tmp_linenum = DATABASE.EventDtls.Single(p => p.AccountCode == p_collection.AccountCode && p.Day == this.Day && p.ObjectiveCode == p_collection.ObjectiveCode && p.EventID == this.EventID).LineNum;
                    }
                    catch (Exception ex) { }

                    if (isExistindtls > 0)
                    {

                        foreach (collection test_collection in collection_list.Where(p => p.AccountCode == p_collection.AccountCode))
                        {
                            //check if details is in List

                            var qry_count = (from dtl1 in DATABASE.EventDtl1
                                             where dtl1.LineNum == tmp_linenum && dtl1.AccountCode == test_collection.AccountCode
                                             && dtl1.Brand == test_collection.Brand
                                             select new { }).ToList();
                        if(qry_count.Count ==0)
                        sql_trans.InsertTo("EventDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", tmp_linenum},
                        {"Day",this.Day},
                        {"AccountCode",test_collection.AccountCode},
                        {"ObjectiveCode",test_collection.ObjectiveCode},
                        {"Brand", test_collection.Brand},
                        {"Amount",test_collection.Amount}

                        });
                        }


                    }

                    else
                    {
                        this.LineNum =  string.Format("{0:000000}", "SEQ" + string.Format("{0:000000}", beggining_linenum));

                        sql_trans.InsertTo("EventDtls", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"AccountCode",p_collection.AccountCode},
                        {"AccountName", p_collection.AccountName},
                        {"AccountAddress", p_collection.AccountAddress},
                        {"AccountClass",p_collection.AccountClass},
                        {"ObjectiveCode",p_collection.ObjectiveCode},
                        {"ContactPerson",p_collection.ContactPerson},
                        {"ContactPersonNo", p_collection.ContactNumber}

                        });


                        foreach (collection test_collection in collection_list.Where(p => p.AccountCode == p_collection.AccountCode))
                        {
                            sql_trans.InsertTo("EventDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"AccountCode",test_collection.AccountCode},
                        {"ObjectiveCode",test_collection.ObjectiveCode},
                        {"Brand", test_collection.Brand},
                        {"Amount",test_collection.Amount}

                        });
                        }
                        beggining_linenum++;
                    }
                }

         
            }
            #endregion

            #region SAVING MERCHANDISE

            if (this.msde_list != null && this.msde_list.Count!=0) 
            {
                this.LineNum = this.GetLineNumCode();
                foreach (Merchandise p_msde in msde_list)
                {
                                        
                    sql_trans.InsertTo("EventDtls", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"Day",this.Day},
                        {"LineNum",this.LineNum}, 
                        {"AccountCode",p_msde.AccountCode},
                        {"AccountName",p_msde.AccountName},
                        {"AccountAddress", p_msde.AccountAddress},
                        {"AccountClass",p_msde.AccountClass},
                        {"ObjectiveCode",p_msde.ObjectiveCode},
                        {"ContactPerson",p_msde.ContactPerson},
                        
                        }); 
                }

            }
            #endregion

            #region SAVING SALES

            if (this.sales_list != null && this.sales_list.Count!=0)
            {
                var list1 = new List<Interface.Event.Sales>();
                list1 = sales_list.GroupBy(o => o.AccountCode).Select(grp => grp.First()).ToList();
                string tmp_eventid = "", tmp_Accountcode = "", tmp_objc = "";
                Int32 beggining_linenum = Convert.ToInt32(this.GetLineNumCode().Replace("SEQ", ""));

                foreach (Sales p_sales in list1)
                {
                   

                    string tmp_linenum = null;

                    int isExistindtls = 0;

                    try
                    {
                        isExistindtls = DATABASE.EventDtls.Count(p => p.AccountCode == p_sales.AccountCode && p.Day == this.Day && p.ObjectiveCode == p_sales.ObjectiveCode && p.EventID == this.EventID);
                        tmp_linenum = DATABASE.EventDtls.Single(p => p.AccountCode == p_sales.AccountCode && p.Day == this.Day && p.ObjectiveCode == p_sales.ObjectiveCode && p.EventID == this.EventID).LineNum;
                    }
                    catch (Exception ex) { }

                    if (isExistindtls > 0)
                    {

                        foreach (Sales test_sales in sales_list.Where(p => p.AccountCode == p_sales.AccountCode))
                        {
                            //check if details is in List

                            var qry_count = (from dtl1 in DATABASE.EventDtl1
                                             where dtl1.LineNum == tmp_linenum && dtl1.AccountCode == test_sales.AccountCode
                                             && dtl1.Brand == test_sales.Brand
                                             select new { }).ToList();
                            if (qry_count.Count == 0)
                                sql_trans.InsertTo("EventDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", tmp_linenum},
                        {"Day",this.Day},
                        {"AccountCode",test_sales.AccountCode},
                        {"ObjectiveCode",test_sales.ObjectiveCode},
                        {"Brand", test_sales.Brand},
                        {"Amount",test_sales.Amount}

                        });
                        }


                    }

                    else 
                    {
                        this.LineNum = string.Format("{0:000000}", "SEQ" + string.Format("{0:000000}", beggining_linenum));

                        sql_trans.InsertTo("EventDtls", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"AccountCode",p_sales.AccountCode},
                        {"AccountName",p_sales.AccountName},
                        {"AccountAddress", p_sales.AccountAddress},
                        {"AccountClass",p_sales.AccountClass},
                        {"ObjectiveCode",p_sales.ObjectiveCode},
                        {"ContactPerson",p_sales.ContactPerson},
                        {"ContactPersonNo", p_sales.ContactNumber}
                        
                        });



                        foreach (Sales test_sales in sales_list.Where(p => p.AccountCode == p_sales.AccountCode))
                        {
                            sql_trans.InsertTo("EventDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day", this.Day},
                        {"AccountCode",test_sales.AccountCode},
                        {"ObjectiveCode",test_sales.ObjectiveCode},
                        {"Brand", test_sales.Brand},
                        {"Amount",test_sales.Amount}
                        
                        });
                        }
                        beggining_linenum++;

                    
                    }


                   

                
                }
            }
                #endregion

            #region SAVING CUSTOMERSERVICE
            if (this.customersrvc_list != null && this.customersrvc_list.Count!=0)
            {
                this.LineNum = this.GetLineNumCode();
                foreach (CustomerSrvc p_cusrvc in customersrvc_list)
                {
                    

                        sql_trans.InsertTo("EventDtls", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"AccountCode",p_cusrvc.AccountCode},
                        {"AccountName",p_cusrvc.AccountName},
                        {"AccountAddress", p_cusrvc.AccountAddress},
                        {"AccountClass",p_cusrvc.AccountClass},
                        {"ObjectiveCode",p_cusrvc.ObjectiveCode},
                        {"ContactPerson",p_cusrvc.ContactPerson},
                        {"ContactPersonNo",p_cusrvc.ContactPersonNo}
                        
                        });
                    
                
                }
            
            }

            #endregion

            return this.EventID; 
        }
        #endregion

        #region Update Status
        public int UpdateDocStatus(ref Class.SQLTransaction sql_trans, string eventID, string action_type)
        {
            

            if (action_type == SkelClass.Globals.DocActionType.Approve.ToString())
            {
                // get new id if approved
                int nextid = 0;
                //LoyaltyLib.Model.LYLTTestEntities DATABASE = new Model.LYLTTestEntities();
               // Class.SQLTransaction sql_trans = new Class.SQLTransaction();
                ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

                    IEnumerable<ARMS_W.Objects.Event.EventHdr> curr_doc_status =
                    (from eventhdr in DATABASE.EventHdrs
                     where eventhdr.EventID == eventID
                     select new ARMS_W.Objects.Event.EventHdr()
                     {
                       DocumentStatusId =(Int16)eventhdr.DocumentStatusId
                     
                     }).Take(1);

                foreach(var itm in curr_doc_status)
                {
                
                    nextid = itm.DocumentStatusId+2;
                }

                sql_trans.CommandText = Class.QueryBuilder.UpdateTo("EventHdr",
                    new Dictionary<string, object>() {{"DocumentStatusId", nextid }},
                    new Dictionary<string, object>() { { "EventID", eventID } });

                DATABASE.Dispose();

                #region SEND EMAIL

                var docStatus = MiscFunctions.getDocumentStatusMessage((int)Globals.InfoType.CalendarEvent, nextid);
                var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, nextid);
                string mail_body = AppHelper.Arms_Url + "?id=" + this.EventID + "&doctype=calendar&Year=" + this.Year + "&Month=" + this.Month;

                var subj = "ARMS Calendar Coverage Plan[" + docStatus.stateDesc + "]";

                foreach (var itm in emails)
                {
                      MiscFunctions.sendMail(itm.EmailAddress, subj, mail_body);
                   // MiscFunctions.sendEmail1(mail_body, subj, itm.EmailAddress);
                }

                #endregion
                
            }

            if (action_type == SkelClass.Globals.DocActionType.ReturnToRequestor.ToString())
            {
                int previd = 0;

                ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

                IEnumerable<ARMS_W.Objects.Event.EventHdr> curr_doc_status =
                (from eventhdr in DATABASE.EventHdrs
                 where eventhdr.EventID == eventID
                 select new ARMS_W.Objects.Event.EventHdr()
                 {
                     DocumentStatusId = (Int16)eventhdr.DocumentStatusId

                 }).Take(1);

                foreach (var itm in curr_doc_status)
                {

                    previd = itm.DocumentStatusId - 1;
                }

                sql_trans.CommandText = Class.QueryBuilder.UpdateTo("EventHdr",
                    new Dictionary<string, object>() { { "DocumentStatusId", previd } },
                    new Dictionary<string, object>() { { "EventID", eventID } });

                DATABASE.Dispose();

                #region SEND EMAIL

                var docStatus = MiscFunctions.getDocumentStatusMessage((int)Globals.InfoType.CalendarEvent, previd);
                var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, previd);
                string mail_body = AppHelper.Arms_Url + "?id=" + this.EventID + "&doctype=calendar&Year=" + this.Year + "&Month=" + this.Month;
                var subj = "ARMS Calendar Coverage Plan[" + docStatus.stateDesc + "]";
                foreach (var itm in emails)
                {
                    MiscFunctions.sendMail(itm.EmailAddress, subj, mail_body);
                    //MiscFunctions.sendEmail1(mail_body, subj, itm.EmailAddress);
                }

                #endregion
            }

           
            
            return 0;
        }


        #endregion





        #region PRIVATE PART GENERATE ID
        private string GetNewCode() 
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            var qry = from evnthdr in DATABASE.EventHdrs
                      where evnthdr.EventID.StartsWith("EVNT")
                      select new { evnthdr.EventID };

            List<int> list_of_id = new List<int>();
            foreach (var itm in qry)
            {
                list_of_id.Add(Convert.ToInt32(itm.EventID.Replace("EVNT", "")));
            
            }

            if (list_of_id.Count > 0)
            {
                int largest_id = list_of_id.Max() + 1;
                return "EVNT" + string.Format("{0:000000}", largest_id);

            }

            else 
            {

                return "EVNT000000";
            }

                 
        }



        private string GetLineNumCode()
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            var qry = (from eventhdr in DATABASE.EventDtls
                      where eventhdr.LineNum.StartsWith("SEQ")
                      select new { eventhdr.LineNum }).ToList();
           

            List<int> list_of_id = new List<int>();
            if(qry.Count >0)
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

#endregion


    }

    //DETAIL AREA
    public class collection : ARMS_W.Interface.Event.collection
    {
      public  string Objdesc { get; set; }
      public string AccountCode { get; set; }
      public string AccountName { get; set; }
      public string AccountAddress { get; set; }
      public string AccountClass { get; set; }
      public string ContactPerson { get; set; }
      public string ContactNumber { get; set; }
      public string ObjectiveCode { get; set; }
      public string Brand { get; set; }
      public string LineNum { get; set; }
      public string Amount { get; set; }
      public string EventId { get; set; }
      public int Day { get; set; }

    }

    public class Merchandise : ARMS_W.Interface.Event.Merchandise
    {
        public string Objdesc { get; set; }
        public string ObjectiveCode { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountAddress { get; set; }
        public string AccountClass { get; set; }
        public string ContactPerson { get; set; }

    }

    public class Sales : ARMS_W.Interface.Event.Sales
    {

        public string Objdesc { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountAddress { get; set; }
        public string AccountClass { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string ObjectiveCode { get; set; }
        public string Brand { get; set; }
        public string Amount { get; set; }

    }

    public class CustomerSrvc : ARMS_W.Interface.Event.CustomerSrvc
    {
        public string Objdesc { get; set; }
        public string ObjectiveCode { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountAddress { get; set; }
        public string AccountClass { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonNo { get; set; }

    }



}
