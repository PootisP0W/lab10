using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhurkevich_lab10.Controls
{
    internal class ListItem : ControlElement
    {
        private string label { get; set; }
        public string Item { get; set; }

        public ListItem(string label)
        {
            this.label = $"  {label}";
            Item = label;
            Console.WriteLine(this.label);
        }
    }
}
