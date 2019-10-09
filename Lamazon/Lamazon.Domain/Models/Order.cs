using Lamazon.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lamazon.Domain.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public StatusType Status { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual IEnumerable<OrderProduct> OrdersProducts { get; set; }
    }
}
