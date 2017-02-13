using System.Web.Mvc;
using HireRight.Models;

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
        public ViewResult Test()
        {
            SectionWithTogglesAndTitleViewModel model = new SectionWithTogglesAndTitleViewModel(" show the order form again.", "hiddenContent", "hideButton", "showButton", "shownContent", "~/Views/Home/Index.cshtml");
            return View("SectionWithPanelHeadAndToggles", model);
        }

        [HttpGet]
        public PartialViewResult WhyUseHireRight() => PartialView("WhyUseHireRightPartial");
    }
}