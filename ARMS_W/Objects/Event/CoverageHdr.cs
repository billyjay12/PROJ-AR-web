using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using ARMS_W.Class;
using ARMS_W.SkelClass;

namespace ARMS_W.Objects.Event
{
    public class CoverageHdr: ARMS_W.Interface.Event.CoverageHdr
    {
        public string EventID { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public int Day { get; set; }
        public string EmpIdNo { get; set; }
        public int DoctypeId { get; set; }
        public int DocumentStatusId { get; set; }
        public string action_type { get; set; }
        public string LineNum { get; set; }
        public string AccountCode { get; set; }
        public string ObjectiveCode { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonNo { get; set; }
        public bool isExist { get; set; }
        public string StoreChecking { get; set; }
        public string IssuesAndConcerns { get; set; }
        public string cFullCollection { get; set; }
        public string cPartialCollection { get; set; }
        public string cNoCollection { get; set; }
        public string StoreCheckingResult { get; set; }
        public string Delivery { get; set; }
        public string Orders { get; set; }
        public string SummaryLackingItems { get; set; }
        public string Recommendation { get; set; }
        public string TimeTable { get; set; }
        public string Remarks { get; set; }
        public string CompetitorActivities { get; set; }
        public string WithOrder { get; set; }
        public string NextCallDate { get; set; }
        public string OtherInformation { get; set; }
        public int qryExist { get; set; }
        public string isPlanned{get;set;}
        public DateTime Tmein { get; set; }
        public DateTime Tmeout { get; set; }
        public int Numvisit { get; set; }
        public string HotelName { get; set; }
        public string HotelContactNum { get; set; }
        public int AcctStatus { get; set; }
        public string isDeleted { get; set; }
        public string Attachment { get; set; }
        public string RmrkChanges { get; set; }
        public string IsAnEdit { get; set; }
        public string hasCallreport { get; set; }
        public string ProductPresentationResult { get; set; }
        public string ApprvrRrmks { get; set; }
        public decimal? ColPostDatedCheck { get; set; }
        public decimal? ColDatedCheck { get; set; }
        public decimal? ColTotal { get; set; }
        public string ColRemarks { get; set; }


        public List<ARMS_W.Interface.Event.collections> collection_list { get; set; }
        public List<ARMS_W.Interface.Event.merchandising> merchandising_list { get; set; }
        public List<ARMS_W.Interface.Event.sales> sales_list { get; set; }
        public List<ARMS_W.Interface.Event.customersrvc> customersrv_list { get; set; }
        public List<ARMS_W.Interface.Event.collections> uncollection_list { get; set; }
        public List<ARMS_W.Interface.Event.sales> unsales_list { get; set; }
        public List<ARMS_W.Interface.Event.changesdtls> Acct_dtls { get; set; }
        public List<ARMS_W.Interface.Event.merchandising> unmerchandising_list { get; set; }
        public List<ARMS_W.Interface.Event.nextInventory> nextinventory { get; set; }


        public CoverageHdr()
        {
            this.collection_list = new List<Interface.Event.collections>();
            this.merchandising_list = new List<Interface.Event.merchandising>();
            this.sales_list = new List<Interface.Event.sales>();
            this.customersrv_list = new List<Interface.Event.customersrvc>();
            this.Acct_dtls = new List<Interface.Event.changesdtls>();
            this.unmerchandising_list = new List<Interface.Event.merchandising>();
            this.nextinventory = new List<Interface.Event.nextInventory>();

         
           
            //This methods are intended for unplanned collection of specific Brand 
            //which was not included in the coverage plan

            this.uncollection_list = new List<Interface.Event.collections>();
            this.unsales_list = new List<Interface.Event.sales>();
           

        }


        public string UpdateToDBCoverage(ref ARMS_W.Class.SQLTransaction sql_trans)
        {

            string obj_code = null;
            string tmp_linenum = null;
            int isExistindtls = 0;
            Int32 beggining_linenum = Convert.ToInt32(this.GetLineNumCode().Replace("SEQ", ""));

            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

          

            if (isExist != true)
            {
                string tmp_id = this.EventID == null ? this.GetNewCode() : this.EventID;
                this.EventID = tmp_id;
            
            //}

            //if (isExist != true)
            //{
                sql_trans.InsertTo("CoverageHdr", new Dictionary<string, object>() {
                {"EventID",this.EventID},
                {"EmpIdNo",this.EmpIdNo},
                {"Year",this.Year},
                {"Month",this.Month},
                {"DoctypeId",this.DoctypeId},
                {"DocumentStatusId",this.DocumentStatusId}
                
                });
            
            
            }

            try { 
                isExistindtls = DATABASE.CoverageDtls.Count(p => p.AccountCode == this.AccountCode && p.EventID == this.EventID && p.Day == this.Day);
                tmp_linenum = DATABASE.CoverageDtls.Single(p => p.AccountCode == this.AccountCode && p.Day == this.Day && p.EventID==this.EventID).LineNum;
            }
            catch (Exception ex)
            { }


            if (isExistindtls > 0)
            {

               sql_trans.DeleteFrom("CoverageDtl1", new Dictionary<string, object>() { 
                                    {"EventID", this.EventID},
                                    {"LineNum", tmp_linenum},
                                    {"Day", this.Day}
                
                });  


                if (this.collection_list != null && this.collection_list.Count != 0)
                {
                    foreach (collections p_collections in collection_list)
                    {
                        sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", tmp_linenum},
                        {"Day",this.Day},
                        {"ObjectiveCode",p_collections.ObjectiveCode},
                        {"Brand", p_collections.Brand},
                        {"PlannedAmount",p_collections.Amount},
                        {"isIncoverage","T"},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/

                        });


                    }

                }

                if (this.StoreChecking != "")
                {
                    if (this.merchandising_list != null && this.merchandising_list.Count != 0)
                    {
                        foreach (merchandising p_merchandising in merchandising_list)
                        {

                            sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", tmp_linenum},
                        {"Day",this.Day},
                        {"ObjectiveCode",p_merchandising.ObjectiveCode},
                        {"CounterClerk", p_merchandising.counterclerk},
                        {"CounterClerkNo",p_merchandising.CounterClerkNo},
                        {"ProductPresented",p_merchandising.Productpresented}

                       
                        
                        
                        });
                        }
                    
                    }
                
                }

                if (this.sales_list != null && this.sales_list.Count!=0)
                {
                    foreach (sales p_sales in sales_list)
                    {

                        sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", tmp_linenum},
                        {"Day",this.Day},
                        {"ObjectiveCode",p_sales.ObjectiveCode},
                        {"Brand", p_sales.Brand},
                        {"PlannedAmount",p_sales.Amount},
                        {"isIncoverage","T"},
                        {"detailRemarks",p_sales.dtlsRrmks},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/
                         });

                    
                    }
                
                }


                if (this.IssuesAndConcerns != "")
                {
                    if (this.customersrv_list != null && this.customersrv_list.Count != 0)
                    {
                        foreach (customersrvc p_sales in customersrv_list)
                        {

                            sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", tmp_linenum},
                        {"Day",this.Day},
                        {"ObjectiveCode",p_sales.ObjectiveCode},
                        {"isIncoverage","T"},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/

                       
                        
                        
                        });
                        }

                    }

                }


            }


            else 
            {
                this.LineNum = string.Format("{0:000000}", "SEQ" + string.Format("{0:000000}", beggining_linenum));

                sql_trans.InsertTo("CoverageDtls", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"AccountCode",this.AccountCode},
                        {"ContactPerson",this.ContactPerson},
                        {"ContactPersonNo", this.ContactPersonNo},
                        {"StoreChecking", this.StoreChecking},
                        {"IssuesAndConcerns",this.IssuesAndConcerns},
                        {"isPlanned","T"},
                        {"IsDeleted","F"},
                        {"HotelName",this.HotelName},
                        {"HotelContactNumber",this.HotelContactNum},
                        {"AcctStatus", this.AcctStatus},
                        {"IsAnEdit", "F"}, /** this is an indicator whether the account added is original from coverage or an edited one*/
                        {"FileAttachment", this.Attachment},
                        {"hasCallreport", "F"},
                        {"ColPostDatedCheck",this.ColPostDatedCheck},
                        {"ColDatedCheck",this.ColDatedCheck},
                        {"ColTotal",this.ColTotal},
                        {"ColRemarks",this.ColRemarks},
                        {"DateEncoded", DateTime.Now }
                        });

                foreach (collections test_collection in collection_list)
                {
                    sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_collection.ObjectiveCode},
                        {"Brand", test_collection.Brand},
                        {"PlannedAmount",test_collection.Amount},
                        {"isIncoverage","T"},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/
                   });
                }
                //if statement here check if there are task under merchandising
                //if (this.StoreChecking != null && this.StoreChecking != "")
                //{
                    foreach (merchandising test_merchandising in merchandising_list)
                    {
                        sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_merchandising.ObjectiveCode},
                         {"CounterClerk", test_merchandising.counterclerk},
                        {"CounterClerkNo",test_merchandising.CounterClerkNo},
                        {"ProductPresented",test_merchandising.Productpresented},
                        {"isIncoverage","T"},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/

                        });
                    
                    }
                
                //}

                foreach (sales test_sales in sales_list)
                {
                    sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_sales.ObjectiveCode},
                        {"Brand", test_sales.Brand},
                        {"PlannedAmount",test_sales.Amount},
                        {"isIncoverage","T"},
                        {"detailRemarks",test_sales.dtlsRrmks},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/
                   });
                }

                //if statement here check if there are task under merchandising
                if (this.IssuesAndConcerns != null && this.IssuesAndConcerns != "")
                {
                    foreach (customersrvc test_custsrv in customersrv_list)
                    {
                        sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_custsrv.ObjectiveCode}

                        });

                    }

                }


                beggining_linenum++;
            }

        

           


            return this.EventID;
        }

        public string UpdateToDBCoverageChanges(ref ARMS_W.Class.SQLTransaction sql_trans,string username)
        {    
            string obj_code = null;
            string tmp_linenum = null;
            int isExistindtls = 0;
            Int32 beggining_linenum = Convert.ToInt32(this.GetLineNumCode().Replace("SEQ", ""));

            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            int role = getRole(username);


            try
            {
                isExistindtls = DATABASE.CoverageDtls.Count(p => p.AccountCode == this.AccountCode && p.EventID == this.EventID && p.Day == this.Day);
                tmp_linenum = DATABASE.CoverageDtls.Single(p => p.EventID == this.EventID && p.AccountCode == this.AccountCode && p.Day == this.Day).LineNum;
            }
            catch (Exception ex)
            { }


            if (isExistindtls > 0)
            {

                if (isDeleted == "T")
                {
                    sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls", new Dictionary<string, object>() {
                                                   {"isDeleted", this.isDeleted},
                                                   {"DateDeleted",DateTime.Now}
                
                },
                                                       new Dictionary<string, object>() {
                                                    {"EventID", this.EventID},
                                                    {"LineNum", tmp_linenum},
                                                    {"Day",this.Day},
                                                    {"AccountCode",this.AccountCode},
                                                   
                                                   });
                }

                /**new inserted sept 1**/
                else {

                    sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls", new Dictionary<string, object>() {
                                                   {"isDeleted", "F"},
                                                   {"StoreChecking", this.StoreChecking},
                                                   {"IssuesAndConcerns", this.IssuesAndConcerns}
                
                },
                                                   new Dictionary<string, object>() {
                                                    {"EventID", this.EventID},
                                                    {"LineNum", tmp_linenum},
                                                    {"Day",this.Day},
                                                    {"AccountCode",this.AccountCode},
                                                   
                                                   });


                    foreach (collections test_collection in collection_list)
                    {
                        sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", tmp_linenum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_collection.ObjectiveCode},
                        {"Brand", test_collection.Brand},
                        {"PlannedAmount",test_collection.Amount},
                        {"isIncoverage","T"},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/
                   });
                    }
                    //if statement here check if there are task under merchandising
                    // if (this.StoreChecking != null && this.StoreChecking != "")
                    //{
                    foreach (merchandising test_merchandising in merchandising_list)
                    {
                        sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", tmp_linenum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_merchandising.ObjectiveCode},
                        {"isIncoverage","T"},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/
                        {"CounterClerk",test_merchandising.counterclerk},
                        {"CounterClerkNo",test_merchandising.CounterClerkNo},
                        {"Remarks", test_merchandising.Remarks},
                        {"ProductPresented", test_merchandising.Productpresented}

                        });

                        // }

                    }

                    foreach (sales test_sales in sales_list)
                    {
                        sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", tmp_linenum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_sales.ObjectiveCode},
                        {"Brand", test_sales.Brand},
                        {"PlannedAmount",test_sales.Amount},
                        {"isIncoverage","T"},
                        {"detailRemarks",test_sales.dtlsRrmks},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/
                   });
                    }

                
                
                }


                /**end new inserted**/



            }


            else
            {

              /**  sql_trans.UpdateTo("CoverageHdr", new Dictionary<string, object>(){
                {"DoctypeId",16},
                {"DocumentStatusId",1}
                },
                new Dictionary<string, object>() {
                    {"EventID", this.EventID}
                
                });  **/


                //if ROLE = SO then 6 [Amended(for ASM Approval)] else 10[Amended(For Channel Manager Approval)]
               // int documentstatusid = role == 8 ? 8 : 16;


                string isFinal = "";

                int documentstatusid  = (new List<int>() { 17, 2 }).Contains(role) ? 16 : 19;
                //var curr_doc_stat = (from approvalState in DATABASE.approvalStates
                //                     where approvalState.docType == 14
                //                     && approvalState.roleID == role
                //                     && approvalState.stateID == this.DocumentStatusId

                var curr_doc_stat = DATABASE.approvalStates.Where(o => o.docType == 14 && o.roleID == role && o.stateID == this.DocumentStatusId);


                foreach (var itm in curr_doc_stat)
                {
                    isFinal = itm.isFinal;
                }

                //if (role==17)
          //      {
                    //if (this.DocumentStatusId != 16)
              //  if (isFinal == "Y")
                if (this.DocumentStatusId != 16 || this.DocumentStatusId != 19)
                {
                    string area_ = string.Empty;
                    string channel_ = string.Empty;
                    var qry_user = DATABASE.userHeaders.Where(o => o.userName == username).Single().apprvrDesigs.Where(o => o.roleID == role).First();
                    channel_ = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.channel == o.channel).First().channel;
                    if (action_type == Globals.DocActionType.SaveAndSend.ToString())
                    {
                       // channel_ = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.channel == o.channel).First().channel;
                     //   forChannelManager = true;
                    }

                    // var qry_user = DATABASE.userHeaders.Where(o => o.userName == username).Single().apprvrDesigs.Where(o => o.roleID == role).First();
                    List<MiscFunctions.GetEmailAddressesTMP> emails = new List<MiscFunctions.GetEmailAddressesTMP>();

                    emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, documentstatusid, area_,true , channel_);
                   // emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, documentstatusid, area_, (documentstatusid == 13 || documentstatusid == 16) ? true : false, channel_);
                    //emails = MiscFunctions.GetEmailAddresses_New(documentstatusid, role, qry_user.userHeader.empIdNo);

                    string mail_body = AppHelper.Arms_Url + "?id=" + this.EventID + "&doctype=clndr&Year=" + this.Year + "&Month=" + this.Month + "&soId=" + this.EmpIdNo;

                    var subj = "ARMS Calendar Coverage Plan[" + DATABASE.approvalStates.Single(o => o.docType == (int)Globals.InfoType.CalendarEvent && o.roleID == role && o.stateID == documentstatusid).stateDesc + "]";

                    foreach (var itm in emails)
                    {
                        //MiscFunctions.sendMail(itm.EmailAddress, subj, mail_body);
                        // MiscFunctions.sendEmail1(mail_body, subj, itm.EmailAddress);

                        sql_trans.InsertTo("SplEmails",
                                        new Dictionary<string, object>() { 
                            {"sFrom","ARMS@matimco.com" }
                            ,{"sTo",itm.EmailAddress}
                            ,{"sCC",null}
                            ,{"sSubject",subj }
                            ,{"sMessage",mail_body}
                            });
                    }
                }
              //  }

                sql_trans.UpdateTo("CoverageHdr", new Dictionary<string, object>() { { "DocumentStatusId", documentstatusid } },
                new Dictionary<string, object>() { { "EventID", this.EventID } });


                SaveRouteChanges(sql_trans, Globals.DocActionType.Update, username, documentstatusid, this.DocumentStatusId, this.EventID,"",role);


                this.LineNum = string.Format("{0:000000}", "SEQ" + string.Format("{0:000000}", beggining_linenum));

                sql_trans.InsertTo("CoverageDtls", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"AccountCode",this.AccountCode},
                        {"ContactPerson",this.ContactPerson},
                        {"ContactPersonNo", this.ContactPersonNo},
                        {"StoreChecking", this.StoreChecking},
                        {"IssuesAndConcerns",this.IssuesAndConcerns},
                        {"isPlanned","T"},
                        {"HotelName",this.HotelName},
                        {"HotelContactNumber",this.HotelContactNum},
                        {"AcctStatus", documentstatusid},
                        {"IsAnEdit", "T"},
                        {"IsDeleted", "F"},
                        {"hasCallreport","F"},
                        {"ColPostDatedCheck",this.ColPostDatedCheck},
                        {"ColDatedCheck",this.ColDatedCheck},
                        {"ColTotal",this.ColTotal},
                        {"ColRemarks",this.ColRemarks},
                        {"DateEncoded", DateTime.Now }
                        });

                foreach (collections test_collection in collection_list)
                {
                    sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_collection.ObjectiveCode},
                        {"Brand", test_collection.Brand},
                        {"PlannedAmount",test_collection.Amount},
                        {"isIncoverage","T"},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/
                   });
                }
                //if statement here check if there are task under merchandising
               // if (this.StoreChecking != null && this.StoreChecking != "")
                //{
                    foreach (merchandising test_merchandising in merchandising_list)
                    {
                        sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_merchandising.ObjectiveCode},
                        {"isIncoverage","T"},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/
                        {"CounterClerk",test_merchandising.counterclerk},
                        {"CounterClerkNo",test_merchandising.CounterClerkNo},
                        {"Remarks", test_merchandising.Remarks}

                        });

                   // }

                }

                foreach (sales test_sales in sales_list)
                {
                    sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_sales.ObjectiveCode},
                        {"Brand", test_sales.Brand},
                        {"PlannedAmount",test_sales.Amount},
                        {"isIncoverage","T"},
                        {"detailRemarks",test_sales.dtlsRrmks},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/
                   });
                }

                //if statement here check if there are task under merchandising
                if (this.IssuesAndConcerns != null && this.IssuesAndConcerns != "")
                {
                    foreach (customersrvc test_custsrv in customersrv_list)
                    {
                        sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_custsrv.ObjectiveCode}

                        });

                    }

                }


                beggining_linenum++;
            }






            return this.EventID;
        }

        public string UpdateDeletion(ref ARMS_W.Class.SQLTransaction sql_trans)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            sql_trans.CommandText = Class.QueryBuilder.DeleteFrom("CoverageDtl1",
                new Dictionary<string, object>() {
                                                 {"EventID", this.EventID},
                                                 {"LineNum", this.LineNum},
                                                 {"Day",this.Day}
                
                });
            sql_trans.CommandText = Class.QueryBuilder.DeleteFrom("CoverageDtls",
                new Dictionary<string, object>() {
                                                 {"EventID", this.EventID},
                                                 {"LineNum", this.LineNum},
                                                 {"Day",this.Day},
                                                 {"AccountCode", this.AccountCode}

                
                });

            return this.EventID;
        }

        public string UpdateDBCallReport(ref ARMS_W.Class.SQLTransaction sql_trans)
        {
            var unplannedCollection = new List<Interface.Event.collection>();
            Int32 beggining_linenum = Convert.ToInt32(this.GetLineNumCode().Replace("SEQ", ""));


            //qryExist is an identifier if the account code is 
            //already exist in the db for that particular day, year and month
            if (qryExist > 0)
            {
                sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls",
                    new Dictionary<string, object>() { 
                                                    {"ContactPerson",ContactPerson},
                                                    {"ContactPersonNo",ContactPersonNo},
                                                    {"IssuesAndConcerns",IssuesAndConcerns},
                                                    {"StoreChecking",StoreChecking},
                                                    {"cFullCollection",cFullCollection},
                                                    {"cPartialCollection",cPartialCollection},
                                                    {"cNoCollection",cNoCollection},
                                                    {"StoreCheckingResult",StoreCheckingResult},
                                                    {"Delivery", Delivery},
                                                    {"Orders", Orders},
                                                    {"SummaryLackingItems",SummaryLackingItems},
                                                    {"Recommendation",Recommendation},
                                                    {"TimeTable",TimeTable},
                                                    {"Remarks",Remarks},
                                                    {"CompetitorActivities",CompetitorActivities},
                                                    {"WithOrder",WithOrder},
                                                    {"NextCallDate",NextCallDate},
                                                    {"OtherInformation",OtherInformation},
                                                    //{"Tmein",Tmein},
                                                    //{"Tmeout",Tmeout},
                                                    {"Numvisit",Numvisit},
                                                    {"FileAttachment", this.Attachment},
                                                    {"hasCallreport", "T"},
                                                    {"IsDeleted","F"},
                                                    {"ColPostDatedCheck",this.ColPostDatedCheck},
                                                    {"ColDatedCheck",this.ColDatedCheck},
                                                    {"ColTotal",this.ColTotal},
                                                    {"ColRemarks",this.ColRemarks}


                },
                    new Dictionary<string, object>() { 

                                                    {"EventID",EventID},
                                                    {"AccountCode",AccountCode},
                                                    {"Day",Day}
                                                 

                
                });

                #region COLLECTION

                if (this.collection_list != null)
                {
                    foreach (var itm in collection_list)
                    {
                        sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtl1",
                            new Dictionary<string, object>() { 
                        
                             {"ActualAmount",itm.ActualAmount},
                             {"Remarks",itm.Remarks}
                        },
                            new Dictionary<string, object>() { 
                        
                                                    {"EventID",this.EventID},
                                                    {"LineNum",this.LineNum},
                                                    {"ObjectiveCode",itm.ObjectiveCode},
                                                    {"Brand",itm.Brand},
                                                    {"PlannedAmount",itm.Amount}
                        
                        });

                    }

                }
                #endregion

                #region UNPLANNED COLLECTION

                if (this.uncollection_list != null)
                {
                    foreach (var itm in uncollection_list)
                    {
                        sql_trans.CommandText = Class.QueryBuilder.InsertTo("CoverageDtl1",
                            new Dictionary<string, object>() { 

                                                    {"EventID",this.EventID},
                                                    {"LineNum",this.LineNum},
                                                    {"Day",this.Day},
                                                    {"ObjectiveCode",itm.ObjectiveCode},
                                                    {"Brand",itm.Brand},
                                                    {"PlannedAmount",itm.Amount},
                                                    {"ActualAmount",itm.ActualAmount},
                                                    {"Remarks",itm.Remarks},
                                                    {"isIncoverage","F"},

                        
                        
                        
                        });

                    }

                }
                #endregion

                #region Merchandising List

                if (this.merchandising_list != null)
                {
                    foreach (var itm in merchandising_list)
                    {

                        sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtl1",
                            new Dictionary<string, object>() { 
                        
                             {"Remarks",itm.Remarks}
                        },
                            new Dictionary<string, object>() { 
                        
                                                    {"EventID",this.EventID},
                                                    {"LineNum",this.LineNum},
                                                    {"ObjectiveCode",itm.ObjectiveCode},
                                                    {"ProductPresented",itm.Productpresented}
                                                   
                        
                        });
                    
                    }

                }

                #endregion

                #region Unplanned Merchandising List
                if (this.unmerchandising_list != null)
                {
                    foreach (var itm in unmerchandising_list)
                    {
                        sql_trans.CommandText = Class.QueryBuilder.InsertTo("CoverageDtl1",
                            new Dictionary<string, object>() { 

                                                    {"EventID",this.EventID},
                                                    {"LineNum",this.LineNum},
                                                    {"Day",this.Day},
                                                    {"ObjectiveCode",itm.ObjectiveCode},
                                                    {"ProductPresented",itm.Productpresented},
                                                    {"CounterClerk",itm.counterclerk},
                                                    {"CounterClerkNo",itm.CounterClerkNo},
                                                    {"Remarks",itm.Remarks},
                                                    {"isIncoverage","F"},

                        
                        
                        
                        });

                    }

                }

                #endregion

                #region TO BE REMOVE
                //if (this.merchandising_list != null)
                //{
                //    // var code = merchandising_list.Single(p => p.ObjectiveCode == p.ObjectiveCode).ObjectiveCode;
                //    sql_trans.CommandText = Class.QueryBuilder.DeleteFrom("CoverageDtl1",
                //           new Dictionary<string, object>()
                //       {
                //            {"EventID", this.EventID},
                //            {"LineNum", this.LineNum},
                //            {"ObjectiveCode","M"}  /**hard coded M to delete merchandise **/

                //       });

                //    foreach (var itm in merchandising_list)
                //    {

                //        sql_trans.CommandText = Class.QueryBuilder.InsertTo("CoverageDtl1",
                //            new Dictionary<string, object>() {
                        
                //                                    {"EventID",this.EventID},
                //                                    {"LineNum",this.LineNum},
                //                                    {"Day",this.Day},
                //                                    {"ObjectiveCode",itm.ObjectiveCode},
                //                                    {"ProductPresented",itm.Productpresented},
                //                                    {"CounterClerk",itm.counterclerk},
                //                                    {"CounterClerkNo",itm.CounterClerkNo},
                //                                    {"isIncoverage","T"},
                //                                    {"Remarks",itm.Remarks}
                        
                //        });

                //    }

                //}

                #endregion

                #region SALES

                //SALES HERE

                if (this.sales_list != null)
                {
                    foreach (var itm in sales_list)
                    {
                        sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtl1",
                            new Dictionary<string, object>() { 
                        
                             {"ActualAmount",itm.ActualAmount},
                             {"Remarks",itm.Remarks}
                        },
                            new Dictionary<string, object>() { 
                        
                                                    {"EventID",this.EventID},
                                                    {"LineNum",this.LineNum},
                                                    {"ObjectiveCode",itm.ObjectiveCode},
                                                    {"Brand",itm.Brand},
                                                    {"PlannedAmount",itm.Amount}
                        
                        });

                    }

                }

                #endregion

                #region UNPLANNED SALES
                if (this.unsales_list != null)
                {
                    foreach (var itm in unsales_list)
                    {
                        sql_trans.CommandText = Class.QueryBuilder.InsertTo("CoverageDtl1",
                            new Dictionary<string, object>() { 

                                                    {"EventID",this.EventID},
                                                    {"LineNum",this.LineNum},
                                                    {"Day",this.Day},
                                                    {"ObjectiveCode",itm.ObjectiveCode},
                                                    {"Brand",itm.Brand},
                                                    {"PlannedAmount",itm.Amount},
                                                    {"ActualAmount",itm.ActualAmount},
                                                    {"Remarks",itm.Remarks},
                                                    {"isIncoverage","F"},
                                                    {"detailRemarks", itm.dtlsRrmks},

                        
                        
                        
                        });

                    }

                }
            }
                #endregion

            //add objective and call report at the same time .. in call report tab
            //unexpected call report

            #region UNEXPECTED CALL REPORT

            else {


                this.LineNum = string.Format("{0:000000}", "SEQ" + string.Format("{0:000000}", beggining_linenum));

                sql_trans.InsertTo("CoverageDtls", new Dictionary<string, object>() {
                                {"EventID", this.EventID},
                                {"LineNum", this.LineNum},
                                {"Day",this.Day},
                                {"AccountCode",this.AccountCode},
                                {"ContactPerson",this.ContactPerson},
                                {"ContactPersonNo", this.ContactPersonNo},
                                {"StoreChecking", this.StoreChecking},
                                {"IssuesAndConcerns",this.IssuesAndConcerns},
                                {"cFullCollection",this.cFullCollection},
                                {"cPartialCollection",this.cPartialCollection},
                                {"cNoCollection",this.cNoCollection},
                                {"StoreCheckingResult",this.StoreCheckingResult},
                                {"Delivery", this.Delivery},
                                {"Orders", this.Orders},
                                {"SummaryLackingItems",this.SummaryLackingItems},
                                {"Recommendation",this.Recommendation},
                                {"TimeTable",this.TimeTable},
                                {"Remarks",this.Remarks},
                                {"CompetitorActivities",this.CompetitorActivities},
                                {"WithOrder",this.WithOrder},
                                {"NextCallDate",this.NextCallDate},
                                {"OtherInformation",this.OtherInformation},
                                {"isPlanned","F"},
                                //{"Tmein",Tmein},
                                //{"Tmeout",Tmeout},
                                {"Numvisit",Numvisit},
                                {"HotelName",this.HotelName},
                                {"HotelContactNumber", this.HotelContactNum},
                                {"AcctStatus",this.AcctStatus},
                                {"FileAttachment", this.Attachment},
                                {"hasCallreport", "T"},
                                {"IsAnEdit","F"},
                                {"isDeleted","F"},
                                {"ColPostDatedCheck",this.ColPostDatedCheck},
                                {"ColDatedCheck",this.ColDatedCheck},
                                {"ColTotal",this.ColTotal},
                                {"ColRemarks",this.ColRemarks}
                        });
                #region COLLECTION

                foreach (collections test_collection in uncollection_list)
                {
                    sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_collection.ObjectiveCode},
                        {"Brand", test_collection.Brand},
                        {"PlannedAmount","0"},
                        {"ActualAmount",test_collection.ActualAmount},
                        {"isIncoverage","F"},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/
                   });
                }

                #endregion

                #region Merchandising

                //if statement here check if there are task under merchandising
               // if (this.StoreChecking != null && this.StoreChecking != "")
               // {
                    foreach (merchandising test_merchandising in unmerchandising_list)
                    {
                        sql_trans.CommandText = Class.QueryBuilder.InsertTo("CoverageDtl1",
                            new Dictionary<string, object>() {
                        
                                                    {"EventID",this.EventID},
                                                    {"LineNum",this.LineNum},
                                                    {"Day",this.Day},
                                                    {"ObjectiveCode",test_merchandising.ObjectiveCode},
                                                    {"ProductPresented",test_merchandising.Productpresented},
                                                    {"CounterClerk",test_merchandising.counterclerk},
                                                    {"CounterClerkNo",test_merchandising.CounterClerkNo},
                                                    {"isIncoverage","F"}
                        
                        });

                    }

               // }

                #endregion

                #region SALES

                foreach (sales test_sales in unsales_list)
                {
                    sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_sales.ObjectiveCode},
                        {"Brand", test_sales.Brand},
                        {"PlannedAmount",test_sales.Amount},
                        {"isIncoverage","F"},
                        {"detailRemarks",test_sales.dtlsRrmks},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/
                   });
                }

                #endregion

                #region CUSTOMER SERVICE

                //if statement here check if there are task under merchandising
                if (this.IssuesAndConcerns != null && this.IssuesAndConcerns != "")
                {
                    foreach (customersrvc test_custsrv in customersrv_list)
                    {
                        sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>() {
                        {"EventID", this.EventID},
                        {"LineNum", this.LineNum},
                        {"Day",this.Day},
                        {"ObjectiveCode",test_custsrv.ObjectiveCode},
                        {"isIncoverage","F"},/**an indicator if its part of the coverage. T:if it's,F:if it's not**/


                        });

                    }

                }
                #endregion



                beggining_linenum++;

            }

            #endregion

            return EventID;
        }

        public string UpdateToDBNextInventoryCount(ref ARMS_W.Class.SQLTransaction sql_trans)
        {
            foreach (var itm in this.nextinventory)
            { 
            
            
            }
            return EventID;
        }

        //this function is a work a round if there is an added account

        #region Update Status
        public int UpdateDocStatus(ref Class.SQLTransaction sql_trans, string eventID, string action_type,string username,bool isExist, string remarks)
        {
            var docStatus = String.Empty;
            int year = 0;
            string month = "";
            int documentStatusId = 0;
            bool forChannelManager = false;
            Globals.DocActionType docActionType;
            int curr_status = 0;
            string area = "";// qry_user.area;
            string channel ="";// qry_user.channel;

            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            int role = getRole(username);
            var qry_user = DATABASE.userHeaders.Where(o => o.userName == username).Single().apprvrDesigs.Where(o => o.roleID == role).First();

            if (action_type == Globals.DocActionType.SaveAndSend.ToString())
            {
                channel = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.channel == o.channel).First().channel;
                forChannelManager = true;
            }
            //else
            //{
            //    area = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.area == o.area).First().area;
            //}

            //if (role != 17)
           // {
           //     channel = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.channel == o.channel).First().channel;

          //  }
          //  else
           // {
          //      area = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.area == o.area).First().area;
          //  }
           
            //int role = (from userHdr in DATABASE.userHeaders
            //            from approver in DATABASE.apprvrDesigs
            //            where approver.counterId == userHdr.counterId
            //            && userHdr.userName == username
            //            select approver.roleID).Single();//(from coverageHdr in DATABASE.CoverageHdrs
            //            from userHdr in DATABASE.userHeaders
            //            from approverDesig in DATABASE.apprvrDesigs
            //            where coverageHdr.EventID == eventID
            //            && coverageHdr.EmpIdNo == userHdr.empIdNo
            //            && approverDesig.counterId == userHdr.counterId
            //            && userHdr.userName == username
            //            select approverDesig.roleID).Single();
            //try {
            
            
            //}

            try
            {
                //ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
                curr_status = (int) DATABASE.CoverageHdrs.Single(p => p.EventID == eventID).DocumentStatusId; 

            }
            catch (Exception ex)
            {
                curr_status = 0;
            }

            if (action_type == SkelClass.Globals.DocActionType.Approve.ToString() || action_type == SkelClass.Globals.DocActionType.SaveAndSend.ToString())
            {
                // get new id if approved
                int nextid = 0;


                if (curr_status == 0)
                {
                    //LoyaltyLib.Model.LYLTTestEntities DATABASE = new Model.LYLTTestEntities();
                    // Class.SQLTransaction sql_trans = new Class.SQLTransaction();


                    IEnumerable<ARMS_W.Objects.Event.CoverageHdr> curr_doc_status =
                    (from eventhdr in DATABASE.CoverageHdrs
                     where eventhdr.EventID == eventID
                     select new ARMS_W.Objects.Event.CoverageHdr()
                     {
                         DocumentStatusId = (Int16)eventhdr.DocumentStatusId,
                         Year = eventhdr.Year,
                         Month = eventhdr.Month

                     }).Take(1);




                    foreach (var itm in curr_doc_status)
                    {
                        documentStatusId = itm.DocumentStatusId;
                        nextid = (from approvalState in DATABASE.approvalStates
                                  where approvalState.docType == 14
                                  && approvalState.roleID == role
                                  && approvalState.stateID == itm.DocumentStatusId
                                  select approvalState.NextStatusId).First().Value;//itm.DocumentStatusId + 2;
                        year = itm.Year;
                        month = itm.Month;
                    }

                    //INSERTED CODE FOR NEW ROUTING
                    DocumentStatus next_apprverstate = new DocumentStatus();

                    next_apprverstate = getNextRouteAcctStatus(role, nextid);

                    if (role == 8) //if RSM/CHM role then SKIP for for RSM Approval
                    {
                        if (next_apprverstate.roleID == 8 && next_apprverstate.isFinal == false) //if RSM/CHM role then SKIP for for RSM Approval
                            next_apprverstate = getNextRouteAcctStatus(role, next_apprverstate.nextStatusID.Value);
                    }

                    nextid = next_apprverstate.stateID.Value;
                    // END INSERTED CODE FOR NEW ROUTING

                    //original code docstatus
                    //docStatus = (from state in DATABASE.approvalStates
                    //             where state.stateID == nextid && state.docType == 14
                    //             select state.stateDesc).First();
                    docStatus = next_apprverstate.stateDesc;
                    

                    sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageHdr",
                        new Dictionary<string, object>() { { "DocumentStatusId", nextid } },
                        new Dictionary<string, object>() { { "EventID", eventID } });

                    sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls",
                        new Dictionary<string, object>() { { "AcctStatus", nextid } },
                        new Dictionary<string, object>() { { "EventID", eventID },
                                                       {"IsDeleted", "F"},
                                                       {"IsAnEdit","F"}});

                    //sql_trans.CommandText = Class.QueryBuilder.InsertTo("RouteRmrks",
                    //                        new Dictionary<string, object>(){ {"EventID", eventID},
                    //                                                          {"Remarks", remarks}

                    //                        });

                }

                else {


                    IEnumerable<ARMS_W.Objects.Event.CoverageHdr> curr_doc_status =
                    (from eventhdr in DATABASE.CoverageHdrs
                     where eventhdr.EventID == eventID
                     select new ARMS_W.Objects.Event.CoverageHdr()
                     {
                         DocumentStatusId = (Int16)eventhdr.DocumentStatusId,
                         Year = eventhdr.Year,
                         Month = eventhdr.Month

                     }).Take(1);                


                    foreach (var itm in curr_doc_status)
                    {
                        documentStatusId = itm.DocumentStatusId;
                        nextid = (from state in DATABASE.approvalStates where state.docType==14 && state.stateID==itm.DocumentStatusId && state.roleID==role select state.NextStatusId).First().Value;//itm.DocumentStatusId + 1;
                        year = itm.Year;
                        month = itm.Month;
                    }

                    //INSERTED CODE FOR NEW ROUTING
                    DocumentStatus next_apprverstate = new DocumentStatus();

                    next_apprverstate = getNextRouteAcctStatus(role, nextid);

                    if (role == 8) //if RSM/CHM role then SKIP for for RSM Approval
                    {
                        if (next_apprverstate.roleID == 8 && next_apprverstate.isFinal == false) //if RSM/CHM role then SKIP for for RSM Approval
                            next_apprverstate = getNextRouteAcctStatus(role, next_apprverstate.nextStatusID.Value);
                    }

                    nextid = next_apprverstate.stateID.Value;
                    // END INSERTED CODE FOR NEW ROUTING

                    //original code docstatus
                    //docStatus = (from state in DATABASE.approvalStates
                    //             where state.stateID == nextid
                    //             && state.docType == 14
                    //           //  && state.roleID==role
                    //             select state).First().stateDesc;

                    docStatus = next_apprverstate.stateDesc;

                    sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageHdr",
                        new Dictionary<string, object>() { { "DocumentStatusId", nextid } },
                        new Dictionary<string, object>() { { "EventID", eventID } });

                    sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls",
                        new Dictionary<string, object>() { { "AcctStatus", nextid } },
                        new Dictionary<string, object>() { { "EventID", eventID },
                                                       {"IsDeleted", "F"},
                                                       {"IsAnEdit","F"}});
                
                
                }
                DATABASE.Dispose();

                #region SEND EMAIL

                //var docStatus = MiscFunctions.getDocumentStatusMessage((int)Globals.InfoType.CalendarEvent, nextid);
               // var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, nextid, area, role != 17 ? true : false, channel);
                List<MiscFunctions.GetEmailAddressesTMP> emails = new List<MiscFunctions.GetEmailAddressesTMP>();

                if (action_type == Globals.DocActionType.SaveAndSend.ToString())
                     emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, nextid, area, forChannelManager, channel);
                     //emails = MiscFunctions.GetEmailAddresses_New(nextid, role, qry_user.userHeader.empIdNo);
                else
                    emails = MiscFunctions.GetEmailAddresses(this.EmpIdNo);

                string mail_body = AppHelper.Arms_Url + "?id=" + eventID + "&doctype=clndr&Year=" + year + "&Month=" + month +"&soId=" +this.EmpIdNo;

                var subj = "ARMS Calendar Coverage Plan[" + docStatus + "]";

                foreach (var itm in emails)
                {
                    //MiscFunctions.sendMail(itm.EmailAddress, subj, mail_body);
                    // MiscFunctions.sendEmail1(mail_body, subj, itm.EmailAddress);

                    sql_trans.InsertTo("SplEmails",
                                    new Dictionary<string, object>() { 
                        {"sFrom","ARMS@matimco.com" }
                        ,{"sTo",itm.EmailAddress}
                        ,{"sCC",null}
                        ,{"sSubject",subj }
                        ,{"sMessage",mail_body}
                        });
                }


                #endregion
                docActionType = isExist ? Globals.DocActionType.Approve : documentStatusId == 1 ? Globals.DocActionType.Update : Globals.DocActionType.Created;

                SaveRouteChanges(sql_trans, docActionType, username, nextid, documentStatusId, eventID, remarks,role);

            }

            if (action_type == SkelClass.Globals.DocActionType.ReturnToRequestor.ToString())
            {
                int previd = 0;
                
               // ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

                IEnumerable<ARMS_W.Objects.Event.CoverageHdr> curr_doc_status =
                (from eventhdr in DATABASE.CoverageHdrs
                 where eventhdr.EventID == eventID
                 select new ARMS_W.Objects.Event.CoverageHdr()
                 {
                     DocumentStatusId = (Int16)eventhdr.DocumentStatusId,
                     Year = eventhdr.Year,
                     Month = eventhdr.Month

                 }).Take(1);

                foreach (var itm in curr_doc_status)
                {

                    previd = (from approval in DATABASE.approvalStates
                              where approval.roleID == role
                              && approval.stateID == itm.DocumentStatusId
                              && approval.docType == 14
                              select approval.CancelStatusId).First().Value;//itm.DocumentStatusId - 1;
                    year = itm.Year;
                    month = itm.Month;
                    documentStatusId = itm.DocumentStatusId;
                }

                docStatus = (from state in DATABASE.approvalStates
                             where state.stateID == previd
                             && state.docType==14
                             
                             select state.stateDesc).First();

                sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageHdr",
                    new Dictionary<string, object>() { { "DocumentStatusId", previd } },
                    new Dictionary<string, object>() { { "EventID", eventID } });

                sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls",
                    new Dictionary<string, object>() { { "AcctStatus", previd } },
                    new Dictionary<string, object>() { { "EventID", eventID } });

                //sql_trans.CommandText = Class.QueryBuilder.InsertTo("RouteRmrks",
                //                        new Dictionary<string, object>(){ {"EventID", eventID},
                //                                                          {"Remarks", remarks}
                                                                          
                //                        });


                DATABASE.Dispose();

                #region SEND EMAIL

                //var docStatus = MiscFunctions.getDocumentStatusMessage((int)Globals.InfoType.CalendarEvent, previd);
                //var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, previd, area, role != 17 ? true : false, channel);
                //var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, previd, area, forChannelManager, channel);
                var emails = MiscFunctions.GetEmailAddresses(this.EmpIdNo);
                string mail_body = AppHelper.Arms_Url + "?id=" + eventID + "&doctype=clndr&Year=" + this.Year + "&Month=" + this.Month + "&soId=" + this.EmpIdNo;
                var subj = "ARMS Calendar Coverage Plan[" + docStatus + "]";
                foreach (var itm in emails)
                {
                   // MiscFunctions.sendMail(itm.EmailAddress, subj, mail_body);
                    //MiscFunctions.sendEmail1(mail_body, subj, itm.EmailAddress);
                    sql_trans.InsertTo("SplEmails",
                                    new Dictionary<string, object>() { 
                        {"sFrom","ARMS@matimco.com" }
                        ,{"sTo",itm.EmailAddress}
                        ,{"sCC",null}
                        ,{"sSubject",subj }
                        ,{"sMessage",mail_body}
                        });
                }

                #endregion

                SaveRouteChanges(sql_trans, Globals.DocActionType.ReturnToRequestor, username, previd, documentStatusId, eventID, remarks,role);
            }

           

            return 0;
        }

        private static void SaveRouteChanges(Class.SQLTransaction sql_trans,Globals.DocActionType act_type,string username, int next_id, int docstat_id,string DocId,string remarks,int roleid)
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


        #endregion

        #region Update Status_backup
        public int UpdateDocStatus_backup(ref Class.SQLTransaction sql_trans, string eventID, string action_type, string username, bool isExist, string remarks)
        {
            var docStatus = String.Empty;
            int year = 0;
            string month = "";
            int documentStatusId = 0;
            bool forChannelManager = false;
            Globals.DocActionType docActionType;
            int curr_status = 0;
            string area = "";// qry_user.area;
            string channel = "";// qry_user.channel;

            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            int role = getRole(username);
            var qry_user = DATABASE.userHeaders.Where(o => o.userName == username).Single().apprvrDesigs.Where(o => o.roleID == role).First();

            if (action_type == Globals.DocActionType.SaveAndSend.ToString())
            {
                if (role == 17)
                    area = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.area == o.area).First().area;
                else
                {
                    channel = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.channel == o.channel).First().channel;
                    forChannelManager = true;
                }
            }
            else
            {
                area = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.area == o.area).First().area;
            }
            //if (role != 17)
            // {
            //     channel = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.channel == o.channel).First().channel;

            //  }
            //  else
            // {
            //      area = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.area == o.area).First().area;
            //  }

            //int role = (from userHdr in DATABASE.userHeaders
            //            from approver in DATABASE.apprvrDesigs
            //            where approver.counterId == userHdr.counterId
            //            && userHdr.userName == username
            //            select approver.roleID).Single();//(from coverageHdr in DATABASE.CoverageHdrs
            //            from userHdr in DATABASE.userHeaders
            //            from approverDesig in DATABASE.apprvrDesigs
            //            where coverageHdr.EventID == eventID
            //            && coverageHdr.EmpIdNo == userHdr.empIdNo
            //            && approverDesig.counterId == userHdr.counterId
            //            && userHdr.userName == username
            //            select approverDesig.roleID).Single();
            //try {


            //}

            try
            {
                //ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
                curr_status = (int)DATABASE.CoverageHdrs.Single(p => p.EventID == eventID).DocumentStatusId;

            }
            catch (Exception ex)
            {
                curr_status = 0;
            }

            if (action_type == SkelClass.Globals.DocActionType.Approve.ToString() || action_type == SkelClass.Globals.DocActionType.SaveAndSend.ToString())
            {
                // get new id if approved
                int nextid = 0;


                if (curr_status == 0)
                {
                    //LoyaltyLib.Model.LYLTTestEntities DATABASE = new Model.LYLTTestEntities();
                    // Class.SQLTransaction sql_trans = new Class.SQLTransaction();


                    IEnumerable<ARMS_W.Objects.Event.CoverageHdr> curr_doc_status =
                    (from eventhdr in DATABASE.CoverageHdrs
                     where eventhdr.EventID == eventID
                     select new ARMS_W.Objects.Event.CoverageHdr()
                     {
                         DocumentStatusId = (Int16)eventhdr.DocumentStatusId,
                         Year = eventhdr.Year,
                         Month = eventhdr.Month

                     }).Take(1);




                    foreach (var itm in curr_doc_status)
                    {
                        documentStatusId = itm.DocumentStatusId;
                        nextid = (from approvalState in DATABASE.approvalStates
                                  where approvalState.docType == 14
                                  && approvalState.roleID == role
                                  && approvalState.stateID == itm.DocumentStatusId
                                  select approvalState.NextStatusId).First().Value;//itm.DocumentStatusId + 2;
                        year = itm.Year;
                        month = itm.Month;
                    }


                    docStatus = (from state in DATABASE.approvalStates
                                 where state.stateID == nextid && state.docType == 14
                                 select state.stateDesc).First();

                    sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageHdr",
                        new Dictionary<string, object>() { { "DocumentStatusId", nextid } },
                        new Dictionary<string, object>() { { "EventID", eventID } });

                    sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls",
                        new Dictionary<string, object>() { { "AcctStatus", nextid } },
                        new Dictionary<string, object>() { { "EventID", eventID },
                                                       {"IsDeleted", "F"},
                                                       {"IsAnEdit","F"}});

                    //sql_trans.CommandText = Class.QueryBuilder.InsertTo("RouteRmrks",
                    //                        new Dictionary<string, object>(){ {"EventID", eventID},
                    //                                                          {"Remarks", remarks}

                    //                        });

                }

                else
                {


                    IEnumerable<ARMS_W.Objects.Event.CoverageHdr> curr_doc_status =
                    (from eventhdr in DATABASE.CoverageHdrs
                     where eventhdr.EventID == eventID
                     select new ARMS_W.Objects.Event.CoverageHdr()
                     {
                         DocumentStatusId = (Int16)eventhdr.DocumentStatusId,
                         Year = eventhdr.Year,
                         Month = eventhdr.Month

                     }).Take(1);


                    foreach (var itm in curr_doc_status)
                    {
                        documentStatusId = itm.DocumentStatusId;
                        nextid = (from state in DATABASE.approvalStates where state.docType == 14 && state.stateID == itm.DocumentStatusId && state.roleID == role select state.NextStatusId).First().Value;//itm.DocumentStatusId + 1;
                        year = itm.Year;
                        month = itm.Month;
                    }

                    docStatus = (from state in DATABASE.approvalStates
                                 where state.stateID == nextid
                                 && state.docType == 14
                                 //  && state.roleID==role
                                 select state).First().stateDesc;

                    sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageHdr",
                        new Dictionary<string, object>() { { "DocumentStatusId", nextid } },
                        new Dictionary<string, object>() { { "EventID", eventID } });

                    sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls",
                        new Dictionary<string, object>() { { "AcctStatus", nextid } },
                        new Dictionary<string, object>() { { "EventID", eventID },
                                                       {"IsDeleted", "F"},
                                                       {"IsAnEdit","F"}});


                }

                DATABASE.Dispose();

                #region SEND EMAIL

                //var docStatus = MiscFunctions.getDocumentStatusMessage((int)Globals.InfoType.CalendarEvent, nextid);

                // var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, nextid, area, role != 17 ? true : false, channel);
                List<MiscFunctions.GetEmailAddressesTMP> emails = new List<MiscFunctions.GetEmailAddressesTMP>();

                if (action_type == Globals.DocActionType.SaveAndSend.ToString())
                    emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, nextid, area, forChannelManager, channel);
                else
                    emails = MiscFunctions.GetEmailAddresses(this.EmpIdNo);

                string mail_body = AppHelper.Arms_Url + "?id=" + eventID + "&doctype=clndr&Year=" + year + "&Month=" + month + "&soId=" + this.EmpIdNo;

                var subj = "ARMS Calendar Coverage Plan[" + docStatus + "]";

                foreach (var itm in emails)
                {
                    //MiscFunctions.sendMail(itm.EmailAddress, subj, mail_body);
                    // MiscFunctions.sendEmail1(mail_body, subj, itm.EmailAddress);

                    sql_trans.InsertTo("SplEmails",
                                    new Dictionary<string, object>() { 
                        {"sFrom","ARMS@matimco.com" }
                        ,{"sTo",itm.EmailAddress}
                        ,{"sCC",null}
                        ,{"sSubject",subj }
                        ,{"sMessage",mail_body}
                        });
                }


                #endregion
                docActionType = isExist ? Globals.DocActionType.Approve : documentStatusId == 1 ? Globals.DocActionType.Update : Globals.DocActionType.Created;

                SaveRouteChanges(sql_trans, docActionType, username, nextid, documentStatusId, eventID, remarks, role);

            }

            if (action_type == SkelClass.Globals.DocActionType.ReturnToRequestor.ToString())
            {
                int previd = 0;

                // ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

                IEnumerable<ARMS_W.Objects.Event.CoverageHdr> curr_doc_status =
                (from eventhdr in DATABASE.CoverageHdrs
                 where eventhdr.EventID == eventID
                 select new ARMS_W.Objects.Event.CoverageHdr()
                 {
                     DocumentStatusId = (Int16)eventhdr.DocumentStatusId,
                     Year = eventhdr.Year,
                     Month = eventhdr.Month

                 }).Take(1);

                foreach (var itm in curr_doc_status)
                {

                    previd = (from approval in DATABASE.approvalStates
                              where approval.roleID == role
                              && approval.stateID == itm.DocumentStatusId
                              && approval.docType == 14
                              select approval.CancelStatusId).First().Value;//itm.DocumentStatusId - 1;
                    year = itm.Year;
                    month = itm.Month;
                    documentStatusId = itm.DocumentStatusId;
                }

                docStatus = (from state in DATABASE.approvalStates
                             where state.stateID == previd
                             && state.docType == 14

                             select state.stateDesc).First();

                sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageHdr",
                    new Dictionary<string, object>() { { "DocumentStatusId", previd } },
                    new Dictionary<string, object>() { { "EventID", eventID } });

                sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls",
                    new Dictionary<string, object>() { { "AcctStatus", previd } },
                    new Dictionary<string, object>() { { "EventID", eventID } });

                //sql_trans.CommandText = Class.QueryBuilder.InsertTo("RouteRmrks",
                //                        new Dictionary<string, object>(){ {"EventID", eventID},
                //                                                          {"Remarks", remarks}

                //                        });


                DATABASE.Dispose();

                #region SEND EMAIL

                //var docStatus = MiscFunctions.getDocumentStatusMessage((int)Globals.InfoType.CalendarEvent, previd);
                //var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, previd, area, role != 17 ? true : false, channel);
                //var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, previd, area, forChannelManager, channel);
                var emails = MiscFunctions.GetEmailAddresses(this.EmpIdNo);
                string mail_body = AppHelper.Arms_Url + "?id=" + eventID + "&doctype=clndr&Year=" + this.Year + "&Month=" + this.Month + "&soId=" + this.EmpIdNo;
                var subj = "ARMS Calendar Coverage Plan[" + docStatus + "]";
                foreach (var itm in emails)
                {
                    // MiscFunctions.sendMail(itm.EmailAddress, subj, mail_body);
                    //MiscFunctions.sendEmail1(mail_body, subj, itm.EmailAddress);
                    sql_trans.InsertTo("SplEmails",
                                    new Dictionary<string, object>() { 
                        {"sFrom","ARMS@matimco.com" }
                        ,{"sTo",itm.EmailAddress}
                        ,{"sCC",null}
                        ,{"sSubject",subj }
                        ,{"sMessage",mail_body}
                        });
                }

                #endregion

                SaveRouteChanges(sql_trans, Globals.DocActionType.ReturnToRequestor, username, previd, documentStatusId, eventID, remarks, role);
            }



            return 0;
        }

        private static void SaveRouteChanges_backup(Class.SQLTransaction sql_trans, Globals.DocActionType act_type, string username, int next_id, int docstat_id, string DocId, string remarks, int roleid)
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


        #endregion

        #region Update Status Changes  TO BE REMOVE/STUDY
        /**
        public int UpdateDocStatusChanges(ref Class.SQLTransaction sql_trans, string eventID, string action_type)
        {
            int year = 0;
            string month = "";

            if (action_type == SkelClass.Globals.DocActionType.Approve.ToString())
            {
                // get new id if approved
                int nextid = 0;

                //LoyaltyLib.Model.LYLTTestEntities DATABASE = new Model.LYLTTestEntities();
                // Class.SQLTransaction sql_trans = new Class.SQLTransaction();
                ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
                if (this.Acct_dtls != null)
                {
                          
                    foreach (var item in this.Acct_dtls)
                    {
                       
                        IEnumerable<ARMS_W.Objects.Event.CoverageHdr> curr_doc_status =
                        (from eventhdtls in DATABASE.CoverageDtls
                         where eventhdtls.EventID == eventID
                         && eventhdtls.Day == item.Day && eventhdtls.AccountCode == item.AccountCode

                         select new ARMS_W.Objects.Event.CoverageHdr()
                         {
                             DocumentStatusId = (Int16)eventhdtls.AcctStatus,
                             Day = eventhdtls.Day,
                             EventID = eventhdtls.EventID,
                             AccountCode = eventhdtls.AccountCode

                         }).Take(1);


                        foreach (var itm in curr_doc_status)
                            {

                                nextid = itm.DocumentStatusId + 2;
                                Day = itm.Day;
                                EventID = itm.EventID;
                                AccountCode = itm.AccountCode;

                            }
                       

                        sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls",
                            new Dictionary<string, object>() { { "AcctStatus", nextid } },
                            new Dictionary<string, object>() { { "EventID", eventID },
                                                       {"Day",Day},
                                                       {"AccountCode",AccountCode}});



                        
                        #region SEND EMAIL

                        var docStatus = MiscFunctions.getDocumentStatusMessage((int)Globals.InfoType.CalendarEvent, nextid);
                        var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, nextid);
                        string mail_body = AppHelper.Arms_Url + "?id=" + eventID + "&doctype=calendar&Year=" + this.Year + "&Month=" + this.Month;

                        var subj = "ARMS Calendar Coverage Plan[" + docStatus.stateDesc + "]";

                        foreach (var itm in emails)
                        {
                            MiscFunctions.sendMail(itm.EmailAddress, subj, mail_body);
                            // MiscFunctions.sendEmail1(mail_body, subj, itm.EmailAddress);
                        }

                        

                }


                    DATABASE.Dispose();
                   

                }

                #endregion

            }

            if (action_type == SkelClass.Globals.DocActionType.ReturnToRequestor.ToString())
            {
                int previd = 0;

                ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
                if (this.Acct_dtls != null)
                {
                    foreach (var item in this.Acct_dtls)
                    {
                        IEnumerable<ARMS_W.Objects.Event.CoverageHdr> curr_doc_status =
                        (from eventhdtls in DATABASE.CoverageDtls
                         where eventhdtls.EventID == eventID
                         && eventhdtls.Day == item.Day && eventhdtls.AccountCode == item.AccountCode
                         select new ARMS_W.Objects.Event.CoverageHdr()
                         {
                             DocumentStatusId = (Int16)eventhdtls.AcctStatus,
                             Day = eventhdtls.Day,
                             EventID = eventhdtls.EventID,
                             AccountCode = eventhdtls.AccountCode

                         }).Take(1);

                        foreach (var itm in curr_doc_status)
                        {

                            previd = itm.DocumentStatusId - 1;
                            Day = itm.Day;
                            EventID = itm.EventID;
                            AccountCode = itm.AccountCode;


                        }



                        sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls",
                            new Dictionary<string, object>() { { "AcctStatus", previd } },
                            new Dictionary<string, object>() { { "EventID", eventID },
                                                        {"Day",Day},
                                                       {"AccountCode",AccountCode}});

                        //DATABASE.Dispose();

                        #region SEND EMAIL

                        var docStatus = MiscFunctions.getDocumentStatusMessage((int)Globals.InfoType.CalendarEvent, previd);
                        var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, previd);
                        string mail_body = AppHelper.Arms_Url + "?id=" + eventID + "&doctype=calendar&Year=" + this.Year + "&Month=" + this.Month;
                        var subj = "ARMS Calendar Coverage Plan[" + docStatus.stateDesc + "]";
                        foreach (var itm in emails)
                        {
                            MiscFunctions.sendMail(itm.EmailAddress, subj, mail_body);
                            //MiscFunctions.sendEmail1(mail_body, subj, itm.EmailAddress);
                        }
                    }

                    DATABASE.Dispose();
                }

                #endregion
            }

              

            return 0;
        }   
        */
        #endregion

        #region Update Status Changes
        public int UpdateDocStatusChanges(ref Class.SQLTransaction sql_trans, string eventID, string action_type, string username)
        {
            var docStatus = String.Empty;
            int nextstatus = 0;
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            var coverage_hdr = DATABASE.CoverageHdrs.Single(o => o.EventID == eventID); 
            string empidno = coverage_hdr.EmpIdNo;
            // int beggining_linenum = GetStatusId(eventID, Day, AccountCode);
            int count = 0;
            int status_holder = (int)coverage_hdr.DocumentStatusId;
            string channel = string.Empty;
            string area = string.Empty;

            string remarks_builder = "";

            int role = getRole(username);
            //int role = (from userHdr in DATABASE.userHeaders
            //            from approverDesig in DATABASE.apprvrDesigs
            //            where
            //            approverDesig.counterId == userHdr.counterId
            //            && userHdr.userName==username
            //            select approverDesig.roleID).Single();

            var qry_user = DATABASE.userHeaders.Where(o => o.userName == username).Single().apprvrDesigs.Where(o => o.roleID == role).First();
            channel = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.channel == o.channel).First().channel;
           // if (role != 17)
           // {
              //  channel = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.channel == o.channel).First().channel;

           // }
           // else
           // {
              //  area = DATABASE.apprvrDesigs.Where(o => o.roleID == role && qry_user.area == o.area).First().area;
            //}
           

            #region APPROVE
            if (action_type == SkelClass.Globals.DocActionType.Approve.ToString())
            {
                //ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
                //var qry = from a in DATABASE = new Models.ARMSTestEntities();

                //var qry = (from a in DATABASE.CoverageHdrs
                //           from b in DATABASE.CoverageDtls
                //           where a.EventID == b.EventID && a.EventID == eventID && b.EventID == eventID &&
                //            b.IsAnEdit == "T" && b.IsDeleted == "F" &&
                //            (b.AcctStatus == 5 || b.AcctStatus == 6 ||
                //            b.AcctStatus == 10 || b.AcctStatus == 11 ||
                //            b.AcctStatus == 15 || b.AcctStatus == 16)
                //           select b.AccountCode).ToList();

                var qry = (from a in DATABASE.CoverageHdrs
                           from b in DATABASE.CoverageDtls
                           where a.EventID == a.EventID && a.EventID == eventID && b.EventID == eventID &&
                            b.IsAnEdit == "T" && b.IsDeleted == "F" &&
                            (b.AcctStatus == 5 && b.AcctStatus == 6
                            && b.AcctStatus == 10 && b.AcctStatus == 11 &&
                            b.AcctStatus == 15 && b.AcctStatus == 16 &&
                            b.AcctStatus == 19 && b.AcctStatus == 20)
                           select new
                           {
                               b.AccountCode
                           }).ToList();

              //  var query = this.Acct_dtls.Select(o => o.AccountCode).Except(qry).ToList();

                count = qry.Count();

                if (count == 0)
                {
                    nextstatus = 8;//(status_holder + 2);
                    sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageHdr",
                        new Dictionary<string, object>() { { "DocumentStatusId", nextstatus } },
                    new Dictionary<string, object>() { { "EventID", eventID } });

                }


                //var qry_1=(from a in DATABASE.CoverageHdrs
                //         where a.EventID ==eventID
                //         select new { a.DocumentStatusId}).ToList();




                //try{
                //     status_holder = (int)DATABASE.CoverageHdrs.Single(p=> p.EventID == eventID).DocumentStatusId;
                //}
                //catch(Exception ex)
                //{
                //   status_holder=0;
                //}



                if (this.Acct_dtls != null)
                {
                    //ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
             
                    foreach (var itm in this.Acct_dtls)
                    {
                        Day = itm.Day;
                        eventID = this.EventID;
                        AccountCode = itm.AccountCode;
                        AcctStatus = itm.cAcctStatus;
                        nextstatus = (from state in DATABASE.approvalStates
                                      where state.stateID == itm.cAcctStatus
                                      && state.roleID == role
                                      && state.docType == 14
                                      select state.NextStatusId).First().Value;
                        //nextstatus = (from state in DATABASE.approvalStates
                        //              where state.roleID == role
                        //              && state.stateID == itm.cAcctStatus
                        //              && state.docType == 14
                        //              select state.NextStatusId).First().Value;
                        string acc_linenum ="";
                        acc_linenum = itm.linenum;
                        //try{
                        //     acc_linenum =  DATABASE.CoverageDtls.Single(p=> p.AccountCode ==AccountCode && p.EventID == eventID && p.Day == Day).LineNum;
                        //}
                        //catch(Exception ex)
                        //{
                        //acc_linenum ="";
                        //}
                        docStatus = (from state in DATABASE.approvalStates
                                     where state.stateID == nextstatus
                                     && state.docType == 14
                                     select state.stateDesc).First();
                        sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls",
                           new Dictionary<string, object>() { { "AcctStatus", nextstatus},//itm.cAcctStatus+2 },
                                                              {"RmrkChanges",itm.RmrkChanges}},
                           new Dictionary<string, object>() { { "EventID", eventID },
                                                       {"Day",itm.Day},
                                                       {"AccountCode",itm.AccountCode}});

                       SaveRouteChanges(sql_trans, Globals.DocActionType.Approve,username, nextstatus/*itm.cAcctStatus+2*/, status_holder,acc_linenum, itm.RmrkChanges,role);
                   

                        #region SEND MAIL
                        //var docStatus = MiscFunctions.getDocumentStatusMessage((int)Globals.InfoType.CalendarEvent, itm.cAcctStatus - 1);
                      // var emails = MiscFunctions.GetEmailAddresses__((int)Globals.InfoType.CalendarEvent, nextstatus, area, role != 17 ? true : false, channel);
                      //var emails = MiscFunctions.GetEmailAddresses_New(nextstatus, role, qry_user.userHeader.empIdNo);
                       var emails = MiscFunctions.GetEmailAddresses(empidno);
                       // var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, itm.cAcctStatus - 1);
                        string mail_body = AppHelper.Arms_Url + "?id=" + eventID + "&doctype=calendar&Year=" + this.Year + "&Month=" + this.Month;

                        var subj = "Your coverage changes for Account " + itm.AccountCode + " was [" + docStatus + "]";

                        foreach (var item in emails)
                        {
                            // MiscFunctions.sendMail(item.EmailAddress, subj, mail_body);
                            // MiscFunctions.sendEmail1(mail_body, subj, itm.EmailAddress);
                            sql_trans.InsertTo("SplEmails",
                                   new Dictionary<string, object>() { 
                                        {"sFrom","ARMS@matimco.com" }
                                        ,{"sTo",item.EmailAddress}
                                        ,{"sCC",null}
                                        ,{"sSubject",subj }
                                        ,{"sMessage",mail_body}
                                        });
                        }

                        #endregion

                    
                    }

                   

                    remarks_builder = string.Join(",", (this.Acct_dtls.Select(o => o.AccountCode)).ToArray());


                    SaveRouteChanges(sql_trans, Globals.DocActionType.Approve, username, nextstatus, status_holder, eventID, remarks_builder,role);
                    
                    

                    DATABASE.Dispose();


                
                }


            }
            #endregion

            #region RETURN_TO_REQUESTOR
            if (action_type == SkelClass.Globals.DocActionType.ReturnToRequestor.ToString())
            {

                if (this.Acct_dtls != null)
                {
                    //ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

                    foreach (var itm in this.Acct_dtls)
                    {
                        Day = itm.Day;
                        eventID = this.EventID;
                        AccountCode = itm.AccountCode;
                        AcctStatus = itm.cAcctStatus;

                        nextstatus = (from state in DATABASE.approvalStates
                                      where state.stateID == itm.cAcctStatus
                                      && state.roleID == role
                                      && state.docType == 14
                                      select state.CancelStatusId).First().Value;

                        string acc_linenum = "";
                        try
                        {
                            acc_linenum = DATABASE.CoverageDtls.Single(p => p.AccountCode == AccountCode && p.EventID == eventID && p.Day == Day).LineNum;
                        }
                        catch (Exception ex)
                        {
                            acc_linenum = "";
                        }



                        sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls",
                           new Dictionary<string, object>() { { "AcctStatus", nextstatus},//itm.cAcctStatus -1 },
                                                              {"RmrkChanges",itm.RmrkChanges}},
                           new Dictionary<string, object>() { { "EventID", eventID },
                                                       {"Day",itm.Day},
                                                       {"AccountCode",itm.AccountCode}});
                        docStatus = (from state in DATABASE.approvalStates
                                     where state.stateID == nextstatus
                                     && state.docType==14
                                     select state.stateDesc).First();
                        SaveRouteChanges(sql_trans, Globals.DocActionType.Approve, username, /*itm.cAcctStatus + 2*/nextstatus, status_holder, acc_linenum, itm.RmrkChanges,role);


                        #region SEND MAIL
                        //var docStatus = MiscFunctions.getDocumentStatusMessage((int)Globals.InfoType.CalendarEvent, itm.cAcctStatus-1);
                       // var emails = MiscFunctions.GetEmailAddresses__((int)Globals.InfoType.CalendarEvent, nextstatus, area,  true, channel);
                        var emails = MiscFunctions.GetEmailAddresses(empidno);
                      //  var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, nextstatus, area, role != 17 ? true : false, channel);
                     //   var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, nextstatus/*itm.cAcctStatus - 1*/);
                        string mail_body = AppHelper.Arms_Url + "?id=" + eventID + "&doctype=calendar&Year=" + this.Year + "&Month=" + this.Month;

                        var subj = "Your coverage changes for Account" + itm.AccountCode + " was [" + docStatus + "]";

                        foreach (var item in emails)
                        {
                           // MiscFunctions.sendMail(item.EmailAddress, subj, mail_body);
                            // MiscFunctions.sendEmail1(mail_body, subj, itm.EmailAddress);

                            sql_trans.InsertTo("SplEmails",
                                  new Dictionary<string, object>() { 
                                        {"sFrom","ARMS@matimco.com" }
                                        ,{"sTo",item.EmailAddress}
                                        ,{"sCC",null}
                                        ,{"sSubject",subj }
                                        ,{"sMessage",mail_body}
                                        });
                        }
                        #endregion
                        }
                    DATABASE.Dispose();
                }



            }
            #endregion

            #region DISAPPROVE
            if (action_type == SkelClass.Globals.DocActionType.Disapprove.ToString())
            {
                var qry = (from a in DATABASE.CoverageHdrs
                           from b in DATABASE.CoverageDtls
                           where a.EventID == a.EventID && a.EventID == eventID && b.EventID == eventID &&
                            b.IsAnEdit == "T" && b.IsDeleted == "F" &&
                            (b.AcctStatus == 5 && b.AcctStatus == 6
                            && b.AcctStatus == 10 && b.AcctStatus == 11
                            && b.AcctStatus == 15 && b.AcctStatus == 16 && b.AcctStatus == 19 && b.AcctStatus == 20)
                           select new
                           {
                               b.AccountCode
                           }).ToList();

                count = qry.Count();

                if (count == 0)
                {

                    /**sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageHdr",
                          new Dictionary<string, object>() { { "DocumentStatusId","8" },
                      new Dictionary<string, object>() { { "EventID", eventID } });
                       **/

                    sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageHdr",
                        new Dictionary<string, object>() { { "DocumentStatusId", "7" } },
                        new Dictionary<string, object>() { { "EventID", eventID } });



                    //SaveRouteChanges(sql_trans, Globals.DocActionType.Disapprove, username, nextstatus, status_holder, eventID, "");
                }


                if (this.Acct_dtls != null)
                {
                    //ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

                    foreach (var itm in this.Acct_dtls)
                    {
                        Day = itm.Day;
                        eventID = this.EventID;
                        AccountCode = itm.AccountCode;
                        AcctStatus = itm.cAcctStatus;

                        string acc_linenum = "";
                        try
                        {
                            acc_linenum = DATABASE.CoverageDtls.Single(p => p.AccountCode == AccountCode && p.EventID == eventID && p.Day == Day).LineNum;
                        }
                        catch (Exception ex)
                        {
                            acc_linenum = "";
                        }
                        nextstatus = 7;
                        docStatus = (from state in DATABASE.approvalStates
                                     where state.stateID == nextstatus
                                     && state.docType==14
                                     select state.stateDesc
                                     ).First();

                        sql_trans.CommandText = Class.QueryBuilder.UpdateTo("CoverageDtls",
                           new Dictionary<string, object>() { { "AcctStatus", nextstatus/*itm.cAcctStatus + 1*/ },
                                                              {"RmrkChanges",itm.RmrkChanges}},
                           new Dictionary<string, object>() { { "EventID", eventID },
                                                       {"Day",itm.Day},
                                                       {"AccountCode",itm.AccountCode}});

                        SaveRouteChanges(sql_trans, Globals.DocActionType.Approve, username, /*itm.cAcctStatus + 2*/nextstatus, status_holder, acc_linenum, itm.RmrkChanges,role);

                       

                        #region SEND MAIL
                        //var docStatus = MiscFunctions.getDocumentStatusMessage((int)Globals.InfoType.CalendarEvent, itm.cAcctStatus+1);
                      //  var emails = MiscFunctions.GetEmailAddresses__((int)Globals.InfoType.CalendarEvent, nextstatus, area, true, channel);
                        var emails = MiscFunctions.GetEmailAddresses(empidno);
                       // var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, nextstatus, area, role != 17 ? true : false, channel);
                        //var emails = MiscFunctions.GetEmailAddresses((int)Globals.InfoType.CalendarEvent, /*itm.cAcctStatus + 1*/nextstatus);
                        string mail_body = AppHelper.Arms_Url + "?id=" + eventID + "&doctype=calendar&Year=" + this.Year + "&Month=" + this.Month;

                        var subj = "Your coverage changes for Account "+itm.AccountCode+" was [" + docStatus + "]";

                        foreach (var item in emails)
                        {
                           // MiscFunctions.sendMail(item.EmailAddress, subj, mail_body);
                            // MiscFunctions.sendEmail1(mail_body, subj, itm.EmailAddress);
                            sql_trans.InsertTo("SplEmails",
                                  new Dictionary<string, object>() { 
                                        {"sFrom","ARMS@matimco.com" }
                                        ,{"sTo",item.EmailAddress}
                                        ,{"sCC",null}
                                        ,{"sSubject",subj }
                                        ,{"sMessage",mail_body}
                                        });
                        }

                        #endregion


                        

                    }
                    DATABASE.Dispose();

                    remarks_builder = string.Join(",", (this.Acct_dtls.Select(o => o.AccountCode)).ToArray());


                    SaveRouteChanges(sql_trans, Globals.DocActionType.Disapprove, username, 8, status_holder, eventID, remarks_builder,role);

                }


            }
            #endregion


            return 0;

        }
        #endregion


        private DocumentStatus getNextRouteAcctStatus(int roleid, int documentstatusid)
        {
            DocumentStatus res = new DocumentStatus();
            List<DocumentStatus> res_list = new List<DocumentStatus>();
            var DATABASE = new ARMS_W.Models.ARMSTestEntities();
            //int roleid = getRole(username);

            var qry = (from a in DATABASE.approvalStates
                       where a.docType == (int)Globals.InfoType.CalendarEvent && a.stateID == documentstatusid
                       select a);
            foreach (var itm in qry)
            {
                res_list.Add(new DocumentStatus()
                {
                    roleID = itm.roleID,
                    CancelStatusID = itm.CancelStatusId,
                    doctype = itm.docType,
                    isFinal = itm.isFinal == "Y" ? true : false,
                    isStart = itm.isStart == "Y" ? true : false,
                    nextStatusID = itm.NextStatusId,
                    stateDesc = itm.stateDesc,
                    stateID = itm.stateID
                });
            }

            //var qry_apprvrstate = DBHelper.getData(dbtype, "SELECT * FROM approvalState WHERE docType=14 and stateId=" + documentstatusid + "").Rows;
            //foreach (DataRow itm in qry_apprvrstate)
            //{

            //    res_list.Add(new PageParam.approvalState()
            //    {
            //        roleid = Convert.ToInt32(itm["roleid"].ToString()),
            //        doctype = Convert.ToInt32(itm["doctype"].ToString()),
            //        stateid = Convert.ToInt32(itm["stateid"].ToString()),
            //        statedesc = itm["statedesc"].ToString(),
            //        isstart = itm["isstart"].ToString() == "Y" ? true : false,
            //        isfinal = itm["isfinal"].ToString() == "Y" ? true : false,
            //        nextstatusid = Globals.ToNullableInt(itm["nextstatusid"].ToString()),
            //        cancelstatusid = Globals.ToNullableInt(itm["cancelstatusid"].ToString())
            //    });
            //}

            return res_list.Count > 1 ? res_list.Where(o => o.roleID == roleid).First() : res_list.First();
        }

        private int GetStatusId(string EventId, int Day, string Accountcode)
        {
            int nextid = 0;
          var  DATABASE = new ARMS_W.Models.ARMSTestEntities();

            IEnumerable<ARMS_W.Objects.Event.CoverageHdr> curr_doc_status =
                        (from eventhdtls in DATABASE.CoverageDtls
                         where eventhdtls.EventID == EventId
                         && eventhdtls.Day == Day && eventhdtls.AccountCode == Accountcode

                         select new ARMS_W.Objects.Event.CoverageHdr()
                         {
                             DocumentStatusId = (Int16)eventhdtls.AcctStatus,
                             Day = eventhdtls.Day,
                             EventID = eventhdtls.EventID,
                             AccountCode = eventhdtls.AccountCode

                         }).Take(1);




            foreach (var itm in curr_doc_status)
            {

                nextid = itm.DocumentStatusId;
               

            }

            DATABASE.Dispose();

            return nextid;
        }

        public int getRole(string username)
        {
            var DATABASE = new ARMS_W.Models.ARMSTestEntities();
            var qry = (from user in DATABASE.userHeaders
                       from approverDesig in DATABASE.apprvrDesigs
                       where user.userName == username
                       && user.counterId == approverDesig.counterId
                       select approverDesig);
            if(qry.Select(p => p.roleID).Contains(5)) return 5;
            else if (qry.Select(p => p.roleID).Contains(8)) return 8;
            else if (qry.Select(p => p.roleID).Contains(2)) return 2;
            else return 17;
        }
        

        #region PRIVATE PART GENERATE ID
        private string GetNewCode()
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            var qry = from evnthdr in DATABASE.CoverageHdrs
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

        #endregion


        #region SAVE Route Remarks

        public string insertRouteRmrks(ref ARMS_W.Class.SQLTransaction sql_trans,string EventID, string Remarks)
        {

            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            sql_trans.CommandText = Class.QueryBuilder.InsertTo("RouteRmrks",
                                    new Dictionary<string, object>(){ {"EventID", EventID},
                                                                      {"Remarks", Remarks}

                                    });

            DATABASE.Dispose();



            return EventID;
        }

        #endregion

    }

    public class collections : ARMS_W.Interface.Event.collections
    {
        public string Objdesc { get; set; }
        public string ObjectiveCode { get; set; }
        public string Brand { get; set; }
        public string Amount { get; set; }
        public string ActualAmount { get; set; }
        public string Remarks { get; set; }
            
    }

    public class merchandising : ARMS_W.Interface.Event.merchandising
    {
        public string Objdesc { get; set; }
        public string ObjectiveCode { get; set; }
        public string Brand { get; set; }
        public string Amount { get; set; }
        public string Productpresented { get; set; }
        public string counterclerk { get; set; }
        public string CounterClerkNo { get; set; }
        public string Remarks { get; set; }
        

    }

    public class sales : ARMS_W.Interface.Event.sales
    {

        public string Objdesc { get; set; }
        public string ObjectiveCode { get; set; }
        public string Brand { get; set; }
        public string Amount { get; set; }
        public string ActualAmount { get; set; }
        public string Remarks { get; set; }
        public string dtlsRrmks { get; set; }

    }

    public class customersrvc : ARMS_W.Interface.Event.customersrvc
    {

        public string Objdesc { get; set; }
        public string ObjectiveCode { get; set; }
        public string Brand { get; set; }
        public string Amount { get; set; }

    }

    public class changesdtls : ARMS_W.Interface.Event.changesdtls
    {

        public int Day { get; set; }
        public string AccountCode { get; set; }
        public int cAcctStatus { get; set; }
        public string RmrkChanges { get; set; }
        public string linenum { get; set; }
       

    }

    public class nextInventory : ARMS_W.Interface.Event.nextInventory
    {
        public string AccountCode { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonNo { get; set; }
        public int Day { get; set; }
        public string ObjectiveCode { get; set; }
    
    }

  




    
}
