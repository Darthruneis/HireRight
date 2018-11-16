using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using HireRight.BusinessLogic.Abstract;

namespace HireRight.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIndustryBusinessLogic _industryBusinessLogic;

        public HomeController(IIndustryBusinessLogic industryBusinessLogic)
        {
            _industryBusinessLogic = industryBusinessLogic;
        }

        [HttpGet]
        public PartialViewResult Consultants() => PartialView("ConsultantsInformationPartial");

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var industriesWithAssessments = await _industryBusinessLogic.GetAllWithAssessments();
            
            return View("Index", industriesWithAssessments.ToList());
        }

        [HttpGet]
        public PartialViewResult Introduction() => PartialView("IntroductionPartial");

        [HttpGet]
        public PartialViewResult OccupationalCategories() => PartialView("WhoWeServePartial");

        public async Task<ActionResult> Industries()
        {
            var industriesWithAssessments = await _industryBusinessLogic.GetAllWithAssessments();

            return PartialView("Industries", industriesWithAssessments.ToList());
        }

        [HttpGet]
        public PartialViewResult WhyUseHireRight() => PartialView("WhyUseHireRightPartial");
    }
}