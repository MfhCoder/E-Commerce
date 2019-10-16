using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;

    namespace E_Commerce.Controllers
    {
        public class ManageUsersController : Controller
        {
            public ApplicationDbContext context = new ApplicationDbContext();

            // GET: ManageUsersController
            public ActionResult Index()
            {
                return View();
            }

            public ActionResult UsersWithRoles()
            {
                var usersWithRoles = (from user in context.Users
                                      select new
                                      {
                                          UserId = user.Id,
                                          Username = user.UserName,
                                          Email = user.Email,
                                          RoleNames = (from userRole in user.Roles
                                                       join role in context.Roles on userRole.RoleId
                                                       equals role.Id
                                                       select role.Name).ToList()
                                      }).ToList().Select(p => new Users_in_Role_ViewModel()

                                      {
                                          UserId = p.UserId,
                                          Username = p.Username,
                                          Email = p.Email,
                                          Role = string.Join(",", p.RoleNames)
                                      });


                return View(usersWithRoles);
            }

            public async Task<ActionResult> DeleteUser(string userId)
            {
                if (userId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserManager<IdentityUser> UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());

            //get User Data from Userid
            var user = await UserManager.FindByIdAsync(userId);

                //List Logins associated with user
                var logins = user.Logins;
                   //Gets list of Roles associated with current user
                var rolesForUser = await UserManager.GetRolesAsync(userId);

            //delete user Itemes
            var UserItems = context.Items.Where(a => a.UserId == userId);

            foreach (var Items in UserItems.ToList())
            {

                context.Items.Remove(Items);
                context.SaveChanges();

            }

            //delete user reviews
            var UserReviews = context.Reviews.Where(a => a.UserID == userId);

            foreach (var Reviews in UserReviews.ToList())
            {

                context.Reviews.Remove(Reviews);
                context.SaveChanges();

            }

            using (var transaction = context.Database.BeginTransaction())
                {
                    foreach (var login in logins.ToList())
                    {
                        await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                    }
               

                if (rolesForUser.Count() > 0)
                    {
                        foreach (var item in rolesForUser.ToList())
                        {
                            // item should be the name of the role
                            var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
                        }
                    }

                    //Delete User
                    await UserManager.DeleteAsync(user);

                    TempData["Message"] = "User Deleted Successfully. ";
                    TempData["MessageValue"] = "1";
                    //transaction.commit();
                }

                return RedirectToAction("UsersWithRoles", "ManageUsers", new { area = "", });
            }

        }
    }
