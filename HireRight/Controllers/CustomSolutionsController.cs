using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using HireRight.Models;
using SDK.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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

        [HttpGet]
        [ValidateInput(false)]
        public async Task<PartialViewResult> FilterCategories(int page, int size, string description, string title)
        {
            CategoryFilter filter = new CategoryFilter(page, size);
            filter.TitleFilter = title;
            filter.DescriptionFilter = description;
            List<CategoryDTO> categories = await _categoriesSDK.GetCategories(filter);

            CustomSolutionsViewModel newModel = CreateViewModelFromCategoryList(categories);
            newModel.CategoryFilter = filter;

            return PartialView("CustomSolutionsPartial", newModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(CustomSolutionsViewModel model)
        {
            if (model == null || !ModelState.IsValid)
                return View(model);

            foreach (UndisplayedCategory category in model.CategoriesFromOtherPages)
            {
                CategoryImportance importance = GetImportanceLevelFromDisplayName(category.Importance);
                JobAnalysisCategoryViewModel categoryInModel = model.Categories.FirstOrDefault(x => x.Id == category.Id);
                if (categoryInModel == null)
                {
                    categoryInModel = new JobAnalysisCategoryViewModel();
                    categoryInModel.Id = category.Id;
                    categoryInModel.Importance = importance;
                    model.Categories.Add(categoryInModel);
                }
                else
                {
                    categoryInModel.Importance = importance;
                }
            }

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
            dto.Categories = model.Categories.Select(x => new CategoryDTO(x.Title, x.Description) { Importance = x.Importance, IsInTopTwelve = x.IsInTopTwelve, Id = x.Id }).ToList();
            dto.CompanyName = model.CompanyName;
            dto.Positions = model.Positions;
            dto.Contact = model.Contact;
            dto.Notes = model.Notes;

            await _ordersSDK.SubmitCards(dto);

            return RedirectToAction("Index", "Home");
        }

        private CustomSolutionsViewModel CreateViewModelFromCategoryList(List<CategoryDTO> categories)
        {
            List<JobAnalysisCategoryViewModel> categoryViewModels = categories.Select(x => new JobAnalysisCategoryViewModel() { Description = x.Description, Title = x.Title, Id = x.Id }).ToList();

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

        private CategoryImportance GetImportanceLevelFromDisplayName(string importanceLevelName)
        {
            foreach (object value in Enum.GetValues(typeof(CategoryImportance)))
            {
                CategoryImportance importance = (CategoryImportance)value;
                if (importance.GetType()
                        .GetMember(importance.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>().Name == importanceLevelName)
                    return importance;
            }

            throw new ArgumentOutOfRangeException(nameof(importanceLevelName), "Importance level does not match a value from " + nameof(CategoryImportance));
        }
    }
}