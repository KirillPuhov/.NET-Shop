using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace OnlineShop.Models.Data
{
    [Table("tblOrder")]
    public class OrderDTO
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string ProductName { get; set; }

        public decimal PriceProduct { get; set; }

        public decimal TotalPrice { get; set; }

        public int Count { get; set; }
    }
}