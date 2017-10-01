using DataTransferObjects;
using HireRight.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using HireRight.BusinessLogic.Abstract;
using WebGrease.Css.Extensions;

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
            CategoryFilter filter = new CategoryFilter(title, description) { PageNumber = page, PageSize = size };
            CustomSolutionsViewModel newModel = CreateViewModelFromCategoryList(await FindCategories(description, title, page, size), filter);

            return PartialView("CustomSolutionsPartial", newModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(CustomSolutionsViewModel model)
        {
            if (model == null)
                return await Index();
            if (!ModelState.IsValid)
                return View(model);

            //go through the categories in the list created by javascript, except for items which are already in the Categories list, as the model binder has
            //already ensured they have the correct values.
            foreach (UndisplayedCategory category in model.CategoriesFromOtherPages.Where(x => !model.Categories.Select(y => y.Id).ToList().Contains(x.Id)))
                model.Categories.Add(new JobAnalysisCategoryViewModel(category.Id, GetImportanceLevelFromDisplayName(category.Importance)));

            model.Categories = EnforceConstraints(model.Categories
                .OrderBy(x => x.Importance)
                .ThenBy(x => x.Title));

            return !ModelState.IsValid
                        ? View(model)
                        : model.Categories.Count > 12
                            ? View("SelectTopTwelve", model)
                            : await TopTwelve(model);
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            CategoryFilter categoryFilter = new CategoryFilter(1, 10);
            PagingResultDTO<CategoryDTO> categories = await _categoriesBusinessLogic.Get(categoryFilter);

            CustomSolutionsViewModel model = CreateViewModelFromCategoryList(categories, categoryFilter);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> TopTwelve(CustomSolutionsViewModel model)
        {
            if (model.Categories.Count <= 12)
                model.Categories.ForEach(x => x.IsInTopTwelve = true);
            else if (model.Categories.Count(x => x.IsInTopTwelve) > 12)
            {
                ModelState.AddModelError("", $"Please narrow down your selections to 12 or fewer. You have selected {model.Categories.Count(x => x.IsInTopTwelve) - 12} too many.");
                return View("SelectTopTwelve", model);
            }

            await _ordersBusinessLogic.SubmitCards(model.CreateSubmitCardsDTO());

            return View("CustomSolutionsSuccess");
        }

        private CustomSolutionsViewModel CreateViewModelFromCategoryList(PagingResultDTO<CategoryDTO> categories, CategoryFilter filter)
        {
            IEnumerable<JobAnalysisCategoryViewModel> categoryViewModels = categories.PageResult.Select(x => new JobAnalysisCategoryViewModel(x.Description, x.Title, x.Id));

            return new CustomSolutionsViewModel(categoryViewModels.OrderBy(x => x.Title), new CategoryFilterViewModel() { Filter = filter, TotalMatchingResults = categories.TotalMatchingResults });
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

        private async Task<PagingResultDTO<CategoryDTO>> FindCategories(string description, string title, int page = 1, int size = 10)
        {
            CategoryFilter filter = new CategoryFilter(title, description);
            filter.PageNumber = page;
            filter.PageSize = size;
            return await _categoriesBusinessLogic.Get(filter);
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