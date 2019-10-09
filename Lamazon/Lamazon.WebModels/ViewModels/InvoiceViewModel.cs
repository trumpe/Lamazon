using Lamazon.WebModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lamazon.WebModels.ViewModels
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public PaymentTypeViewModel PaymentMethod { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public int OrderId { get; set; }
    }
}
