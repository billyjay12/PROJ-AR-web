using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARMS_W.Class;
using System.Data.OleDb;
using System.IO;
using System.Data;

namespace ARMS_W.Controllers
{
    public class BusinessReviewController : Controller
    {
        //
        // GET: /BusinessReview/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /BusinessReview/Create

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateBusinessReview()
        {
            return View();
        }

        public ActionResult SetUp()
        {
            return View();
        }

        public ActionResult BusinessReviewList()
        {
            return View();
        }

        public ActionResult BusinessReviewDetails()
        {
            return View();
        }


        // Conduct Business Review
        [HttpPost]
        public string SaveBusinessReviewDoc(
            string br_no,
            string ccaNum,
            string date,
            string acctCode,
            string comExAgr,
            string comAcctPer,
            string STOrigAnn,
            string STRevAnn,
            string STReason,
            string plan,
            string support,
            string other_info,
            string ExstcrdLimit,
            string ReccrdLimit,
            string ExstcrdTerm,
            string encoded_by,
            string ReccrdTerm
            )
        {

            SQLTransaction mt_trans = new SQLTransaction();
            routingController route_trans = new routingController();

            string newccaNum = "";
            string strquery = "SELECT ccaNum from customerHeader WHERE acctCode='" + acctCode + "'";
            OleDbDataReader greader = SqlDbHelper.getData(strquery);
            // check if ID exist and assign value to br_no
            if (greader.Read())
            {
                newccaNum = greader.GetValue(0).ToString();
            }

            /*
             * RECODE CHARACTERS
            */
            comExAgr = StringHelper.ReCodeCharacters(comExAgr);
            comAcctPer = StringHelper.ReCodeCharacters(comAcctPer);
            plan = StringHelper.ReCodeCharacters(plan);
            support = StringHelper.ReCodeCharacters(support);
            other_info = StringHelper.ReCodeCharacters(other_info);
            STReason = StringHelper.ReCodeCharacters(STReason);

            try
            {


                mt_trans.StartTransaction();
                /*
                comExAgr = StringHelper.InsertQoutes(comExAgr);
                comAcctPer = StringHelper.InsertQoutes(comAcctPer);
                plan = StringHelper.InsertQoutes(plan);
                support = StringHelper.InsertQoutes(support);
                other_info = StringHelper.InsertQoutes(other_info);
                STReason = StringHelper.InsertQoutes(STReason);
                */
                mt_trans.CommandText = SqlQueryHelper.UpdateBusReview(
                      br_no,
                      comExAgr,
                      comAcctPer,
                      encoded_by

                    );

                mt_trans.CommandText = SqlQueryHelper.InsertBusRevStrategicPlan(
                     br_no,
                     STOrigAnn,
                     STRevAnn,
                     STReason,
                     plan,
                     support,
                     other_info
                  );

                mt_trans.CommandText = SqlQueryHelper.InsertBusRevPropCreditChange(
                    br_no,
                    ExstcrdLimit,
                    ReccrdLimit,
                    ExstcrdTerm,
                    ReccrdTerm
                 );


                mt_trans.Committransaction();
                return "00:";
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;
            }
        }


        [HttpPost]
        
        // for scheduling
        public string SetUpBusRevSchedule(
        string status,
        string ccaNum,
        string date,
        string acctCode,
        string txt_encoded_by

        )
        {
            SQLTransaction mt_trans = new SQLTransaction();
            routingController route_trans = new routingController();
            string br_no = "";
            string newccaNum = "";
            status = "0";

            string strquery = "select dbo.getNewReviewNo('" + acctCode + "')";
            OleDbDataReader _greader = SqlDbHelper.getData(strquery);
            // check if ID exist and assign value to br_no
            if (_greader.Read())
            {
                br_no = _greader.GetValue(0).ToString();
            }


            // query for ccaNum
            string strquery1 = "SELECT ccaNum from customerHeader where acctCode ='" + acctCode + "'";
            OleDbDataReader greader = SqlDbHelper.getData(strquery1);
            // check if ccaNum exist and assign value to newccaNum
            if (greader.Read())
            {
                newccaNum = greader.GetValue(0).ToString();

            }


            // saving
            try
            {
                mt_trans.StartTransaction();

                mt_trans.CommandText = SqlQueryHelper.InsertBusReview(
                      br_no,
                      acctCode,
                      newccaNum,
                      Convert.ToDateTime(date),
                      status.ToString(),
                      txt_encoded_by

                    );

                mt_trans.Committransaction();

                SqlDbHelper.ExecNQuery("EXEC MTC_sp_OnScheduleNotification '" + br_no + "'");

                return "00:";
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;
            }

        }

        // ROUTING 
        public string CallRouting(string val_action_type, string val_brNo, string remarks)
        {

            routingController eroute = new routingController();
            string u_name = Session["username"].ToString();
            string branch = "";
            string sQuery = "SELECT acctCode FROM busReview WHERE busReviewNo='" + val_brNo + "'";
            string StrQuery = "SELECT status FROM busReview WHERE busReviewNo='" + val_brNo + "'";
            string acctCode = "";
            string prev_status = "";
            
            DataTable status = SqlDbHelper.getDataDT(StrQuery);
            DataTable acctCodeTbl = SqlDbHelper.getDataDT(sQuery);
            
            foreach (DataRow tmp_status in status.Rows)
            {
                prev_status = tmp_status[0].ToString();
            }
            foreach (DataRow tmp_code in acctCodeTbl.Rows) 
            {
                acctCode = tmp_code[0].ToString();
            }

            bool isApprove;
            string docLink = AppHelper.Arms_Url + "?id=" + val_brNo + "&doctype=brw";

            branch = val_brNo.IndexOf("BRQC") > -1 ? "Luzon" : "VisMin";

            try
            {

                if (val_action_type == "Approve")
                {
                    int val = 1;
                    isApprove = Convert.ToBoolean(val);

                }
                else
                {
                    int val2 = 0;
                    isApprove = Convert.ToBoolean(val2);

                }

                // Disapproved by VP Finance
                if (prev_status == "5" && val_action_type == "Disapprove")
                {
                    DisapprovedByVPTFI(val_brNo, acctCode, branch);
                }
                
                // Approved by VP Sales
                else if (prev_status == "7" && val_action_type == "Approve")
                {
                    CloseBusReviewDoc(val_brNo, "9", acctCode, branch);
                }
                
                // Send Back to VP Finance by VP Sales
                else if (prev_status == "7" && val_action_type == "SendBackToVpFinance")
                {
                    
                    SendBackToVPFinance(val_brNo, branch, acctCode);
                }

                // ROUTING
                else
                {
                    try{ eroute.routeNext(5, val_brNo, docLink, branch, isApprove, "'" +  acctCode + "'"); }
                    catch (Exception x) { x.Message.ToString(); }
                }
                
                if (prev_status == "0")
                {
                    val_action_type = "Encode";
                }

                if (prev_status == "10" && val_action_type == "Disapprove")
                {
                    val_action_type = "Update";

                }

                remarks = remarks == "undefined" ? "" : StringHelper.ReCodeCharacters(remarks);

                val_action_type = val_action_type == "SendBackToVpFinance" ? "Send_Back_To_Requester" : val_action_type;
               
                // SAVE TO DOCUMENT_HISTORY
                SqlDbHelper.ExecNQuery("INSERT INTO document_history " +
                                        " (DocType,docId,mType,Remarks,mDate,Creator_Pos,Creator_id,Creator_Uname,DocStatus,PrevDocStatus,TAG,TAG1) " +
                                        " SELECT 'BR','" + val_brNo + "','"+ val_action_type.ToUpper() + "','" + remarks + "','" + DateTime.Now.ToString() + "' " +
                                        ",'',(SELECT DISTINCT empidNo FROM userHeader WHERE username = '" + u_name + "'),'" + u_name + "',(SELECT status FROM busreview WHERE busreviewNo='" + val_brNo + "'),'" + prev_status + "','" + val_action_type.ToUpper() + "',''");
                return "00:";

            }
            catch (Exception ex)
            {
                return "01:" + ex.Message;
            }
        }


        
        public string SaveFinanceDoc(
            string busReviewNo,
            string lenPayment,
            string existCreditTerm,
            string remarksCredTerm,
            string dishonCheck,
            string remarksDisCheck
            )
        {
            SQLTransaction mt_trans = new SQLTransaction();
            /*
             * RECODE CHARACTERS
            */
            remarksCredTerm = StringHelper.ReCodeCharacters(remarksCredTerm);
            remarksDisCheck = StringHelper.ReCodeCharacters(remarksDisCheck);
            try
            {
                mt_trans.StartTransaction();
                remarksCredTerm = StringHelper.InsertQoutes(remarksCredTerm);
                remarksDisCheck = StringHelper.InsertQoutes(remarksDisCheck);
                mt_trans.CommandText = SqlQueryHelper.BusRevFinanceUse(
                      busReviewNo,
                      lenPayment,
                      existCreditTerm,
                      remarksCredTerm,
                      dishonCheck,
                      remarksDisCheck
                   );


                mt_trans.Committransaction();
                return "00:";
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;

            }
        }

        public string DisapprovedByVPTFI(string val_brNo, string acctCode, string branch)
        {

            SQLTransaction mt_trans = new SQLTransaction();
            routingController eroute = new routingController();
            DataTable email_list = null;
            string status = "7";
            string docLink = AppHelper.Arms_Url + "?id=" + val_brNo + "&doctype=brw";
            string mailserver = "mail2.matimco.com";
            string title = "Business Review doc. no. " + val_brNo + " was disapproved and is wating: " + AppHelper.BusReviewDocStateMsg(status) + "";
            string area = eroute.getArea("'" + acctCode + "'");
            string channel = eroute.getChannel("'" + acctCode + "'");
            
            branch = branch == "Luzon" ? "LZ" : "VM";
           
            try
            {
               
                mt_trans.StartTransaction();
                mt_trans.CommandText = SqlQueryHelper.CloseBusRevDocument(val_brNo,status);
                email_list = eroute.getRecipientEmail(branch, area, channel, status);
                eroute.sendEmailToRecepients(docLink, title, email_list, mailserver);
                eroute.sendEmailToNxtApprvr(docLink, val_brNo, eroute.getNxtApprvrEmail("5", branch, area, channel), mailserver);

                mt_trans.Committransaction();
                return "00:";

            }

            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;
            }


        }

        public string SendBackToVPFinance(string brNo, string branch, string acctCode)
        {

            SQLTransaction mt_trans = new SQLTransaction();
            routingController eroute = new routingController();
            DataTable email_list = null;
            string status = "5";
            string docLink = AppHelper.Arms_Url + "?id=" + brNo + "&doctype=brw";
            string mailserver = "mail2.matimco.com";
            // E-mail title
            string title = "Business Review doc. no. " + brNo + " was disapproved and is wating: " + AppHelper.BusReviewDocStateMsg(status) + "";
            // Get areas
            string area = eroute.getArea("'" + acctCode + "'");
            // Get channels
            string channel = eroute.getChannel("'" + acctCode + "'");
          
            try
            {
                // Begin transaction
                mt_trans.StartTransaction();
                // Update Business Review Table
                mt_trans.CommandText = SqlQueryHelper.SendBackToVPTFI(brNo,status);
                // Get recepient's e-mail address
                email_list = eroute.getRecipientEmail(branch, area, channel, status);
                // Send e-mail to recepients
                eroute.sendEmailToRecepients(docLink, title, email_list, mailserver);
                // Send e-mail to next approver
                eroute.sendEmailToNxtApprvr(docLink, brNo, eroute.getNxtApprvrEmail("3", branch, area, channel), mailserver);
                // Commit transaction
                mt_trans.Committransaction();
                return "00:";

            }

            catch (Exception ex)
            {
                // Rollback transaction
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;
            }


        }

        public string CloseBusReviewDoc(string val_brNo, string status, string acctCode, string branch)
        {
            routingController route = new routingController();
            SQLTransaction mt_trans = new SQLTransaction();
            DataTable AcctClass = SqlDbHelper.getDataDT("SELECT rtrim(ltrim(cast(a.acctClass as nvarchar))) as 'acctClass' FROM customerHeader a, busreview b WHERE a.acctcode = b.acctcode and b.busreviewno = '" + val_brNo + "'");
            string docLink = AppHelper.Arms_Url + "?id=" + val_brNo + "&doctype=brw";
            string mailserver = "mail2.matimco.com";
            string tagCEO = "";
            string title = "";
            string channel = route.getChannel("'" + acctCode + "'");
            string area = route.getArea("'" + acctCode + "'");
            branch = branch == "Luzon" ? "LZ" : "VM";

            DataTable recepient_email = route.getRecipientEmail(branch, area, channel, status);
            string _emailTitle = "";

            foreach (DataRow tmp_class in AcctClass.Rows)
            {
                tagCEO = tmp_class["acctclass"].ToString();
            }
           
                if (tagCEO == "TOP TIER")
                {
                    status = "8";
                    _emailTitle = "Business Review doc. no. " + val_brNo + " was approved and is waiting: " + AppHelper.BusReviewDocStateMsg("8") + "";
                    route.sendEmailToRecepients(docLink, _emailTitle, recepient_email, mailserver);
                    route.sendEmailToNxtApprvr(docLink, val_brNo, route.getNxtApprvrEmail("7", branch, area, channel), mailserver);
                }
                else
                {

                    title = "Business Review doc. no. " + val_brNo + " was successfully closed.";
                    route.sendEmailToRecepients(docLink, title, recepient_email, mailserver);

                }

                try
                {
                    mt_trans.StartTransaction();
                    mt_trans.CommandText = SqlQueryHelper.CloseBusRevDocument(val_brNo,status);
                    mt_trans.Committransaction();
                    return "00:";
                }
                catch (Exception ex)
                {
                    mt_trans.RollbackTransaction();
                    return "01:" + ex.Message;
                }

            }


        }
    }


