using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using HireRight.BusinessLogic.Abstract;
using HireRight.BusinessLogic.Extensions;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Concrete
{
    public class ProductsBusinessLogic : IProductsBusinessLogic
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsBusinessLogic(IProductsRepository repo)
        {
            _productsRepository = repo;
        }

        public async Task<ProductDTO> Add(ProductDTO productDto)
        {
            Product productToAdd = ConvertDtoToModel(productDto);

            return ConvertModelToDto(await _productsRepository.Add(productToAdd).ConfigureAwait(false));
        }

        public Product ConvertDtoToModel(ProductDTO dto)
        {
            Product model = new Product(dto.Title, dto.Price, dto.RowGuid, new List<Discount>());
            model.RowGuid = dto.RowGuid;
            model.CreatedUtc = dto.CreatedUtc;
            model.Discounts = dto.Discounts.Select(x => x.ConvertDtoToModel()).ToList();

            return model;
        }

        public ProductDTO ConvertModelToDto(Product model)
        {
            ProductDTO dto = new ProductDTO();
            dto.RowGuid = model.RowGuid;
            dto.CreatedUtc = model.CreatedUtc;
            dto.Title = model.Title;
            dto.Price = model.Price;
            dto.Discounts = model.Discounts.Select(x => x.ConvertModelToDto()).ToList();

            return dto;
        }

        public async Task<ProductDTO> Get(Guid productGuid)
        {
            Product product = await _productsRepository.Get(productGuid).ConfigureAwait(false);

            return ConvertModelToDto(product);
        }

        public async Task<PagingResultDTO<ProductDTO>> Get(ProductFilter filterParameters)
        {
            PageResult<Product> products = await _productsRepository.Get(filterParameters).ConfigureAwait(false);

            return products.PageResultToDto(ConvertModelToDto);
        }

        public async Task<ProductDTO> Update(ProductDTO productDto)
        {
            Product productToUpdate = ConvertDtoToModel(productDto);

            return ConvertModelToDto(await _productsRepository.Update(productToUpdate).ConfigureAwait(false));
        }
    }
}