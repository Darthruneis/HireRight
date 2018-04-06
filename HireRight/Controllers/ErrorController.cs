using System;
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