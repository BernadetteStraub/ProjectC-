using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Straub_Bernadette_Lab8.Data;
using Straub_Bernadette_Lab8.Models;

namespace Straub_Bernadette_Lab8.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly Straub_Bernadette_Lab8.Data.Straub_Bernadette_Lab8Context _context;

        public IndexModel(Straub_Bernadette_Lab8.Data.Straub_Bernadette_Lab8Context context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }
        public OrderData OrderD { get; set; }
        public int OrderID { get; set; }
        public int DishID { get; set; }

        public async Task OnGetAsync(int? id, int? ingredientID)
        {
            OrderD = new OrderData();

            OrderD.Orders = await _context.Order
                .Include(b => b.OrderDishes)
                .ThenInclude(b => b.Dish)
                .AsNoTracking()
                .OrderBy(b => b.Server)
                .ToListAsync();

            if (id != null)
            {
                OrderID = id.Value;
                Order order = OrderD.Orders
                .Where(i => i.ID == id.Value).Single();
                OrderD.Dishes = order.OrderDishes.Select(s => s.Dish);
            }
        }
    }
}
