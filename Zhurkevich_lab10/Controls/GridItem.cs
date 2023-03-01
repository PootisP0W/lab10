using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhurkevich_lab10.Controls
{
    internal class GridRowId : ControlElement
    {
        public int Id { get; set; }

        public GridRowId(int id)
        {
            Id = id;
        }
    }
}
