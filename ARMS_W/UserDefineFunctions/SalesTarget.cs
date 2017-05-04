using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARMS_W.Class;
using ARMS_W.SkelClass;

namespace ARMS_W.UserDefineFunctions
{
    public class SalesTarget
    {
        public static List<page_param.SalesTarget> getSalesTarget()
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new List<page_param.SalesTarget>();

            try
            {
                var qry = (from a in DATABASE.SalesTargetMaintenances
                           join b in DATABASE.userHeaders on a.SlpCode equals b.SlpCode

                           select new { a, b.lastName,b.firstName,b.empIdNo }).ToList();

                foreach (var itm in qry)
                {
                    res.Add(new page_param.SalesTarget()
                    {
                        year = itm.a.Year,
                        month = itm.a.Month,
                        empid = itm.empIdNo,
                        empfullname = itm.lastName + ", " + itm.firstName,
                        salestarget = itm.a.Amount??0
                    });
                }
            }
            finally
            {
                DATABASE.Dispose();
            }

            return res;
        }
    }
}