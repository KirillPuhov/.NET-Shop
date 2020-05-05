using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.Data;
using OnlineShop.Models.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CheckorderController : Controller
    {
        // GET: Admin/Checkorder
        public ActionResult Index()
        {
            ViewBag.Title = "Order";
            List<OrderVM> item;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                item = db.Orders.ToArray().OrderBy(x => x.Id).Select(x => new OrderVM(x)).ToList();
            }

            return View(item);
        }

        [HttpGet]
        public ActionResult DeleteOrder(int id)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                OrderDTO order = db.Orders.Find(id);

                db.Orders.Remove(order);

                db.SaveChanges();
            }

            TempData["SM"] = "Order deleted successfully!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Success(int id)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                OrderDTO order = db.Orders.Find(id);

                db.Orders.Remove(order);

                db.SaveChanges();
            }

            TempData["SM"] = "Successfully!";

            return RedirectToAction("Index");
        }
    }
}