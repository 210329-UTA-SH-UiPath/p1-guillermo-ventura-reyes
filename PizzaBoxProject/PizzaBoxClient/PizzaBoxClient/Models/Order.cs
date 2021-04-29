using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//validations
using System.ComponentModel.DataAnnotations.Schema;// constaints and display

namespace PizzaBoxClient.Models
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

        public Decimal GetOrderTotal()
        {
            Decimal OrderTotal = 0.00M;
            foreach (var detail in OrderDetails)
            {
                if (detail != null)
                {
                    OrderTotal += detail.getPrice();
                }
            }

            return decimal.Round(OrderTotal, 2, MidpointRounding.AwayFromZero);
        }
    }
}
