using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Straub_Bernadette_Lab8.Data;
using Straub_Bernadette_Lab8.Models;

namespace Straub_Bernadette_Lab8.Pages.Orders
{
    public class EditModel : OrderDishPageModel
    {
        private readonly Straub_Bernadette_Lab8.Data.Straub_Bernadette_Lab8Context _context;
        public DishData DishD { get; set; }

        public EditModel(Straub_Bernadette_Lab8.Data.Straub_Bernadette_Lab8Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order
             .Include(b => b.OrderDishes)
             .ThenInclude(b => b.Dish)
             .AsNoTracking()
             .FirstOrDefaultAsync(m => m.ID == id);

            if (Order == null)
            {
                return NotFound();
            }
            PopulateAssignedDishData(_context, Order);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedDishes)
        {
            if (id == null)
            {
                return NotFound();
            }

            DishD = new DishData();

            DishD.Dishes = await _context.Dish
                .Include(b => b.Category)
                .Include(b => b.DishIngredients)
                .ThenInclude(b => b.Ingredient)
                .AsNoTracking()
                .OrderBy(b => b.Name)
                .ToListAsync();

            var orderToUpdate = await _context.Order
            .Include(i => i.OrderDishes)
            .ThenInclude(i => i.Dish)
            .FirstOrDefaultAsync(s => s.ID == id);

            if (orderToUpdate == null)
            {
                return NotFound();
            }

            var newTotal = 0.0m;

            if (selectedDishes != null)
            {
                foreach (var cat in selectedDishes)
                {
                    var price = DishD.Dishes.First(dish => dish.ID == int.Parse(cat)).Price;
                    newTotal = newTotal + price;
                }
            }
            orderToUpdate.Total = newTotal;

            if (await TryUpdateModelAsync(
                orderToUpdate,
                "Order",
                i => i.Server, i => i.OrderDate))
            {
                UpdateOderDishes(_context, selectedDishes, orderToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateOderDishes(_context, selectedDishes, orderToUpdate);
            PopulateAssignedDishData(_context, orderToUpdate);
            return Page();
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.ID == id);
        }
    }
}
