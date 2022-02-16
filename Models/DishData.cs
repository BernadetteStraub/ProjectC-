using System.Collections.Generic;

namespace Straub_Bernadette_Lab8.Models
{
    public class DishData
    {
        public IEnumerable<Dish> Dishes { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public IEnumerable<DishIngredient> DishIngredients { get; set; }
    }
}
