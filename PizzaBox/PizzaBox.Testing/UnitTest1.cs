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
            
            Assert.Equal(4, sauces.Count);
          

        }
        [Fact]
        public void toppingTest()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var toppings = dbContext.Toppings.AsNoTracking().ToList();

            Assert.Equal( 6, toppings.Count);


        }
        [Fact]
        public void storesTest()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var stores = dbContext.Stores.AsNoTracking().ToList();

            Assert.Equal( 3, stores.Count);


        }
        [Fact]
        public void pizzasTest()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var pizzas = dbContext.Pizzas.AsNoTracking().ToList();

            Assert.Equal(6,pizzas.Count);


        }
        [Fact]
        public void ordersTest()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var orders = dbContext.Orders.AsNoTracking().ToList();

            Assert.Equal(6, orders.Count);


        }
        [Fact]
        public void customersTest()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var customers = dbContext.Customers.AsNoTracking().ToList();

            Assert.Equal( 3, customers.Count);


        }

        [Fact]
        public void customersTest2()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var customers = dbContext.Customers.AsNoTracking().ToList();

            Assert.Equal(3, customers.Count);


        }

        [Fact]
        public void storesTest2()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var stores = dbContext.Stores.AsNoTracking().ToList();

            Assert.Equal("Giuseppe's", stores[0].Name);


        }

        [Fact]
        public void storesTest3()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var stores = dbContext.Stores.AsNoTracking().ToList();

            Assert.Equal("Kibble's", stores[1].Name);


        }

        [Fact]
        public void storesTest4()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var stores = dbContext.Stores.AsNoTracking().ToList();

            Assert.Equal("Oval Pizza", stores[2].Name);


        }

        [Fact]
        public void storesTest5()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var stores = dbContext.Stores.AsNoTracking().ToList();

            Assert.NotEqual("Square Pizza", stores[2].Name);


        }

        [Fact]
        public void SauceTest2()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var sauces = dbContext.Sauces.AsNoTracking().ToList();

            Assert.Equal("Regular", sauces[0].Name);


        }
        [Fact]
        public void SauceTest3()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var sauces = dbContext.Sauces.AsNoTracking().ToList();

            Assert.NotEqual("Not Regular", sauces[0].Name);


        }
        [Fact]
        public void SauceTest4()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var sauces = dbContext.Sauces.AsNoTracking().ToList();

            Assert.Equal("Spicy", sauces[1].Name);


        }
        [Fact]
        public void SauceTest5()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var sauces = dbContext.Sauces.AsNoTracking().ToList();

            Assert.Equal("Garlic", sauces[2].Name);


        }

        [Fact]
        public void SauceTest6()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var sauces = dbContext.Sauces.AsNoTracking().ToList();

            Assert.Equal("Squid Ink", sauces[3].Name);


        }

        [Fact]
        public void SauceTest7()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var sauces = dbContext.Sauces.AsNoTracking().ToList();

            Assert.NotEqual("squid ink", sauces[3].Name);


        }

        [Fact]
        public void CrustTest()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var crusts = dbContext.Crusts.AsNoTracking().ToList();

            Assert.Equal(4, crusts.Count);


        }

        [Fact]
        public void CrustTest2()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var crusts = dbContext.Crusts.AsNoTracking().ToList();

            Assert.Equal("Thin", crusts[0].Name);


        }

        [Fact]
        public void CrustTest3()
        {
            optionsBuilder.UseSqlServer("Server=tcp:guillermo-rev-demo.database.windows.net,1433;Initial Catalog=BoxPizza;Persist Security Info=False;User ID=dev;Password=Mastersw1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            var dbContext = new PizzaBox.Storing.Entities.PizzaBoxContext(optionsBuilder.Options);

            var crusts = dbContext.Crusts.AsNoTracking().ToList();

            Assert.Equal("Stuffed", crusts[2].Name);


        }
    }
}
