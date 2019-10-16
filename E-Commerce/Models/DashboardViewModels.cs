using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class DashboardViewModels
    {
        public int users_count { get; set; }
        public int categories_count { get; set; }
        public int items_count { get; set; }
        public int orders_count { get; set; }

    }
}