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
    public class MMAgreementController : Controller
    {
        //
        // GET: /MMAgreement/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateMMAgreement() 
        {
            return View();
        }

        public ActionResult MMAgreementDetails() 
        {
            return View();
        }

        [AuthorizeUsr]
        [HttpPost]
        public string AddNewMeetingsAndAgreements(SkelClass.page_param.mmagreement_addnewmeetingsandagreements page_param)
        {
            SQLTransaction mt_trans = new SQLTransaction();
            string new_agreement_num = "", ccanum = "";

            // OleDbDataReader _greader = SqlDbHelper.getData("select isnull((select max(agreeNo) + 1 from mtgMinutesAgreement),'1')");
            DataTable _gtable = SqlDbHelper.getDataDT("select isnull((select max(agreeNo) + 1 from mtgMinutesAgreement),'1')");
            
            foreach (DataRow itm in _gtable.Rows) { new_agreement_num = itm[0].ToString(); }

            _gtable = SqlDbHelper.getDataDT("select ccanum from customerheader where case when sapacctcode is null then acctcode else sapacctcode end = '" + page_param.acct_code + "'");
            foreach (DataRow itm in _gtable.Rows) { ccanum = itm[0].ToString(); }

            try
            {
                mt_trans.StartTransaction();

                // mtgMinutesAgreement
                mt_trans.InsertTo("mtgMinutesAgreement", new Dictionary<string, object>() { 
                    {"agreeNo", Convert.ToInt64(new_agreement_num) }
                    , {"ccaNum", ccanum }
                    , {"acctCode", page_param.acct_code }
                    , {"acctName", page_param.acct_name }
                    , {"mtgType", page_param.meeting_type }
                    , {"mtgDate", page_param.meeting_date }
                    , {"mtgObjective", page_param.meeting_objective }
                    , {"preparedBy", page_param.meeting_prepared_by }
                    , {"status", AppHelper.GetUserPositionId("so") }
                });
                
                // mtgAttendees
                foreach(string attendee in page_param.list_of_attendees)
                {
                    mt_trans.InsertTo("mtgAttendees", new Dictionary<string, object>() { 
                        {"agreeNo", Convert.ToInt64(new_agreement_num)}
                        , {"attendName", attendee}
                    });
                }

                if (UploadHelper.ProcessMmaAttachments(new_agreement_num, Session["username"].ToString()) == true)
                {
                    foreach (string attachment in page_param.meeting_signed_minutes) 
                    {
                        string[] tmp_str = attachment.Split('|');
                        mt_trans.InsertTo("mtgAttachment", new Dictionary<string, object>() { 
                            {"fileID", 0 }
                            , {"agreeNo", Convert.ToInt64(new_agreement_num) }
                            , {"filePath", tmp_str[0] }
                            , {"dscription", tmp_str[1] }
                        });
                    }
                }
                else
                {
                    throw new Exception("Error copying files.");
                }

                // mtgActionItems
                foreach (string action in page_param.list_of_actions) 
                {
                    string[] tmp_str = action.Split('|');
                    mt_trans.InsertTo("mtgActionItems", new Dictionary<string, object>() { 
                        {"agreeNo", Convert.ToInt64(new_agreement_num) }
                        , {"actionItm", tmp_str[0] }
                        , {"proposedTime", tmp_str[1] }
                        , {"status", tmp_str[2] }
                    });
                }
                
                mt_trans.Committransaction();

                // mark current documents
                try { MarkMMaDocument(new_agreement_num, "APPROVE"); }
                catch (Exception exs) { }

                return "00:" + new_agreement_num;
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;
            }
        }

        [AuthorizeUsr]
        [HttpPost]
        public string MarkMMaDocument(string mma_num, string action_type)
        {
            SQLTransaction mt_trans = new SQLTransaction();
            string cur_docstatus = "", new_docstatus = "";
            string acct_code = "", grp_name = "";

            DataTable mtgMinutesAgreement = SqlDbHelper.getDataDT("select * from mtgMinutesAgreement where agreeno='" + mma_num + "'");
            foreach (DataRow item in mtgMinutesAgreement.Rows)
            {
                cur_docstatus = item["status"].ToString().Trim();
                acct_code = item["acctCode"].ToString().Trim();
            }

            // channelGroup
            DataTable channelGroup = SqlDbHelper.getDataDT("Select b.* from Customerheader a inner join ChannelGroup b ON a.area=b.area where acctcode='" + acct_code + "'");
            foreach (DataRow itm in channelGroup.Rows)
            {
                grp_name = itm["grp_name"].ToString().Trim();
            }

            try
            {
                mt_trans.StartTransaction();

                if (action_type == "APPROVE")
                {
                    new_docstatus = AppHelper.GetMMANextStep(cur_docstatus);

                    //SKIP ASM due to re organization of sales. KAO,KAM,RSM
                    if ((grp_name=="GTL" || grp_name=="GTV") && new_docstatus == AppHelper.GetUserPositionId("asm"))
                    {
                        new_docstatus = AppHelper.GetUserPositionId("chm");
                    }

                    mt_trans.CommandText = "Update mtgMinutesAgreement set status='" + new_docstatus + "' where agreeno=" + mma_num + "";

                    //mt_trans.CommandText = "Update mtgMinutesAgreement set status='" + AppHelper.GetMMANextStep(cur_docstatus) + "' where agreeno=" + mma_num + "";
                    //new_docstatus = AppHelper.GetMMANextStep(cur_docstatus);
                }
                else if (action_type == "DISAPPROVE")
                {
                    new_docstatus = "-" + cur_docstatus;
                    mt_trans.CommandText = "Update mtgMinutesAgreement set status='" + new_docstatus + "' where agreeno=" + mma_num + "";
                }
                else
                {
                    throw new Exception("Action Type not Present (" + action_type + ")");
                }

                mt_trans.Committransaction();

                // send mail
                SendMailMMa(new_docstatus, mma_num);

                return "00:" + mma_num;
            }
            catch (Exception ex)
            {
                return "01:" + ex.Message;
            }
        }

        private void SendMailMMa(string dest_id, string agree_no)
        {
            string doc_region = "", new_dest_id = "", str_subject = "", acct_code = "", acct_name = "", grp_name = "", new_docstatus = "";
            //OleDbDataReader mreader = SqlDbHelper.getData("select case when right(left(acctcode,2),1)='L' then 'Luzon' when right(left(acctcode,2),1)='V' then 'Vismin' end as 'docregion' from mtgMinutesAgreement where agreeno=" + agree_no + "");
            //if (mreader.Read())
            //{
            //    doc_region = mreader.GetValue(0).ToString();
            //}
            DataTable mreader = SqlDbHelper.getDataDT("select case when right(left(a.acctcode,2),1)='L' then 'Luzon' when right(left(a.acctcode,2),1)='V' then 'Vismin' end as 'docregion' , b.acctname, b.acctcode from mtgMinutesAgreement a inner join customerheader b on a.acctcode=b.acctcode where a.agreeno=" + agree_no + "");
            foreach (DataRow itm in mreader.Rows) 
            {
                doc_region = itm["docregion"].ToString().Trim();
                acct_code = itm["acctcode"].ToString().Trim();
                acct_name = itm["acctname"].ToString().Trim();
            }

            // channelGroup
            DataTable channelGroup = SqlDbHelper.getDataDT("Select b.* from Customerheader a inner join ChannelGroup b ON a.area=b.area where acctcode='" + acct_code + "'");
            foreach (DataRow itm in channelGroup.Rows)
            {
                grp_name = itm["grp_name"].ToString().Trim();
            }

            if (dest_id == "1000")
            {
                // approved
                // send to SO
                new_dest_id = AppHelper.GetUserPositionId("so");
                str_subject = "Contracts and Agreements for " + acct_name + "(" + acct_code + ") (" + agree_no + ") has been Approved";
            }
            else
            {
                new_dest_id = dest_id;
                if (grp_name == "GTL" || grp_name == "GTV")
                {
                    new_docstatus = AppHelper.GetMMaDocStatusMessage(new_dest_id);
                    if (new_docstatus == "for Channel Manager")
                    {
                        new_docstatus = "for RSM Approval";
                    }
                }
                str_subject = "Contracts and Agreements for " + acct_name + "(" + acct_code + ") (" + agree_no + ") is waiting for your Approval (" + AppHelper.GetMMaDocStatusMessage(new_dest_id) + ")";
            }

            if (Convert.ToInt32(dest_id) < 0)
            {
                // disapprove, notify only the SO
                new_dest_id = AppHelper.GetUserPositionId("so");
                str_subject = "Contracts and Agreements for " + acct_name + "(" + acct_code + ") (" + agree_no + ") has been Disapproved";
            }
            
            DataTable userHeader_email = null;
            // userHeader_email = SqlDbHelper.getDataDT("select emailadd from userHeader where region='" + doc_region + "' and position='" + AppHelper.GetUserPosition(new_dest_id) + "'");
            _Document oDocumnt = new _Document("MMA", agree_no);

            userHeader_email = GetDestEmail(new_dest_id, oDocumnt.Region, oDocumnt.Channel, oDocumnt.Area);

            string strBody = "";
            strBody = "To view the details, please click this link -->  " + AppHelper.Arms_Url + "/?id=" + agree_no + "&doctype=mma";

            try
            {
                foreach (DataRow item in userHeader_email.Rows)
                {
                    MailHelper.SendMail("ARMS@matimco.com", item["email"].ToString(), str_subject, strBody);
                }
            }
            catch (Exception ex)
            {

            }
        }

        [AuthorizeUsr]
        [HttpPost]
        public string SaveMMAgreement(
                string mma_num
            ) {

            try
            {

                return "00:";
            }
            catch (Exception ex)
            {
                return "01:" + ex.Message;
            }
        }

        private DataTable GetDestEmail(string new_docstatus, string region = "", string channel = "", string area = "", string docid = "") 
        {
            string strQuery = "";
            DataTable em_list = null;

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

                strQuery = strQuery + "" +
                    "select a.email from apprvrDesig a, userheader b , apprvrRole c " +
                    "where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + ") and " +
                    "left(a.branch,1) = '" + region.Substring(0, 1) + "' " +
                    "group by a.email" +
                    "";

                em_list = SqlDbHelper.getDataDT(strQuery);
            }

            // CHANNEL
            if (ChannelUsers.IndexOf(AppHelper.GetUserPosition(new_docstatus)) != -1)
            {
                string poss = "";
                if (AppHelper.GetUserPosition(new_docstatus) == "chm") poss = "'chm','CHM'";

                strQuery = strQuery + "" +
                    "select a.email from apprvrDesig a, userheader b , apprvrRole c " +
                    "where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + ") and " +
                    "a.channel = '" + channel + "' " +
                    "group by a.email" +
                    "";

                em_list = SqlDbHelper.getDataDT(strQuery);
            }

            // AREA
            if (AreaUsers.IndexOf(AppHelper.GetUserPosition(new_docstatus)) != -1)
            {
                string poss = "";
                if (AppHelper.GetUserPosition(new_docstatus) == "asm") poss = "'asm','ASM'";

                strQuery = strQuery + "" +
                    "select a.email from apprvrDesig a, userheader b , apprvrRole c " +
                    "where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + ") and " +
                    "a.area = '" + area + "' " +
                    "group by a.email" +
                    "";

                em_list = SqlDbHelper.getDataDT(strQuery);
            }

            // FOR VPBSM, VPTFI
            if (NolFilterUsers.IndexOf(AppHelper.GetUserPosition(new_docstatus)) != -1)
            {
                string poss = "";
                if (AppHelper.GetUserPosition(new_docstatus) == "vpbsm") poss = "'vpbsm','VPBSM'";
                if (AppHelper.GetUserPosition(new_docstatus) == "vptfi") poss = "'vptfi','VPTFI'";

                strQuery = strQuery + "" +
                    "select a.email from apprvrDesig a, userheader b , apprvrRole c " +
                    "where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + ") " +
                    "group by a.email" +
                    "";

                em_list = SqlDbHelper.getDataDT(strQuery);
            }


            return em_list;

        }

    }
}
