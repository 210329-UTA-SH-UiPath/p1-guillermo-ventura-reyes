using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//validations
using System.ComponentModel.DataAnnotations.Schema;// constaints and display

namespace PizzaBox.Storing.Entities
{
    public class Customer
    {
        public Customer(int id, string name, int phoneNumber)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Name { get; set; }
        [Required]
        public int PhoneNumber { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
