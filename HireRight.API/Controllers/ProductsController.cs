using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using HireRight.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HireRight.API.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiControllerBase<ProductDTO>
    {
        private readonly Func<ProductFilter, Task<List<ProductDTO>>> _getPage;
        private readonly IProductsBusinessLogic _productsBusinessLogic;

        public ProductsController(IProductsBusinessLogic bll)
        {
            _productsBusinessLogic = bll;

            _add = _productsBusinessLogic.Add;
            _update = _productsBusinessLogic.Update;
            _getSingle = _productsBusinessLogic.Get;
            _getPage = _productsBusinessLogic.Get;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddProduct(ProductDTO productToAdd)
        {
            return await AddBase(productToAdd);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProduct(Guid productGuid)
        {
            return await GetSingleBase(productGuid);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProducts([FromUri] ProductFilter filter)
        {
            return await GetMultipleBase(_getPage(filter));
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateProduct(ProductDTO productToUpdate)
        {
            return await UpdateBase(productToUpdate);
        }
    }
}