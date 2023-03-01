using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhurkevich_lab10.Classes;
using Zhurkevich_lab10.Controls;
using Zhurkevich_lab10.Interfaces;

namespace Zhurkevich_lab10.Models
{
    internal class RestaurantManager : Employee, ICreatable, IReadable, IUpdatable, IDeletable, ISearchable
    {
        public RestaurantManager(int id, string login, string password, string secondName, string firstName, string patronymic, DateTime birthdate, decimal salary) : base(id, login, password, secondName, firstName, patronymic, birthdate, Role.RestaurantManager, salary)
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
            Console.Write("Название");
            Console.SetCursorPosition(60, Console.CursorTop);
            Console.Write("Дата");
            Console.SetCursorPosition(80, Console.CursorTop);
            Console.Write("Время");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            ArrowsMenu.Items.Clear();
            Console.SetCursorPosition(15, 3);
            foreach (Note note in Note.Notes)
            {
                Console.SetCursorPosition(2, Console.CursorTop);
                Console.Write(note.Id);
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.Write(note.Name);              
                Console.SetCursorPosition(60, Console.CursorTop);
                Console.Write(note.Date);
                Console.SetCursorPosition(80, Console.CursorTop);
                Console.Write(note.Time);
                ArrowsMenu.Items.Add(new GridRowId(note.Id));
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
            Console.Write("Название");
            Console.SetCursorPosition(60, Console.CursorTop);
            Console.Write("Дата");
            Console.SetCursorPosition(80, Console.CursorTop);
            Console.Write("Время");
            Console.SetCursorPosition(92, Console.CursorTop);
            Console.Write("|");
            Console.SetCursorPosition(92, Console.CursorTop + 1);
            Console.Write("|");
            ArrowsMenu.Items.Clear();
            Console.SetCursorPosition(15, 3);
            foreach (Note note in Note.SearchedNotes)
            {
                Console.SetCursorPosition(2, Console.CursorTop);
                Console.Write(note.Id);
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.Write(note.Name);
                Console.SetCursorPosition(60, Console.CursorTop);
                Console.Write(note.Date);
                Console.SetCursorPosition(80, Console.CursorTop);
                Console.Write(note.Time);
                ArrowsMenu.Items.Add(new GridRowId(note.Id));
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
        }

        private void PrintHeader()
        {
            Console.Clear();
            Console.Write($"  Добро пожаловать, {Login}!");
            Console.SetCursorPosition(54, Console.CursorTop);
            Console.WriteLine("Роль: Менеджер ресторана");
            for (int i = 0; i < 120; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        public void PrintMainWindow()
        {
            Note.SearchedNotes.Clear();
            Read();
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            if (Note.Notes.Count > 0)
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
            ArrowsMenu.Items.Clear();
            ArrowsMenu.Items.Add(new TextBox("ID"));
            ArrowsMenu.Items.Add(new TextBox("Название"));
            ArrowsMenu.Items.Add(new TextBox("Описание"));
            ArrowsMenu.Items.Add(new TextBox("Дата"));
            ArrowsMenu.Items.Add(new TextBox("Время"));
            Console.SetCursorPosition(92, 2);
            for (int i = 0; i < 8; i++)
            {
                Console.SetCursorPosition(92, Console.CursorTop);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            Console.Write("->");
        }


        public void PrintUpdateWindow(Note note)
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
            Console.SetCursorPosition(0, 3);
            ArrowsMenu.Items.Clear();
            ArrowsMenu.Items.Add(new TextBox("ID", note.Id.ToString()) { Enable = false });
            ArrowsMenu.Items.Add(new TextBox("Название", note.Name));
            ArrowsMenu.Items.Add(new TextBox("Описание", note.Description));
            ArrowsMenu.Items.Add(new TextBox("Дата", note.Date.ToString()));
            ArrowsMenu.Items.Add(new TextBox("Время", note.Time.ToString()));
            Console.SetCursorPosition(92, 2);
            for (int i = 0; i < 8; i++)
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
            ArrowsMenu.Items.Add(new ListItem("Название"));
            ArrowsMenu.Items.Add(new ListItem("Дата"));
            for (int i = 2; i < 9; i++)
            {
                Console.SetCursorPosition(92, i);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            Console.Write("->");
        }

        public void PrintSearchedMainWindow()
        {
            Search();
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);
            if (Note.SearchedNotes.Count > 0)
            {
                Console.Write("->");
            }
        }
    }
}
