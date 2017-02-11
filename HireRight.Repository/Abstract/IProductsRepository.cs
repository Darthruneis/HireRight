using DataTransferObjects.Filters.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.Repository.Abstract
{
    public interface IProductsRepository
    {
        Task<Product> Add(Product itemToAdd);

        Task<List<Product>> Get(ProductFilter filter);

        Task<Product> Get(Guid itemGuid);

        Task<Product> Update(Product itemToUpdate);
    }
}