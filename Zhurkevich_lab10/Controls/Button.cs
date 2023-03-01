using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhurkevich_lab10.Controls
{
    internal class Button : ControlElement
    {
        public string Label { get; set; }

        public Button(string label)
        {
            Label = $"  |{label}|";
            Console.WriteLine(Label);
        }
    }
}
