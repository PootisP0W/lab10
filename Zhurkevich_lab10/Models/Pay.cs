using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhurkevich_lab10.Classes;
using Zhurkevich_lab10.Interfaces;

namespace Zhurkevich_lab10.Models
{
    internal class Pay : ICreatable, IUpdatable, IDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Sum { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public bool IsIncrease { get; set; }
        public static List<Pay> Pays { get; set; } = new List<Pay>();
        public static List<Pay> SearchedPays { get; set; } = new List<Pay>();

        public Pay(int id, string name, decimal sum, DateOnly date, TimeOnly time, bool isIncrease)
        {
            Id = id;
            Name = name;
            Sum = sum;
            Date = date;
            Time = time;
            IsIncrease = isIncrease;
        }

        public void Create()
        {
            Pays.Add(this);
            MyJsonConverter.Serialize();
        }

        public void Update()
        {
            MyJsonConverter.Serialize();
        }

        public void Delete()
        {
            Pays.Remove(this);
            MyJsonConverter.Serialize();       
        }
    }
}
