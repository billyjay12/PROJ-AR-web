using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ARMS_W.Controllers
{
    public partial class CalendarController : Controller
    {


        #region GET EVENT LIST

        public JsonResult GetEventListSO(string month, int year, int day, string EmpId)
        {
            string Eventid;
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            List<string[]> Events = ARMS_W.UserDefineFunctions.Application.GetEventList(month, year,day,EmpId);

            return Json(Events);
        }

        #endregion

        #region GET EVENT LIST FOR CALL REPORT
        [HttpPost]
        public JsonResult getEventListCallreport(string month, int day, int year, string EmpId)
        {
            SkelClass.AjxResult ajx_result = new SkelClass.AjxResult();
            ajx_result.iserror = true;

            try {
                ajx_result.iserror = false;
                var eventList = UserDefineFunctions.CalendarEvent.SoCalendar.GetListEventCallReport(month, day, year, EmpId);

                ajx_result.data = new {
                    Events = eventList
                
                };
            }

            catch (Exception ex)
            {

                ajx_result.iserror = true;
                ajx_result.message = ex.Message;
            }
            return Json(ajx_result);


        }
        #endregion
    }
}
