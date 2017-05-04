using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ARMS_W.Controllers
{
    public class UserGuideController : Controller
    {
        //
        // GET: /UserGuide/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult FAQ_Vol2()
        {
            return View();
        }

    }
}
