using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Straub_Bernadette_Lab8.Models
{
    public class Order
    {
        public int ID { get; set; }
        [Display(Name = "Served by")]
        public string Server { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Total { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of order")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Dishes")]
        public ICollection<OrderDish> OrderDishes { get; set; }
    }
}
