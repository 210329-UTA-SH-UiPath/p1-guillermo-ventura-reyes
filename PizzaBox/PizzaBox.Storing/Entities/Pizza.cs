using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//validations
using System.ComponentModel.DataAnnotations.Schema;// constaints and display

namespace PizzaBox.Storing.Entities
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
    }
}
