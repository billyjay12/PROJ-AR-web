using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace ARMS_W.Objects.Event
{
    public class CallReport : ARMS_W.Interface.Event.CallReport
    {
        public string cFullCollection { get; set; }
        public string cPartialCollection { get; set; }
        public string cNoCollection { get; set; }
        public string EventID { get; set; }
        public string AccountCode { get; set; }
        public string ObjectiveCode { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public int Day { get; set; }
        public string EventDesc { get; set; }
        public string StoreCheckingResult { get; set; }
        public string LineNum { get; set;}
        public string CompetitorActivities  {get;set;}
        public string WithOrder { get;set;}
        public string NextCalldate { get; set; }
        public string OtherInformation { get; set; }
        public string IssuesAndConcerns { get; set; }
        public string Delivery { get; set; }
        public string Orders { get; set; }
        public string SummaryLackingItems { get; set; }
        public string Remarks { get; set; }
        public string Recommendation { get; set; }
        public string TimeTable { get; set; }
        public string SoId { get; set; }
        
        public List<ARMS_W.Interface.Event.presented> Presented_list { get; set; }
        public List<ARMS_W.Interface.Event.forceObjective> forceObjective_list { get; set; }

        public CallReport()
        {

            this.Presented_list = new List<Interface.Event.presented>();
            this.forceObjective_list = new List<Interface.Event.forceObjective>();
        
        }



        public string UpdateToDB(ref ARMS_W.Class.SQLTransaction sql_trans)
        {

            #region IF COLLECTION AND UPDATED INDIVIDUALLY
            if (SkelClass.Globals.EventType.Collection.ToString() == EventDesc)
            {

                sql_trans.CommandText = Class.QueryBuilder.UpdateTo("EventDtls",
                     new Dictionary<string, object>(){
                                                    {"cFullCollection",cFullCollection},
                                                    {"cPartialCollection",cPartialCollection},
                                                    {"cNoCollection",cNoCollection}
                                                },

                     new Dictionary<string, object>(){
                                                    {"EventID",EventID},
                                                    {"AccountCode",AccountCode},
                                                    {"Day",Day},
                                                    {"ObjectiveCode",ObjectiveCode}
                                                });
            }

            #endregion

            #region IF Merchandising AND UPDATED INDIVIDUALLY
            if (SkelClass.Globals.EventType.Merchandising.ToString() == EventDesc)
            {

                sql_trans.CommandText = Class.QueryBuilder.UpdateTo("EventDtls",
                    new Dictionary<string, object>(){
                                                    {"StoreCheckingResult",StoreCheckingResult}
                                                   },
                    new Dictionary<string, object> {
                                                    {"EventID",EventID},
                                                    {"AccountCode",AccountCode},
                                                    {"Day",Day},
                                                    {"ObjectiveCode",ObjectiveCode}
                                                    });


                foreach (var presented in Presented_list)
                {
                    sql_trans.InsertTo("EventDtl1", new Dictionary<string, object>() {
                    
                                                    {"EventID",EventID},                                                    
                                                    {"LineNum",this.LineNum},
                                                    {"Day",Day},
                                                    {"AccountCode",AccountCode},
                                                    {"ObjectiveCode",ObjectiveCode},
                                                    {"Brand",presented.Brand},
                                                    {"CounterClerk", presented.CounterClerk}
                    
                    });
                
                }

            }

            #endregion

            #region IF SALES AND UPDATED INDIVIDUALLY

            if (SkelClass.Globals.EventType.Sales.ToString() == EventDesc)
            {
                sql_trans.CommandText = Class.QueryBuilder.UpdateTo("EventDtls",
                     new Dictionary<string, object>(){
                                                    {"WithOrder",WithOrder},
                                                    {"NextCalldate", NextCalldate},
                                                    {"OtherInformation", OtherInformation},
                                                    {"CompetitorActivities",CompetitorActivities}
                                                   },
                     new Dictionary<string, object> {
                                                    {"EventID",EventID},
                                                    {"AccountCode",AccountCode},
                                                    {"Day",Day},
                                                    {"ObjectiveCode",ObjectiveCode}
                                                    });

            }

            #endregion

            #region IF CUSTOMER SERVICE  AND UPDATED INDIVIDUALLY

            if (SkelClass.Globals.EventType.CustomerService.ToString()== EventDesc)
            {

                sql_trans.CommandText = Class.QueryBuilder.UpdateTo("EventDtls",
                    new Dictionary<string, object>(){
                                                    {"IssuesAndConcerns",IssuesAndConcerns},
                                                    {"Delivery", Delivery},
                                                    {"Orders", Orders},
                                                    {"SummaryLackingItems",SummaryLackingItems},
                                                    {"Remarks", Remarks},
                                                    {"Recommendation", Recommendation},
                                                    {"TimeTable",TimeTable}

                                                   },
                    new Dictionary<string, object> {
                                                    {"EventID",EventID},
                                                    {"AccountCode",AccountCode},
                                                    {"Day",Day},
                                                    {"ObjectiveCode",ObjectiveCode}
                                                    });


            }
            #endregion



            return this.EventID;   
        }
        public string UpdateToDB1(ref ARMS_W.Class.SQLTransaction sql_trans)
        {
           //Model Connector
             ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
             //Caputer data from list to be distinct by AccountCode
            var list1 = new List<Interface.Event.forceObjective>();
            list1 = forceObjective_list.GroupBy(grp => grp.AccountCode).Select(grp1 => grp1.First()).ToList();
            //Line Number Initializer
            Int32 beggining_linenum = Convert.ToInt32(this.GetLineNumCode().Replace("SEQ", ""));
            //LineNumber Pattern Generator

            foreach (forceObjective f_collection in list1)
            {
                //temp line number if Cardcode Exist in DATABASE
                string tmp_linenum = null;
                //variable counter if its exist
                int isExistinDtls = 0;

                try {

                    isExistinDtls = DATABASE.EventDtls.Count(p => p.AccountCode == f_collection.AccountCode
                                    && p.Day == this.Day && p.ObjectiveCode == f_collection.ObjectiveCode && p.EventID == this.EventID);
                    tmp_linenum = DATABASE.EventDtls.Single(p => p.AccountCode == f_collection.AccountCode
                                    && p.Day == this.Day && p.ObjectiveCode == f_collection.ObjectiveCode && p.EventID == this.EventID).LineNum;
                }
                catch(Exception ex) { }

                if (isExistinDtls > 0)
                {
                    foreach (forceObjective test_collection in forceObjective_list.Where(p => p.AccountCode == f_collection.AccountCode))
                    {
                        var qry_count = (from dtl1 in DATABASE.EventDtl1
                                         where dtl1.LineNum == tmp_linenum && dtl1.AccountCode == test_collection.AccountCode
                                         && dtl1.Brand == test_collection.Brand
                                         select new { }).ToList();

                        if (qry_count.Count == 0)
                            sql_trans.InsertTo("EventDtl1", new Dictionary<string, object>() {
                            {"EventID",this.EventID},
                            {"LineNum",tmp_linenum},
                            {"Day",this.Day},
                            {"AccountCode",test_collection.AccountCode},
                            {"ObjectiveCode",test_collection.ObjectiveCode},
                            {"Brand",test_collection.Brand},
                            {"Amount",test_collection.Amount}
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
                        {"AccountCode",f_collection.AccountCode},
                        {"AccountName",f_collection.AccountName},
                        {"AccountAddress", f_collection.AccountAddress},
                        {"AccountClass",f_collection.AccountClass},
                        {"ObjectiveCode",f_collection.ObjectiveCode},
                        {"ContactPerson",f_collection.ContactPerson},
                        {"ContactPersonNo",f_collection.ContactNumber},
                        {"cFullCollection",f_collection.cFullCollection},
                        {"cPartialCollection",f_collection.cPartialCollection},
                        {"cNoCollection",f_collection.cNoCollection}


                    
                    });

                    foreach (forceObjective test_collection in forceObjective_list.Where(p => p.AccountCode == f_collection.AccountCode))
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




            return this.EventID;
        }


        private string GetLineNumCode()
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            var qry = (from eventhdr in DATABASE.EventDtls
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



    }

    public class presented : ARMS_W.Interface.Event.presented
    {
        public string Brand { get; set; }
        public string CounterClerk { get; set; }

    }


    public class forceObjective : ARMS_W.Interface.Event.forceObjective
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
        public string cFullCollection { get; set; }
        public string cPartialCollection { get; set; }
        public string cNoCollection { get; set; }
        public string Remarks { get; set; }
    }

}