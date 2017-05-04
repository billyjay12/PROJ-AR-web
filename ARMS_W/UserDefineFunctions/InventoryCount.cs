using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARMS_W.Class;
using ARMS_W.SkelClass;
using System.Text;


namespace ARMS_W.UserDefineFunctions
{
    public class InventoryCount
    {

        #region INVENTORY COUNT FOR SO

        public static page_param.inventoryCount getInventoryDetails(string inventoryCountId)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new page_param.inventoryCount();
            var sb = new StringBuilder();
            var sb1 = new StringBuilder();

            try
            {
                var qry_header = (from a in DATABASE.arms2_vw_getInventoryHeader
                                  where a.inventoryCountId == inventoryCountId
                                  select a);

                foreach (var itm in qry_header)
                {
                    res.acctCode = itm.acctCode;
                    res.acctName = itm.CardName;
                    res.acctAddress = itm.acctAddress;
                    res.countRange = itm.countRange;
                    res.dateTimeStamp = itm.dateTimeStamp;
                    res.nextCountDate = itm.nextCountDate;
                    res.prevCountDate = itm.prevCountDate;
                    res.empId = itm.empId;
                    res.empFirstName = itm.firstName;
                    res.empLastName = itm.lastName;
                    res.pareto = itm.Pareto;
                    res.area = itm.AreaName;
                    res.territoryName = itm.TerritoryName;
                    res.inventoryCountId = itm.inventoryCountId;
                    res.remarks = itm.remarks;
                    res.totalAmount = itm.totalAmount;
                    res.whsInchargeID = itm.whsInchargeID;
                    res.doctypeid = (int)itm.docTypeId;
                    res.documentstatusid = (int)itm.DocumentStatusId;
                    res.inventoryCountStatus = itm.inventoryCountStatus;
                    res.actualCountDate = itm.actualCountDate;
                    res.actualCountEndValidDate = itm.actualCountEndValidDate;

                    res.forthemonth = itm.actualCountDate.HasValue ? String.Format("{0:y}", itm.actualCountDate) : "";

                    res.totalVarianceAmt = itm.totalVarianceAmt;
                    res.totalForecastAmt0 = itm.totalSalesForecastAmt0;
                    res.totalForecastAmt1 = itm.totalSalesForecastAmt1;
                    res.totalForecastAmt2 = itm.totalSalesForecastAmt2;
                    res.totalForecastAmt3 = itm.totalSalesForecastAmt3;

                    //warehouse incharge details
                    res.whs_details = new page_param.inventoryCount.whsIncharge()
                    {
                        whsInchargeID = itm.whsInchargeID,
                        whsInchargeFirstName = itm.whsInchargeFirstName,
                        whsInchargeMiddleName = itm.whsInchargeMiddleName,
                        whsInchargeLastName = itm.whsInchargeLastName,
                        whsInchargeContactNo = itm.whsInchargeContactNo
                    };
                }

                var qry_detail = (from a in DATABASE.arms2_vw_getInventoryDetail
                                  where a.inventoryCountId == inventoryCountId
                                  select a).ToList();

                res.inventorycount_list = new List<page_param.inventoryCount.inventoryCountdetails>();

                foreach (var itm in qry_detail.OrderBy(o => o.lineId))
                {
                    res.inventorycount_list.Add(new page_param.inventoryCount.inventoryCountdetails()
                    {
                        inventoryCountId = itm.inventoryCountId,
                        lineId = itm.lineId,
                        itemCode = itm.itemCode,
                        ssr = itm.ssr,
                        begNvPcs = itm.begNvPcs,
                        sellIn = itm.netSellIn,
                        endNvPcs = itm.endNvPcs,
                        netOnHand = itm.netSellinOnHand,
                        actualSellOutAmt = itm.actualSellOutAmt,
                        actualSellOutPcs = itm.actualSellOutPcs,
                        forecastFTMamt0 = itm.salesForecastAmt0,
                        forecastFTMpcs0 = itm.salesForecastPcs0,
                        forecastFTMamt1 = itm.salesForecastAmt1,
                        forecastFTMpcs1 = itm.salesForecastPcs1,
                        forecastFTMamt2 = itm.salesForecastAmt2,
                        forecastFTMpcs2 = itm.salesForecastPcs2,
                        forecastFTMamt3 = itm.salesForecastAmt3,
                        forecastFTMpcs3 = itm.salesForecastPcs3,
                        remarks = itm.remarks ?? "",
                        varianceAmt = itm.varianceAmt,
                        variancePcs = itm.variancePcs,
                        brand = itm.brand,
                        prodGrp = itm.prodgrp,
                        itemDesc = itm.itemdescription,
                        itemPrice = itm.srp
                    });


                    sb1.Append("<tr clone=\"true\">");
                    sb1.Append("<td><input type=\"text\" value=\"" + itm.lineId + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb1.Append("<td><input type=\"text\" value=\"" + itm.brand + "\" style=\"width:60px; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb1.Append("<td><input type=\"text\" value=\"" + itm.prodgrp + "\" style=\"width:50px; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb1.Append("<td><input type=\"text\" value=\"" + itm.itemCode + "\" style=\"width:120px; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb1.Append("<td><input type=\"text\" value=\"" + itm.itemdescription + "\" style=\"width:250px; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb1.Append("</tr>");

                    sb.Append("<tr clone=\"true\">");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.ssr + "\" style=\"width:80px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.begNvPcs + "\" style=\"width:60px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.netSellIn + "\" style=\"width:60px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.endNvPcs + "\" style=\"width:60px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.netSellinOnHand + "\" style=\"width:60px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.actualSellOutPcs + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + round(itm.actualSellOutAmt) + "\" style=\"width:80px;background-color:#efefef; text-align:right\" readonly=\"readonly\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.variancePcs + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");

                    sb.Append("<td><input type=\"text\" value=\"" + round(itm.varianceAmt) + "\" style=\"width:80px; background-color:#efefef; text-align:right\" readonly=\"readonly\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.salesForecastPcs0 + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + round(itm.salesForecastAmt0) + "\" style=\"width:80px; background-color:#efefef; text-align:right\" readonly=\"readonly\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.salesForecastPcs1 + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + round(itm.salesForecastAmt1) + "\" style=\"width:80px; background-color:#efefef; text-align:right\" readonly=\"readonly\" /></td>");
                    //sb.Append("<td><input type=\"text\" value=\"" + itm.salesForecastPcs2 + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    //sb.Append("<td><input type=\"text\" value=\"" + round(itm.salesForecastAmt2) + "\" style=\"width:80px;background-color:#efefef; text-align:right\" readonly=\"readonly\" /></td>");
                    //sb.Append("<td><input type=\"text\" value=\"" + itm.salesForecastPcs3 + "\" style=\"width:50px; text-align:center; background-color:#efefef\" readonly=\"readonly\" /></td>");
                    //sb.Append("<td><input type=\"text\" value=\"" + round(itm.salesForecastAmt3) + "\" style=\"width:80px;  background-color:#efefef; text-align:right\" readonly=\"readonly\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.remarks + "\" style=\"background-color:#efefef\" readonly=\"readonly\" /></td>");
                    sb.Append("</tr>");

                    const string asad = "1";

                }

                res.inventoryCountlist_tablebuilder_freeze = sb1.ToString();
                res.inventoryCountlist_tablebuilder = sb.ToString();

                if (!res.totalAmount.HasValue)
                    res.totalAmount = res.inventorycount_list.Sum(o => o.actualSellOutAmt);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                DATABASE.Dispose();
            }

            return res;
        }

        public static string round(decimal? num)
        {
            decimal result;
            try
            {
                result = Math.Round(num.Value);
            }
            catch { return ""; }
            return result.ToString("#,##0");
        }
      
        public static List<page_param.inventoryCountList> getInventoryCountList(bool isSuperUser=false)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new List<page_param.inventoryCountList>();

            try
            {
                var qry = isSuperUser == true ?
                        (from a in DATABASE.mtc_vw_inventoryCountlist select a)
                        : (from a in DATABASE.mtc_vw_inventoryCountlist where a.DocumentStatusId == 1 select a);

                foreach (var itm in qry)
                {
                    res.Add(new page_param.inventoryCountList()
                    {
                        inventoryCountId = itm.inventoryCountId,
                        empId = itm.empId,
                        empName = itm.firstName + ' ' + itm.lastName,
                        acctCode = itm.acctCode,
                        acctName = itm.Name,
                        acctAddress = itm.location,
                        statusDesc = itm.stateDesc,
                        roleId = itm.roleid,
                        invStatus = itm.inventoryCountStatus,
                        inventoryCountMonth = itm.actualCountDate.HasValue ? String.Format("{0:y}", (DateTime)itm.actualCountDate.Value) : "",
                        DocumentStatusId = itm.DocumentStatusId.HasValue ? itm.DocumentStatusId.Value : 1
                    });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            DATABASE.Dispose();
            return res;
        }

        public static List<page_param.inventoryCountList> getInventoryCountList(string empIDNo)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new List<page_param.inventoryCountList>();

            try
            {
               // var get_assigned_accounts = DATABASE.arms2_vw_actveBusPrtnr.Where(o => o.empIdNo == empIDNo);
              //  var get_inventorycounthdr = DATABASE.InventoryCountHdrs.

               // var qry1 = (from a in DATABASE.arms2_vw_actveBusPrtnr
               //             join b in DATABASE.mtc_vw_inventoryCountlist on a.CardCode equals b.acctCode
               //             where a.empIdNo == empIDNo
               //             select b).ToList();


                var qry = (from a in DATABASE.mtc_vw_inventoryCountlist
                           where a.empId == empIDNo
                           select a).ToList();

                foreach (var itm in qry)
                {
                    res.Add(new page_param.inventoryCountList()
                    {
                        inventoryCountId = itm.inventoryCountId,
                        empId = itm.empId,
                        empName = itm.firstName + ' ' + itm.lastName,
                        acctCode = itm.acctCode,
                        acctName = itm.Name,
                        acctAddress = itm.location,
                        statusDesc = itm.stateDesc,
                        roleId = itm.roleid,
                        invStatus = itm.inventoryCountStatus,
                        inventoryCountMonth = itm.actualCountDate.HasValue ? String.Format("{0:y}", (DateTime)itm.actualCountDate.Value) : "",
                        DocumentStatusId = itm.DocumentStatusId.HasValue ? itm.DocumentStatusId.Value : 1
                    });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            DATABASE.Dispose();
            return res;
        }

        public static List<page_param.inventoryCountList> getInventoryCountList_ASMSO(string empIDNo)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new List<page_param.inventoryCountList>();
            var emp_list = new List<string>();
            try
            {
                var region_user = (from a in DATABASE.apprvrDesigs
                                   from b in DATABASE.userHeaders
                                   where a.counterId == b.counterId && b.empIdNo == empIDNo && a.roleID == 8 //roleid 8 =  (CHannel MANAGER)
                                   select a.branch).ToList();

                var so_list = (from a in DATABASE.apprvrDesigs
                               from b in DATABASE.userHeaders
                               where a.counterId == b.counterId && a.roleID == 17
                               select new { a.branch, a.counterId, b.firstName, b.lastName, b.empIdNo }).ToList();
                foreach (var itm in so_list.Where(o => region_user.Contains(o.branch)))
                {
                    emp_list.Add(itm.empIdNo);
                }


                //var qry_channels = (from a in DATABASE.apprvrDesigs
                //                    from b in DATABASE.userHeaders
                //                    where a.counterId == b.counterId && b.empIdNo == empIDNo && a.roleID == 8
                //                    select a.channel).Distinct().ToList();

                //var qry_empIdNo = (from apprvrdesig in DATABASE.apprvrDesigs
                //                   from userheader in DATABASE.userHeaders
                //                   where apprvrdesig.counterId == userheader.counterId
                //                        && qry_channels.Contains(apprvrdesig.channel)
                //                        && apprvrdesig.roleID == 2
                //                   select userheader.empIdNo).Distinct().ToList();

                var qry_inventory_accounts = (from a in DATABASE.InventoryCountHdrs
                                              from b in DATABASE.approvalStates
                                              from d in DATABASE.customerHeaders
                                              from e in DATABASE.userHeaders
                                              where a.acctCode == d.SapAcctCode &&
                                                  a.docTypeId == b.docType && a.DocumentStatusId == b.stateID &&
                                                  a.empId == e.empIdNo && d.TAG1 == null &&
                                                 // qry_empIdNo.Contains(a.empId) change to
                                                 emp_list.Contains(a.empId)
                                                 
                                              select new
                                              {
                                                  a.acctCode,
                                                  a.inventoryCountId,
                                                  a.empId,
                                                  e.firstName,
                                                  e.lastName,
                                                  d.bussAdd,
                                                  d.acctName,
                                                  b.stateDesc,
                                                  b.roleID,
                                                  a.inventoryCountStatus,
                                                  a.actualCountDate,
                                                  a.DocumentStatusId
                                              }).ToList();

                foreach (var itm in qry_inventory_accounts)
                {
                    res.Add(new page_param.inventoryCountList()
                    {
                        inventoryCountId = itm.inventoryCountId,
                        empId = itm.empId ?? "",
                        empName = itm.firstName ?? "" + ' ' + itm.lastName ?? "",
                        acctCode = itm.acctCode ?? "",
                        acctName = itm.acctName ?? "",
                        acctAddress = itm.bussAdd ?? "",
                        statusDesc = itm.stateDesc ?? "",
                        roleId = itm.roleID,
                        invStatus = itm.inventoryCountStatus ?? "",
                        inventoryCountMonth = itm.actualCountDate.HasValue ? String.Format("{0:y}", (DateTime)itm.actualCountDate.Value) : "",
                        DocumentStatusId = itm.DocumentStatusId.HasValue ? itm.DocumentStatusId.Value : 1
                    });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            DATABASE.Dispose();
            return res;
        }

        public static page_param.inventoryCount getInventoryCountHdr(string inventoryCountId)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new page_param.inventoryCount();

            try
            {
                var qry = (from a in DATABASE.InventoryCountHdrs
                           where a.inventoryCountId == inventoryCountId
                           select new { a });

                foreach (var itm in qry)
                {
                    res.inventoryCountId = itm.a.inventoryCountId;
                    res.documentstatusid = itm.a.DocumentStatusId.HasValue ? (int)itm.a.DocumentStatusId : 0;
                    res.doctypeid = itm.a.docTypeId.HasValue ? (int)itm.a.docTypeId : 0;
                    res.whsInchargeID = itm.a.whsInchargeID ?? "";
                    res.actualCountDate = itm.a.actualCountDate; 
                    res.inventoryCountStatus = itm.a.inventoryCountStatus??"";
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            DATABASE.Dispose();
            return res;
        }

        public static page_param.nextInventoryCount getNextInventoryCount(string acctCode)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new page_param.nextInventoryCount();

            try
            {
                var qry = (from a in DATABASE.nextInventoryCountHdrs
                           where a.acctCode == acctCode
                           select new
                           {
                               a.lineId,
                               a.acctCode,
                               a.prevCountDate,
                               a.startCountDate
                           });


                foreach (var itm in qry)
                {
                    res.lineId = itm.lineId ?? "";
                    res.acctCode = itm.acctCode ?? "";
                    res.prevCountDate = itm.prevCountDate;
                    res.startCountDate = itm.startCountDate;
                }

                var qry1 = (from a in DATABASE.arms2_vw_nextInvDtl_itemMasterFile
                            where a.lineId == res.lineId
                            select a);

                res.nextInventory_detail = new List<page_param.nextInventoryCount.nextInventoryCountDetail>();
                foreach (var itm in qry1)
                {
                    res.nextInventory_detail.Add(new page_param.nextInventoryCount.nextInventoryCountDetail()
                    {
                        lineId = itm.lineId ?? "",
                        ssr = itm.ssr,
                        begNvPcs = itm.begNvPcs.HasValue ? (int)itm.begNvPcs : 0,
                        itemCode = itm.itemCode ?? "",
                        brand = itm.brand ?? "",
                        itemDesc = itm.itemdescription ?? "",
                        prodGrp = itm.prodgrp ?? "",
                        forecastFTMpcs0 = itm.salesForecastPcs0.HasValue ? (int)itm.salesForecastPcs0 : 0,
                        forecastFTMamt0 = itm.salesForecastAmt0.HasValue ? (int)itm.salesForecastAmt0 : 0,
                        forecastFTMpcs1 = itm.salesForecastPcs1.HasValue ? (int)itm.salesForecastPcs1 : 0,
                        forecastFTMamt1 = itm.salesForecastAmt1.HasValue ? (int)itm.salesForecastAmt1 : 0,
                        forecastFTMpcs2 = itm.salesForecastPcs2.HasValue ? (int)itm.salesForecastPcs2 : 0,
                        forecastFTMamt2 = itm.salesForecastAmt2.HasValue ? (int)itm.salesForecastAmt2 : 0,
                        itemPrice = itm.srp.HasValue ? (decimal)itm.srp : 0
                    });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            DATABASE.Dispose();
            return res;
        }

        public static Globals.inventoryCountPermission getPermission(int roleId,_User current_user)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new Globals.inventoryCountPermission();

            try
            {
                var qry = (from a in DATABASE.apprvrRoles
                           from b in DATABASE.mtc_vw_User_Role_Details
                           where a.roleCode == b.roleCode && a.roleID == roleId && b.empIDNo == current_user.EmployeeIdNo
                           select a);
                if (current_user.Roles.Any(o => o.Position.ToUpper() == "ASMSO"))
                {
                    res.AllowApprove = true;
                    res.AllowSave = true; res.AllowEditSO = true;
                }
                foreach (var itm in qry)
                {
                    res.AllowApprove = true;
                    res.AllowSave = true;

                    if (current_user.Roles.Any(o => o.Position.ToUpper() == "SO")) { res.AllowEditSO = true; }
                    if (current_user.Roles.Any(o => o.Position.ToUpper() == "ASMSO")) { res.AllowEditSO = true; }
                    break;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            DATABASE.Dispose();
            return res;
        }

        public static page_param.DocumentStatus getDocumentStatus(string inventoryCountId)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new page_param.DocumentStatus();
            try
            {
                var qry = (from a in DATABASE.InventoryCountHdrs
                           from b in DATABASE.approvalStates
                           where a.docTypeId == b.docType && a.DocumentStatusId == b.stateID
                           && a.inventoryCountId == inventoryCountId
                           select b);

                foreach (var itm in qry)
                {
                    res.doctype = itm.docType;
                    res.roleId = itm.roleID;
                    res.stateId = itm.stateID;
                    res.statusDesc = itm.stateDesc;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            DATABASE.Dispose();
            return res;
        }

        public static int saveToCoveragePlan(ref SQLTransaction sql_trans, string empId, string acctCode, DateTime? startcountdate, DateTime actualCountDate, string status, string generatedLineNum, string EventID_GeneratedCode, string inventoryCountId)
        {
            var DATABASE = new Models.ARMSTestEntities();
            DateTime DateTimeStamp = DateTime.Now;
            string EventId = "";
            int success = 0;
            string month = DateTimeStamp.Month.ToString();
            try
            {
                var EventIds = DATABASE.CoverageHdrs.Where(o => o.EmpIdNo == empId && o.Month == month && o.Year == DateTimeStamp.Year);
               
                foreach (var itm in EventIds)
                {
                    EventId = itm.EventID;
                    break;
                }

                var CoverageDtl = DATABASE.CoverageDtls.Where(o => o.EventID == EventId && o.AccountCode == acctCode && o.Day == DateTimeStamp.Day).ToList();

                if (EventId == "")
                {
                    EventId = EventID_GeneratedCode;
                    sql_trans.InsertTo("CoverageHdr", new Dictionary<string, object>()
                            {
                                {"EventID",EventID_GeneratedCode},
                                {"EmpIdNo",empId},
                                {"Year",DateTimeStamp.Year },
                                {"Month",DateTimeStamp.Month.ToString()},
                                {"DocTypeId",(int)Globals.InfoType.CalendarEvent},
                                {"DocumentStatusId","0"}
                            });
                }

                if(DATABASE.CoverageDtls.Any(o=>o.EventID == EventId && o.AccountCode == acctCode && o.Day == startcountdate.Value.Day)==true)
                    if (startcountdate != null)
                    {
                        var qryExistingSched = DATABASE.CoverageDtls.Where(o => o.EventID == EventId && o.AccountCode == acctCode && o.Day == startcountdate.Value.Day).Single();
                        var qryExistingSched_dtl1 = DATABASE.CoverageDtl1.Where(o => o.LineNum == qryExistingSched.LineNum);

                        if (qryExistingSched_dtl1.Count() == 1)
                        {
                            var isObjectiveInventory = DATABASE.CoverageDtl1.Any(p => p.LineNum == qryExistingSched.LineNum && p.ObjectiveCode == "INV" && (p.inventoryCountId == "" || p.inventoryCountId == null));
                            if (isObjectiveInventory)
                            {
                                sql_trans.DeleteFrom("CoverageDtl1", new Dictionary<string, object>() { { "LineNum", qryExistingSched.LineNum }, { "ObjectiveCode", "INV" }, { "inventoryCountId", null } });
                                sql_trans.DeleteFrom("CoverageDtls", new Dictionary<string, object>() { { "EventId", qryExistingSched.EventID }, { "LineNum", qryExistingSched.LineNum } });
                            }
                        }
                        else
                        {
                            sql_trans.DeleteFrom("CoverageDtl1", new Dictionary<string, object>() { { "LineNum", qryExistingSched.LineNum }, { "ObjectiveCode", "INV" }, { "inventoryCountId", null } });
                        }
                    }

                bool hasAlreadyEvent = true;
                if (CoverageDtl.Count() < 1)
                {
                    hasAlreadyEvent = false;

                    sql_trans.InsertTo("CoverageDtls", new Dictionary<string, object>()
                    {
                        {"EventID",EventId},
                        {"LineNum",generatedLineNum},
                        {"Day",DateTime.Now.Day},
                        {"AccountCode",acctCode},
                        {"IsPlanned",status=="HIT"?"T":"F"},
                        {"IsDeleted","F"},
                        {"IsAnEdit","F"},
                        {"AcctStatus","0"},
                        {"hasCallreport",status=="HIT"?"T":"F"}
                    });
                }

                sql_trans.InsertTo("CoverageDtl1", new Dictionary<string, object>()
                    {
                        {"EventID",EventId},
                        {"LineNum",hasAlreadyEvent? CoverageDtl.First().LineNum:generatedLineNum},
                        {"Day",DateTime.Now.Day},
                        {"ObjectiveCode","INV"},
                        {"isIncoverage",status=="HIT"?"T":"F"},
                        {"IsDeleted","F"},
                        {"inventoryCountId",inventoryCountId}
                    });
            }
            catch
            {
                success = -1;
            }

            return success;
        }

        #endregion 

        #region INVENTORY COUNT FOR AUDIT

        public static List<page_param.inventoryCountList> getForAuditInventory()
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new List<page_param.inventoryCountList>();

            try
            {
                var qry = (from a in DATABASE.mtc_vw_inventoryCountlist
                           where a.DocumentStatusId!=0 && a.audited=="F"
                           select a).ToList();

                foreach (var itm in qry)
                {
                    res.Add(new page_param.inventoryCountList()
                    {
                        inventoryCountId = itm.inventoryCountId,
                        empId = itm.empId ?? "",
                        empName = itm.firstName ?? "" + ' ' + itm.lastName ?? "",
                        acctCode = itm.acctCode ?? "",
                        acctName = itm.Name ?? "",
                        acctAddress = itm.location ?? "",
                        statusDesc = itm.stateDesc ?? "",
                        roleId = itm.roleid,
                        invStatus = itm.inventoryCountStatus ?? "",
                        inventoryCountMonth = itm.actualCountDate.HasValue ? String.Format("{0:y}", (DateTime)itm.actualCountDate.Value) : ""
                    });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            DATABASE.Dispose();
            return res;
        }

        public static List<page_param.inventoryCountAuditList> getAuditInventoryList()
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new List<page_param.inventoryCountAuditList>();
            string auditName ="";
            try
            {
                var qry = (from a in DATABASE.mtc_vw_inventoryCountlist
                           from b in DATABASE.InventoryCountAuditHdrs
                           where b.referenceInventoryCountId == a.inventoryCountId
                           select new { a, b }).ToList();

                var employeeDetails = DATABASE.mtc_vw_User_Role_Details.ToList();

                foreach (var itm in qry)
                {
                    var qry_auditedby = (from a in employeeDetails
                                         where a.empIDNo == itm.b.auditedBy
                                         select a).ToList();
                    foreach (var itm1 in qry_auditedby)
                    {
                        auditName = itm1.empIDNo + " - " + itm1.firstName.ToString() + " " + itm1.lastName.ToString();
                        break;
                    }

                    res.Add(new page_param.inventoryCountAuditList()
                    {
                        inventoryCountAuditId = itm.b.inventoryCountAuditId,
                        inventoryCountId = itm.a.inventoryCountId,
                        empId = itm.a.empId ?? "",
                        empName = itm.a.firstName ?? "" + ' ' + itm.a.lastName ?? "",
                        acctCode = itm.a.acctCode ?? "",
                        outletName = itm.a.Name ?? "",
                        outletLocation = itm.a.location ?? "",
                        invStatus = itm.a.inventoryCountStatus ?? "",
                        AuditedBy = auditName
                    });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            DATABASE.Dispose();
            return res;
        }

    /*    public static page_param.inventoryCountAuditDetails getInventoryCountAuditDetails11111111(string inventoryCountAuditId)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new page_param.inventoryCountAuditDetails();
            try
            {
                var qry = (from a in DATABASE.InventoryCountHdrs
                           from b in DATABASE.mtc_vw_User_Role_Details
                           from e in DATABASE.customerHeaders
                           from d in DATABASE.InventoryCountAuditHdrs
                           where a.empId == b.empIDNo && a.acctCode == e.acctCode &&
                               d.referenceInventoryCountId==a.inventoryCountId &&
                               d.inventoryCountAuditId == inventoryCountAuditId
                           select new
                           {
                               a.acctCode,
                               a.countRange,
                               a.dateTimeStamp,
                               a.nextCountDate,
                               a.prevCountDate,
                               a.empId,
                               a.actualCountDate,
                               b.firstName,
                               b.lastName,
                               e.Pareto,
                               e.area,
                               a.inventoryCountId,
                               a.remarks,
                               a.totalAmount,
                               a.whsInchargeID,
                               a.docTypeId,
                               a.DocumentStatusId,
                               d,
                               a.inventoryCountStatus
                           }).ToList();



                foreach (var itm in qry)
                {
                    res.inventoryCountAuditId = itm.d.inventoryCountAuditId;
                    res.auditedByID = itm.d.auditedBy;
                    res.remarks = itm.d.remarks ?? "";
                    res.date = itm.d.date;

                    var qry_auditedby = (from a in DATABASE.mtc_vw_User_Role_Details
                                         where a.empIDNo==res.auditedByID
                                         select a).ToList();
                    foreach (var itm1 in qry_auditedby)
                    {
                        res.auditedByName = itm1.firstName.ToString() + " " + itm1.lastName.ToString();
                        break;
                    }

                    res.acctCode = itm.acctCode;
                    res.empId = itm.empId ?? "";
                    res.empFirstName = itm.firstName ?? "";
                    res.empLastName = itm.lastName ?? "";
                    res.pareto = itm.Pareto ?? "";
                    res.area = itm.area ?? "";
                    res.inventoryCountId = itm.inventoryCountId ?? "";
                    res.whsInchargeID = itm.whsInchargeID;
                    res.inventoryCountStatus = itm.inventoryCountStatus ?? "";

                    res.forthemonth = itm.actualCountDate.HasValue ? String.Format("{0:y}", itm.actualCountDate) : "";

                }

                var qry_detail = (from a in DATABASE.mtc_vw_inventoryCountlist
                                  where a.inventoryCountId == res.inventoryCountId
                                  select a);
                foreach (var itm in qry_detail)
                {
                    res.acctName = itm.Name;
                    res.acctAddress = itm.location;
                    break;
                }

                var qry_CustOutletDetails = (from a in DATABASE.custOutletIncharges
                                             where  a.acctCode == res.acctCode
                                                && a.whsInchargeID == res.whsInchargeID
                                             select new { a }).ToList();

                foreach (var itm in qry_CustOutletDetails)
                {
                    //warehouse incharge details
                    res.whs_details = new page_param.inventoryCountAuditDetails.whsIncharge()
                    {
                        whsInchargeID = itm.a.whsInchargeID ?? "",
                        whsInchargeFirstName = itm.a.whsInchargeFirstName ?? "",
                        whsInchargeMiddleName = itm.a.whsInchargeMiddleName ?? "",
                        whsInchargeLastName = itm.a.whsInchargeLastName ?? "",
                        whsInchargeContactNo = itm.a.whsInchargeContactNo ?? ""
                    };

                }


                var qry1 = from a in DATABASE.InventoryCountAuditDtls.ToList()
                           from b in DATABASE.armsII_vw_itemMasterFile.ToList()
                           where a.itemCode == b.ITEMCODE && a.inventoryCountAuditId == res.inventoryCountAuditId
                           select new { a, b };



                res.inventorycount_list = new List<page_param.inventoryCountAuditDetails.inventoryCountAuditdetails>();
                foreach (var itm in qry1)
                {
                    res.inventorycount_list.Add(new page_param.inventoryCountAuditDetails.inventoryCountAuditdetails()
                    {
                        inventoryCountId = itm.a.inventoryCountAuditId,
                        lineId = itm.a.lineId,
                        itemCode = itm.a.itemCode ?? "",
                        actualCount = itm.a.actualCount,
                        remarks = itm.a.remarks ?? "", brand = itm.b.Brand ?? "",
                        prodGrp = itm.b.ProdGrp ?? "",
                        itemDesc = itm.b.ItemDescription ?? ""
                    });
                }

                res.totalCount = res.inventorycount_list.Sum(p => p.actualCount);
                res.inventorycount_list = res.inventorycount_list.OrderBy(o => o.lineId).ToList();

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            DATABASE.Dispose();
            return res;
        }
        */

        public static page_param.inventoryCountAuditDetails getInventoryCountAuditDetails(string inventoryCountAuditId)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new page_param.inventoryCountAuditDetails();
            var sb = new StringBuilder();

            try
            {
                var qry_header = (from a in DATABASE.arms2_vw_getInventoryAuditHeader
                                  where a.inventoryCountAuditId == inventoryCountAuditId
                                  select a);

                foreach (var itm in qry_header)
                {
                    res.inventoryCountAuditId = itm.inventoryCountAuditId;
                    res.acctCode = itm.acctCode;
                    res.acctName = itm.CardName;
                    res.acctAddress = itm.acctAddress;

                    res.auditedByID = itm.auditedBy;
                    res.auditedByName = itm.auditedName;
                    res.empId = itm.empId;
                    res.empFirstName = itm.firstName;
                    res.empLastName = itm.lastName;
                    res.pareto = itm.Pareto;
                    res.area = itm.AreaName;
                    res.territoryName = itm.TerritoryName;
                    res.inventoryCountId = itm.inventoryCountId;
                    res.remarks = itm.remarks;
                    res.whsInchargeID = itm.whsInchargeID;
                    res.date = itm.date;
                    
                    res.forthemonth = itm.actualCountDate.HasValue ? String.Format("{0:y}", itm.actualCountDate) : "";

                    res.totalCount = itm.totalActualCount;

                    //warehouse incharge details
                    res.whs_details = new page_param.inventoryCountAuditDetails.whsIncharge()
                    {
                        whsInchargeID = itm.whsInchargeID,
                        whsInchargeFirstName = itm.whsInchargeFirstName,
                        whsInchargeMiddleName = itm.whsInchargeMiddleName,
                        whsInchargeLastName = itm.whsInchargeLastName,
                        whsInchargeContactNo = itm.whsInchargeContactNo
                    };
                }

                var qry_detail = (from a in DATABASE.arms2_vw_getInventoryAuditDetail
                                  where a.inventoryCountAuditId == inventoryCountAuditId
                                  select a);

                res.inventorycount_list = new List<page_param.inventoryCountAuditDetails.inventoryCountAuditdetails>();

                foreach (var itm in qry_detail.OrderBy(o => o.lineId))
                {
                    res.inventorycount_list.Add(new page_param.inventoryCountAuditDetails.inventoryCountAuditdetails()
                    {
                        inventoryCountId = itm.inventoryCountAuditId,
                        lineId = itm.lineId,
                        itemCode = itm.itemCode ?? "",
                        actualCount = itm.actualCount,
                        remarks = itm.remarks ?? "",
                        brand = itm.brand ?? "",
                        prodGrp = itm.prodgrp ?? "",
                        itemDesc = itm.itemdescription ?? ""
                    });

                    sb.Append("<tr clone=\"true\">");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.lineId + "\" style=\"width:50px; text-align:center;\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.brand + "\"  style=\"width:60px;\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.prodgrp  + "\" style=\"width:70px;\" /></td>'");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.itemCode + "\" style=\"width:120px;\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.itemdescription + "\" style=\"width:250px;\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.actualCount + "\" style=\" text-align:center\" /></td>");
                    sb.Append("<td><input type=\"text\" value=\"" + itm.remarks + "\" /></td>");

                    sb.Append("</tr>");
                }

                res.inventoryCountlist_tablebuilder = sb.ToString();

                if (!res.totalCount.HasValue)
                    res.totalCount = res.inventorycount_list.Sum(o => o.actualCount);
            }
            finally
            {
                DATABASE.Dispose();
            }

            return res;
        }
        #endregion

        private static decimal getVarianceAmount(decimal amount1, decimal amount2)
        {
            return amount1 - amount2;
        }
        private static int getVariancePcs(int pcs1, int pcs2)
        {
            return pcs1 - pcs2;
        }


    }
}