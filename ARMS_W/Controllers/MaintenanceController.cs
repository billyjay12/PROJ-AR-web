using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ARMS_W.SkelClass;
using ARMS_W.Class;
using ARMS_W.GLOBALS;
using System.Text;
using System.IO;

namespace ARMS_W.Controllers
{
    public class MaintenanceController : Controller
    {
        //
        // GET: /Maintenance/

        public ActionResult Index()
        {
            return View();
        }

        #region SALES TARGET MAINTANENCE

        public ActionResult SalesTargetMaintenance()
        {
            var database = new Models.ARMSTestEntities();
            var first_year = database.SalesTargetMaintenances.Select(o => o.Year).OrderBy(y => y).ToList();

            ViewData["first_year"] = DateTime.Now.Year;
            foreach (var itm in first_year)
            {
                ViewData["first_year"] = itm;
                break;
            }

            database.Dispose();

            //ViewData["first_year"] = first_year == null ? DateTime.Now.Year : first_year.Year;

            return View();
        }

        public string getSalesTarget(int year, string month, string empidno)
        {
            var DATABASE = new Models.ARMSTestEntities();

            var qry = (from a in DATABASE.SalesTargetMaintenances
                       join b in DATABASE.userHeaders on a.SlpCode equals b.SlpCode
                       where a.Year == year && a.Month == month && b.empIdNo == empidno
                       select a);
            string amount = string.Empty;
            foreach (var itm in qry)
            {
                amount = itm.Amount.ToString();
                break;
            }

            return amount;
        }

        public String getSalesOfficer()
        {
            StringBuilder sb = new StringBuilder();
            var employees = UserDefineFunctions.Application.ListOfSalesOfficer(Session["userid"].ToString());

            foreach (var itm in employees.OrderBy(o=>o.empFullName))
            {
                sb.Append("<option value=\"" + itm.empFullName + "\" code=\"" + itm.empIDNo + "\">" + itm.empFullName + "</option>");
            }

            return sb.ToString();
        }
       
        public JsonResult lookUpCode(string page_param)
        {
            var desc = new SkelClass.page_param.SalesTargetMaintenance();
            List<string[]> salestarget = new List<string[]>();

            desc = (SkelClass.page_param.SalesTargetMaintenance)Utils.JsonToObject<SkelClass.page_param.SalesTargetMaintenance>(page_param);
            try
            {
                foreach (var itm in desc.list_code)
                {
                    salestarget.Add(new string[]{
                        itm._desc,
                        itm._amount.ToString()
                    });
                }
                salestarget.GroupBy(o => o[0]).Select(grp => grp.First());
            }
            catch { }
            return Json(salestarget);
        }
        /*
        public JsonResult UpdateSalesTarget(page_param.SalesTargetMaintenance page_param)
        {
            var sql_trans = new  SQLTransaction();
            var ajx_res = new SkelClass.AjxResult();

            ajx_res.iserror = true;

            try
            {
                sql_trans.StartTransaction();

                var qry_changes = new List<Globals.db_changes>();
                var salestarget = UserDefineFunctions.SalesTarget.getSalesTarget();

                //log changes
                    if (page_param.list_code != null)
                    {
                        qry_changes = (List<Globals.db_changes>)
                           (from sales_target_page in page_param.list_code.AsEnumerable()
                            from sales_target_db in salestarget
                            where sales_target_page._desc == sales_target_db.Description && sales_target_page._amount != sales_target_db.Amount
                            select new Globals.db_changes
                            {
                                FieldName = sales_target_page._desc ,
                                NewValue = sales_target_page._amount.ToString()
                                ,
                                PrevValue = sales_target_db.Amount.ToString()
                            }).Union((from sales_target_page in page_param.list_code.AsEnumerable()
                                      select new Globals.db_changes
                                      {
                                          FieldName = sales_target_page._desc,
                                          NewValue = sales_target_page._amount.ToString(),
                                          PrevValue = ""
                                      }).Where(p => !salestarget.Any(x => x.Description == p.FieldName))).ToList();
                    }
                if(page_param.list_code_deleted!=null)
                    foreach (var itm in page_param.list_code_deleted)
                    {
                        qry_changes.Add(new Globals.db_changes()
                        {
                            FieldName = itm._desc,
                            NewValue = "deleted"
                        });
                    }

                //INSERT LOG CHANGES
                if (qry_changes.Count > 0)
                    DataChanges.LogChanges(ref sql_trans, qry_changes, (int)Globals.InfoType.SalesTarget, Session["username"].ToString().ToUpper(), "");

                sql_trans.DeleteFrom("SalesTarget", new Dictionary<string, object>() { });

                int code = 1;
                //SAVE SALES TARGET
                if(page_param.list_code!=null)
                    foreach (var itm in page_param.list_code)
                    {
                        sql_trans.InsertTo("SalesTarget",
                                new Dictionary<string, object>() { 
                            {"Code",code}
                            ,{"Description", itm._desc}
                            ,{"Amount", itm._amount }
                            ,{"DocTypeId", (int)Globals.InfoType.SalesTarget }
                        });
                        code = code + 1;
                    }

                sql_trans.Committransaction();
                ajx_res.iserror = false;
            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
            }

            return Json(ajx_res);
        }
        */
        public JsonResult UpdateSalesTargetMaintenance(List<page_param.SalesTarget> page_param)
        {
            var sql_trans = new SQLTransaction();
            var ajx_res = new SkelClass.AjxResult();
            var database = new Models.ARMSTestEntities();
            ajx_res.iserror = true;

            try
            {
                sql_trans.StartTransaction();

                

                var qry = (from a in page_param
                           join b in database.userHeaders.Where(o=>o.SlpCode.HasValue) on a.empid equals b.SlpCode.Value.ToString()
                           select new { a, b.SlpCode });

                
                           

                var qry_changes = new List<Globals.db_changes>();
             //   var salestarget = UserDefineFunctions.SalesTarget.getSalesTarget();


                var qry_tobedeleted = qry.Where(o => o.a.remarks == "For Update Sales Target");
                foreach (var itm in qry_tobedeleted)
                {
                    sql_trans.DeleteFrom("SalesTargetMaintenance", new Dictionary<string, object>() { { "Year", itm.a.year }, { "month", itm.a.month }, { "slpcode", itm.SlpCode } });
                }

                qry_changes = (List<Globals.db_changes>)qry_tobedeleted.Select(o => new Globals.db_changes
                {
                    FieldName = o.a.empfullname + " [" + o.a.month + ", " + o.a.year + "]",
                    PrevValue = o.a.prevsalestarget.ToString(),
                    NewValue = o.a.salestarget.ToString()
                }).ToList();

                foreach (var itm in qry.Where(o=>o.a.remarks != "For Update Sales Target"))
                {
                    qry_changes.Add(new Globals.db_changes()
                    {
                        FieldName = itm.a.empfullname + " [" + itm.a.month + ", " + itm.a.year + "]",
                        PrevValue = "New Sales Target",
                        NewValue = itm.a.salestarget.ToString()
                    });
                }

                if (qry_changes.Count > 0)
                    DataChanges.LogChanges(ref sql_trans, qry_changes, (int)Globals.InfoType.SalesTarget, Session["username"].ToString().ToUpper(), "");

                //SAVE SALES TARGET
                if (qry != null)
                    foreach (var itm in qry)
                    {
                        sql_trans.InsertTo("SalesTargetMaintenance",
                                new Dictionary<string, object>() { 
                            {"Year",itm.a.year}
                            ,{"Month", itm.a.month}
                            ,{"SlpCode",itm.SlpCode}
                            ,{"Amount", itm.a.salestarget }
                            ,{"DocTypeId", (int)Globals.InfoType.SalesTarget }
                        });
                    }
                database.Dispose();
                sql_trans.Committransaction();
                ajx_res.iserror = false;
            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
            }

            return Json(ajx_res);
        }


        public JsonResult getSalesTargetList(string year, string month, string empidno)
        {
            var ajx_res = new AjxResult();

            ajx_res.iserror = true;

            try
            {
                
                //for (int x = 0; x < Session.Count; x++)
                //{
                //    string asdasds = Session[x].ToString();
                //}
                if (Session["username"].ToString() == null) throw new Exception("Session Expired!");

                var salestarget = UserDefineFunctions.SalesTarget.getSalesTarget();

                if(year!="ALL")
                    salestarget = salestarget.Where(o => o.year == Int16.Parse(year)).ToList();
                if (month != "ALL")
                    salestarget = salestarget.Where(o => o.month.ToUpper() == month).ToList();
                if (empidno != "ALL")
                    salestarget = salestarget.Where(o => o.empid == empidno).ToList();

                salestarget = salestarget.OrderBy(o => o.year).ToList();


                ajx_res.data = new { list = salestarget };

                ajx_res.iserror = false;
            }
            catch(Exception ex)
            {
                ajx_res.message = ex.Message;
                ajx_res.iserror = true;
            }

            return Json(ajx_res);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public string getSalesTargetLogChanges()
        {
            List<Globals.db_changes_hdr> log_changes = DataChanges.GetLogChanges(Globals.InfoType.SalesTarget);

            StringBuilder table_builder = new StringBuilder();

            string a_style = "font-weight:bold; font-size:11px; background:#ededed; padding:2px;";
            string b_style = "font-weight:bold; font-size:11px; background:#f8f8f8; padding:2px;";
            string c_style = "font-size:11px; padding:2px;";

            log_changes = log_changes.OrderByDescending(c => c.log_datetime).ToList();

            table_builder.Append("<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\" >");
            foreach (var itm in log_changes)
            {
                // user name and time stamp
                table_builder.Append("<tr>");
                table_builder.Append("<td style=\"" + a_style + "\" colspan=\"3\">").Append(itm.username).Append(": ").Append(itm.log_datetime.ToString()).Append("</td>");
                table_builder.Append("</tr>");

                // detail header
                table_builder.Append("<tr>");
                table_builder.Append("<td style=\"" + b_style + "\"> &nbsp; </td><td style=\"" + b_style + "\">Prev. Value</td><td style=\"" + b_style + "\">New Value</td>");
                table_builder.Append("</tr>");

                // details
                foreach (var itm_sub in itm.logs)
                {

                    table_builder.Append("<tr>");
                    table_builder.Append("<td style=\"" + c_style + "\">").Append(itm_sub.FieldName).Append("</td>");
                    table_builder.Append("<td style=\"" + c_style + "\">").Append(itm_sub.PrevValue).Append("</td>");
                    table_builder.Append("<td style=\"" + c_style + "\">").Append(itm_sub.NewValue).Append("</td>");
                    table_builder.Append("</tr>");
                }

                table_builder.Append("<tr>");
                table_builder.Append("<td colspan=\"3\">&nbsp;</td>");
                table_builder.Append("</tr>");
            }
            table_builder.Append("</table>");

            return table_builder.ToString();
        }

        public String getUploadedSalesTarget()
        {
            var DATABASE = new Models.ARMSTestEntities();
            var ajx_res = new AjxResult();
            var excel_sales_target = new List<page_param.SalesTarget>();
            var res_sales_target = new List<page_param.SalesTarget>();
            StringBuilder sb = new StringBuilder();


            string[] months = { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };
            string res = "0";

            HttpFileCollectionBase file = Request.Files;
            string[] file_extensions = { ".xlsx", "xls" };
            string upload_file_extension = Path.GetExtension(file[0].FileName.ToLower());

            try
            {
                ajx_res.iserror = false;

                if (Array.IndexOf(file_extensions, upload_file_extension) > -1)
                {
                    string new_tmp_directory = Server.MapPath("..\\UPLOAD_TEMPFOLER");
                    string new_file_name = Path.GetFileName(file[0].FileName);
                    string str_full_filename = new_tmp_directory + "\\" + new_file_name;

                    Directory.CreateDirectory(new_tmp_directory);
                    file[0].SaveAs(str_full_filename);

                    DataTable qry = ExcelReader.getExclData12(
                        @" SELECT *
                           FROM [Sheet1$]
                           WHERE [SALESTARGET] is not null
                           ",
                            str_full_filename);

                    foreach (DataRow itm in qry.Rows)
                    {
                        try
                        {
                            var SLpcode = short.Parse(itm["SOCODE"].ToString());
                            var year = Int16.Parse(itm["YEAR"].ToString());
                            var month = itm["MONTH"].ToString();
                            decimal? prevsalestarget = 0;
                            var fullname = DATABASE.arms2_vw_lookUpOSLP.SingleOrDefault(o => o.SlpCode == SLpcode);
                            var SalesTargetMaintenances = DATABASE.SalesTargetMaintenances.SingleOrDefault(o => o.Year == year && o.Month == month && o.SlpCode == SLpcode);

                            if (SalesTargetMaintenances != null) prevsalestarget = SalesTargetMaintenances.Amount;
                            if (fullname == null) throw new Exception("Error: SO Code not found");
                            if (!months.Any(o => o == month)) throw new Exception("Error: Invalid Month inputted.");
                            if (year > 9999 || year < 1900) throw new Exception("Error: Invalid Year inputted.");

                            excel_sales_target.Add(new page_param.SalesTarget()
                            {
                                empid = itm["SOCODE"].ToString(),
                                month = itm["MONTH"].ToString(),
                                year = Int16.Parse(itm["YEAR"].ToString()),
                                salestarget = Decimal.Parse(itm["SALESTARGET"].ToString()),

                                empfullname = fullname.SlpName,
                                prevsalestarget = prevsalestarget ?? 0,
                                remarks = prevsalestarget != 0 ? "For Update Sales Target" : "New Sales Target"
                            });

                        }
                        catch(Exception ex)
                        {
                            ajx_res.iserror = true;
                            ajx_res.message = Newtonsoft.Json.JsonConvert.SerializeObject(ex.Message);
                
                            break;
                        }
                    }

                    res = Newtonsoft.Json.JsonConvert.SerializeObject(excel_sales_target);
                }
                ajx_res.data = new { salestarget = res_sales_target };

            }
            catch (Exception ex)
            {
                ajx_res.iserror = true;
                ajx_res.message = ex.InnerException.ToString() ?? ex.Message.ToString();
                
            }

            return ajx_res.iserror ? ajx_res.message : res;
        }

        #endregion

        #region DAY CYCLE MAINTENANCE

        public ActionResult DayCycleMaintenance()
        {
            return View();
        }

        public JsonResult getDayCycleCount()
        {
            var ajx_res = new AjxResult();
            var sql_trans = new SQLTransaction();
            ajx_res.iserror = true;

            try
            {
                if (Session["username"] == null) throw new Exception("Session Expired");
                sql_trans.StartTransaction();

                var daycycle = UserDefineFunctions.DayCycle.getDayCycle();

                ajx_res.data = new { dayCycleNo = daycycle.DayCycleCount, rangeDayCycle = daycycle.rangeDayCycle, startDayOfTheMonth = daycycle.startDayOfTheMonth };

                sql_trans.Committransaction();
                ajx_res.iserror = false;
            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
            }

            return Json(ajx_res);
        }

        public JsonResult UpdateDayCycle(page_param.DayCycleMaintenance page_param)
        {
            var ajx_res = new AjxResult();
            var sql_trans = new SQLTransaction();
            ajx_res.iserror = true;

            try
            {
                if (Session["username"] == null) throw new Exception("Session Expired");
                sql_trans.StartTransaction();

                var daycycle = UserDefineFunctions.DayCycle.getDayCycle();

                var qry_changes = new List<Globals.db_changes>();

                if (daycycle.DayCycleCount != page_param.DayCycleCount)
                {

                    qry_changes.Add(new Globals.db_changes()
                    {
                        FieldName = "Counting Day Cycle",
                        NewValue = page_param.DayCycleCount.ToString(),
                        PrevValue = daycycle.DayCycleCount.ToString(),
                    });
                }

                if (daycycle.rangeDayCycle != page_param.rangeDayCycle)
                {

                    qry_changes.Add(new Globals.db_changes()
                    {
                        FieldName = "range Day Cycle",
                        NewValue = page_param.rangeDayCycle.ToString(),
                        PrevValue = daycycle.rangeDayCycle.ToString(),
                    });
                }

                if (daycycle.startDayOfTheMonth != page_param.startDayOfTheMonth)
                {
                    qry_changes.Add(new Globals.db_changes()
                    {
                        FieldName = "Start Sched Counting Day of the Month",
                        NewValue = page_param.startDayOfTheMonth.ToString(),
                        PrevValue = daycycle.startDayOfTheMonth.ToString(),
                    });
                }


                if (qry_changes.Count > 0)
                    DataChanges.LogChanges(ref sql_trans, qry_changes, (int)Globals.InfoType.DayCycleMaintenance, Session["username"].ToString());

                sql_trans.DeleteFrom("dayCycle", new Dictionary<string, object>() { });

                sql_trans.InsertTo("dayCycle",
                        new Dictionary<string, object>() { 
                        {"DayCycleNo",page_param.DayCycleCount}
                        ,{"rangeDayCycle",page_param.rangeDayCycle}
                        ,{"startDayOfTheMonth",page_param.startDayOfTheMonth}
                        ,{"DocTypeId", (int)Globals.InfoType.DayCycleMaintenance }
                    });
                    

                sql_trans.Committransaction();
                ajx_res.iserror = false;
            }
            catch (Exception ex)
            {
                sql_trans.RollbackTransaction();
                ajx_res.iserror = true;
                ajx_res.message = ex.Message;
            }

            return Json(ajx_res);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public string getDayCycleLogChanges()
        {
            List<Globals.db_changes_hdr> log_changes = DataChanges.GetLogChanges(Globals.InfoType.DayCycleMaintenance);
  
            StringBuilder table_builder = new StringBuilder();

            string a_style = "font-weight:bold; font-size:11px; background:#ededed; padding:2px;";
            string b_style = "font-weight:bold; font-size:11px; background:#f8f8f8; padding:2px;";
            string c_style = "font-size:11px; padding:2px;";

            log_changes = log_changes.OrderByDescending(c => c.log_datetime).ToList();

            table_builder.Append("<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\" >");
            foreach (var itm in log_changes)
            {
                // user name and time stamp
                table_builder.Append("<tr>");
                table_builder.Append("<td style=\"" + a_style + "\" colspan=\"3\">").Append(itm.username).Append(": ").Append(itm.log_datetime.ToString()).Append("</td>");
                table_builder.Append("</tr>");

                // detail header
                table_builder.Append("<tr>");
                table_builder.Append("<td style=\"" + b_style + "\"> &nbsp; </td><td style=\"" + b_style + "\">Prev. Value</td><td style=\"" + b_style + "\">New Value</td>");
                table_builder.Append("</tr>");

                // details
                foreach (var itm_sub in itm.logs)
                {

                    table_builder.Append("<tr>");
                    table_builder.Append("<td style=\"" + c_style + "\">").Append(itm_sub.FieldName).Append("</td>");
                    table_builder.Append("<td style=\"" + c_style + "\">").Append(itm_sub.PrevValue).Append("</td>");
                    table_builder.Append("<td style=\"" + c_style + "\">").Append(itm_sub.NewValue).Append("</td>");
                    table_builder.Append("</tr>");
                }

                table_builder.Append("<tr>");
                table_builder.Append("<td colspan=\"3\">&nbsp;</td>");
                table_builder.Append("</tr>");
            }
            table_builder.Append("</table>");

            return table_builder.ToString();
        }

        #endregion

        #region OBJECTIVE MAINTENANCE

        public ActionResult ObjectiveMaintenance()
        {
            return View();
        }

        public JsonResult UpdateObjective(page_param.ObjectiveHdr page_param)
        {
            var sql_trans = new SQLTransaction();

            try
            {
                sql_trans.StartTransaction();

                var objectives = UserDefineFunctions.Objective.getListObjectives();

                //log changes
                var qry_changes = (List<Globals.db_changes>)
                    (from objective_page in page_param.objective_list.AsEnumerable()
                     from objective_db in objectives
                     where objective_page.objectiveCode == objective_db.objectiveCode && objective_page.FieldName == objective_db.FieldName && objective_page.isUsed != objective_db.isUsed
                     select new Globals.db_changes
                     {
                         FieldName = objective_page.objectiveCode+" - "+ objective_page.FieldName,
                         NewValue = objective_page.isUsed == true ? "Enabled" : "Disabled",
                         PrevValue = objective_db.isUsed == true ? "Enabled" : "Disabled"
                     }).Union((from objective_page in page_param.objective_list.AsEnumerable()
                               select new Globals.db_changes
                               {
                                   FieldName = objective_page.objectiveCode + " - " + objective_page.FieldName,
                                   NewValue = objective_page.isUsed == true ? "Enabled" : "Disabled",
                                   PrevValue = ""
                               }).Where(p => !objectives.Any(x => x.objectiveCode + " - " + x.FieldName == p.FieldName))).ToList();


                //INSERT LOG CHANGES
                if (qry_changes.Count > 0)
                    DataChanges.LogChanges(ref sql_trans, qry_changes, (int)Globals.InfoType.ObjectiveMaintenance, Session["username"].ToString().ToUpper(), "");


                foreach (var itm in page_param.objective_list)
                {
                    sql_trans.UpdateTo1("Objectives", new Dictionary<string, object>()
                                    {
                                        {"isUsed",itm.isUsed==true?"Y":"N"}
                                    }, new Dictionary<string, object> { { "objectiveCode", itm.objectiveCode },{"FieldName",itm.FieldName} });
                }
                sql_trans.Committransaction();
            }
            catch(Exception ex)
            {
                sql_trans.RollbackTransaction();
                string err = ex.Message;
            }

            return Json("");
        }

        public string listOfAllObjectives()
        {
            StringBuilder table_builder = new StringBuilder();

            var objectives = UserDefineFunctions.Objective.getObjectives();

            table_builder.Append("<ul id=\"tbl_list\">");
            table_builder.Append("<li>");
            table_builder.Append("<input type=\"checkbox\" />");
            table_builder.Append("<label>Objectives</label>");
            table_builder.Append("<ul>");
            foreach (var itm in objectives)
            {
                table_builder.Append("<li>");
                table_builder.Append("<input type=\"checkbox\" />");
                table_builder.Append("<label>").Append(itm.objectiveDesc).Append("</label>");
                table_builder.Append("<ul>");
                foreach (var itm_sub in itm.objective_list)
                {
                    table_builder.Append("<li>");
                    table_builder.Append("<input type=\"checkbox\" code=\"" + itm_sub.objectiveCode + "\" value=\"" + itm_sub.FieldName + "\" ").Append(itm_sub.isUsed == true ? "checked=\"checked\"" : "").Append("/>");
                    table_builder.Append("<label>").Append(itm_sub.FieldName).Append("</label>");

                    table_builder.Append("</li>");
                }
                table_builder.Append("</ul>");
            }

            table_builder.Append("</ul>");
            table_builder.Append("</li>");
            table_builder.Append("</ul>'");

            return table_builder.ToString();
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public string getObjectiveLogChanges()
        {
            List<Globals.db_changes_hdr> log_changes = DataChanges.GetLogChanges(Globals.InfoType.ObjectiveMaintenance);

            StringBuilder table_builder = new StringBuilder();

            string a_style = "font-weight:bold; font-size:11px; background:#ededed; padding:2px;";
            string b_style = "font-weight:bold; font-size:11px; background:#f8f8f8; padding:2px;";
            string c_style = "font-size:11px; padding:2px;";

            log_changes = log_changes.OrderByDescending(c => c.log_datetime).ToList();

            table_builder.Append("<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\" >");
            foreach (var itm in log_changes)
            {
                // user name and time stamp
                table_builder.Append("<tr>");
                table_builder.Append("<td style=\"" + a_style + "\" colspan=\"3\">").Append(itm.username).Append(": ").Append(itm.log_datetime.ToString()).Append("</td>");
                table_builder.Append("</tr>");

                // detail header
                table_builder.Append("<tr>");
                table_builder.Append("<td style=\"" + b_style + "\"> &nbsp; </td><td style=\"" + b_style + "\">Prev. Value</td><td style=\"" + b_style + "\">New Value</td>");
                table_builder.Append("</tr>");

                // details
                foreach (var itm_sub in itm.logs)
                {

                    table_builder.Append("<tr>");
                    table_builder.Append("<td style=\"" + c_style + "\">").Append(itm_sub.FieldName).Append("</td>");
                    table_builder.Append("<td style=\"" + c_style + "\">").Append(itm_sub.PrevValue).Append("</td>");
                    table_builder.Append("<td style=\"" + c_style + "\">").Append(itm_sub.NewValue).Append("</td>");
                    table_builder.Append("</tr>");
                }

                table_builder.Append("<tr>");
                table_builder.Append("<td colspan=\"3\">&nbsp;</td>");
                table_builder.Append("</tr>");
            }
            table_builder.Append("</table>");

            return table_builder.ToString();
        }


        #endregion

        #region SO ASSIGN OUTLET MAINTENANCE

        public ActionResult OutletAssign()
        {
            return View();
        }

        public String lookUpSalesOfficerEmployee()
        {
            //List<string[]> SOemployee = UserDefineFunctions.Application.getListOfSalesOfficerEmployee().ToList();
            var SOemployee = UserDefineFunctions.Application.getListOfSalesOfficerEmployee();
            StringBuilder sb = new StringBuilder();

            foreach (var itm in SOemployee)
            {
                sb.Append("<option value=\"" + itm.empFullName + "\" code=\"" + itm.empIDNo + "\">" + itm.empFullName + "</option>");
            }

            return sb.ToString();
        }

        public JsonResult getOutlets()
        {
            var current_user = new _User(Session["username"].ToString());
           // return Json(UserDefineFunctions.OutletHdr.getOutletList());

            return Json(current_user.Roles.Any(o => o.Position.ToUpper() == "ADMIN") ?
                    UserDefineFunctions.OutletHdr.getInventoryCountSchedule() : UserDefineFunctions.OutletHdr.getInventoryCountSchedule(current_user.EmployeeIdNo));
        }

        public JsonResult UpdateOutlet(string _custOutletId, string empId)
        {
            SQLTransaction sql_trans = new SQLTransaction();

            try
            {
                sql_trans.StartTransaction();

                sql_trans.UpdateTo("custOutlets",
                    new Dictionary<string, object>() { 
                        {"inventoryIncharge", empId }
                    }
                    , new Dictionary<string, object>() { { "custOutletsID", _custOutletId } });

                sql_trans.Committransaction();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                sql_trans.RollbackTransaction();
                throw new Exception("Failed updating..!");
            }
            return Json("");
        }
        #endregion



    }
}
