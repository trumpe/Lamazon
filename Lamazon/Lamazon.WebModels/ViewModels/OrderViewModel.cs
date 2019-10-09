using Lamazon.WebModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lamazon.WebModels.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public StatusTypeViewModel Status { get; set; }
        public double Price => Products.Sum(p => p.Price);
        public UserViewModel User { get; set; }
        public List<ProductViewModel> Products { get; set; }
        //Plus invoice
    }
}
