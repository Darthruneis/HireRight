﻿using DataTransferObjects.Data_Transfer_Objects;
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
            CreateOrderViewModel viewModel = await GetProductListForModel();

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
            List<ProductDTO> products = await _productsSDK.GetProducts(assessmentTestFilter);

            model.Order.Products = products;

            model.Order.SelectedProduct = model.Order.Products.First();

            return model;
        }
    }
}