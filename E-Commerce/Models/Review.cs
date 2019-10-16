using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public string UserID { get; set; }
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] UserPhoto { get; set; }
        public Nullable<int> Rate { get; set; }
        public string Review1 { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public Nullable<bool> isDelete { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
        public virtual Items Item { get; set; }
    }
}