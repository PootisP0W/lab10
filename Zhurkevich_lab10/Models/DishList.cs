using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhurkevich_lab10.Models
{
    internal class DishList
    {
        public Dish Dish { get; set; }
        public int Count { get; set; }
        public decimal TotalCost { get; set; }

        public DishList(Dish dish, int count, decimal totalCost)
        {
            Dish = dish;
            Count = count;
            TotalCost = totalCost;
        }

        public DishList()
        {

        }
    }
}
