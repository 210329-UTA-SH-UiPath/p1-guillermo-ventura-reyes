using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//validations
using System.ComponentModel.DataAnnotations.Schema;// constaints and display

namespace PizzaBoxClient.Models
{
    public class Pizza
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PizzaName { get; set; }
        public int? CrustId { get; set; }
        public int? SauceId { get; set; }

        public virtual Crust Crust { get; set; }
        public virtual Sauce Sauce { get; set; }
        public virtual ICollection<Topping> Topping { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public decimal getPrice()
        {
            decimal tempPrice = 0.00M;
            tempPrice += Crust?.Price ?? 0;
            tempPrice += Sauce?.Price ?? 0;
            foreach (var topping in Topping)
            {
                tempPrice += topping.Price ?? 0;
            }
            return tempPrice;
        }
    }
}
