using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhurkevich_lab10.Controls;

namespace Zhurkevich_lab10.Classes
{
    public enum Key
    {
        Unused,
        UpArrow,
        DownArrow,
        Enter,
        Escape,
        F1,
        F2,
        S,
        Del,
        Plus,
        Minus
    }

    public enum Window
    {
        AuthorizationWindow,
        PersonnelOfficerMainWindow,
        PersonnelOfficerAddWindow,
        PersonnelOfficerUpdateWindow,
        PersonnelOfficerSearchWindow,
        PersonnelOfficerSearchedMainWindow,
        RestaurantManagerMainWindow,
        RestaurantManagerAddWindow,
        RestaurantManagerUpdateWindow,
        RestaurantManagerSearchWindow,
        RestaurantManagerSearchedMainWindow,
        ChefOfficerMainWindow,
        ChefOfficerAddWindow,
        ChefOfficerUpdateWindow,
        ChefOfficerSearchWindow,
        ChefOfficerSearchedMainWindow,
        AccountantMainWindow,
        AccountantAddWindow,
        AccountantUpdateWindow,
        AccountantSearchWindow,
        AccountantSearchedMainWindow,
        WaiterMainWindow,
        WaiterAddWindow
    }

    internal static class ArrowsMenu
    {
        private static int current;
        public static List<ControlElement> Items = new List<ControlElement>();
        private static int min { get; } = 3;
        private static int max { get { return Items.Count + 2; } }
        public static int Current { get { return current - 3; } set { current = value + 3; } }

        public static Key ReadKey()
        {
            Key key = new Key();
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    key = Key.UpArrow;
                    break;
                case ConsoleKey.DownArrow:
                    key = Key.DownArrow;
                    break;
                case ConsoleKey.Enter:
                    key = Key.Enter;
                    break;
                case ConsoleKey.Escape:
                    key = Key.Escape;
                    break;
                case ConsoleKey.F1:
                    key = Key.F1;
                    break;
                case ConsoleKey.F2:
                    key = Key.F2;
                    break;
                case ConsoleKey.S:
                    key = Key.S;
                    break;
                case ConsoleKey.Delete:
                    key = Key.Del;
                    break;
                case ConsoleKey.Add:
                case ConsoleKey.OemPlus:
                    key = Key.Plus;
                    break;
                case ConsoleKey.Subtract:
                case ConsoleKey.OemMinus:
                    key = Key.Minus;
                    break;
                default:
                    key = Key.Unused;
                    break;
            }
            return key;
        }

        public static void Up()
        {
            EraseArrow();
            if (current == min)
            {
                current = max;
            }
            else
            {
                current--;
            }
            DrawArrow();
        }

        public static void Down()
        {
            EraseArrow();
            if (current == max)
            {
                current = min;
                Console.SetCursorPosition(0, 0);
            }
            else
            {
                current++;
            }
            DrawArrow();
        }

        private static void DrawArrow()
        {
            Console.SetCursorPosition(0, current);
            Console.Write("->");
        }

        private static void EraseArrow()
        {
            Console.SetCursorPosition(0, current);
            Console.Write("  ");
        }
    }
}
