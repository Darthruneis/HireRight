using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using HireRight.BusinessLogic.Abstract;
using HireRight.BusinessLogic.Extensions;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HireRight.BusinessLogic.Concrete
{
    public class OrdersBusinessLogic : IOrdersBusinessLogic
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsBusinessLogic _productsBusinessLogic;

        public OrdersBusinessLogic(IOrdersRepository repo, IProductsBusinessLogic productBll)
        {
            _ordersRepository = repo;
            _productsBusinessLogic = productBll;
        }

        public static void SendFormattedEmail(string email, string greeting, string message, string subject, string replyTo = null)
        {
            string body;

            const string path = @"C:\Users\Chris\Documents\GitHubVisualStudio\HireRight\HireRight.BusinessLogic\Models\EmailBase.cshtml";

            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }

            string messageBody = string.Format(body, greeting, message);

            SendEmail(email, messageBody, subject, replyTo);
        }

        public async Task<OrderDetailsDTO> Add(OrderDetailsDTO objectDto)
        {
            Order orderToAdd = ConvertDtoToModel(objectDto);

            return ConvertModelToDto(await _ordersRepository.Add(orderToAdd).ConfigureAwait(false));
        }

        public async Task<decimal> CalculatePrice(Guid productGuid, long quantity)
        {
            ProductDTO product = await _productsBusinessLogic.Get(productGuid);

            if (!product.Discounts.Any()) return product.Price * quantity;

            DiscountDTO discountToApply = null;

            foreach (DiscountDTO discount in product.Discounts)
            {
                if (quantity >= discount.Threshold)
                {
                    if (discountToApply == null) discountToApply = discount;
                    if (discount.Threshold > discountToApply.Threshold) discountToApply = discount;
                }
            }

            if (discountToApply == null) return product.Price * quantity;

            decimal discountedPrice = discountToApply.IsPercent
                ? product.Price * (1.00m - discountToApply.Amount)
                                          : product.Price - discountToApply.Amount;

            decimal orderTotal = quantity * discountedPrice;

            return Math.Round(orderTotal, 2);
        }

        public Order ConvertDtoToModel(OrderDetailsDTO dto)
        {
            Order model = new Order();
            model.Id = dto.Id;
            model.CreatedUtc = dto.CreatedUtc;
            model.CompanyId = dto.CompanyId;
            model.ProductId = dto.ProductId;
            model.Notes = dto.Notes;
            model.PositionsOfInterest = dto.PositionsOfInterest;
            model.Quantity = dto.Quantity;

            return model;
        }

        public OrderDetailsDTO ConvertModelToDto(Order model)
        {
            OrderDetailsDTO dto = new OrderDetailsDTO();
            dto.Id = model.Id;
            dto.CreatedUtc = model.CreatedUtc;
            dto.Quantity = model.Quantity;
            dto.ProductId = model.ProductId;
            dto.CompanyId = model.CompanyId;
            dto.Notes = model.Notes;
            dto.PositionsOfInterest = model.PositionsOfInterest;

            return dto;
        }

        public async Task CreateOrder(NewOrderDTO model)
        {
            string greetingLine = $"An order has just been placed for assessment testing for {model.PrimaryContact.FullName} of {model.Company.Name}.  The details of this order are shown below. <br /><br />";

            StringBuilder message = new StringBuilder(greetingLine);

            ProductDTO dto = await _productsBusinessLogic.Get(model.Order.ProductId).ConfigureAwait(false);

            message.AppendLine($"Quantity: {model.Order.Quantity}")
                   .AppendLine($"Subtotal: {CalculatePrice(dto.Id, model.Order.Quantity)}")
                   .AppendLine("Positions of Interest:");
            foreach (string position in model.Order.PositionsOfInterest) message.AppendLine(position);

            message.AppendLine("<br /><br />")
                   .AppendLine("Primary Contact:")
                   .AppendLine(model.PrimaryContact.FullName)
                   .AppendLine(model.PrimaryContact.Email)
                   .AppendLine(model.PrimaryContact.Address.GetFullAddress)
                   .AppendLine("Office Number: " + model.PrimaryContact.OfficeNumber)
                   .AppendLine("Cell: " + model.PrimaryContact.CellNumber);

            if (model.SecondaryContact.FullName != null)
                message.AppendLine("<br />Secondary Contact:")
                       .AppendLine(model.SecondaryContact.FullName)
                       .AppendLine(model.SecondaryContact.Email)
                       .AppendLine(model.SecondaryContact.CellNumber)
                       .AppendLine(model.SecondaryContact.Address.GetFullAddress);

            if (model.Order.Notes != null)
                message.AppendLine("<br />The customer has left the following notes for you:")
                       .AppendLine(model.Order.Notes);

            EmailConsultants(message.ToString(), "New Order Placed!");
        }

        public async Task<OrderDetailsDTO> Get(Guid orderGuid)
        {
            Order order = await _ordersRepository.Get(orderGuid).ConfigureAwait(false);

            return ConvertModelToDto(order);
        }

        public async Task<List<OrderDetailsDTO>> Get(OrderFilter filter)
        {
            List<Order> orders = await _ordersRepository.Get(filter).ConfigureAwait(false);

            return orders.Select(ConvertModelToDto).ToList();
        }

        public void SubmitCards(IList<CategoryDTO> categories)
        {
            categories = categories.Where(x => x.Importance != CategoryImportance.Irrelevant).ToList();

            StringBuilder message = new StringBuilder("A new custom test profile has been created by ...");

            //add the client's information to the message...

            message.AppendLine();
            message.AppendLine("The following categories were marked as <b>High Importance</b>:");
            foreach (CategoryDTO categoryDTO in categories.Where(x => x.Importance == CategoryImportance.HighImportance))
                message.AppendLine("• " + categoryDTO.Title);

            message.AppendLine();
            message.AppendLine("The following categories were marked as <b>Normal Importance</b>:");
            foreach (CategoryDTO categoryDTO in categories.Where(x => x.Importance == CategoryImportance.NormalImportance))
                message.AppendLine("• " + categoryDTO.Title);

            message.AppendLine();
            message.AppendLine("The following categories were marked as <b>Low Importance</b>:");
            foreach (CategoryDTO categoryDTO in categories.Where(x => x.Importance == CategoryImportance.LowImportance))
                message.AppendLine("• " + categoryDTO.Title);

            EmailConsultants(message.ToString(), "New Custom Test Request");
        }

        public async Task<OrderDetailsDTO> Update(OrderDetailsDTO objectDto)
        {
            Order orderToUpdate = ConvertDtoToModel(objectDto);

            return ConvertModelToDto(await _ordersRepository.Update(orderToUpdate).ConfigureAwait(false));
        }

        private static void EmailConsultants(string message, string subject, string replyTo = null)
        {
            SendFormattedEmail("silverasoc@aol.com", "Diana", message, subject, replyTo);
            SendFormattedEmail("janet@something.com", "Janet", message, subject, replyTo);
        }

        private static void SendEmail(string recipient, string body, string subject, string replyTo = null)
        {
            using (SmtpClient emailClient = new SmtpClient())
            {
                MailMessage mailMessage = new MailMessage(
                    "Hire Right Testing Admin Admin@HireRightTesting.com",
                    recipient,
                    subject,
                    body);

                mailMessage.IsBodyHtml = true;

                if (replyTo != null) mailMessage.ReplyToList.Add(replyTo);

                emailClient.PickupDirectoryLocation = @"C:\Users\Chris\Desktop\HireRight\HireRight Test Emails";
                emailClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;

                emailClient.Send(mailMessage);
            }
        }
    }
}