using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using PizzaBoxClient.Models;

namespace PizzaBoxClient.Controllers
{
    public class StoreController : Controller
    {
        string StoreUrl = "https://localhost:44301/api/Store/";
        string CustomerUrl = "https://localhost:44301/api/Customer/";
        string PizzaUrl = "https://localhost:44301/api/Pizza/";
        string postUrl = "https://localhost:44301/api/Order/";
        public IActionResult Index()
        {
            IEnumerable<Store> stores = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(StoreUrl);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Store>>();
                    readTask.Wait();

                    stores = readTask.Result;
                }
                else
                {
                    stores = Enumerable.Empty<Store>();
                    ModelState.AddModelError(string.Empty, "Server error, Get Stores is empty");
                }
            }
            return View(stores);
        }

        public IActionResult showOrders(int id)
        {
            Store store = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(StoreUrl + id);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Store>();
                    readTask.Wait();

                    store = readTask.Result;
                }
                else
                {
                    store = null;
                    ModelState.AddModelError(string.Empty, "Server error, Get Stores is empty");
                }

            }
            return View(store.Orders);

        }

        public ActionResult Order(int id)
        {
            Store store = null;
            IEnumerable<Customer> customers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(StoreUrl + id);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Store>();
                    readTask.Wait();

                    store = readTask.Result;
                }
                else
                {
                    store = null;
                    ModelState.AddModelError(string.Empty, "Server error, Get Stores is empty");
                }

            }

            var sessionOrder = UtilsSession.GetCurrentOrder(HttpContext.Session);
            sessionOrder.Store = store;
            sessionOrder.StoreId = store.Id;
            sessionOrder.Date = DateTime.Now;
            UtilsSession.SaveOrder(HttpContext.Session, sessionOrder);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CustomerUrl);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Customer>>();
                    readTask.Wait();

                    customers = readTask.Result;
                }
                else
                {
                    customers = Enumerable.Empty<Customer>();
                    ModelState.AddModelError(string.Empty, "Server error, Get Customers is empty");
                }
            }
            return View(customers);

        }

        public ActionResult SelectPizza(int id)
        {
            Customer customer = null;
            IEnumerable<Pizza> pizzas = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CustomerUrl + id);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Customer>();
                    readTask.Wait();

                    customer = readTask.Result;
                }
                else
                {
                    customer = null;
                    ModelState.AddModelError(string.Empty, "Server error, Get Stores is empty");
                }

            }
            var sessionOrder = UtilsSession.GetCurrentOrder(HttpContext.Session);
            sessionOrder.Customer = customer;
            sessionOrder.CustomerId = customer.Id;
            UtilsSession.SaveOrder(HttpContext.Session, sessionOrder);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PizzaUrl);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Pizza>>();
                    readTask.Wait();

                    pizzas = readTask.Result;
                }
                else
                {
                    pizzas = Enumerable.Empty<Pizza>();
                    ModelState.AddModelError(string.Empty, "Server error, Get Customers is empty");
                }
            }
            return View(pizzas);

        }

        public Object ChoosePizza(int id)
        {
            Pizza pizza = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PizzaUrl + id);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Pizza>();
                    readTask.Wait();

                    pizza = readTask.Result;
                    System.Console.WriteLine("Pizza Retrieved!");
                }
                else
                {
                    pizza = null;
                    System.Console.WriteLine("Pizza Not Retrieved!");
                    ModelState.AddModelError(string.Empty, "Server error, Get pizza is empty");
                }

            }
            var sessionOrder = UtilsSession.GetCurrentOrder(HttpContext.Session);
            OrderDetail detail = new OrderDetail();
            detail.Pizza = pizza;
            detail.PizzaId = id;
            
            detail.OrderNumberNavigation = sessionOrder;

            if (sessionOrder.OrderDetails == null)
            {
                sessionOrder.OrderDetails = new List<OrderDetail>();
            }

            sessionOrder.OrderDetails.Add(detail);
            UtilsSession.SaveOrder(HttpContext.Session, sessionOrder);

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> postOrder(int numPizzas)
        {
            var sessionOrder = UtilsSession.GetCurrentOrder(HttpContext.Session);
            sessionOrder.OrderDetails.Last().NumberOfPizzas = numPizzas;
            UtilsSession.SaveOrder(HttpContext.Session, sessionOrder);

            var json = JsonConvert.SerializeObject(sessionOrder);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync(postUrl, data);
            var result = response.Content.ReadAsStringAsync().Result;

            
            return Redirect("Index");
        }
    }
}
