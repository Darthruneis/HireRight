using HireRight.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HireRight.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult CustomSolutions()
        {
            List<JobAnalysisCategoryViewModel> testingModel = new List<JobAnalysisCategoryViewModel>();
            JobAnalysisCategoryViewModel testModel = new JobAnalysisCategoryViewModel();
            testModel.Title = "Achievement Drive";
            testModel.Description = "Achievement Drive measures the degree to which the individual is likely to be competitive and driven to be the best. This characteristic is important for jobs where the attainment of established goals and benchmarks are important (e.g., sales jobs). It is also important for jobs where there may be competition within departments or between coworkers and positions where the individual is expected to grow and advance to higher levels within the organization.";
            testModel.Importance = CategoryImportance.HighImportance;
            testingModel.Add(testModel);

            return View(testingModel);
        }

        [HttpPost]
        public ActionResult CustomSolutions(IList<JobAnalysisCategoryViewModel> models)
        {
            if (models == null || !ModelState.IsValid)
                return View(models);

            List<JobAnalysisCategoryViewModel> model = models.ToList();

            List<JobAnalysisCategoryViewModel> low = model.Where(x => x.Importance == CategoryImportance.LowImportance).ToList();
            List<JobAnalysisCategoryViewModel> normal = model.Where(x => x.Importance == CategoryImportance.NormalImportance).ToList();
            List<JobAnalysisCategoryViewModel> high = model.Where(x => x.Importance == CategoryImportance.HighImportance).ToList();

            List<JobAnalysisCategoryViewModel> listToReturn = new List<JobAnalysisCategoryViewModel>();

            if (low.Count > 15)
            {
                ModelState.AddModelError("", "Please narrow down your selections to fewer than 15 Low Importance categories.");
                listToReturn.AddRange(low);
            }

            if (normal.Count > 15)
            {
                ModelState.AddModelError("", "Please narrow down your selections to fewer than 15 Normal Importance categories.");
                listToReturn.AddRange(normal);
            }

            if (high.Count > 15)
            {
                ModelState.AddModelError("", "Please narrow down your selections to fewer than 15 High Importance categories.");
                listToReturn.AddRange(high);
            }

            if (!ModelState.IsValid)
                return View(listToReturn);

            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}