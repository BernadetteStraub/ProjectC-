using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Straub_Bernadette_Lab8.Data;
using Straub_Bernadette_Lab8.Models;

namespace Straub_Bernadette_Lab8.Pages.Ingredients
{
    public class CreateModel : PageModel
    {
        private readonly Straub_Bernadette_Lab8.Data.Straub_Bernadette_Lab8Context _context;

        public CreateModel(Straub_Bernadette_Lab8.Data.Straub_Bernadette_Lab8Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Ingredient Ingredient { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Ingredient.Add(Ingredient);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
