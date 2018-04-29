using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using HireRight.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HireRight.BusinessLogic.Extensions;
using HireRight.Persistence;
using HireRight.Persistence.Abstract;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Concrete
{
    public class OrdersBusinessLogic : IOrdersBusinessLogic
    {
        private readonly ICategoriesBusinessLogic _categoriesBusinessLogic;
        private readonly IEmailSender _emailSender;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsBusinessLogic _productsBusinessLogic;
        public const int MaxCriticalCategories = 18;
        public const int MinCriticalCategories = 3;
        public const int MaxNiceCategories = 12;
        private readonly IProductsRepository _productsRepository;

        public OrdersBusinessLogic(IOrdersRepository repo, IProductsBusinessLogic productBll, 
                                   ICategoriesBusinessLogic categoriesBusinessLogic, IEmailSender emailSender, 
                                   IProductsRepository productsRepository)
        {
            _ordersRepository = repo;
            _productsBusinessLogic = productBll;
            _categoriesBusinessLogic = categoriesBusinessLogic;
            _emailSender = emailSender;
            _productsRepository = productsRepository;
        }

        public async Task<OrderDetailsDTO> Add(OrderDetailsDTO objectDto)
        {
            Order orderToAdd = ConvertDtoToModel(objectDto);
            return ConvertModelToDto(await _ordersRepository.Add(orderToAdd).ConfigureAwait(false));
        }

        public async Task<decimal> CalculatePrice(Guid productGuid, long quantity)
        {
            Maybe<Product> product = await _productsRepository.GetWithDiscounts(productGuid);
            if (product.HasNoValue)
                return default(decimal);

            if (!product.Value.Discounts.Any())
                return product.Value.Price * quantity;

            var discountToApply = product.Value.GetHighestDiscountForQuantity(quantity);

            //foreach (DiscountDTO discount in product.Value.Discounts)
            //    if (quantity >= discount.Threshold)
            //        //if no discount has yet been applied, or a higher discount has been found for the quantity, apply the current discount
            //        if (discountToApply == null || discount.Threshold > discountToApply.Threshold)
            //            discountToApply = discount;

            if (discountToApply == null)
                return product.Value.Price * quantity;

            decimal discountedPrice = discountToApply.IsPercent
                ? product.Value.Price * (1.00m - discountToApply.Amount)
                                          : product.Value.Price - discountToApply.Amount;

            decimal orderTotal = quantity * discountedPrice;
            return Math.Round(orderTotal, 2);
        }

        public Order ConvertDtoToModel(OrderDetailsDTO dto)
        {
            Order model = new Order();
            model.RowGuid = dto.RowGuid;
            model.CreatedUtc = dto.CreatedUtc;
            model.Notes = dto.NotesAndPositions.Notes;
            model.PositionsOfInterest = dto.NotesAndPositions.PositionsOfInterest;
            model.Quantity = dto.Quantity;

            return model;
        }

        public OrderDetailsDTO ConvertModelToDto(Order model)
        {
            OrderDetailsDTO dto = new OrderDetailsDTO();
            dto.RowGuid = model.RowGuid;
            dto.CreatedUtc = model.CreatedUtc;
            dto.Quantity = model.Quantity;
            dto.NotesAndPositions.Notes = model.Notes;
            dto.NotesAndPositions.PositionsOfInterest = model.PositionsOfInterest;

            return dto;
        }

        public async Task CreateOrder(NewOrderDTO model)
        {
            StringBuilder message = new StringBuilder($"An order has just been placed for assessment testing for " +
                $"{model.PrimaryContact.FullName} of {model.Company.Name}. The details of this order are shown below.");
            
            decimal total = await CalculatePrice(model.Order.ProductId, model.Order.Quantity);

            message.AppendLine()
                   .AppendLine($"Quantity: {model.Order.Quantity}")
                   .AppendLine($"Subtotal: {total:C2}")
                   .AppendLine("Positions of Interest:");
            foreach (string position in model.Order.NotesAndPositions.PositionsOfInterest)
                message.AppendLine(position);

            message.AppendLine()
                   .AppendLine("Primary Contact:")
                   .AppendLine(model.PrimaryContact.FullName)
                   .AppendLine(model.PrimaryContact.Email)
                   .AppendLine(model.PrimaryContact.Address.GetFullAddress)
                   .AppendLine("Office Number: " + model.PrimaryContact.OfficeNumber)
                   .AppendLine("Cell: " + model.PrimaryContact.CellNumber);

            if (model.SecondaryContact.FullName != null)
                message.AppendLine()
                       .AppendLine("Secondary Contact:")
                       .AppendLine(model.SecondaryContact.FullName)
                       .AppendLine(model.SecondaryContact.Email)
                       .AppendLine(model.SecondaryContact.CellNumber)
                       .AppendLine(model.SecondaryContact.Address.GetFullAddress);

            if (model.Order.NotesAndPositions.Notes != null)
                message.AppendLine()
                       .AppendLine("The customer has left the following notes for you:")
                       .AppendLine(model.Order.NotesAndPositions.Notes)
                       .AppendLine();

            _emailSender.EmailConsultants(message.ToString(), "New Order Placed!");
        }

        public async Task<OrderDetailsDTO> Get(Guid orderGuid)
        {
            Order order = await _ordersRepository.Get(orderGuid).ConfigureAwait(false);
            return ConvertModelToDto(order);
        }

        public async Task<PagingResultDTO<OrderDetailsDTO>> Get(OrderFilter filter)
        {
            PageResult<Order> orders = await _ordersRepository.Get(filter).ConfigureAwait(false);
            return orders.PageResultToDto(ConvertModelToDto);
        }

        public async Task SubmitCards(SubmitCardsDTO cardsToSubmit)
        {
            string message = await CreateEmailMessageFromDto(cardsToSubmit);
            _emailSender.EmailConsultants(message, "New Custom Test Request");
        }

        public async Task<OrderDetailsDTO> Update(OrderDetailsDTO objectDto)
        {
            Order orderToUpdate = ConvertDtoToModel(objectDto);
            return ConvertModelToDto(await _ordersRepository.Update(orderToUpdate).ConfigureAwait(false));
        }

        private async Task<string> CreateEmailMessageFromDto(SubmitCardsDTO cards)
        {
            StringBuilder message = new StringBuilder("A new custom test profile has been created by ");

            message.Append(cards.Contact.FullName + ". ");
            message.Append($"This custom test profile would be used to help {cards.CompanyName} fill the following positions: ");
            message.AppendLine();
            message.AppendLine(string.Join("," + Environment.NewLine, cards.Positions.Select(x => " • " + x)));
            message.AppendLine();
            message.AppendLine($"{cards.Contact.FullName} can be reached in the following ways:");
            if (!string.IsNullOrWhiteSpace(cards.Contact.Email))
                message.AppendLine("Email: " + cards.Contact.Email);
            if (!string.IsNullOrWhiteSpace(cards.Contact.OfficeNumber))
                message.AppendLine("Office: " + cards.Contact.OfficeNumber);
            if (!string.IsNullOrWhiteSpace(cards.Contact.CellNumber))
                message.AppendLine("Personal: " + cards.Contact.CellNumber);
            if (!string.IsNullOrWhiteSpace(cards.Notes))
                message.AppendLine().AppendLine("The following additional information was provided:").AppendLine(cards.Notes);

            message.AppendLine();
            await WriteCategoryInfoToStringBuilder(cards.Categories.Where(x => x.Importance != CategoryImportance.Irrelevant).ToList(), message);
            return message.ToString();
        }

        private string GetEnumName(CategoryImportance level)
        {
            Type type = typeof(CategoryImportance);
            MemberInfo[] memberInfo = type.GetMember(level.ToString());
            object[] attributes = memberInfo.First().GetCustomAttributes(typeof(DisplayAttribute), false);
            return ((DisplayAttribute)attributes.First()).Name;
        }

        private async Task RetrieveLostCategoryTitles(IList<CategoryDTO> categories)
        {
            IEnumerable<Guid> guids = categories.Select(x => x.RowGuid);
            var categoriesFromRepo = await _categoriesBusinessLogic.Get(new CategoryFilter(1, categories.Count, guids.ToArray()));

            foreach (var category in categoriesFromRepo.PageResult)
            {
                category.Importance = categories.First(y => y.RowGuid == category.RowGuid).Importance;
            }

            categories = categoriesFromRepo.PageResult.ToList();
        }

        private async Task WriteCategoryInfoToStringBuilder(IList<CategoryDTO> categories, StringBuilder message)
        {
            await RetrieveLostCategoryTitles(categories);
            message.AppendLine($"The following categories were marked as <b>{GetEnumName(CategoryImportance.HighImportance)}</b>:");
            foreach (CategoryDTO categoryDTO in categories.Where(x => x.Importance == CategoryImportance.HighImportance).OrderBy(x => x.Title))
                    message.AppendLine("• " + categoryDTO.Title);
            message.AppendLine();

            message.AppendLine($"The following categories were marked as <b>{GetEnumName(CategoryImportance.LowImportance)}</b>:");
            foreach (CategoryDTO categoryDTO in categories.Where(x => x.Importance == CategoryImportance.LowImportance).OrderBy(x => x.Title))
                    message.AppendLine("• " + categoryDTO.Title);
            message.AppendLine();
        }
    }
}