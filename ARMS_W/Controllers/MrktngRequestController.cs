using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using System.IO;
using System.Data;
using ARMS_W.Class;
using ARMS_W.GLOBALS;

namespace ARMS_W.Controllers
{
    public class MrktngRequestController : Controller
    {
        //
        // GET: /MrktngRequest/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult mrktRequestCreate()
        {
            return View();
        }

        public ActionResult mrktRequest()
        {
            return View();           
        }
        //
        // GET: /MrktngRequest/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MrktngRequest/Create

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult listOfMarketingRequest()
        {

            return View();
        }

        public ActionResult marketingReqDetails()
        {
            return View();         
        }
        //
        // POST: /MrktngRequest/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /MrktngRequest/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MrktngRequest/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MrktngRequest/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MrktngRequest/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public string insertMrktngRequest(
                            string encodedBy,
                            //string mrktReqId,
                           
                            string acctCode,
                            string acctName,
                            string acctAdd,
                            string acctArea,
                            string acctOfficer,
                            string requestedBy,
                            string brand,
                            string category,
                            string type,
                            string setUpDate,
                            string size,
                            string value,
                            string availDeploy,
                            string actualDeploy,
                            string otherStipulation,
                            string attchmnts1,
                            string attchmnts2,
                            string attchmnts3,
                            string Attachment
           ){
                SQLTransaction mt_trans = new SQLTransaction();
                routingController route_trans = new routingController();
                string mrktReqId = "";
                string branch = "";
                string ccanum = "";
                OleDbDataReader greader = SqlDbHelper.getData("SELECT dbo.MTC_fn_generateMrktReqNo()");
               
                if (greader.Read())
                {
                    mrktReqId = greader.GetValue(0).ToString();
                }

                OleDbDataReader greader1 = SqlDbHelper.getData("select ccanum from customerHeader where acctcode='" + acctCode +"'");

                if (greader1.Read())
                {
                    ccanum = greader1.GetValue(0).ToString();
                }

                setUpDate = setUpDate == "" ? "NULL" : "'" + setUpDate + "'";
                availDeploy = availDeploy == "" ? "NULL" : "'" + availDeploy + "'";
                actualDeploy = actualDeploy == "" ? "NULL" : "'" + actualDeploy + "'";
                
                try
                {
                    mt_trans.StartTransaction();
                    mt_trans.CommandText = SqlQueryHelper.InsertNew_marktingRequest(
                            mrktReqId,
                            ccanum,
                            "-1",
                            encodedBy,
                            "1",
                            acctCode,
                            acctName,
                            acctAdd,
                            acctArea,
                            acctOfficer,
                            requestedBy,
                            brand,
                            category,
                            setUpDate,
                            size,
                            value,
                            availDeploy,
                            actualDeploy,
                            "'" + DateTime.Now.ToString("MM/dd/yyyy").ToString() + "'"
                        );

                    //ATTACHMENT
                    // mt_trans.StartTransaction();
                    //mt_trans.CommandText = SqlQueryHelper.InsertNewMarktingReqAttach2(attchmnts1, mrktReqId);
                   // mt_trans.CommandText = SqlQueryHelper.InsertNewMarktingReqAttach2(attchmnts2, mrktReqId);
                   // mt_trans.CommandText = SqlQueryHelper.InsertNewMarktingReqAttach3(attchmnts3, mrktReqId);

                    if (Attachment.Length > 0)
                    {
                        foreach (string strn in StringHelper.GetRows(Attachment))
                        {
                            string[] tmp_columns = strn.Split('|');
                            mt_trans.CommandText = SqlQueryHelper.InsertNew_AttachmentMRKTR(
                               mrktReqId,
                                tmp_columns[0]

                              );
                        }
                    }





                    if (otherStipulation.Length > 0)
                    {
                        foreach (string strn in StringHelper.GetRows(otherStipulation))
                        {                                                   
                            mt_trans.CommandText = SqlQueryHelper.InsertMrktngReqDtls(mrktReqId,strn);                              
                        }
                    }

                    if (acctCode.IndexOf("L") > -1)
                    {
                        branch = "Luzon";
                    }
                    else
                    {
                        branch = "Vismin";
                    }

                    mt_trans.Committransaction();
                    acctCode = "'" + acctCode + "'";
                    route_trans.routeNext(7, mrktReqId, AppHelper.Arms_Url + "?id=" + mrktReqId + "&doctype=mkr", branch, true, acctCode);

                    return SActionResult.Success + mrktReqId;
                }
                catch (Exception ex)
                {
                mt_trans.RollbackTransaction();
                return SActionResult.Error + ex.Message;
                }
        
        }

        public string CallRouting(string action_type, string reqID, string val_accCode)
        {
            string branch = "";

            OleDbDataReader g_reader;
            g_reader = SqlDbHelper.getData("SELECT acctCode FROM  marktingRequest WHERE reqID='" + Convert.ToString(reqID) + "'");
            //branch = Convert.ToString(region.Rows[0]["acctCode"]);

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
                val_accCode = "'" + val_accCode + "'";
                if ( action_type == "Approve")
                {
                    int val = 1;
                    bool isApprove = Convert.ToBoolean(val);
                    routingController eroute = new routingController();
                    eroute.routeNext(7, reqID, AppHelper.Arms_Url + "?id=" + reqID + "&doctype=mkr", branch, isApprove, val_accCode);
                    return "00:";
                }
                else
                {
                    int val2 = 0;

                    bool isApprove = Convert.ToBoolean(val2);
                    routingController eroute = new routingController();
                    eroute.routeNext(7, reqID, AppHelper.Arms_Url + "?id=" + reqID + "&doctype=mkr", branch, isApprove, val_accCode);

                    return "00:";
                }
            }
            catch (Exception ex)
            {
                return "01:" + ex.Message;
            }
        }

        [AuthorizeUsr]
        [HttpPost]
        public string GetFilteredList(string _str_data, string par1 = "", string par2 = "", string tArea = "", string array1 = "") 
        {
            OleDbDataReader tmp_reader = null;
            string strquery = "", str_region = "", str_channel = "", str_area = "";

            _User oUsr = new _User(HttpContext.Session["username"].ToString());

            foreach (_Roles rls in oUsr.Roles)
            {
                foreach (string region_name in rls.Region)
                {
                    if (str_region != "") str_region = str_region + ",";
                    str_region = "'" + region_name + "'";
                }

                foreach (string channel_name in rls.Channel)
                {
                    if (str_channel != "") str_channel = str_channel + ",";
                    str_channel = "'" + channel_name + "'";
                }

                foreach (string area_name in rls.Area)
                {
                    if (str_area != "") str_area = str_area + ",";
                    str_area = "'" + area_name + "'";
                }
            }

            try
            {
                if (_str_data == "ListOfCodeAndName")
                {
                    strquery = "" +
                        "SELECT a.acctcode, a.acctName, a.acctOffcr, a.area, b.area, a.bussAdd " +
                        "FROM customerHeader a left join ChannelGroup b on a.area=b.area where " +
                        "(case when charindex('LUZON',b.channel) > 0 then 'LUZON' when charindex('VISMIN',b.channel) > 0 then 'VISMIN' else '' end) in (" + str_region + ") " +
                        "";
                }

                if (_str_data == "ListOfSo")
                {
                    strquery = "" +
                        "select a.slpcode, a.slpname from " +
                        "SAPSERVER.MATIMCO.dbo.OSLP a inner join SAPSERVER.MATIMCO.dbo.oter z on a.u_area=left(z.descript,5) " +
                        "inner join SAPSERVER.MATIMCO.dbo.oter x on z.parent=x.territryid left join ChannelGroup y on " +
                        "y.area collate SQL_Latin1_General_CP850_CI_AS=z.descript " +
                        "where left(z.descript,2) = 'AR' and a.u_active='Y' and a.u_position='AO' " +
                        "and (case when charindex('LUZON',x.descript) > 0 then 'LUZON' when charindex('VISMIN',x.descript) > 0 then 'VISMIN' else '' end) in (" + str_region + ")" +
                        "";
                    
                }

                if (_str_data == "ListofMarketingBrand")
                {
                    strquery = "SELECT FldValue, UPPER(Descr) FROM SAPSERVER.MATIMCO.dbo.ufd1 WHERE TableId='OITM' AND FieldId='23' and FldValue NOT IN('XX','AD','MH','GM')UNION ALL select 'ALL BRANDS', 'ALL BRANDS'";
                    //strquery = "SELECT DISTINCT U_Brand FROM SAPSERVER.MATIMCO.dbo.OITM WHERE u_brand NOT IN('','XX') UNION ALL select 'ALL BRANDS' ";
                }

                if (_str_data == "ListOfCategoryType")
                {
                    strquery = "SELECT categoryType FROM marketingReqCategory";
                }

                tmp_reader = SqlDbHelper.getData(strquery);
                return "00:" + StringHelper.ConvertReaderToString(tmp_reader);
            }
            catch (Exception ex)
            {
                return "01:" + ex.Message;
            }
        }


    }
}

