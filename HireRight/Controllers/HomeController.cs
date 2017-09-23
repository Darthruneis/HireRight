using System.Web.Mvc;

namespace HireRight.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public PartialViewResult Consultants() => PartialView("ConsultantsInformationPartial");

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult Introduction() => PartialView("IntroductionPartial");

        [HttpGet]
        public PartialViewResult OccupationalCategories() => PartialView("WhoWeServePartial");

        [HttpGet]
        public PartialViewResult WhyUseHireRight() => PartialView("WhyUseHireRightPartial");
    }
}