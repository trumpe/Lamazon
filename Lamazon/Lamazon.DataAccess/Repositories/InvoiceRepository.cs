using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lamazon.DataAccess.Repositories
{
    public class InvoiceRepository : BaseRepository<LamazonDbContext>, IRepository<Invoice>
    {
        public InvoiceRepository(LamazonDbContext context) : base(context) { }

        public IEnumerable<Invoice> GetAll()
        {
            return _db.Invoices
                .Include(i => i.Order)
                    .ThenInclude(o => o.OrdersProducts)
                        .ThenInclude(op => op.Product)
                .ToList();
        }

        public Invoice GetById(int id)
        {
            return _db.Invoices
                .Include(i => i.Order)
                    .ThenInclude(o => o.OrdersProducts)
                        .ThenInclude(op => op.Product)
                .FirstOrDefault(i => i.Id == id);
        }

        public int Insert(Invoice entity)
        {
            _db.Invoices.Add(entity);

            return _db.SaveChanges();
        }

        public int Update(Invoice entity)
        {
            Invoice invoice = _db.Invoices
                .FirstOrDefault(i => i.Id == entity.Id);

            invoice.DateOfPay = entity.DateOfPay;
            invoice.Address = entity.Address;
            invoice.PaymentMethod = entity.PaymentMethod;

            return _db.SaveChanges();
        }

        public int Delete(int id)
        {
            Invoice invoice = _db.Invoices
                .FirstOrDefault(i => i.Id == id);
            if (invoice == null)
                return -1;

            _db.Invoices.Remove(invoice);

            return _db.SaveChanges();
        }
    }
}
