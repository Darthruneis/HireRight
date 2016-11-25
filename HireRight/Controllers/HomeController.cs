using System.Web.Mvc;

namespace HireRight.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult CustomSolutions()
        {
            return View("Index");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}