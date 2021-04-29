using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaBox.Storing.Entities;
using Microsoft.EntityFrameworkCore;

namespace PizzaBox.Storing.Repositories
{
    public class CustomerRepo
    {
        private readonly Entities.PizzaBoxContext context;

       
        public CustomerRepo(Entities.PizzaBoxContext context)
        {
            this.context = context;

        }

        public void AddCustomer(Customer customer)
        {
            context.Add(customer);
            context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            var CustomerToDelete = context.Customers.Find(customer.Id);
            if (CustomerToDelete != null)
            {
                context.Customers.Remove(CustomerToDelete);
                context.SaveChanges();
            }

        }

        public List<Pizza> GetPizzas()
        { 
            var pizzas = context.Pizzas.AsNoTracking().Include(pizza => pizza.Crust)
                .Include(pizza => pizza.Sauce)
                .Include(pizza => pizza.Topping)
                .ToList(); ;

            return pizzas;
        }

        public List<Sauce> GetSauces()
        {
            var sauces = context.Sauces.AsNoTracking().ToList(); ;

            return sauces;
        }

        public Pizza getPizzaById(int id)
        {
            var pizza = context.Pizzas.AsNoTracking()
                .Include(pizza => pizza.Crust)
                .Include(pizza => pizza.Sauce)
                .Include(pizza => pizza.Topping).AsNoTracking()
                .Where(x => x.Id == id).FirstOrDefault();

            if (pizza != null)
            {
                return pizza;
            }
            else
            {
                return null;
            }
        }

        public List<Topping> GetToppings()
        {
            var toppings = context.Toppings.AsNoTracking().ToList(); ;

            return toppings;
        }

        public Sauce getSauceById(int id)
        {
            var sauce = context.Sauces.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();

            if (sauce != null)
            {
                return sauce;
            }
            else
            {
                return null;
            }
        }

            public List<Crust> GetCrusts()
        {
            var crusts = context.Crusts.AsNoTracking().ToList(); ;

            return crusts;
        }

        public Topping getToppingById(int id)
        {
            var topping = context.Toppings.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();

            if (topping != null)
            {
                return topping;
            }
            else
            {
                return null;
            }
        }

        public List<Customer> GetCustomers()
        {

            var customers =
            context.Customers.AsNoTracking()
            .Include(customer => customer.Order).ThenInclude(order => order.OrderDetails).ThenInclude(orderdetail => orderdetail.Pizza).ThenInclude(Pizza => Pizza.Topping)
            .Include(customer => customer.Order).ThenInclude(order => order.OrderDetails).ThenInclude(orderdetail => orderdetail.Pizza).ThenInclude(Pizza => Pizza.Crust)
            .Include(customer => customer.Order).ThenInclude(order => order.OrderDetails).ThenInclude(orderdetail => orderdetail.Pizza).ThenInclude(Pizza => Pizza.Sauce)
            .Include(customer => customer.Order).ThenInclude(order => order.Store).AsNoTracking()
            .ToList();
            return customers;
        }

        public Crust getCrustById(int id)
        {
            var crust = context.Crusts.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();

            if (crust != null)
            {
                return crust;
            }
            else
            {
                return null;
            }
        }

        public Customer getCustomerByID(int id)
        {
            var customer = context.Customers.AsNoTracking().Where(x => x.Id == id)
            .Include(customer => customer.Order).ThenInclude(order => order.OrderDetails).ThenInclude(orderdetail => orderdetail.Pizza).ThenInclude(Pizza => Pizza.Topping)
            .Include(customer => customer.Order).ThenInclude(order => order.OrderDetails).ThenInclude(orderdetail => orderdetail.Pizza).ThenInclude(Pizza => Pizza.Crust)
            .Include(customer => customer.Order).ThenInclude(order => order.OrderDetails).ThenInclude(orderdetail => orderdetail.Pizza).ThenInclude(Pizza => Pizza.Sauce)
            .Include(customer => customer.Order).ThenInclude(order => order.Store).AsNoTracking()
            .FirstOrDefault();
            if (customer != null)
            {
                return customer;
            }
            else
            {
                return null;
            }
        }

        public Customer UpdateCustomer(Customer customer)
        {
            var customertemp = getCustomerByID(customer.Id);

            if (customer != null)
            {
                customertemp.Order = customer.Order;
                context.Update(customertemp);
                context.SaveChanges();
                return customertemp;
            }
            return customer;
        }

        public void addOrder(Order order)
        {

            var temp = new Order();

            temp.OrderDetails = order.OrderDetails;
            temp.OrderDetails.Last().Pizza = null;
            
            temp.Date = order.Date;
            temp.StoreId = order.StoreId;
            temp.CustomerId = order.CustomerId;
            
            context.Update(temp);
            context.SaveChanges();


            

            context.SaveChanges();

        }

        public List<Store> GetStores()
        {
            var stores = context.Stores.AsNoTracking().Include(stores => stores.Orders).ThenInclude(orders => orders.OrderDetails).ThenInclude(detail => detail.Pizza).ThenInclude(pizza => pizza.Topping)
                .Include(stores => stores.Orders).ThenInclude(orders => orders.OrderDetails).ThenInclude(detail => detail.Pizza).ThenInclude(pizza => pizza.Sauce)
                .Include(stores => stores.Orders).ThenInclude(orders => orders.OrderDetails).ThenInclude(detail => detail.Pizza).ThenInclude(pizza => pizza.Crust)
                .Include(stores => stores.Orders).ThenInclude(orders => orders.Customer).AsNoTracking()
                .ToList();

            return stores;
        }

        public Store getStoreById(int id)
        {
            var store = context.Stores.AsNoTracking().Include(stores => stores.Orders).ThenInclude(orders => orders.OrderDetails).ThenInclude(detail => detail.Pizza).ThenInclude(pizza => pizza.Topping)
                .Include(stores => stores.Orders).ThenInclude(orders => orders.OrderDetails).ThenInclude(detail => detail.Pizza).ThenInclude(pizza => pizza.Sauce)
                .Include(stores => stores.Orders).ThenInclude(orders => orders.OrderDetails).ThenInclude(detail => detail.Pizza).ThenInclude(pizza => pizza.Crust)
                .Include(stores => stores.Orders).ThenInclude(orders => orders.Customer).AsNoTracking()
                .Where(x => x.Id == id).FirstOrDefault();

            if (store != null)
            {
                return store;
            }
            else
            {
                return null;
            }
        }

        public List<Order> getOrders()
        {
            var orders = context.Orders.AsNoTracking().Include(order=> order.OrderDetails)
                .ThenInclude(detail => detail.Pizza).ThenInclude(pizza => pizza.Topping)
                .Include(order => order.OrderDetails).ThenInclude(detail => detail.Pizza).ThenInclude(pizza => pizza.Sauce)
                .Include(order => order.OrderDetails).ThenInclude(detail => detail.Pizza).ThenInclude(pizza => pizza.Crust).AsNoTracking().ToList(); ;


            return orders;
        }

        public Order getOrderById(int id)
        {
            var order = context.Orders.AsNoTracking().Where(x => x.Id == id).Include(order => order.OrderDetails)
                .ThenInclude(detail => detail.Pizza).ThenInclude(pizza => pizza.Topping)
                .Include(order => order.OrderDetails).ThenInclude(detail => detail.Pizza).ThenInclude(pizza => pizza.Sauce)
                .Include(order => order.OrderDetails).ThenInclude(detail => detail.Pizza).ThenInclude(pizza => pizza.Crust).AsNoTracking()
                .FirstOrDefault();

            if (order != null)
            {
                return order;
            }
            else
            {
                return null;
            }

        }
    }
}
