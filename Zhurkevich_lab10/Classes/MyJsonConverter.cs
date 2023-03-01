using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Zhurkevich_lab10.Models;

namespace Zhurkevich_lab10.Classes
{
    internal static class MyJsonConverter
    {
        private static string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}\Esenin";
        private static string employee = "employee.json";
        private static string note = "note.json";
        private static string dish = "dish.json";
        private static string pay = "pay.json";
        private static string order = "order.json";

        public static void Serialize()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists($@"{path}\{employee}"))
            {
                File.Create($@"{path}\{employee}").Close();
            }
            string json = JsonConvert.SerializeObject(Employee.Employees);
            File.WriteAllText($@"{path}\{employee}", json);
            if (!File.Exists($@"{path}\{note}"))
            {
                File.Create($@"{path}\{note}").Close();
            }
            json = JsonConvert.SerializeObject(Note.Notes);
            File.WriteAllText($@"{path}\{note}", json);
            if (!File.Exists($@"{path}\{dish}"))
            {
                File.Create($@"{path}\{dish}").Close();
            }
            json = JsonConvert.SerializeObject(Dish.Dishes);
            File.WriteAllText($@"{path}\{dish}", json);
            if (!File.Exists($@"{path}\{pay}"))
            {
                File.Create($@"{path}\{pay}").Close();
            }
            json = JsonConvert.SerializeObject(Pay.Pays);
            File.WriteAllText($@"{path}\{pay}", json);
            if (!File.Exists($@"{path}\{order}"))
            {
                File.Create($@"{path}\{order}").Close();
            }
            json = JsonConvert.SerializeObject(Order.Orders);
            File.WriteAllText($@"{path}\{order}", json);
        }

        public static void Deserialize()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists($@"{path}\{employee}"))
            {
                File.Create($@"{path}\{employee}").Close();
            }
            Employee.Employees.Clear();
            string json = File.ReadAllText($@"{path}\{employee}");
            if (!string.IsNullOrEmpty(json))
            {
                foreach (Employee employee in JsonConvert.DeserializeObject<List<Employee>>(json))           {
                    switch (employee.Role)
                    {
                        case Role.PersonnelOfficer:
                            Employee.Employees.Add(new PersonnelOfficer(employee.Id, employee.Login, employee.Password, employee.SecondName, employee.FirstName, employee.Patronymic, employee.Birthdate, employee.Salary));
                            break;
                        case Role.RestaurantManager:
                            Employee.Employees.Add(new RestaurantManager(employee.Id, employee.Login, employee.Password, employee.SecondName, employee.FirstName, employee.Patronymic, employee.Birthdate, employee.Salary));
                            break;
                        case Role.Chef:
                            Employee.Employees.Add(new Chef(employee.Id, employee.Login, employee.Password, employee.SecondName, employee.FirstName, employee.Patronymic, employee.Birthdate, employee.Salary));
                            break;
                        case Role.Accountant:
                            Employee.Employees.Add(new Accountant(employee.Id, employee.Login, employee.Password, employee.SecondName, employee.FirstName, employee.Patronymic, employee.Birthdate, employee.Salary));
                            break;
                        case Role.Waiter:
                            Employee.Employees.Add(new Waiter(employee.Id, employee.Login, employee.Password, employee.SecondName, employee.FirstName, employee.Patronymic, employee.Birthdate, employee.Salary));
                            break;
                    }
                }
            }
            if (!File.Exists($@"{path}\{note}"))
            {
                File.Create($@"{path}\{note}").Close();
            }
            Note.Notes.Clear();
            json = File.ReadAllText($@"{path}\{note}");
            if (!string.IsNullOrEmpty(json))
            {
                Note.Notes = JsonConvert.DeserializeObject<List<Note>>(json);
            }
            if (!File.Exists($@"{path}\{dish}"))
            {
                File.Create($@"{path}\{dish}").Close();
            }
            Dish.Dishes.Clear();
            json = File.ReadAllText($@"{path}\{dish}");
            if (!string.IsNullOrEmpty(json))
            {
                Dish.Dishes = JsonConvert.DeserializeObject<List<Dish>>(json);
            }
            if (!File.Exists($@"{path}\{pay}"))
            {
                File.Create($@"{path}\{pay}").Close();
            }
            Pay.Pays.Clear();
            json = File.ReadAllText($@"{path}\{pay}");
            if (!string.IsNullOrEmpty(json))
            {
                Pay.Pays = JsonConvert.DeserializeObject<List<Pay>>(json);
            }
            if (!File.Exists($@"{path}\{order}"))
            {
                File.Create($@"{path}\{order}").Close();
            }
            Order.Orders.Clear();
            json = File.ReadAllText($@"{path}\{order}");
            if (!string.IsNullOrEmpty(json))
            {
                Order.Orders = JsonConvert.DeserializeObject<List<Order>>(json);
            }
        }
    }
}
