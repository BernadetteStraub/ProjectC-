using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Straub_Bernadette_Lab8.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        public ICollection<Dish> Dishes { get; set; }
    }
}
