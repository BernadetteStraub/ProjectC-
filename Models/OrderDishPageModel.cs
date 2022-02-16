using Straub_Bernadette_Lab8.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Straub_Bernadette_Lab8.Models
{
    public class OrderDishPageModel: PageModel
    {
         
        public List<AssignedDishData> AssignedDishDataList;
        public void PopulateAssignedDishData(Straub_Bernadette_Lab8Context context,
        Order order)
        {
            var allDishs = context.Dish;
            var orderDishes = new HashSet<int>(
            order.OrderDishes.Select(c => c.DishID));
            AssignedDishDataList = new List<AssignedDishData>();
            foreach (var cat in allDishs)
            {
                AssignedDishDataList.Add(new AssignedDishData
                {
                    DishID = cat.ID,
                    Name = cat.Name,
                    Price = cat.Price,
                    Assigned = orderDishes.Contains(cat.ID)
                }); ;
            }
        }
        public void UpdateOderDishes(Straub_Bernadette_Lab8Context context,
        string[] selectedDishes, Order orderToUpdate)
        {
            if (selectedDishes == null)
            {
                orderToUpdate.OrderDishes = new List<OrderDish>();
                return;
            }
            var selectedDishHS = new HashSet<string>(selectedDishes);
            var orderDishes = new HashSet<int>
            (orderToUpdate.OrderDishes.Select(c => c.Dish.ID));
            foreach (var cat in context.Dish)
            {
                if (selectedDishHS.Contains(cat.ID.ToString()))
                {
                    if (!orderDishes.Contains(cat.ID))
                    {
                        orderToUpdate.OrderDishes.Add(
                        new OrderDish
                        {
                            OrderID = orderToUpdate.ID,
                            DishID = cat.ID
                        });
                    }
                }
                else
                {
                    if (orderDishes.Contains(cat.ID))
                    {
                        OrderDish courseToRemove
                        = orderToUpdate
                        .OrderDishes
                        .SingleOrDefault(i => i.DishID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
