using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Data
{
    [Table("tblProducts")]
    public class ProductDTO
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public string Producer { get; set; }

        public string ProductModel { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public string Date { get; set; }

        public int Price { get; set; }

        public string Image { get; set; }
    }
}