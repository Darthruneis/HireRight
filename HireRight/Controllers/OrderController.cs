using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using HireRight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using HireRight.BusinessLogic.Abstract;

namespace HireRight.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICompanyBusinessLogic _companyBusinessLogic;
        private readonly IContactsBusinessLogic _contactsBusinessLogic;
        private readonly IOrdersBusinessLogic _ordersBusinessLogic;
        private readonly IProductsBusinessLogic _productsBusinessLogic;

        public OrderController(IOrdersBusinessLogic ordersBusinessLogic, ICompanyBusinessLogic companyBusinessLogic, IContactsBusinessLogic contactsBusinessLogic, IProductsBusinessLogic productsBusinessLogic)
        {
            _ordersBusinessLogic = ordersBusinessLogic;
            _companyBusinessLogic = companyBusinessLogic;
            _contactsBusinessLogic = contactsBusinessLogic;
            _productsBusinessLogic = productsBusinessLogic;
        }

        [HttpGet]
        public ViewResult About()
        {
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> GetDiscounts(Guid itemSelected)
        {
            ProductDTO product = await _productsBusinessLogic.Get(itemSelected);

            return PartialView("ProductDiscountsPartial", product);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetTotalPrice(Guid itemSelected, int quantity)
        {
            try
            {
                decimal total = await _ordersBusinessLogic.CalculatePrice(itemSelected, quantity);
                return PartialView("OrderTotalPartial", total);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Order()
        {
            CreateOrderViewModel viewModel = await GetProductListForModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ViewResult> Order(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                NewOrderDTO dto = model.ConvertToNewOrderDTO();

                await _companyBusinessLogic.Add(dto.Company);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error ocurred while saving, please try again later.");
#if DEBUG
                ModelState.AddModelError("", ex.Message);
#endif

                //add product information back to model before returning it with modelstate errors.
                return View(await GetProductListForModel(model));
            }

            return View("Success");
        }

        private async Task<CreateOrderViewModel> GetProductListForModel(CreateOrderViewModel model = null)
        {
            if (model == null)
                model = new CreateOrderViewModel();

            ProductFilter assessmentTestFilter = new ProductFilter(1, 10);
            assessmentTestFilter.Title = "test";
            List<ProductDTO> products = await _productsBusinessLogic.Get(assessmentTestFilter);

            model.Order.Products = products;

            model.Order.SelectedProduct = model.Order.Products.First();

            return model;
        }
    }
}