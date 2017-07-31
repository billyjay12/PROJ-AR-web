using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ARMS_W.Class
{
    public class AppHelper
    {
        private static IList<string> _RegCustomerDirect = new List<string>(new string[] {"1", "2", "3", "7", "8", "6", "9", "1008", "1000"});
        public static IList<string> RegCustomerDirect { get {return _RegCustomerDirect;} }

        private static IList<string> _RegCustomerInDirect = new List<string>(new string[] { "1", "1008", "1000" });
        public static IList<string> RegCustomerInDirect { get { return _RegCustomerInDirect; } }

        private static IList<string> _WalkCustomer = new List<string>(new string[] { "1", "1008", "1000" });
        public static IList<string> WalkCustomer { get { return _WalkCustomer; } }

        public static string GetAccCreationNextStep(string current_stat)
        {

            if (current_stat == "1") { return "2"; } // csr -> asm
            else if (current_stat == "2") { return "3"; } // asm -> chm
            else if (current_stat == "3") { return "7"; } // chm -> cnc
            else if (current_stat == "7") { return "8"; } // cnc -> fnm
            else if (current_stat == "8") { return "6"; } // fnm -> vpbsm
            else if (current_stat == "6") { return "9"; } // vpbsm -> vptfi
            else if (current_stat == "9") { return "1008"; } // vpbsm -> fnm
            /* cheat code */
            else if (current_stat == "1008") { return "1000"; } // fnm - > csr/1000

            else { return "0"; }
        }

        public static string GetUserPosition(string user_id)
        {
            if (user_id == "1") { return "csr"; }
            else if (user_id == "2") { return "asm"; }
            else if (user_id == "3") { return "chm"; }
            else if (user_id == "4") { return "brm"; }
            else if (user_id == "5") { return "brd"; }
            else if (user_id == "6") { return "vpbsm"; }
            else if (user_id == "7") { return "cnc"; }
            else if (user_id == "8") { return "fnm"; }
            else if (user_id == "9") { return "vptfi"; }
            else if (user_id == "10") { return "tmg"; }
            else if (user_id == "11") { return "fsp"; }
            else if (user_id == "12") { return "csm"; }
            else if (user_id == "13") { return "so"; }
            else if (user_id == "14") { return "bmt"; }
            else if (user_id == "1000") { return "csr"; }
            else if (user_id == "1008") { return "fnm"; }

            else { return ""; }
        }

        public static string GetUserPositionId(string user_position)
        {
            if (user_position == "csr") { return "1"; }
            else if (user_position == "asm") { return "2"; }
            else if (user_position == "chm") { return "3"; }
            else if (user_position == "brm") { return "4"; }
            else if (user_position == "brd") { return "5"; }
            else if (user_position == "vpbsm") { return "6"; }
            else if (user_position == "cnc") { return "7"; }
            else if (user_position == "fnm") { return "8"; }
            else if (user_position == "vptfi") { return "9"; }
            else if (user_position == "tmg") { return "10"; }
            else if (user_position == "fsp") { return "11"; }
            else if (user_position == "csm") { return "1"; }
            else if (user_position == "so") { return "13"; }
            else if (user_position == "bmt") { return "14"; }

            else { return ""; }
        }

        public static string GetDocStateMsg(string docstatus)
        {
            if (docstatus == "1") { return "For CSR Update"; }
            else if (docstatus == "2") { return "For ASM Approval"; }
            else if (docstatus == "3") { return "For Channel Manager Approval"; }
            else if (docstatus == "4") { return "For Brand Manager Approval"; }
            else if (docstatus == "5") { return "For Brand Director Approval"; }
            else if (docstatus == "6") { return "For Sales Director Approval"; }
            else if (docstatus == "7") { return "For Credit Investigation"; }
            else if (docstatus == "8") { return "For Finance Manager Approval"; }
            else if (docstatus == "9") { return "For VP TFI Approval"; }
            else if (docstatus == "10") { return "For TMG"; }
            else if (docstatus == "11") { return "For FSP"; }
            /* also for fnm */
            else if (docstatus == "1008") { return "For Customer Code Creation"; }  
            
            /* disapprovals */
            else if (docstatus == "-1") { return "Request Cancelled by CSR"; }
            else if (docstatus == "-2") { return "Disapproved by Area Sales Manager"; }
            else if (docstatus == "-3") { return "Disapproved by Channel Manager"; }
            else if (docstatus == "-4") { return "Disapproved by Brand Manager"; }
            else if (docstatus == "-5") { return "Disapproved by Brand Director"; }
            else if (docstatus == "-6") { return "Disapproved by Sales Director"; }
            else if (docstatus == "-7") { return "Disapproved by CNC"; }
            else if (docstatus == "-8") { return "Disapproved by Finance Manager"; }
            else if (docstatus == "-9") { return "Disapproved by VP TFI"; }
            else if (docstatus == "-10") { return "Disapproved by TMG"; }
            else if (docstatus == "-11") { return "Disapproved by FSP"; }
            else if (docstatus == "-12") { return "Disapproved by CSM"; }
            else if (docstatus == "-1008") { return "Disapproved by Finance Manager"; }

            /* final doc status */
            else if (docstatus == "1000") { return "Approved"; }
            else { return "unknown Status"; }
        }
                
        public static string GetEMATDocStateMsg(string docstatus)
        {
            if (docstatus == "1") { return "For CS Manager Approval"; }
            
            /* disapproved */
            else if (docstatus == "2") { return "Disapproved by CS Manager"; }

            /* final doc status */
            else { return "Posted"; }
        }
        
        public static string BusReviewDocStateMsg(string busReviewDocStatus)
        {
            if (busReviewDocStatus == "0") { return "For Review"; }
            else if (busReviewDocStatus == "1") { return "For Marketing Director Approval"; }
            else if (busReviewDocStatus == "2") { return "Closed"; }
            else if (busReviewDocStatus == "3") { return "For Finance Manager Assessment"; }
            else if (busReviewDocStatus == "4") { return "Closed"; }
            else if (busReviewDocStatus == "5") { return "For VP-Finance Concurrence"; }
            else if (busReviewDocStatus == "6") { return "For Sales Director Concurrence"; }
            else if (busReviewDocStatus == "7") { return "For Sales Director Concurrence"; }
            else if (busReviewDocStatus == "8") { return "For CEO Approval"; }
            else if (busReviewDocStatus == "9") { return "Closed"; }
            else if (busReviewDocStatus == "10") { return "For CSR Update"; }
            else if (busReviewDocStatus == "11") { return "Closed"; }
            else { return "Approved"; }
        }

        public static string MKTDocStateMsg(string MKTDocStatus)
        {
            if (MKTDocStatus == "2") { return "Area Sales Manager"; }
            else if (MKTDocStatus == "4") { return "Channel Manager"; }
            else if (MKTDocStatus == "6") { return "Sales Director"; }
            else if (MKTDocStatus == "8") { return "Brand Manager"; }
            else if (MKTDocStatus == "10") { return "Brand Director"; }
            else { return "Approved"; }
        }

        public static string GetMMANextStep(string current_stat) 
        {
            if (current_stat == AppHelper.GetUserPositionId("so")) { return AppHelper.GetUserPositionId("asm"); }
            else if (current_stat == AppHelper.GetUserPositionId("asm")) { return AppHelper.GetUserPositionId("chm"); }
            else if (current_stat == AppHelper.GetUserPositionId("chm")) { return "1000"; }
            else { return "0"; }
        }

        public static string MarketingProgramstatus(string MarketingProgramStatus)
        {
            if (MarketingProgramStatus == "0") { return "Open"; }
            else if (MarketingProgramStatus == "1") { return "For SSG Manager Approval"; }
            else if (MarketingProgramStatus == "2") { return "Disapproved by SSG Manager"; }
            else if (MarketingProgramStatus == "3") { return "Approved by SSG Manager"; }
            else if (MarketingProgramStatus == "4") { return "For BMT Proposal"; }
            else if (MarketingProgramStatus == "5") { return "Approved by BMT"; }
            else if (MarketingProgramStatus == "6") { return "Disapproved to BMT"; }
            else { return "Close"; }
        }

        public static string MarketingProgramUserPosition(string user_position)
        {
            if (user_position == "ssm") { return "1"; }
            else if (user_position == "bmt") { return "5"; }
            else { return ""; }
        }

        public static string GetMarketingDocStateMsg(string docstatus)
        {
            if (docstatus == "1") { return "For SSG Manager Approval"; }
            else if (docstatus == "2") { return "Disapproved by SSG Manager"; }
            else if (docstatus == "3") { return "Approved by SSG Manager"; }
            else if (docstatus == "4") { return "For BMT Approval "; }
            else if (docstatus == "5") { return "Approved By VP-BMT"; }
            else if (docstatus == "6") { return "Disapproved By VP-BMT"; }
            else  { return "Close"; }
        }
        
        public static string GetAccDocStatusMessage(string status1/*customerheader*/, string status2/*propca*/, string ccanum = "", string hasmodified = "", string proposedchanges_issentback = "" ) {
            
            System.Data.DataTable quickFix = null;
            
            if (proposedchanges_issentback != "")
            {
                if (proposedchanges_issentback == "1") return GetDocStateMsg(status2);
            }
            
            if (ccanum != "")
            {
                if (hasmodified == "1" && status1 == "1000" && status2 == "1") return "Amended";
            }
            
            if (status1 == "1000" && GetUserPosition(status2) == "csr")
            {
                return "Approved";
            }

            if (GetUserPosition(status1) == "csr" && GetUserPosition(status2) == "csr")
            {
                return GetDocStateMsg(status1);
            }

            if (GetUserPosition(status1) == "asm" && GetUserPosition(status2) == "csr")
            {
                return GetDocStateMsg(status1); ;
            }

            if (GetUserPosition(status1) == "chm" && GetUserPosition(status2) == "csr")
            {
                return GetDocStateMsg(status1);
            }

            if (GetUserPosition(status1) == "vpbsm" && GetUserPosition(status2) == "csr")
            {
                return GetDocStateMsg(status1);
            }

            if (GetUserPosition(status1) == "cnc" && GetUserPosition(status2) == "csr")
            {
                return GetDocStateMsg(status1);
            }

            if (GetUserPosition(status1) == "fnm" && GetUserPosition(status2) == "csr")
            {
                return GetDocStateMsg(status1);
            }

            if (GetUserPosition(status1) == "vptfi" && GetUserPosition(status2) == "csr")
            {
                return GetDocStateMsg(status1);
            }

            if (status1 == "1000" && GetUserPosition(status2) == "asm")
            {
                return GetDocStateMsg(status2);
            }

            if (status1 == "1000" && GetUserPosition(status2) == "chm")
            {
                return GetDocStateMsg(status2);
            }

            if (status1 == "1000" && GetUserPosition(status2) == "vpbsm")
            {
                return GetDocStateMsg(status2);
            }

            if (status1 == "1000" && GetUserPosition(status2) == "cnc")
            {
                return GetDocStateMsg(status2);
            }

            if (status1 == "1000" && GetUserPosition(status2) == "fnm")
            {
                return GetDocStateMsg(status2);
            }

            if (status1 == "1000" && GetUserPosition(status2) == "vptfi")
            {
                return GetDocStateMsg(status2);
            }

            if (status1 != "1000" && GetUserPosition(status2) == "csr")
            {
                return GetDocStateMsg(status1);
            }
            
            return "unknown Status";
        }

        public static string GetMMaDocStatusMessage(string docstatus) {
            if (docstatus == "2") { return "For ASM Approval"; }
            if (docstatus == "3") { return "for Channel Manager"; }

            if (docstatus == "1000") { return "Approved"; }

            if (docstatus == "-2") { return "Disapproved by ASM"; }
            if (docstatus == "-3") { return "Disapproved by CHM"; }

            return "unknown Status";
        }

        public static string GetLdbDocStatMsg(
            string is_nego_contacted,
            string nego_quote_submitted,
            string nego_followup,
            string is_lost_sales,
            string is_closed,
            string is_conf_encoded
            ) {

                if (
                        (
                            is_nego_contacted == "1" ||
                            nego_quote_submitted == "1" ||
                            nego_followup == "1"
                        ) &&
                        is_lost_sales != "1" &&
                        is_closed != "1" &&
                        is_conf_encoded != "1"
                    ) {
                        return "Ongoing negotiation";
                }

                if (
                        (
                            is_nego_contacted == "1" ||
                            nego_quote_submitted == "1" ||
                            nego_followup == "1"
                        ) &&
                        is_lost_sales == "1" &&
                        is_closed != "1" &&
                        is_conf_encoded != "1"
                    ) {
                    return " a Lost Sales";
                }

                if (
                        (
                            is_nego_contacted == "1" ||
                            nego_quote_submitted == "1" ||
                            nego_followup == "1"
                        ) &&
                        is_lost_sales != "1" &&
                        is_closed == "1" &&
                        is_conf_encoded != "1"
                    ) {
                    return "Closed";
                }

                if (
                        (
                            is_nego_contacted == "1" ||
                            nego_quote_submitted == "1" ||
                            nego_followup == "1"
                        ) &&
                        (
                            is_lost_sales == "1" ||
                            is_closed == "1" 
                        ) &&
                        is_conf_encoded == "1"
                    ) {
                    return "Confirmed";
                }

            return "unknown Status";
        }

        public static string GetLdbIDocStatMsg(string docstatus) {

            if (docstatus == "1") { return "For CSR Update"; }
            else if (docstatus == "2") { return "For ASM Approval"; }
            else if (docstatus == "3") { return "For Channel Manager Approval"; }
            else if (docstatus == "4") { return "For Brand Manager Approval"; }
            else if (docstatus == "8") { return "For Finance Manager Approval"; }
            else if (docstatus == "10") { return "For TMG"; }
            else if (docstatus == "11") { return "For FSP"; }
            /* also for CSR/TMG/FSP/BRAND */
            else if (docstatus == "1111") { return "For Update"; }
            else if (docstatus == "1000") { return "Approved"; }

            /* disapprovals */
            else if (docstatus == "-2") { return "Disapproved by Area Sales Manager"; }
            else if (docstatus == "-3") { return "Disapproved by Channel Manager"; }
            else if (docstatus == "-4") { return "Disapproved by Brand Manager"; }
            else if (docstatus == "-8") { return "Disapproved by Finance Manager"; }
            else if (docstatus == "-10") { return "Disapproved by TMG"; }
            else if (docstatus == "-11") { return "Disapproved by FSP"; }

            return "unknown Status";
        }

        public static void SaveToLOg(
                string doctype,
                string docid,
                string action_type,
                string remarks,
                string current_user,
                string docstatus = "",
                string prev_docstatus = "",
                string tag = ""
            ) {
            SQLTransaction mt_trans = new SQLTransaction();
            _User Ousr = new _User(current_user);

            try 
            {
                mt_trans.StartTransaction();
                
                mt_trans.InsertTo("document_history", new Dictionary<string, object>() { 
                    {"DocType", doctype}
                    ,{"DocId", docid}
                    ,{"mType", action_type}
                    ,{"Remarks", remarks}
                    ,{"mDate", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}
                    ,{"Creator_Pos", ""}
                    ,{"Creator_Id", Ousr.EmployeeIdNo}
                    ,{"Creator_Uname", Ousr.UserName.ToUpper()}
                    ,{"DocStatus", docstatus}
                    ,{"PrevDocStatus", prev_docstatus}
                    ,{"TAG", tag}
                });

                //mt_trans.RollbackTransaction();
                mt_trans.Committransaction();
            }
            catch (Exception ex) 
            {
                mt_trans.RollbackTransaction();
            }
        }

        public static string GetAccWalkInNextStep(string cur_docstatus) {
            // cheat code
            if (cur_docstatus == AppHelper.GetUserPositionId("csr")) { return "1008"; }

            else if (cur_docstatus == AppHelper.GetUserPositionId("cnc")) { return "8"; }
            else if (cur_docstatus == AppHelper.GetUserPositionId("fnm")) { return "9"; }
            else if (cur_docstatus == AppHelper.GetUserPositionId("vptfi")) { return "1008"; }
            

            // to finance
            if (cur_docstatus == "1008") { return "1000"; }
            else { return ""; }
        }
        
        public static string GetAccIndirectCreationNextStep(string current_stat)
        {
            if (current_stat == "1") { return "1008"; } // csr -> fnm
            /* cheat code */
            else if (current_stat == "1008") { return "1000"; } // fnm - > csr/1000

            else { return "0"; }
        }

        public static string GetUserPositionTitle(string user_id)
        {
            if (user_id == "1") { return "Customer Service Representative"; }
            else if (user_id == "2") { return "Area Sales Manager"; }
            else if (user_id == "3") { return "Channel Manager"; }
            else if (user_id == "4") { return "brm"; }
            else if (user_id == "5") { return "brd"; }
            else if (user_id == "6") { return "Sales Director"; }
            else if (user_id == "7") { return "Credit & Collection"; }
            else if (user_id == "8") { return "Finance Manager"; }
            else if (user_id == "9") { return "VP Finance"; }
            else if (user_id == "10") { return "tmg"; }
            else if (user_id == "11") { return "fsp"; }
            else if (user_id == "12") { return "csm"; }
            else if (user_id == "13") { return "Sales Officer"; }
            else if (user_id == "14") { return "bmt"; }
            else if (user_id == "1000") { return "Customer Service Representative"; }
            else if (user_id == "1008") { return "Finance Manager"; }

            else { return ""; }
        }

        public static void InsertToTable(string tablename, Dictionary<string, object> values) 
        {
            SQLTransaction m_tr = new SQLTransaction();
            try 
            {
                m_tr.StartTransaction();

                m_tr.InsertTo("Scheduler", values);

                m_tr.Committransaction();
            }
            catch (Exception ex) 
            {
                m_tr.RollbackTransaction();
            }
            m_tr = null;
        }

        public static void InsertActivityLog(string username, string action) 
        {
            SQLTransaction m_tr = new SQLTransaction();

            try
            {
                m_tr.StartTransaction();

                m_tr.InsertTo("ActivityLogs", new Dictionary<string, object>() { 
                    {"username", username }
                    ,{"action", action }
                    ,{"datetimestmp", DateTime.Now }
                });

                m_tr.Committransaction();
            }
            catch (Exception ex)
            {
                m_tr.RollbackTransaction();
            }

            m_tr = null;
        }

        public static string Arms_Url = "http://122.53.100.210/arms";
        public static string ArmsDB = "ARMS";

    }
}
 