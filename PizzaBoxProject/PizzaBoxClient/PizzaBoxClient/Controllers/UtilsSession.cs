using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PizzaBoxClient.Controllers
{
    public class UtilsSession
    {
        private static void SetObjectAsJson(ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            
        }

        private static T GetObjectFromJson<T>(ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static Models.Order GetCurrentOrder(ISession session)
        {
            var order = GetObjectFromJson<Models.Order>(session, "order");
            if (order is null)
            {
                order = new Models.Order();
            }
            
            return order;
        }

        public static Models.Pizza GetCustomPizza(ISession session)
        {
            var pizza = GetObjectFromJson<Models.Pizza>(session, "pizza");
            if (pizza is null)
            {
                pizza = new Models.Pizza();
            }
            return pizza;
        }

        public static void SavePizza(ISession session, Models.Pizza pizza)
        {
            SetObjectAsJson(session, "pizza", pizza);
        }

        public static void ClearPizza(ISession session)
        {
            SetObjectAsJson(session, "pizza", new Models.Pizza());
        }

        public static void SaveOrder(ISession session, Models.Order order)
        {
            SetObjectAsJson(session, "order", order);
        }

        public static void Clear(ISession session)
        {
            session.Clear();
        }
    }
}
