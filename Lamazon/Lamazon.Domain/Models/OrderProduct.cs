using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lamazon.Domain.Models
{
    public class OrderProduct
    {
        [Key, Column(Order = 0)]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
