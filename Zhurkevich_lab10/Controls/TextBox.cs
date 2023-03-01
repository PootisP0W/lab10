using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhurkevich_lab10.Controls
{
    internal class TextBox : ControlElement
    {
        public string Label { get; set; }
        public string Text { get; set; }
        public bool Enable { get; set; } = true;

        public TextBox(string label)
        {
            Label = $"  {label}:";
            Console.WriteLine(Label);
        }

        public TextBox(string label, string text)
        {
            Label = $"  {label}:";
            Text = text;
            Console.WriteLine($"{Label} {Text}");
        }

        public void Input()
        {
            if (Enable)
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
                Text = Console.ReadLine();
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            }
        }
    }
}
