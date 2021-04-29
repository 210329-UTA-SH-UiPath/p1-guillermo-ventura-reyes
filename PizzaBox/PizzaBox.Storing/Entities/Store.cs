using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//validations
using System.ComponentModel.DataAnnotations.Schema;// constaints and display

namespace PizzaBox.Storing.Entities
{
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
