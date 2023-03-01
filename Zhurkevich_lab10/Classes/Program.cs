using System.Data;
using Zhurkevich_lab10.Classes;
using Zhurkevich_lab10.Controls;
using Zhurkevich_lab10.Models;

Window window = Window.AuthorizationWindow;
Authorization.PrintWindow();
Console.Title = "Ресторан Есенин";

while (true)
{
    Key key = ArrowsMenu.ReadKey();
    switch (window)
    {
        case Window.AuthorizationWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    if (ArrowsMenu.Items[ArrowsMenu.Current] is TextBox)
                    {
                        (ArrowsMenu.Items[ArrowsMenu.Current] as TextBox).Input();
                    }
                    else if (ArrowsMenu.Items[ArrowsMenu.Current] is PasswordBox)
                    {
                        (ArrowsMenu.Items[ArrowsMenu.Current] as PasswordBox).Input();
                    }
                    else if (ArrowsMenu.Items[ArrowsMenu.Current] is Button)
                    {
                        int cursorTopWas = Console.CursorTop;
                        Console.SetCursorPosition(0, 7);
                        Console.Write("                                                                ");
                        Console.SetCursorPosition(0, cursorTopWas);
                        TextBox loginTextBox = (ArrowsMenu.Items[0] as TextBox);
                        PasswordBox passwordBox = (ArrowsMenu.Items[1] as PasswordBox);
                        Authorization.SignIn(loginTextBox.Text, passwordBox.Text);
                        if (string.IsNullOrEmpty(loginTextBox.Text) || string.IsNullOrEmpty(passwordBox.Text))
                        {
                            PrintError("Введите логин и пароль", 7, cursorTopWas);
                        }
                        else if (Authorization.SignedEmployee == null)
                        {
                            PrintError("Введите корректный логин и пароль", 7, cursorTopWas);
                        }
                        else
                        {
                            switch (Authorization.SignedEmployee.Role)
                            {
                                case Role.PersonnelOfficer:
                                    window = Window.PersonnelOfficerMainWindow;
                                    (Authorization.SignedEmployee as PersonnelOfficer).PrintMainWindow();
                                    break;
                                case Role.RestaurantManager:
                                    window = Window.RestaurantManagerMainWindow;
                                    (Authorization.SignedEmployee as RestaurantManager).PrintMainWindow();
                                    break;
                                case Role.Chef:
                                    window = Window.ChefOfficerMainWindow;
                                    (Authorization.SignedEmployee as Chef).PrintMainWindow();
                                    break;
                                case Role.Accountant:
                                    window = Window.AccountantMainWindow;
                                    (Authorization.SignedEmployee as Accountant).PrintMainWindow();
                                    break;
                                case Role.Waiter:
                                    window = Window.WaiterMainWindow;
                                    (Authorization.SignedEmployee as Waiter).PrintMainWindow();
                                    break;
                            }
                        }
                    }
                    break;
            }
            break;
        case Window.PersonnelOfficerMainWindow:
            switch (key)
            {
                case Key.UpArrow:
                    if (Employee.Employees.Count() > 0)
                    {
                        ArrowsMenu.Up();
                    }
                    break;
                case Key.DownArrow:
                    if (Employee.Employees.Count() > 0)
                    {
                        ArrowsMenu.Down();
                    }
                    break;
                case Key.Enter:
                    if (Employee.Employees.Count() > 0)
                    {
                        window = Window.PersonnelOfficerUpdateWindow;
                        (Authorization.SignedEmployee as PersonnelOfficer).PrintUpdateWindow(Employee.Employees[ArrowsMenu.Current]);
                    }
                    break;
                case Key.F1:
                    window = Window.PersonnelOfficerAddWindow;
                    (Authorization.SignedEmployee as PersonnelOfficer).PrintAddWindow();
                    break;
                case Key.F2:
                    window = Window.PersonnelOfficerSearchWindow;
                    (Authorization.SignedEmployee as PersonnelOfficer).PrintSearchWindow();
                    break;
                case Key.Escape:
                    window = Window.AuthorizationWindow;
                    Authorization.SignOut();
                    break;
            }
            break;
        case Window.PersonnelOfficerAddWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    (ArrowsMenu.Items[ArrowsMenu.Current] as TextBox).Input();
                    break;
                case Key.S:
                    int cursorTopWas = Console.CursorTop;
                    Console.SetCursorPosition(0, 13);
                    Console.Write("                                                                ");
                    Console.SetCursorPosition(0, cursorTopWas);
                    string idString = (ArrowsMenu.Items[0] as TextBox).Text;
                    string login = (ArrowsMenu.Items[1] as TextBox).Text;
                    string password = (ArrowsMenu.Items[2] as TextBox).Text;
                    string secondName = (ArrowsMenu.Items[3] as TextBox).Text;
                    string firstName = (ArrowsMenu.Items[4] as TextBox).Text;
                    string patronymic = (ArrowsMenu.Items[5] as TextBox).Text;
                    string birthdateString = (ArrowsMenu.Items[6] as TextBox).Text;
                    string roleString = (ArrowsMenu.Items[7] as TextBox).Text;
                    string salaryString = (ArrowsMenu.Items[8] as TextBox).Text;
                    if (string.IsNullOrEmpty(idString) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)
                        || string.IsNullOrEmpty(secondName) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(patronymic)
                        || string.IsNullOrEmpty(birthdateString) || string.IsNullOrEmpty(roleString) || string.IsNullOrEmpty(salaryString))
                    {
                        PrintError("Заполните все поля", 13, cursorTopWas);
                    }
                    else if (!int.TryParse(idString, out int id))
                    {
                        PrintError("Введите числовой ID", 13, cursorTopWas);
                    }
                    else if (Employee.Employees.Where(e => e.Id == id).Count() > 0)
                    {
                        PrintError("Введите уникальный ID", 13, cursorTopWas);
                    }
                    else if (Employee.Employees.Where(e => e.Login == login).Count() > 0)
                    {
                        PrintError("Введите уникальный логин", 13, cursorTopWas);
                    }
                    else if (!DateTime.TryParse(birthdateString, out DateTime birthdate))
                    {
                        PrintError("Введите корректный формат даты рождения", 13, cursorTopWas);
                    }
                    else if (!Enum.TryParse(roleString, out Role role))
                    {
                        PrintError("Введите корректную роль", 13, cursorTopWas);
                    }
                    else if (!decimal.TryParse(salaryString, out decimal salary))
                    {
                        PrintError("Введите вещественную зарплату", 13, cursorTopWas);
                    }
                    else
                    {
                        switch (role)
                        {
                            case Role.PersonnelOfficer:
                                new PersonnelOfficer(id, login, password, secondName, firstName, patronymic, birthdate, salary).Create();
                                break;
                            case Role.RestaurantManager:
                                new RestaurantManager(id, login, password, secondName, firstName, patronymic, birthdate, salary).Create();
                                break;
                            case Role.Chef:
                                new Chef(id, login, password, secondName, firstName, patronymic, birthdate, salary).Create();
                                break;
                            case Role.Accountant:
                                new Accountant(id, login, password, secondName, firstName, patronymic, birthdate, salary).Create();
                                break;
                            case Role.Waiter:
                                new Waiter(id, login, password, secondName, firstName, patronymic, birthdate, salary).Create();
                                break;
                        }
                        window = Window.PersonnelOfficerMainWindow;
                        (Authorization.SignedEmployee as PersonnelOfficer).PrintMainWindow();
                    }
                    break;
                case Key.Escape:
                    window = Window.PersonnelOfficerMainWindow;
                    (Authorization.SignedEmployee as PersonnelOfficer).PrintMainWindow();
                    break;
            }
            break;
        case Window.PersonnelOfficerUpdateWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    (ArrowsMenu.Items[ArrowsMenu.Current] as TextBox).Input();
                    break;
                case Key.S:
                    int cursorTopWas = Console.CursorTop;
                    Console.SetCursorPosition(0, 13);
                    Console.Write("                                                                ");
                    Console.SetCursorPosition(0, cursorTopWas);
                    int id = int.Parse((ArrowsMenu.Items[0] as TextBox).Text);
                    string password = (ArrowsMenu.Items[2] as TextBox).Text;
                    string secondName = (ArrowsMenu.Items[3] as TextBox).Text;
                    string firstName = (ArrowsMenu.Items[4] as TextBox).Text;
                    string patronymic = (ArrowsMenu.Items[5] as TextBox).Text;
                    string birthdateString = (ArrowsMenu.Items[6] as TextBox).Text;
                    string roleString = (ArrowsMenu.Items[7] as TextBox).Text;
                    string salaryString = (ArrowsMenu.Items[8] as TextBox).Text;
                    if (string.IsNullOrEmpty(password)
                        || string.IsNullOrEmpty(secondName) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(patronymic)
                        || string.IsNullOrEmpty(birthdateString) || string.IsNullOrEmpty(roleString) || string.IsNullOrEmpty(salaryString))
                    {
                        PrintError("Заполните все поля", 13, cursorTopWas);
                    }
                    else if (!DateTime.TryParse(birthdateString, out DateTime birthdate))
                    {
                        PrintError("Введите корректный формат даты рождения", 13, cursorTopWas);
                    }
                    else if (!decimal.TryParse(salaryString, out decimal salary))
                    {
                        PrintError("Введите вещественную зарплату", 13, cursorTopWas);
                    }
                    else
                    {
                        Employee updateEmployee = Employee.Employees.Where(e => e.Id == id).FirstOrDefault();
                        updateEmployee.Password = password;
                        updateEmployee.SecondName = secondName;
                        updateEmployee.FirstName = firstName;
                        updateEmployee.Patronymic = patronymic;
                        updateEmployee.Birthdate = birthdate;
                        updateEmployee.Salary = salary;
                        switch (updateEmployee.Role)
                        {
                            case Role.PersonnelOfficer:
                                (updateEmployee as PersonnelOfficer).Update();
                                break;
                            case Role.RestaurantManager:
                                (updateEmployee as RestaurantManager).Update();
                                break;
                            case Role.Chef:
                                (updateEmployee as Chef).Update();
                                break;
                            case Role.Accountant:
                                (updateEmployee as Accountant).Update();
                                break;
                            case Role.Waiter:
                                (updateEmployee as Waiter).Update();
                                break;
                        }
                        window = Window.PersonnelOfficerMainWindow;
                        (Authorization.SignedEmployee as PersonnelOfficer).PrintMainWindow();
                    }
                    break;
                case Key.Del:
                    id = int.Parse((ArrowsMenu.Items[0] as TextBox).Text);
                    Employee employee = Employee.Employees.Where(e => e.Id == id).FirstOrDefault();
                    switch (employee.Role)
                    {
                        case Role.PersonnelOfficer:
                            (employee as PersonnelOfficer).Delete();
                            break;
                        case Role.RestaurantManager:
                            (employee as RestaurantManager).Delete();
                            break;
                        case Role.Chef:
                            (employee as Chef).Delete();
                            break;
                        case Role.Accountant:
                            (employee as Accountant).Delete();
                            break;
                        case Role.Waiter:
                            (employee as Waiter).Delete();
                            break;
                    }
                    window = Window.PersonnelOfficerMainWindow;
                    (Authorization.SignedEmployee as PersonnelOfficer).PrintMainWindow();
                    break;
                case Key.Escape:
                    window = Window.PersonnelOfficerMainWindow;
                    (Authorization.SignedEmployee as PersonnelOfficer).PrintMainWindow();
                    break;
            }
            break;
        case Window.PersonnelOfficerSearchWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    Employee.SearchedEmployees.Clear();
                    string item = (ArrowsMenu.Items[ArrowsMenu.Current] as ListItem).Item;
                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine("Введите значение для поиска:");
                    string value = Console.ReadLine();
                    switch (item)
                    {
                        case "ID":
                            Employee.SearchedEmployees = Employee.Employees.Where(e => e.Id.ToString() == value).ToList();
                            break;
                        case "Логин":
                            Employee.SearchedEmployees = Employee.Employees.Where(e => e.Login == value).ToList();
                            break;
                        case "Фамилия":
                            Employee.SearchedEmployees = Employee.Employees.Where(e => e.SecondName == value).ToList();
                            break;
                        case "Имя":
                            Employee.SearchedEmployees = Employee.Employees.Where(e => e.FirstName == value).ToList();
                            break;
                        case "Отчество":
                            Employee.SearchedEmployees = Employee.Employees.Where(e => e.Patronymic == value).ToList();
                            break;
                        case "Роль":
                            if (Enum.TryParse(value, out Role role))
                            {
                                Employee.SearchedEmployees = Employee.Employees.Where(e => e.Role == role).ToList();
                            }
                            break;
                    }
                    window = Window.PersonnelOfficerSearchedMainWindow;
                    (Authorization.SignedEmployee as PersonnelOfficer).PrintSearchedMainWindow();
                    break;
                case Key.Escape:
                    window = Window.PersonnelOfficerMainWindow;
                    (Authorization.SignedEmployee as PersonnelOfficer).PrintMainWindow();
                    break;
            }
            break;
        case Window.PersonnelOfficerSearchedMainWindow:
            switch (key)
            {
                case Key.UpArrow:
                    if (Employee.SearchedEmployees.Count() > 0)
                    {
                        ArrowsMenu.Up();
                    }
                    break;
                case Key.DownArrow:
                    if (Employee.SearchedEmployees.Count() > 0)
                    {
                        ArrowsMenu.Down();
                    }
                    break;
                case Key.Enter:
                    if (Employee.SearchedEmployees.Count() > 0)
                    {
                        window = Window.PersonnelOfficerUpdateWindow;
                        (Authorization.SignedEmployee as PersonnelOfficer).PrintUpdateWindow(Employee.SearchedEmployees[ArrowsMenu.Current]);
                    }
                    break;
                case Key.F1:
                    window = Window.PersonnelOfficerAddWindow;
                    (Authorization.SignedEmployee as PersonnelOfficer).PrintAddWindow();
                    break;
                case Key.Escape:
                    window = Window.PersonnelOfficerMainWindow;
                    (Authorization.SignedEmployee as PersonnelOfficer).PrintMainWindow();
                    break;
            }
            break;
        case Window.RestaurantManagerMainWindow:
            switch (key)
            {
                case Key.UpArrow:
                    if (Note.Notes.Count() > 0)
                    {
                        ArrowsMenu.Up();
                    }
                    break;
                case Key.DownArrow:
                    if (Note.Notes.Count() > 0)
                    {
                        ArrowsMenu.Down();
                    }
                    break;
                case Key.Enter:
                    if (Note.Notes.Count() > 0)
                    {
                        window = Window.RestaurantManagerUpdateWindow;
                        (Authorization.SignedEmployee as RestaurantManager).PrintUpdateWindow(Note.Notes[ArrowsMenu.Current]);
                    }
                    break;
                case Key.F1:
                    window = Window.RestaurantManagerAddWindow;
                    (Authorization.SignedEmployee as RestaurantManager).PrintAddWindow();
                    break;
                case Key.F2:
                    window = Window.RestaurantManagerSearchWindow;
                    (Authorization.SignedEmployee as RestaurantManager).PrintSearchWindow();
                    break;
                case Key.Escape:
                    window = Window.AuthorizationWindow;
                    Authorization.SignOut();
                    break;
            }
            break;
        case Window.RestaurantManagerAddWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    (ArrowsMenu.Items[ArrowsMenu.Current] as TextBox).Input();
                    break;
                case Key.S:
                    int cursorTopWas = Console.CursorTop;
                    Console.SetCursorPosition(0, 9);
                    Console.Write("                                                                ");
                    Console.SetCursorPosition(0, cursorTopWas);
                    string idString = (ArrowsMenu.Items[0] as TextBox).Text;
                    string name = (ArrowsMenu.Items[1] as TextBox).Text;
                    string description = (ArrowsMenu.Items[2] as TextBox).Text;
                    string dateString = (ArrowsMenu.Items[3] as TextBox).Text;
                    string timeString = (ArrowsMenu.Items[4] as TextBox).Text;
                    if (string.IsNullOrEmpty(idString) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description)
                        || string.IsNullOrEmpty(dateString) || string.IsNullOrEmpty(timeString))
                    {
                        PrintError("Заполните все поля", 9, cursorTopWas);
                    }
                    else if (!int.TryParse(idString, out int id))
                    {
                        PrintError("Введите числовой ID", 9, cursorTopWas);
                    }
                    else if (Note.Notes.Where(e => e.Id == id).Count() > 0)
                    {
                        PrintError("Введите уникальный ID", 9, cursorTopWas);
                    }
                    else if (!DateOnly.TryParse(dateString, out DateOnly date))
                    {
                        PrintError("Введите корректный формат даты", 9, cursorTopWas);
                    }
                    else if (!TimeOnly.TryParse(timeString, out TimeOnly time))
                    {
                        PrintError("Введите корректный формат времени", 9, cursorTopWas);
                    }
                    else
                    {
                        new Note(id, name, description, date, time).Create();
                        window = Window.RestaurantManagerMainWindow;
                        (Authorization.SignedEmployee as RestaurantManager).PrintMainWindow();
                    }
                    break;
                case Key.Escape:
                    window = Window.RestaurantManagerMainWindow;
                    (Authorization.SignedEmployee as RestaurantManager).PrintMainWindow();
                    break;
            }
            break;
        case Window.RestaurantManagerUpdateWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    (ArrowsMenu.Items[ArrowsMenu.Current] as TextBox).Input();
                    break;
                case Key.S:
                    int cursorTopWas = Console.CursorTop;
                    Console.SetCursorPosition(0, 9);
                    Console.Write("                                                                ");
                    Console.SetCursorPosition(0, cursorTopWas);
                    int id = int.Parse((ArrowsMenu.Items[0] as TextBox).Text);
                    string name = (ArrowsMenu.Items[1] as TextBox).Text;
                    string description = (ArrowsMenu.Items[2] as TextBox).Text;
                    string dateString = (ArrowsMenu.Items[3] as TextBox).Text;
                    string timeString = (ArrowsMenu.Items[4] as TextBox).Text;
                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description)
                        || string.IsNullOrEmpty(dateString) || string.IsNullOrEmpty(timeString))
                    {
                        PrintError("Заполните все поля", 9, cursorTopWas);
                    }
                    else if (!DateOnly.TryParse(dateString, out DateOnly date))
                    {
                        PrintError("Введите корректный формат даты", 9, cursorTopWas);
                    }
                    else if (!TimeOnly.TryParse(timeString, out TimeOnly time))
                    {
                        PrintError("Введите корректный формат времени", 9, cursorTopWas);
                    }
                    else
                    {
                        Note updateNote = Note.Notes.Where(e => e.Id == id).FirstOrDefault();
                        updateNote.Name = name;
                        updateNote.Description = description;
                        updateNote.Date = date;
                        updateNote.Time = time;
                        updateNote.Update();
                        window = Window.RestaurantManagerMainWindow;
                        (Authorization.SignedEmployee as RestaurantManager).PrintMainWindow();
                    }
                    break;
                case Key.Del:
                    id = int.Parse((ArrowsMenu.Items[0] as TextBox).Text);
                    Note.Notes.Where(e => e.Id == id).FirstOrDefault().Delete();
                    window = Window.RestaurantManagerMainWindow;
                    (Authorization.SignedEmployee as RestaurantManager).PrintMainWindow();
                    break;
                case Key.Escape:
                    window = Window.RestaurantManagerMainWindow;
                    (Authorization.SignedEmployee as RestaurantManager).PrintMainWindow();
                    break;
            }
            break;
        case Window.RestaurantManagerSearchWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    Note.SearchedNotes.Clear();
                    string item = (ArrowsMenu.Items[ArrowsMenu.Current] as ListItem).Item;
                    Console.SetCursorPosition(0, 7);
                    Console.WriteLine("Введите значение для поиска:");
                    string value = Console.ReadLine();
                    switch (item)
                    {
                        case "ID":
                            Note.SearchedNotes = Note.Notes.Where(e => e.Id.ToString() == value).ToList();
                            break;
                        case "Название":
                            Note.SearchedNotes = Note.Notes.Where(e => e.Name == value).ToList();
                            break;
                        case "Дата":
                            if (DateOnly.TryParse(value, out DateOnly date))
                            {
                                Note.SearchedNotes = Note.Notes.Where(e => e.Date == date).ToList();
                            }
                            break;
                    }
                    window = Window.RestaurantManagerSearchedMainWindow;
                    (Authorization.SignedEmployee as RestaurantManager).PrintSearchedMainWindow();
                    break;
                case Key.Escape:
                    window = Window.RestaurantManagerMainWindow;
                    (Authorization.SignedEmployee as RestaurantManager).PrintMainWindow();
                    break;
            }
            break;
        case Window.RestaurantManagerSearchedMainWindow:
            switch (key)
            {
                case Key.UpArrow:
                    if (Note.SearchedNotes.Count() > 0)
                    {
                        ArrowsMenu.Up();
                    }
                    break;
                case Key.DownArrow:
                    if (Note.SearchedNotes.Count() > 0)
                    {
                        ArrowsMenu.Down();
                    }
                    break;
                case Key.Enter:
                    if (Note.SearchedNotes.Count() > 0)
                    {
                        window = Window.RestaurantManagerUpdateWindow;
                        (Authorization.SignedEmployee as RestaurantManager).PrintUpdateWindow(Note.SearchedNotes[ArrowsMenu.Current]);
                    }
                    break;
                case Key.F1:
                    window = Window.RestaurantManagerAddWindow;
                    (Authorization.SignedEmployee as RestaurantManager).PrintAddWindow();
                    break;
                case Key.Escape:
                    window = Window.RestaurantManagerMainWindow;
                    (Authorization.SignedEmployee as RestaurantManager).PrintMainWindow();
                    break;
            }
            break;
        case Window.ChefOfficerMainWindow:
            switch (key)
            {
                case Key.UpArrow:
                    if (Dish.Dishes.Count() > 0)
                    {
                        ArrowsMenu.Up();
                    }
                    break;
                case Key.DownArrow:
                    if (Dish.Dishes.Count() > 0)
                    {
                        ArrowsMenu.Down();
                    }
                    break;
                case Key.Enter:
                    if (Dish.Dishes.Count() > 0)
                    {
                        window = Window.ChefOfficerUpdateWindow;
                        (Authorization.SignedEmployee as Chef).PrintUpdateWindow(Dish.Dishes[ArrowsMenu.Current]);
                    }
                    break;
                case Key.F1:
                    window = Window.ChefOfficerAddWindow;
                    (Authorization.SignedEmployee as Chef).PrintAddWindow();
                    break;
                case Key.F2:
                    window = Window.ChefOfficerSearchWindow;
                    (Authorization.SignedEmployee as Chef).PrintSearchWindow();
                    break;
                case Key.Escape:
                    window = Window.AuthorizationWindow;
                    Authorization.SignOut();
                    break;
            }
            break;
        case Window.ChefOfficerAddWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    (ArrowsMenu.Items[ArrowsMenu.Current] as TextBox).Input();
                    break;
                case Key.S:
                    int cursorTopWas = Console.CursorTop;
                    Console.SetCursorPosition(0, 9);
                    Console.Write("                                                                ");
                    Console.SetCursorPosition(0, cursorTopWas);
                    string idString = (ArrowsMenu.Items[0] as TextBox).Text;
                    string name = (ArrowsMenu.Items[1] as TextBox).Text;
                    string categoryString = (ArrowsMenu.Items[2] as TextBox).Text;
                    string weightString = (ArrowsMenu.Items[3] as TextBox).Text;
                    string costString = (ArrowsMenu.Items[4] as TextBox).Text;
                    if (string.IsNullOrEmpty(idString) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(categoryString)
                                          || string.IsNullOrEmpty(weightString) || string.IsNullOrEmpty(costString))
                    {
                        PrintError("Заполните все поля", 9, cursorTopWas);
                    }
                    else if (!int.TryParse(idString, out int id))
                    {
                        PrintError("Введите числовой ID", 9, cursorTopWas);
                    }
                    else if (Dish.Dishes.Where(e => e.Id == id).Count() > 0)
                    {
                        PrintError("Введите уникальный ID", 9, cursorTopWas);
                    }
                    else if (Dish.Dishes.Where(e => e.Name == name).Count() > 0)
                    {
                        PrintError("Введите уникальное название", 9, cursorTopWas);
                    }
                    else if (!Enum.TryParse(categoryString, out Dish.DishCategory category))
                    {
                        PrintError("Введите корректную категорию", 9, cursorTopWas);
                    }
                    else if (!int.TryParse(weightString, out int weight))
                    {
                        PrintError("Введите числовой вес", 9, cursorTopWas);
                    }
                    else if (!decimal.TryParse(costString, out decimal cost))
                    {
                        PrintError("Введите вещественную цену", 9, cursorTopWas);
                    }
                    else
                    {
                        new Dish(id, name, category, weight, cost).Create();
                        window = Window.ChefOfficerMainWindow;
                        (Authorization.SignedEmployee as Chef).PrintMainWindow();
                    }
                    break;
                case Key.Escape:
                    window = Window.ChefOfficerMainWindow;
                    (Authorization.SignedEmployee as Chef).PrintMainWindow();
                    break;
            }
            break;
        case Window.ChefOfficerUpdateWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    (ArrowsMenu.Items[ArrowsMenu.Current] as TextBox).Input();
                    break;
                case Key.S:
                    int cursorTopWas = Console.CursorTop;
                    Console.SetCursorPosition(0, 9);
                    Console.Write("                                                                ");
                    Console.SetCursorPosition(0, cursorTopWas);
                    int id = int.Parse((ArrowsMenu.Items[0] as TextBox).Text);
                    string categoryString = (ArrowsMenu.Items[2] as TextBox).Text;
                    string weightString = (ArrowsMenu.Items[3] as TextBox).Text;
                    string costString = (ArrowsMenu.Items[4] as TextBox).Text;
                    if (string.IsNullOrEmpty(categoryString) || string.IsNullOrEmpty(weightString) || string.IsNullOrEmpty(costString))
                    {
                        PrintError("Заполните все поля", 9, cursorTopWas);
                    }
                    else if (!Enum.TryParse(categoryString, out Dish.DishCategory category))
                    {
                        PrintError("Введите корректную категорию", 9, cursorTopWas);
                    }
                    else if (!int.TryParse(weightString, out int weight))
                    {
                        PrintError("Введите числовой вес", 9, cursorTopWas);
                    }
                    else if (!decimal.TryParse(costString, out decimal cost))
                    {
                        PrintError("Введи" +
                            "те вещественную цену", 9, cursorTopWas);
                    }
                    else
                    {
                        Dish updateDish = Dish.Dishes.Where(e => e.Id == id).FirstOrDefault();
                        updateDish.Category = category;
                        updateDish.Weight = weight;
                        updateDish.Cost = cost;
                        updateDish.Update();
                        window = Window.ChefOfficerMainWindow;
                        (Authorization.SignedEmployee as Chef).PrintMainWindow();
                    }
                    break;
                case Key.Del:
                    id = int.Parse((ArrowsMenu.Items[0] as TextBox).Text);
                    Dish dish = Dish.Dishes.Where(e => e.Id == id).FirstOrDefault();
                    dish.Delete();
                    window = Window.ChefOfficerMainWindow;
                    (Authorization.SignedEmployee as Chef).PrintMainWindow();
                    break;
                case Key.Escape:
                    window = Window.ChefOfficerMainWindow;
                    (Authorization.SignedEmployee as Chef).PrintMainWindow();
                    break;
            }
            break;
        case Window.ChefOfficerSearchWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    Dish.SearchedDishes.Clear();
                    string item = (ArrowsMenu.Items[ArrowsMenu.Current] as ListItem).Item;
                    Console.SetCursorPosition(0, 7);
                    Console.WriteLine("Введите значение для поиска:");
                    string value = Console.ReadLine();
                    switch (item)
                    {
                        case "ID":
                            Dish.SearchedDishes = Dish.Dishes.Where(e => e.Id.ToString() == value).ToList();
                            break;
                        case "Название":
                            Dish.SearchedDishes = Dish.Dishes.Where(e => e.Name == value).ToList();
                            break;
                        case "Категория":
                            if (Enum.TryParse(value, out Dish.DishCategory category))
                            {
                                Dish.SearchedDishes = Dish.Dishes.Where(e => e.Category == category).ToList();
                            }
                            break;
                    }
                    window = Window.ChefOfficerSearchedMainWindow;
                    (Authorization.SignedEmployee as Chef).PrintSearchedMainWindow();
                    break;
                case Key.Escape:
                    window = Window.ChefOfficerMainWindow;
                    (Authorization.SignedEmployee as Chef).PrintMainWindow();
                    break;
            }
            break;
        case Window.ChefOfficerSearchedMainWindow:
            switch (key)
            {
                case Key.UpArrow:
                    if (Dish.SearchedDishes.Count() > 0)
                    {
                        ArrowsMenu.Up();
                    }
                    break;
                case Key.DownArrow:
                    if (Dish.SearchedDishes.Count() > 0)
                    {
                        ArrowsMenu.Down();
                    }
                    break;
                case Key.Enter:
                    if (Dish.SearchedDishes.Count() > 0)
                    {
                        window = Window.ChefOfficerUpdateWindow;
                        (Authorization.SignedEmployee as Chef).PrintUpdateWindow(Dish.SearchedDishes[ArrowsMenu.Current]);
                    }
                    break;
                case Key.F1:
                    window = Window.ChefOfficerAddWindow;
                    (Authorization.SignedEmployee as Chef).PrintAddWindow();
                    break;
                case Key.Escape:
                    window = Window.ChefOfficerMainWindow;
                    (Authorization.SignedEmployee as Chef).PrintMainWindow();
                    break;
            }
            break;
        case Window.AccountantMainWindow:
            switch (key)
            {
                case Key.UpArrow:
                    if (Pay.Pays.Count() > 0)
                    {
                        ArrowsMenu.Up();
                    }
                    break;
                case Key.DownArrow:
                    if (Pay.Pays.Count() > 0)
                    {
                        ArrowsMenu.Down();
                    }
                    break;
                case Key.Enter:
                    if (Pay.Pays.Count() > 0)
                    {
                        window = Window.AccountantUpdateWindow;
                        (Authorization.SignedEmployee as Accountant).PrintUpdateWindow(Pay.Pays[ArrowsMenu.Current]);
                    }
                    break;
                case Key.F1:
                    window = Window.AccountantAddWindow;
                    (Authorization.SignedEmployee as Accountant).PrintAddWindow();
                    break;
                case Key.F2:
                    window = Window.AccountantSearchWindow;
                    (Authorization.SignedEmployee as Accountant).PrintSearchWindow();
                    break;
                case Key.Escape:
                    window = Window.AuthorizationWindow;
                    Authorization.SignOut();
                    break;
            }
            break;
        case Window.AccountantAddWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    (ArrowsMenu.Items[ArrowsMenu.Current] as TextBox).Input();
                    break;
                case Key.S:
                    int cursorTopWas = Console.CursorTop;
                    Console.SetCursorPosition(0, 10);
                    Console.Write("                                                                ");
                    Console.SetCursorPosition(0, cursorTopWas);
                    string idString = (ArrowsMenu.Items[0] as TextBox).Text;
                    string name = (ArrowsMenu.Items[1] as TextBox).Text;
                    string sumString = (ArrowsMenu.Items[2] as TextBox).Text;
                    string dateString = (ArrowsMenu.Items[3] as TextBox).Text;
                    string timeString = (ArrowsMenu.Items[4] as TextBox).Text;
                    string isIncreaseString = (ArrowsMenu.Items[5] as TextBox).Text;
                    if (string.IsNullOrEmpty(idString) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(sumString)
                        || string.IsNullOrEmpty(dateString) || string.IsNullOrEmpty(timeString) || string.IsNullOrEmpty(isIncreaseString))
                    {
                        PrintError("Заполните все поля", 10, cursorTopWas);
                    }
                    else if (!int.TryParse(idString, out int id))
                    {
                        PrintError("Введите числовой ID", 10, cursorTopWas);
                    }
                    else if (Pay.Pays.Where(e => e.Id == id).Count() > 0)
                    {
                        PrintError("Введите уникальный ID", 10, cursorTopWas);
                    }
                    else if (!decimal.TryParse(sumString, out decimal sum))
                    {
                        PrintError("Введите числовую сумму", 10, cursorTopWas);
                    }
                    else if (!DateOnly.TryParse(dateString, out DateOnly date))
                    {
                        PrintError("Введите корректный формат даты", 10, cursorTopWas);
                    }
                    else if (!TimeOnly.TryParse(timeString, out TimeOnly time))
                    {
                        PrintError("Введите корректный формат времени", 10, cursorTopWas);
                    }
                    else if (!bool.TryParse(isIncreaseString, out bool isIncrease))
                    {
                        PrintError("Введите корректное поле выплата?", 10, cursorTopWas);
                    }
                    else
                    {
                        new Pay(id, name, sum, date, time, isIncrease).Create();
                        window = Window.AccountantMainWindow;
                        (Authorization.SignedEmployee as Accountant).PrintMainWindow();
                    }
                    break;
                case Key.Escape:
                    window = Window.AccountantMainWindow;
                    (Authorization.SignedEmployee as Accountant).PrintMainWindow();
                    break;
            }
            break;
        case Window.AccountantUpdateWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    (ArrowsMenu.Items[ArrowsMenu.Current] as TextBox).Input();
                    break;
                case Key.S:
                    int cursorTopWas = Console.CursorTop;
                    Console.SetCursorPosition(0, 10);
                    Console.Write("                                                                ");
                    Console.SetCursorPosition(0, cursorTopWas);
                    int id = int.Parse((ArrowsMenu.Items[0] as TextBox).Text);
                    string name = (ArrowsMenu.Items[1] as TextBox).Text;
                    string sumString = (ArrowsMenu.Items[2] as TextBox).Text;
                    string dateString = (ArrowsMenu.Items[3] as TextBox).Text;
                    string timeString = (ArrowsMenu.Items[4] as TextBox).Text;
                    string isIncreaseString = (ArrowsMenu.Items[5] as TextBox).Text;
                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(sumString)
                        || string.IsNullOrEmpty(dateString) || string.IsNullOrEmpty(timeString) || string.IsNullOrEmpty(isIncreaseString))
                    {
                        PrintError("Заполните все поля", 10, cursorTopWas);
                    }
                    else if (!decimal.TryParse(sumString, out decimal sum))
                    {
                        PrintError("Введите числовую сумму", 10, cursorTopWas);
                    }
                    else if (!DateOnly.TryParse(dateString, out DateOnly date))
                    {
                        PrintError("Введите корректный формат даты", 10, cursorTopWas);
                    }
                    else if (!TimeOnly.TryParse(timeString, out TimeOnly time))
                    {
                        PrintError("Введите корректный формат времени", 10, cursorTopWas);
                    }
                    else if (!bool.TryParse(isIncreaseString, out bool isIncrease))
                    {
                        PrintError("Введите корректное поле выплата?", 10, cursorTopWas);
                    }
                    else
                    {
                        Pay updatePay = Pay.Pays.Where(e => e.Id == id).FirstOrDefault();
                        updatePay.Name = name;
                        updatePay.Sum = sum;
                        updatePay.Date = date;
                        updatePay.Time = time;
                        updatePay.IsIncrease = isIncrease;
                        updatePay.Update();
                        window = Window.AccountantMainWindow;
                        (Authorization.SignedEmployee as Accountant).PrintMainWindow();
                    }
                    break;
                case Key.Del:
                    id = int.Parse((ArrowsMenu.Items[0] as TextBox).Text);
                    Pay.Pays.Where(e => e.Id == id).FirstOrDefault().Delete();
                    window = Window.AccountantMainWindow;
                    (Authorization.SignedEmployee as Accountant).PrintMainWindow();
                    break;
                case Key.Escape:
                    window = Window.AccountantMainWindow;
                    (Authorization.SignedEmployee as Accountant).PrintMainWindow();
                    break;
            }
            break;
        case Window.AccountantSearchWindow:
            switch (key)
            {
                case Key.UpArrow:
                    ArrowsMenu.Up();
                    break;
                case Key.DownArrow:
                    ArrowsMenu.Down();
                    break;
                case Key.Enter:
                    Pay.SearchedPays.Clear();
                    string item = (ArrowsMenu.Items[ArrowsMenu.Current] as ListItem).Item;
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Введите значение для поиска:");
                    string value = Console.ReadLine();
                    switch (item)
                    {
                        case "ID":
                            Pay.SearchedPays = Pay.Pays.Where(p => p.Id.ToString() == value).ToList();
                            break;
                        case "Название":
                            Pay.SearchedPays = Pay.Pays.Where(p => p.Name == value).ToList();
                            break;
                        case "Дата":
                            if (DateOnly.TryParse(value, out DateOnly date))
                            {
                                Pay.SearchedPays = Pay.Pays.Where(p => p.Date == date).ToList();
                            }
                            break;
                        case "Выплата?":
                            if (bool.TryParse(value, out bool isIncrease))
                            {
                                Pay.SearchedPays = Pay.Pays.Where(p => p.IsIncrease == isIncrease).ToList();
                            }
                            break;
                    }
                    window = Window.AccountantSearchedMainWindow;
                    (Authorization.SignedEmployee as Accountant).PrintSearchedMainWindow();
                    break;
                case Key.Escape:
                    window = Window.AccountantMainWindow;
                    (Authorization.SignedEmployee as Accountant).PrintMainWindow();
                    break;
            }
            break;
        case Window.AccountantSearchedMainWindow:
            switch (key)
            {
                case Key.UpArrow:
                    if (Pay.SearchedPays.Count() > 0)
                    {
                        ArrowsMenu.Up();
                    }
                    break;
                case Key.DownArrow:
                    if (Pay.SearchedPays.Count() > 0)
                    {
                        ArrowsMenu.Down();
                    }
                    break;
                case Key.Enter:
                    if (Pay.SearchedPays.Count() > 0)
                    {
                        window = Window.AccountantUpdateWindow;
                        (Authorization.SignedEmployee as Accountant).PrintUpdateWindow(Pay.SearchedPays[ArrowsMenu.Current]);
                    }
                    break;
                case Key.F1:
                    window = Window.AccountantAddWindow;
                    (Authorization.SignedEmployee as Accountant).PrintAddWindow();
                    break;
                case Key.Escape:
                    window = Window.AccountantMainWindow;
                    (Authorization.SignedEmployee as Accountant).PrintMainWindow();
                    break;
            }
            break;
        case Window.WaiterMainWindow:
            switch (key)
            {
                case Key.UpArrow:
                    if (Order.Orders.Count() > 0)
                    {
                        ArrowsMenu.Up();
                    }
                    break;
                case Key.DownArrow:
                    if (Order.Orders.Count() > 0)
                    {
                        ArrowsMenu.Down();
                    }
                    break;
                case Key.F1:
                    window = Window.WaiterAddWindow;
                    (Authorization.SignedEmployee as Waiter).PrintAddWindow();
                    break;
                case Key.Escape:
                    window = Window.AuthorizationWindow;
                    Authorization.SignOut();
                    break;
            }
            break;
        case Window.WaiterAddWindow:
            switch (key)
            {
                case Key.UpArrow:
                    if (Order.NewOrder.DishList.Count > 0)
                    {
                        ArrowsMenu.Up();
                    }
                    break;
                case Key.DownArrow:
                    if (Order.NewOrder.DishList.Count > 0)
                    {
                        ArrowsMenu.Down();
                    }
                    break;
                case Key.S:
                    if (Order.NewOrder.TotalCost > 0)
                    {
                        Order.NewOrder.Date = DateOnly.Parse(DateTime.Now.ToShortDateString());
                        Order.NewOrder.Time = TimeOnly.Parse(DateTime.Now.ToShortTimeString());
                        Order.NewOrder.DishList.RemoveAll(d => d.Count == 0);
                        Order.NewOrder.Create();
                        new Pay(Pay.Pays.Max(p => p.Id) + 1, $"Заказ №{Order.NewOrder.Id}",Order.NewOrder.TotalCost, Order.NewOrder.Date, Order.NewOrder.Time, true).Create();
                        window = Window.WaiterMainWindow;
                        (Authorization.SignedEmployee as Waiter).PrintMainWindow();
                    }
                    else
                    {
                        PrintError("Добавьте блюда в заказ", Order.NewOrder.DishList.Count + 5, Console.CursorTop);
                    }
                    break;
                case Key.Plus:
                    if (Order.NewOrder.DishList[ArrowsMenu.Current].Count < 10)
                    {
                        Order.NewOrder.DishList[ArrowsMenu.Current].Count++;
                        Order.NewOrder.DishList[ArrowsMenu.Current].TotalCost = Order.NewOrder.DishList[ArrowsMenu.Current].Dish.Cost * Order.NewOrder.DishList[ArrowsMenu.Current].Count;
                    }
                     (Authorization.SignedEmployee as Waiter).UpdateAddWindow(Console.CursorTop);
                    break;
                case Key.Minus:
                    if (Order.NewOrder.DishList[ArrowsMenu.Current].Count > 0)
                    {
                        Order.NewOrder.DishList[ArrowsMenu.Current].Count--;
                        Order.NewOrder.DishList[ArrowsMenu.Current].TotalCost = Order.NewOrder.DishList[ArrowsMenu.Current].Dish.Cost * Order.NewOrder.DishList[ArrowsMenu.Current].Count;
                    }
                    (Authorization.SignedEmployee as Waiter).UpdateAddWindow(Console.CursorTop);
                    break;
                case Key.Escape:
                    Order.NewOrder = null;
                    window = Window.WaiterMainWindow;
                    (Authorization.SignedEmployee as Waiter).PrintMainWindow();
                    break;
            }
            break;
    }
}

static void PrintError(string error, int cursorTopError, int cursorTopWas)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.SetCursorPosition(0, cursorTopError);
    Console.Write(error);
    Console.SetCursorPosition(0, cursorTopWas);
    Console.ResetColor();
}