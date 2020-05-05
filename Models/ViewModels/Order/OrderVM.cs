using OnlineShop.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ViewModels.Order
{
    public class OrderVM
    {
        public OrderVM()
        {

        }

        public OrderVM(OrderDTO row)
        {
            Id = row.Id;
            Name = row.Name;
            Surname = row.Surname;
            Email = row.Email;
            Phone = row.Phone;
            Address = row.Address;
            ProductName = row.ProductName;
            PriceProduct = row.PriceProduct;
            TotalPrice = row.TotalPrice;
            Count = row.Count;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal PriceProduct { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public int Count { get; set; }
    }
}