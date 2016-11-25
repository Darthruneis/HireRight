using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.Repository.Abstract
{
    public interface IProductsRepository
    {
        Task<Product> Add(Product itemToAdd);

        Task<List<Product>> Get(ProductFilter filterParameters);

        Task<Product> Get(Guid itemGuid);

        Task<Product> Update(Product itemToUpdate);
    }
}