using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhurkevich_lab10.Classes;
using Zhurkevich_lab10.Controls;
using Zhurkevich_lab10.Interfaces;

namespace Zhurkevich_lab10.Models
{
    internal class Waiter : Employee, ICreatable, IReadable, IUpdatable, IDeletable
    {
        public Waiter(int id, string login, string password, string secondName, string firstName, string patronymic, DateTime birthdate, decimal salary) : base(id, login, password, secondName, firstName, patronymic, birthdate, Role.Waiter, salary)
        {
        }

        public void Create()
        {
            Employees.Add(this);
            MyJsonConverter.Serialize();
        }

        public void Update()
        {
            MyJsonConverter.Serialize();
        }

        public void Delete()
        {
            Employees.Remove(this);
            MyJsonConverter.Serialize();
        }

        public void Read()
        {
            Console.Clear();
            ArrowsMenu.Current = 0;
            PrintHeader();
            Console.SetCursorPosition(95, Console.CursorTop);
            Console.Write("F1 - Добавить запись");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Escape - Выйти из системы");
            Console.SetCursorPosition(2, 2);
            Console.Write("ID");
            Console.SetCursorPosition(10, Console.CursorTop);
            Console.Write("Дата");
            Console.SetCursorPosition(25, Console.CursorTop);
            Console.Write("Время");
            Console.SetCursorPosition(35, Console.CursorTop);
            Console.Write("Официант");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.Write("Общая стоимость");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            ArrowsMenu.Items.Clear();
            Console.SetCursorPosition(15, 3);
            foreach (Order order in Order.Orders)
            {
                Console.SetCursorPosition(2, Console.CursorTop);
                Console.Write(order.Id);
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.Write(order.Date);
                Console.SetCursorPosition(25, Console.CursorTop);
                Console.Write(order.Time);
                Console.SetCursorPosition(35, Console.CursorTop);
                Console.Write($"{order.Waiter.SecondName} {order.Waiter.FirstName}");
                Console.SetCursorPosition(70, Console.CursorTop);
                Console.Write(order.TotalCost);
                ArrowsMenu.Items.Add(new GridRowId(order.Id));
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
        }

        private void PrintHeader()
        {
            Console.Clear();
            Console.Write($"  Добро пожаловать, {Login}!");
            Console.SetCursorPosition(54, Console.CursorTop);
            Console.WriteLine("Роль: Официант");
            for (int i = 0; i < 120; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        public void PrintMainWindow()
        {
            Read();
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            if (Order.Orders.Count > 0)
            {
                Console.Write("->");
            }
        }

        public void PrintAddWindow()
        {
            Order.NewOrder = new Order() { Waiter = Authorization.SignedEmployee as Waiter };
            foreach (Dish dish in Dish.Dishes)
            {
                Order.NewOrder.DishList.Add(new DishList() { Dish = dish });
            }
            Console.Clear();
            ArrowsMenu.Current = 0;
            PrintHeader();
            Console.SetCursorPosition(95, Console.CursorTop);
            Console.Write("S - Сформировать заказ");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("+ - Добавить блюдо");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("- - Убрать блюдо");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Escape - Отменить заказ");
            Console.SetCursorPosition(0, 3);
            Console.SetCursorPosition(2, 2);
            Console.Write("ID");
            Console.SetCursorPosition(10, Console.CursorTop);
            Console.Write("Название");
            Console.SetCursorPosition(45, Console.CursorTop);
            Console.Write("Категория");
            Console.SetCursorPosition(65, Console.CursorTop);
            Console.Write("Цена за ед.");
            Console.SetCursorPosition(80, Console.CursorTop);
            Console.Write("Кол-во");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            ArrowsMenu.Items.Clear();
            Console.SetCursorPosition(15, 3);
            foreach (DishList orderList in Order.NewOrder.DishList)
            {
                Console.SetCursorPosition(2, Console.CursorTop);
                Console.Write(orderList.Dish.Id);
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.Write(orderList.Dish.Name);
                Console.SetCursorPosition(45, Console.CursorTop);
                switch (orderList.Dish.Category)
                {
                    case Dish.DishCategory.Breakfasts:
                        Console.Write("Завтраки");
                        break;
                    case Dish.DishCategory.Snacks:
                        Console.Write("Закуски");
                        break;
                    case Dish.DishCategory.Salads:
                        Console.Write("Салаты");
                        break;
                    case Dish.DishCategory.Soups:
                        Console.Write("Супы");
                        break;
                    case Dish.DishCategory.HotDishes:
                        Console.Write("Горячие блюда");
                        break;
                    case Dish.DishCategory.SideDishes:
                        Console.Write("Гарниры");
                        break;
                    case Dish.DishCategory.Desserts:
                        Console.Write("Десерты");
                        break;
                    case Dish.DishCategory.Drinks:
                        Console.Write("Напитки");
                        break;
                }
                Console.SetCursorPosition(65, Console.CursorTop);
                Console.Write(orderList.Dish.Cost);
                Console.SetCursorPosition(80, Console.CursorTop);
                Console.Write(orderList.Count);
                ArrowsMenu.Items.Add(new GridRowId(orderList.Dish.Id));
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(92, 2);
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(0, Order.NewOrder.DishList.Count + 3);
            for (int i = 0; i < 92; i++)
            {
                Console.Write("-");
            }
            Console.Write("|");
            Console.SetCursorPosition(70, Console.CursorTop + 1);
            Console.Write($"Итог: {Order.NewOrder.DishList.Sum(o => o.TotalCost)}");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.Write("|");
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            if (Order.NewOrder.DishList.Count > 0)
            {
                Console.Write("->");
            }
        }

        public void UpdateAddWindow(int cursorTop)
        {
            Console.SetCursorPosition(15, 3);
            foreach (DishList orderList in Order.NewOrder.DishList)
            {          
                Console.SetCursorPosition(80, Console.CursorTop);
                Console.Write("  ");
                Console.SetCursorPosition(80, Console.CursorTop);
                Console.WriteLine(orderList.Count);
            }
            Console.SetCursorPosition(0, Order.NewOrder.DishList.Count + 3);
            for (int i = 0; i < 92; i++)
            {
                Console.Write("-");
            }
            Console.SetCursorPosition(70, Console.CursorTop + 1);
            Console.Write("              ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Order.NewOrder.TotalCost = Order.NewOrder.DishList.Sum(o => o.TotalCost);
            Console.Write($"Итог: {Order.NewOrder.TotalCost}");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.Write("|");
            Console.SetCursorPosition(2, cursorTop);
        }
    }
}
