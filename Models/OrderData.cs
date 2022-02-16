using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Straub_Bernadette_Lab8.Models
{
    public class OrderData
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Dish> Dishes { get; set; }
        public IEnumerable<OrderDish> OrderDishes { get; set; }
    }
}
