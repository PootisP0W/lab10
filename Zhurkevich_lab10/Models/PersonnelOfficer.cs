using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhurkevich_lab10.Classes;
using Zhurkevich_lab10.Controls;
using Zhurkevich_lab10.Interfaces;

namespace Zhurkevich_lab10.Models
{
    internal class PersonnelOfficer : Employee, ICreatable, IReadable, IUpdatable, IDeletable, ISearchable
    {
        public PersonnelOfficer(int id, string login, string password, string secondName, string firstName, string patronymic, DateTime birthdate, decimal salary) : base(id, login, password, secondName, firstName, patronymic, birthdate, Role.PersonnelOfficer, salary)
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
            Console.Write("Фамилия");
            Console.SetCursorPosition(30, Console.CursorTop);
            Console.Write("Имя");
            Console.SetCursorPosition(50, Console.CursorTop);
            Console.Write("Логин");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.Write("Роль");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            ArrowsMenu.Items.Clear();
            Console.SetCursorPosition(3, 3);
            foreach (Employee employee in Employees)
            {
                Console.SetCursorPosition(2, Console.CursorTop);
                Console.Write(employee.Id);
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.Write(employee.SecondName);
                Console.SetCursorPosition(30, Console.CursorTop);
                Console.Write(employee.FirstName);
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.Write(employee.Login);
                Console.SetCursorPosition(70, Console.CursorTop);
                switch (employee.Role)
                {
                    case Role.PersonnelOfficer:
                        Console.Write("Кадровик");
                        break;
                    case Role.RestaurantManager:
                        Console.Write("Менеджер ресторана");
                        break;
                    case Role.Chef:
                        Console.Write("Шеф");
                        break;
                    case Role.Accountant:
                        Console.Write("Бухгалтер");
                        break;
                    case Role.Waiter:
                        Console.Write("Официант");
                        break;
                }
                ArrowsMenu.Items.Add(new GridRowId(employee.Id));
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
            Console.Write("Фамилия");
            Console.SetCursorPosition(30, Console.CursorTop);
            Console.Write("Имя");
            Console.SetCursorPosition(50, Console.CursorTop);
            Console.Write("Логин");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.Write("Роль");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            ArrowsMenu.Items.Clear();
            Console.SetCursorPosition(15, 3);
            foreach (Employee employee in SearchedEmployees)
            {
                Console.SetCursorPosition(2, Console.CursorTop);
                Console.Write(employee.Id);
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.Write(employee.SecondName);
                Console.SetCursorPosition(30, Console.CursorTop);
                Console.Write(employee.FirstName);
                Console.SetCursorPosition(50, Console.CursorTop);
                Console.Write(employee.Login);
                Console.SetCursorPosition(70, Console.CursorTop);
                switch (employee.Role)
                {
                    case Role.PersonnelOfficer:
                        Console.Write("Кадровик");
                        break;
                    case Role.RestaurantManager:
                        Console.Write("Менеджер ресторана");
                        break;
                    case Role.Chef:
                        Console.Write("Шеф");
                        break;
                    case Role.Accountant:
                        Console.Write("Бухгалтер");
                        break;
                    case Role.Waiter:
                        Console.Write("Официант");
                        break;
                }
                ArrowsMenu.Items.Add(new GridRowId(employee.Id));
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
        }

        private void PrintHeader()
        {
            Console.Clear();
            Console.Write($"  Добро пожаловать, {Login}!");
            Console.SetCursorPosition(54, Console.CursorTop);
            Console.WriteLine("Роль: Кадровик");
            for (int i = 0; i < 120; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        public void PrintMainWindow()
        {
            SearchedEmployees.Clear();
            Read();
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            if (Employees.Count > 0)
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
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Роли:");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("0 - PersonnelOfficer");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("1 - RestaurantManager");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("2 - Chef");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("3 - Accountant");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("4 - Waiter");
            Console.SetCursorPosition(0, 3);
            ArrowsMenu.Items.Clear();
            ArrowsMenu.Items.Add(new TextBox("ID"));
            ArrowsMenu.Items.Add(new TextBox("Логин"));
            ArrowsMenu.Items.Add(new TextBox("Пароль"));
            ArrowsMenu.Items.Add(new TextBox("Фамилия"));
            ArrowsMenu.Items.Add(new TextBox("Имя"));
            ArrowsMenu.Items.Add(new TextBox("Отчество"));
            ArrowsMenu.Items.Add(new TextBox("Дата рождения"));
            ArrowsMenu.Items.Add(new TextBox("Роль"));
            ArrowsMenu.Items.Add(new TextBox("Зарплата"));
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

        public void PrintUpdateWindow(Employee employee)
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
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("Роли:");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("0 - PersonnelOfficer");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("1 - RestaurantManager");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("2 - Chef");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("3 - Accountant");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("4 - Waiter");
            Console.SetCursorPosition(0, 3);
            ArrowsMenu.Items.Clear();
            ArrowsMenu.Items.Add(new TextBox("ID", employee.Id.ToString()) { Enable = false });
            ArrowsMenu.Items.Add(new TextBox("Логин", employee.Login) { Enable = false });
            ArrowsMenu.Items.Add(new TextBox("Пароль", employee.Password));
            ArrowsMenu.Items.Add(new TextBox("Фамилия", employee.SecondName));
            ArrowsMenu.Items.Add(new TextBox("Имя", employee.FirstName));
            ArrowsMenu.Items.Add(new TextBox("Отчество", employee.Patronymic));
            ArrowsMenu.Items.Add(new TextBox("Дата рождения", employee.Birthdate.ToShortDateString()));
            ArrowsMenu.Items.Add(new TextBox("Роль", employee.Role.ToString()) { Enable = false });
            ArrowsMenu.Items.Add(new TextBox("Зарплата", employee.Salary.ToString()));
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
            ArrowsMenu.Items.Add(new ListItem("Логин"));
            ArrowsMenu.Items.Add(new ListItem("Фамилия"));
            ArrowsMenu.Items.Add(new ListItem("Имя"));
            ArrowsMenu.Items.Add(new ListItem("Отчество"));
            ArrowsMenu.Items.Add(new ListItem("Роль"));
            for (int i = 2; i < 12; i++)
            {
                Console.SetCursorPosition(92, i);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(95, 3);
            Console.Write("Роли:");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("0 - PersonnelOfficer");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("1 - RestaurantManager");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("2 - Chef");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("3 - Accountant");
            Console.SetCursorPosition(95, Console.CursorTop + 1);
            Console.Write("4 - Waiter");
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            Console.Write("->");
        }

        public void PrintSearchedMainWindow()
        {
            Search();        
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            if (SearchedEmployees.Count > 0)
            {
                Console.Write("->");
            }
        }
    }
}
