using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using HireRight.Models;
using SDK.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HireRight.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoriesSDK _categoriesSDK;
        private readonly IOrdersSDK _ordersSDK;

        public HomeController(ICategoriesSDK categoriesSdk, IOrdersSDK ordersSdk)
        {
            _categoriesSDK = categoriesSdk;
            _ordersSDK = ordersSdk;
        }

        [HttpGet]
        public async Task<ActionResult> CustomSolutions()
        {
            List<CategoryDTO> categories = await _categoriesSDK.GetCategories();

            List<JobAnalysisCategoryViewModel> testingModel = categories.Select(x => new JobAnalysisCategoryViewModel() { Description = x.Description, Title = x.Title }).ToList();

            return View(testingModel.OrderBy(x => x.Title).ToList());
        }

        [HttpPost]
        public ActionResult CustomSolutions(IList<JobAnalysisCategoryViewModel> models)
        {
            if (models == null || !ModelState.IsValid)
                return View(models);

            List<JobAnalysisCategoryViewModel> listToReturn = EnforceConstraints(models.Where(x => x.Importance != CategoryImportance.Irrelevant).ToList());

            if (!ModelState.IsValid)
                return View(listToReturn.OrderBy(x => x.Importance).ThenBy(x => x.Title).ToList());

            return TopTwelve(listToReturn);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> TopTwelve(IList<CategoryDTO> models)
        {
            if (models.Count(x => x.IsInTopTwelve) > 12)
            {
                ModelState.AddModelError("", "Please narrow down your selections to 12 or fewer.");
                return View("SelectTopTwelve", models.ToList());
            }

            await _ordersSDK.SubmitCards(models);

            return RedirectToAction("Index");
        }

        private List<JobAnalysisCategoryViewModel> EnforceConstraints(IList<JobAnalysisCategoryViewModel> model)
        {
            List<JobAnalysisCategoryViewModel> lowImportanceModels = model.Where(x => x.Importance == CategoryImportance.LowImportance).ToList();
            List<JobAnalysisCategoryViewModel> highImportanceModels = model.Where(x => x.Importance == CategoryImportance.HighImportance).ToList();

            List<JobAnalysisCategoryViewModel> listToReturn = new List<JobAnalysisCategoryViewModel>();

            int total = lowImportanceModels.Count + highImportanceModels.Count;
            const int minimum = 3;
            const int maximum = 20;

            if (total < minimum)
            {
                ModelState.AddModelError("", $"Please select at least {minimum - total} additional Important categories.");
                return model.ToList();
            }

            if (lowImportanceModels.Count > maximum)
                ModelState.AddModelError("", $"Please narrow down your selections to fewer than {maximum} Nice To Have categories.  You have selected {lowImportanceModels.Count - maximum} too many.");

            if (highImportanceModels.Count > maximum)
                ModelState.AddModelError("", $"Please narrow down your selections to fewer than {maximum} Nice To Have categories.  You have selected {highImportanceModels.Count - maximum} too many.");

            listToReturn.AddRange(lowImportanceModels);
            listToReturn.AddRange(highImportanceModels);

            return listToReturn;
        }

        private ActionResult TopTwelve(IList<JobAnalysisCategoryViewModel> models)
        {
            if (models == null || !ModelState.IsValid)
                return View("CustomSolutions", models);

            List<CategoryDTO> dtos = models.Select(x => new CategoryDTO(x.Title, x.Description) { Importance = x.Importance }).ToList();

            return View("SelectTopTwelve", dtos);
        }
    }
}