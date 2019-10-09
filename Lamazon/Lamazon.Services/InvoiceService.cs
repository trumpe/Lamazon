using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Models;
using Lamazon.Services.Interfaces;
using Lamazon.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lamazon.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository<Invoice> _invoiceRepo;
        private readonly IMapper _mapper;

        public InvoiceService(IRepository<Invoice> invoiceRepo, IMapper mapper)
        {
            _invoiceRepo = invoiceRepo;
            _mapper = mapper;
        }

        public void CreateInvoice(InvoiceViewModel invoice)
        {
            _invoiceRepo.Insert(
                _mapper.Map<Invoice>(invoice)
            );
        }

        public InvoiceViewModel GetInvoice(int orderId)
        {
            Invoice invoice = _invoiceRepo.GetAll()
                .FirstOrDefault(i => i.OrderId == orderId);
            if (invoice == null)
                throw new Exception("Invoice is not generated");

            return _mapper.Map<InvoiceViewModel>(invoice);
        }
    }
}
