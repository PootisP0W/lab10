using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhurkevich_lab10.Controls;
using Zhurkevich_lab10.Models;

namespace Zhurkevich_lab10.Classes
{
    internal static class Authorization
    {
        public static Employee SignedEmployee { get; set; }

        public static void SignIn(string login, string password)
        {
            MyJsonConverter.Deserialize();
            SignedEmployee = Employee.Employees.FirstOrDefault(e => e.Login == login && e.Password == password); 
        }

        public static void SignOut()
        {
            SignedEmployee = null;
            PrintWindow();
        }

        public static void PrintWindow()
        {
            Console.Clear();
            Console.SetCursorPosition(42, Console.CursorTop);
            Console.WriteLine("Добро пожаловать в ресторан Есенин");
            for (int i = 0; i < 120; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("\nВход");
            ArrowsMenu.Items.Clear();
            ArrowsMenu.Items.Add(new TextBox("Логин"));
            ArrowsMenu.Items.Add(new PasswordBox("Пароль"));
            ArrowsMenu.Items.Add(new Button("Войти"));
            Console.SetCursorPosition(0, 3);
            Console.Write("->");
            ArrowsMenu.Current = 0;
        }
    }
}
