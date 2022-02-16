using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Straub_Bernadette_Lab8.Data;
using Straub_Bernadette_Lab8.Models;

namespace Straub_Bernadette_Lab8.Pages.Ingredients
{
    public class DeleteModel : PageModel
    {
        private readonly Straub_Bernadette_Lab8.Data.Straub_Bernadette_Lab8Context _context;

        public DeleteModel(Straub_Bernadette_Lab8.Data.Straub_Bernadette_Lab8Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Ingredient Ingredient { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ingredient = await _context.Ingredient.FirstOrDefaultAsync(m => m.ID == id);

            if (Ingredient == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ingredient = await _context.Ingredient.FindAsync(id);

            if (Ingredient != null)
            {
                _context.Ingredient.Remove(Ingredient);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
