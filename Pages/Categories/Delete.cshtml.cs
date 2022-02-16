using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Straub_Bernadette_Lab8.Data;
using Straub_Bernadette_Lab8.Models;

namespace Straub_Bernadette_Lab8.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly Straub_Bernadette_Lab8Context _context;

        public DeleteModel(Straub_Bernadette_Lab8Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FirstOrDefaultAsync(m => m.ID == id);

            if (Category == null)
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

            var dishes = await _context
                .Dish
                .Where(b => b.CategoryID == id)
                .ToListAsync();

            if (dishes.Count != 0)
            {
                ErrorMessage = "Unable to delete Category as it is used in one or more dishes";
                return Page();
            }

            Category = await _context.Category.FindAsync(id);

            if (Category != null)
            {
                _context.Category.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
