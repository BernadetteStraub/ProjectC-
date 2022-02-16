using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Straub_Bernadette_Lab8.Models;

namespace Straub_Bernadette_Lab8.Pages.Orders
{
    public class CreateModel : OrderDishPageModel
    {
        private readonly Straub_Bernadette_Lab8.Data.Straub_Bernadette_Lab8Context _context;
        public DishData DishD { get; set; }


        public CreateModel(Straub_Bernadette_Lab8.Data.Straub_Bernadette_Lab8Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //ViewData["DishID"] = new SelectList(_context.Set<Dish>(), "ID", "Name", "Price");

            var order = new Order();
            order.OrderDishes = new List<OrderDish>();
            PopulateAssignedDishData(_context, order);

            return Page();
        }

        [BindProperty]
        public Order Order { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedDishes)
        {

            DishD = new DishData();

            DishD.Dishes = await _context.Dish
                .Include(b => b.Category)
                .Include(b => b.DishIngredients)
                .ThenInclude(b => b.Ingredient)
                .AsNoTracking()
                .OrderBy(b => b.Name)
                .ToListAsync();

            var newOrder = new Order();
            var total = 0.0m;
            if (selectedDishes != null)
            {
                newOrder.OrderDishes = new List<OrderDish>();
                foreach (var cat in selectedDishes)
                {
                    var price = DishD.Dishes.First(dish => dish.ID == int.Parse(cat)).Price;
                    var catToAdd = new OrderDish
                    {
                        DishID = int.Parse(cat)
                    };
                    newOrder.OrderDishes.Add(catToAdd);
                    total = total + price;
                }
            }
            newOrder.Total = total;
            newOrder.OrderDate = DateTime.Today;
            if (await TryUpdateModelAsync(
                newOrder,
                "Order",
                i => i.Server))
            {
                _context.Order.Add(newOrder);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedDishData(_context, newOrder);
            return Page();
        }
    }
}
