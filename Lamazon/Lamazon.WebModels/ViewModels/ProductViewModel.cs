using Lamazon.WebModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lamazon.WebModels.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryTypeViewModel Category { get; set; }
        public double Price { get; set; }
    }
}
