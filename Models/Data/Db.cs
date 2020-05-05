using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Data
{
    public class Db : DbContext
    {
        public DbSet<AdminDTO> Admins { get; set; }
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<CategoryDTO> Categorys { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public Db(DbContextOptions<Db> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}