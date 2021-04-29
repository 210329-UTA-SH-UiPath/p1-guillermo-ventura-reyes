using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//validations
using System.ComponentModel.DataAnnotations.Schema;// constaints and display

namespace PizzaBoxClient.Models
{
    public class OrderDetail
    {
        
        public int OrderId { get; set; }
        public int? NumberOfPizzas { get; set; }
        
        public int PizzaId { get; set; }
        public virtual Order OrderNumberNavigation { get; set; }
        public virtual Pizza Pizza { get; set; }

        public decimal getPrice()
        {
            decimal total = 0;

            total = Pizza.getPrice() * NumberOfPizzas ?? 0;

            return total;
        }
    }
}
