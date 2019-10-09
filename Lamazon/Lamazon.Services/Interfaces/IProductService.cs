using Lamazon.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lamazon.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductViewModel> GetAllProducts();
        ProductViewModel GetProductById(int id);
        void CreateProduct(ProductViewModel product);
        void UpdateProduct(ProductViewModel product);
        void RemoveProduct(int id);
    }
}
