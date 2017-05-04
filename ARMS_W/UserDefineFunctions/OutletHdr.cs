using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARMS_W.Class;
using ARMS_W.SkelClass;

namespace ARMS_W.UserDefineFunctions
{
    public class OutletHdr
    {
        public static List<page_param.CustOutletsDetails> getOutletList()
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new List<page_param.CustOutletsDetails>();

            try
            {
                var qry = (from a in DATABASE.custOutlets
                           from b in DATABASE.mtc_vw_User_Role_Details
                           where a.inventoryIncharge == b.empIDNo
                               && a.status.ToUpper() == "ACTIVE"
                           select new page_param.CustOutletsDetails()
                           {
                               ccaNum = a.ccaNum,
                               custOutletId = a.custOutletsID,
                               custOutletName = a.name,
                               custOutletLocation = a.location,
                               empId = b.empIDNo,
                               empName = b.firstName + " " + b.lastName,
                               status = a.status//,
                               //prevCountDate=DATABASE.nextInventoryCountHdrs.FirstOrDefault(o=>o.custOutletsID==a.custOutletsID).prevCountDate,
                               // nextCountDate = DATABASE.nextInventoryCountHdrs.FirstOrDefault(o => o.custOutletsID == a.custOutletsID).startCountDate,
                           }).Union(from a in DATABASE.custOutlets
                                    where a.inventoryIncharge == null
                                    && a.status.ToUpper() == "ACTIVE"
                                    select new page_param.CustOutletsDetails()
                                    {
                                        ccaNum = a.ccaNum,
                                        custOutletId = a.custOutletsID,
                                        custOutletName = a.name,
                                        custOutletLocation = a.location,
                                        empId = "",
                                        empName = "",
                                        status = a.status//,
                                        //prevCountDate=null,
                                        //nextCountDate=null
                                    }).ToList();


                //var qqq = UserDefineFunctions.GTAccountMasterFile.getAccountList();

                var qrrry = (from a in DATABASE.customerHeaders
                             where (from b in DATABASE.customerAccStatus select b.ccaNum).Contains(a.SapAcctCode)
                                && (a.TAG1 == null || a.TAG1 != "INVALID")
                             select a).ToList();

                var qrya = (from a in DATABASE.nextInventoryCountHdrs select a);
                var qryb = (from a in DATABASE.arms2_vw_actveBusPrtnr 
                            from b in DATABASE.userHeaders
                            where a.empIdNo==b.empIdNo
                            select new {a,b});

                
               // var qry = (from a in DATABASE.customerHeaders


                //var qry1 = (from a in DATABASE.nextInventoryCountHdrs
                //           from b in qry
                //           where b.custOutletId == a.custOutletsID
                //           select a);

                foreach (var itm in qrrry)
                {
                    //var getDates = qrya.FirstOrDefault(o => o.acctCode == itm.acctCode);
                    var SO = qryb.FirstOrDefault(o=>o.a.CardCode==itm.acctCode).b;
                    if(qrya.Any(o=>o.acctCode==itm.acctCode))
                     //  var getDates = qrya.FirstOrDefault(o => o.acctCode == itm.acctCode);
                    //else
                        //var getDates = qrya.FirstOrDefault(o => o.acctCode == itm.acctCode);

                    res.Add(new page_param.CustOutletsDetails()
                    {
                        ccaNum = itm.ccaNum.ToString(),
                        acctAddress = itm.bussAdd.ToString(),
                        acctCode = itm.acctCode.ToString(),
                        acctName = itm.acctName.ToString(),
                        empId = SO.empIdNo.ToString(),
                        empName = SO.firstName.ToString() + " - " + SO.lastName.ToString(),
                        status = itm.status.ToString(),
                        prevCountDate = qrya.SingleOrDefault(o => o.acctCode == itm.acctCode).prevCountDate,// getDates.prevCountDate,//qry1.FirstOrDefault(o => o.custOutletsID == itm.custOutletId).prevCountDate,
                        nextCountDate = qrya.SingleOrDefault(o => o.acctCode == itm.acctCode).startCountDate// getDates.startCountDate //qry1.FirstOrDefault(o => o.custOutletsID == itm.custOutletId).startCountDate,
                    });

                    //res.Add(new page_param.CustOutletsDetails()
                    //{
                    //    ccaNum = itm.ccaNum.ToString(),
                    //    custOutletId = int.Parse(itm.custOutletId.ToString()),
                    //    custOutletName = itm.custOutletName.ToString(),
                    //    custOutletLocation = itm.custOutletLocation.ToString(),
                    //    empId = itm.empId.ToString(),
                    //    empName = itm.empName.ToString(),
                    //    status = itm.status.ToString(),
                    //    prevCountDate = itm.prevCountDate,//qry1.FirstOrDefault(o => o.custOutletsID == itm.custOutletId).prevCountDate,
                    //    nextCountDate = itm.nextCountDate //qry1.FirstOrDefault(o => o.custOutletsID == itm.custOutletId).startCountDate,
                    //});
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
        public static List<page_param.CustOutletsDetails> getInventoryCountSchedule()
        {
            var res = new List<page_param.CustOutletsDetails>();
            var DATABASE = new Models.ARMSTestEntities();

            // var qry = (from a in DATABASE.arms2_sp_InventoryCountSchedule);
            //  var qqq = DATABASE.arms2_sp_InventoryCountSchedule().ToList();
            var accounts = DATABASE.arms2_vw_inventoryScheduleAccounts;
            foreach (var itm in accounts)
            {
                res.Add(new page_param.CustOutletsDetails()
                {
                    ccaNum = itm.ccaNum.Trim(),
                    acctAddress = itm.bussAdd,
                    acctCode = itm.acctCode,
                    acctName = itm.acctName,
                    empId = itm.empIdNo,
                    empName = itm.firstName + " - " + itm.lastName,
                    prevCountDate = itm.prevCountDate,
                    nextCountDate = itm.startCountDate
                });
            }

            return res;
        }
        public static List<page_param.CustOutletsDetails> getInventoryCountSchedule(string empId)
        {
            var res = new List<page_param.CustOutletsDetails>();
            var DATABASE = new Models.ARMSTestEntities();

           // var qry = (from a in DATABASE.arms2_sp_InventoryCountSchedule);
          //  var qqq = DATABASE.arms2_sp_InventoryCountSchedule().ToList();
            var accounts = from a in DATABASE.arms2_vw_inventoryScheduleAccounts
                           where a.empIdNo == empId
                           select a;
            foreach (var itm in accounts)
            {
                res.Add(new page_param.CustOutletsDetails()
                {
                    ccaNum = itm.ccaNum.Trim(),
                    acctAddress = itm.bussAdd,
                    acctCode = itm.acctCode,
                    acctName = itm.acctName,
                    empId = itm.empIdNo,
                    empName = itm.firstName + " - " + itm.lastName,
                    prevCountDate = itm.prevCountDate,
                    nextCountDate = itm.startCountDate
                });
            }

            return res;
        }
    }
}