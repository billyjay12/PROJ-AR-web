using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using System.Data;
using ARMS_W.Class;

namespace ARMS_W.Controllers
{
    public class LeadDbController : Controller
    {
        //
        // GET: /LeadDb/

        [AuthorizeUsr]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUsr]
        public ActionResult LeadDbList() {
            return View();
        }

        [AuthorizeUsr]
        public ActionResult NewLeadDb() {
            return View();
        }

        [AuthorizeUsr]
        [HttpPost]
        public string GetLeadCodeList() {
            OleDbDataReader tmp_reader;
            DataTable tmp_table = null;
            _User oUsr = new _User(Session["username"].ToString());

            string areas = "", region = "";
            int asm_index = -1;
            int so_index = -1;
            int csr_index = -1;
            int csm_index = -1;

            asm_index  = oUsr.HasPositionOf("asm");
            so_index  = oUsr.HasPositionOf("so");
            csr_index  = oUsr.HasPositionOf("csr");
            csm_index  = oUsr.HasPositionOf("csm");
            
            if (asm_index > -1) 
            {
                foreach (string area_name in oUsr.Roles[asm_index].Area)
                {
                    if (areas != "") areas = areas + ",";
                    areas = areas + "'" + area_name + "'";
                }
            }

            if (so_index > -1)
            {
                foreach (string area_name in oUsr.Roles[so_index].Area)
                {
                    if (areas != "") areas = areas + ",";
                    areas = areas + "'" + area_name + "'";
                }
            }

            if (csr_index > -1)
            {
                foreach (string region_name in oUsr.Roles[csr_index].Region)
                {
                    if (region != "") region = region + ",";
                    region = region + "'" + region_name + "'";
                }
            }

            if (csm_index > -1)
            {
                foreach (string region_name in oUsr.Roles[csm_index].Region)
                {
                    if (region != "") region = region + ",";
                    region = region + "'" + region_name + "'";
                }
            }

            string to_filter = "";

            if (areas != "")
            {
                to_filter = to_filter + " and c.area in (" + areas + ")";
            }

            if (region != "")
            {
                to_filter = to_filter + " and (case when charindex('LUZON', c.channel) > 0 then 'LUZON' when charindex('VISMIN', c.channel) > 0 then 'VISMIN' else '' end) in (" + region + ")";
            }
            

            string strQ = "";
            strQ = @"
                select a.RequestId, a.sapleadcode from customerleadi a left join 
                SAPSERVER.MATIMCO.dbo.oslp b on a.AssignTo_empId=b.slpcode 
                left join ChannelGroup c on c.area collate SQL_Latin1_General_CP850_CI_AS = (b.u_area + ' - ' + b.u_area) 
                where a.sapleadcode != '' and a.RequestId not in (select requestid from customerLeadDb) " +
                to_filter + @" order by a.sapleadcode ";
            
            try
            {
                tmp_table = SqlDbHelper.getDataDT(strQ);
                return "00:" + StringHelper.ConvertDataTableToString(tmp_table);
            }
            catch (Exception ex)
            {
                return "01:" + ex.Message;
            }
            
        }
        
        [AuthorizeUsr]
        [HttpPost]
        [ParseUrl]
        public string AddNewLead(
                string ldb_requestid,
                string ldb_channel,
                string ldb_sapleadcode,
                string ldb_is_nego_contacted,
                string ldb_nego_date,
                string ldb_nego_contact_person,
                string ldb_nego_contact_number,
                string ldb_is_lost_sales,
                string ldb_ls_date,
                string ldb_reason1,
                string ldb_reason2,
                string ldb_reason3,
                string ldb_reason4,
                string ldb_reason5,
                string ldb_reason6,
                string ldb_reason6_desc,
                string ldb_is_closed,
                string ldb_closed_date,
                string ldb_closed_amount,
                string encodedby
            ) {
                SQLTransaction mt_trans = new SQLTransaction();
                string new_doc_status = "1";
                _User oUsr = new _User(Session["username"].ToString());
                
                /* get current user ID */
                string new_doc_owner = oUsr.EmployeeIdNo;

                if (ldb_is_lost_sales == "1")
                {
                    if (oUsr.HasPositionOf("asm") != -1) 
                    {
                        new_doc_status = AppHelper.GetUserPositionId("asm");
                    }

                    if (oUsr.HasPositionOf("so") != -1)
                    {
                        new_doc_status = AppHelper.GetUserPositionId("so");
                    }
                }
                
                if (ldb_is_closed == "1")
                {
                    new_doc_status = AppHelper.GetUserPositionId("csr");
                }

            try
            {
                mt_trans.StartTransaction();

                ldb_is_nego_contacted = "1";

                mt_trans.InsertTo("customerLeadDb", 
                new Dictionary<string, object>() { 
                    {"RequestId", ldb_requestid}
                    ,{"channel", ldb_channel}
                    ,{"sapleadcode", ldb_sapleadcode}
                    ,{"is_nego_contacted", ldb_is_nego_contacted}
                    ,{"nego_date", ldb_nego_date != "" ? Convert.ToDateTime(ldb_nego_date) : (DateTime?)null }
                    ,{"nego_contact_person", ldb_nego_contact_person != "" ? ldb_nego_contact_person : null }
                    ,{"nego_contact_number", ldb_nego_contact_number != "" ? ldb_nego_contact_number : null }
                    ,{"is_lost_sales", ldb_is_lost_sales != "" ? Convert.ToInt16(ldb_is_lost_sales) : (Int16?)null }
                    ,{"ls_date", ldb_ls_date != "" ? Convert.ToDateTime(ldb_ls_date) : (DateTime?)null }
                    ,{"ls_reason1", ldb_reason1 != "" ? Convert.ToInt16(ldb_reason1) : (Int16?)null }
                    ,{"ls_reason2", ldb_reason2 != "" ? Convert.ToInt16(ldb_reason2) : (Int16?)null }
                    ,{"ls_reason3", ldb_reason3 != "" ? Convert.ToInt16(ldb_reason3) : (Int16?)null }
                    ,{"ls_reason4", ldb_reason4 != "" ? Convert.ToInt16(ldb_reason4) : (Int16?)null }
                    ,{"ls_reason5", ldb_reason5 != "" ? Convert.ToInt16(ldb_reason5) : (Int16?)null }
                    ,{"ls_reason6", ldb_reason6 != "" ? Convert.ToInt16(ldb_reason6) : (Int16?)null }
                    ,{"ls_reason6_desc", ldb_reason6_desc != "" ? ldb_reason6_desc : null }
                    ,{"is_closed", ldb_is_closed != "" ? Convert.ToInt16(ldb_is_closed) : (Int16?)null }
                    ,{"closed_total_amount", ldb_closed_amount != "" ? Convert.ToDecimal(ldb_closed_amount) : (decimal?)null }
                    ,{"closed_date", ldb_closed_date != "" ? Convert.ToDateTime(ldb_closed_date) : (DateTime?)null }
                    ,{"status", new_doc_status }
                    ,{"empIdNo", new_doc_owner }
                    ,{"encodedby", encodedby }
                    ,{"dateEncoded", DateTime.Now }
                });

                mt_trans.Committransaction();

                // make a log
                AppHelper.SaveToLOg("LDB", ldb_requestid, "ADD_NEW_LEADDB", "", Session["username"].ToString(), new_doc_status);

                /*
                if (ldb_is_closed == "1") 
                { 
                    NotifyApprovers(new_doc_status, ldb_requestid);
                }

                if (ldb_is_lost_sales == "1")
                {
                    NotifyApprovers(new_doc_status, ldb_requestid);
                }
                */
                NotifyApprovers(new_doc_status, ldb_requestid);

                
                return "00:" + ldb_requestid;
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;
            }
        }

        public ActionResult LeadDbDetails() 
        {
            return View();
        }

        [AuthorizeUsr]
        [HttpPost]
        public string SaveLeadDb(
                string m_request_id,
                string m_is_qt_submitted,
                string m_txt_qt_submit_date,
                string m_txt_qoute_received_by,
                string m_is_for_followup,
                string m_txt_followup_date,
                string m_rdo_lost_sales,
                string m_txt_ls_date,
                string m_chk_reason1,
                string m_chk_reason2,
                string m_chk_reason3,
                string m_chk_reason4,
                string m_chk_reason5,
                string m_chk_reason6,
                string m_chk_reason6_desc,
                string m_rdo_Closed,
                string m_txt_closed_amount,
                string m_txt_closed_date
            ) {
            SQLTransaction mt_trans = new SQLTransaction();
            
            try
            {
                mt_trans.StartTransaction();

                mt_trans.UpdateTo("customerLeadDb", 
                new Dictionary<string, object>() { 
                    {"nego_qoute_submitted", m_is_qt_submitted }
                    ,{"nego_qoute_date", m_txt_qt_submit_date != "" ? Convert.ToDateTime(m_txt_qt_submit_date) : (DateTime?)null }
                    ,{"nego_qoute_receivedby", m_txt_qoute_received_by }
                    ,{"nego_followup", m_is_for_followup != "" ? Convert.ToInt16(m_is_for_followup) : (Int16?)null }
                    ,{"nego_followup_date", m_txt_followup_date != "" ? Convert.ToDateTime(m_txt_followup_date) : (DateTime?)null }
                    ,{"is_lost_sales", m_rdo_lost_sales != "" ? Convert.ToInt16(m_rdo_lost_sales) : (Int16?)null }
                    ,{"ls_date", m_txt_ls_date != "" ? Convert.ToDateTime(m_txt_ls_date) : (DateTime?)null }
                    ,{"ls_reason1", m_chk_reason1 != "" ? Convert.ToInt16(m_chk_reason1) : (Int16?)null }
                    ,{"ls_reason2", m_chk_reason2 != "" ? Convert.ToInt16(m_chk_reason2) : (Int16?)null }
                    ,{"ls_reason3", m_chk_reason3 != "" ? Convert.ToInt16(m_chk_reason3) : (Int16?)null }
                    ,{"ls_reason4", m_chk_reason4 != "" ? Convert.ToInt16(m_chk_reason4) : (Int16?)null }
                    ,{"ls_reason5", m_chk_reason5 != "" ? Convert.ToInt16(m_chk_reason5) : (Int16?)null }
                    ,{"ls_reason6", m_chk_reason6 != "" ? Convert.ToInt16(m_chk_reason6) : (Int16?)null }
                    ,{"ls_reason6_desc", m_chk_reason6_desc }
                    ,{"is_closed", m_rdo_Closed != "" ? Convert.ToInt16(m_rdo_Closed) : (Int16?)null }
                    ,{"closed_total_amount", m_txt_closed_amount != "" ? Convert.ToDecimal(m_txt_closed_amount) : (decimal?)null }
                    ,{"closed_date", m_txt_closed_date != "" ? Convert.ToDateTime(m_txt_closed_date) : (DateTime?)null }
                }, new Dictionary<string, object>() { 
                    {"requestid", m_request_id}
                });

                if (m_rdo_lost_sales == "1" || m_rdo_Closed == "1")
                {
                    // update status into 1, for CSR
                    mt_trans.UpdateTo("customerLeadDb", 
                        new Dictionary<string, object>() { { "status", "1" } }, 
                        new Dictionary<string, object>() { { "requestid", m_request_id } 
                    });

                }

                mt_trans.Committransaction();

                // send mail to CSR
                
                // log
                AppHelper.SaveToLOg("LDB", m_request_id, "UPDATE_LEADDB", "", Session["username"].ToString().ToUpper());

                return "00:" + m_request_id;
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;
            }
        }

        [AuthorizeUsr]
        [HttpPost]
        public string ConfirmLeadDb(
                string m_txt_po_number,
                string m_txt_so_number,
                string m_txt_est_delivery_date,
                string m_txt_attachment,
                string m_txt_confirmed_by,
                string m_txt_date_confirmed,
                string requestid
            ) {

            SQLTransaction mt_trans = new SQLTransaction();

            try
            {
                mt_trans.StartTransaction();

                mt_trans.UpdateTo("customerLeadDb", 
                new Dictionary<string, object>() { 
                    {"is_conf_encoded", 1 }
                    ,{"conf_po_no", m_txt_po_number }
                    ,{"conf_so_no", m_txt_so_number }
                    ,{"conf_est_delivery_date", m_txt_est_delivery_date != "" ? Convert.ToDateTime(m_txt_est_delivery_date) : (DateTime?)null }
                    ,{"conf_attachments", m_txt_attachment }
                    ,{"conf_confirmed_by", m_txt_confirmed_by }
                    ,{"conf_date_confirmed", m_txt_date_confirmed != "" ? Convert.ToDateTime(m_txt_date_confirmed) : (DateTime?)null }
                }, new Dictionary<string, object>() { 
                    {"requestid", requestid}
                });

                // process attachemnt
                if (m_txt_attachment != "")
                {
                    UploadHelper.ProcessLdbAttachment(requestid, Session["username"].ToString(), m_txt_attachment);
                }
                else 
                {
                    throw new Exception("File not Found!");
                }

                // update status into 1000 (finalized)
                mt_trans.CommandText = "update customerLeadDb set status='1000' where requestid=" + requestid;

                mt_trans.Committransaction();

                // send mail to ASM/SO (docOwner)

                // log
                AppHelper.SaveToLOg("LDB", requestid, "CONFIRM_LEADDB", "", Session["username"].ToString().ToUpper());

                return "00:" + requestid;
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;
            }

        }

        private void SendMail(string userpositionid, string requestid, string empidno) 
        {
            DataRowCollection mtable_row = SqlDbHelper.GetData_dr("userHeader", "where empidno=='" + empidno + "'");

            string mail_body = AppHelper.Arms_Url + "?requestid=" + requestid + "&doctype=ldb";

            try 
            {
                foreach (System.Data.DataRow item in mtable_row)
                    MailHelper.SendMail("noreply@matimco.com", item["emailAdd"].ToString(), "", mail_body);                
            }
            catch(Exception ex) { }
        }

        // 
        private int NotifyCSR(string docid) 
        {
            _Document oDocument = new _Document("LDB", docid);
            string strQ = "", mail_body = "", mail_subject = "";

            // get ALL CSR UNDER A DOCUMENTS REGION
            strQ = @"
                select a.email from apprvrDesig a, userheader b , apprvrRole c 
                where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in ('csr', 'CSR') and 
                left(a.branch,1) = '" + oDocument.Region.Substring(0, 1) + @"' group by a.email";

            return 0;
        }

        private void NotifyApprovers(string new_docstatus, string docid) 
        {
            _Document oDocument = new _Document("LDB", docid);
            string mail_subject = "", mail_body = "", str_docstatus = "";

            string strQ = @"
                select a.*, case when b.ccanum is null then 'false' else 'true' end as 'reclassed' 
                from customerLeadDb a left join customerLeadI b on a.requestid=b.requestid where a.requestid='" + docid + "'";

            DataTable customerLeadDb = SqlDbHelper.getDataDT(strQ);
            foreach (DataRow item in customerLeadDb.Rows) 
            {
                str_docstatus = 
                AppHelper.GetLdbDocStatMsg(
                    item["is_nego_contacted"].ToString(),
                    item["nego_qoute_submitted"].ToString(),
                    item["nego_followup"].ToString(),
                    item["is_lost_sales"].ToString(),
                    item["is_closed"].ToString(),
                    item["is_conf_encoded"].ToString()
                );
            }

            mail_subject = "Active Lead (" + docid + ") is " + str_docstatus;
            mail_body = "To view the details, please click this link --> " + AppHelper.Arms_Url + "?id=" + docid + "&doctype=ldb";

            DataTable emails = GetDestEmails(new_docstatus, oDocument.Region, oDocument.Channel, oDocument.Area);

            try 
            {
                foreach (DataRow itm in emails.Rows) 
                {
                    MailHelper.SendMail("ARMS@matimco.com", itm["email"].ToString().Trim(), mail_subject, mail_body);
                }
            }
            catch (Exception ex) 
            {
                string err = ex.Message;
            }
        }

        private DataTable GetDestEmails(string new_docstatus, string region = "", string channel = "", string area = "") 
        {
            DataTable em_list = null;
            string strQuery = "";

            IList<string> RegionUsers = new List<string>();
            RegionUsers.Add("csr");
            RegionUsers.Add("CSR");
            RegionUsers.Add("fnm");
            RegionUsers.Add("FNM");
            RegionUsers.Add("cnc");
            RegionUsers.Add("CNC");
            RegionUsers.Add("Finance Mgr.");

            IList<string> ChannelUsers = new List<string>();
            ChannelUsers.Add("chm");
            ChannelUsers.Add("CHM");

            IList<string> AreaUsers = new List<string>();
            AreaUsers.Add("asm");
            AreaUsers.Add("ASM");

            IList<string> NolFilterUsers = new List<string>();
            NolFilterUsers.Add("vpbsm");
            NolFilterUsers.Add("VPBSM");
            NolFilterUsers.Add("vptfi");
            NolFilterUsers.Add("VPTFI");

            // REGION
            if (RegionUsers.IndexOf(AppHelper.GetUserPosition(new_docstatus)) != -1)
            {
                string poss = "";
                if (AppHelper.GetUserPosition(new_docstatus) == "csr") poss = "'csr','CSR'";

                if (AppHelper.GetUserPosition(new_docstatus) == "cnc") poss = "'cnc','CNC'";

                if (AppHelper.GetUserPosition(new_docstatus) == "fnm") poss = "'fnm','FNM','Finance Mgr.'";

                strQuery = strQuery + @"
                    select a.email from apprvrDesig a, userheader b , apprvrRole c 
                    where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + @") and 
                    left(a.branch,1) = '" + region.Substring(0, 1) + @"' group by a.email";

                em_list = SqlDbHelper.getDataDT(strQuery);
            }

            // CHANNEL
            if (ChannelUsers.IndexOf(AppHelper.GetUserPosition(new_docstatus)) != -1)
            {
                string poss = "";
                if (AppHelper.GetUserPosition(new_docstatus) == "chm") poss = "'chm','CHM'";

                strQuery = strQuery + @"
                    select a.email from apprvrDesig a, userheader b , apprvrRole c 
                    where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + @") and 
                    a.channel = '" + channel + "' group by a.email";

                em_list = SqlDbHelper.getDataDT(strQuery);
            }

            // AREA
            if (AreaUsers.IndexOf(AppHelper.GetUserPosition(new_docstatus)) != -1)
            {
                string poss = "";
                if (AppHelper.GetUserPosition(new_docstatus) == "asm") poss = "'asm','ASM'";

                strQuery = strQuery + @"
                    select a.email from apprvrDesig a, userheader b , apprvrRole c 
                    where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + @") and 
                    a.area = '" + area + @"' group by a.email";

                em_list = SqlDbHelper.getDataDT(strQuery);
            }

            return em_list;
        }

    }
}
