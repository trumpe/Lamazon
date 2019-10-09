using Lamazon.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lamazon.Services.Interfaces
{
    public interface IInvoiceService
    {
        void CreateInvoice(InvoiceViewModel invoice);
        InvoiceViewModel GetInvoice(int orderId);
    }
}
