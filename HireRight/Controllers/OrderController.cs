using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using HireRight.Models;
using SDK.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HireRight.Controllers
{
    public class OrderController : ControllerBase<OrderDetailsDTO>
    {
        private readonly ICompaniesSDK _companiesSDK;
        private readonly IContactsSDK _contactsSDK;
        private readonly IOrdersSDK _ordersSDK;
        private readonly IProductsSDK _productsSDK;

        public OrderController(IOrdersSDK ordersSDK, ICompaniesSDK companiesSDK, IContactsSDK contactsSDK, IProductsSDK productsSDK)
        {
            _ordersSDK = ordersSDK;
            _companiesSDK = companiesSDK;
            _contactsSDK = contactsSDK;
            _productsSDK = productsSDK;
        }

        [HttpGet]
        public ViewResult About()
        {
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> GetDiscounts(Guid itemSelected)
        {
            ProductDTO product = await _productsSDK.GetProduct(itemSelected);

            return PartialView("ProductDiscountsPartial", product.Discounts);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetTotalPrice(Guid itemSelected, int quantity)
        {
            try
            {
                decimal total = await _ordersSDK.CalculatePrice(itemSelected, quantity);
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
            ProductFilter assessmentTestFilter = new ProductFilter(1, 10);
            assessmentTestFilter.Title = "test";

            List<ProductDTO> tests = await _productsSDK.GetProducts(assessmentTestFilter);

            CreateOrderViewModel viewModel = new CreateOrderViewModel(tests.ToArray());
            viewModel.Order.SelectedProduct = tests.First();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Order(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            NewOrderDTO dto = model.ConvertToNewOrderDTO();

            ProductFilter productFilter = new ProductFilter(1, 1);
            productFilter.Title = "Assessment Test";

            List<ProductDTO> products = await _productsSDK.GetProducts(productFilter);

            try
            {
                //CompanyDTO company = await _companiesSDK.AddCompany(dto.Company);

                //dto.PrimaryContact.CompanyId = company.Id;
                //dto.SecondaryContact.CompanyId = company.Id;
                //dto.Order.CompanyId = company.Id;
                //dto.Order.ProductId = product.Id;

                //Task<ContactDTO> primaryContactTask = _contactsSDK.AddContact(dto.PrimaryContact);
                //Task<ContactDTO> secondaryContactTask = _contactsSDK.AddContact(dto.SecondaryContact);
                //Task<OrderDetailsDTO> orderTask = _ordersSDK.AddOrder(dto.Order);

                //await Task.WhenAll(orderTask, primaryContactTask, secondaryContactTask);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error ocurred while saving, please try again later.");
            }

            if (!ModelState.IsValid) return View(model);

            return RedirectToAction("Order");
        }
    }
}