using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Straub_Bernadette_Lab8.Models
{
    public class OrderDish
    {
        public int ID { get; set; }
        public int DishID { get; set; }
        public Dish Dish { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
    }
}
