using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.Data;
using OnlineShop.Models.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Schema;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session["Cart"] as List<CartVM> ?? new List<CartVM>();

            if (cart.Count == 0 || Session["Cart"] == null)
            {
                ViewBag.Message = "Your cart is empty!";
                return View();
            }

            decimal total = 0m;

            foreach(var item in cart)
            {
                total += item.Total;
            }

            ViewBag.GrandTotal = total;

            return View(cart);
        }

        public ActionResult CartPartial()
        {
            CartVM model = new CartVM();

            int qty = 0;

            decimal price = 0m;

            if(Session["Cart"] != null)
            {
                var list = (List<CartVM>)Session["Cart"];

                foreach(var item in list)
                {
                    qty += item.Quantity;
                    price += item.Quantity * item.Price;
                }

                model.Quantity = qty;
                model.Price = price;
            }
            else
            {
                model.Quantity = 0;
                model.Price = 0m;
            }

            return PartialView("_CartPartial", model);
        }

        public ActionResult PricePartial()
        {
            CartVM model = new CartVM();

            int qty = 0;

            decimal price = 0m;

            if (Session["Cart"] != null)
            {
                var list = (List<CartVM>)Session["Cart"];

                foreach (var item in list)
                {
                    qty += item.Quantity;
                    price += item.Quantity * item.Price;
                }

                model.Quantity = qty;
                model.Price = price;
            }
            else
            {
                model.Quantity = 0;
                model.Price = 0m;
            }

            return PartialView("_Price", model);
        }

        public ActionResult CoutPartial()
        {
            CartVM model = new CartVM();

            int qty = 0;

            decimal price = 0m;

            if (Session["Cart"] != null)
            {
                var list = (List<CartVM>)Session["Cart"];

                foreach (var item in list)
                {
                    qty += item.Quantity;
                    price += item.Quantity * item.Price;
                }

                model.Quantity = qty;
                model.Price = price;
            }
            else
            {
                model.Quantity = 0;
                model.Price = 0m;
            }

            return PartialView("_Count", model);
        }

        public ActionResult AddToCartPartial(int id)
        {
            List<CartVM> cart = Session["Cart"] as List<CartVM> ?? new List<CartVM>();

            CartVM model = new CartVM();

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using(Db db = new Db(options))
            {
                ProductDTO product = db.Products.Find(id);

                var productInCart = cart.FirstOrDefault(x => x.ProductId == id);

                if(productInCart == null)
                {
                    cart.Add(new CartVM()
                    {
                        ProductId = product.Id,
                        ProductTitle = product.Title,
                        Quantity = 1,
                        Price = product.Price,
                        Image = product.Image
                    });
                }
                else
                {
                    productInCart.Quantity++;
                }
            }

            int qty = 0;
            decimal price = 0m;

            foreach(var item in cart)
            {
                qty += item.Quantity;
                price += item.Quantity * item.Price;
            }

            model.Quantity = qty;
            model.Price = price;

            Session["Cart"] = cart;

            return PartialView("_AddToCartPartial", model);
        }

        public ActionResult IncrementProduct(int ProductId)
        {
            // Объявляем лист cart
            List<CartVM> cart = Session["Cart"] as List<CartVM>;

            CartVM model = new CartVM();

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                var productInCart = cart.FirstOrDefault(x => x.ProductId == ProductId);
                productInCart.Quantity++;
            }

            int qty = 0;
            decimal price = 0m;

            foreach (var item in cart)
            {
                qty += item.Quantity;
                price += item.Quantity * item.Price;
            }

            model.Quantity = qty;
            model.Price = price;

            Session["Cart"] = cart;

            // Возвращаем JSON ответ с данными
            return PartialView("_AddToCartPartial", model);
        }

        public ActionResult DecrementProduct(int ProductId)
        {
            // Объявляем лист cart
            List<CartVM> cart = Session["cart"] as List<CartVM>;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                // Получаем модель CartVM из листа
                CartVM model = cart.FirstOrDefault(x => x.ProductId == ProductId);

                // Отнимаем количество
                if (model.Quantity > 1)
                    model.Quantity--;
                else
                {
                    model.Quantity = 0;
                    cart.Remove(model);
                }

                // Возвращаем JSON ответ с данными
                return PartialView("_AddToCartPartial", model);
            }
        }

        public void RemoveProduct(int ProductId)
        {
            // Объявляем лист cart
            List<CartVM> cart = Session["Cart"] as List<CartVM>;

            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            var options = optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;").Options;

            using (Db db = new Db(options))
            {
                // Получаем модель CartVM из листа
                CartVM model = cart.FirstOrDefault(x => x.ProductId == ProductId);

                cart.Remove(model); 
            }
        }
    }
}