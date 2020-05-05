using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.Data;
using OnlineShop.Models.ViewModels.Admins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Admin/Admin
        [HttpPost]
        [Authorize]
        public ActionResult Index(AdminVM model)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using(Db db = new Db(options))
            {
                AdminDTO admin = new AdminDTO();
                if (db.Admins.Any(x => x.Email == model.Email))
                {
                    ModelState.AddModelError("", "That Email already exist!");
                    return View(model);
                }
                else if (db.Admins.Any(x => x.Phone == model.Phone))
                {
                    ModelState.AddModelError("", "That Phone already exist!");
                    return View(model);
                }

                admin.Email = model.Email;
                admin.Name = model.Name;
                admin.Surname = model.Surname;
                admin.Role = model.Role;
                admin.Phone = model.Phone;
                admin.Password = model.Password;

                db.Admins.Add(admin);
                db.SaveChanges();
            }

            return RedirectPermanent("~/Admin/Dashboard/Index");
        }

        // GET: Admin/Admin/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin/Admin/Login
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            List<AdminVM> item;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using(Db db = new Db(options))
            {
                item = db.Admins.Where(x => x.Email == email).Select(x => new AdminVM(x)).ToList();
                foreach(var el in item )
                {
                    if(el.Password == password)
                    {
                        string name = el.Name;
                        string phone = el.Phone;
                        var cookie = FormsAuthentication.GetAuthCookie(name, true);
                        Response.Cookies.Add(cookie);
                        TempData["LoginName"] = name;
                        return RedirectPermanent("~/Admin/Dashboard/Index");
                    }
                }
            }
            return View();
        }

        // GET: Admin/Admin/Admins
        [HttpGet]
        [Authorize]
        public ActionResult Admins()
        {
            List<AdminVM> item;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                item = db.Admins.ToArray().OrderBy(x => x.Id).Select(x => new AdminVM(x)).ToList();
            }

            return View(item);
        }

        // GET: Admin/Admin/EditAdmin
        [HttpGet]
        [Authorize]
        public ActionResult EditAdmin(int id)
        {
            AdminVM model;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                AdminDTO admin = db.Admins.Find(id);

                if (admin == null)
                {
                    return Content("The product does not exist!");
                }

                model = new AdminVM(admin);
            }
            return View(model);
        }

        // POST: Admin/Admin/EditAdmin
        [HttpPost]
        [Authorize]
        public ActionResult EditAdmin(AdminVM model)
        {
            int id = model.Id;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                AdminDTO product = db.Admins.Find(id);

                if (db.Admins.Where(x => x.Id != id).Any(x => x.Email == model.Email))
                {
                    ModelState.AddModelError("", "That Email already exist!");
                    return View(model);
                }
                else if (db.Admins.Where(x => x.Id != id).Any(x => x.Phone == model.Phone))
                {
                    ModelState.AddModelError("", "That Phone already exist!");
                    return View(model);
                }

                product.Email = model.Email;
                product.Name = model.Name;
                product.Surname = model.Surname;
                product.Role = model.Role;
                product.Phone = model.Phone;
                product.Password = model.Password;

                db.SaveChanges();
            }

            TempData["SM"] = "You have edited the admin!";

            return RedirectToAction("Admins");
        }

        // GET: Admin/Admin/DeleteAdmin
        [HttpGet]
        [Authorize]
        public ActionResult DeleteAdmin(int id)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                AdminDTO admin = db.Admins.Find(id);

                db.Admins.Remove(admin);

                db.SaveChanges();
            }

            TempData["SM"] = "Admin deleted successfully!";

            return RedirectToAction("Admins");
        }
    }
}