using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//validations
using System.ComponentModel.DataAnnotations.Schema;// constaints and display

namespace PizzaBox.Storing.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Store Store { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
