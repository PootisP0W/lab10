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
    internal class Chef : Employee, ICreatable, IReadable, IUpdatable, IDeletable, ISearchable
    {
        public Chef(int id, string login, string password, string secondName, string firstName, string patronymic, DateTime birthdate, decimal salary) : base(id, login, password, secondName, firstName, patronymic, birthdate, Role.Chef, salary)
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
            Console.Write("F2 - Найти запись");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Escape - Выйти из системы");
            Console.SetCursorPosition(2, 2);
            Console.Write("ID");
            Console.SetCursorPosition(10, Console.CursorTop);
            Console.Write("Название");
            Console.SetCursorPosition(50, Console.CursorTop);
            Console.Write("Категория");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.Write("Вес");
            Console.SetCursorPosition(80, Console.CursorTop);
            Console.Write("Цена");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            ArrowsMenu.Items.Clear();
            Console.SetCursorPosition(15, 3);
            foreach (Dish dish in Dish.Dishes)
            {
                Console.SetCursorPosition(2, Console.CursorTop);
                Console.Write(dish.Id);
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.Write(dish.Name);
                Console.SetCursorPosition(50, Console.CursorTop);
                switch (dish.Category)
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
                Console.SetCursorPosition(70, Console.CursorTop);
                Console.Write(dish.Weight);
                Console.SetCursorPosition(80, Console.CursorTop);
                Console.Write(dish.Cost);
                ArrowsMenu.Items.Add(new GridRowId(dish.Id));
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
        }

        public void Search()
        {
            Console.Clear();
            ArrowsMenu.Current = 0;
            PrintHeader();
            Console.SetCursorPosition(95, Console.CursorTop);
            Console.Write("F1 - Добавить запись");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Escape - Отменить поиск");
            Console.SetCursorPosition(2, 2);
            Console.Write("ID");
            Console.SetCursorPosition(10, Console.CursorTop);
            Console.Write("Название");
            Console.SetCursorPosition(50, Console.CursorTop);
            Console.Write("Категория");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.Write("Вес");
            Console.SetCursorPosition(80, Console.CursorTop);
            Console.Write("Цена");
            Console.SetCursorPosition(92, Console.CursorTop );
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            ArrowsMenu.Items.Clear();
            Console.SetCursorPosition(15, 3);
            foreach (Dish dish in Dish.SearchedDishes)
            {
                Console.SetCursorPosition(2, Console.CursorTop);
                Console.Write(dish.Id);
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.Write(dish.Name);
                Console.SetCursorPosition(50, Console.CursorTop);
                switch (dish.Category)
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
                Console.SetCursorPosition(70, Console.CursorTop);
                Console.Write(dish.Weight);
                Console.SetCursorPosition(80, Console.CursorTop);
                Console.Write(dish.Cost);
                ArrowsMenu.Items.Add(new GridRowId(dish.Id));
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
        }

        private void PrintHeader()
        {
            Console.Clear();
            Console.Write($"  Добро пожаловать, {Login}!");
            Console.SetCursorPosition(54, Console.CursorTop);
            Console.WriteLine("Роль: Шеф");
            for (int i = 0; i < 120; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        public void PrintMainWindow()
        {
            Dish.SearchedDishes.Clear();
            Read();
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            if (Dish.Dishes.Count > 0)
            {
                Console.Write("->");
            }
        }

        public void PrintAddWindow()
        {
            Console.Clear();
            ArrowsMenu.Current = 0;
            PrintHeader();
            Console.SetCursorPosition(95, Console.CursorTop);
            Console.Write("S - Сохранить изменения");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Escape - Отменить добав.");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Категории блюд:");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("0 - Breakfasts");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("1 - Snacks");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("2 - Salads");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("3 - Soups");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("4 - HotDishes");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("5 - SideDishes");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("6 - Desserts");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("7 - Drinks");
            Console.SetCursorPosition(0, 3);
            ArrowsMenu.Items.Clear();
            ArrowsMenu.Items.Add(new TextBox("ID"));
            ArrowsMenu.Items.Add(new TextBox("Название"));
            ArrowsMenu.Items.Add(new TextBox("Категория"));
            ArrowsMenu.Items.Add(new TextBox("Вес"));
            ArrowsMenu.Items.Add(new TextBox("Цена"));
            Console.SetCursorPosition(92, 2);
            for (int i = 3; i < 14; i++)
            {
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            Console.Write("->");
        }

        public void PrintUpdateWindow(Dish dish)
        {
            Console.Clear();
            ArrowsMenu.Current = 0;
            PrintHeader();
            Console.SetCursorPosition(95, Console.CursorTop);
            Console.Write("S - Сохранить изменения");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Escape - Отменить измен.");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Del - Удалить запись");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Категории блюд:");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("0 - Breakfasts");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("1 - Snacks");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("2 - Salads");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("3 - Soups");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("4 - HotDishes");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("5 - SideDishes");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("6 - Desserts");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("7 - Drinks");
            Console.SetCursorPosition(0, 3);
            ArrowsMenu.Items.Clear();
            ArrowsMenu.Items.Add(new TextBox("ID", dish.Id.ToString()) { Enable = false});
            ArrowsMenu.Items.Add(new TextBox("Название", dish.Name) { Enable = false});
            ArrowsMenu.Items.Add(new TextBox("Категория", dish.Category.ToString()));
            ArrowsMenu.Items.Add(new TextBox("Вес", dish.Weight.ToString()));
            ArrowsMenu.Items.Add(new TextBox("Цена", dish.Cost.ToString()));
            Console.SetCursorPosition(92, 2);
            for (int i = 3; i < 15; i++)
            {
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            Console.Write("->");
        }

        public void PrintSearchWindow()
        {
            Console.Clear();
            ArrowsMenu.Current = 0;
            PrintHeader();
            Console.SetCursorPosition(95, Console.CursorTop);
            Console.Write("Escape - Отменить поиск");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Выберите, по какому пункту вы хотите произвести поиск:");
            ArrowsMenu.Items.Clear();
            ArrowsMenu.Items.Add(new ListItem("ID"));
            ArrowsMenu.Items.Add(new ListItem("Название"));
            ArrowsMenu.Items.Add(new ListItem("Категория"));
            for (int i = 2; i < 12; i++)
            {
                Console.SetCursorPosition(92, i);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(95, 3);
            Console.Write("Роли:");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("0 - Breakfasts");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("1 - Snacks");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("2 - Salads");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("3 - Soups");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("4 - HotDishes");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("5 - SideDishes");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("6 - Desserts");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("7 - Drinks");
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            Console.Write("->");
        }

        public void PrintSearchedMainWindow()
        {
            Search();
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            if (Dish.SearchedDishes.Count() > 0)
            {
                Console.Write("->");
            }
        }
    }
}
