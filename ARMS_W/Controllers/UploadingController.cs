using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Data;
using ARMS_W.Class;

namespace ARMS_W.Controllers
{
    public class UploadingController : Controller
    {
        //
        // GET: /Uploading/

        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUsr]
        public ActionResult CcaUploadDialogBox() 
        {
            return View();
        }

        public static bool CcaProcessUploadFiles(string ccaNum, string username) 
        {
            try 
            {
                Directory.CreateDirectory(UploadHelper.CcaUploadDirectory + ccaNum);

                if (Directory.Exists(UploadHelper.CcaUploadDirectory + username + "\\") == false)
                {
                    // create a directory named after a user
                    Directory.CreateDirectory(UploadHelper.CcaUploadDirectory + username);
                }

                // copy all files from [username] folder to [ccaNum] folder
                UploadHelper.CopyAllFiles(UploadHelper.CcaUploadDirectory + username + "\\", UploadHelper.CcaUploadDirectory + ccaNum + "\\");

                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        [AuthorizeUsr]
        public ActionResult CcaUpload() 
        {
            HttpFileCollectionBase File = Request.Files;
            try
            {
                // check extension
                string[] FileExtension = { ".pdf", ".jpeg", ".jpg", ".doc", ".docx", ".hfx" };

                if (Array.IndexOf(FileExtension, Path.GetExtension(File[0].FileName.ToLower())) > -1)
                {
                    Directory.CreateDirectory(UploadHelper.CcaUploadDirectory + Session["username"]);

                    File[0].SaveAs(UploadHelper.CcaUploadDirectory + Session["username"] + "\\" + Path.GetFileName(File[0].FileName));

                    ViewData["fname"] = Path.GetFileName(File[0].FileName); 
                    ViewData["ftype"] = File[0].InputStream.Length;
                    ViewData["fsize"] = File[0].ContentType;
                }
                else
                {
                    throw new System.OperationCanceledException("The file can only be a PDF file");
                }
            }
            catch (Exception ex)
            {
                ViewData["ferror"] = ex.Message;
                ViewData["fname"] = "";
                ViewData["ftype"] = "";
                ViewData["fsize"] = "";
            }

            return View("UploadResult");
        }

        public ActionResult MmaUploadDialogBox() 
        {
            return View();
        }

        public ActionResult MmaUpload() 
        {
            HttpFileCollectionBase File = Request.Files;
            try
            {
                // check extension
                string[] FileExtension = { ".pdf", ".jpeg", ".jpg", ".doc", ".docx" };

                if (Array.IndexOf(FileExtension, Path.GetExtension(File[0].FileName.ToLower())) > -1)
                {
                    Directory.CreateDirectory(UploadHelper.MmaUploadDirectory + Session["username"]);
                    File[0].SaveAs(UploadHelper.MmaUploadDirectory + Session["username"] + "\\" + Path.GetFileName(File[0].FileName));

                    ViewData["fname"] = Path.GetFileName(File[0].FileName);
                    ViewData["ftype"] = File[0].InputStream.Length;
                    ViewData["fsize"] = File[0].ContentType;
                }
                else
                {
                    throw new System.OperationCanceledException("The file can only be a PDF file");
                }
            }
            catch (Exception ex)
            {
                ViewData["ferror"] = ex.Message;
                ViewData["fname"] = "";
                ViewData["ftype"] = "";
                ViewData["fsize"] = "";
            }

            return View("UploadResult");
        }

        public ActionResult UploadResult() 
        {
            return View();
        }

        public ActionResult UploadResultMarketing()
        {
            return View();
        }

        public ActionResult marketingReq()
        {
            HttpFileCollectionBase File = Request.Files;

            try
            {
                // check extension
                if (Path.GetFileName(File[0].FileName).Substring(Path.GetFileName(File[0].FileName).Length - 4, 4) == ".pdf" || Path.GetFileName(File[0].FileName).Substring(Path.GetFileName(File[0].FileName).Length - 4, 4) == ".rpt" || Path.GetFileName(File[0].FileName).Substring(Path.GetFileName(File[0].FileName).Length - 4, 4) == ".doc" || Path.GetFileName(File[0].FileName).Substring(Path.GetFileName(File[0].FileName).Length - 4, 4) == ".docx" || Path.GetFileName(File[0].FileName).Substring(Path.GetFileName(File[0].FileName).Length - 4, 4) == ".xlsx" || Path.GetFileName(File[0].FileName).Substring(Path.GetFileName(File[0].FileName).Length - 4, 4) == ".xls")
                {
                    Directory.CreateDirectory(UploadHelper.MrktngReqDirectory + Session["username"]);
                    File[0].SaveAs(UploadHelper.MrktngReqDirectory + Session["username"] + "\\" + Path.GetFileName(File[0].FileName));

                    ViewData["fname"] = Path.GetFileName(File[0].FileName);
                    ViewData["ftype"] = File[0].InputStream.Length;
                    ViewData["fsize"] = File[0].ContentType;
                }
                else
                {
                    throw new System.OperationCanceledException("The file can only be a PDF file");
                }
            }
            catch (Exception ex)
            {
                ViewData["ferror"] = ex.Message;
                ViewData["fname"] = "";
                ViewData["ftype"] = "";
                ViewData["fsize"] = "";
            }

            return View("UploadResult");
        }

        public ActionResult UploadFileMarketing()
        {
            return View();
        }

        public ActionResult LdbUploadDialogBox() {
            return View();
        }

        public ActionResult LdbUpload() {
            HttpFileCollectionBase File = Request.Files;
            try
            {
                // check extension
                string[] FileExtension = { ".pdf", ".jpeg", ".jpg", ".doc", ".docx" };

                if (Array.IndexOf(FileExtension, Path.GetExtension(File[0].FileName.ToLower())) > -1)
                {
                    Directory.CreateDirectory(UploadHelper.LdbUploadDirectory + Session["username"]);
                    File[0].SaveAs(UploadHelper.LdbUploadDirectory + Session["username"] + "\\" + Path.GetFileName(File[0].FileName));

                    ViewData["fname"] = Path.GetFileName(File[0].FileName);
                    ViewData["ftype"] = File[0].InputStream.Length;
                    ViewData["fsize"] = File[0].ContentType;
                }
                else
                {
                    throw new System.OperationCanceledException("The file can only be a PDF file");
                }
            }
            catch (Exception ex)
            {
                ViewData["ferror"] = ex.Message;
                ViewData["fname"] = "";
                ViewData["ftype"] = "";
                ViewData["fsize"] = "";
            }

            return View("UploadResult");
        }

        public ActionResult SalesOrderStatusUploading(DateTime uploaddate) 
        {
            TempData["date"] = uploaddate.ToString("MM/dd/yyyy");
            return View();
        }

        public ActionResult SalesOrderStatusUpload(DateTime txt_date) 
        {

            HttpFileCollectionBase File = Request.Files;
            try
            {
                // check extension
                string[] FileExtension = { ".xls", ".xlsx" };

                if (Array.IndexOf(FileExtension, Path.GetExtension(File[0].FileName.ToLower())) > -1)
                {
                    Directory.CreateDirectory(UploadHelper.SalesStatusUploadDirectory + Session["username"]);

                    string file_name = UploadHelper.SalesStatusUploadDirectory + Session["username"] + "\\" + Path.GetFileName(File[0].FileName);

                    File[0].SaveAs(file_name);

                    // get the contents of the excel file
                    DataTable excel_contents = null;
                    excel_contents = SqlDbHelper.getExclDataDt("select * from [Sheet1$]", file_name);
                    List<SkelClass.SalesStatus> excel_contents_obj = new List<SkelClass.SalesStatus>();
                    foreach (DataRow itm in excel_contents.Rows) 
                    {
                        excel_contents_obj.Add(new SkelClass.SalesStatus()
                        {
                            Channel = itm["Channel"].ToString()
                            ,
                            Area = itm["Area"].ToString()
                            ,
                            CardCode = itm["CardCode"].ToString()
                            ,
                            CardName = itm["CardName"].ToString()
                            ,
                            SalesOfficer = itm["SalesOfficer"].ToString()
                                /* 
                                    , Territory = itm["Territory"].ToString() 
                                */

                            /*
                                , POSTED_PREV = Convert.ToDecimal(itm["POSTED_PREV"].ToString())
                                , POSTED_MTD = Convert.ToDecimal(itm["POSTED_MTD"].ToString()) 
                            */
                            ,
                            POSTED_TOTAL = Convert.ToDecimal(itm["POSTED_TOTAL"].ToString())

                            /*
                                , CM_RETURNS = Convert.ToDecimal(itm["CM_RETURNS"].ToString())
                                , CM_SALES_ADJ = Convert.ToDecimal(itm["CM_SALES_ADJ"].ToString())
                            */
                            ,
                            CM_TOTAL = Convert.ToDecimal(itm["CM_TOTAL"].ToString())

                            ,
                            NET_POSTED = Convert.ToDecimal(itm["NET_POSTED"].ToString())

                            /*
                                , UNPOSTED_TRF_PREV = Convert.ToDecimal(itm["UNPOSTED_TRF_PREV"].ToString())
                                , UNPOSTED_TRF_MTD = Convert.ToDecimal(itm["UNPOSTED_TRF_MTD"].ToString())
                            */
                            ,
                            UNPOSTED_TRF_TOTAL = Convert.ToDecimal(itm["UNPOSTED_TRF_TOTAL"].ToString())

                            ,
                            OUTSTANDING_PICKLIST = Convert.ToDecimal(itm["OUTSTANDING_PICKLIST"].ToString())
                            ,
                            SUBTOTAL = Convert.ToDecimal(itm["SUBTOTAL"].ToString())

                            /*
                                , PENDING_PREV = Convert.ToDecimal(itm["PENDING_PREV"].ToString())
                                , PENDING_MTD = Convert.ToDecimal(itm["PENDING_MTD"].ToString())
                            */
                            ,
                            PENDING_TOTAL = Convert.ToDecimal(itm["PENDING_TOTAL"].ToString())

                            /*
                                , BALANCE_ORDER_PREV = Convert.ToDecimal(itm["BALANCE_ORDER_PREV"].ToString())
                                , BALANCE_ORDER_MTD = Convert.ToDecimal(itm["BALANCE_ORDER_MTD"].ToString())
                            */
                            ,
                            BALANCE_ORDER_TOTAL = Convert.ToDecimal(itm["BALANCE_ORDER_TOTAL"].ToString())
                                /*
                                    , GROSS_ORDER = Convert.ToDecimal(itm["GROSS_ORDER"].ToString())
                                */
                            ,
                            GROSS_BOOKINGS = Convert.ToDecimal(itm["GROSS_BOOKINGS"].ToString())

                        });
                    }

                    // insert to databse

                    SQLTransaction sql_trans = new SQLTransaction();

                    try 
                    {
                        sql_trans.StartTransaction();
                        DateTime dt_now = DateTime.Now;

                        sql_trans.DeleteFrom("UploadedSalesReport", new Dictionary<string, object>() { { "UploadDateTime", txt_date } });
                        
                        foreach (SkelClass.SalesStatus itm in excel_contents_obj) 
                        {
                            
                            sql_trans.InsertTo("UploadedSalesReport", 
                                new Dictionary<string, object>() { 
                                    {"UploadDateTime", txt_date }
                                    , {"Channel", itm.Channel }
                                    , {"Area", itm.Area }
                                    , {"CardCode", itm.CardCode }
                                    , {"CardName", itm.CardName }
                                    , {"SalesOfficer", itm.SalesOfficer }
                                    , {"Territory", itm.Territory }
                                    , {"POSTED_PREV", itm.POSTED_PREV }
                                    , {"POSTED_MTD", itm.POSTED_MTD }
                                    , {"POSTED_TOTAL", itm.POSTED_TOTAL }
                                    , {"CM_RETURNS", itm.CM_RETURNS }
                                    , {"CM_SALES_ADJ", itm.CM_SALES_ADJ }
                                    , {"CM_TOTAL", itm.CM_TOTAL }
                                    , {"NET_POSTED", itm.NET_POSTED }
                                    , {"UNPOSTED_TRF_PREV", itm.UNPOSTED_TRF_PREV }
                                    , {"UNPOSTED_TRF_MTD", itm.UNPOSTED_TRF_MTD }
                                    , {"UNPOSTED_TRF_TOTAL", itm.UNPOSTED_TRF_TOTAL }
                                    , {"OUTSTANDING_PICKLIST", itm.OUTSTANDING_PICKLIST }
                                    , {"SUBTOTAL", itm.SUBTOTAL }
                                    , {"PENDING_PREV", itm.PENDING_PREV }
                                    , {"PENDING_MTD", itm.PENDING_MTD }
                                    , {"PENDING_TOTAL", itm.PENDING_TOTAL }
                                    , {"BALANCE_ORDER_PREV", itm.BALANCE_ORDER_PREV }
                                    , {"BALANCE_ORDER_MTD", itm.BALANCE_ORDER_MTD }
                                    , {"BALANCE_ORDER_TOTAL", itm.BALANCE_ORDER_TOTAL }
                                    , {"GROSS_ORDER", itm.GROSS_ORDER }
                                    , {"GROSS_BOOKINGS", itm.GROSS_BOOKINGS }
                                });
                        }

                        sql_trans.Committransaction();
                    }
                    catch (Exception ex) 
                    {
                        sql_trans.RollbackTransaction();
                    }

                    ViewData["fname"] = Path.GetFileName(File[0].FileName);
                    ViewData["ftype"] = File[0].InputStream.Length;
                    ViewData["fsize"] = File[0].ContentType;
                    

                    ViewData["data"] = "";
                }
                else
                {
                    throw new Exception("The file can only be a an Excel file");
                }
            }
            catch (Exception ex)
            {
                ViewData["ferror"] = ex.Message;
                ViewData["fname"] = "";
                ViewData["ftype"] = "";
                ViewData["fsize"] = "";
            }

            return View("UploadResult");
        }

    }
}
