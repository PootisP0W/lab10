using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhurkevich_lab10.Classes;
using Zhurkevich_lab10.Interfaces;

namespace Zhurkevich_lab10.Models
{
    internal class Note : ICreatable, IUpdatable, IDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public static List<Note> Notes { get; set; } = new List<Note>();
        public static List<Note> SearchedNotes { get; set; } = new List<Note>();

        public Note(int id, string name, string description, DateOnly date, TimeOnly time)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
            Time = time;
        }

        public void Create()
        {
            Notes.Add(this);
            MyJsonConverter.Serialize();
        }

        public void Update()
        {
            MyJsonConverter.Serialize();
        }

        public void Delete()
        {
            Notes.Remove(this);
            MyJsonConverter.Serialize();
        }
    }
}
