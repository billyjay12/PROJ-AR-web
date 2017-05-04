using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARMS_W.Class;
using ARMS_W.SkelClass;

namespace ARMS_W.UserDefineFunctions
{
    public class DayCycle
    {
        public static page_param.DayCycleMaintenance getDayCycle()
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new page_param.DayCycleMaintenance();
            try
            {
                var qry = (from a in DATABASE.dayCycles
                           select a);

                foreach (var itm in qry)
                {
                    res.DayCycleCount = itm.DayCycleNo;
                    res.rangeDayCycle = (int)itm.rangeDayCycle;
                    res.startDayOfTheMonth = (int)itm.startDayOfTheMonth;
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
    }
}