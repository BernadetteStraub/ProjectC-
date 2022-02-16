using Straub_Bernadette_Lab8.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Straub_Bernadette_Lab8.Models
{
    public class DishIngredientsPageModel : PageModel
    {
        public List<AssignedIngredientData> AssignedIngredientDataList;
        public void PopulateAssignedIngredientData(Straub_Bernadette_Lab8Context context,
        Dish dish)
        {
            var allIngredients = context.Ingredient;
            var dishIngredients = new HashSet<int>(
            dish.DishIngredients.Select(c => c.IngredientID)); 
            AssignedIngredientDataList = new List<AssignedIngredientData>();
            foreach (var cat in allIngredients)
            {
                AssignedIngredientDataList.Add(new AssignedIngredientData
                {
                    IngredientID = cat.ID,
                    Name = cat.IngredientName,
                    Assigned = dishIngredients.Contains(cat.ID)
                });
            }
        }
        public void UpdateDishIngredients(Straub_Bernadette_Lab8Context context,
        string[] selectedIngredients, Dish dishToUpdate)
        {
            if (selectedIngredients == null)
            {
                dishToUpdate.DishIngredients = new List<DishIngredient>();
                return;
            }
            var selectedIngredientsHS = new HashSet<string>(selectedIngredients);
            var dishIngredients = new HashSet<int>
            (dishToUpdate.DishIngredients.Select(c => c.Ingredient.ID));
            foreach (var cat in context.Ingredient)
            {
                if (selectedIngredientsHS.Contains(cat.ID.ToString()))
                {
                    if (!dishIngredients.Contains(cat.ID))
                    {
                        dishToUpdate.DishIngredients.Add(
                        new DishIngredient
                        {
                            DishID = dishToUpdate.ID,
                            IngredientID = cat.ID
                        });
                    }
                }
                else
                {
                    if (dishIngredients.Contains(cat.ID))
                    {
                        DishIngredient courseToRemove
                        = dishToUpdate
                        .DishIngredients
                        .SingleOrDefault(i => i.IngredientID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
