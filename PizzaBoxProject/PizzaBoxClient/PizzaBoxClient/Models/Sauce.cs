using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//validations
using System.ComponentModel.DataAnnotations.Schema;// constaints and display

namespace PizzaBoxClient.Models
{
    public class Sauce
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal? Price { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
