using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using SDK.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting;
using System.Threading.Tasks;

namespace SDK.Concrete
{
    public class OrdersSDK : IOrdersSDK
    {
        private readonly IApiSDKClient _client;

        public OrdersSDK(IApiSDKClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(ConfigurationConstants.ApiUrl + typeof(OrdersSDK).Name.Replace("SDK", string.Empty));
        }

        public async Task<OrderDetailsDTO> AddOrder(OrderDetailsDTO orderToAdd)
        {
            ApiResponse<OrderDetailsDTO> response = await _client.PostAsync(orderToAdd);

            return response.Results.First();
        }

        public async Task<decimal> CalculatePrice(Guid productGuid, long quantity)
        {
            HttpClient tempClient = new HttpClient();

            HttpResponseMessage response = await tempClient.GetAsync(_client.BaseAddress +
                $"?{nameof(productGuid)}={productGuid}&{nameof(quantity)}={quantity}");

            decimal result = await response.Content.ReadAsAsync<decimal>();

            return result;
        }

        public async Task<OrderDetailsDTO> GetOrder(Guid orderGuid)
        {
            ApiResponse<OrderDetailsDTO> response = await _client.GetAsync<OrderDetailsDTO>($"?{nameof(orderGuid)}={orderGuid}");

            return response.Results.First();
        }

        public async Task<List<OrderDetailsDTO>> GetOrders(OrderFilter filter)
        {
            ApiResponse<OrderDetailsDTO> response = await _client.GetAsync<OrderDetailsDTO>(filter.CreateQuery());

            return response.Results.ToList();
        }

        public async Task SubmitCards(SubmitCardsDTO cardsToSubmit)
        {
            _client.BaseAddress = new Uri(_client.BaseAddress + "/cards");
            await _client.PostAsync(cardsToSubmit);
        }

        public async Task<OrderDetailsDTO> UpdateOrder(OrderDetailsDTO orderToUpdate)
        {
            ApiResponse<OrderDetailsDTO> response = await _client.PutAsync(orderToUpdate);

            return response.Results.First();
        }
    }
}