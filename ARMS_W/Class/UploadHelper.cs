using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ARMS_W.Class
{
	public class UploadHelper
	{

        public static string CcaUploadDirectory { get { return "d:\\APP_FILE_UPLOADS\\" + AppHelper.ArmsDB + "_UPLOADS\\cca\\"; } }
        public static string MmaUploadDirectory { get { return "d:\\APP_FILE_UPLOADS\\" + AppHelper.ArmsDB + "_UPLOADS\\mma\\"; } }
        public static string MktpUploadDirectory { get { return "d:\\APP_FILE_UPLOADS\\" + AppHelper.ArmsDB + "_UPLOADS\\Mktp\\"; } }
        public static string MrktngReqDirectory { get { return "d:\\APP_FILE_UPLOADS\\" + AppHelper.ArmsDB + "_UPLOADS\\MrtngReq\\"; } }
        public static string LdbUploadDirectory { get { return "d:\\APP_FILE_UPLOADS\\" + AppHelper.ArmsDB + "_UPLOADS\\Ldb\\"; } }
        public static string SalesStatusUploadDirectory { get { return "d:\\APP_FILE_UPLOADS\\" + AppHelper.ArmsDB + "_UPLOADS\\SUD\\"; } }
        public static string CalendarUploadDirectory { get { return "d:\\APP_FILE_UPLOADS\\" + AppHelper.ArmsDB + "_UPLOADS\\SUD\\"; } }

		public static void CopyAllFiles ( string frm, string to ) { 
			foreach(string fname in Directory.GetFiles(frm)){
				File.Copy(fname, to + Path.GetFileName(fname), true);
			}
		}

		public static void DeletePrevFiles (string frm) { 
			foreach(string fname in Directory.GetFiles(frm)){
				File.Delete(fname);
			}
		}

        public static Boolean ProcessMmaAttachments(string mma_num, string username) {
            try
            {
                // create a folder for a new mma_num
                Directory.CreateDirectory(MmaUploadDirectory + mma_num);

                if (Directory.Exists(MmaUploadDirectory + username + "\\") == false)
                {
                    // create a directory named after a user
                    Directory.CreateDirectory(MmaUploadDirectory + username);
                }

                // copy all files from [username] folder to [mma_num] folder
                UploadHelper.CopyAllFiles(MmaUploadDirectory + username + "\\", MmaUploadDirectory + mma_num + "\\");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ProcessMmaAttachment(string mma_num, string username, string filename)
        {
            try
            {
                // create a folder for a new mma_num
                Directory.CreateDirectory(MmaUploadDirectory + mma_num);

                if (Directory.Exists(MmaUploadDirectory + username + "\\") == false)
                {
                    // create a directory named after a user
                    Directory.CreateDirectory(MmaUploadDirectory + username);
                }

                // copy file from [username] folder to [mma_num] folder
                System.IO.File.Copy(MmaUploadDirectory + username + "\\" + filename, MmaUploadDirectory + mma_num + "\\" + filename, true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean ProcessCcaAttachments(string ccaNum, string username)
        {
            try
            {
                // create a folder for a new ccaNum
                Directory.CreateDirectory(CcaUploadDirectory + ccaNum);

                if (Directory.Exists(CcaUploadDirectory + username + "\\") == false)
                {
                    // create a directory named after a user
                    Directory.CreateDirectory(CcaUploadDirectory + username);
                }

                // copy all files from [username] folder to [ccaNum] folder
                UploadHelper.CopyAllFiles(CcaUploadDirectory + username + "\\", CcaUploadDirectory + ccaNum + "\\");
                

                // should delete source files
                

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ProcessCcaAttachment(string ccaNum, string username, string filename)
        {
            try
            {
                if (ccaNum == "") 
                {
                    throw new Exception("Error processing file[" + filename + "][" + ccaNum + "]");
                }
                
                if (Directory.Exists(CcaUploadDirectory + username + "\\") == false)
                {
                    // create a directory named after a user
                    Directory.CreateDirectory(CcaUploadDirectory + username);
                }

                // create a folder for a new ccaNum
                if (Directory.Exists(CcaUploadDirectory + ccaNum) == false)
                {
                    // create a directory named after a user
                    Directory.CreateDirectory(CcaUploadDirectory + ccaNum);
                }

                // copy file from [username] folder to [ccaNum] folder
                System.IO.File.Copy(CcaUploadDirectory + username + "\\" + filename, CcaUploadDirectory + ccaNum + "\\" + filename, true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ProcessLdbAttachment(string requestid, string username, string filename) 
        {
            try
            {
                // create a folder for a new requestid
                Directory.CreateDirectory(LdbUploadDirectory + requestid);

                if (Directory.Exists(LdbUploadDirectory + username + "\\") == false)
                {
                    // create a directory named after a user
                    Directory.CreateDirectory(LdbUploadDirectory + username);
                }

                // copy file from [username] folder to [requestid] folder
                System.IO.File.Copy(LdbUploadDirectory + username + "\\" + filename, LdbUploadDirectory + requestid + "\\" + filename, true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

	}
}