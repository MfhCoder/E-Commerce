using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dashboard
        [Authorize]
        public ActionResult Index()
        {
            DashboardViewModels dashboard = new DashboardViewModels();

            dashboard.users_count = db.Users.Count();
            dashboard.categories_count = db.Categories.Count();
            dashboard.items_count = db.Items.Count();
            dashboard.orders_count = db.Orders.Count();

            return View(dashboard);
        }
    }
}