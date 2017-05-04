using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARMS_W.Class;
using ARMS_W.SkelClass;

namespace ARMS_W.UserDefineFunctions
{
    public class GTAccountMasterFile
    {
        public static List<page_param.gtaccount_pareto.gtacctlist> getAccountList()
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new List<page_param.gtaccount_pareto.gtacctlist>();
            try
            {
                //var qry = (from a in DATABASE.customerHeaders
                //           from b in DATABASE.customerAccStatus
                //           where a.acctCode == b.ccaNum && b.Status.ToUpper() == "ACTIVE"
                //           select a);

             //   var db_accounts = (from a in DATABASE.arms2_vw_actveBusPrtnr
              //                     select new { a }).ToList();

                var db_accounts = DATABASE.arms2_vw_actveBusPrtnr.ToList().GroupBy(o => o.CardCode).Select(grp => grp.First());
                var custHdr = DATABASE.customerHeaders.ToList().GroupBy(o => o.acctCode).Select(grp => grp.First());
                var usrHdr = (from a in DATABASE.userHeaders
                              select new { a.empIdNo, a.firstName, a.lastName }).ToList();

                //var custHdr = (from a in DATABASE.customerHeaders
                //               select a).ToList();

                var qry_22 = (from a in db_accounts
                              from b in custHdr
                              where a.CardCode == b.acctCode
                              select new
                              {
                                  b.ccaNum,
                                  a.CardCode,
                                  a.CardName,
                                  b.Pareto,
                                  a.U_AREA,
                                  a.empIdNo
                              }).ToList();



                //qry_22 = qry_22.GroupBy(o => o.a.a.CardCode).First(grp => grp);

                //qry_22 = qry_22.GroupBy(o => o.CardCode).Select(grp => grp.First()).ToList();

                foreach (var itm in qry_22)
                {
                    var SOdetail = usrHdr.Where(o => o.empIdNo == itm.empIdNo).First();
                    res.Add(new page_param.gtaccount_pareto.gtacctlist()
                    {
                        acct_ccanum = itm.ccaNum,
                        acctCode = itm.CardCode,
                        Pareto = itm.Pareto == "Y" ? true : false,
                        acctName = itm.CardName,
                        area = itm.U_AREA,
                        firstNameSO = SOdetail.firstName,
                        lastNameSO = SOdetail.lastName
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
            return res;
        }

        public static List<page_param.gtaccount_pareto.gtacctlist> getAccounts()
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new List<page_param.gtaccount_pareto.gtacctlist>();
           // var getAccounts = DATABASE.arms2_sp_InventoryCountSchedule().ToList();
            //var getAccounts = DATABASE.arms2_vw_inventoryScheduleAccounts;
            var getAccounts = DATABASE.arms2_vw_inventoryAccounts.ToList();
            foreach (var itm in getAccounts)
            {
                res.Add(new page_param.gtaccount_pareto.gtacctlist()
                {
                    acct_ccanum = itm.ccaNum,
                    acctCode = itm.acctCode,
                    Pareto = itm.pareto == "Y" ? true : false,
                    acctName = itm.acctName,
                    area = itm.areaname,
                    firstNameSO = itm.firstName,
                    lastNameSO = itm.lastName
                });
            }

            return res;
        }


    }
}