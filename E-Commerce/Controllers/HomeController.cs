
using E_Commerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class HomeController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            return View(db.Categories.ToList());
        }

        public ActionResult GetProductsByCategory(string categoryName)
        {
            ViewBag.categoryName = categoryName;
            return View(db.Items.Where(x => x.Category.CategoryName == categoryName).ToList());
        }


        public ActionResult FilterByPrice(int minPrice, int maxPrice, string categoryName)
        {
            return View("GetProductsByCategory", db.Items.Where(x => x.Price >= minPrice && x.Price <= maxPrice ).ToList());
        }


        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Details(int ItemId)
        {
            if (ItemId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(ItemId);
            var reviews = db.Reviews.Where(x => x.ItemID == ItemId).ToList();
            ViewBag.Reviews = reviews;
            ViewBag.TotalReviews = reviews.Count();
            var ratedProd = db.Reviews.Where(x => x.ItemID == ItemId).ToList();
            int count = ratedProd.Count();
            int TotalRate = ratedProd.Sum(x => x.Rate).GetValueOrDefault();
            ViewBag.AvgRate = TotalRate > 0 ? TotalRate / count : 0;

            if (items == null)
            {
                return HttpNotFound();
            }
   
            
            return View(items);
        }


        [Authorize]
        public ActionResult GetItemsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var items = db.Items.Where(a => a.UserId == UserId);
            return View(items.ToList());
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Items.Where(a => a.Title.Contains(searchName)
            || a.Description.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.Description.Contains(searchName)).ToList();

            return View(result);
        }


        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("mohamedfathi853.mf@gmail.com", "MMfh0111");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("mohamedfathi853.mf@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "اسم المرسل: " + contact.Name + "<br>" +
                            "بريد المرسل: " + contact.Email + "<br>" +
                            "عنوان الرسالة: " + contact.Subject + "<br>" +
                            "نص الرسالة: <b>" + contact.Message + "</b>";
            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }

        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");

                }
                // to get the user details to load user Image
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault();

                return new FileContentResult(userImage.UserPhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }

    }
}