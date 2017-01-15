using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.BusinessLogic.Models
{
    public class CreateOrderModel
    {
        public Company Company { get; set; }

        public Order Order { get; set; }

        public Contact PrimaryContact { get; set; }

        public Contact SecondaryContact { get; set; }

        public CreateOrderModel()
        {
            Company = new Company();
            Order = new Order();
            PrimaryContact = new Contact();
            SecondaryContact = new Contact();
        }
    }
}