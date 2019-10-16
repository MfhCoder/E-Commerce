using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class WishList
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ItemId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Items Item { get; set; }

    }
}