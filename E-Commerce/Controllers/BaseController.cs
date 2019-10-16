using E_Commerce.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var UserID = User.Identity.GetUserId();

            var data = db.WishLists.Where(x => x.UserId == UserID).ToList();
            ViewBag.cartBox = data.Count == 0 ? null : data;

            int count = db.WishLists.Where(x => x.UserId == UserID).ToList().Count();
            ViewBag.WlItemsNo = count;

            int? SubTotal = Convert.ToInt32(data.Sum(x => x.Item.Price));
            ViewBag.Total = SubTotal;

            base.OnActionExecuting(filterContext);
        }
    }
}