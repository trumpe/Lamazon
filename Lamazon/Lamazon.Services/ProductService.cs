using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Enums;
using Lamazon.Domain.Models;
using Lamazon.Services.Helpers;
using Lamazon.Services.Interfaces;
using Lamazon.WebModels.Enums;
using Lamazon.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lamazon.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductService(
            IRepository<Product> productRepo, 
            IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public IEnumerable<ProductViewModel> GetAllProducts()
        {
            //return _productRepo.GetAll()
            //    .Select(p => _mapper.Map<ProductViewModel>(p))
            //    .ToList();
            return _mapper.Map<IEnumerable<ProductViewModel>>(
                _productRepo.GetAll()
            );
        }

        public ProductViewModel GetProductById(int id)
        {
            Product product = _productRepo.GetById(id);
            if (product == null)
                throw new Exception("No such product");

            return _mapper.Map<ProductViewModel>(product);
        }

        public void CreateProduct(ProductViewModel product)
        {
            _productRepo.Insert(
                _mapper.Map<Product>(product)
            );
        }

        public void UpdateProduct(ProductViewModel product)
        {
            _productRepo.Update(
                _mapper.Map<Product>(product)
            );
        }

        public void RemoveProduct(int id)
        {
            _productRepo.Delete(id);
        }
    }
}
