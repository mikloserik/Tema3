using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Tema2.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }

    public class CartDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}