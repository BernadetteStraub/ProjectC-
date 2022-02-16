using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Straub_Bernadette_Lab8.Data;
using Straub_Bernadette_Lab8.Models;
using System.Collections.Generic;

namespace Straub_Bernadette_Lab8.Pages.Dishes
{
    public class CreateModel : DishIngredientsPageModel
    {
        private readonly Straub_Bernadette_Lab8Context _context;

        public CreateModel(Straub_Bernadette_Lab8Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "ID", "CategoryName");

            var dish = new Dish();
            dish.DishIngredients = new List<DishIngredient>();
            PopulateAssignedIngredientData(_context, dish);

            return Page();
        }

        [BindProperty]
        public Dish Dish { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedIngredients)
        {
            var newDish = new Dish();
            if (selectedIngredients != null)
            {
                newDish.DishIngredients = new List<DishIngredient>();
                foreach (var cat in selectedIngredients)
                {
                    var catToAdd = new DishIngredient
                    {
                        IngredientID = int.Parse(cat)
                    };
                    newDish.DishIngredients.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync(
                newDish,
                "Dish",
                i => i.Name,
                i => i.Price, i => i.CategoryID))
            {
                _context.Dish.Add(newDish);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedIngredientData(_context, newDish);
            return Page();
        }
    }
}
