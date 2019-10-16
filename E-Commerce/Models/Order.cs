using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int TotalAmount { get; set; }

        public DateTime? OrderDate { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}