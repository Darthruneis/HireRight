using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using HireRight.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HireRight.API.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiControllerBase<OrderDetailsDTO>
    {
        private readonly Func<OrderFilter, Task<List<OrderDetailsDTO>>> _getPage;
        private readonly IOrdersBusinessLogic _ordersBusinessLogic;

        public OrdersController(IOrdersBusinessLogic bll)
        {
            _ordersBusinessLogic = bll;

            _add = _ordersBusinessLogic.Add;
            _update = _ordersBusinessLogic.Update;
            _getPage = _ordersBusinessLogic.Get;
            _getSingle = _ordersBusinessLogic.Get;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddOrder(OrderDetailsDTO orderToAdd)
        {
            return await AddBase(orderToAdd);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetOrder(Guid orderGuid)
        {
            return await GetSingleBase(orderGuid);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetOrderPrice(Guid productGuid, long quantity)
        {
            decimal total = await _ordersBusinessLogic.CalculatePrice(productGuid, quantity);

            return Request.CreateResponse(HttpStatusCode.OK, total);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetOrders([FromUri] OrderFilter filter)
        {
            return await GetMultipleBase(_getPage(filter));
        }

        [Route("cards")]
        [HttpPost]
        public HttpResponseMessage SubmitCards(SubmitCardsDTO cardsToSubmit)
        {
            _ordersBusinessLogic.SubmitCards(cardsToSubmit);

            ApiResponse<CategoryDTO> response = new ApiResponse<CategoryDTO>(HttpStatusCode.OK, 0);

            return Request.CreateResponse(response);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateOrder(OrderDetailsDTO orderToUpdate)
        {
            return await UpdateBase(orderToUpdate);
        }
    }
}