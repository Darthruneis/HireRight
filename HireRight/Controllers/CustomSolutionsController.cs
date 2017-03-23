using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using HireRight.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTransferObjects.Filters.Concrete;
using HireRight.BusinessLogic.Abstract;

namespace HireRight.Controllers
{
    public class CustomSolutionsController : Controller
    {
        private readonly ICategoriesBusinessLogic _categoriesBusinessLogic;
        private readonly IOrdersBusinessLogic _ordersBusinessLogic;

        public CustomSolutionsController(ICategoriesBusinessLogic categoriesBusinessLogic, IOrdersBusinessLogic ordersBusinessLogic)
        {
            _categoriesBusinessLogic = categoriesBusinessLogic;
            _ordersBusinessLogic = ordersBusinessLogic;
        }

        [HttpGet]
        [ValidateInput(false)]
        public async Task<PartialViewResult> FilterCategories(int page, int size, string description, string title)
        {
            CategoryFilter filter = new CategoryFilter(title, description);
            List<CategoryDTO> categories = await _categoriesBusinessLogic.Get(filter);

            CustomSolutionsViewModel newModel = CreateViewModelFromCategoryList(categories, filter);

            return PartialView("CustomSolutionsPartial", newModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(CustomSolutionsViewModel model)
        {
            if (model == null || !ModelState.IsValid)
                return model == null ? await Index()
                                     : View(new CustomSolutionsViewModel());

            //go through the categories in the list created by javascript, except for items which are already in the Categories list, as the model binder has
            //already ensured they have the correct values.
            foreach (UndisplayedCategory category in model.CategoriesFromOtherPages.Where(x => !model.Categories.Select(y => y.Id).ToList().Contains(x.Id)))
                model.Categories.Add(new JobAnalysisCategoryViewModel(category.Id, GetImportanceLevelFromDisplayName(category.Importance)));

            model.Categories = EnforceConstraints(model.Categories
                .Where(x => x.Importance != CategoryImportance.Irrelevant))
                .OrderBy(x => x.Importance)
                .ThenBy(x => x.Title)
                .ToList();

            return !ModelState.IsValid
                        ? View(model)
                        : model.Categories.Count > 12
                            ? View("SelectTopTwelve", model)
                            : TopTwelve(model);
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            CategoryFilter categoryFilter = new CategoryFilter(1, 10);
            List<CategoryDTO> categories = await _categoriesBusinessLogic.Get(categoryFilter);

            CustomSolutionsViewModel model = CreateViewModelFromCategoryList(categories, categoryFilter);

            return View(model);
        }

        [HttpPost]
        public ActionResult TopTwelve(CustomSolutionsViewModel model)
        {
            if (model.Categories.Count <= 12)
                foreach (JobAnalysisCategoryViewModel jobAnalysisCategoryViewModel in model.Categories)
                    jobAnalysisCategoryViewModel.IsInTopTwelve = true;
            else if (model.Categories.Count(x => x.IsInTopTwelve) > 12)
            {
                ModelState.AddModelError("", "Please narrow down your selections to 12 or fewer.");
                return View("SelectTopTwelve", model);
            }

            _ordersBusinessLogic.SubmitCards(model.CreateSubmitCardsDTO());

            return View("CustomSolutionsSuccess");
        }

        private CustomSolutionsViewModel CreateViewModelFromCategoryList(List<CategoryDTO> categories, CategoryFilter filter)
        {
            IEnumerable<JobAnalysisCategoryViewModel> categoryViewModels = categories.Select(x => new JobAnalysisCategoryViewModel(x.Description, x.Title, x.Id));

            CustomSolutionsViewModel model = new CustomSolutionsViewModel(categoryViewModels.OrderBy(x => x.Title));
            model.CategoryFilter = filter;

            return model;
        }

        private List<JobAnalysisCategoryViewModel> EnforceConstraints(IEnumerable<JobAnalysisCategoryViewModel> model)
        {
            IEnumerable<JobAnalysisCategoryViewModel> lowImportanceModels = model.Where(x => x.Importance == CategoryImportance.LowImportance);
            IEnumerable<JobAnalysisCategoryViewModel> highImportanceModels = model.Where(x => x.Importance == CategoryImportance.HighImportance);

            List<JobAnalysisCategoryViewModel> listToReturn = new List<JobAnalysisCategoryViewModel>();

            int total = lowImportanceModels.Count() + highImportanceModels.Count();
            const int minimum = 3;
            const int maximum = 20;

            if (total < minimum)
            {
                ModelState.AddModelError("", $"Please select at least {minimum} Important categories. You have only selected {total} so far.");
                return model.ToList();
            }

            if (lowImportanceModels.Count() > maximum)
                ModelState.AddModelError("", $"Please narrow down your selections to fewer than {maximum} Nice To Have categories.  You have selected {lowImportanceModels.Count() - maximum} too many.");

            if (highImportanceModels.Count() > maximum)
                ModelState.AddModelError("", $"Please narrow down your selections to fewer than {maximum} Nice To Have categories.  You have selected {highImportanceModels.Count() - maximum} too many.");

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