using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.Data;
using OnlineShop.Models.ViewModels.Categorys;
using OnlineShop.Models.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            List<CategoryVM> item;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using(Db db = new Db(options))
            {
                item = db.Categorys.ToArray().OrderBy(x => x.Id).Select(x => new CategoryVM(x)).ToList();
            }    

            return View(item);
        }

        // GET: Admin/Category/AddCategory
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        // POST: Admin/Category/AddCategory
        [HttpPost]
        public ActionResult AddCategory(ProductVM model)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                CategoryDTO category = new CategoryDTO();

                if (db.Categorys.Any(x => x.Title == model.Title))
                {
                    ModelState.AddModelError("", "That title already exist!");
                    return View(model);
                }
                else if (db.Categorys.Any(x => x.Type == model.Type))
                {
                    ModelState.AddModelError("", "That Type already exist!");
                    return View(model);
                }

                category.Title = model.Title;
                category.Type = model.Type;
                category.Description = model.Description;
                category.Image = model.Image;

                db.Categorys.Add(category);
                db.SaveChanges();
            }

            TempData["SM"] = "You have added a new category!";

            return RedirectToAction("Index");
        }

        // GET: Admin/Category/EditCategory
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            CategoryVM model;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                CategoryDTO category = db.Categorys.Find(id);

                if (category == null)
                {
                    return Content("The category does not exist!");
                }

                model = new CategoryVM(category);
            }
            return View(model);
        }

        // POST: Admin/Category/EditCategory
        [HttpPost]
        public ActionResult EditCategory(CategoryVM model)
        {
            int id = model.Id;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                CategoryDTO category = db.Categorys.Find(id);

                if (db.Categorys.Where(x => x.Id != id).Any(x => x.Title == model.Title))
                {
                    ModelState.AddModelError("", "That title already exist!");
                    return View(model);
                }
                else if (db.Categorys.Where(x => x.Id != id).Any(x => x.Type == model.Type))
                {
                    ModelState.AddModelError("", "That Type already exist!");
                    return View(model);
                }

                category.Title = model.Title;
                category.Type = model.Type;
                category.Description = model.Description;
                category.Image = model.Image;

                db.SaveChanges();
            }

            TempData["SM"] = "You have edited the category!";

            return RedirectToAction("Index");
        }

        // GET: Admin/Category/DetailsCategory
        [HttpGet]
        public ActionResult DetailsCategory(int id)
        {
            CategoryVM model;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                CategoryDTO category = db.Categorys.Find(id);

                if (category == null)
                {
                    return Content("The category does not exist!");
                }

                model = new CategoryVM(category);
            }
            return View(model);
        }

        // GET: Admin/Category/DeleteCategory
        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                CategoryDTO category = db.Categorys.Find(id);

                db.Categorys.Remove(category);

                db.SaveChanges();
            }

            TempData["SM"] = "Category deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}