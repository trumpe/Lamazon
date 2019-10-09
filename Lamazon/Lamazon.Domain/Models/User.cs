using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lamazon.Domain.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
