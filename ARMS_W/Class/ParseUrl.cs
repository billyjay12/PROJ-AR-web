using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ARMS_W.Class
{
    public class ParseUrl : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext CurrentContext = HttpContext.Current;
            HttpRequestWrapper hr = new HttpRequestWrapper(CurrentContext.Request);

            foreach (string val in hr.Form.Keys)
            {
                if (val != null) 
                {
                    filterContext.ActionParameters[val] = hr.Form[val].Replace("$AG$", "&");
                    filterContext.ActionParameters[val] = filterContext.ActionParameters[val].ToString().Replace("$AH$", "+");
                }
            }
        }
    }
}