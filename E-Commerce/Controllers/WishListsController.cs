using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;
using Microsoft.AspNet.Identity;

namespace E_Commerce.Controllers
{
    public class WishListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WishList
        public ActionResult Index()
        {

            var UserID = User.Identity.GetUserId();
            var wishlistProducts = db.WishLists.Where(x => x.UserId == UserID).ToList();
            return View(wishlistProducts);
        }

        //REMOVE ITEM FROM WISHLIST
        public ActionResult Remove(int id)
        {
            db.WishLists.Remove(db.WishLists.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Order(int totalAmount)
        {
            var UserId = User.Identity.GetUserId();
            Order Orders = new Order();
            Orders.UserId = UserId;
            Orders.TotalAmount = totalAmount;
            Orders.OrderDate = DateTime.Now;
            db.Orders.Add(Orders);

            db.WishLists.Where(p => p.UserId == UserId)
               .ToList().ForEach(p => db.WishLists.Remove(p));

            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
