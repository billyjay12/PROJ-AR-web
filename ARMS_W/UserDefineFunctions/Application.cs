using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ARMS_W.Class;
using ARMS_W.SkelClass;

namespace ARMS_W.UserDefineFunctions
{
    public partial class Application
    {
        public static List<string[]> getSO(string userId)
        {
            List<string[]> SalesOfficer = new List<string[]>();
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            //query to get asm and SO
            try
            {
                var qry = (from pos in DATABASE.armsII_vw_CalendarUser
                           where pos.ASM_ID == userId
                           select new
                           {
                               pos.empIDNo,
                               pos.firstName,
                               pos.lastName

                           }).Union(from pos_in in DATABASE.armsII_vw_CalendarUser // get the so only
                                    where pos_in.empIDNo == userId
                                    select new
                                    {
                                        pos_in.empIDNo,
                                        pos_in.firstName,
                                        pos_in.lastName
                                    }).ToList();

                foreach (var itm in qry)
                {
                    SalesOfficer.Add(new string[] { 
                     itm.empIDNo +'-'+itm.lastName+','+itm.firstName,
                     itm.empIDNo
                    });
                }
            }
            finally
            {
                DATABASE.Dispose();
            }

            return SalesOfficer;
        }

        public static List<string[]> getforCallreport(string Eventmonth, int Eventday, int Eventyear, string soId, string ObjectiveCode)
        {
            List<string[]> Events = new List<string[]>();
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            var qry = (from evnt_vw in DATABASE.armsII_vw_CallReport
                       where evnt_vw.EmpIdNo == soId
                           && evnt_vw.Month == Eventmonth && evnt_vw.Year == Eventyear
                           && evnt_vw.Day == Eventday && evnt_vw.ObjectiveCode == ObjectiveCode
                       select new
                       {

                           evnt_vw.EventId,
                           evnt_vw.EmpIdNo,
                           evnt_vw.Year,
                           evnt_vw.Month,
                           evnt_vw.DoctypeId,
                           evnt_vw.DocumentStatusId,
                           evnt_vw.Day,
                           evnt_vw.AccountCode,
                           evnt_vw.AccountName,
                           evnt_vw.AccountAddress,
                           evnt_vw.AccountClass,
                           evnt_vw.ContactPerson,
                           evnt_vw.ContactPersonNo,
                           evnt_vw.ObjectiveCode,
                           evnt_vw.StoreCheckingResult,
                           evnt_vw.ProductPresentationResult,
                           evnt_vw.TotalAmount,
                           evnt_vw.CompetitorActivities,
                           evnt_vw.WithOrder,
                           evnt_vw.NextCallDate,
                           evnt_vw.PONum,
                           evnt_vw.IssuesAndConcerns,
                           evnt_vw.Delivery,
                           evnt_vw.Orders,
                           evnt_vw.SummaryLackingItems,
                           evnt_vw.MobileNo,
                           evnt_vw.Recommendation,
                           evnt_vw.TimeTable,
                           evnt_vw.Remarks,
                           evnt_vw.Brand,
                           evnt_vw.cFullCollection,
                           evnt_vw.cNoCollection,
                           evnt_vw.cPartialCollection,
                           evnt_vw.LineNum,
                           evnt_vw.OtherInformation
                       });


            foreach (var itm in qry)
            {
                Events.Add(new string[] {
                           itm.EventId,
                           itm.EmpIdNo,
                           itm.Year.ToString(),
                           itm.Month,
                           itm.DoctypeId.ToString(),
                           itm.DocumentStatusId.ToString(),
                           itm.Day.ToString(),
                           itm.AccountCode,
                           itm.AccountName,
                           itm.AccountAddress,
                           itm.AccountClass,
                           itm.ContactPerson,
                           itm.ContactPersonNo,
                           itm.ObjectiveCode,
                           itm.StoreCheckingResult,
                           itm.ProductPresentationResult,
                           itm.TotalAmount.ToString(),
                           itm.CompetitorActivities,
                           itm.WithOrder,
                           itm.NextCallDate.ToString(),
                           itm.PONum,
                           itm.IssuesAndConcerns,
                           itm.Delivery,
                           itm.Orders,
                           itm.SummaryLackingItems,
                           itm.MobileNo,
                           itm.Recommendation,
                           itm.TimeTable,
                           itm.Remarks,
                           itm.Brand,
                           itm.cFullCollection,
                           itm.cNoCollection,
                           itm.cPartialCollection,
                           itm.LineNum,
                           itm.OtherInformation
                });
            }

            return Events;
        }

        public static List<string[]> GetEventList(string month, int year, int day, string EmpId)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> Events = new List<string[]>();

            var qry = (from events_hdr in DATABASE.EventHdrs
                       from events_dtl in DATABASE.EventDtls
                       where events_hdr.EventID == events_dtl.EventID
                       && events_hdr.Month == month
                       && events_hdr.Year == year
                       && events_dtl.Day == day
                       && events_hdr.EmpIdNo == EmpId
                       select new
                       {
                           events_hdr.EventID,
                           events_hdr.EmpIdNo,
                           events_hdr.Month,
                           events_hdr.Year,
                           events_dtl.Day,
                           events_dtl.ObjectiveCode,
                           events_dtl.AccountCode
                       });

            foreach (var itm in qry)
            {
                Events.Add(new string[] {
                    itm.EventID,
                    itm.EmpIdNo,
                    itm.Month,
                    itm.Year.ToString(),
                    itm.Day.ToString(),
                    itm.ObjectiveCode,
                    itm.AccountCode
                });
            }

            return Events;
        }

        public static List<string[]> GetEventInfobyDateDtls(string LineNum)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> EventsDtls = new List<string[]>();

            var qry = (from evnts in DATABASE.EventDtl1
                       where evnts.LineNum == LineNum
                       select new
                       {
                           evnts.EventID,
                           evnts.LineNum,
                           evnts.Brand,
                           evnts.Amount,
                           evnts.CounterClerk,
                           evnts.ObjectiveCode
                       }).ToList();

            foreach (var item in qry)
            {
                EventsDtls.Add(new string[] {
                    item.EventID,
                    item.LineNum,
                    item.Brand,
                    item.Amount.ToString(),
                    item.CounterClerk,
                    item.ObjectiveCode
                });
            }

            return EventsDtls;
        }

        public static List<page_param.itemFile> getListOfBrand()
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<page_param.itemFile> Brand = new List<page_param.itemFile>();

            try
            {
                var brands = (from list_brand in DATABASE.armsII_vw_itemMasterFile
                              select new { list_brand.Brand }).Distinct();

                foreach (var itm in brands)
                {
                    Brand.Add(new page_param.itemFile()
                    {
                        brand = itm.Brand
                    });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                DATABASE.Dispose();
            }
            return Brand;
        }

        public static List<string[]> getListOfGroup()
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> Group = new List<string[]>();
            try
            {
                var groups = (from list_group in DATABASE.app_vw_itemgroup
                              select new { list_group.Code, list_group.Name }).ToList();

                foreach (var itm in groups)
                {
                    Group.Add(new string[] {
                        itm.Code,
                        itm.Code+" - "+itm.Name,
                        itm.Name
                    });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                DATABASE.Dispose();
            }
            return Group;
        }

        public static List<page_param.itemFile> getListOfItemCode()
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            var inventoryProducts = new List<page_param.itemFile>();

            try
            {
                var prod = (from list_invProd in DATABASE.armsII_vw_itemMasterFile_srp
                            select
                            new
                            {
                                list_invProd.ITEMCODE,
                                list_invProd.ItemDescription,
                                list_invProd.Brand,
                                list_invProd.ProdGrp,
                                list_invProd.SRP
                            }).ToList();

                foreach (var itm in prod)
                {
                    inventoryProducts.Add(new page_param.itemFile()
                    {
                        itemCode = itm.ITEMCODE,
                        itemDesc = itm.ItemDescription,
                        brand = itm.Brand,
                        prodGrp = itm.ProdGrp,
                        itemCodeDesc = itm.ITEMCODE + " - " + itm.ItemDescription,
                        itemPrice = itm.SRP
                    });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                DATABASE.Dispose();
            }
            return inventoryProducts;
        }

        public static List<page_param.itemFile> getListOfItemCode(string acctCode, DateTime? prevCountDate, DateTime? actualCountDate)
        {
            var inventoryProducts = new List<page_param.itemFile>();
            try
            {
                DataTable dt_from_db = SqlDbHelper.getDataDT("SELECT b.*,quantity from [MTC_Func_armsII_SellInPcs]('" + prevCountDate.Value.ToShortDateString() + "','" + actualCountDate.Value.ToShortDateString() + "','" + acctCode + "') a " +
                                                             "INNER JOIN armsII_vw_itemMasterFile_srp b ON a.itemCode=b.ITEMCODE COLLATE SQL_Latin1_General_CP1_CI_AS  " +
                                                             "UNION " +
                                                             "SELECT a.*,0 from armsII_vw_itemMasterFile_srp a " +
                                                             "WHERE a.ITEMCODE  COLLATE SQL_Latin1_General_CP1_CI_AS not in (select itemcode from [MTC_Func_armsII_SellInPcs]('" + prevCountDate.Value.ToShortDateString() + "','" + actualCountDate.Value.ToShortDateString() + "','" + acctCode + "'))");

                foreach (DataRow itm in dt_from_db.Rows)
                {
                    inventoryProducts.Add(new page_param.itemFile()
                    {

                        itemCode = itm["ItemCode"].ToString(),
                        itemDesc = itm["ItemDescription"].ToString(),
                        brand = itm["Brand"].ToString(),
                        prodGrp = itm["ProdGrp"].ToString(),
                        itemCodeDesc = itm["ItemCode"].ToString() + " - " + itm["ItemDescription"].ToString(),
                        itemPrice = (decimal)itm["SRP"],
                        sellin = (int)itm["quantity"]
                    });
                }


            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return inventoryProducts;
        }

        public static List<page_param.employeeDetail> getListOfSalesOfficerEmployee()
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            var SO_employee_list = new List<page_param.employeeDetail>();

            try
            {
                var Employees = (from vw_user_role in DATABASE.mtc_vw_User_Role_Details
                                 where vw_user_role.position == "SO"
                                 select
                                 new
                                 {
                                     empIDNo = vw_user_role.empIDNo,
                                     lastName = vw_user_role.lastName,
                                     firstName = vw_user_role.firstName,
                                     Position = vw_user_role.position,
                                     Branch = vw_user_role.branch,
                                     Channel = vw_user_role.channel,
                                     Area = vw_user_role.area
                                 });

                foreach (var itm in Employees)
                {
                    SO_employee_list.Add(new page_param.employeeDetail()
                    {
                        empIDNo = itm.empIDNo,
                        empFirstName = itm.firstName,
                        empLastName = itm.lastName,
                        empFullName = itm.firstName + " " + itm.lastName
                    });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                DATABASE.Dispose();
            }
            return SO_employee_list;
        }

        public static List<string[]> getListOfWhsIncharge(string acctCode)
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> whsIncharge = new List<string[]>();
            try
            {
                var whsIncharge_db = (from custOutletIncharge_db in DATABASE.custOutletIncharges
                                      where custOutletIncharge_db.acctCode == acctCode
                                      select
                                      new
                                      {
                                          custOutletIncharge_db.acctCode,
                                          custOutletIncharge_db.whsInchargeID,
                                          custOutletIncharge_db.whsInchargeFirstName,
                                          custOutletIncharge_db.whsInchargeMiddleName,
                                          custOutletIncharge_db.whsInchargeLastName,
                                          custOutletIncharge_db.whsInchargeContactNo
                                      });

                foreach (var itm in whsIncharge_db)
                {
                    whsIncharge.Add(new string[] {
                    itm.acctCode.ToString(),
                    itm.whsInchargeID.ToString(),
                    itm.whsInchargeFirstName.ToString(),
                    itm.whsInchargeMiddleName.ToString(),
                    itm.whsInchargeLastName.ToString(),
                    itm.whsInchargeContactNo.ToString(),
                    itm.whsInchargeID.ToString() + " - "  +itm.whsInchargeFirstName.ToString() + " " +itm.whsInchargeMiddleName.ToString()+" "+itm.whsInchargeLastName.ToString(),
                });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                DATABASE.Dispose();
            }
            return whsIncharge;
        }

        #region LIST OF ACCOUNT CODE

        //public static List<string[]> getListOfAccountCode()
        //{
        //    Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
        //    List<string[]> accounts = new List<string[]>();
        //    try
        //    {
        //        var db_accounts = (from a in DATABASE.arms2_vw_actveBusPrtnr.ToList()
        //                           from b in DATABASE.customerHeaders
        //                           where a.CardCode == b.acctCode
        //                           select new { a.CardCode,a.CardName, b.Pareto,b.ccaNum,b.area });

        //        foreach (var itm in db_accounts)
        //        {
        //            accounts.Add(new string[] {
        //                itm.CardCode.ToString(),
        //                itm.CardName.ToString(),
        //                itm.CardCode.ToString()+ ", "+itm.CardName.ToString(),
        //                itm.Pareto.ToString(),
        //                itm.ccaNum.ToString(),
        //                itm.area.ToString()
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string err = ex.Message;
        //    }
        //    finally
        //    {
        //        DATABASE.Dispose();
        //    }
        //    return accounts;
        //}

        //public static List<string[]> getListOfAccountCode1(string empIDNo)
        //{
        //    Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
        //    List<string[]> accounts = new List<string[]>();
        //    try
        //    {
        //        var db_accounts = (from a in DATABASE.arms2_vw_actveBusPrtnr
        //                           where a.empIdNo == empIDNo
        //                           select new { a }).ToList();

        //        var custHdr = (from a in DATABASE.customerHeaders
        //                       select a).ToList();

        //        var qry_22 = (from a in db_accounts
        //                     from b in custHdr
        //                     where a.a.CardCode == b.acctCode
        //                     select new { a, b }).ToList();

        //        qry_22 = qry_22.GroupBy(o => o.a.a.CardCode).Select(grp => grp.First()).ToList();

        //        foreach (var itm in qry_22)
        //        {
        //            accounts.Add(new string[] {
        //                itm.a.a.CardCode,
        //                itm.a.a.CardName,
        //                itm.a.a.CardCode+ ", "+itm.a.a.CardName,
        //                itm.b.Pareto,
        //                itm.b.ccaNum,
        //                itm.a.a.U_AREA,
        //                itm.a.a.Address
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string err = ex.Message;
        //    }
        //    finally
        //    {
        //        DATABASE.Dispose();
        //    }
        //    return accounts;
        //}


        public static List<string[]> getListOfAccountCode(string empIDNo)
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> accounts = new List<string[]>();
            try
            {
                var db_accounts = (from a in DATABASE.arms2_vw_inventoryScheduleAccounts
                                   where a.empIdNo == empIDNo
                                   select a);

                foreach (var itm in db_accounts)
                {
                    accounts.Add(new string[] {
                        itm.acctCode,
                        itm.acctName,
                        itm.acctCode+ ", "+itm.acctName,
                        itm.pareto,
                        itm.ccaNum,
                        itm.areaname,
                        itm.bussAdd,
                        itm.territoryName
                    });
                }

                accounts = accounts.OrderBy(o => o[1]).ToList();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                DATABASE.Dispose();
            }
            return accounts;
        }


        #endregion

        public static string stripNull(string val = null)
        {
            return val ?? "";
        }

        #region LIST OF OUTLETS

        public static List<string[]> getListOfOutlet(string ccaNum)
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> outlets = new List<string[]>();
            try
            {
                var db_outlets = (from custHdr_db in DATABASE.customerHeaders
                                  from custOutlet_db in DATABASE.custOutlets
                                  where custHdr_db.ccaNum == custOutlet_db.ccaNum
                                       && custOutlet_db.ccaNum == ccaNum
                                  select
                                  new
                                  {
                                      custOutlet_db.custOutletsID,
                                      custOutlet_db.ccaNum,
                                      custOutlet_db.name,
                                      custOutlet_db.location,
                                      custHdr_db.acctCode
                                  }).ToList();

                foreach (var itm in db_outlets)
                {
                    outlets.Add(new string[] {
                    itm.custOutletsID.ToString(),
                    itm.ccaNum.ToString(),
                    itm.name.ToString(),
                    itm.location.ToString(),
                    itm.acctCode.ToString()
                });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                DATABASE.Dispose();
            }
            return outlets;
        }

        public static List<string[]> getListOfOutlet(string ccaNum, string empIDNo)
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> outlets = new List<string[]>();

            try
            {
                var db_outlets = (from custHdr_db in DATABASE.customerHeaders
                                  from custOutlet_db in DATABASE.custOutlets
                                  where custHdr_db.ccaNum == custOutlet_db.ccaNum
                                       && custOutlet_db.ccaNum == ccaNum
                                       && custOutlet_db.inventoryIncharge == empIDNo
                                  select
                                  new
                                  {
                                      custOutlet_db.custOutletsID,
                                      custOutlet_db.ccaNum,
                                      custOutlet_db.name,
                                      custOutlet_db.location,
                                      custHdr_db.acctCode
                                  }).ToList();

                foreach (var itm in db_outlets)
                {
                    outlets.Add(new string[] {
                    itm.custOutletsID.ToString(),
                    itm.ccaNum.ToString(),
                    itm.name.ToString(),
                    itm.location.ToString(),
                    itm.acctCode.ToString()
                });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                DATABASE.Dispose();
            }
            return outlets;
        }

        #endregion


        //NEW CODE FOR COVERAGEHDR
        public static List<string[]> GetCoverageInfobyDate(string Eventmonth, int Eventday, int Eventyear, string soId)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> coverage = new List<string[]>();

            var qry = (from cvrge_vw in DATABASE.arms2_vw_soCoverage
                       where cvrge_vw.EmpIdNo == soId
                         && cvrge_vw.Month == Eventmonth && cvrge_vw.Year == Eventyear
                         && cvrge_vw.Day == Eventday
                       select new
                       {
                           cvrge_vw.EventID,
                           cvrge_vw.EmpIdNo,
                           cvrge_vw.Year,
                           cvrge_vw.Month,
                           cvrge_vw.DoctypeId,
                           cvrge_vw.LineNum,
                           cvrge_vw.Day,
                           cvrge_vw.AccountCode,
                           cvrge_vw.ContactPerson,
                           cvrge_vw.ContactPersonNo,
                           cvrge_vw.StoreChecking,
                           cvrge_vw.StoreCheckingResult,
                           cvrge_vw.ProductPresentationResult,
                           cvrge_vw.TotalAmount,
                           cvrge_vw.CompetitorActivities,
                           cvrge_vw.WithOrder,
                           cvrge_vw.NextCallDate,
                           cvrge_vw.PONum,
                           cvrge_vw.IssuesAndConcerns,
                           cvrge_vw.Delivery,
                           cvrge_vw.Orders,
                           cvrge_vw.SummaryLackingItems,
                           cvrge_vw.MobileNo,
                           cvrge_vw.Recommendation,
                           cvrge_vw.TimeTable,
                           cvrge_vw.Remarks,
                           cvrge_vw.cFullCollection,
                           cvrge_vw.cPartialCollection,
                           cvrge_vw.cNoCollection,
                           cvrge_vw.OtherInformation,
                           cvrge_vw.ObjectiveCode,
                           cvrge_vw.Brand,
                           cvrge_vw.PlannedAmount,
                           cvrge_vw.ActualAmount,
                           cvrge_vw.CounterClerk
                       }).ToList();

            foreach (var itm in qry)
            {
                coverage.Add(new string[]
                {
                       itm.EventID,
                       itm.EmpIdNo,
                       itm.Year.ToString(),
                       itm.Month,
                       itm.DoctypeId.ToString(),
                       itm.LineNum,
                       itm.Day.ToString(),
                       itm.AccountCode,
                       itm.ContactPerson,
                       itm.ContactPersonNo,
                       itm.StoreChecking,
                       itm.StoreCheckingResult,
                       itm.ProductPresentationResult,
                       itm.TotalAmount.ToString(),
                       itm.CompetitorActivities,
                       itm.WithOrder,
                       itm.NextCallDate.ToString(),
                       itm.PONum,
                       itm.IssuesAndConcerns,
                       itm.Delivery,
                       itm.Orders,
                       itm.SummaryLackingItems,
                       itm.MobileNo,
                       itm.Recommendation,
                       itm.TimeTable,
                       itm.Remarks,
                       itm.cFullCollection,
                       itm.cPartialCollection,
                       itm.cNoCollection,
                       itm.OtherInformation,
                       itm.ObjectiveCode,
                       itm.Brand,
                       itm.PlannedAmount.ToString(),
                       itm.ActualAmount.ToString(),
                       itm.CounterClerk
                });
            }
            return coverage;
        }

        public static List<string[]> GetCoverageInfo(string Eventmonth, int Eventday, int Eventyear, string soId)
        {

            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> coverage = new List<string[]>();

            /** var qry_1 = (from c_hdr in DATABASE.CoverageHdrs
                          from c_dtl in DATABASE.CoverageDtls
                          from c_dtl1 in DATABASE.CoverageDtl1
                          where c_hdr.EventID == c_dtl.EventID
                          && c_hdr.EmpIdNo == soId &&  **/

            var qry_1 = (from c in DATABASE.arms2_vw_AccountInCoverage
                         where c.EventID == c.EventID &&
                        c.EmpIdNo == soId && c.Month == Eventmonth
                        && c.Day == Eventday
                        && c.AccountCode == c.AccountCode
                         select new
                         {
                             c.AccountCode,
                             c.EmpIdNo,
                             c.Month,
                             c.Day,
                             c.Year,
                             c.EventID,
                             c.DoctypeId,
                             c.DocumentStatusId,
                             c.LineNum,
                             c.ContactPerson,
                             c.ContactPersonNo,
                             c.StoreChecking,
                             c.StoreCheckingResult,
                             c.ProductPresentationResult,
                             c.TotalAmount,
                             c.CompetitorActivities,
                             c.WithOrder,
                             c.NextCallDate,
                             c.PONum,
                             c.IssuesAndConcerns,
                             c.Delivery,
                             c.Orders,
                             c.SummaryLackingItems,
                             c.MobileNo,
                             c.Recommendation,
                             c.TimeTable,
                             c.Remarks,
                             c.cFullCollection,
                             c.cPartialCollection,
                             c.cNoCollection,
                             c.OtherInformation,
                             c.acctName,
                             c.acctClassfxn,
                             c.bussAdd,
                             c.FileAttachment
                         }).Distinct().ToList();

            var qry = (from cvrgehdr in DATABASE.CoverageHdrs
                       from cvrgedtl in DATABASE.CoverageDtls
                       from custhdr in DATABASE.arms2_vw_customerheader_lookup//DATABASE.customerHeaders
                       where cvrgehdr.EventID == cvrgedtl.EventID
                       && cvrgehdr.EmpIdNo == soId && cvrgehdr.Month == Eventmonth
                       && cvrgedtl.Day == Eventday
                       && cvrgedtl.AccountCode == custhdr.acctcode
                       select new
                       {
                           cvrgedtl.AccountCode,
                           cvrgehdr.EmpIdNo,
                           cvrgehdr.Month,
                           cvrgedtl.Day,
                           cvrgehdr.Year,
                           cvrgehdr.EventID,
                           cvrgehdr.DoctypeId,
                           cvrgehdr.DocumentStatusId,
                           cvrgedtl.LineNum,
                           cvrgedtl.ContactPerson,
                           cvrgedtl.ContactPersonNo,
                           cvrgedtl.StoreChecking,
                           cvrgedtl.StoreCheckingResult,
                           cvrgedtl.ProductPresentationResult,
                           cvrgedtl.TotalAmount,
                           cvrgedtl.CompetitorActivities,
                           cvrgedtl.WithOrder,
                           cvrgedtl.NextCallDate,
                           cvrgedtl.PONum,
                           cvrgedtl.IssuesAndConcerns,
                           cvrgedtl.Delivery,
                           cvrgedtl.Orders,
                           cvrgedtl.SummaryLackingItems,
                           cvrgedtl.MobileNo,
                           cvrgedtl.Recommendation,
                           cvrgedtl.TimeTable,
                           cvrgedtl.Remarks,
                           cvrgedtl.cFullCollection,
                           cvrgedtl.cPartialCollection,
                           cvrgedtl.cNoCollection,
                           cvrgedtl.OtherInformation,
                           custhdr.acctName,
                           custhdr.acctClassfxn,
                           custhdr.bussAdd,
                           cvrgedtl.FileAttachment
                       }).Distinct().ToList();

            foreach (var itm in qry_1)
            {
                coverage.Add(new string[]
                {
                           itm.AccountCode,
                           itm.EmpIdNo,
                           itm.Month,
                           itm.Day.ToString(),
                           itm.Year.ToString(),
                           itm.EventID,
                           itm.DoctypeId.ToString(),
                           itm.DocumentStatusId.ToString(),
                           itm.LineNum,
                           itm.ContactPerson,
                           itm.ContactPersonNo,
                           itm.StoreChecking,
                           itm.StoreCheckingResult,
                           itm.ProductPresentationResult,
                           itm.TotalAmount.ToString(),
                           itm.CompetitorActivities,
                           itm.WithOrder,
                           itm.NextCallDate.ToString(),
                           itm.PONum,
                           itm.IssuesAndConcerns,
                           itm.Delivery,
                           itm.Orders,
                           itm.SummaryLackingItems,
                           itm.MobileNo,
                           itm.Recommendation,
                           itm.TimeTable,
                           itm.Remarks,
                           itm.cFullCollection,
                           itm.cPartialCollection,
                           itm.cNoCollection,
                           itm.OtherInformation,
                           itm.acctName,
                           itm.bussAdd,
                           itm.acctClassfxn,
                           sub_coverages(itm.EventID, itm.LineNum,itm.EmpIdNo, int.Parse(itm.Month),itm.Day,itm.Year,itm.AccountCode).ToList().ToString(),
                           itm.FileAttachment
                });
            }
            return coverage;
        }

        public static List<string[]> getYear()
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> res = new List<string[]>();

            var qry = (from cvrgehdr in DATABASE.CoverageHdrs
                       select new
                       {
                           cvrgehdr.Year
                       });

            foreach (var itm in qry)
            {
                res.Add(new string[] {
                   itm.Year.ToString()
                
                });

            }

            DATABASE.Dispose();


            return res;
        }

        /*add by billy jay delima*/
        public class TempUploadCoveragePlan
        {
            public List<coverageplans> upload_data { get; set; }
        }

        public class coverageplans
        {
            public string Day { get; set; }
            public string AccountCode { get; set; }
            public string ContactPerson { get; set; }
            public string ContactPersonNo { get; set; }
            //public string ContactNum { get; set; }
            //public string HotelName { get; set; }
            //public string HotelNum { get; set; }
            public string ObjectiveCode { get; set; }
            public string Brand { get; set; }
            public string PlannedAmount { get; set; }
            //public string CounterClerkNo { get; set; }
            //public string CounterClerk { get; set; }
            //public string ProductPresented { get; set; }
            //public string StoreChecking { get; set; }
            public string IssuesAndConcerns { get; set; }
            public string DetailRemarks { get; set; }
        }
        /* end */

        #region TEMP VARIABLE COVRAGE

        public class CoverageHdrTmp
        {

            public string EventID { get; set; }
            public string EmpIdNo { get; set; }
            public string Year { get; set; }
            public string Month { get; set; }
            public string DoctypeId { get; set; }
            public string LineNum { get; set; }
            public string Day { get; set; }
            public string AccountCode { get; set; }
            public string AccountName { get; set; }
            public string AccountAddress { get; set; }
            public string AccountClass { get; set; }
            public string ContactPerson { get; set; }
            public string ContactPersonNo { get; set; }

            /*added by billy jay delima */
            public string HotelName { get; set; }
            public string HotelNum { get; set; }
            public string CounterClerkNo { get; set; }

            public string StoreChecking { get; set; }
            public string StoreCheckingResult { get; set; }
            public string ProductPresentationResult { get; set; }
            public string TotalAmount { get; set; }
            public string CompetitorActivities { get; set; }
            public string WithOrder { get; set; }
            public string NextCallDate { get; set; }
            public string PONum { get; set; }
            public string IssuesAndConcerns { get; set; }
            public string Delivery { get; set; }
            public string Orders { get; set; }
            public string SummaryLackingItems { get; set; }
            public string MobileNo { get; set; }
            public string Recommendation { get; set; }
            public string TimeTable { get; set; }
            public string Remarks { get; set; }
            public string cFullCollection { get; set; }
            public string cPartialCollection { get; set; }
            public string cNoCollection { get; set; }
            public string OtherInformation { get; set; }
            public string ObjectiveCode { get; set; }
            public string Brand { get; set; }
            public string PlannedAmount { get; set; }
            public string ActualAmount { get; set; }
            public string CounterClerk { get; set; }
            public string ProductPresented { get; set; }
            public string TimeIn { get; set; }
            public string Timeout { get; set; }
            public string freqVisit { get; set; }
            public List<sub_coverage> Sub_coverage { get; set; }
            public string isFinal { get; set; }
            public string stateDesc { get; set; }
            public int DocumentStatusId { get; set; }
            public string Attachment { get; set; }
            public bool hasCallreport { get; set; }

            public decimal totalcoveragesales { get; set; }

            public decimal? ColDatedCheck { get; set; }
            public decimal? ColPostDatedCheck { get; set; }
            public decimal? ColTotal { get; set; }
            public string ColRemarks { get; set; }

            public decimal? totalCollection { get; set; }

            public string CheckInAddress { get; set; }
            public string CheckOutAddress { get; set; }
            public string CheckInTime { get; set; }
            public string CheckOutTime { get; set; }


            public string estPlannedAmount { get; set; }
            public string estActualAmount { get; set; }
            public List<total_dtls> Total_dtls { get; set; }


            public class sub_coverage
            {
                public string Brand { get; set; }
                public string PlannedAmount { get; set; }
                public string ActualAmount { get; set; }
                public string CounterClerk { get; set; }
                public string ProductPresented { get; set; }
                public string ObjectiveCode { get; set; }
                public string Remarks { get; set; }
                public string CounterClerkNo { get; set; }
                public string dtlsrmks { get; set; }

                /* code inserted by billy jay delima*/
                public string inventoryCountID { get; set; }
            }


            public class total_dtls
            {
                public string EventID { get; set; }
                public string LineNum { get; set; }
                public string Day { get; set; }
                public string ObjectiveCode { get; set; }
                public string estPlannedAmount { get; set; }
                public string estActualAmount { get; set; }
            }



        }

        #endregion


        public static List<CoverageHdrTmp> GetCoverageInfo(string EventId, string Eventmonth, int Eventday, int Eventyear, string soId, string accoutcode)
        {

            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<CoverageHdrTmp> coverage = new List<CoverageHdrTmp>();

            var qry = (from cvrgehdr in DATABASE.CoverageHdrs
                       from cvrgedtl in DATABASE.CoverageDtls
                       join cust_hdr in DATABASE.arms2_vw_customerheader_lookup
                      on cvrgedtl.AccountCode equals cust_hdr.SapAcctCode into cvrage
                       from cust_hdr in cvrage.DefaultIfEmpty()
                       where cvrgehdr.EventID == EventId && cvrgedtl.EventID == EventId
                       && cvrgedtl.AccountCode == accoutcode && cvrgehdr.Month == Eventmonth
                       && cvrgehdr.Year == Eventyear && cvrgedtl.Day == Eventday
                       && cvrgehdr.EmpIdNo == soId
                       select new
                       {
                           cvrgehdr.EventID,
                           cvrgehdr.EmpIdNo,
                           cvrgehdr.Year,
                           cvrgehdr.Month,
                           cvrgehdr.DoctypeId,
                           cvrgehdr.DocumentStatusId,
                           cvrgedtl.LineNum,
                           cvrgedtl.Day,
                           cvrgedtl.AccountCode,
                           cust_hdr.acctName,
                           cust_hdr.bussAdd,
                           cust_hdr.acctClassfxn,
                           cvrgedtl.ContactPerson,
                           cvrgedtl.ContactPersonNo,
                           cvrgedtl.StoreChecking,
                           cvrgedtl.StoreCheckingResult,
                           cvrgedtl.ProductPresentationResult,
                           cvrgedtl.TotalAmount,
                           cvrgedtl.CompetitorActivities,
                           cvrgedtl.WithOrder,
                           cvrgedtl.NextCallDate,
                           cvrgedtl.PONum,
                           cvrgedtl.IssuesAndConcerns,
                           cvrgedtl.Delivery,
                           cvrgedtl.Orders,
                           cvrgedtl.SummaryLackingItems,
                           cvrgedtl.MobileNo,
                           cvrgedtl.Recommendation,
                           cvrgedtl.TimeTable,
                           cvrgedtl.Remarks,
                           cvrgedtl.cFullCollection,
                           cvrgedtl.cPartialCollection,
                           cvrgedtl.cNoCollection,
                           cvrgedtl.OtherInformation,
                           cvrgedtl.Numvisit,
                           cvrgedtl.FileAttachment,
                           cvrgedtl.CheckInAddress,
                           cvrgedtl.CheckInTime,
                           cvrgedtl.CheckOutAddress,
                           cvrgedtl.CheckOutTime,
                           cvrgedtl.ColDatedCheck,
                           cvrgedtl.ColPostDatedCheck,
                           cvrgedtl.ColTotal,
                           cvrgedtl.ColRemarks,
                           cvrgedtl.hasCallreport
                       }).Distinct().ToList();


            foreach (var itm in qry)
            {
                coverage.Add(new CoverageHdrTmp()
                {
                    EventID = itm.EventID,
                    EmpIdNo = itm.EmpIdNo,
                    Year = itm.Year.ToString(),
                    Month = itm.Month,
                    DoctypeId = itm.DoctypeId.ToString(),
                    LineNum = itm.LineNum,
                    Day = itm.Day.ToString(),
                    AccountCode = itm.AccountCode,
                    AccountName = itm.acctName,
                    AccountAddress = itm.bussAdd,
                    AccountClass = itm.acctClassfxn,
                    ContactPerson = itm.ContactPerson,
                    ContactPersonNo = itm.ContactPersonNo,
                    StoreChecking = itm.StoreChecking,
                    StoreCheckingResult = itm.StoreCheckingResult,
                    ProductPresentationResult = itm.ProductPresentationResult,
                    TotalAmount = itm.TotalAmount.ToString(),
                    CompetitorActivities = itm.CompetitorActivities,
                    WithOrder = itm.WithOrder,
                    NextCallDate = itm.NextCallDate.ToString(),
                    PONum = itm.PONum,
                    IssuesAndConcerns = itm.IssuesAndConcerns,
                    Delivery = itm.Delivery,
                    Orders = itm.Orders,
                    SummaryLackingItems = itm.SummaryLackingItems,
                    MobileNo = itm.MobileNo,
                    Recommendation = itm.Recommendation,
                    TimeTable = itm.TimeTable,
                    Remarks = itm.Remarks,
                    cFullCollection = itm.cFullCollection,
                    cPartialCollection = itm.cPartialCollection,
                    cNoCollection = itm.cNoCollection,
                    OtherInformation = itm.OtherInformation,
                    freqVisit = itm.Numvisit.ToString(),
                    Sub_coverage = sub_coverages(itm.EventID, itm.LineNum, itm.EmpIdNo, int.Parse(itm.Month), itm.Day, itm.Year, itm.AccountCode),
                    DocumentStatusId = (int)itm.DocumentStatusId,
                    Attachment = itm.FileAttachment,
                    CheckInAddress = itm.CheckInAddress,
                    CheckOutAddress = itm.CheckOutAddress,
                    CheckInTime = itm.CheckInTime.ToString(),
                    CheckOutTime = itm.CheckOutTime.ToString(),
                    Total_dtls = total_dtls(itm.EventID, itm.LineNum),
                    ColDatedCheck = itm.ColDatedCheck,
                    ColPostDatedCheck = itm.ColPostDatedCheck,
                    ColTotal = itm.ColTotal,
                    ColRemarks = itm.ColRemarks,
                    hasCallreport = itm.hasCallreport == "T" ? true : false
                });
            }
            return coverage;
        }


        public static List<CoverageHdrTmp.sub_coverage> sub_coverages(string EventID, string LineNum, string EmpIdNo, int month, int day, int year, string acctCode)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<CoverageHdrTmp.sub_coverage> s_info = new List<CoverageHdrTmp.sub_coverage>();

            DateTime coverage_date = new DateTime(year, month, day);

            var qry = (from sub in DATABASE.CoverageDtl1
                       where sub.EventID == EventID && sub.LineNum == LineNum

                       select new
                       {
                           sub.Brand,
                           sub.PlannedAmount,
                           sub.ActualAmount,
                           sub.CounterClerk,
                           sub.ProductPresented,
                           sub.ObjectiveCode,
                           sub.Remarks,
                           sub.CounterClerkNo,
                           sub.detailRemarks,
                           sub.inventoryCountId
                       });

            foreach (var itm in qry)
            {
                s_info.Add(new CoverageHdrTmp.sub_coverage()
                {
                    Brand = itm.Brand,
                    PlannedAmount = itm.PlannedAmount.ToString(),
                    CounterClerk = itm.CounterClerk,
                    ProductPresented = itm.ProductPresented,
                    ObjectiveCode = itm.ObjectiveCode,
                    ActualAmount = itm.ActualAmount.ToString(),
                    Remarks = itm.Remarks,
                    CounterClerkNo = itm.CounterClerkNo,
                    dtlsrmks = itm.detailRemarks,
                    inventoryCountID = itm.inventoryCountId//itm.ObjectiveCode != "INV" ? "" : inventorycountId.Count() >= 1 ? inventorycountId.Single() : ""
                });
            }


            return s_info;
        }

        public static List<CoverageHdrTmp.total_dtls> total_dtls(string EventID, string LineNum)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<CoverageHdrTmp.total_dtls> s_info = new List<CoverageHdrTmp.total_dtls>();

            var qry = (from sub in DATABASE.arms2_vw_estAmount_actAmount
                       where sub.EventID == EventID && sub.LineNum == LineNum

                       select new
                       {
                           sub.EventID,
                           sub.LineNum,
                           sub.Day,
                           sub.ObjectiveCode,
                           sub.estActualAmount,
                           sub.estPlannedAmount
                       });

            foreach (var itm in qry)
            {
                s_info.Add(new CoverageHdrTmp.total_dtls()
                {
                    EventID = itm.EventID,
                    LineNum = itm.LineNum,
                    Day = itm.Day.ToString(),
                    ObjectiveCode = itm.ObjectiveCode,
                    estActualAmount = itm.estActualAmount.ToString(),
                    estPlannedAmount = itm.estPlannedAmount.ToString()
                });
            }


            return s_info;
        }

        public class coverageTemp
        {
            public String AccountCode;
        }

        public class actvBusprtnr
        {
            public String CardCode;
            public String CardName;
            public String Address;
            public String acctClassfxn;
            public String Phone1;
            public String CntctPrsn;
        }

        public static List<string[]> GetSOAccountCode(string SoId, string month, int day, int year)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> accounts = new List<string[]>();

            IEnumerable<coverageTemp> qry_1 = (from c_hdr in DATABASE.CoverageHdrs
                                               from c_dtls in DATABASE.CoverageDtls
                                               where c_hdr.EmpIdNo == SoId && c_hdr.EventID == c_dtls.EventID
                                               && c_dtls.Day == day && c_hdr.Month == month && c_hdr.Year == year
                                               select new coverageTemp
                                {

                                    AccountCode = c_dtls.AccountCode

                                }).ToList();

            IEnumerable<actvBusprtnr> qry = (from acct in DATABASE.arms2_vw_actveBusPrtnr.ToList()
                                             where acct.empIdNo == SoId
                                             && !qry_1.Any(es => (es.AccountCode == acct.CardCode))
                                             select new actvBusprtnr
                                             {
                                                 CardCode = acct.CardCode,
                                                 CardName = acct.CardName,
                                                 Address = acct.Address,
                                                 Phone1 = acct.Phone1,
                                                 CntctPrsn = acct.CntctPrsn
                                             });

            foreach (var itm in qry)
            {
                accounts.Add(new string[] {
                    itm.CardCode,
                    itm.CardName,
                    itm.Address,
                    itm.acctClassfxn,
                    itm.Phone1,
                    itm.CntctPrsn
                });
            }

            return accounts;
        }


        public static List<string[]> GetSOAccountCode2(string SoId, string month, int day, int year)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> accounts = new List<string[]>();

            var qry = (from acct in DATABASE.arms2_vw_actveBusPrtnr
                       where acct.empIdNo == SoId

                       select new
                       {
                           acct.CardCode,
                           acct.CardName,
                           acct.Address,
                           acct.acctClassfxn,
                           acct.Phone1,
                           acct.CntctPrsn
                       });


            foreach (var itm in qry)
            {
                accounts.Add(new string[] {
                    itm.CardCode,
                    itm.CardName,
                    itm.Address,
                    itm.acctClassfxn,
                    itm.Phone1,
                    itm.CntctPrsn
                });
            }

            return accounts;
        }

        public static List<page_param.SoAccount> GetSOAccountCode3(string SoId, string month, int day, int year)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<page_param.SoAccount> accounts = new List<page_param.SoAccount>();

            var qry = (from acct in DATABASE.arms2_vw_actveBusPrtnr
                       where acct.empIdNo == SoId

                       select new
                       {
                           acct.CardCode,
                           acct.CardName,
                           acct.Address,
                           acct.acctClassfxn,
                           acct.Phone1,
                           acct.CntctPrsn
                       });

            foreach (var itm in qry)
            {
                accounts.Add(new page_param.SoAccount
                {
                    CardCode = itm.CardCode,
                    CardName = itm.CardName,
                    Address = itm.Address,
                    acctClassfxn = itm.acctClassfxn,
                    Phone1 = itm.Phone1,
                    CntctPrsn = itm.CntctPrsn

                });
            }

            return accounts;
        }

        public static List<page_param.SoAccount> getAccountinday(string SoId, string month, int day, int year)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<page_param.SoAccount> accounts = new List<page_param.SoAccount>();

            var qry = (from n in DATABASE.arms2_vw_LookupAccounts
                       where n.EmpIdNo == SoId && n.Month == month && n.day == day && n.Year == year 
                       select new
                       {

                           n.CardCode,
                           n.CardName,
                           n.Address,
                           n.acctClassfxn,
                           n.Phone1,
                           n.CntctPrsn,
                           n.HotelName,
                           n.HotelContactNumber
                       });

            foreach (var itm in qry)
            {
                accounts.Add(new page_param.SoAccount
                {
                    CardCode = itm.CardCode,
                    CardName = itm.CardName,
                    Address = itm.Address,
                    acctClassfxn = itm.acctClassfxn,
                    Phone1 = itm.Phone1,
                    CntctPrsn = itm.CntctPrsn,
                    HotelName = itm.HotelName,
                    HotelContactNum = itm.HotelContactNumber
                });
            }

            return accounts;
        }



        public static List<string[]> GetListofSOEventperASM(string ASMID)
        {

            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> res = new List<string[]>();

            var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                       where solist.ASM_ID == ASMID
                       select new
                       {

                           solist.EventID,
                           solist.EmpIdNo,
                           solist.FirstName,
                           solist.LastName,
                           solist.Month,
                           solist.Year,
                           solist.stateDesc

                       });

            foreach (var itm in qry)
            {
                res.Add(new string[] { 
                
                  itm.EventID,
                  itm.EmpIdNo,
                  itm.FirstName+" "+itm.LastName,
                  itm.Month,
                  itm.Year.ToString(),
                  itm.stateDesc
                });
            }

            DATABASE.Dispose();

            return res;
        }

        public static List<string[]> searhAll(List<string[]> list, string keyword)
        {
            List<string[]> res = new List<string[]>();

            foreach (var itm in list)
            {
                for (int i = 0; i < itm.Length; i++)
                {
                    if (itm[i].ToUpper().Contains(keyword.ToUpper())) { res.Add(itm); break; }
                }
            }

            return res;
        }

        public static List<string[]> GetListofSOEventperRSM(string ASMID, string filter_by, string username)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> res = new List<string[]>();
            var current_user = new _User(username);
            int roleid = getRole(username);
            if (filter_by == "APPROVED_DISAPPROVED")
            {
                //var qry_ = from a in DATABASE.arms2_vw_CoverageHdrStatus
                //           where a.isFinal == "Y"
                //           select a;
                //foreach (var itm in qry_)
                //{
                //    res.Add(new string[] { 
                //          itm.EventID,
                //          itm.EmpIdNo,
                //          itm.firstname+" "+itm.lastname,
                //          itm.Month,
                //          itm.Year.ToString(),
                //          itm.statedesc
                //        });
                //}


                var final_doc_statuses = DATABASE.approvalStates.Where(o => o.docType == (int)Globals.InfoType.CalendarEvent && o.isFinal == "Y").Select(o => o.stateID).ToList();
                if (current_user.Roles.Any(o => o.Position == "SPRUSER"))
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where (final_doc_statuses.Contains((int)solist.DocumentStatusId.Value) || solist.DocumentStatusId.Value == 1)
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc

                               });
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                        });
                    }
                }
                else if (roleid == 17)
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where solist.EmpIdNo == ASMID &&
                                 (final_doc_statuses.Contains((int)solist.DocumentStatusId.Value) || solist.DocumentStatusId.Value == 1) &&
                                 solist.roleId == roleid
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc

                               });
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                        });
                    }
                }
                else if (roleid == 2)
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where solist.ASM_ID == ASMID &&
                                 (final_doc_statuses.Contains((int)solist.DocumentStatusId.Value) || solist.DocumentStatusId.Value == 1) && solist.roleId == 17 && solist.RoleCode == "SO"
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc

                               });
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                    });
                    }
                }
                else
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where solist.CHM_ID == ASMID &&
                                (final_doc_statuses.Contains((int)solist.DocumentStatusId.Value) || solist.DocumentStatusId.Value == 1) && solist.roleId == 2 && solist.RoleCode == "ASM"
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc

                               });
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                    });
                    }
                }
            }
            else if (filter_by == "FOR_ASM_APPROVAL")
            {
                //  var final_doc_statuses = DATABASE.approvalStates.Where(o => o.docType == (int)Globals.InfoType.CalendarEvent && o.isFinal == "Y").Select(o => o.stateID);
                if (roleid == 2)
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where solist.ASM_ID == ASMID &&
                                 solist.DocumentStatusId.Value == 2
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc
                               });
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                    });
                    }
                }
                else
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where solist.CHM_ID == ASMID &&
                                 solist.DocumentStatusId.Value == 9
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc

                               });
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                    });
                    }
                }
            }
            else if (filter_by == "FOR_RSM_APPROVAL")
            {
                //  var final_doc_statuses = DATABASE.approvalStates.Where(o => o.docType == (int)Globals.InfoType.CalendarEvent && o.isFinal == "Y").Select(o => o.stateID);
                if (roleid == 8)
                {
                    var qry = (from solist in DATABASE.arms2_vw_CoverageHdrStatus
                               where solist.roleid==roleid && solist.isFinal =="N" && solist.isStart=="N"
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.firstname,
                                   solist.lastname,
                                   solist.Month,
                                   solist.Year,
                                   solist.statedesc
                               });
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.firstname+" "+itm.lastname,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.statedesc
                    });
                    }
                }
            }
            DATABASE.Dispose();

            return res;
        }

        public static List<string[]> GetListofSOEventperASM(string ASMID, string filter_by, string username)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> res = new List<string[]>();
            var current_user = new _User(username);
            int roleid = getRole(username);
            

            if (filter_by == "APPROVED_DISAPPROVED")
            {
                var final_doc_statuses = DATABASE.approvalStates.Where(o => o.docType == (int)Globals.InfoType.CalendarEvent && o.isFinal == "Y").Select(o => o.stateID).ToList();
                if (current_user.Roles.Any(o => o.Position == "SPRUSER"))
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where  (final_doc_statuses.Contains((int)solist.DocumentStatusId.Value) || solist.DocumentStatusId.Value == 1)
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc

                               });
                    qry = qry.GroupBy(o => o.EventID).Select(grp => grp.FirstOrDefault());
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                        });
                    }
                }
                else if (roleid == 17)
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where solist.EmpIdNo == ASMID &&
                                 (final_doc_statuses.Contains((int)solist.DocumentStatusId.Value) || solist.DocumentStatusId.Value == 1) &&
                                 solist.roleId == roleid
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc

                               });
                    qry = qry.GroupBy(o => o.EventID).Select(grp => grp.FirstOrDefault());
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                        });
                    }
                }
                else if (roleid == 2)
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where solist.ASM_ID == ASMID &&
                                 (final_doc_statuses.Contains((int)solist.DocumentStatusId.Value) || solist.DocumentStatusId.Value == 1) && solist.roleId == 17 && solist.RoleCode == "SO"
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc

                               });
                    qry = qry.GroupBy(o => o.EventID).Select(grp => grp.FirstOrDefault());
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                    });
                    }
                }
                else if (roleid == 5)
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where solist.VPSALES_ID == ASMID &&
                                (final_doc_statuses.Contains((int)solist.DocumentStatusId.Value) || solist.DocumentStatusId.Value == 1) && solist.RoleCode=="CHM"
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc

                               });
                    qry = qry.GroupBy(o => o.EventID).Select(grp => grp.FirstOrDefault());
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                    });
                    }
                }
                else
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where solist.CHM_ID == ASMID &&
                                (final_doc_statuses.Contains((int)solist.DocumentStatusId.Value) || solist.DocumentStatusId.Value == 1) && (new List<string>(){ "ASM","SO" }).Contains(solist.RoleCode) //&& solist.roleId == 2 && solist.RoleCode == "ASM"
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc

                               });
                    qry = qry.GroupBy(o => o.EventID).Select(grp => grp.FirstOrDefault());
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                    });
                    }
                }
            }
            else if (filter_by == "FOR_ASM_APPROVAL")
            {
                //  var final_doc_statuses = DATABASE.approvalStates.Where(o => o.docType == (int)Globals.InfoType.CalendarEvent && o.isFinal == "Y").Select(o => o.stateID);
                if (roleid == 2)
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where solist.ASM_ID == ASMID &&
                                 solist.DocumentStatusId.Value == 2
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc
                               });
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                    });
                    }
                }
                else
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where solist.CHM_ID == ASMID &&
                                 solist.DocumentStatusId.Value == 13 
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc

                               });
                    qry = qry.GroupBy(o => o.EventID).Select(grp => grp.FirstOrDefault());
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                    });
                    }

                    
                }
            }
            else if (filter_by == "FOR_RSM_APPROVAL")
            {
                List<string> branches = new List<string>();
                
                //  var final_doc_statuses = DATABASE.approvalStates.Where(o => o.docType == (int)Globals.InfoType.CalendarEvent && o.isFinal == "Y").Select(o => o.stateID);
                if (roleid == 8)
                {
                //    foreach (var itm in current_user.Roles.Where(o => o.Position == "CHM").Single().Region)
                //    {
                //        branches.Add(itm);
                //    }

                    var qry_branches = (from a in DATABASE.apprvrDesigs
                                        from b in DATABASE.userHeaders
                                        where a.counterId == b.counterId
                                            && b.empIdNo == current_user.EmployeeIdNo
                                            && a.roleID == roleid && a.channel.Contains("TRADE")
                                        select a.branch);

                    foreach (var branch in qry_branches.Distinct())
                    {
                        branches.Add(branch);
                    }

                    var qry = (from solist in DATABASE.arms2_vw_CoverageHdrStatus
                               where solist.roleid == roleid && solist.isFinal == "N" && solist.isStart == "N"
                               && branches.Contains(solist.branch) && !solist.statedesc.StartsWith("AMENDED")
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.firstname,
                                   solist.lastname,
                                   solist.Month,
                                   solist.Year,
                                   solist.statedesc
                               }).ToList();
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.firstname+" "+itm.lastname,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.statedesc
                    });
                    }
                }


            }
            else if (filter_by == "FOR_VP_SALES_APPROVAL")
            {
                var qry = (from solist in DATABASE.arms2_vw_CoverageHdrStatus
                           where solist.roleid == roleid && solist.isFinal == "N" && solist.isStart == "N" && !solist.statedesc.StartsWith("AMENDED")
                           select new
                           {
                               solist.EventID,
                               solist.EmpIdNo,
                               solist.firstname,
                               solist.lastname,
                               solist.Month,
                               solist.Year,
                               solist.statedesc
                           }).ToList();

                qry = qry.GroupBy(o => new { o.EmpIdNo, o.EventID, o.firstname, o.lastname, o.Month, o.statedesc, o.Year }).Select(grp => grp.FirstOrDefault()).ToList();
                foreach (var itm in qry)
                {
                    res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.firstname+" "+itm.lastname,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.statedesc
                    });
                }

            }
            else if (filter_by == "ALL")
            {
                if (roleid == 8)
                {
                    var qry = (from solist in DATABASE.arms2_vw_asmViewSoCoverage
                               where solist.CHM_ID == ASMID &&
                                 solist.DocumentStatusId.Value == 13
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.FirstName,
                                   solist.LastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc

                               });
                    qry = qry.GroupBy(o => o.EventID).Select(grp => grp.FirstOrDefault());
                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.FirstName+" "+itm.LastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                    });
                    }
                }
                if (roleid == 5)
                {
                    int? counterid = 10000;
                    var qry_username = DATABASE.userHeaders.SingleOrDefault(o => o.userName == username);
                    if (qry_username != null) counterid = qry_username.counterId;
                    if (DATABASE.apprvrDesigs.Any(o => o.roleID == roleid && o.counterId==counterid && (o.channel.StartsWith("LIS") || o.channel.StartsWith("VIS") || o.channel.StartsWith("VTS") || o.channel.StartsWith("LTS"))))
                    {
                        var qry1 = (from solist in DATABASE.arms2_vw_CoverageHdrStatus
                                    where solist.roleid == roleid && solist.isFinal == "N" && solist.isStart == "N" && !solist.statedesc.StartsWith("AMENDED")
                                    select new
                                    {
                                        solist.EventID,
                                        solist.EmpIdNo,
                                        solist.firstname,
                                        solist.lastname,
                                        solist.Month,
                                        solist.Year,
                                        solist.statedesc
                                    }).ToList();

                        qry1 = qry1.GroupBy(o => new { o.EmpIdNo, o.EventID, o.firstname, o.lastname, o.Month, o.statedesc, o.Year }).Select(grp => grp.FirstOrDefault()).ToList();
                        foreach (var itm in qry1)
                        {
                            res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.firstname+" "+itm.lastname,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.statedesc
                    });
                        }
                    }
                }
            }
            DATABASE.Dispose();

            return res;
        }

        public static List<CoverageHdrTmp> GetChangesDtlsbySO(string soId, string month, int year, string EventId)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<CoverageHdrTmp> changes = new List<CoverageHdrTmp>();

            var qry = (from apprv_changes in DATABASE.arms2_vw_coveragechanges
                       where apprv_changes.EmpIdNo == soId && apprv_changes.Month == month
                       && apprv_changes.Year == year
                       && apprv_changes.EventID == EventId
                       select new
                       {

                           apprv_changes.EventID,
                           apprv_changes.EmpIdNo,
                           apprv_changes.Month,
                           apprv_changes.Day,
                           apprv_changes.Year,
                           apprv_changes.AccountCode,
                           apprv_changes.LineNum
                       });
            qry = qry.GroupBy(o => new { o.LineNum, o.EmpIdNo, o.AccountCode, o.Day, o.EventID, o.Month, o.Year }).Select(grp => grp.FirstOrDefault());
            foreach (var itm in qry)
            {
                changes.Add(new CoverageHdrTmp()
                {
                    EventID = itm.EventID,
                    EmpIdNo = itm.EmpIdNo,
                    Month = itm.Month,
                    Day = itm.Day.ToString(),
                    Year = itm.Year.ToString(),
                    AccountCode = itm.AccountCode

                });

            }

            return changes;
        }

        public static List<string[]> GetListofCoveragechangesbySO(string ASMID, string username)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> res = new List<string[]>();
            var current_user = new _User(username);
            int roleid = getRole(username);

            if (roleid == 2)
            {
                var qry = (from solist in DATABASE.arms2_vw_coveragechanges
                           where solist.ASM_ID == ASMID && solist.AcctStatus == 6
                           select new
                           {
                               solist.EventID,
                               solist.EmpIdNo,
                               solist.firstname,
                               solist.lastName,
                               solist.Month,
                               solist.Year,
                               solist.stateDesc
                           }).Distinct();

                foreach (var itm in qry)
                {
                    res.Add(new string[] { 
                      itm.EventID,
                      itm.EmpIdNo,
                      itm.firstname+" "+itm.lastName,
                      itm.Month,
                      itm.Year.ToString(),
                      itm.stateDesc
                    });
                }
            }
            else if (roleid == 8)
            {  
                //List<string> branches=new List<string>();

                //var qry_branches = (from a in DATABASE.apprvrDesigs
                //                        from b in DATABASE.userHeaders
                //                        where a.counterId == b.counterId
                //                            && b.empIdNo == current_user.EmployeeIdNo
                //                            && a.roleID == roleid && a.channel.Contains("TRADE")
                //                        select a.branch);

                //    foreach (var branch in qry_branches.Distinct())
                //    {
                //        branches.Add(branch);
                //    }

                //    var qry = (from solist in DATABASE.arms2_vw_coveragechanges
                //               where solist.AcctStatus == 16 
                //               && branches.Contains(solist.branch)
                //               select new
                //               {
                //                   solist.EventID,
                //                   solist.EmpIdNo,
                //                   solist.firstname,
                //                   solist.lastName,
                //                   solist.Month,
                //                   solist.Year,
                //                   solist.stateDesc
                //               }).ToList();
                var qry = (from solist in DATABASE.arms2_vw_coveragechanges
                           where solist.CHM_ID == ASMID && solist.AcctStatus == 16
                           select new
                           {
                               solist.EventID,
                               solist.EmpIdNo,
                               solist.firstname,
                               solist.lastName,
                               solist.Month,
                               solist.Year,
                               solist.stateDesc
                           }).Distinct();

                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.firstname+" "+itm.lastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                        });


                    } 

                res = res.GroupBy(o => o[0]).Select(grp => grp.First()).ToList();
            }
            else if(roleid==5)
            {
                int? counterid = 10000;
                var qry_username = DATABASE.userHeaders.SingleOrDefault(o => o.userName == username);
                if (qry_username != null) counterid = qry_username.counterId;
                if (DATABASE.apprvrDesigs.Any(o => o.roleID == roleid && o.counterId==counterid && (o.channel.StartsWith("LIS") || o.channel.StartsWith("VIS") || o.channel.StartsWith("VTS") || o.channel.StartsWith("LTS"))))
                {
                    var qry = (from solist in DATABASE.arms2_vw_coveragechanges
                               where solist.roleid == roleid
                               select new
                               {
                                   solist.EventID,
                                   solist.EmpIdNo,
                                   solist.firstname,
                                   solist.lastName,
                                   solist.Month,
                                   solist.Year,
                                   solist.stateDesc
                               }).ToList();

                    foreach (var itm in qry)
                    {
                        res.Add(new string[] { 
                          itm.EventID,
                          itm.EmpIdNo,
                          itm.firstname+" "+itm.lastName,
                          itm.Month,
                          itm.Year.ToString(),
                          itm.stateDesc
                        });


                    }

                    res = res.GroupBy(o => o[0]).Select(grp => grp.First()).ToList();
                }
                //var qry = (from solist in DATABASE.arms2_vw_coveragechanges
                //           where solist.CHM_ID == ASMID && solist.AcctStatus == 10
                //           select new
                //           {

                //               solist.EventID,
                //               solist.EmpIdNo,
                //               solist.firstname,
                //               solist.lastName,
                //               solist.Month,
                //               solist.Year,
                //               solist.stateDesc

                //           }).Distinct();
                //if (qry == null) return res;
                //foreach (var itm in qry)
                //{
                //    res.Add(new string[] { 
                
                //  itm.EventID,
                //  itm.EmpIdNo,
                //  itm.firstname+" "+itm.lastName,
                //  itm.Month,
                //  itm.Year.ToString(),
                //  itm.stateDesc

                
                //});

                //}
            }
            DATABASE.Dispose();

            return res;
        }

        public static int getRole(string username)
        {
            var DATABASE = new ARMS_W.Models.ARMSTestEntities();
            var qry = (from user in DATABASE.userHeaders
                       from approverDesig in DATABASE.apprvrDesigs
                       where user.userName == username
                       && user.counterId == approverDesig.counterId
                       select approverDesig).ToList();

            DATABASE.Dispose();
            if(qry.Select(p=>p.roleID).Contains(5)) return 5;
            else if (qry.Select(p => p.roleID).Contains(8)) return 8;
            else if (qry.Select(p => p.roleID).Contains(2)) return 2;
            else return 17;
        }


        public static List<CoverageHdrTmp> getstatus(string soId, string Month, int year)
        {

            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<CoverageHdrTmp> coveragestatus = new List<CoverageHdrTmp>();

            var qry = (from covrage in DATABASE.CoverageHdrs
                       from apprv in DATABASE.approvalStates
                       where covrage.DoctypeId == apprv.docType
                       && covrage.DocumentStatusId == apprv.stateID
                       && covrage.EmpIdNo == soId && covrage.Month == Month && covrage.Year == year
                       select new
                       {

                           covrage.EmpIdNo,
                           covrage.DocumentStatusId,
                           covrage.EventID,
                           apprv.isFinal,
                           apprv.stateDesc
                       });

            foreach (var itm in qry)
            {
                coveragestatus.Add(new CoverageHdrTmp()
                {

                    EmpIdNo = itm.EmpIdNo,
                    DocumentStatusId = (int)itm.DocumentStatusId,
                    EventID = itm.EventID,
                    isFinal = itm.isFinal,
                    stateDesc = itm.stateDesc


                });
                break;
            }

            DATABASE.Dispose();

            return coveragestatus;
        }

        //get eventID

        public static string getEventId(string soId, string month, int year)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            string eventId = "";
            try
            {
                eventId = DATABASE.CoverageHdrs.Single(p => p.EmpIdNo == soId && p.Month == month && p.Year == year).EventID;
            }
            catch (Exception ex)
            {
                eventId = "";
            }
            DATABASE.Dispose();
            return eventId;
        }



        //this check whether an account is already exist in particular day

        //public static List<CoverageHdrTmp> CheckAccountExist(string soId, string month, int day, int year )
        //{
        //    ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
        //    List<CoverageHdrTmp> coveragestatus = new List<CoverageHdrTmp>();


        //    var qry=(from coverage in DATABASE.CoverageHdrs
        //             from Coverah

        //}



        public static string UploadTempDirectory
        {
            get
            {
                if (ARMS_W.SkelClass.Globals.Settings.Database.ToUpper() == "ARMS")
                {
                    return "d:\\ARMS_W\\COVERAGE\\Temp\\";

                }
                else
                    return "d:\\ARMS_W\\COVERAGE\\Temp\\";
            }
        }


        public class GetInvenroty
        {
            public string ccaNum { get; set; }
            public string acctCode { get; set; }
            public string bussAdd { get; set; }
            public string acctName { get; set; }
            public string areaname { get; set; }
            public string pareto { get; set; }
            public string territoryName { get; set; }
            public string empIdNo { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string prevCountDate { get; set; }
            public string startCountDate { get; set; }
            public int Day { get; set; }
            public int month { get; set; }
            public int year { get; set; }
            public string Contactperson { get; set; }
            public string ContactpersonNo { get; set; }
        }

        public static List<string[]> GetInventoryperSo(int month, int year, string soId)
        {
            List<string[]> res = new List<string[]>();
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            var qry = (from inv_cnt in DATABASE.arms2_vw_inventoryScheduleAccounts
                       where inv_cnt.startCountDate.Value.Month == month
                       && inv_cnt.startCountDate.Value.Year == year
                       && inv_cnt.empIdNo == soId
                       select new
                       {

                           inv_cnt.empIdNo,
                           inv_cnt.acctName,
                           inv_cnt.bussAdd
                       });
            foreach (var itm in qry)
            {
                res.Add(new string[] { 
                
                    itm.empIdNo,
                    itm.acctName,
                    itm.bussAdd
                });
            }

            return res;
        }


        public static List<GetInvenroty> GetNextScheduleInventoryCount(int Month, int year, string soId)
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            string castedMonth = Month.ToString();

            List<GetInvenroty> nextcount = new List<GetInvenroty>();

            var qry = (from inv in DATABASE.arms2_vw_inventoryScheduleAccounts
                       where !(from b in DATABASE.arms2_vw_soCoverage
                               where b.Month == castedMonth && b.Year == year
                      && b.EmpIdNo == soId && b.ObjectiveCode == "INV"
                               select b.AccountCode).Contains(inv.acctCode)
                           && inv.startCountDate.Value.Month == Month
                       && inv.startCountDate.Value.Year == year
                       && inv.empIdNo == soId

                       select new
                       {
                           inv.empIdNo,
                           inv.acctCode,
                           inv.acctName,
                           inv.bussAdd,
                           inv.startCountDate,
                           inv.ccaNum,
                           inv.lastName,
                           inv.pareto,
                           inv.prevCountDate,
                           inv.CntctPrsn,
                           inv.Phone1
                       }).ToList();

            foreach (var itm in qry)
            {
                nextcount.Add(new GetInvenroty()
                {
                    empIdNo = itm.empIdNo,
                    acctCode = itm.acctCode,
                    acctName = itm.acctName,
                    bussAdd = itm.bussAdd,
                    Day = itm.startCountDate.Value.Day,
                    year = itm.startCountDate.Value.Year,
                    month = itm.startCountDate.Value.Month,
                    startCountDate = itm.startCountDate.Value.ToShortDateString(),
                    ccaNum = itm.ccaNum,
                    lastName = itm.lastName,
                    pareto = itm.pareto,
                    prevCountDate = itm.prevCountDate.Value.ToShortDateString(),
                    Contactperson = itm.CntctPrsn,
                    ContactpersonNo = itm.Phone1
                });
            }

            DATABASE.Dispose();

            return nextcount;
        }

        public static Interface.Event.CoverageHdr GetCoverageDetails(string EventID, int Day, string month, int year, string AccountCode)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            Interface.Event.CoverageHdr covrageDtl = new Objects.Event.CoverageHdr();

            CoverageHdrTmp pending_changes = new CoverageHdrTmp();


            if (EventID.Trim() == "" && Day.ToString().Trim() == "" && year.ToString().Trim() == "" && AccountCode.Trim() == "")
                throw new Exception("Err.");

            var qry = from cvrgDtl in DATABASE.CoverageDtls
                      where cvrgDtl.EventID == EventID
                      && cvrgDtl.Day == Day
                      && cvrgDtl.AccountCode == AccountCode

                      select cvrgDtl;

            var qry_pending_changes =
                  from cvrgDtl_changes in DATABASE.CoverageDataChanges
                  where (cvrgDtl_changes.DocTypeId == (int)Globals.InfoType.CalendarEvent)
                  && cvrgDtl_changes.DocId == EventID
                  select cvrgDtl_changes;

            bool has_pending_changes = false;

            foreach (var itm_changes in qry_pending_changes)
            {

                has_pending_changes = true;
                pending_changes = (CoverageHdrTmp)SkelClass.Utils.JsonToObject<CoverageHdrTmp>(itm_changes.DataChanges);

            }

            if (has_pending_changes == false)
            {

                foreach (var itm in qry)
                {
                    #region ORIGINAL DATA

                    covrageDtl.AccountCode = itm.AccountCode;
                    covrageDtl.EventID = itm.EventID;
                    covrageDtl.Day = itm.Day;
                    covrageDtl.LineNum = itm.LineNum;
                    covrageDtl.ContactPerson = itm.ContactPerson;
                    covrageDtl.ContactPersonNo = itm.ContactPersonNo;
                    covrageDtl.StoreChecking = itm.StoreChecking;
                    covrageDtl.StoreCheckingResult = itm.StoreCheckingResult;
                    covrageDtl.ProductPresentationResult = itm.ProductPresentationResult;
                    covrageDtl.CompetitorActivities = itm.CompetitorActivities;
                    covrageDtl.WithOrder = itm.WithOrder;
                    covrageDtl.NextCallDate = itm.NextCallDate.ToString();
                    covrageDtl.IssuesAndConcerns = itm.IssuesAndConcerns;
                    covrageDtl.Delivery = itm.Delivery;
                    covrageDtl.WithOrder = itm.Orders;
                    covrageDtl.SummaryLackingItems = itm.SummaryLackingItems;
                    covrageDtl.Recommendation = itm.Recommendation;
                    covrageDtl.TimeTable = itm.TimeTable;
                    covrageDtl.Remarks = itm.Remarks;
                    covrageDtl.cFullCollection = itm.cFullCollection;
                    covrageDtl.cPartialCollection = itm.cPartialCollection;
                    covrageDtl.cNoCollection = itm.cNoCollection;
                    covrageDtl.OtherInformation = itm.OtherInformation;
                    covrageDtl.isPlanned = itm.isPlanned;
                    // covrageDtl.Tmein = (DateTime)itm.Tmein;
                    // covrageDtl.Tmeout = (DateTime)itm.Tmeout;
                    covrageDtl.Numvisit = (int)itm.Numvisit;
                    covrageDtl.HotelName = itm.HotelName;
                    covrageDtl.HotelContactNum = itm.HotelContactNumber;
                    covrageDtl.isDeleted = itm.IsDeleted;
                    covrageDtl.AcctStatus = (int)itm.AcctStatus;
                    covrageDtl.IsAnEdit = itm.IsAnEdit;
                    covrageDtl.RmrkChanges = itm.RmrkChanges;
                    covrageDtl.Attachment = itm.FileAttachment;
                    covrageDtl.hasCallreport = itm.hasCallreport;
                    covrageDtl.ColPostDatedCheck = itm.ColPostDatedCheck;
                    covrageDtl.ColDatedCheck = itm.ColDatedCheck;
                    covrageDtl.ColRemarks = itm.ColRemarks;
                    covrageDtl.ColTotal = itm.ColTotal;

                    #endregion
                }

            }

            DATABASE.Dispose();
            return covrageDtl;

        }


        public class ItemMasterFile
        {
            public string ItemCode { get; set; }
            public string Brand { get; set; }
            public string Productgrp { get; set; }
            public string ItemDesc { get; set; }
            public string SRP { get; set; }
            public string OUM { get; set; }
            public string Thickness { get; set; }
            public string Length { get; set; }
            public string Width { get; set; }
            public string Multiplier { get; set; }
            public string ItemClass { get; set; }
            public string PLName { get; set; }

        }


        public static List<ItemMasterFile> getItmMasterFileDtls()
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new List<ItemMasterFile>();

            var qry = (from a in DATABASE.armsII_vw_itemMasterFile
                       select new
                       {
                           a.ITEMCODE,
                           a.ItemDescription,
                           a.ProdGrp,
                           a.Brand,
                           a.SRP,
                           a.U_PL_UOM,
                           a.U_GThickness,
                           a.U_GMultiplier,
                           a.U_GWidth,
                           a.U_GLength,
                           a.U_PL_NAME,
                           a.U_ItemClass

                       }).ToList();

            foreach (var itm in qry)
            {
                res.Add(new ItemMasterFile()
                {

                    ItemCode = itm.ITEMCODE,
                    Brand = itm.Brand,
                    Productgrp = itm.ProdGrp,
                    ItemDesc = itm.ItemDescription,
                    SRP = itm.SRP.ToString(),
                    OUM = itm.U_PL_UOM,
                    Thickness = itm.U_GThickness.ToString(),
                    Length = itm.U_GLength.ToString(),
                    Width = itm.U_GWidth.ToString(),
                    Multiplier = itm.U_GMultiplier.ToString(),
                    ItemClass = itm.U_ItemClass,
                    PLName = itm.U_PL_NAME

                });
            }

            DATABASE.Dispose();

            return res;
        }

        public static List<string[]> getBrand()
        {
            var res = new List<string[]>();
            var DATABASE = new ARMS_W.Models.ARMSTestEntities();
            string desc = "";
            var qry = (from a in DATABASE.app_vw_itemBrand
                       select new
                       {

                           a.FldValue,
                           a.Descr
                       });

            foreach (var itm in qry)
            {
                desc = itm.Descr.ToUpper() == "AIRDRIED" ? "ECO LUMBER" : itm.Descr;
                res.Add(new string[] {
                
                  itm.FldValue,
                  desc,
                  itm.FldValue+"-"+desc
                
                });

            }
            return res;
        }

        public static List<page_param.SoAccount> LookUpAccount(string SoId, string month, int day, int year)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<page_param.SoAccount> accounts = new List<page_param.SoAccount>();

            IEnumerable<page_param.SoAccount> qry_1 = (from c_hdr in DATABASE.CoverageHdrs
                                                       from c_dtls in DATABASE.CoverageDtls
                                                       where c_hdr.EmpIdNo == SoId && c_hdr.EventID == c_dtls.EventID
                                                       && c_dtls.Day == day && c_hdr.Month == month && c_hdr.Year == year
                                                       select new page_param.SoAccount
                                               {
                                                   CardCode = c_dtls.AccountCode
                                               }).ToList();

            var qeqweqwewq = (from adad in DATABASE.arms2_vw_actveBusPrtnr select adad).ToList();

            IEnumerable<page_param.SoAccount> qry = (from acct in DATABASE.arms2_vw_actveBusPrtnr.ToList()
                                                     where (acct.empIdNo == SoId || acct.empIdNo == "0")
                                                     && !qry_1.Any(es => (es.CardCode == acct.CardCode))
                                                     select new page_param.SoAccount
                                             {
                                                 CardCode = acct.CardCode,
                                                 CardName = acct.CardName,
                                                 Address = acct.Address,
                                                 Phone1 = acct.Phone1,
                                                 CntctPrsn = acct.CntctPrsn

                                             });

            foreach (var itm in qry)
            {
                accounts.Add(new page_param.SoAccount
                {
                    CardCode = itm.CardCode,
                    CardName = itm.CardName,
                    Address = itm.Address,
                    acctClassfxn = itm.acctClassfxn,
                    Phone1 = itm.Phone1,
                    CntctPrsn = itm.CntctPrsn,
                    numberOfVisits = itm.numberOfVisits

                });
            }

            return accounts;
        }

        public static List<page_param.SoAccount> LookupItemisCoverage(string SoId, string month, int day, int year)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<page_param.SoAccount> accounts = new List<page_param.SoAccount>();

            IEnumerable<page_param.SoAccount> qry_1 = (from c_hdr in DATABASE.CoverageHdrs
                                                       from c_dtls in DATABASE.CoverageDtls
                                                       where c_hdr.EmpIdNo == SoId && c_hdr.EventID == c_dtls.EventID
                                                       && c_dtls.Day == day && c_hdr.Month == month && c_hdr.Year == year
                                                       select new page_param.SoAccount
                                               {
                                                   CardCode = c_dtls.AccountCode
                                               }).ToList();

            IEnumerable<page_param.SoAccount> qry = (from acct in DATABASE.arms2_vw_actveBusPrtnr.ToList()
                                                     where acct.empIdNo == SoId
                                                     && !qry_1.Any(es => (es.CardCode == acct.CardCode))
                                                     select new page_param.SoAccount
                                             {
                                                 CardCode = acct.CardCode,
                                                 CardName = acct.CardName,
                                                 Address = acct.Address,
                                                 Phone1 = acct.Phone1,
                                                 CntctPrsn = acct.CntctPrsn
                                             });

            foreach (var itm in qry)
            {
                accounts.Add(new page_param.SoAccount
                {
                    CardCode = itm.CardCode,
                    CardName = itm.CardName,
                    Address = itm.Address,
                    acctClassfxn = itm.acctClassfxn,
                    Phone1 = itm.Phone1,
                    CntctPrsn = itm.CntctPrsn
                });
            }

            return accounts;
        }

        public static List<CoverageHdrTmp> LookupItemisCoveragebydate(string Eventmonth, int Eventday, int Eventyear, string soId)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<CoverageHdrTmp> coverage = new List<CoverageHdrTmp>();

            var qry_1 = (from c in DATABASE.arms2_vw_AccountInCoverage
                         where c.EventID == c.EventID &&
                        c.EmpIdNo == soId && c.Month == Eventmonth
                        && c.Day == Eventday
                        && c.AccountCode == c.AccountCode
                         select new
                         {
                             c.AccountCode,
                             c.EmpIdNo,
                             c.Month,
                             c.Day,
                             c.Year,
                             c.EventID,
                             c.DoctypeId,
                             c.DocumentStatusId,
                             c.LineNum,
                             c.ContactPerson,
                             c.ContactPersonNo,
                             c.acctName,
                             c.acctClassfxn,
                             c.bussAdd,
                             c.FileAttachment,
                             c.HotelName,
                             c.HotelContactNumber
                         }).Distinct().ToList();

            var qry = (from cvrgehdr in DATABASE.CoverageHdrs
                       from cvrgedtl in DATABASE.CoverageDtls
                       from custhdr in DATABASE.arms2_vw_customerheader_lookup
                       where cvrgehdr.EventID == cvrgedtl.EventID
                       && cvrgehdr.EmpIdNo == soId && cvrgehdr.Month == Eventmonth
                       && cvrgedtl.Day == Eventday
                       && cvrgedtl.AccountCode == custhdr.acctcode
                       select new
                       {
                           cvrgedtl.AccountCode,
                           cvrgehdr.EmpIdNo,
                           cvrgehdr.Month,
                           cvrgedtl.Day,
                           cvrgehdr.Year,
                           cvrgehdr.EventID,
                           cvrgehdr.DoctypeId,
                           cvrgehdr.DocumentStatusId,
                           cvrgedtl.LineNum,
                           cvrgedtl.ContactPerson,
                           cvrgedtl.ContactPersonNo,
                           cvrgedtl.StoreChecking,
                           cvrgedtl.StoreCheckingResult,
                           cvrgedtl.ProductPresentationResult,
                           cvrgedtl.TotalAmount,
                           cvrgedtl.CompetitorActivities,
                           cvrgedtl.WithOrder,
                           cvrgedtl.NextCallDate,
                           cvrgedtl.PONum,
                           cvrgedtl.IssuesAndConcerns,
                           cvrgedtl.Delivery,
                           cvrgedtl.Orders,
                           cvrgedtl.SummaryLackingItems,
                           cvrgedtl.MobileNo,
                           cvrgedtl.Recommendation,
                           cvrgedtl.TimeTable,
                           cvrgedtl.Remarks,
                           cvrgedtl.cFullCollection,
                           cvrgedtl.cPartialCollection,
                           cvrgedtl.cNoCollection,
                           cvrgedtl.OtherInformation,
                           custhdr.acctName,
                           custhdr.acctClassfxn,
                           custhdr.bussAdd,
                           cvrgedtl.FileAttachment,
                           cvrgedtl.HotelName,
                           cvrgedtl.HotelContactNumber
                       }).Distinct().ToList();

            foreach (var itm in qry_1)
            {
                coverage.Add(new CoverageHdrTmp
                {
                    AccountCode = itm.AccountCode,
                    EmpIdNo = itm.EmpIdNo,
                    Month = itm.Month,
                    Day = itm.Day.ToString(),
                    Year = itm.Year.ToString(),
                    EventID = itm.EventID,
                    DoctypeId = itm.DoctypeId.ToString(),
                    DocumentStatusId = (int)itm.DocumentStatusId,
                    LineNum = itm.LineNum,
                    ContactPerson = itm.ContactPerson,
                    ContactPersonNo = itm.ContactPersonNo,
                    AccountName = itm.acctName,
                    AccountAddress = itm.bussAdd,
                    AccountClass = itm.acctClassfxn,
                    // Sub_coverage= sub_coverages(itm.EventID, itm.LineNum).ToList(),
                    Attachment = itm.FileAttachment,
                    HotelName = itm.HotelName,
                    HotelNum = itm.HotelContactNumber
                });
            }
            return coverage;
        }

        public static CoverageHdrTmp AccountInfo(string Eventmonth, int Eventday, int Eventyear, string soId, string acctCode)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            CoverageHdrTmp coverage = new CoverageHdrTmp();
            // bool hasrow = false;

            var qry_1 = (from c in DATABASE.arms2_vw_actveBusPrtnr
                         where c.CardCode == acctCode
                         select new
                         {

                             c.CardCode,
                             c.empIdNo,
                             c.CntctPrsn,
                             c.CardName,
                             c.acctClassfxn,
                             c.Address

                         }).Distinct().ToList();

            foreach (var itm in qry_1)
            {
                coverage.AccountCode = itm.CardCode;
                coverage.EmpIdNo = itm.empIdNo;
                coverage.ContactPerson = itm.CntctPrsn;
                coverage.AccountName = itm.CardName;
                coverage.AccountAddress = itm.Address;
                coverage.AccountClass = itm.acctClassfxn;
            }
            return coverage;
        }

        public static CoverageHdrTmp getCoverageCallReportAccount(string Eventmonth, int Eventday, int Eventyear, string soId, string acctCode)
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            CoverageHdrTmp res = new CoverageHdrTmp();

            var coverageheader = (from a in DATABASE.CoverageHdrs
                                  from b in DATABASE.CoverageDtls
                                  //from c in DATABASE.arms2_vw_customerheader_lookup
                                  from c in DATABASE.arms2_vw_actveBusPrtnr
                                  where a.EmpIdNo == soId && a.Month == Eventmonth && a.Year == Eventyear
                                   && a.EventID == b.EventID && b.Day == Eventday && b.AccountCode == acctCode
                                   && c.CardCode == acctCode && b.AccountCode == c.CardCode
                                  select new { a, b, c.CardName, c.Address, c.acctClassfxn }).ToList();

            foreach (var itm in coverageheader)
            {
                res.DoctypeId = itm.a.DoctypeId.ToString();
                res.DocumentStatusId = itm.a.DocumentStatusId.Value;
                res.Month = itm.a.Month;
                res.Year = itm.a.Year.ToString();

                res.LineNum = itm.b.LineNum;
                res.EventID = itm.b.EventID;
                res.Day = itm.b.Day.ToString();

                res.AccountAddress = itm.Address;
                res.AccountName = itm.CardName;
                res.AccountClass = itm.acctClassfxn;

                res.AccountCode = itm.b.AccountCode;
                res.ContactPerson = itm.b.ContactPerson;
                res.ContactPersonNo = itm.b.ContactPersonNo;
                res.HotelName = itm.b.HotelName;
                res.HotelNum = itm.b.HotelContactNumber;

                //coverage plan
                res.StoreChecking = itm.b.StoreChecking;
                res.IssuesAndConcerns = itm.b.IssuesAndConcerns;

                //callreport
                res.ProductPresentationResult = itm.b.ProductPresentationResult;
                res.freqVisit = itm.b.Numvisit.ToString();
                res.CheckInTime = itm.b.CheckInTime.ToString();
                res.CheckInAddress = itm.b.CheckInAddress;
                res.CheckOutTime = itm.b.CheckOutTime.ToString();
                res.CheckOutAddress = itm.b.CheckOutAddress;
                res.CompetitorActivities = itm.b.CompetitorActivities;
                res.NextCallDate = itm.b.NextCallDate.ToString();
                res.OtherInformation = itm.b.OtherInformation;
                res.TimeTable = itm.b.TimeTable;

                res.Attachment = itm.b.FileAttachment;

                res.cFullCollection = itm.b.cFullCollection;
                res.cPartialCollection = itm.b.cPartialCollection;
                res.cNoCollection = itm.b.cNoCollection;

                res.WithOrder = itm.b.WithOrder;
                res.Delivery = itm.b.Delivery;
                res.Orders = itm.b.Orders;
                res.StoreCheckingResult = itm.b.StoreCheckingResult;
                res.SummaryLackingItems = itm.b.SummaryLackingItems;
                res.Recommendation = itm.b.Recommendation;
                res.Remarks = itm.b.Remarks;

                res.ColDatedCheck = itm.b.ColDatedCheck;
                res.ColPostDatedCheck = itm.b.ColPostDatedCheck;
                res.ColTotal = itm.b.ColTotal;
                res.ColRemarks = itm.b.ColRemarks;

                res.hasCallreport = itm.b.hasCallreport == "T" ? true : false;

                res.Total_dtls = total_dtls(res.EventID, res.LineNum);

            }

            var coverageobjectives = (from a in DATABASE.CoverageDtl1
                                      where a.LineNum == res.LineNum && a.EventID == res.EventID
                                      select a).ToList();

            res.Sub_coverage = new List<CoverageHdrTmp.sub_coverage>();
            foreach (var itm in coverageobjectives)
            {
                res.Sub_coverage.Add(new CoverageHdrTmp.sub_coverage()
                {
                    ObjectiveCode = itm.ObjectiveCode,
                    Brand = itm.Brand,
                    ActualAmount = itm.ActualAmount.ToString(),
                    PlannedAmount = itm.PlannedAmount.HasValue ? itm.PlannedAmount.ToString() : "0",
                    dtlsrmks = itm.detailRemarks,
                    ProductPresented = itm.ProductPresented,
                    CounterClerk = itm.CounterClerk,
                    CounterClerkNo = itm.CounterClerkNo,
                    Remarks = itm.Remarks
                });
            }

            res.totalcoveragesales = res.Sub_coverage.Where(o => o.ObjectiveCode == "S").Sum(o => Convert.ToDecimal(o.PlannedAmount));

            DATABASE.Dispose();

            return res;
        }


        public static List<page_param.CoverageHdr> getObjectiveCode(string EventId, string Eventmonth, int Eventday, int Eventyear, string soId, string Acctcode)
        {
            ARMS_W.Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<page_param.CoverageHdr> objective = new List<page_param.CoverageHdr>();

            var qry = (from a in DATABASE.arms2_vw_soCoverage
                       where a.EventID == EventId && a.Month == Eventmonth
                       && a.Day == Eventday && a.Year == Eventyear
                       && a.EmpIdNo == soId && a.AccountCode == Acctcode
                       select new
                       {

                           a.ObjectiveCode

                       }).Distinct();

            foreach (var itm in qry)
            {
                objective.Add(new page_param.CoverageHdr()
                {
                    ObjectiveCode = itm.ObjectiveCode == null ? "" : itm.ObjectiveCode.ToUpper()
                });
            }

            return objective;
        }

        public class objective_inventory_details
        {
            public bool hasForInventoryCount { get; set; }
            public string inventoryCountId { get; set; }
        }

        public class objective_collection_details
        {
            public string docnum { get; set; }
            public string transid { get; set; }
            public string actDelDate { get; set; }
            public string DueDate { get; set; }
            public decimal? balance { get; set; }
        }

        /* inserted by billy jay delima */
        public static bool hasNextInventoryCount(DateTime? coverage_date, string acctCode, string empIdNo)
        {
            var DATABASE = new Models.ARMSTestEntities();

            var qry = DATABASE.arms2_sp_InventoryCountSchedule().Where(o => o.startCountDate == coverage_date && o.acctCode == acctCode && o.empId == empIdNo).ToList();
            DATABASE.Dispose();
            return qry.Count() >= 1 ? true : false;
        }

        public static string getInventoryCount(DateTime? coverage_date, string acctCode, string empIdNo)
        {
            var DATABASE = new Models.ARMSTestEntities();
            var inventoryCount = DATABASE.InventoryCountHdrs.Where(o => o.empId == empIdNo && o.acctCode == acctCode && o.prevCountDate == coverage_date).Select(o => o.inventoryCountId).ToList();
            DATABASE.Dispose();
            return inventoryCount.Count() >= 1 ? inventoryCount.Single() : "";
        }

        public class temp_inventory_sched
        {
           public DateTime? startCountDate { get; set; }
           public string acctCode { get; set; }
           public string empIdNo { get; set; }
           public DateTime? prevCountDate { get; set; }
           public string inventoryCountId { get; set; }
        }

        public static objective_inventory_details getInventoryCountObjective(DateTime? coverage_date, string acctCode, string empIdNo)
        {
            objective_inventory_details result = new objective_inventory_details();
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            result.hasForInventoryCount = false;
            result.inventoryCountId = "";
            try
            {
                var tmp = new List<temp_inventory_sched>();
                var qry_inv_sched = (from a in DATABASE.arms2_vw_inventoryScheduleAccounts
                                    select new { a.startCountDate, a.acctCode, a.empIdNo }).ToList();

                foreach (var itm in qry_inv_sched)
                {

                    tmp.Add(new temp_inventory_sched()
                    {
                        acctCode = itm.acctCode,
                        empIdNo = itm.empIdNo,
                        startCountDate = itm.startCountDate
                    });
                }
                var qry_forInventoryCount = tmp.Where(o => o.startCountDate == coverage_date && o.acctCode == acctCode && o.empIdNo == empIdNo).ToList();
                //var qry_forInventoryCount = DATABASE.arms2_vw_inventoryScheduleAccounts.Where(o => o.startCountDate == coverage_date && o.acctCode == acctCode && o.empIdNo == empIdNo).ToList();

                tmp = new List<temp_inventory_sched>();
                var InventoryCountHdrs = (from a in DATABASE.InventoryCountHdrs
                                 select new { a.inventoryCountId, a.dateTimeStamp, a.acctCode, a.empId }).ToList();

                foreach (var itm in InventoryCountHdrs)
                {

                    tmp.Add(new temp_inventory_sched()
                    {
                        inventoryCountId=itm.inventoryCountId,
                        acctCode = itm.acctCode,
                        empIdNo = itm.empId,
                        prevCountDate = itm.dateTimeStamp.Value.Date
                    });
                }
                coverage_date = coverage_date.Value.Date;
                //var qry_performedInventoryCount = tmp.Where(p => p.empIdNo == empIdNo && p.acctCode == acctCode && p.prevCountDate == coverage_date).Select(o => o.inventoryCountId).ToList();
                var qry_performedInventoryCount = tmp.Where(p => p.empIdNo == empIdNo && p.acctCode == acctCode && p.prevCountDate == coverage_date).Select(o => o.inventoryCountId).ToList();

                result.hasForInventoryCount = qry_forInventoryCount.Count() != 0 ? true : false;
                result.inventoryCountId = qry_performedInventoryCount.Count() != 0 ? qry_performedInventoryCount.First() : "";
            }
            catch { }
            return result;
        }

        public static List<objective_collection_details> BalanceLookUpFromSAP(string acctCode)
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<objective_collection_details> res = new List<objective_collection_details>();

            //var qry_trf = DATABASE.arms2_sp_PendingAccounts88(acctCode);
            var qry_trf = from a in DATABASE.CollectionPendingAccounts
                          where a.cardcode == acctCode
                          select a;
            foreach (var itm in qry_trf)
            {
                res.Add(new objective_collection_details()
                {
                    docnum = itm.docnum,
                    transid = itm.transid,
                    actDelDate = itm.actdeldate.ToString(),
                    DueDate = itm.duedate.ToString(),
                    balance = (decimal?)itm.balance
                });
            }

            DATABASE.Dispose();
            return res;
        }
        /*end */

        public static List<page_param.employeeDetail> ListOfSalesOfficer(string EmpIdNo)
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            var SO_employee_list = new List<page_param.employeeDetail>();
            var Employees = new List<page_param.employeeDetail>();
            Models.arms2_vw_lookUpOSLP slp_info;

            try
            {
                var slp_roles = getUserRoles(EmpIdNo);
                var region_list = (from a in DATABASE.userHeaders
                              from b in DATABASE.apprvrDesigs
                              where a.counterId == b.counterId
                              select a);

                if (slp_roles.Contains("SPRUSER") || slp_roles.Contains("VPBSM"))// || slp_roles.Contains("CVW"))
                {
                    Employees = (from vw_user_role in DATABASE.arms2_vw_lookUpOSLP
                                 select
                                 new page_param.employeeDetail
                                 {
                                     empIDNo = vw_user_role.empIdNo,
                                     empFullName = vw_user_role.SlpName
                                 }).ToList();
                }
                else if (slp_roles.Contains("CVW"))
                {
                    var region_user = (from a in DATABASE.apprvrDesigs
                                       from b in DATABASE.userHeaders
                                       where a.counterId == b.counterId &&b.empIdNo==EmpIdNo && a.roleID==61 //roleid 61 = CVW (CALENDARVIEWER)
                                       select a.branch ).ToList();

                    var so_list = (from a in DATABASE.apprvrDesigs
                              from b in DATABASE.userHeaders
                              where a.counterId == b.counterId && (a.roleID == 17 || a.roleID==2) 
                              select new {a.branch,a.counterId,b.firstName,b.lastName,b.empIdNo}).ToList();

                    foreach (var itm in so_list.Where(o=> region_user.Contains(o.branch)))
                    {
                        Employees.Add(new page_param.employeeDetail()
                        {
                            empIDNo = itm.empIdNo,
                            empFullName = itm.lastName.ToUpper() + ", " + itm.firstName.ToUpper()
                        });
                    }
                    
                    //Employees = (from vw_user_role in DATABASE.arms2_vw_lookUpOSLP
                    //             select
                    //             new page_param.employeeDetail
                    //             {
                    //                 empIDNo = vw_user_role.empIdNo,
                    //                 empFullName = vw_user_role.SlpName
                    //             }).ToList();
                }
                else if (slp_roles.Contains("CHM"))
                {
                    var qry_channel = (from a in DATABASE.apprvrDesigs
                                       from b in DATABASE.userHeaders
                                       where a.counterId == b.counterId &&b.empIdNo==EmpIdNo && a.roleID==8 //roleid 61 = CVW (CALENDARVIEWER)
                                       select a.channel ).ToList();

                    var so_list = (from a in DATABASE.apprvrDesigs
                                   from b in DATABASE.userHeaders
                                   where a.counterId==b.counterId && (a.roleID == 17 || a.roleID==2)
                                   select new { a.branch,a.channel, a.counterId, b.firstName, b.lastName, b.empIdNo }).ToList();

                    foreach (var itm in so_list.Where(o => qry_channel.Contains(o.channel)))
                    {
                        Employees.Add(new page_param.employeeDetail()
                        {
                            empIDNo = itm.empIdNo,
                            empFullName = itm.lastName.ToUpper() + ", " + itm.firstName.ToUpper()
                        });
                    }
                    Employees = Employees.GroupBy(o => o.empIDNo).Select(grp => grp.FirstOrDefault()).ToList();
                }
                else if (slp_roles.Contains("ASM"))
                {
                    //slp_info = DATABASE.arms2_vw_lookUpOSLP.Single(o => o.empIdNo == EmpIdNo);
                    //Employees = (from vw_user_role in DATABASE.arms2_vw_lookUpOSLP
                    //             where vw_user_role.U_AREA == slp_info.U_AREA && vw_user_role.U_ACTIVE == "Y"
                    //             select
                    //             new page_param.employeeDetail
                    //             {
                    //                 empIDNo = vw_user_role.empIdNo,
                    //                 empFullName = vw_user_role.SlpName
                    //             }).ToList();
                    slp_info = DATABASE.arms2_vw_lookUpOSLP.Single(o => o.empIdNo == EmpIdNo);
                    Employees = (from vw_user_role in DATABASE.arms2_vw_lookUpOSLP
                                 where vw_user_role.SlpName == slp_info.SlpName
                                 select
                                 new page_param.employeeDetail
                                 {
                                     empIDNo = vw_user_role.empIdNo,
                                     empFullName = vw_user_role.SlpName
                                 }).ToList();
                }
                else if (slp_roles.Contains("SO"))
                {
                    slp_info = DATABASE.arms2_vw_lookUpOSLP.Single(o => o.empIdNo == EmpIdNo);
                    Employees = (from vw_user_role in DATABASE.arms2_vw_lookUpOSLP
                                 where vw_user_role.SlpName == slp_info.SlpName
                                 select
                                 new page_param.employeeDetail
                                 {
                                     empIDNo = vw_user_role.empIdNo,
                                     empFullName = vw_user_role.SlpName
                                 }).ToList();
                }

                foreach (var itm in Employees)
                {
                    SO_employee_list.Add(new page_param.employeeDetail()
                    {
                        empIDNo = itm.empIDNo,
                        empFullName = itm.empFullName
                    });
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                DATABASE.Dispose();
            }
            return SO_employee_list;
        }

        public static List<string> getUserRoles(string empIdNo)
        {
            var DATABASE = new Models.ARMSTestEntities();

            var qry = (from a in DATABASE.apprvrRoles
                       where (from b in DATABASE.apprvrDesigs
                              where (from c in DATABASE.userHeaders
                                     where c.empIdNo == empIdNo
                                     select c.counterId).Contains(b.counterId.Value)
                              select b.roleID).Contains(a.roleID)
                       select a).ToList();

            var listOfRoles = new List<string>();
            foreach (var itm in qry)
            {
                listOfRoles.Add(itm.roleCode.ToString());
            }

            DATABASE.Dispose();
            return listOfRoles;
        }

        public static DateTime? ToNullableDateTime(string s)
        {
            DateTime i;
            if (DateTime.TryParse(s, out i)) return i;
            return null;
        }

    }
}
