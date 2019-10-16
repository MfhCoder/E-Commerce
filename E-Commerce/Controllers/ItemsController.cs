using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace E_Commerce.Controllers
{
    public class ItemsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Category);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            var reviews = db.Reviews.Where(x => x.ItemID == id).ToList();
            ViewBag.Reviews = reviews;
            ViewBag.TotalReviews = reviews.Count();
            var ratedProd = db.Reviews.Where(x => x.ItemID == id).ToList();
            int count = ratedProd.Count();
            int TotalRate = ratedProd.Sum(x => x.Rate).GetValueOrDefault();
            ViewBag.AvgRate = TotalRate > 0 ? TotalRate / count : 0;

            if (items == null)
            {
                return HttpNotFound();
            }
   
            return View(items);
        }


        //ADD REVIEWS ABOUT PRODUCT
        public ActionResult AddReview(int ItemID, FormCollection getReview)
        {
            var UserID = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
            Review r = new Review();
            r.UserID = UserID;
            r.ItemID = ItemID;
            r.Name = user.UserName;
            r.UserPhoto = user.UserPhoto;
            r.Email = user.Email;
            r.Review1 = getReview["message"];
            r.Rate = Convert.ToInt32(getReview["rate"]);
            r.DateTime = DateTime.Now;

            db.Reviews.Add(r);
            db.SaveChanges();
            return RedirectToAction("Details/" + ItemID + "");
        }


        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Items Item, HttpPostedFileBase upload)
        {
            var UserId = User.Identity.GetUserId();
            var item = new Items();

            if (ModelState.IsValid)
            {
                Item.UserId = UserId;
                Item.AddedDate = DateTime.Now;
                string path = Path.Combine(Server.MapPath("~/images"), upload.FileName);
                upload.SaveAs(path);
                Item.ItemImage = upload.FileName;
                db.Items.Add(Item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", Item.CategoryId);
            return View(Item);
        }


        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", items.CategoryId);
            return View(items);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Items items, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
           
                string oldPath = Path.Combine(Server.MapPath("~/images"), items.ItemImage);

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/images"), upload.FileName);
                    upload.SaveAs(path);
                    items.ItemImage = upload.FileName;
                }

                db.Entry(items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                      
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", items.CategoryId);
            return View(items);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Items items = db.Items.Find(id);

            //delete image from the server
            var itemImage = items.ItemImage;
            string fullPath = Request.MapPath("~/images/" + itemImage);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.Items.Remove(items);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //WISHLIST
        public ActionResult WishList(int id)
        {
            var UserID = User.Identity.GetUserId();
            WishList wl = new WishList();
            wl.ItemId = id;
            wl.UserId = UserID;

            db.WishLists.Add(wl);
            db.SaveChanges();
            //if (TempData["returnURL"].ToString() == "/")
            //{
                return RedirectToAction("Index", "Home");
            //}
            //return Redirect(TempData["returnURL"].ToString());
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
