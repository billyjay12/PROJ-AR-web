using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data;
using ARMS_W.Class;
using ARMS_W.SkelClass;
using System.IO;
using System.Net.Mime;

namespace ARMS_W.Controllers
{
    public class UtilitiesController : Controller
    {
        //
        // GET: /Utilities/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SalesOrderStatusUpload() 
        {
            return View();
        }

        public ActionResult PricelistUpload()
        {
            _User cuurent_user = (_User)Session["Ousr"];
            
            ViewData["DateTimeStamp"] = "";
            ViewData["isUploader"] = false;

            if (cuurent_user.HasPositionOf("SPRUSER") > -1)
                ViewData["isUploader"] = true;

            DataTable pricelist = SqlDbHelper.getDataDT("SELECT DateTimeStamp FROM UploadedPricelist");
            foreach (DataRow itm in pricelist.Rows)
            {
                ViewData["DateTimeStamp"] = itm["DateTimeStamp"].ToString();
            }

            return View();
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult getLatestDateSalesOrderStatusReport()
        {
            var ajx_res = new SkelClass.AjxResult();
            ajx_res.iserror = true;
            try
            {
                string UploadDateTime = "";

                var dt_from_db = SqlDbHelper.getDataDT("select isnull((select max(UploadDateTime) from UploadedSalesReport), getdate()) UploadDateTime"); 
                foreach (DataRow itm in dt_from_db.Rows)
                {
                    UploadDateTime = Convert.ToDateTime(itm["UploadDateTime"]).ToShortDateString();
                    break;
                }

                ajx_res.data = new { uploadDateTime = UploadDateTime };

                ajx_res.iserror = false;
            }
            catch (Exception ex)
            {
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
            }


            return Json(ajx_res);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadPricelist()
        {
            var DATABASE = new Models.ARMSTestEntities();
            int file_counter = 0;

           // var fileupload_db = DATABASE.UploadedPricelists.SingleOrDefault();

            //if (DATABASE.UploadedPricelists.Count() > 0)
               // DATABASE.DeleteObject(fileupload_db);

            foreach (string inputTagName in Request.Files)
            {


                HttpPostedFileBase file = Request.Files[inputTagName];
                string fileExt = Path.GetExtension(file.FileName);
              //  if (fileExt != ".xlsx")
                  //  throw new Exception("file uploaded is not an excel");
                if (file.ContentLength > 0)
                {


                    string mimeType = Request.Files[inputTagName].ContentType;
                    Stream fileStream = Request.Files[inputTagName].InputStream;
                    string fileName = Path.GetFileName(Request.Files[inputTagName].FileName);
                    int fileLength = Request.Files[inputTagName].ContentLength;
                    byte[] fileData = new byte[fileLength];
                    fileStream.Read(fileData, 0, fileLength);


                   // var fileDesc = Request.Form["txt_desc" + file_counter + ""].ToString();
                    var fileDesc = Request.Form[inputTagName].ToString();

                    file_counter += 1;

                    DATABASE.UploadedPricelists.AddObject(new Models.UploadedPricelist()
                    {
                        FileAttachment = fileName,
                        AttachmentType = mimeType,
                        FileContent = fileData,
                        DateTimeStamp = DateTime.Now,
                        UploadedBy = Session["username"].ToString(),
                        FileDescription = fileDesc
                    });


                }
            }

            try
            {
                DATABASE.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
            finally
            {
                DATABASE.Dispose();
            }

            return RedirectToAction("PricelistUpload", "Utilities");
        }

        public JsonResult getListOfFiles()
        {
            var ajx_res = new AjxResult();

            List<page_param.FilesUploaded> files = new List<page_param.FilesUploaded>();
            
            byte[] filecontent = (byte[])null;
            string mimetype = "",
                   filename = "";

            DataTable uploadedpricelist = SqlDbHelper.getDataDT("SELECT * FROM UploadedPricelist");
            foreach (DataRow pricelist in uploadedpricelist.Rows)
            {
                filecontent = (byte[])pricelist["FileContent"];
                mimetype = pricelist["AttachmentType"].ToString();
                filename = pricelist["FileAttachment"].ToString();

                files.Add(new page_param.FilesUploaded()
                {
                    id = (long)pricelist["id"],
                    DateTimeStamp = (DateTime)pricelist["DateTimeStamp"],
                    FileAttachment = pricelist["FileAttachment"].ToString(),
                    FileDescription = pricelist["FileDescription"].ToString(),
                    UploadedBy = pricelist["UploadedBy"].ToString()
                });
            }

            ajx_res.data = new { file = files.OrderByDescending(p=>p.DateTimeStamp).ToList() };

            return Json(ajx_res);
        }

        public int DeleteFile(int id)
        {
            int success = 0;
            var DATABASE = new Models.ARMSTestEntities();
            var getFile = DATABASE.UploadedPricelists.Single(o => o.id == id);

            DATABASE.DeleteObject(getFile);

            try
            {
                DATABASE.SaveChanges();
            }
            catch
            {
                success = -1;
            }
            finally { DATABASE.Dispose(); }

            return success;
        }

        public FileContentResult PreviewFile(int id)
        {
            var DATABASE = new Models.ARMSTestEntities();

            var file = DATABASE.UploadedPricelists.Single(o => o.id == id);


            byte[] filecontent = (byte[])null;
            string mimetype = "",
                  filename = "";

            filecontent = file.FileContent;
            mimetype = file.AttachmentType;
            filename = file.FileAttachment;

            DATABASE.Dispose();

            var contentDispostion = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = true,
            };
            Response.AppendHeader("Content-Disposition", contentDispostion.ToString());
            return File(filecontent, mimetype);
        }

        public FileContentResult DownloadFile(int id)
        {
            var DATABASE = new Models.ARMSTestEntities();

            var file = DATABASE.UploadedPricelists.Single(o => o.id == id);


            byte[] filecontent = (byte[])null;
            string mimetype = "",
                  filename = "";

            filecontent = file.FileContent;
            mimetype = file.AttachmentType;
            filename = file.FileAttachment;

            DATABASE.Dispose();

           return File(filecontent, mimetype, filename);
        }

    }
}
