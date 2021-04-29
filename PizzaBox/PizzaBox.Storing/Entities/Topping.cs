using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//validations
using System.ComponentModel.DataAnnotations.Schema;// constaints and display

namespace PizzaBox.Storing.Entities
{
    public class Topping
    {
        public Topping()
        {

        }
        public Topping(string name, decimal? price)
        {
            Name = name;
            Price = price;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
