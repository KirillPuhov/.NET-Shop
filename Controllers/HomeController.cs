using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.Data;
using OnlineShop.Models.ViewModels.Categorys;
using OnlineShop.Models.ViewModels.Order;
using OnlineShop.Models.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<ProductVM> item;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                item = db.Products.ToArray().OrderBy(x => x.Id).Select(x => new ProductVM(x)).ToList();
            }

            return View(item);
        }

        // GET: Home/Categories
        public ActionResult Categories()
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

        // GET: Home/Type
        public ActionResult Type(string type)
        {
            List<ProductVM> item;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                item = db.Products.Where(x => x.Type == type).Select(x => new ProductVM(x)).ToList();
            }

            return View(item);
        }

        // GET: Home/Full
        public ActionResult Full(int id)
        {
            List<ProductVM> item;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                item = db.Products.Where(x => x.Id == id).Select(x => new ProductVM(x)).ToList();
            }

            return View(item);
        }
    }
}