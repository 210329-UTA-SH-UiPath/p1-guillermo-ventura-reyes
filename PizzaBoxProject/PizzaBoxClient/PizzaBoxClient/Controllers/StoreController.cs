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
        string PostPizzaUrl = "https://localhost:44301/api/Pizza/";
        string sauceUrl = "https://localhost:44301/api/Sauce/";
        string crustUrl = "https://localhost:44301/api/Crusts/";
        string toppingUrl = "https://localhost:44301/api/Topping/";

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

        public ActionResult ChoosePizza(int id)
        {
            Pizza pizza = null;
            ViewBag.PresetTotalError = TempData["error"];
            if (ViewBag.PresetTotalError == null)
            {
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
            }
            

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PostOrder(int numPizzas)
        {
            

            var sessionOrder = UtilsSession.GetCurrentOrder(HttpContext.Session);
            sessionOrder.OrderDetails.Last().NumberOfPizzas = numPizzas;

            

            if (numPizzas < 1)
            {
                TempData["error"] = "Your order must have at least 1 Pizza";
                
                return RedirectToAction("ChoosePizza",sessionOrder.OrderDetails.Last().PizzaId);
            }

            if (sessionOrder.GetOrderTotal() > 250)
            {
                TempData["error"] = "Your order cannot be cost greater than $250";
                return RedirectToAction("ChoosePizza", sessionOrder.OrderDetails.Last().PizzaId);
            }

            UtilsSession.SaveOrder(HttpContext.Session, sessionOrder);

            var json = JsonConvert.SerializeObject(sessionOrder);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync(postUrl, data);
            var result = response.Content.ReadAsStringAsync().Result;

            UtilsSession.Clear(HttpContext.Session);
            
            return Redirect("Index");
        }

        public ActionResult SelectCustomPizza(int id)
        {
            Customer customer = null;
            IList<Pizza> pizzas = null;
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
            if (sessionOrder.CustomerId == 0)
            {
                sessionOrder.CustomerId = id;
            }
            
            UtilsSession.SaveOrder(HttpContext.Session, sessionOrder);

            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            

            Pizza pizza = UtilsSession.GetCustomPizza(HttpContext.Session);
            pizza.PizzaName = "custom";

            UtilsSession.SavePizza(HttpContext.Session, pizza);
            

            if (pizza.Sauce != null)
            {
                ViewBag.SauceName = pizza.Sauce.Name;
                ViewBag.SaucePrice = pizza.Sauce.Price;
            }

            pizzas = new List<Pizza>();
            pizzas.Add(pizza);

            return View(pizzas);
        }

        public ActionResult AddCustomCrust()
        {
            IEnumerable<Crust> crusts = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(crustUrl);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Crust>>();
                    readTask.Wait();

                    crusts = readTask.Result;
                }
                else
                {
                    crusts = Enumerable.Empty<Crust>();
                    ModelState.AddModelError(string.Empty, "Server error, Get crusts is empty");
                }
            }
            return View(crusts);

        }

        public ActionResult AddCrustToPizza(int id)
        {
            Crust crust = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(crustUrl + id);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Crust>();
                    readTask.Wait();

                    crust = readTask.Result;
                    System.Console.WriteLine("Pizza Retrieved!");
                }
                else
                {
                    crust = null;
                    System.Console.WriteLine("Pizza Not Retrieved!");
                    ModelState.AddModelError(string.Empty, "Server error, Get crust is empty");
                }

            }

            var pizza = UtilsSession.GetCustomPizza(HttpContext.Session);
            pizza.Crust = crust;
            pizza.Crust.Name = crust.Name;
            pizza.CrustId = crust.Id;

            UtilsSession.SavePizza(HttpContext.Session, pizza);

            var customer = UtilsSession.GetCurrentOrder(HttpContext.Session);

            return RedirectToAction("SelectCustomPizza", customer.CustomerId);
        }

        public ActionResult AddCustomSauce()
        {
            IEnumerable<Sauce> sauces = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(sauceUrl);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Sauce>>();
                    readTask.Wait();

                    sauces = readTask.Result;
                }
                else
                {
                    sauces = Enumerable.Empty<Sauce>();
                    ModelState.AddModelError(string.Empty, "Server error, Get crusts is empty");
                }
            }
            return View(sauces);

        }

        public ActionResult AddSauceToPizza(int id)
        {
            Sauce sauce = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(sauceUrl + id);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Sauce>();
                    readTask.Wait();

                    sauce = readTask.Result;
                    System.Console.WriteLine("Sauce Retrieved!");
                }
                else
                {
                    sauce = null;
                    System.Console.WriteLine("Sauce Not Retrieved!");
                    ModelState.AddModelError(string.Empty, "Server error, Get crust is empty");
                }

            }

            var pizza = UtilsSession.GetCustomPizza(HttpContext.Session);
            pizza.Sauce = sauce;
            pizza.SauceId = sauce.Id;

            UtilsSession.SavePizza(HttpContext.Session, pizza);

            var cid = UtilsSession.GetCurrentOrder(HttpContext.Session);

            return RedirectToAction("SelectCustomPizza", cid.CustomerId);
        }

        public ActionResult AddCustomTopping()
        {
            IEnumerable<Topping> toppings = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(toppingUrl);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Topping>>();
                    
                    readTask.Wait();

                    toppings = readTask.Result;
                }
                else
                {
                    toppings = Enumerable.Empty<Topping>();
                    ModelState.AddModelError(string.Empty, "Server error, Get toppings is empty");
                }
            }
            return View(toppings);

        }

        public ActionResult AddToppingToPizza(int id)
        {
            var cid = UtilsSession.GetCurrentOrder(HttpContext.Session).CustomerId;
            if (UtilsSession.GetCustomPizza(HttpContext.Session).Topping.Count == 5)
            {
                TempData["ErrorMessage"] = " You cannot add more than 5 toppings";
                return RedirectToAction("SelectCustomPizza", cid);
            }

            Topping topping = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(toppingUrl + id);
                var response = client.GetAsync("");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Topping>();
                    readTask.Wait();

                    topping = readTask.Result;

                }
                else
                {
                    topping = null;
                    ModelState.AddModelError(string.Empty, "Server error, Get crust is empty");
                }
            }
            var pizza = UtilsSession.GetCustomPizza(HttpContext.Session);
            pizza.Topping.Add(topping);
            UtilsSession.SavePizza(HttpContext.Session, pizza);

            

            return RedirectToAction("SelectCustomPizza", cid);

        }


        public async Task<ActionResult> SubmitCustomOrder()
        {
            var cid = UtilsSession.GetCurrentOrder(HttpContext.Session).CustomerId;
            if (UtilsSession.GetCustomPizza(HttpContext.Session).Topping.Count < 2)
            {
                TempData["ErrorMessage"] = " You Must at least 2 toppings";
                return RedirectToAction("SelectCustomPizza", cid);
            }
            if (UtilsSession.GetCustomPizza(HttpContext.Session).SauceId == null)
            {
                TempData["ErrorMessage"] = " You Must select a sauce";
                return RedirectToAction("SelectCustomPizza", cid);
            }
            if (UtilsSession.GetCustomPizza(HttpContext.Session).CrustId == null)
            {
                TempData["ErrorMessage"] = " You Must select a crust";
                return RedirectToAction("SelectCustomPizza", cid);
            }


            Pizza pizza = UtilsSession.GetCustomPizza(HttpContext.Session);

            int newPizzaId;

            var json = JsonConvert.SerializeObject(pizza);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var pizzaclient = new HttpClient())
            {
                var response = await pizzaclient.PostAsync(PizzaUrl, data);
                var result = response.Content.ReadAsAsync<Pizza>();
                newPizzaId = result.Id;
            }

            Pizza custpizza = UtilsSession.GetCustomPizza(HttpContext.Session);
            custpizza.Id = newPizzaId;

            UtilsSession.SavePizza(HttpContext.Session, custpizza);

            return RedirectToAction("SelectNumCustomPizza", cid);
        }

        public ActionResult SelectNumCustomPizza(int id)
        {
            Pizza pizza = null;
            IEnumerable<Pizza> pizzas = null;
            ViewBag.PresetTotalError = TempData["error"];
            if (ViewBag.PresetTotalError == null)
            {
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
                        System.Console.WriteLine("Pizza Retrieved!");
                    }
                    else
                    {
                        pizzas = null;
                        System.Console.WriteLine("Pizza Not Retrieved!");
                        ModelState.AddModelError(string.Empty, "Server error, Get pizza is empty");
                    }
                }




                detail.Pizza = pizzas.Last();
                detail.PizzaId = pizzas.Last().Id;

                detail.OrderNumberNavigation = sessionOrder;

                if (sessionOrder.OrderDetails == null)
                {
                    sessionOrder.OrderDetails = new List<OrderDetail>();
                }

                sessionOrder.OrderDetails.Add(detail);
                UtilsSession.SaveOrder(HttpContext.Session, sessionOrder);
            }


            return View();

        }

        [HttpPost]
        public async Task<ActionResult> PostCustomOrder(int numPizzas)
        {
            var sessionOrder = UtilsSession.GetCurrentOrder(HttpContext.Session);


            sessionOrder.OrderDetails.Last().NumberOfPizzas = numPizzas;
            if (numPizzas < 1)
            {
                TempData["error"] = "your order must have at least 1 pizza";
                return RedirectToAction("ChoosePizza", sessionOrder.OrderDetails.Last().PizzaId);
            }
            if (sessionOrder.OrderDetails.First().getPrice() > 250)
            {
                TempData["error"] = "your order must have less than 250";
                return RedirectToAction("ChoosePizza", sessionOrder.OrderDetails.Last().PizzaId);
            }


            UtilsSession.SaveOrder(HttpContext.Session, sessionOrder);

            var json = JsonConvert.SerializeObject(sessionOrder);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync(postUrl, data);
            var result = response.Content.ReadAsStringAsync().Result;

            UtilsSession.Clear(HttpContext.Session);

            return Redirect("Index");

        }


    }

    
}
