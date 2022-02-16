using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Straub_Bernadette_Lab8.Models
{
    public class Ingredient
    {
        public int ID { get; set; }
        [Display(Name = "Ingredient")]
        public string IngredientName { get; set; }
        [Display(Name = "Ingredients")]
        public ICollection<DishIngredient> DishIngredients { get; set; }
    }
}
