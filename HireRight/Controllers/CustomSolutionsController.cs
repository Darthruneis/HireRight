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
        private const int MaximumNumberOfCategories = 9;

        public CustomSolutionsController(ICategoriesBusinessLogic categoriesBusinessLogic, IOrdersBusinessLogic ordersBusinessLogic)
        {
            _categoriesBusinessLogic = categoriesBusinessLogic;
            _ordersBusinessLogic = ordersBusinessLogic;
        }
        
        [HttpPost]
        public async Task<ActionResult> Index(CustomSolutionsViewModel model)
        {
            //if (model == null)
            //    return await Index();

            if (model.Categories.Count(x => x.Importance != CategoryImportance.Irrelevant) > MaximumNumberOfCategories)
                ModelState.AddModelError("", $"Please choose 1 - {MaximumNumberOfCategories} important scales.");

            if (model.Categories.Count(x => x.Importance == CategoryImportance.HighImportance) < 3)
                ModelState.AddModelError("", "Please select at least 3 critical scales.");
            
            if (!ModelState.IsValid)
                return View("Index", model);

            await _ordersBusinessLogic.SubmitCards(model.CreateSubmitCardsDTO());
            return View("CustomSolutionsSuccess");
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var categories = await _categoriesBusinessLogic.GetAll();
            CustomSolutionsViewModel model = CreateViewModelFromCategoryList(categories);

            return View(model);
        }

        private CustomSolutionsViewModel CreateViewModelFromCategoryList(ICollection<CategoryDTO> categories)
        {
            IEnumerable<JobAnalysisCategoryViewModel> categoryViewModels = categories.Select(x => new JobAnalysisCategoryViewModel(x.Description, x.Title, x.Id));

            return new CustomSolutionsViewModel(categoryViewModels.OrderBy(x => x.Title));
        }
    }
}