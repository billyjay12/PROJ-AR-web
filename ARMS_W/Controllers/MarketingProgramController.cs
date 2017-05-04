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
    public class MarketingProgramController : Controller
    {
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult MarketingPog()
        {


            return View();
        }


        public ActionResult SearchWindow()
        {


            return View();
        }


        public ActionResult MarketingProgramDetails()
        {


            return View();
        }


        public ActionResult SearchMarketingProgram() { 
        
        return View();
        
        }

        public ActionResult SearchList()
        {

            return View();

        }


        public ActionResult MarketingProgramStatus()
        {

            return View();

        }


        public ActionResult MarketingProgramFrontpage()
        {

            return View();

        }



        public ActionResult RunningCostDetails()
        {

            return View();

        }






         [HttpPost]
        public string ConfirMktgPrograms(
             //Int64 programNo,
            string status,
            string progName,
            string progType,
            string brand,
            string targetChannel,
            //string targetArea,
            string backGround,
            string objective,
            string measures,
            string preparedBy,
            string resources,
            string timeline,
            string Attachment,
           // string targetAccounts,
            string actActivities,
            double totalAmtResources,
            string branch,
             //string xcelRead,
             string TargetAcct,
             double totalAmt
           )
        {
            SQLTransaction mt_trans = new SQLTransaction();
            routingController route_trans = new routingController();
            string programNo = "";
            string newprogramNo = SqlQueryHelper.GenerateMktReq();
            OleDbDataReader greader = SqlDbHelper.getData(newprogramNo);
            //string filename = "";
            //OleDbDataReader xcelreader = SqlDbHelper.getExlData("Select * from [sheet1$] ","C:\\Documents and Settings\\hervieinoc\\Desktop\\sample.xlsx");
            if (greader.Read())
            {

                programNo = greader.GetValue(0).ToString();

            }

            
            //while (xcelreader.Read())
            //{

            //    xcelRead = xcelreader.GetValue(0).ToString();

            //}

            
           

            try
            {
                mt_trans.StartTransaction();

                mt_trans.CommandText = SqlQueryHelper.Insert_marketingprog(
                             programNo,
                             //"",
                             status,
                             progName,
                              progType,
                             brand,
                             targetChannel,
                             "",
                            // targetArea,
                             backGround,
                             objective,
                             measures,
                             preparedBy,
                             totalAmtResources,
                             totalAmt

       
                    );


                //mt_trans.CommandText = SqlQueryHelper.Insert_targetAccounts(
                //    programNo,
                //    targetAccounts
                    
                    
                //    );


                if (resources.Length > 0)
                {
                    foreach (string strn in StringHelper.GetRows(resources))
                    {
                        string[] tmp_columns = strn.Split('|');
                        mt_trans.CommandText = SqlQueryHelper.InsertNew_mktgResources(
                           programNo,
                            tmp_columns[0],
                           tmp_columns[1],
                           tmp_columns[2]
                            
                          );
                    }
                }



                if (timeline.Length > 0)
                {
                    foreach (string strn in StringHelper.GetRows(timeline))
                    {
                        string[] tmp_columns = strn.Split('|');
                        mt_trans.CommandText = SqlQueryHelper.InsertNew_mktgTimeline(
                        programNo,
                        Convert.ToDateTime(tmp_columns[0]),
                        Convert.ToDateTime(tmp_columns[1]),
                        tmp_columns[2],
                        tmp_columns[3],
                        tmp_columns[4]

                          );
                    }
                }


                if (Attachment.Length > 0)
                {
                    foreach (string strn in StringHelper.GetRows(Attachment))
                    {
                        string[] tmp_columns = strn.Split('|');
                        mt_trans.CommandText = SqlQueryHelper.InsertNew_Attachment(
                           programNo,
                            tmp_columns[0],
                           tmp_columns[1]
                        
                          );
                    }
                }



                if (actActivities.Length > 0)
                {
                    foreach (string strn in StringHelper.GetRows(actActivities))
                    {
                        string[] tmp_columns = strn.Split('|');
                        mt_trans.CommandText = SqlQueryHelper.InsertNew_actActivities(
                           programNo,
                           Convert.ToDateTime(tmp_columns[0]),
                           tmp_columns[1]
                        
                          );
                    }
                }

                TargetAcct = StringHelper.UrlDecode( TargetAcct);
                if (TargetAcct.Length > 0)
                {
                    foreach (string strn in StringHelper.GetRows(TargetAcct))
                    {
                        string[] tmp_columns = strn.Split('|');
                        mt_trans.CommandText = SqlQueryHelper.InsertNew_TargetAcct(
                           programNo,
                           tmp_columns[0],
                           StringHelper.InsertQoutes( tmp_columns[1]),
                           tmp_columns[2],
                           Convert.ToDecimal(tmp_columns[3])

                          );
                    }
                }


                //if (targetAccounts.IndexOf("L") > -1)
                //{
                //    branch = "Luzon";
                //}
                //else
                //{
                //    branch = "Vismin";
                //}




               


                mt_trans.Committransaction();
                

                return "00:";
            }
            catch (Exception ex)
            {
                mt_trans.RollbackTransaction();
                return "01:" + ex.Message;
            }

              }









         public string GenerateMktReq(string newprogramNo)
         {
             SQLTransaction mt_trans = new SQLTransaction();
             try
             {

                 string programNo = "";
                 OleDbDataReader _greader = SqlDbHelper.getData(SqlQueryHelper.GenerateMktReq());
                 if (_greader.Read())
                 {
                     programNo = _greader.GetValue(0).ToString();
                     newprogramNo = programNo;
                     return newprogramNo;
                 }
                 else
                 {
                     throw new FormatException("program Number not available.");

                 }
             }
             catch (Exception ex)
             {
                 mt_trans.RollbackTransaction();
                 return "01:" + ex.Message;
             }
         }



    



    //Routing


        // routing
         public string CallRouting(string val_action_type, string val_mkt/*, string val_accCode**/)
         {
             string branch = "";
             //string Total = "";
             //double TotalResources=0;

             OleDbDataReader g_reader;
             //g_reader = SqlDbHelper.getData("SELECT targtAccts from dbo.mrktTargetAccounts where programNo='" + Convert.ToString(val_mkt) + "'");
             //branch = Convert.ToString(region.Rows[0]["acctCode"]);
            // v_reader = SqlDbHelper.getData("SELECT totalAmtResources from dbo.mrktProgram where programNo='" + Convert.ToString(val_mkt) + "'");

            // if (g_reader.Read())
             //{
           //      branch = g_reader.GetValue(0).ToString();
           //  }

             //if (v_reader.Read())
             //{
             //    Total = v_reader.GetValue(0).ToString();
             //}




             //if (branch.IndexOf("CL") > -1)
             //{
             //    branch = "Luzon";
             //}
             //else
             //{
             //    branch = "Vismin";
             //}



             try
             {
                 if (val_action_type == "Approve")
                 {



                     int val = 1;
                     bool isApprove = Convert.ToBoolean(val);
                     routingController eroute = new routingController();
                     //eroute.routeNext(6, val_mkt, "http://textlink.com", branch, isApprove);
                     return "00:";

                 }



                 else
                 {
                     int val2 = 0;

                     bool isApprove = Convert.ToBoolean(val2);
                     routingController eroute = new routingController();
                     //  eroute.routeNext(6, val_mkt, "http://textlink.com", branch, isApprove);

                     return "00:";

                 }






             }
             catch (Exception ex)
             {
                 return "01:" + ex.Message;
             }
         }



    }
}
