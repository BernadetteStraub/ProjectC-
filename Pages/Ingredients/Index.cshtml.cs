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
    public class IndexModel : PageModel
    {
        private readonly Straub_Bernadette_Lab8.Data.Straub_Bernadette_Lab8Context _context;

        public IndexModel(Straub_Bernadette_Lab8.Data.Straub_Bernadette_Lab8Context context)
        {
            _context = context;
        }

        public IList<Ingredient> Ingredient { get;set; }

        public async Task OnGetAsync()
        {
            Ingredient = await _context.Ingredient.ToListAsync();
        }
    }
}
