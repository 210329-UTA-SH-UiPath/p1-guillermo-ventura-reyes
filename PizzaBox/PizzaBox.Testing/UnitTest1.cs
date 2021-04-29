using PizzaBox.Storing.Entities;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PizzaBox.Testing
{
    public class UnitTest1
    {
        static DbContextOptionsBuilder<PizzaBox.Storing.Entities.PizzaBoxContext> optionsBuilder = new DbContextOptionsBuilder<PizzaBox.Storing.Entities.PizzaBoxContext>();
        
        [Fact]
        public void SauceTest()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var sauces = dbContext.Sauces.AsNoTracking().ToList();
            
            Assert.Equal(sauces.Count(), 4);
          

        }
        [Fact]
        public void toppingTest()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var toppings = dbContext.Toppings.AsNoTracking().ToList();

            Assert.Equal(toppings.Count(), 6);


        }
        [Fact]
        public void storesTest()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var stores = dbContext.Stores.AsNoTracking().ToList();

            Assert.Equal(stores.Count(), 3);


        }
        [Fact]
        public void pizzasTest()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var pizzas = dbContext.Pizzas.AsNoTracking().ToList();

            Assert.Equal(pizzas.Count(), 4);


        }
        [Fact]
        public void ordersTest()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var orders = dbContext.Orders.AsNoTracking().ToList();

            Assert.Equal(orders.Count(), 6);


        }
        [Fact]
        public void customersTest()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var customers = dbContext.Customers.AsNoTracking().ToList();

            Assert.Equal(customers.Count(), 6);


        }
    }
}
