﻿using DataTransferObjects.Data_Transfer_Objects;
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
        private ICategoriesSDK _categoriesSDK;

        public HomeController(ICategoriesSDK sdk)
        {
            _categoriesSDK = sdk;
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

            List<JobAnalysisCategoryViewModel> model = models.Where(x => x.Importance != CategoryImportance.Irrelevant).ToList();

            List<JobAnalysisCategoryViewModel> low = model.Where(x => x.Importance == CategoryImportance.LowImportance).ToList();
            List<JobAnalysisCategoryViewModel> normal = model.Where(x => x.Importance == CategoryImportance.NormalImportance).ToList();
            List<JobAnalysisCategoryViewModel> high = model.Where(x => x.Importance == CategoryImportance.HighImportance).ToList();

            List<JobAnalysisCategoryViewModel> listToReturn = new List<JobAnalysisCategoryViewModel>();

            int total = low.Count + normal.Count + high.Count;

            if (total > 15)
            {
                ModelState.AddModelError("", $"Please narrow down your selections to fewer than 15 Important categories.  You have selected {total - 15} too many.");
                listToReturn.AddRange(low);
                listToReturn.AddRange(normal);
                listToReturn.AddRange(high);
            }

            if (!ModelState.IsValid)
                return View(listToReturn.OrderBy(x => x.Importance).ThenBy(x => x.Title).ToList());

            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}