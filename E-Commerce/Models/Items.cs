using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Items
    {
        public int Id { get; set; }

        [DisplayName("Item Name")]
        public string Title { get; set; }
         
        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Item Photo")]
        public string ItemImage { get; set; }

        public DateTime? AddedDate { get; set; }

        public int Price { get; set; }

        [DisplayName("Type")]
        public int CategoryId { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<WishList> WishList { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual Category Category { get; set; }

    }
}