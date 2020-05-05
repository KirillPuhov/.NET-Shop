using OnlineShop.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ViewModels.Products
{
    public class ProductVM
    {
        public ProductVM()
        {

        }

        public ProductVM(ProductDTO row)
        {
            Id = row.Id;
            Title = row.Title;
            Type = row.Type;
            Category = row.Category;
            Producer = row.Producer;
            ProductModel = row.ProductModel;
            Description = row.Description;
            Country = row.Country;
            Date = row.Date;
            Price = row.Price;
            Image = row.Image;
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Producer { get; set; }

        [Required]
        public string ProductModel { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Image { get; set; }
    }
}