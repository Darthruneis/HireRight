using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using HireRight.Models;
using SDK.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.Controllers
{
    public class CustomSolutionsController : Controller
    {
        private readonly ICategoriesSDK _categoriesSDK;
        private readonly IOrdersSDK _ordersSDK;

        public CustomSolutionsController(ICategoriesSDK categoriesSdk, IOrdersSDK ordersSdk)
        {
            _categoriesSDK = categoriesSdk;
            _ordersSDK = ordersSdk;
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> FilterCategories(CategoryFilter model)
        {
            List<CategoryDTO> categories = await _categoriesSDK.GetCategories(model);

            CustomSolutionsViewModel newModel = CreateViewModelFromCategoryList(categories);

            return View("Index", newModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(CustomSolutionsViewModel model)
        {
            if (model == null || !ModelState.IsValid)
                return View(model);

            List<JobAnalysisCategoryViewModel> listToReturn = EnforceConstraints(model.Categories.Where(x => x.Importance != CategoryImportance.Irrelevant).ToList());

            model.Categories = listToReturn.OrderBy(x => x.Importance).ThenBy(x => x.Title).ToList();

            return !ModelState.IsValid
                        ? View(model)
                        : model.Categories.Count > 12
                            ? View("SelectTopTwelve", model)
                            : await TopTwelve(model);
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<CategoryDTO> categories = await _categoriesSDK.GetCategories(new CategoryFilter(1, 10));

            CustomSolutionsViewModel model = CreateViewModelFromCategoryList(categories);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> TopTwelve(CustomSolutionsViewModel model)
        {
            if (model.Categories.Count <= 12)
                foreach (JobAnalysisCategoryViewModel jobAnalysisCategoryViewModel in model.Categories)
                    jobAnalysisCategoryViewModel.IsInTopTwelve = true;
            else if (model.Categories.Count(x => x.IsInTopTwelve) > 12)
            {
                ModelState.AddModelError("", "Please narrow down your selections to 12 or fewer.");
                return View("SelectTopTwelve", model);
            }

            SubmitCardsDTO dto = new SubmitCardsDTO();
            dto.Categories = model.Categories.Select(x => new CategoryDTO(x.Title, x.Description) { Importance = x.Importance, IsInTopTwelve = x.IsInTopTwelve }).ToList();
            dto.CompanyName = model.CompanyName;
            dto.Positions = model.Positions;
            dto.Contact = model.Contact;
            dto.Notes = model.Notes;

            await _ordersSDK.SubmitCards(dto);

            return RedirectToAction("Index", "Home");
        }

        private CustomSolutionsViewModel CreateViewModelFromCategoryList(List<CategoryDTO> categories)
        {
            List<JobAnalysisCategoryViewModel> categoryViewModels = categories.Select(x => new JobAnalysisCategoryViewModel() { Description = x.Description, Title = x.Title }).ToList();

            CustomSolutionsViewModel model = new CustomSolutionsViewModel();
            model.Categories = categoryViewModels.OrderBy(x => x.Title).ToList();

            return model;
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
    }
}