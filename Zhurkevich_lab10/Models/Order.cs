using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhurkevich_lab10.Classes;
using Zhurkevich_lab10.Interfaces;

namespace Zhurkevich_lab10.Models
{
    internal class Order : ICreatable
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public Waiter Waiter { get; set; }
        public List<DishList> DishList { get; set; } = new List<DishList>();
        public decimal TotalCost { get; set; }
        public static List<Order> Orders { get; set; } = new List<Order>();
        public static Order NewOrder { get; set; } = null;

        public Order(int id, DateOnly date, TimeOnly time, Waiter waiter, List<DishList> dishList, decimal totalCost)
        {
            Id = id;
            Date = date;
            Time = time;
            Waiter = waiter;
            DishList = dishList;
            TotalCost = totalCost;
        }

        public Order()
        {
            if (Orders.Count == 0)
            {
                Id = 0;
            }
            else
            {
                Id = Orders.Max(o => o.Id) + 1;
            }
        }

        public void Create()
        {
            Orders.Add(this);
            MyJsonConverter.Serialize();
        }
    }
}
