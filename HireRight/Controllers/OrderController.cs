using DataTransferObjects.Data_Transfer_Objects;
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

            return PartialView("ProductDiscountsPartial", product);
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
        public async Task<ViewResult> Order(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                ProductFilter productFilter = new ProductFilter(1, 1);
                productFilter.Title = "Assessment Test";
                List<ProductDTO> products = await _productsSDK.GetProducts(productFilter);

                NewOrderDTO dto = model.ConvertToNewOrderDTO();

                CompanyFilter companyFilter = new CompanyFilter(1, 10);
                companyFilter.Name = dto.Company.Name;

                List<CompanyDTO> companies = await _companiesSDK.GetCompanies(companyFilter);

                CompanyDTO company = companies.All(x => x.Name != dto.Company.Name)
                                         ? await _companiesSDK.AddCompany(dto.Company)
                                         : companies.First(x => x.Name != dto.Company.Name);

                dto.PrimaryContact.CompanyId = company.Id;
                Task<ContactDTO> primaryContactTask = _contactsSDK.AddContact(dto.PrimaryContact);

                dto.SecondaryContact.CompanyId = company.Id;
                Task<ContactDTO> secondaryContactTask = _contactsSDK.AddContact(dto.SecondaryContact);

                dto.Order.CompanyId = company.Id;
                dto.Order.ProductId = products.First().Id;
                Task<OrderDetailsDTO> orderTask = _ordersSDK.AddOrder(dto.Order);

                await Task.WhenAll(orderTask, primaryContactTask, secondaryContactTask);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error ocurred while saving, please try again later.");
#if DEBUG
                ModelState.AddModelError("", ex.Message);
#endif
            }

            return View("Success");
        }
    }
}