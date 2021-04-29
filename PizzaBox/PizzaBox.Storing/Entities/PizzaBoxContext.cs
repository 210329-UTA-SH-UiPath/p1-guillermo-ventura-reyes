using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaBox.Storing.Entities
{
   public  class PizzaBoxContext:DbContext
    {
        public PizzaBoxContext()
        {
            
        }

        public PizzaBoxContext(DbContextOptions<PizzaBoxContext> options)
        {
            
        }

        public virtual DbSet<Crust> Crusts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Sauce> Sauces { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Topping> Toppings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(new Customer(-1,"John", 1234));

            modelBuilder.Entity<OrderDetail>(entity =>
           {
               entity.HasKey(e => new { e.OrderId, e.PizzaId });
           });
        }
    }
}
