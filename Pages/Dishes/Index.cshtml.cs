using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Straub_Bernadette_Lab8.Data;
using Straub_Bernadette_Lab8.Models;
using System.Linq;

namespace Straub_Bernadette_Lab8.Pages.Dishes
{
    public class IndexModel : PageModel
    {
        private readonly Straub_Bernadette_Lab8Context _context;

        public IndexModel(Straub_Bernadette_Lab8Context context)
        {
            _context = context;
        }

        public IList<Dish> Dish { get;set; }
        public DishData DishD { get; set; }
        public int DishID { get; set; }
        public int IngredientID { get; set; }
        public async Task OnGetAsync(int? id, int? ingredientID)
        {
            DishD = new DishData();

            DishD.Dishes = await _context.Dish
                .Include(b => b.Category)
                .Include(b => b.DishIngredients)
                .ThenInclude(b => b.Ingredient)
                .AsNoTracking()
                .OrderBy(b => b.Name)
                .ToListAsync();

            if (id != null)
            {
                DishID = id.Value;
                Dish dish = DishD.Dishes
                .Where(i => i.ID == id.Value).Single();
                DishD.Ingredients = dish.DishIngredients.Select(s => s.Ingredient);
            }
        }
    }
}
