using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;
using HireRight.Models;

namespace HireRight.Infrastructure
{
    public static class NewOrderDTOExtensions
    {
        public static NewOrderDTO CreateOrderDTOFromViewModel(CreateOrderViewModel model)
        {
            model.Order.Positions = model.Order.PositionsToFill.Split(',').ToList();

            Company company = new Company();
            company.Name = model.Company.CompanyTitle;

            Order order = new Order();
            order.Company = company;
            order.Completed = null;
            order.Notes = model.Order.Notes;
            order.Quantity = (int)model.Order.Quantity;
            order.Status = OrderStatus.Pending;

            Contact primary = new Contact();
            primary.Address = model.Primary.Address.GetFullAddress();
            primary.CellNumber = model.Primary.CellPhone;
            primary.Company = company;
            primary.Email = model.Primary.Email;
            primary.IsAdmin = true;
            primary.IsPrimary = true;
            primary.Name = model.Primary.FirstName + " " + model.Primary.LastName;
            primary.OfficeNumber = model.Primary.OfficePhone;

            Contact secondary = new Contact();
            secondary.OfficeNumber = model.Admin.PhoneNumber;
            secondary.Email = model.Admin.Email;
            secondary.Name = model.Admin.FirstName + " " + model.Admin.LastName;
            
            return new NewOrderDTO(order, company, primary, secondary);
        }
    }
}

