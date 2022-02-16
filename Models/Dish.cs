using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Straub_Bernadette_Lab8.Models
{
    public class Dish
    {
        public int ID { get; set; }

        [Required, StringLength(150, MinimumLength = 3)]
        [Display(Name = "Dish Name")]
        public string Name { get; set; }
        [Range(1, 300)]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        [Display(Name = "Ingredients")]
        public ICollection<DishIngredient> DishIngredients { get; set; }
        public ICollection<OrderDish> OrderDishes { get; set; }

    }
}
