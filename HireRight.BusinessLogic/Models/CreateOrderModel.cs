﻿using HireRight.EntityFramework.CodeFirst.Models;

namespace HireRight.BusinessLogic.Models
{
    public class CreateOrderModel
    {
        public Client Client { get; set; }

        public Company Company { get; set; }

        public Order Order { get; set; }

        public Contact PrimaryContact { get; set; }

        public Contact SecondaryContact { get; set; }
    }
}