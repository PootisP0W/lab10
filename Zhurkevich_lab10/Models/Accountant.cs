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
    internal class Accountant : Employee, ICreatable, IReadable, IUpdatable, IDeletable, ISearchable
    {
        public Accountant(int id, string login, string password, string secondName, string firstName, string patronymic, DateTime birthdate, decimal salary) : base(id, login, password, secondName, firstName, patronymic, birthdate, Role.Accountant, salary)
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
            Console.SetCursorPosition(40, Console.CursorTop);
            Console.Write("Сумма");
            Console.SetCursorPosition(55, Console.CursorTop);
            Console.Write("Дата");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.Write("Время");
            Console.SetCursorPosition(80, Console.CursorTop);
            Console.Write("Прибавка?");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            ArrowsMenu.Items.Clear();
            Console.SetCursorPosition(15, 3);
            foreach (Pay pay in Pay.Pays)
            {
                Console.SetCursorPosition(2, Console.CursorTop);
                Console.Write(pay.Id);
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.Write(pay.Name);
                Console.SetCursorPosition(40, Console.CursorTop);
                Console.Write(pay.Sum);
                Console.SetCursorPosition(55, Console.CursorTop);
                Console.Write(pay.Date);
                Console.SetCursorPosition(70, Console.CursorTop);
                Console.Write(pay.Time);
                Console.SetCursorPosition(80, Console.CursorTop);
                Console.Write(pay.IsIncrease);
                ArrowsMenu.Items.Add(new GridRowId(pay.Id));
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < 92; i++)
            {
                Console.Write("-");
            }
            Console.Write("|");
            Console.SetCursorPosition(70, Console.CursorTop + 1);
            decimal sum = 0;
            foreach(Pay pay in Pay.Pays)
            {
                if (pay.IsIncrease)
                {
                    sum += pay.Sum;
                }
                else
                {
                    sum -= pay.Sum;
                }
            }
            Console.Write($"Итог: {sum}");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.WriteLine("|");
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
            Console.SetCursorPosition(40, Console.CursorTop);
            Console.Write("Сумма");
            Console.SetCursorPosition(55, Console.CursorTop);
            Console.Write("Дата");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.Write("Время");
            Console.SetCursorPosition(80, Console.CursorTop);
            Console.Write("Прибавка?");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.Write("|");
            ArrowsMenu.Items.Clear();
            Console.SetCursorPosition(15, 3);
            foreach (Pay pay in Pay.SearchedPays)
            {
                Console.SetCursorPosition(2, Console.CursorTop);
                Console.Write(pay.Id);
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.Write(pay.Name);
                Console.SetCursorPosition(40, Console.CursorTop);
                Console.Write(pay.Sum);
                Console.SetCursorPosition(55, Console.CursorTop);
                Console.Write(pay.Date);
                Console.SetCursorPosition(70, Console.CursorTop);
                Console.Write(pay.Time);
                Console.SetCursorPosition(80, Console.CursorTop);
                Console.Write(pay.IsIncrease);
                ArrowsMenu.Items.Add(new GridRowId(pay.Id));
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(0,Console.CursorTop);
            for (int i = 0; i < 92; i++)
            {
                Console.Write("-");
            }
            Console.Write("|");
            Console.SetCursorPosition(70, Console.CursorTop + 1);
            Console.Write($"Итог: {Pay.SearchedPays.Sum(p => p.Sum)}");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.WriteLine("|");
        }

        private void PrintHeader()
        {
            Console.Clear();
            Console.Write($"  Добро пожаловать, {Login}!");
            Console.SetCursorPosition(54, Console.CursorTop);
            Console.WriteLine("Роль: Бухгалтер");
            for (int i = 0; i < 120; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        public void PrintMainWindow()
        {
            Pay.SearchedPays.Clear();
            Read();
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            if (Pay.Pays.Count > 0)
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
            Console.SetCursorPosition(0, 3);
            ArrowsMenu.Items.Clear();
            ArrowsMenu.Items.Add(new TextBox("ID"));
            ArrowsMenu.Items.Add(new TextBox("Название"));
            ArrowsMenu.Items.Add(new TextBox("Сумма"));
            ArrowsMenu.Items.Add(new TextBox("Дата записи"));
            ArrowsMenu.Items.Add(new TextBox("Время записи"));
            ArrowsMenu.Items.Add(new TextBox("Прибавка?"));
            Console.SetCursorPosition(92, 2);
            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            Console.Write("->");
        }


        public void PrintUpdateWindow(Pay pay)
        {
            Console.Clear();
            ArrowsMenu.Current = 0;
            PrintHeader();
            Console.SetCursorPosition(95, Console.CursorTop);
            Console.Write("S - Сохранить изменения");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Del - Удалить запись");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Escape - Отменить измен.");
            Console.SetCursorPosition(0, 3);
            ArrowsMenu.Items.Clear();
            ArrowsMenu.Items.Add(new TextBox("ID", pay.Id.ToString()) { Enable = false});
            ArrowsMenu.Items.Add(new TextBox("Название", pay.Name));
            ArrowsMenu.Items.Add(new TextBox("Сумма", pay.Sum.ToString()));
            ArrowsMenu.Items.Add(new TextBox("Дата записи", pay.Date.ToString()));
            ArrowsMenu.Items.Add(new TextBox("Время записи", pay.Time.ToString()));
            ArrowsMenu.Items.Add(new TextBox("Прибавка?", pay.IsIncrease.ToString()));
            Console.SetCursorPosition(92, 2);
            for (int i = 0; i < 9; i++)
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
            ArrowsMenu.Items.Add(new ListItem("Дата"));
            ArrowsMenu.Items.Add(new ListItem("Выплата?"));
            for (int i = 2; i < 10; i++)
            {
                Console.SetCursorPosition(92, i);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            Console.Write("->");
        }

        public void PrintSearchedMainWindow()
        {
            Search();
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            if (Pay.SearchedPays.Count > 0)
            {
                Console.Write("->");
            }
        }
    }
}
