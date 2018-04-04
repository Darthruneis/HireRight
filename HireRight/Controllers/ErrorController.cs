using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HireRight.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View("GenericErrorPage");
        }

#if DEBUG
        public ActionResult Test()
        {
            throw new Exception("Testing");
        }
#endif
    }
}