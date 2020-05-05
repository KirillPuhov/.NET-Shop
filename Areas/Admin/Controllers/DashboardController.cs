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
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.Title = "Dashboard";
            List<OrderVM> item;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                item = db.Orders.ToArray().OrderBy(x => x.Id).Select(x => new OrderVM(x)).ToList();
            }

            return View(item);
        }
    }
}