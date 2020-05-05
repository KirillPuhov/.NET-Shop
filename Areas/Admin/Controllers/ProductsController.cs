using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineShop.Models.Data;
using OnlineShop.Models.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        public ActionResult Index()
        {
            List<ProductVM> item;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using(Db db = new Db(options))
            {
                item = db.Products.ToArray().OrderBy(x => x.Id).Select(x => new ProductVM(x)).ToList();
            }

            return View(item);
        }

        // GET: Admin/Products/AddPage
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        // POST: Admin/Products/AddPage
        [HttpPost]
        public ActionResult AddProduct(ProductVM model)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                ProductDTO product = new ProductDTO();

                if (db.Products.Any(x => x.Title == model.Title))
                {
                    ModelState.AddModelError("", "That title already exist!");
                    return View(model);
                }
                else if (db.Products.Any(x => x.ProductModel == model.ProductModel))
                {
                    ModelState.AddModelError("", "That ProductModel already exist!");
                    return View(model);
                }

                product.Title = model.Title;
                product.Type = model.Type;
                product.Category = model.Category;
                product.Producer = model.Producer;
                product.ProductModel = model.ProductModel;
                product.Description = model.Description;
                product.Country = model.Country;
                product.Date = model.Date;
                product.Price = model.Price;
                product.Image = model.Image;

                db.Products.Add(product);
                db.SaveChanges();
            }

            TempData["SM"] = "You have added a new product!";

            return RedirectToAction("Index");
        }

        // GET: Admin/Products/EditProduct
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            ProductVM model;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                ProductDTO dto = db.Products.Find(id);

                if (dto == null)
                {
                    return Content("The product does not exist!");
                }

                model = new ProductVM(dto);
            }
            return View(model);
        }

        // POST: Admin/Products/EditProduct
        [HttpPost]
        public ActionResult EditProduct(ProductVM model)
        {
            int id = model.Id;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using(Db db = new Db(options))
            {
                ProductDTO product = db.Products.Find(id);

                if (db.Products.Where(x => x.Id != id).Any(x => x.Title == model.Title))
                {
                    ModelState.AddModelError("", "That title already exist!");
                    return View(model);
                }
                else if (db.Products.Where(x => x.Id != id).Any(x => x.ProductModel == model.ProductModel))
                {
                    ModelState.AddModelError("", "That ProductModel already exist!");
                    return View(model);
                }

                product.Title = model.Title;
                product.Type = model.Type;
                product.Category = model.Category;
                product.Producer = model.Producer;
                product.ProductModel = model.ProductModel;
                product.Description = model.Description;
                product.Country = model.Country;
                product.Date = model.Date;
                product.Price = model.Price;
                product.Image = model.Image;

                db.SaveChanges();
            }

            TempData["SM"] = "You have edited the product!";

            return RedirectToAction("Index");
        }

        // GET: Admin/Products/DetailsProduct
        [HttpGet]
        public ActionResult DetailsProduct(int id)
        {
            ProductVM model;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                ProductDTO dto = db.Products.Find(id);

                if (dto == null)
                {
                    return Content("The product does not exist!");
                }

                model = new ProductVM(dto);
            }
            return View(model);
        }

        // GET: Admin/Products/DeleteProduct
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                ProductDTO product = db.Products.Find(id);

                db.Products.Remove(product);

                db.SaveChanges();
            }

            TempData["SM"] = "Product deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}