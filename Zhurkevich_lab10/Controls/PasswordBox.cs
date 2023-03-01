using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhurkevich_lab10.Controls
{
    internal class PasswordBox : ControlElement
    {
        public string Label { get; set; }
        public string Text { get; set; }

        public PasswordBox(string label)
        {
            Label = $"  {label}:";
            Console.WriteLine(Label);
        }

        public void Input()
        {
            Console.SetCursorPosition(Label.Length + 1, Console.CursorTop);
            if (!string.IsNullOrEmpty(Text))
            {
                for (int i = 0; i < Text.Length; i++)
                {
                    Console.Write(" ");
                }
                Text = null;
            }
            Console.SetCursorPosition(Label.Length + 1, Console.CursorTop);
            while (true)
            {
                ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (keyinfo.Key == ConsoleKey.Backspace)
                {
                    if (Text.Length > 0)
                    {
                        Text = Text.Substring(0, Text.Length - 1);
                        Console.SetCursorPosition(Console.CursorLeft -1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                }
                else if (keyinfo.KeyChar >= 'a' && keyinfo.KeyChar <= 'z' || keyinfo.KeyChar >= 'A' && keyinfo.KeyChar <= 'Z' ||
                    keyinfo.KeyChar >= 'а' && keyinfo.KeyChar <= 'я' || keyinfo.KeyChar >= 'А' && keyinfo.KeyChar <= 'Я' ||
                    keyinfo.KeyChar >= '0' && keyinfo.KeyChar <= '9')
                {
                    Console.Write("*");
                    Text += keyinfo.KeyChar;
                }
            }
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}
