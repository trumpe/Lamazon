using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lamazon.DataAccess.Repositories
{
    public class ProductRepository : BaseRepository<LamazonDbContext>, IRepository<Product>
    {
        public ProductRepository(LamazonDbContext context) : base(context) { }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products
                .ToList();
        }

        public Product GetById(int id)
        {
            return _db.Products
                .FirstOrDefault(p => p.Id == id);
        }

        public int Insert(Product entity)
        {
            _db.Products.Add(entity);
            return _db.SaveChanges();
        }

        public int Update(Product entity)
        {
            Product product = _db.Products
                .FirstOrDefault(p => p.Id == entity.Id);
            if (product == null)
                return -1;

            product.Name = entity.Name;
            product.Description = entity.Description;
            product.Category = entity.Category;
            product.Price = entity.Price;

            return _db.SaveChanges();
        }

        public int Delete(int id)
        {
            Product product = _db.Products
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
                return -1;

            _db.Products.Remove(product);
            return _db.SaveChanges();
        }
    }
}
