using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhurkevich_lab10.Models
{
    public enum Role
    {
        PersonnelOfficer,
        RestaurantManager,
        Chef,
        Accountant,
        Waiter
    }

    internal class Employee
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthdate { get; set; }
        public Role Role { get; set; }
        public decimal Salary { get; set; }
        public static List<Employee> Employees { get; set; } = new List<Employee>();
        public static List<Employee> SearchedEmployees { get; set; } = new List<Employee>();

        public Employee(int id, string login, string password, string secondName, string firstName, string patronymic, DateTime birthdate, Role role, decimal salary)
        {
            Id = id;
            Login = login;
            Password = password;
            SecondName = secondName;
            FirstName = firstName;
            Patronymic = patronymic;
            Birthdate = birthdate;
            Role = role;
            Salary = salary;
        }
    }
}
