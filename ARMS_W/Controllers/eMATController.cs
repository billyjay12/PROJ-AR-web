using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARMS_W.Class;
using ARMS_W.GLOBALS;
using System.Data.OleDb;
using System.IO;
using System.Data;
using ARMS_W.SkelClass;


namespace ARMS_W.Controllers
{
    public class eMATController : Controller
    {
        
        // GET: /eMAT/
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult MyPage()
        {
            return View();
        }

        // GET: /eMAT/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: list of emat
        public ActionResult ListOfEMATS()
        {
            return View();
        }

        public ActionResult eMATDetails() {
            return View();
        }
        
        // GET: /eMAT/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /eMAT/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public string ConfirmEmatTrans( SkelClass.EMAT.emat_confirmemattrans p_param)
        {
            SQLTransaction mt_trans = new SQLTransaction();
            routingController route_trans = new routingController();
            string eMATno = "";
            
            DataTable gtable = SqlDbHelper.getDataDT(SqlQueryHelper.GenerateEMATno());
            
            foreach (DataRow itm in gtable.Rows) 
            {
                eMATno = itm[0].ToString();
            }

            try
            {
                mt_trans.StartTransaction();

                mt_trans.InsertTo("emat", new Dictionary<string, object>() { 
                    {"eMATno", Convert.ToInt64(eMATno) }
                    ,{"status", "1" }
                    ,{"encodedBy", p_param.encodedBy }
                    ,{"buyer", p_param.buyer }
                    ,{"buyerAdd", p_param.buyerAdd }
                    ,{"telNo", p_param.telNo }
                    ,{"contactPerson", p_param.contactPerson }
                    ,{"terms", p_param.terms }
                    ,{"deliveryDate", Convert.ToDateTime(p_param.deliveryDate) }
                    ,{"deliveryInstrct", p_param.deliveryInstrct }
                    ,{"acctCode", p_param.acctCode }
                    ,{"submttdTo", p_param.submttdTo }
                    ,{"submttdContactNum", p_param.submttdContactNum }
                    ,{"tradeSalesRep", p_param.tradeSalesRep }
                    ,{"confirmedDelBy", p_param.confirmedDelBy }
                    ,{"ematDoc", p_param.ematDoc }
                });

                if (p_param.product.Count > 0)
                {
                    foreach (string strn in p_param.product)
                    {
                        string[] tmp_columns = strn.Split('|');
                        mt_trans.InsertTo("eMATdtls", new Dictionary<string, object>() { 
                            {"products", tmp_columns[1]}
                            ,{"price", Convert.ToDouble(tmp_columns[2]) }
                            ,{"qty", Convert.ToInt32(tmp_columns[4]) }
                            ,{"total", Convert.ToDouble( tmp_columns[5]) }
                            ,{"eMATno", Convert.ToInt32(eMATno) }
                            ,{"ItemCode",  tmp_columns[0] }
                            ,{"unitOfMeasure", tmp_columns[3] }
                        });
                    }
                }

                if (p_param.acctCode.IndexOf("CL") > -1)
                {
                    p_param.branch = "Luzon";
                }
                else
                {
                    p_param.branch = "Vismin";
                }

                mt_trans.Committransaction();

                AppHelper.SaveToLOg("EMAT", eMATno, "CREATE_NEW", "", Session["username"].ToString(), "1", "0");

                try 
                {
                    sendMail(eMATno, p_param.acctCode);
                }
                catch (Exception ex) { }

                return SActionResult.Success + eMATno;
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return SActionResult.Error + ex.Message;
            }
        }

        public string sendMail(string docNum, string acctCode) { 
        
        routingController eroute = new routingController();
            try
            {
                string mailserver = "mail2.matimco.com";
                string docLink = AppHelper.Arms_Url + "?id=" + docNum + "&doctype=ematlink";
                string email = getCSEmail(acctCode);
                eroute.sendEmailToNxtEMATApprvr(docLink, docNum, email, mailserver);

                return "00:";
            }
            catch (Exception ex)
            {
               return "01" + ex.Message;
            }
        }

         public string getCSEmail(string acctcode) {
            //routingController route = new routingController();
            string email = "";
            
             try{
                if (acctcode.IndexOf("CL") > -1)
                {
                    //branch = "LZ";
                    string strQuery1 = "SELECT email FROM apprvrDesig WHERE roleID = '4' AND branch = 'LZ'";
                    OleDbDataReader tmpreader = null;      
                    tmpreader = SqlDbHelper.getData(strQuery1);
                    if (tmpreader.Read())
                    {
                        email = tmpreader.GetValue(0).ToString();
                    }
                }
                else
                {
                    //branch = "VM";
                    string strQuery2 = "SELECT email FROM apprvrDesig WHERE roleID = '4' AND branch = 'VM'";
                    OleDbDataReader tmpreader;      
                    tmpreader = SqlDbHelper.getData(strQuery2);

                    if (tmpreader.Read())
                    {
                        email = tmpreader.GetValue(0).ToString();   
                    }
                }
            } 
            catch (Exception ex)
            {
                return "01" + ex.Message;
            }

            return email;
        }

        //routing
        public string CallRouting(string val_action_type, string val_emat, string val_accCode)
        {
            string branch = "";
            string docLink = AppHelper.Arms_Url + "?id=" + val_emat;

            OleDbDataReader g_reader;
            g_reader = SqlDbHelper.getData("SELECT acctCode from dbo.eMAT where eMATno='" + Convert.ToString(val_emat) + "'");

            if (g_reader.Read())
            {
                branch = g_reader.GetValue(0).ToString();
            }
            
            if (branch.IndexOf("CL") > -1)
            {
                branch = "Luzon";
            }
            else
            {
                branch = "Vismin";
            }

            try
            {
                if (val_action_type == "Approve")
                {
                    int val = 1;
                    bool isApprove= Convert.ToBoolean(val);
                    routingController eroute = new routingController();
                    eroute.routeNext(9, val_emat, docLink, branch, isApprove,val_accCode);

                    // another log

                    return "00:";
                }
                else
                {
                    int val2 = 0;

                    bool isApprove = Convert.ToBoolean(val2);
                    routingController eroute = new routingController();
                    eroute.routeNext(9, val_emat, docLink, branch, isApprove, val_accCode);

                    // another log

                    return "00:";
                }

            }
            catch (Exception ex)
            {
                return "01:" + ex.Message;
            }
        }

        public string GenerateEMATno(string newEMATno)
        {
            try
            {
                string eMATno = "";
                OleDbDataReader _greader = SqlDbHelper.getData(SqlQueryHelper.GenerateEMATno());
                if (_greader.Read())
                {
                    eMATno = _greader.GetValue(0).ToString();
                    newEMATno = eMATno;

                    return newEMATno;
                }
                else
                {
                    throw new FormatException("eMAT Number not available.");
                }
            }
            catch(Exception ex)
			{
				return "01:" + ex.Message;
			}
        }

        /*-------------------------Approve-------------------------------------------*/
        // update if Approved
        public string ApprovedEmat( string val_emat, string encodedBy )
        {
            SQLTransaction mt_trans = new SQLTransaction();

            try
            {
                mt_trans.StartTransaction();
                mt_trans.CommandText = SqlQueryHelper.Update_eMATstatus("3", val_emat);
                mt_trans.Committransaction();

                sendMailApp(val_emat, encodedBy);
                /*
                SqlDbHelper.ExecNQuery("INSERT INTO document_history " +
                                    " (DocType,docId,mType,Remarks,mDate,Creator_Pos,Creator_id,Creator_Uname,DocStatus,PrevDocStatus,TAG,TAG1) " +
                                    " SELECT 'EMAT','" + Convert.ToInt64(val_emat) + "','APPROVED' ,'','" + DateTime.Now.ToString() + "' " +
                                    ",'',(SELECT empidNo FROM userHeader WHERE username = '" + Session["username"].ToString() + "'),'" + Session["username"].ToString() + "',3,'1',APPROVE,''");
                */
                AppHelper.SaveToLOg("EMAT", val_emat, "APPROVE", "", Session["username"].ToString(), "3", "2");
                return "00:";
            }
            catch (Exception ex) {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;
            }
        }
        // end of update if Approved

        // Update if disApproved
        public string disApprovedEmat( string val_emat, string approved, string encodedBy )
        {
            SQLTransaction mt_trans = new SQLTransaction();

            try
            {
                mt_trans.StartTransaction();
                mt_trans.CommandText = SqlQueryHelper.Update_eMATstatus(approved, val_emat);
                mt_trans.Committransaction();

                sendMaildisApp(val_emat, encodedBy);
                /*
                SqlDbHelper.ExecNQuery("INSERT INTO document_history " +
                                    " (DocType,docId,mType,Remarks,mDate,Creator_Pos,Creator_id,Creator_Uname,DocStatus,PrevDocStatus,TAG,TAG1) " +
                                    " SELECT 'EMAT','" + Convert.ToInt64(val_emat) + "','DISAPPROVED' ,'','" + DateTime.Now.ToString() + "' " +
                                    ",'',(SELECT empidNo FROM userHeader WHERE username = '" + Session["username"].ToString() + "'),'" + Session["username"].ToString() + "',2,'1',DISAPPROVE,''");
                */
                AppHelper.SaveToLOg("EMAT", val_emat, "DISAPPROVE", "", Session["username"].ToString(), "3", "2");
                return "00:";
            }
            catch (Exception ex)
            {

                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;
            }
        }
        //End if disApproved

        // SEND MAIL AS APPROVED
        public string sendMailApp(string docNum, string encodedBy)
        {
            //string encodedBy = "";
            routingController eroute = new routingController();
            try
            {
              
                string title = "E-MAT Doc. No. " + docNum + " was approved.";
                string mailserver = "mail2.matimco.com";
                string docLink = AppHelper.Arms_Url + "?id=" + docNum + "&doctype=ematlink";
                string email = getCSREmail(encodedBy);
                eroute.sendEmail(docLink, title, email, mailserver);

                return "00:";
            }
            catch (Exception ex)
            {
                return "01" + ex.Message;
            }
        }

        public string sendMaildisApp(string docNum, string encodedBy)
        {
            //string encodedBy = "";
            routingController eroute = new routingController();
            try
            {
                string title = "E-MAT Doc. No. " + docNum + " was disapproved.";
                string mailserver = "mail2.matimco.com";
                string docLink = AppHelper.Arms_Url + "?id=" + docNum + "&doctype=ematlink";
                string email = getCSREmail(encodedBy);
                eroute.sendEmail(docLink, title, email, mailserver);

                return "00:";
            }
            catch (Exception ex)
            {
                return "01" + ex.Message;
            }
         }
        //END SEND MAIL AS APPROVED

        //GET CSR EMAIL
        public string getCSREmail(string encodedBy)
        {
            //routingController route = new routingController();
            string email = "";

            try
            {

                string strQuery1 = "SELECT emailAdd from userHeader where userName='" + encodedBy + "'";
                OleDbDataReader tmpreader = null;
                tmpreader = SqlDbHelper.getData(strQuery1);
                if (tmpreader.Read())
                {
                    email = tmpreader.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                return "01" + ex.Message;
            }
            return email;
        }
        //END GET CSR EMAIL

        public string GetList(string _str_data = "", string keyword1 = "") 
        {
            string Regions = "";
            string strquery = "";
            _User Ousr = new _User(Session["username"].ToString());

            foreach (_Roles rls in Ousr.Roles) 
            {
                foreach (string rgn_names in rls.Region) 
                {
                    if (Regions != "") 
                    {
                        Regions = Regions + ",";
                    }

                    Regions = Regions + "'" + rgn_names + "'";
                }
            }

            if (_str_data == "ListOfINDirAcccod")
            {
                strquery = "select '', acctName, bussAdd, telNum from dbo.customerHeader where acctType='indirect' and status = '1000' and region in (" + Regions + ")";
            }

            if (_str_data == "ListOfDirAcccod")
            {
                strquery = "select '', acctCode from dbo.customerHeader where acctType='direct' and status = '1000' and region in (" + Regions + ")";
            }

            if (_str_data == "ListOfitemCode")
            {
                strquery = "SELECT '', itemcode from arms_vw_itemcode";
            }

            if (_str_data == "ListOfunit")
            {
                strquery = " SELECT '', uom FROM unitOfMeasure ";
            }

            if (_str_data == "ListOfPrice") 
            {
                strquery = "select u_pl_name, u_pl_itemcode, cast(u_pl_price as decimal (18,2)) from SAPSERVER.MATIMCO.dbo.[@MTC_PRICELIST] where u_pl_itemcode = '" + keyword1 + "' order by code";
            }

            try
            {
                DataTable tmp_table = SqlDbHelper.getDataDT(strquery);
                return "00:" + StringHelper.ConvertDataTableToString(tmp_table);
            }
            catch (Exception ex)
            {
                return "01:" + ex.Message;
            }
        }

        public string GetJList(string _str_data = "", string keyword = "") 
        {
            string strquery = "";

            if (_str_data == "ListOfitemCode") strquery = "SELECT top 10 itemcode, itemname from arms_vw_itemcode where itemcode like '" + keyword + "%'";

            DataTable tmp_table = SqlDbHelper.getDataDT(strquery);

            return StringHelper.ConvertDataTableToString(tmp_table);
        }

        public string IsEmatExist(string proposed_emat_doc) 
        {
            DataTable etable = SqlDbHelper.getDataDT("select * from emat where ematDoc = '" + proposed_emat_doc + "'");
            foreach (DataRow itm in etable.Rows) 
            {
                return SActionResult.Success + proposed_emat_doc + " IS NOT AVAILABLE.";
            }

            return SActionResult.Error + proposed_emat_doc + " IS AVAILABLE.";
        }

        [HttpPost]
        public JsonResult GetDetails(string eMATno)
        {
            DataTable emat_info = null;
            DataTable items = null;
            EMAT.ematContents emcnts = new EMAT.ematContents();
            
            emat_info = SqlDbHelper.getDataDT("select *, convert(varchar(10), deliveryDate, 101) as 'deliveryDateFormatted',  convert(varchar(10), confirmedDelBy, 101) as 'confirmedDelByFormatted' from dbo.eMAT where eMATno='" + eMATno + "'");
            items = SqlDbHelper.getDataDT("select * from dbo.eMATdtls where ematno='" + eMATno + "'");

            foreach (DataRow itm in emat_info.Rows)
            {
                emcnts.encodedBy = itm["encodedBy"].ToString();
                emcnts.buyer = itm["buyer"].ToString();
                emcnts.buyerAdd = itm["buyerAdd"].ToString();
                emcnts.telNo = itm["telNo"].ToString();
                emcnts.contactPerson = itm["contactPerson"].ToString();
                emcnts.terms = itm["terms"].ToString();
                emcnts.deliveryDateFormatted = itm["deliveryDateFormatted"].ToString();
                emcnts.deliveryInstrct = itm["deliveryInstrct"].ToString();
                emcnts.acctCode = itm["acctCode"].ToString();
                emcnts.submttdTo = itm["submttdTo"].ToString();
                emcnts.submttdContactNum = itm["submttdContactNum"].ToString();
                emcnts.tradeSalesRep = itm["tradeSalesRep"].ToString();
                emcnts.confirmedDelBy = itm["confirmedDelBy"].ToString();
                emcnts.ematDoc = itm["ematDoc"].ToString();
                emcnts.DocStatusMsg = AppHelper.GetEMATDocStateMsg( itm["status"].ToString() );
            }

            emcnts.list_of_items = new string[items.Rows.Count];
            int i = 0;
            foreach (DataRow itm in items.Rows)
            {
                
                emcnts.list_of_items[i] =
                    itm["ItemCode"].ToString() + "|" + itm["products"].ToString() + "|" + itm["price"].ToString() + "|" +
                    itm["unitOfMeasure"].ToString() + "|" + itm["qty"].ToString() + "|" + itm["total"].ToString();
                i++;
            }

            return Json(emcnts);
        }

        
    }
}
        




