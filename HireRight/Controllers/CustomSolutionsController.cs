using DataTransferObjects;
using HireRight.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTransferObjects.Data_Transfer_Objects;
using HireRight.BusinessLogic.Abstract;
using HireRight.BusinessLogic.Concrete;

namespace HireRight.Controllers
{
    public class CustomSolutionsController : Controller
    {
        private readonly ICategoriesBusinessLogic _categoriesBusinessLogic;
        private readonly IOrdersBusinessLogic _ordersBusinessLogic;
        private readonly IIndustryBusinessLogic _industryBusinessLogic;

        public CustomSolutionsController(ICategoriesBusinessLogic categoriesBusinessLogic, IOrdersBusinessLogic ordersBusinessLogic, IIndustryBusinessLogic industryBusinessLogic)
        {
            _categoriesBusinessLogic = categoriesBusinessLogic;
            _ordersBusinessLogic = ordersBusinessLogic;
            _industryBusinessLogic = industryBusinessLogic;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("_FirstStep", new CustomSolutionsFirstStepModel());
        }

        [HttpPost]
        public async Task<ActionResult> FirstStep(CustomSolutionsFirstStepModel model)
        {
            if(!ModelState.IsValid)
                return View("_FirstStep", model);

            ICollection<CategoryDTO> categories = await _categoriesBusinessLogic.GetAll();
            var industries = await _industryBusinessLogic.GetAll();
            industries = industries.Where(x => categories.SelectMany(y => y.Industries).Contains(x.Id)).ToList();

            JobAnalysisCategoryViewModel createCategoryModel(CategoryDTO category)
                => new JobAnalysisCategoryViewModel(category.Description, category.Title, category.RowGuid,
                                                    industries.Where(y => category.Industries.Contains(y.Id)).Select(y => y.Id).ToList(),
                                                    industries.ToList());

            CustomSolutionsSecondStepModel nextModel = new CustomSolutionsSecondStepModel(model, categories.Select(createCategoryModel).OrderBy(x => x.Title), industries);
            return View("_SecondStep", nextModel);
        }
        
        [HttpPost]
        public async Task<ActionResult> SecondStep(CustomSolutionsSecondStepModel model, long selectedIndustry, bool isGeneralSelected)
        {
            ICollection<CategoryDTO> categories = await _categoriesBusinessLogic.GetAll();
            ICollection<IndustryDTO> industries = await _industryBusinessLogic.GetAll();
            model.RefreshModel(industries, categories);
            model.IsGeneralSelected = isGeneralSelected;
            model.SelectedIndustry = selectedIndustry;

            if (model.Categories.Count(x => x.Importance == CategoryImportance.LowImportance) > OrdersBusinessLogic.MaxNiceCategories)
                ModelState.AddModelError("", $"Please select at most {OrdersBusinessLogic.MaxNiceCategories} 'Nice to Have' categories. You have selected {model.Categories.Count(x => x.Importance == CategoryImportance.LowImportance)}.");

            if (model.Categories.Count(x => x.Importance == CategoryImportance.HighImportance) < OrdersBusinessLogic.MinCriticalCategories)
                ModelState.AddModelError("", $"Please select at least {OrdersBusinessLogic.MinCriticalCategories} 'Critical' categories. You have selected {model.Categories.Count(x => x.Importance == CategoryImportance.HighImportance)}.");

            if (model.Categories.Count(x => x.Importance == CategoryImportance.HighImportance) > OrdersBusinessLogic.MaxCriticalCategories)
                ModelState.AddModelError("", $"Please choose at most {OrdersBusinessLogic.MaxCriticalCategories} 'Critical' categories. You have selected {model.Categories.Count(x => x.Importance == CategoryImportance.HighImportance)}.");
            
            ClearIrrelevantModelstateErrorsForContactAddress();
            if (!ModelState.IsValid)
                return View("_SecondStep", model);
            
            return View("_Review", model);
        }

        public async Task<ActionResult> Resume(CustomSolutionsSecondStepModel model, long selectedIndustry, bool isGeneralSelected)
        {
            ICollection<CategoryDTO> categories = await _categoriesBusinessLogic.GetAll();
            ICollection<IndustryDTO> industries = await _industryBusinessLogic.GetAll();
            model.RefreshModel(industries, categories);
            model.IsGeneralSelected = isGeneralSelected;
            model.SelectedIndustry = selectedIndustry;

            return View("_SecondStep", model);
        }

        public async Task<ActionResult> Finish(CustomSolutionsSecondStepModel model, long selectedIndustry, bool isGeneralSelected)
        {
            ICollection<CategoryDTO> categories = await _categoriesBusinessLogic.GetAll();
            ICollection<IndustryDTO> industries = await _industryBusinessLogic.GetAll();
            model.RefreshModel(industries, categories);
            model.IsGeneralSelected = isGeneralSelected;
            model.SelectedIndustry = selectedIndustry;
            ClearIrrelevantModelstateErrorsForContactAddress();
            if (!ModelState.IsValid)
                return View("_Review", model);

            await _ordersBusinessLogic.SubmitCards(model.CreateSubmitCardsDTO());
            return View("CustomSolutionsSuccess");
        }

        private void ClearIrrelevantModelstateErrorsForContactAddress()
        {
            ModelState["PreviousInformation.Contact.Address.City"].Errors.Clear();
            ModelState["PreviousInformation.Contact.Address.PostalCode"].Errors.Clear();
            ModelState["PreviousInformation.Contact.Address.State"].Errors.Clear();
            ModelState["PreviousInformation.Contact.Address.StreetAddress"].Errors.Clear();
        }
    }
}