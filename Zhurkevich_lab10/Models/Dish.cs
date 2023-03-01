using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhurkevich_lab10.Classes;
using Zhurkevich_lab10.Interfaces;

namespace Zhurkevich_lab10.Models
{
    internal class Dish : ICreatable, IUpdatable, IDeletable
    {
        public enum DishCategory
        {
            Breakfasts,
            Snacks,
            Salads,
            Soups,
            HotDishes,
            SideDishes,
            Desserts,
            Drinks
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DishCategory Category { get; set; }
        public int Weight { get; set; }
        public decimal Cost { get; set; }
        public static List<Dish> Dishes { get; set; } = new List<Dish>();
        public static List<Dish> SearchedDishes { get; set; } = new List<Dish>();

        public Dish(int id, string name, DishCategory category, int weight, decimal cost)
        {
            Id = id;
            Name = name;
            Category = category;
            Weight = weight;
            Cost = cost;
        }

        public void Create()
        {
            Dishes.Add(this);
            MyJsonConverter.Serialize();
        }

        public void Update()
        {
            MyJsonConverter.Serialize();
        }

        public void Delete()
        {
            Dishes.Remove(this);
            MyJsonConverter.Serialize();
        }
    }
}
