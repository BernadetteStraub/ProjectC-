using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Straub_Bernadette_Lab8.Data;
using Straub_Bernadette_Lab8.Models;

namespace Straub_Bernadette_Lab8.Pages.Dishes
{
    public class EditModel : DishIngredientsPageModel
    {
        private readonly Straub_Bernadette_Lab8Context _context;

        public EditModel(Straub_Bernadette_Lab8Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Dish Dish { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dish = await _context.Dish
             .Include(b => b.Category)
             .Include(b => b.DishIngredients).ThenInclude(b => b.Ingredient)
             .AsNoTracking()
             .FirstOrDefaultAsync(m => m.ID == id);

            if (Dish == null)
            {
                return NotFound();
            }
            PopulateAssignedIngredientData(_context, Dish);

            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "ID", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedIngredients)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dishToUpdate = await _context.Dish
            .Include(i => i.Category)
            .Include(i => i.DishIngredients)
            .ThenInclude(i => i.Ingredient)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (dishToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync(
                dishToUpdate, 
                "Dish",
                i => i.Name,i => i.Price, i => i.Category))
            {
                UpdateDishIngredients(_context, selectedIngredients, dishToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            
            UpdateDishIngredients(_context, selectedIngredients, dishToUpdate);
            PopulateAssignedIngredientData(_context, dishToUpdate);
            return Page();
        }
    }
}
