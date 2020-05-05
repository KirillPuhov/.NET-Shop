using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.Data;
using OnlineShop.Models.ViewModels.Cart;
using OnlineShop.Models.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(OrderVM model)
        {
            var cart = Session["Cart"] as List<CartVM> ?? new List<CartVM>();

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                OrderDTO order = new OrderDTO();

                foreach(var item in cart)
                {
                    order.Name = model.Name;
                    order.Surname = model.Surname;
                    order.Email = model.Email;
                    order.Phone = model.Phone;
                    order.Address = model.Address;
                    order.ProductName = item.ProductTitle;
                    order.PriceProduct = item.Price;
                    order.TotalPrice = item.Total;
                    order.Count = item.Quantity;

                    db.Orders.Add(order);
                    db.SaveChanges();
                }         
            }
            Session["Cart"] = null;
            Session.Clear();

            return RedirectPermanent("~/Cart/Index");
        }
    }
}