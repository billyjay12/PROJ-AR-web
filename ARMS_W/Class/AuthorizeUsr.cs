using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ARMS_W.Class
{
    public class AuthorizeUsr : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext CurrentContext = HttpContext.Current;

            System.Web.UI.Control x = new System.Web.UI.Control();
            string r_path = x.ResolveUrl("~/");
            
            if (CurrentContext.Session["username"] == null || CurrentContext.Session["username"].ToString() == "")
            {
                filterContext.Result = new RedirectResult(r_path + "Home/mError");
            }

            
        }
    }
}