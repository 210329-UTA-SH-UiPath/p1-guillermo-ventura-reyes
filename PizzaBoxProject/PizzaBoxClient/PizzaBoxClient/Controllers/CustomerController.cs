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
    public class CustomerController : Controller
    {
        string CustomerUrl = "https://localhost:44301/api/Customer/";
        public IActionResult Index()
        {
            IEnumerable<Customer> customers = null;

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
                    ModelState.AddModelError(string.Empty, "Server error, Get customers is empty");
                }
            }
            return View(customers);
        }

        public IActionResult details(int id)
        {
            Customer customer = null;
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
                    ModelState.AddModelError(string.Empty, "Server error, Get customer is empty");
                }

            }
            return View(customer.Order);

        }
    }
}
